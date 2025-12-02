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
    public static class ImageReaderAnalyzerGridHelper
    {
        private static List<ImageReaderModel> imageReaders = new List<ImageReaderModel>();
        public static void Setup(RzListView lv)
        {
            imageReaders = new List<ImageReaderModel>();

            ApplyStyle(lv);
            SetupColumns(lv);
            ApplyDarkTheme(lv);
        }

        private static void SetupColumns(RzListView lv)
        {
            lv.Columns.Clear();

            lv.Columns.Add("KEY", 300);
            lv.Columns.Add("VALUE", 445);

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

        public static void LoadData(RzListView lv, string json)
        {
            imageReaders = new List<ImageReaderModel>();

            string pureJson = GetJsonSafe(json);
            using JsonDocument doc = JsonDocument.Parse(pureJson);
            var root = doc.RootElement;

            ParseJsonRecursive(root, "");

            lv.BeginUpdate();
            lv.Items.Clear();

            foreach (var p in imageReaders)
            {
                var row = new ListViewItem(p.Key);
                row.SubItems.Add(p.Value);

                lv.Items.Add(row);
            }

            lv.EndUpdate();
        }

        private static void ParseJsonRecursive(JsonElement element, string parentKey)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    foreach (var prop in element.EnumerateObject())
                    {
                        string newKey = string.IsNullOrEmpty(parentKey)
                            ? prop.Name
                            : parentKey + "." + prop.Name;

                        ParseJsonRecursive(prop.Value, newKey);
                    }
                    break;

                case JsonValueKind.Array:
                    int index = 0;
                    foreach (var item in element.EnumerateArray())
                    {
                        string newKey = $"{parentKey}[{index}]";
                        ParseJsonRecursive(item, newKey);
                        index++;
                    }
                    break;

                case JsonValueKind.String:
                    AddToListView(parentKey, element.GetString());
                    break;

                case JsonValueKind.Number:
                    AddToListView(parentKey, element.GetRawText());
                    break;

                case JsonValueKind.True:
                case JsonValueKind.False:
                    AddToListView(parentKey, element.GetBoolean().ToString());
                    break;

                case JsonValueKind.Null:
                    AddToListView(parentKey, "null");
                    break;

                default:
                    AddToListView(parentKey, element.GetRawText());
                    break;
            }
        }
        static void AddToListView(string key, string value)
        {
            imageReaders.Add(new ImageReaderModel
            {
                Key = key,
                Value = value
            });
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
        private static string GetJsonSafe(string text)
        {
            int startIndex = text.IndexOf('{');
            int endIndex = text.LastIndexOf('}');

            if (startIndex == -1 || endIndex == -1 || endIndex <= startIndex)
                throw new Exception("No valid JSON object found.");

            var pureJson = text.Substring(startIndex, endIndex - startIndex + 1).Trim();
            pureJson = System.Text.RegularExpressions.Regex
                        .Replace(pureJson, @"[^\u0000-\u007F]+", "");
            return pureJson;
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
