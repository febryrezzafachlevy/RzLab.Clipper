using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class AiSummarizer
{
    private readonly string apiKey;
    private readonly string model;
    private readonly HttpClient client;

    public AiSummarizer(string apiKey, string model)
    {
        this.apiKey = apiKey;
        this.model = model;

        client = new HttpClient();
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
    }

    public async Task<List<Segment>> ExtractAsync(string transcript, TimeSpan duration)
    {
        var prompt = $$"""
        You are given a transcription. Return important segments as a JSON array:

        [
          { "start": 3.5, "end": 10.0, "reason": "key point" }
        ]

        - Start/end in seconds
        - Only JSON output
        - Keep durations 2–60 seconds

        Transcription:
        {{transcript}}

        DurationSeconds: {{duration.TotalSeconds}}
        """;

        var body = new
        {
            model = model,
            messages = new[]
            {
                new { role = "system", content = "Return only JSON array." },
                new { role = "user", content = prompt }
            },
            temperature = 0
        };

        var json = JsonConvert.SerializeObject(body);
        var resp = await client.PostAsync(
            "https://api.openai.com/v1/chat/completions",
            new StringContent(json, Encoding.UTF8, "application/json"));

        var txt = await resp.Content.ReadAsStringAsync();
        if (!resp.IsSuccessStatusCode)
            throw new Exception("AI Error: " + txt);

        dynamic parsed = JsonConvert.DeserializeObject(txt);
        string content = parsed.choices[0].message.content.ToString();

        var start = content.IndexOf("[");
        var end = content.LastIndexOf("]");
        var arr = content.Substring(start, end - start + 1);

        var list = JsonConvert.DeserializeObject<List<AiSeg>>(arr);

        var segments = new List<Segment>();
        foreach (var x in list)
        {
            var s = TimeSpan.FromSeconds(x.start);
            var e = TimeSpan.FromSeconds(x.end);
            segments.Add(new Segment(s, e, x.reason));
        }

        return segments;
    }

    private class AiSeg
    {
        public double start { get; set; }
        public double end { get; set; }
        public string reason { get; set; }
    }
}
