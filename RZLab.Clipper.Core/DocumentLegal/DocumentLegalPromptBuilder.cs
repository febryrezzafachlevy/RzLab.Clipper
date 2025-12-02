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
            // instruct model to return AnalysisResultModel JSON
            var instruction = @$"
            You are a legal AI assistant. Analyze the following condensed clause summaries from document: {fileName}.
            Return a valid JSON object with this exact schema:
            {{
              ""risk_level"": ""low|medium|high"",
              ""risks"": [""...""],
              ""clauses"": [ {{ ""title"": ""Pasal X"", ""content"": ""..."", ""risk"": ""low|medium|high"", ""page"": 1 }} ],
              ""recommendations"": [""...""],
              ""summary"": ""short summary (1-3 sentences)""
            }}
            Respond *only* with the JSON object. No explanation.
            ----
            CONTENT:
            {combinedSummaries}
            ";
            return instruction;
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

            // 6. parse finalJson => AnalysisResultModel
            AnalysisResultModel analysis;
            try
            {
                analysis = JsonSerializer.Deserialize<AnalysisResultModel>(finalJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                // fallback: create minimal analysis
                analysis = new AnalysisResultModel
                {
                    summary = "Analysis failed to parse. See raw output.",
                    risks = new List<string> { "Parsing error: " + ex.Message },
                    recommendations = new List<string>(),
                    clauses = new List<ClauseModel>()
                };
            }

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
