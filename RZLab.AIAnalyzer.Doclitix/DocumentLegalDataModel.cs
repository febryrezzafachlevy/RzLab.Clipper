#nullable disable
using System.Linq;

namespace RZLab.AIAnalyzer.Doclitix
{
    public class DocumentDatabaseModel
    {
        public List<DocumentDataModel> Documents { get; set; } = new();
    }
    public class DocumentDataModel
    {
        public string document_id { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public DateTime uploaded_at { get; set; }
        public string document_type { get; set; }

        public MetadataModel metadata { get; set; }
        public string raw_text { get; set; }

        public AnalysisResultModel analysis_result { get; set; }

        public string ConvertToString(List<PDFContent> contents)
        {
            return string.Join(Environment.NewLine, contents.Select(x => x.Content));
        }
    }

    public class MetadataModel
    {
        public int page_count { get; set; }
        public long file_size_kb { get; set; }
    }

    public class AnalysisResultModel
    {
        public string risk_level { get; set; }
        public List<RiskModel> risks { get; set; }
        public List<ClauseModel> clauses { get; set; }
        public List<string> recommendations { get; set; }
        public string summary { get; set; }

        public AnalysisResultModel()
        {
            risks = new List<RiskModel>();
            clauses = new List<ClauseModel>();
            recommendations = new List<string>();
        }
    }
    public class ClauseModel
    {
        public string title { get; set; }
        public string content { get; set; }
        public string risk { get; set; }
        // optional page reference
        public int page { get; set; }
    }
    public class RiskModel
    {
        public string level { get; set; }        // "high", "medium", "low"
        public string description { get; set; }  // penjelasan risiko
        public int? page { get; set; }           // optional: lokasi risiko di dokumen
        public string clause_title { get; set; } // optional: title pasal terkait
    }
    public class PDFMetadata
    {
        public List<PDFContent> Contents { get; set; }
        public int TotalPage { get; set; }
        public long FileSize { get; set; }
    }
    public class PDFContent
    {
        public string Content { get; set; }
        public int PageNumber { get; set; }
    }
    public class CandidateClause
    {
        public int PageNumber { get; set; }         // 1-based
        public string Snippet { get; set; }
        public List<string> MatchedKeywords { get; set; } = new List<string>();
        public double Score => MatchedKeywords.Count; // simple score
    }
    public class ClauseBlockModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int PageNumber { get; set; }
    }
    public class PDFHighlight
    {
        public string text { get; set; }
        public string color { get; set; }
    }
    public class DoclitixSetting
    {
        public string ApiKey { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
    }
}
