using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RZLab.Clipper.Core.DocumentLegal
{
    public static class DocumentLegalPasalExtractor
    {
        // detect lines starting with "PASAL 1", "Pasal I", "BAB I", "Clause 1" etc.
        private static readonly Regex pasalRegex = new Regex(
            @"(?im)^(?:\s*)(pasal|bab|bagian|clause|article)\s+([0-9IVXLC]+)\b.*",
            RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public static List<ClauseBlockModel> ExtractPasal(List<string> pages)
        {
            var result = new List<ClauseBlockModel>();

            for (int pageIndex = 0; pageIndex < pages.Count; pageIndex++)
            {
                string pageText = pages[pageIndex] ?? "";
                if (string.IsNullOrWhiteSpace(pageText)) continue;

                // Find all pasal headings (by index)
                var matches = pasalRegex.Matches(pageText);
                if (matches.Count == 0)
                {
                    // if no pasal headings, consider whole page as single block with no title
                    result.Add(new ClauseBlockModel
                    {
                        Title = $"Page {pageIndex + 1}",
                        Content = pageText.Trim(),
                        PageNumber = pageIndex + 1
                    });
                    continue;
                }

                var indices = matches.Cast<Match>().Select(m => new { m.Index, Title = m.Value.Trim() }).ToList();
                for (int mi = 0; mi < indices.Count; mi++)
                {
                    int start = indices[mi].Index;
                    int end = (mi + 1 < indices.Count) ? indices[mi + 1].Index : pageText.Length;
                    var block = pageText.Substring(start, end - start).Trim();

                    result.Add(new ClauseBlockModel
                    {
                        Title = indices[mi].Title,
                        Content = block,
                        PageNumber = pageIndex + 1
                    });
                }
            }

            return result;
        }
    }
}
