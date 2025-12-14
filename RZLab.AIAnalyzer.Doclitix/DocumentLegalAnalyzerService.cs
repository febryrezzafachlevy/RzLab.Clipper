using RZLab.AIAnalyzer.Doclitix;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RZLab.Clipper.Core
{
    public class DocumentLegalAnalyzerService : IDocumentLegalAnalyzerService
    {
        private readonly DoclitixSetting _doclitixSetting;
        private readonly HttpClient http;

        public DocumentLegalAnalyzerService(DoclitixSetting doclitixSetting)
        {
            _doclitixSetting = doclitixSetting;

            http = new HttpClient();
            http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _doclitixSetting.ApiKey);
        }

        /// <summary>
        /// Sends a prompt to OpenAI Chat Completions endpoint using response_format json_object
        /// Returns the JSON string in the "content" field (assumes model returns JSON).
        /// </summary>
        public async Task<string> AnalyzeAsync(string model, string prompt)
        {
            var url = "https://api.openai.com/v1/chat/completions";

            var body = new
            {
                model = model,
                response_format = new { type = "json_object" },
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await http.PostAsync(url, content);
            var text = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
            {
                throw new Exception($"OpenAI error {res.StatusCode}: {text}");
            }

            using var doc = JsonDocument.Parse(text);
            // navigate: choices[0].message.content (string)
            var contentStr = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return contentStr;
        }

        /// <summary>
        /// Sends a prompt to OpenAI Chat Completions endpoint using response_format json_object
        /// Returns the JSON string in the "content" field (assumes model returns JSON).
        /// </summary>
        public async Task<AnalysisResultModel> AnalyzeAsync(string prompt)
        {
            var url = "https://api.openai.com/v1/chat/completions";
            prompt = "Analisa risiko dokumen ini:\n" + prompt;

            var body = new
            {
                model = _doclitixSetting.Model,
                response_format = new { type = "json_object" },
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await http.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"OpenAI Error: {response.StatusCode}\n{error}");
            }

            var resultJson = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(resultJson);

            // ---- ini bagian yang penting ----
            string output =
                doc.RootElement
                   .GetProperty("choices")[0]
                   .GetProperty("message")
                   .GetProperty("content")
                   .GetString();

            var result = JsonSerializer.Deserialize<AnalysisResultModel>(output,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
            return result;  // JSON string di dalam content
        }
    }
}
