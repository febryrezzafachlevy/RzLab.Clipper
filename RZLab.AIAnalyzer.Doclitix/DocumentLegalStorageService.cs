using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RZLab.AIAnalyzer.Doclitix;
public class DocumentLegalStorageService
{
    private readonly string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "documnet_legal.db");
    public DocumentLegalStorageService()
    {
        // jika file belum ada → buat file kosong
        if (!File.Exists(dbPath))
        {
            File.WriteAllText(dbPath, "[]");
        }
    }

    public List<DocumentDataModel> LoadAll()
    {
        if (!File.Exists(dbPath))
            return new List<DocumentDataModel>();

         string json = File.ReadAllText(dbPath);

        return JsonSerializer.Deserialize<List<DocumentDataModel>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? new List<DocumentDataModel>();
    }

    public DocumentDataModel Get(string documentId)
    {
        return LoadAll().FirstOrDefault(x => x.document_id == documentId);
    }

    public void Save(DocumentDataModel doc)
    {
        var list = LoadAll();

        list.Add(doc);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string output = JsonSerializer.Serialize(list, options);
        File.WriteAllText(dbPath, output);
    }

    public void Save(List<DocumentDataModel> list)
    {
        var opt = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(dbPath, JsonSerializer.Serialize(list, opt));
    }

    public void SaveAnalysis(string documentId, AnalysisResultModel analysis)
    {
        var list = LoadAll();
        var doc = list.FirstOrDefault(x => x.document_id == documentId);

        if (doc == null)
            throw new Exception($"Document '{documentId}' not found.");

        doc.analysis_result = analysis;

        var opt = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(dbPath, JsonSerializer.Serialize(list, opt));
    }

    /// <summary>
    /// ExtractCandidates: given pages (list of page text), return candidate clauses containing keywords.
    /// - pages: list where index 0 => page 1
    /// - keywords: list of keywords (case-insensitive)
    /// - contextLines: number of surrounding lines to include as snippet
    /// - minMatchKeywords: minimum distinct keywords to accept (default 1)
    /// - maxCandidatesPerDoc: limit overall candidates
    /// </summary>
    public static List<CandidateClause> ExtractCandidates(
        List<string> pages,
        IEnumerable<string> keywords = null,
        int contextLines = 2,
        int minMatchKeywords = 1,
        int maxCandidatesPerDoc = 200)
    {
        if (pages == null) return new List<CandidateClause>();
        var kw = (keywords ?? DefaultKeywords()).Where(x => !string.IsNullOrWhiteSpace(x)).Select(k => k.ToLowerInvariant()).Distinct().ToArray();

        var results = new List<CandidateClause>();

        for (int i = 0; i < pages.Count; i++)
        {
            string pageText = pages[i] ?? "";
            // normalize line endings
            var lines = Regex.Split(pageText, @"\r\n|\r|\n").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            for (int li = 0; li < lines.Length; li++)
            {
                var line = lines[li];
                var lineLower = line.ToLowerInvariant();

                var matched = new List<string>();
                foreach (var k in kw)
                {
                    if (lineLower.Contains(k))
                        matched.Add(k);
                }

                if (matched.Count >= minMatchKeywords)
                {
                    // gather context
                    int start = Math.Max(0, li - contextLines);
                    int end = Math.Min(lines.Length - 1, li + contextLines);
                    var snippetLines = new List<string>();
                    for (int t = start; t <= end; t++) snippetLines.Add(lines[t].Trim());
                    var snippet = string.Join(" ", snippetLines);

                    // dedupe: if near existing result, merge keywords & skip adding
                    var nearby = results.FirstOrDefault(r =>
                        r.PageNumber == i + 1 &&
                        LevenshteinDistanceSimplified(r.Snippet, snippet) < 40); // heuristic

                    if (nearby != null)
                    {
                        foreach (var m in matched)
                            if (!nearby.MatchedKeywords.Contains(m)) nearby.MatchedKeywords.Add(m);
                    }
                    else
                    {
                        results.Add(new CandidateClause
                        {
                            PageNumber = i + 1,
                            Snippet = snippet,
                            MatchedKeywords = matched.Distinct().ToList()
                        });
                    }
                }
            } // lines
        } // pages

        // rank by score (descending) and limit
        var ordered = results.OrderByDescending(r => r.Score).ThenBy(r => r.PageNumber).Take(maxCandidatesPerDoc).ToList();
        return ordered;
    }

    // Default keyword list for Indonesian/English mixed legal doc
    public static IEnumerable<string> DefaultKeywords()
    {
        return new[]
        {
                "pembayaran","payment","jatuh tempo","penalti","denda","denda keterlambatan",
                "jangka waktu","masa berlaku","durasi","periode",
                "pengakhiran","termination","berakhir","break clause",
                "kewajiban","kewajiban pihak","hak","liability","tanggung jawab",
                "kerahasiaan","confidentiality","nda",
                "force majeure","keadaan kahar","ganti rugi","indemnity",
                "sengketa","dispute","penyelesaian sengketa","arbitrase","arbitration",
                "jaminan","warranty","garansi","penalty","sanksi"
            };
    }

    // Simple heuristic for near-duplicate detection (not full Levenshtein)
    // returns small integer estimate of difference
    private static int LevenshteinDistanceSimplified(string a, string b)
    {
        if (string.IsNullOrEmpty(a)) return (b ?? "").Length;
        if (string.IsNullOrEmpty(b)) return a.Length;
        // use ratio of common tokens
        var ta = new HashSet<string>(Regex.Split(a.ToLowerInvariant(), @"\W+").Where(t => t.Length > 2));
        var tb = new HashSet<string>(Regex.Split(b.ToLowerInvariant(), @"\W+").Where(t => t.Length > 2));
        if (ta.Count == 0 || tb.Count == 0) return 100;
        var inter = ta.Intersect(tb).Count();
        var total = Math.Max(ta.Count, tb.Count);
        // return inverse similarity
        return (int)((1.0 - (double)inter / total) * 100);
    }
}
