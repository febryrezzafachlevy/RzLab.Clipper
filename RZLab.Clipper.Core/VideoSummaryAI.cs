using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class VideoSummaryAI
{
    private readonly string apiKey;
    private readonly string model;
    private readonly HttpClient client;

    public VideoSummaryAI(string apiKey, string model)
    {
        this.apiKey = apiKey;
        this.model = model;

        client = new HttpClient();
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
    }

    public async Task<string> SummarizeWithTimestamps(string transcript, TimeSpan duration)
    {
        var prompt = $$"""
        You are given a video transcription. Summarize the important points.
        Your output MUST follow this exact format:

        [mm:ss] Point summary here

        Example:
        [00:12] Introduction to topic
        [03:55] Explanation of feature A
        [07:40] Important warning

        RULES:
        - Extract only meaningful events.
        - Convert seconds into mm:ss format.
        - Use simple and short Indonesian sentences.
        - DO NOT output anything except the timestamped bullet list.

        Transcription:
        {{transcript}}

        Video duration (seconds): {{duration.TotalSeconds}}
        """;

        var body = new
        {
            model = model,
            messages = new[]
            {
                new { role = "system", content = "Return only timestamped bullet list." },
                new { role = "user", content = prompt }
            },
            temperature = 0.4,
        };

        var json = JsonConvert.SerializeObject(body);
        var response = await client.PostAsync(
            "https://api.openai.com/v1/chat/completions",
            new StringContent(json, Encoding.UTF8, "application/json"));

        var respText = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception(respText);

        dynamic parsed = JsonConvert.DeserializeObject(respText);
        return parsed.choices[0].message.content.ToString();
    }

    public async Task<string> GenerateSummary(
        string transcript,
        TimeSpan duration,
        List<TimeSpan> sceneChanges)
    {
        string sceneJson = JsonConvert.SerializeObject(sceneChanges);

        var prompt = $$"""
    Anda adalah sistem analisa video yang harus menggabungkan hasil transkripsi dan scene change.

    Buat ringkasan video dalam format berikut:

    [mm:ss] Judul poin utama
        Scene: mm:ss atau "Tidak ada"
        Highlight: KATA-KUNCI-PENTING
        Kesimpulan: 1-2 kalimat yang menjelaskan inti poin tersebut

    RULES:
    1. Timestamp poin harus dalam format mm:ss (dibulatkan ke bawah).
    2. Untuk menentukan "Scene:" gunakan logika berikut:
       - Konversi timestamp poin ke detik.
       - Dari daftar SceneChanges, cari scene change dengan nilai DETIK yang 
         PALING DEKAT tetapi TIDAK MELEBIHI timestamp poin tersebut.
       - Jika ada scene change yang sesuai → tampilkan dalam format mm:ss.
       - Jika tidak ada scene sebelum timestamp poin → gunakan scene change PERTAMA.
       - Jika daftar SceneChanges kosong → tulis "Tidak ada".
    3. Highlight harus berupa 3–7 kata penting yang terkait dengan poin.
    4. Semua output harus dalam bahasa Indonesia.
    5. Jangan tambahkan teks lain selain format di atas.
    6. Jangan tambahkan penjelasan atau paragraf tambahan — hanya list hasil.

    Transcription (detik):
    {{transcript}}

    SceneChanges (detik):
    {{sceneJson}}

    Durasi video (detik):
    {{duration.TotalSeconds}}

    Keluarkan hasilnya langsung.
    """;


        var body = new
        {
            model = model,
            messages = new[]
            {
                new { role = "system", content = "Format output wajib mengikuti instruksi." },
                new { role = "user", content = prompt }
            },
            temperature = 0.5
        };

        var json = JsonConvert.SerializeObject(body);
        var response = await client.PostAsync(
            "https://api.openai.com/v1/chat/completions",
            new StringContent(json, Encoding.UTF8, "application/json"));

        var respText = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception(respText);

        dynamic parsed = JsonConvert.DeserializeObject(respText);
        return parsed.choices[0].message.content.ToString();
    }
}
