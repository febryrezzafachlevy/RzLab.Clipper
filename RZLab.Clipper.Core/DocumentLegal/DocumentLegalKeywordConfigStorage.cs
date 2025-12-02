using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RZLab.Clipper.Core.DocumentLegal
{
    public static class DocumentLegalKeywordConfigStorage
    {
        private const string PATH = "keywords.json";

        public static Dictionary<string, List<string>> Load()
        {
            if (!File.Exists(PATH))
            {
                // fallback default minimal
                return new Dictionary<string, List<string>>();
            }

            var json = File.ReadAllText(PATH);
            return JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new Dictionary<string, List<string>>();
        }

        public static void Save(Dictionary<string, List<string>> map)
        {
            var opt = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(PATH, JsonSerializer.Serialize(map, opt));
        }
    }

    public static class DocumentLegalKeywordProvider
    {
        public static List<string> GetKeywords(string documentType)
        {
            var all = DocumentLegalKeywordConfigStorage.Load();
            var list = new List<string>();

            if (all.TryGetValue("global", out var global))
                list.AddRange(global);

            if (!string.IsNullOrWhiteSpace(documentType))
            {
                var key = documentType.ToLower().Replace(" ", "_");
                if (all.TryGetValue(key, out var typed))
                    list.AddRange(typed);
            }

            return list.Distinct().ToList();
        }
    }
}
