using RzLab.Clipper.ControlsLib;
using RZLab.Clipper.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace RZLab.AIAnalyzer.Helpers
{
    public static class StockAnalyzerGridHelper
    {

        public static void Setup(RzListView lv)
        {
            ApplyStyle(lv);
            SetupColumns(lv);
            ApplyDarkTheme(lv);
        }

        private static void SetupColumns(RzListView lv)
        {
            lv.Columns.Clear();

            lv.Columns.Add("Emiten", 150);
            //lv.Columns.Add("Entry", 150);
            //lv.Columns.Add("Take Profit 1", 100);
            //lv.Columns.Add("Take Profit 2", 100);
            //lv.Columns.Add("Take Profit 3", 100);
            //lv.Columns.Add("Stop Loss", 150);
            lv.Columns.Add("Confidence", 80);
            lv.Columns.Add("Score", 80);
            lv.Columns.Add("MOAT", 150);
            lv.Columns.Add("Growth", 150);
            lv.Columns.Add("Risk", 136);

        }
        private static void ApplyStyle(RzListView lv)
        {
            lv.View = View.Details;
            //lv.FullRowSelect = true;
            //lv.GridLines = false;
            //lv.HideSelection = false;
            //lv.MultiSelect = false;
            //lv.OwnerDraw = true;

            //lv.Font = new Font("Segoe UI", 9f);
        }
        private static void ApplyDarkTheme(RzListView lv)
        {
            lv.Font = new Font("Segoe UI", 9f);
            //lv.BackColor = Color.FromArgb(32, 32, 32);
            lv.ForeColor = Color.White;
            //lv.BorderStyle = BorderStyle.None;
        }

        public static void LoadData(RzListView lv, List<EmitenScrenerSahamResultModel> items)
        {
            lv.BeginUpdate();
            lv.Items.Clear();

            foreach (var p in items)
            {
                var row = new ListViewItem(p.Emiten);
                row.SubItems.Add(p.Confidence);
                row.SubItems.Add(p.MultibaggerScore.ToString("N2"));
                row.SubItems.Add(p.MoatType);
                row.SubItems.Add(p.GrowthType);
                row.SubItems.Add(p.RiskLevel);

                // Highlight jika SCENE ada
                //if (!p.Scene.Equals("Tidak ada", StringComparison.OrdinalIgnoreCase))
                //{
                //    row.BackColor = Color.FromArgb(50, 90, 50);
                //    row.ForeColor = Color.White;
                //}
                //
                ApplyRowColor(row, p.MultibaggerScore);

                lv.Items.Add(row);
            }

            lv.EndUpdate();
        }

        public static EmitenScrenerSahamResultModel ParseResult(JsonDocument doc)
        {
            string jsonText = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

            string clean = CleanJson(jsonText);
            JsonDocument parsed = JsonDocument.Parse(clean);
            var root = parsed.RootElement;

            var model = new EmitenScrenerSahamResultModel();

            // safe get string (emiten)
            if (root.TryGetProperty("emiten", out JsonElement eEmiten))
                model.Emiten = GetStringSafe(eEmiten);
            else
                model.Emiten = "";

            // safe get ints
            model.Entry = GetIntSafe(root, "entry", defaultValue: 0);
            model.TP1 = GetIntSafe(root, "tp1", defaultValue: model.Entry);
            model.TP2 = GetIntSafe(root, "tp2", defaultValue: model.TP1);
            model.TP3 = GetIntSafe(root, "tp3", defaultValue: model.TP2);
            model.SL = GetIntSafe(root, "sl", defaultValue: 0);

            // confidence: bisa number atau string
            if (root.TryGetProperty("confidence", out JsonElement eConf))
            {
                // normalisasi: jadikan string persentase jika angka besar (mis. 85 -> "85%")
                var confRaw = GetStringSafe(eConf);
                model.Confidence = confRaw;
                // jika Anda ingin memastikan format "85%" jika angka 0..100
                if (double.TryParse(confRaw, out double confNum))
                {
                    // jika 0..1 maka ubah ke persen
                    if (confNum > 0 && confNum <= 1) model.Confidence = (confNum * 100).ToString("0.##") + "%";
                    else model.Confidence = Math.Round(confNum, 2).ToString() + (confNum <= 100 ? "%" : "");
                }
            }
            else model.Confidence = "";

            // reason: bisa array atau string
            if (root.TryGetProperty("reason", out JsonElement eReason))
            {
                model.Reason = GetStringSafe(eReason);
            }
            else
            {
                model.Reason = "";
            }

            model.MultibaggerScore = GetIntSafe(root, "multibagger_score", 0);
            model.MoatType = root.TryGetProperty("moat_type", out JsonElement moatEl)
                ? GetStringSafe(moatEl) : "";

            model.GrowthType = root.TryGetProperty("growth_type", out JsonElement growEl)
                ? GetStringSafe(growEl) : "";

            model.RiskLevel = root.TryGetProperty("risk_level", out JsonElement riskEl)
                ? GetStringSafe(riskEl) : "";

            // Hitung risk reward aman (hindari pembagian nol)
            double risk = (double)(model.Entry - model.SL);
            if (risk == 0) risk = 1; // fallback untuk menghindari div/0

            model.RR1 = Math.Round((model.TP1 - model.Entry) / risk, 2);
            model.RR2 = Math.Round((model.TP2 - model.Entry) / risk, 2);
            model.RR3 = Math.Round((model.TP3 - model.Entry) / risk, 2);

            return model;
        }
        private static string CleanJson(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "{}";

            // Hapus wrapper kode seperti ```json dan ```
            input = input.Replace("```json", "")
                         .Replace("```", "")
                         .Trim();

            // Cari { pertama
            int start = input.IndexOf('{');
            if (start < 0)
                throw new Exception("JSON object tidak ditemukan di response AI");

            // Cari } terakhir
            int end = input.LastIndexOf('}');
            if (end < 0)
                throw new Exception("JSON closing brace tidak ditemukan");

            string json = input.Substring(start, end - start + 1);

            return json.Trim();
        }
        private static string GetStringSafe(JsonElement el)
        {
            switch (el.ValueKind)
            {
                case JsonValueKind.String:
                    return el.GetString();
                case JsonValueKind.Number:
                    // cobalah integer dulu, jika gagal ambil double
                    if (el.TryGetInt64(out long l)) return l.ToString();
                    if (el.TryGetDouble(out double d)) return d.ToString();
                    return el.GetRawText();
                case JsonValueKind.True:
                case JsonValueKind.False:
                    return el.GetBoolean().ToString();
                case JsonValueKind.Null:
                    return string.Empty;
                case JsonValueKind.Array:
                    // gabungkan elemen array menjadi satu string, jika array string/number
                    return string.Join("; ", el.EnumerateArray().Select(x => GetStringSafe(x)));
                case JsonValueKind.Object:
                    return el.GetRawText();
                default:
                    return el.GetRawText();
            }
        }
        private static int GetIntSafe(JsonElement root, string propName, int defaultValue = 0)
        {
            if (!root.TryGetProperty(propName, out JsonElement p)) return defaultValue;
            try
            {
                if (p.ValueKind == JsonValueKind.Number)
                {
                    if (p.TryGetInt32(out int v)) return v;
                    if (p.TryGetInt64(out long lv)) return (int)lv;
                    if (p.TryGetDouble(out double dv)) return (int)Math.Round(dv);
                }
                // jika property bertipe string berisi angka
                if (p.ValueKind == JsonValueKind.String)
                {
                    var s = p.GetString();
                    if (int.TryParse(s, out int v)) return v;
                    if (double.TryParse(s, out double dv)) return (int)Math.Round(dv);
                }
            }
            catch { /* ignore and fallback */ }
            return defaultValue;
        }

        //private static void DoubleClick(ListViewSubItem lstView)
        //{
        //    if (lstView.SelectedItems.Count == 0) return;
        //    var row = lstView.SelectedItems[0];
        //    var emiten = row.SubItems[0].Text;

        //    var emitenParam = _emitenScreenerParamList.FirstOrDefault(x => x.Name == emiten);
        //    var emitenResult = _emitenScreenerResultList.FirstOrDefault(x => x.Emiten == emiten);

        //    var frm = new FrmStockScreenerDetail(emitenParam, emitenResult);
        //    frm.ShowDialog();
        //}

        private static void ApplyRowColor(ListViewItem item, int score)
        {
            if (score < 6)
            {
                item.ForeColor = Color.FromArgb(220, 53, 69);
            }
        }

    }
}
