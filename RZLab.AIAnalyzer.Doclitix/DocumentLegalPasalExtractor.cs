using System.Text.RegularExpressions;

namespace RZLab.AIAnalyzer.Doclitix
{
    public static class DocumentLegalPasalExtractor
    {
        // detect lines starting with "PASAL 1", "Pasal I", "BAB I", "Clause 1" etc.
        private static readonly Regex pasalRegex = new Regex(
            @"(?im)^(?:\s*)(pasal|bab|bagian|clause|article)\s+([0-9IVXLC]+)\b.*",
            RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public static List<ClauseBlockModel> ExtractPasal(List<PDFContent> pages)
        {
            var result = new List<ClauseBlockModel>();

            foreach (var pageText in pages)
            {
                if (string.IsNullOrWhiteSpace(pageText.Content)) continue;

                // Find all pasal headings (by index)
                var matches = pasalRegex.Matches(pageText.Content);
                if (matches.Count == 0)
                {
                    // if no pasal headings, consider whole page as single block with no title
                    result.Add(new ClauseBlockModel
                    {
                        Title = $"Page {pageText.PageNumber}",
                        Content = pageText.Content.Trim(),
                        PageNumber = pageText.PageNumber
                    });
                    continue;
                }

                var indices = matches.Cast<Match>().Select(m => new { m.Index, Title = m.Value.Trim() }).ToList();
                for (int mi = 0; mi < indices.Count; mi++)
                {
                    int start = indices[mi].Index;
                    int end = (mi + 1 < indices.Count) ? indices[mi + 1].Index : pageText.Content.Length;
                    var block = pageText.Content.Substring(start, end - start).Trim();

                    result.Add(new ClauseBlockModel
                    {
                        Title = indices[mi].Title,
                        Content = block,
                        PageNumber = pageText.PageNumber
                    });
                }
            }

            return result;
        }
    }
}
