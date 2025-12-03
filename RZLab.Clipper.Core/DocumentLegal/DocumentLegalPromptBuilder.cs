using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RZLab.Clipper.Core.DocumentLegal
{
    public static class DocumentLegalPromptBuilder
    {
        public static string BuildSummarizeCandidatesPrompt(List<CandidateClause> candidates)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Ringkas setiap potongan berikut menjadi 1-2 kalimat per potongan. Kembalikan sebagai JSON array of objects {page:int, snippet:string, summary:string}.");
            sb.AppendLine("Jangan menulis teks lain di luar JSON.");
            sb.AppendLine();
            sb.AppendLine("INPUT:");
            int i = 1;
            foreach (var c in candidates)
            {
                sb.AppendLine($"--- ITEM {i} [Page {c.PageNumber}] ---");
                sb.AppendLine(c.Snippet);
                i++;
            }
            return sb.ToString();
        }

        public static string BuildFinalAnalysisPrompt(string combinedSummaries, string fileName)
        {
            return @$"
            You are an advanced legal analysis AI.

            Your task is to analyze the contract summaries below and generate a structured JSON output.
            You MUST respond with a VALID JSON OBJECT only.

            Return EXACT JSON with the following schema:

            {{
              ""risk_level"": ""low | medium | high"",
              ""risks"": [
                {{
                  ""level"": ""low | medium | high"",
                  ""description"": ""short explanation of the risk"",
                  ""page"": 0,
                  ""clause_title"": """"
                }}
              ],
              ""clauses"": [
                {{
                  ""title"": ""name of clause or pasal"",
                  ""content"": ""cleaned text of the clause"",
                  ""risk"": ""low | medium | high"",
                  ""page"": 0
                }}
              ],
              ""recommendations"": [
                ""short actionable recommendations""
              ],
              ""summary"": ""1–3 sentence human-readable summary of the document""
            }}

            VERY IMPORTANT RULES:
            1. ALWAYS return valid JSON.
            2. NEVER include commentary, explanation, or natural language outside the JSON.
            3. If page number cannot be inferred, set ""page"": 0.
            4. If clause title is unknown, set ""clause_title"": """".
            5. If clause risk cannot be determined, set ""risk"": ""medium"".
            6. All text MUST be generated inside the JSON only.
            7. The JSON must NOT be wrapped in code fences.
            8. Use your best judgment to classify risk levels.
            9. The document name is: {fileName}

            CONTENT TO ANALYZE:
            {combinedSummaries}
            ";
        }

    }

    public class ContractAnalysisPipeline
    {
        private readonly DocumentLegalAnalyzer finalAnalyzer;
        private readonly DocumentLegalAnalyzer cheapAnalyzer;
        private readonly DocumentLegalStorageService storage;
        private readonly string apiKey;
        private readonly string cheapModel;
        private readonly string finalModel;

        public ContractAnalysisPipeline()
        {
            var config = ConfigLoader.Load();
            this.apiKey = config["OpenAI:ApiKey"];
            this.cheapModel = config["OpenAI:Model"];
            this.finalModel = config["OpenAI:Model"];
            cheapAnalyzer = new DocumentLegalAnalyzer(apiKey, this.finalModel);
            finalAnalyzer = new DocumentLegalAnalyzer(apiKey, this.finalModel);
            storage = new DocumentLegalStorageService();
        }

        /// <summary>
        /// Full pipeline:
        /// 1. extract pages
        /// 2. extract pasal
        /// 3. select candidates by keywords
        /// 4. summarize candidates using cheap model (batch)
        /// 5. final analysis using final model on combined summaries
        /// 6. parse result, save to JSON storage
        /// </summary>
        public async Task<AnalysisResultModel> AnalyzeDocumentAsync(DocumentDataModel doc, IEnumerable<string> keywords, int maxCandidates = 100)
        {
            // 1. extract pages
            var pages = PdfUtils.ExtractPagesText(doc.file_path);
            var (pageCount, sizeKb) = PdfUtils.GetBasicMetadata(doc.file_path);
            doc.metadata = new MetadataModel { page_count = pageCount, file_size_kb = sizeKb };

            // 2. extract pasal blocks
            var pasalBlocks = DocumentLegalPasalExtractor.ExtractPasal(pages);

            // 3. select candidates
            var candidates = DocumentLegalImportantClauseExtractor.ExtractCandidatesFromPasal(pasalBlocks, keywords, maxCandidates);

            // fallback if no candidates found
            if (candidates.Count == 0)
            {
                candidates = DocumentLegalImportantClauseExtractor.ExtractCandidatesFromPages(pages, keywords, contextLines: 2, minMatchKeywords: 1, maxCandidatesPerDoc: maxCandidates);
            }

            // 4. summarize candidates with cheap model (we ask to return JSON array)
            List<(int page, string snippet, string summary)> summaries = new();
            if (candidates.Count > 0)
            {
                // batch candidates into chunks to avoid token overflow
                var chunks = Chunk(candidates, 20);
                foreach (var chunk in chunks)
                {
                    var prompt = DocumentLegalPromptBuilder.BuildSummarizeCandidatesPrompt(chunk);
                    var jsonStr = await cheapAnalyzer.AnalyzeAsync(cheapModel, prompt);

                    // parse JSON array returned by cheap model
                    try
                    {
                        var docJson = JsonDocument.Parse(jsonStr);
                        if (docJson.RootElement.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var el in docJson.RootElement.EnumerateArray())
                            {
                                int page = el.GetProperty("page").GetInt32();
                                string snippet = el.GetProperty("snippet").GetString();
                                string summary = el.GetProperty("summary").GetString();
                                summaries.Add((page, snippet, summary));
                            }
                        }
                        else
                        {
                            // fallback: create summaries from chunk using naive approach
                            foreach (var c in chunk)
                                summaries.Add((c.PageNumber, c.Snippet, Shorten(c.Snippet, 200)));
                        }
                    }
                    catch
                    {
                        foreach (var c in chunk)
                            summaries.Add((c.PageNumber, c.Snippet, Shorten(c.Snippet, 200)));
                    }
                }
            }

            // 5. combine summaries and send to final model
            var combined = string.Join("\n\n---\n\n", summaries.Select(s => $"[Page {s.page}] {s.summary}"));

            if (string.IsNullOrWhiteSpace(combined))
            {
                // if nothing found - still run a safe fallback on entire document header
                combined = string.Join("\n\n", pages.Take(3)); // first 3 pages
            }

            var finalPrompt = DocumentLegalPromptBuilder.BuildFinalAnalysisPrompt(combined, doc.file_name);
            var finalJson = await finalAnalyzer.AnalyzeAsync(finalModel, finalPrompt);

            try
            {
                var analysis = JsonSerializer.Deserialize<AnalysisResultModel>(finalJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // attach page to clause items if possible (try to match snippet to summaries)
                foreach (var clause in analysis.clauses)
                {
                    // try to find matching summary snippet for page mapping
                    var match = summaries.FirstOrDefault(s => clause.content != null && s.summary != null && s.summary.Length > 10 && clause.content.Contains(s.summary.Substring(0, Math.Min(80, s.summary.Length))));
                    if (match != default) clause.page = match.page;
                }

                // 7. save to JSON storage
                storage.SaveAnalysis(doc.document_id, analysis);

                return analysis;
            }
            catch (Exception ex)
            {
                throw new Exception("JSON parsing error: " + ex.Message);
            }
        }

        private static IEnumerable<List<T>> Chunk<T>(List<T> source, int chunkSize)
        {
            for (int i = 0; i < source.Count; i += chunkSize)
            {
                yield return source.GetRange(i, Math.Min(chunkSize, source.Count - i));
            }
        }

        private static string Shorten(string s, int max)
        {
            if (string.IsNullOrEmpty(s)) return s;
            if (s.Length <= max) return s;
            return s.Substring(0, max) + "...";
        }
    }
}
