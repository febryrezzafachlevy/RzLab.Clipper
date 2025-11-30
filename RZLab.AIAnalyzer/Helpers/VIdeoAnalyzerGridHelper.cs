using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RZLab.AIAnalyzer.Helpers
{
    public static class VIdeoAnalyzerGridHelper
    {
        // ============================
        //  PUBLIC ENTRY POINT
        // ============================
        public static void Setup(ListView lv)
        {
            SetupColumns(lv);
            ApplyDarkTheme(lv);
        }

        public static void AutoResize(ListView lv)
        {
            if (lv.Columns.Count < 5) return;

            int w = lv.ClientSize.Width;

            lv.Columns[0].Width = (int)(w * 0.10); // Timestamp
            lv.Columns[1].Width = (int)(w * 0.20); // Title
            lv.Columns[2].Width = (int)(w * 0.10); // Scene
            lv.Columns[3].Width = (int)(w * 0.25); // Highlight
            lv.Columns[4].Width = (int)(w * 0.35); // Summary
        }

        // ============================
        //  SETUP LISTVIEW
        // ============================
        private static void SetupColumns(ListView lv)
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.GridLines = true;
            lv.HideSelection = false;
            lv.MultiSelect = false;

            lv.Columns.Clear();

            lv.Columns.Add("Timestamp", 100);
            lv.Columns.Add("Title", 200);
            lv.Columns.Add("Scene", 100);
            lv.Columns.Add("Highlight", 250);
            lv.Columns.Add("Summary", 500);

            lv.Font = new Font("Segoe UI", 9f);
        }

        private static void ApplyDarkTheme(ListView lv)
        {
            lv.BackColor = Color.FromArgb(30, 30, 30);
            lv.ForeColor = Color.White;
        }

        // ============================
        //  LOAD SUMMARY INTO LISTVIEW
        // ============================
        public static void LoadSummary(ListView lv, List<SummaryPoint> points)
        {
            lv.BeginUpdate();
            lv.Items.Clear();

            foreach (var p in points)
            {
                var row = new ListViewItem(p.Timestamp);
                row.SubItems.Add(p.Title);
                row.SubItems.Add(p.Scene);
                row.SubItems.Add(p.Highlight);
                row.SubItems.Add(p.Summary);

                // Highlight jika SCENE ada
                if (!p.Scene.Equals("Tidak ada", StringComparison.OrdinalIgnoreCase))
                {
                    row.BackColor = Color.FromArgb(50, 90, 50);
                    row.ForeColor = Color.White;
                }

                lv.Items.Add(row);
            }

            lv.EndUpdate();
        }

        // ============================
        //  PARSER SUMMARY AI
        // ============================
        public static List<SummaryPoint> ParseSummary(string text)
        {
            var points = new List<SummaryPoint>();

            var blocks = text.Split(
                new[] { "\n\n", "\r\n\r\n" },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var block in blocks)
            {
                var lines = block.Split('\n')
                                 .Select(l => l.Trim())
                                 .Where(l => l.Length > 0)
                                 .ToList();

                if (lines.Count < 4)
                    continue;

                // Line 1 → [mm:ss] Title
                var m = Regex.Match(lines[0], @"\[(\d{2}:\d{2})\]\s*(.+)");
                if (!m.Success)
                    continue;

                var point = new SummaryPoint
                {
                    Timestamp = m.Groups[1].Value,
                    Title = m.Groups[2].Value,
                    Scene = lines[1].Replace("Scene:", "").Trim(),
                    Highlight = lines[2].Replace("Highlight:", "").Trim(),
                    Summary = lines[3].Replace("Kesimpulan:", "").Trim()
                };

                points.Add(point);
            }

            return points;
        }


        public static SummaryPoint? HandleDoubleClick(ListView lv)
        {
            if (lv.SelectedItems.Count == 0) return null;
            var row = lv.SelectedItems[0];

            var point = new SummaryPoint
            {
                Timestamp = row.SubItems[0].Text,
                Title = row.SubItems[1].Text,
                Scene = row.SubItems[2].Text,
                Highlight = row.SubItems[3].Text,
                Summary = row.SubItems[4].Text
            };

            return point;
        }
    }
}
