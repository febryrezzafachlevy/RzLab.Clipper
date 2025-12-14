namespace RZLab.AIAnalyzer.Doclitix
{
    public interface IDocumentLegalAnalyzerService
    {
        Task<string> AnalyzeAsync(string model, string prompt);
        Task<AnalysisResultModel> AnalyzeAsync(string prompt);
    }
}
