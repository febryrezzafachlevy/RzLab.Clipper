using RzLab.Clipper.ControlsLib;
using RZLab.Clipper.Core;
using RZLab.Clipper.Core.DocumentLegal;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RZLab.AIAnalyzer.Helpers
{
    public static class DocumentLegalAnalyzerGridHelper
    {
        public static void Setup(RzListView lv)
        {
            ApplyStyle(lv);
            SetupColumns(lv);
            ApplyDarkTheme(lv);
        }

        private static void SetupColumns(RzListView lv)
        {
            //lv.Columns.Clear();

            //lv.Columns.Add("Document", 700);
            //lv.Columns.Add("Type", 150);
            //lv.Columns.Add("Page Count", 100);
            //lv.Columns.Add("File Size", 100);
            //lv.Columns.Add("Upload At", 100);

        }
        private static void ApplyStyle(RzListView lv)
        {
            lv.View = View.Tile;
            lv.TileSize = new Size(700, 120);
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

        public static void LoadData(RzListView lv, List<DocumentDataModel> items)
        {
            lv.BeginUpdate();
            lv.Items.Clear();

            foreach (var p in items)
            {
                var row = new ListViewItem(p.file_name);
                row.SubItems.Add(p.document_type);
                row.SubItems.Add(p.metadata.page_count.ToString());
                row.SubItems.Add(p.metadata.file_size_kb.ToString() + " KB");
                row.SubItems.Add(p.uploaded_at.ToString("yyyy-MM-dd"));

                lv.Items.Add(row);
            }

            lv.EndUpdate();
        }
        public static void LoadData(FlowLayoutPanel flowCards, List<DocumentDataModel> items, AppSettingModel appSetting, EventHandler ViewClicked)
        {
            flowCards.SuspendLayout();
            flowCards.Controls.Clear();

            foreach (var p in items)
            {
                var card = new DocumentCardControl();
                card.SetData(
                    fileName: p.file_name,
                    fileType: p.document_type,
                    uploadedAt: p.uploaded_at,
                    relevantFacts: p.metadata.page_count,
                    Properties.Resources.pdf_96px
                );
                card.Width = flowCards.ClientSize.Width - 6;

                // Full width sesuai FlowLayoutPanel
                //card.Width = flowCards.Width - 20;   // padding kiri-kanan
                //card.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                //card.Margin = new Padding(10);

                //card.ViewClicked += (s, e) => ViewClicked(p, e);

                flowCards.Controls.Add(card);
            }

            flowCards.ResumeLayout();
        }
        public static void LoadData(FlowLayoutPanel flowCards, List<DocumentDataModel> items, AppSettingModel appSetting, Action<DocumentDataModel> OnViewDocument, Action<DocumentDataModel> OnDeleteDocument)
        {
            flowCards.SuspendLayout();
            flowCards.Controls.Clear();

            foreach (var p in items)
            {
                var card = new DocumentCardControl();
                card.SetData(
                    fileName: p.file_name,
                    fileType: p.document_type,
                    uploadedAt: p.uploaded_at,
                    relevantFacts: p.metadata.page_count,
                    Properties.Resources.pdf_96px
                );
                card.Width = flowCards.ClientSize.Width - 6;
                card.MenuClicked += (s, e) =>
                {
                    ShowDocumentMenu(card, p, OnViewDocument, OnDeleteDocument);
                };
                // Full width sesuai FlowLayoutPanel
                //card.Width = flowCards.Width - 20;   // padding kiri-kanan
                //card.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                //card.Margin = new Padding(10);

                //card.ViewClicked += (s, e) => ViewClicked(p, e);

                flowCards.Controls.Add(card);
            }

            flowCards.ResumeLayout();
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
        private static void ShowDocumentMenu(DocumentCardControl card, DocumentDataModel document, Action<DocumentDataModel> OnViewDocument, Action<DocumentDataModel> OnDeleteDocument)
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            menu.BackColor = Color.FromArgb(45, 45, 45);
            menu.ForeColor = Color.White;
            menu.ShowImageMargin = false;

            var viewItem = new ToolStripMenuItem("View");
            var deleteItem = new ToolStripMenuItem("Delete");

            // EVENT: View
            viewItem.Click += (s, e) =>
            {
                OnViewDocument(document);
            };

            // EVENT: Delete
            deleteItem.Click += (s, e) =>
            {
                OnDeleteDocument(document);
            };

            menu.Items.Add(viewItem);
            menu.Items.Add(deleteItem);

            // Tampilkan menu tepat di bawah tombol ⋮
            menu.Show(Cursor.Position);
        }
    }
}
