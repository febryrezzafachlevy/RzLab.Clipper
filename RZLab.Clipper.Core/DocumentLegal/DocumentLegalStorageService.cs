using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RZLab.Clipper.Core.DocumentLegal;
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

    // load seluruh database
    public List<DocumentDataModel> LoadAll()
    {
        string json = File.ReadAllText(dbPath);

        return JsonSerializer.Deserialize<List<DocumentDataModel>>(json)
               ?? new List<DocumentDataModel>();
    }

    // append 1 document ke database
    public void Append(DocumentDataModel doc)
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
}
