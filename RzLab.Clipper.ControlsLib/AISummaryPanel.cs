using RZLab.Clipper.Core.DocumentLegal;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace RzLab.Clipper.ControlsLib
{
    public class AISummaryPanel : UserControl
    {
        private Label lblTitle;
        private Panel accentLine;
        private Label lblRisk;
        private RichTextBox txtSummary;
        private TableLayoutPanel layout;

        public AISummaryPanel()
        {
            this.BackColor = Color.FromArgb(28, 28, 28);
            this.Padding = new Padding(15, 15, 15, 15);

            // MAIN LAYOUT
            layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = Color.FromArgb(28, 28, 28),
            };

            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));  // Title
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 3)); // Accent
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));  // Risk Badge
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Summary

            this.Controls.Add(layout);

            // TITLE
            lblTitle = new Label()
            {
                Text = "AI Summary",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Padding = new Padding(0, 0, 0, 10),
                AutoSize = true,
            };
            layout.Controls.Add(lblTitle, 0, 0);

            // ACCENT LINE
            accentLine = new Panel()
            {
                BackColor = Color.FromArgb(0, 120, 215),
                Height = 3,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 0, 0, 10),
            };
            layout.Controls.Add(accentLine, 0, 1);

            // RISK BADGE
            lblRisk = new Label()
            {
                AutoSize = true,
                Padding = new Padding(10, 4, 10, 4),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Gray,
                Margin = new Padding(0, 5, 0, 10),
            };
            layout.Controls.Add(lblRisk, 0, 2);

            // SUMMARY
            txtSummary = new RichTextBox()
            {
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(28, 28, 28),
                ForeColor = Color.WhiteSmoke,
                Font = new Font("Segoe UI", 11),
                Dock = DockStyle.Fill,
            };
            layout.Controls.Add(txtSummary, 0, 3);
        }

        public void SetSummary(AnalysisResultModel analysis)
        {
            if (analysis == null)
            {
                lblRisk.Visible = false;
                txtSummary.Text = "No summary available.";
                return;
            }

            lblRisk.Visible = true;

            // Set badge color
            switch ((analysis.risk_level ?? "").ToLower())
            {
                case "high":
                    lblRisk.BackColor = Color.FromArgb(180, 40, 40);
                    lblRisk.Text = "⚠ HIGH RISK";
                    break;

                case "medium":
                    lblRisk.BackColor = Color.FromArgb(230, 140, 10);
                    lblRisk.Text = "🟧 MEDIUM RISK";
                    break;

                case "low":
                    lblRisk.BackColor = Color.FromArgb(40, 150, 60);
                    lblRisk.Text = "🟩 LOW RISK";
                    break;

                default:
                    lblRisk.BackColor = Color.FromArgb(80, 80, 80);
                    lblRisk.Text = "UNKNOWN RISK";
                    break;
            }

            txtSummary.Text = FormatBulletSummary(analysis.summary);
        }

        private string FormatBulletSummary(string summary)
        {
            if (string.IsNullOrWhiteSpace(summary))
                return "• No AI summary provided.";

            var lines = summary.Split(new[] { '.', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string result = "";
            foreach (var l in lines)
            {
                var clean = l.Trim();
                if (clean.Length > 0)
                    result += "• " + clean + ".\n";
            }
            return result.Trim();
        }
    }
}
