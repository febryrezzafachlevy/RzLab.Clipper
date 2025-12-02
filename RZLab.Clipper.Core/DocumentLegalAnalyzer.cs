using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat;
using RZLab.Clipper.Core.DocumentLegal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RZLab.Clipper.Core
{
    public class DocumentLegalAnalyzer
    {
        private readonly string apiKey;
        private readonly string model;
        private readonly HttpClient http;

        public DocumentLegalAnalyzer(string apiKey, string model)
        {
            this.apiKey = apiKey;
            this.model = model;

            http = new HttpClient();
            http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);
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
        public async Task<AnalysisResultModel> AnalyzeAsync(string text)
        {
            var url = "https://api.openai.com/v1/chat/completions";
            var prompt = "Analisa risiko dokumen ini:\n" + text;

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
