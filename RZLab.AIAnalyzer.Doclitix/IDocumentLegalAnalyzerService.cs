using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZLab.AIAnalyzer.Doclitix
{
    public interface IDocumentLegalAnalyzerService
    {
        Task<string> AnalyzeAsync(string model, string prompt);
        Task<string> AnalyzeAsync(string prompt);
    }
}
