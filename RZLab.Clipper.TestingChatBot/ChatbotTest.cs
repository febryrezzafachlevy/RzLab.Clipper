using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class ChatbotTest
{
    private readonly string _apiKey;
    private readonly HttpClient _http;

    public ChatbotTest(string apiKey)
    {
        _apiKey = apiKey;
        _http = new HttpClient();
        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<string> AskAsync(string message)
    {
        var body = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "user", content = message }
            },
            temperature = 0.7
        };

        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync("https://api.openai.com/v1/chat/completions", content);

        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            throw new Exception("API Error: " + err);
        }

        var respJson = await response.Content.ReadAsStringAsync();
        dynamic parsed = JsonConvert.DeserializeObject(respJson);

        return parsed.choices[0].message.content.ToString();
    }
}
