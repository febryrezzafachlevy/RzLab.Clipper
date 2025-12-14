using System.Text.Json;

namespace RZLab.AIAnalyzer.Doclitix
{
    public class ContractAnalysisPipeline : IContractAnalysisPipeline
    {
        private readonly IDocumentLegalAnalyzerService _analyzerService;
        private readonly IDocumentLegalStorageService _storageService;
        private readonly DoclitixSetting _doclitixSetting;

        public ContractAnalysisPipeline(DoclitixSetting doclitixSetting, IDocumentLegalAnalyzerService analyzerService, IDocumentLegalStorageService storageService)
        {
            _doclitixSetting = doclitixSetting;
            _analyzerService = analyzerService;
            _storageService = storageService;
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
            var pdfMetadata = PDFUtility.ExtractPagesText(doc.file_path);

            // 2. extract pasal blocks
            var pasalBlocks = DocumentLegalPasalExtractor.ExtractPasal(pdfMetadata.Contents);

            // 3. select candidates
            var candidates = DocumentLegalImportantClauseExtractor.ExtractCandidatesFromPasal(pasalBlocks, keywords, maxCandidates);

            // fallback if no candidates found
            if (candidates.Count == 0)
            {
                candidates = DocumentLegalImportantClauseExtractor.ExtractCandidatesFromPages(pdfMetadata.Contents, keywords, contextLines: 2, minMatchKeywords: 1, maxCandidatesPerDoc: maxCandidates);
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
                    var jsonStr = await _analyzerService.AnalyzeAsync(_doclitixSetting.Model, prompt);

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
                combined = string.Join("\n\n", pdfMetadata.Contents.Take(3)); // first 3 pages
            }

            var finalPrompt = DocumentLegalPromptBuilder.BuildFinalAnalysisPrompt(combined, doc.file_name);
            var finalJson = await _analyzerService.AnalyzeAsync(_doclitixSetting.Model, finalPrompt);

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
                _storageService.SaveAnalysis(doc.document_id, analysis);

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
