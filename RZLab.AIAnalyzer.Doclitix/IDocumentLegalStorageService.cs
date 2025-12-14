namespace RZLab.AIAnalyzer.Doclitix
{
    public interface IDocumentLegalStorageService
    {
        List<DocumentDataModel> LoadAll();
        List<CandidateClause> ExtractCandidates(List<string> pages, IEnumerable<string>? keywords = null, int contextLines = 2, int minMatchKeywords = 1, int maxCandidatesPerDoc = 200);
        void Save(DocumentDataModel doc);
        void SaveAnalysis(string documentId, AnalysisResultModel analysis);
    }
}
