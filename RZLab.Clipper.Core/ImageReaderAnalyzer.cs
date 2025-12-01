using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RZLab.Clipper.Core
{
    public class ImageReaderAnalyzer
    {
        private readonly string apiKey;
        private readonly string model;
        private readonly HttpClient client;

        public ImageReaderAnalyzer(string apiKey, string model)
        {
            this.apiKey = apiKey;
            this.model = model;

            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> ReadImageAsync(string imagePath, int imageType, Action<int> progress)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Image not found", imagePath);

            string mime = GetMimeType(imagePath);
            string base64Image = Convert.ToBase64String(File.ReadAllBytes(imagePath));
            string dataUri = $"data:{mime};base64,{base64Image}";

            var requestJson = new
            {
                model = model,
                messages = new object[]
                {
                    new {
                        role = "user",
                        content = new object[]
                        {
                            new { type = "text", text = GetPromptFromFile(imageType) },
                            new { type = "image_url", image_url = new { url = dataUri } }
                        }
                    }
                },
                max_tokens = 2000
            };

            progress(20);

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", this.apiKey);

            string json = JsonSerializer.Serialize(requestJson);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            string resultString = await response.Content.ReadAsStringAsync();

            JsonDocument doc;
            try
            {
                doc = JsonDocument.Parse(resultString);
                progress(30);
            }
            catch
            {
                throw new Exception("Invalid JSON received from OpenAI:\n" + resultString);
            }

            using (doc)
            {
                var root = doc.RootElement;

                // Handle error from OpenAI API
                if (root.TryGetProperty("error", out var err))
                {
                    string msg = err.TryGetProperty("message", out var msgNode)
                        ? msgNode.GetString()
                        : err.ToString();
                    throw new Exception("OpenAI Error: " + msg);
                }

                if (!root.TryGetProperty("choices", out var choices))
                    throw new Exception("No choices returned: " + resultString);

                var message = choices[0].GetProperty("message");

                if (!message.TryGetProperty("content", out var contentNode))
                    throw new Exception("Message content missing: " + resultString);

                var contentResult = ExtractContent(contentNode).Trim();
                progress(60);

                return contentResult;
            }

        }

        private string ExtractContent(JsonElement contentNode)
        {
            StringBuilder sb = new();

            switch (contentNode.ValueKind)
            {
                case JsonValueKind.Array:
                    foreach (var item in contentNode.EnumerateArray())
                    {
                        if (item.ValueKind == JsonValueKind.Object &&
                            item.TryGetProperty("text", out var textNode))
                        {
                            sb.Append(textNode.GetString());
                        }
                        else if (item.ValueKind == JsonValueKind.String)
                        {
                            sb.Append(item.GetString());
                        }
                    }
                    break;

                case JsonValueKind.String:
                    sb.Append(contentNode.GetString());
                    break;

                case JsonValueKind.Object:
                    if (contentNode.TryGetProperty("text", out var txt))
                        sb.Append(txt.GetString());
                    else
                        sb.Append(contentNode.ToString());
                    break;

                default:
                    sb.Append(contentNode.ToString());
                    break;
            }

            return sb.ToString();
        }


        private static string TryGetString(JsonElement elem, string propName)
        {
            if (elem.ValueKind == JsonValueKind.Object && elem.TryGetProperty(propName, out var p) && p.ValueKind == JsonValueKind.String)
                return p.GetString();
            return null;
        }

        private string GetMimeType(string path)
        {
            string ext = Path.GetExtension(path).ToLower();

            return ext switch
            {
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".bmp" => "image/bmp",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
        }
        private static string GetPromptBasedOnSelection(int imageType)
        {
            switch (imageType)
            {
                case 1: //"KTP":
                    return
                        "Extract text from this Indonesian KTP image and return JSON ONLY with structure: " +
                        "{ \"nik\":\"\", \"nama\":\"\", \"tempat_tanggal_lahir\":\"\", \"jenis_kelamin\":\"\", \"golongan_darah\":\"\", " +
                        "\"alamat\": {\"jalan\":\"\", \"rt_rw\":\"\", \"kelurahan\":\"\", \"kecamatan\":\"\"}, " +
                        "\"agama\":\"\", \"status_perkawinan\":\"\", \"pekerjaan\":\"\", \"kewarganegaraan\":\"\", \"berlaku_hingga\":\"\" }";

                case 2: //"Nota":
                    return
                        "Extract receipt/nota information and return JSON ONLY with structure: " +
                        "{ \"nota no.\":\"\", \"jumlah rp.\":0,\"items\":[{\"banyaknya\":0,\"nama barang\":\"\",\"harga satuan\":0,\"jumlah\":0}] }";

                case 3: //"SIM":
                    return
                        "Extract Indonesian SIM (driver license) information and return JSON ONLY with structure: " +
                        "{ \"nama\":\"\", \"alamat\":\"\", \"tempat &\":\"\", \"tgl. lahir\":\"\", \"tinggi\":\"\", \"pekerjaan\":\"\", \"no. sim\":\"\", \"berlaku s/d\":\"\" }";

                case 4: //"Kuitansi":
                    return
                        "Extract Indonesian payment receipt (kuitansi) information and return JSON ONLY with structure: " +
                        "{ \"penerima\":\"\", \"jumlah\":\"\", \"tanggal\":\"\", \"keperluan\":\"\", \"dibayarkan_oleh\":\"\", \"diterima_oleh\":\"\" }";
                default:
                    return "Extract text and return JSON.";
            }
        }
        private string GetPromptFromFile(int imageType)
        {
            string template = LoadTemplate(imageType);

            return
                "Extract the information from the document image and fill the template JSON below. " +
                "Do not change field names, do not add or remove fields. " +
                "Return JSON ONLY.\n\n" +
                template;
        }

        private static string LoadTemplate(int imageType)
        {
            string docType = "";
            switch (imageType)
            {
                case 1: //"KTP":
                    docType = "ktp";
                    break;
                case 2: //"Nota":
                    docType = "nota";
                    break;
                case 3: //"SIM":
                    docType = "sim";
                    break;
                case 4: //"Kuitansi":
                    docType = "kuitansi";
                    break;
                default:
                    return "Extract text and return JSON.";
            }

            string fileName = docType.ToLower() + ".json";   // ktp.json / sim.json / nota.json / kuitansi.json
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Templates", fileName);

            if (!File.Exists(path))
                throw new Exception("Template file not found: " + path);

            return File.ReadAllText(path);
        }
    }
}
