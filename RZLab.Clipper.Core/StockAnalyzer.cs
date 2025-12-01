using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RZLab.Clipper.Core
{
    public class StockAnalyzer
    {
        private readonly string apiKey;
        private readonly string model;
        private readonly HttpClient client;

        public StockAnalyzer(string apiKey, string model)
        {
            this.apiKey = apiKey;
            this.model = model;

            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
        }
        public async Task<JsonDocument> AskAsync(EmitenScrenerSahamModel e)
        {
            var prompt = BuildPrompt(e);

            var payload = new
            {
                model = this.model, // atau gpt-4o / gpt-4.1 / gpt-3.5-turbo
                messages = new[]
                {
                    new { role = "system", content = "You are a stock analyst. Answer in valid JSON only." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.0,
                max_tokens = 400
            };

            string json = JsonSerializer.Serialize(payload);

            var response = await client.PostAsync(
                "https://api.openai.com/v1/chat/completions",
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            return JsonDocument.Parse(result);
        }

        private string BuildPrompt(EmitenScrenerSahamModel e)
        {
            return $@"
            Analisa saham {e.Name} dengan harga saat ini {e.CurrentValue}.

            Gunakan seluruh kriteria multibagger berikut:

            1. VALUASI & FUNDAMENTAL
            - PE, PBV, PEG ratio
            - EPS growth 5 tahun
            - FCF & OCF positif dan meningkat
            - Debt level rendah (Debt/Equity, Interest Coverage)
            - Margin operasi & margin bersih stabil
            - ROE & ROIC meningkat
            - Asset turnover membaik

            2. GROWTH POTENSIAL
            - Revenue visibility
            - Market share growth
            - Early S-curve atau expansion phase

            3. COMPETITIVE ADVANTAGE (MOAT)
            - Switching cost tinggi
            - Network effect
            - Cost advantage
            - Brand moat
            - Technology moat
            - Licensing/patent moat

            4. MANAGEMENT QUALITY
            - Insider ownership tinggi
            - Insider buying (jika ada)
            - Capital allocation skill bagus
            - Transparansi

            5. INDUSTRY & MEGATREND
            - Industri bertumbuh cepat
            - Relevansi jangka panjang
            - Posisi kompetitif perusahaan dalam industri

            6. RISK ASSESSMENT
            - Leverage
            - Regulatory risk
            - Customer concentration
            - Volatilitas saham

            7. STOCK BEHAVIOR (TECHNICAL CONFIRMATION)
            - Institutional accumulation
            - Volume breakout
            - Base formation
            - All-time-high setup

            TUGAS:
            Buat penilaian lengkap dan beri output JSON valid dengan format berikut:

            {{
              ""emiten"": ""{e.Name}"",
              ""entry"": number,
              ""tp1"": number,
              ""tp2"": number,
              ""tp3"": number,
              ""sl"": number,
              ""confidence"": ""string"",
              ""reason"": ""string"",
              ""multibagger_score"": number,
              ""moat_type"": ""string"",
              ""growth_type"": ""string"",
              ""risk_level"": ""string""
            }}
             ";
        }

    }
}
