using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RZLab.AIAnalyzer.Doclitix
{
    public static class DocumentLegalImportantClauseExtractor
    {
        // extract candidates from clause blocks (preferred) - returns those that match keywords
        public static List<CandidateClause> ExtractCandidatesFromPasal(
            List<ClauseBlockModel> clauses,
            IEnumerable<string> keywords,
            int maxCandidates = 200)
        {
            if (clauses == null) return new List<CandidateClause>();
            var kw = (keywords ?? Enumerable.Empty<string>()).Where(x => !string.IsNullOrWhiteSpace(x)).Select(k => k.ToLowerInvariant()).Distinct().ToArray();
            var results = new List<CandidateClause>();

            foreach (var clause in clauses)
            {
                var txt = (clause.Content ?? "").ToLowerInvariant();
                var matched = new List<string>();
                foreach (var k in kw)
                {
                    if (txt.Contains(k)) matched.Add(k);
                }

                if (matched.Count > 0)
                {
                    results.Add(new CandidateClause
                    {
                        PageNumber = clause.PageNumber,
                        Snippet = clause.Content.Trim(),
                        MatchedKeywords = matched.Distinct().ToList()
                    });
                }
            }

            return results.OrderByDescending(r => r.Score).ThenBy(r => r.PageNumber).Take(maxCandidates).ToList();
        }

        // fallback: extract by paragraph lines if pasal detection yields nothing useful
        public static List<CandidateClause> ExtractCandidatesFromPages(
            List<PDFContent> pages,
            IEnumerable<string> keywords,
            int contextLines = 2,
            int minMatchKeywords = 1,
            int maxCandidatesPerDoc = 200)
        {
            var kw = (keywords ?? Enumerable.Empty<string>()).Where(x => !string.IsNullOrWhiteSpace(x)).Select(k => k.ToLowerInvariant()).Distinct().ToArray();
            var results = new List<CandidateClause>();

            for (int i = 0; i < pages.Count; i++)
            {
                string pageText = pages[i].Content;
                var lines = Regex.Split(pageText, @"\r\n|\r|\n").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
                for (int li = 0; li < lines.Length; li++)
                {
                    var line = lines[li];
                    var lower = line.ToLowerInvariant();
                    var matched = new List<string>();
                    foreach (var k in kw)
                        if (lower.Contains(k)) matched.Add(k);

                    if (matched.Count >= minMatchKeywords)
                    {
                        int start = Math.Max(0, li - contextLines);
                        int end = Math.Min(lines.Length - 1, li + contextLines);
                        var snippet = string.Join(" ", lines.Skip(start).Take(end - start + 1));
                        results.Add(new CandidateClause
                        {
                            PageNumber = i + 1,
                            Snippet = snippet.Trim(),
                            MatchedKeywords = matched.Distinct().ToList()
                        });
                    }
                }
            }

            return results.OrderByDescending(r => r.Score).ThenBy(r => r.PageNumber).Take(maxCandidatesPerDoc).ToList();
        }
    }
}
