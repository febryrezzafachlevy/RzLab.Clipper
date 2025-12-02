#nullable disable
using RZLab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZLab.Clipper.Core.DocumentLegal
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
    }

    public class MetadataModel
    {
        public int page_count { get; set; }
        public long file_size_kb { get; set; }
    }

    public class AnalysisResultModel
    {
        public string risk_level { get; set; }
        public List<string> risks { get; set; }
        public List<ClauseModel> clauses { get; set; }
        public List<string> recommendations { get; set; }
        public string summary { get; set; }

        public AnalysisResultModel()
        {
            risks = new List<string>();
            clauses = new List<ClauseModel>();
            recommendations = new List<string>();
        }
    }
    public class ClauseModel
    {
        public string title { get; set; }
        public string content { get; set; }
        public string risk { get; set; }
    }

    public class PDFMetadata
    {
        public string text_raw { get; set; }
        public int page_count { get; set; }
    }
}
