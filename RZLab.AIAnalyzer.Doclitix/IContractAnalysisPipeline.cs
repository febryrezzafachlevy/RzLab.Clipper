using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZLab.AIAnalyzer.Doclitix
{
    public interface IContractAnalysisPipeline
    {
        Task<AnalysisResultModel> AnalyzeDocumentAsync(DocumentDataModel doc, IEnumerable<string> keywords, int maxCandidates = 100);
    }
}
