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

        public async Task<AnalysisResultModel> Analyze(string text)
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

            return output;  // JSON string di dalam content
        }
    }
}
