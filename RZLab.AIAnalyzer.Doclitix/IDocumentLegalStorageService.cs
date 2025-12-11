using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZLab.AIAnalyzer.Doclitix
{
    public interface IDocumentLegalStorageService
    {
        List<DocumentDataModel> LoadAll();
        void Save(DocumentDataModel doc);
        void SaveAnalysis(string documentId, AnalysisResultModel analysis);
        IEnumerable<string> DefaultKeywords();
    }
}
