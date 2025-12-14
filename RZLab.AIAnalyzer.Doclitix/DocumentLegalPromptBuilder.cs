using System.Text;

namespace RZLab.AIAnalyzer.Doclitix
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
}
