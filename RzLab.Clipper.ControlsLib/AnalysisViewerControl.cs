using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RzLab.Clipper.ControlsLib
{
    // Event args ketika clause dipilih — berikan page dan snippet
    public class ClauseSelectedEventArgs : EventArgs
    {
        public int PageNumber { get; }
        public string Snippet { get; }
        public ClauseSelectedEventArgs(int page, string snippet)
        {
            PageNumber = page;
            Snippet = snippet;
        }
    }

    public partial class AnalysisViewerControl : UserControl
    {
        // Models expected: AnalysisResultModel, ClauseModel (same names as previously)
        private TabControl tab;
        private RichTextBox txtSummary;
        private ListView lvRisks;
        private FlowLayoutPanel flowClauses;
        private ListBox lbRecommendations;

        public event EventHandler<ClauseSelectedEventArgs> ClauseSelected;

        public AnalysisViewerControl()
        {
            InitializeComponent();
            BuildUi();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "AnalysisViewerControl";
            this.Size = new Size(560, 720);
            this.ResumeLayout(false);
        }

        private void BuildUi()
        {
            // Tab control
            tab = new TabControl { Dock = DockStyle.Fill };
            tab.TabPages.Add("Summary");
            tab.TabPages.Add("Risks");
            tab.TabPages.Add("Clauses");
            tab.TabPages.Add("Recommendations");

            // Summary tab
            txtSummary = new RichTextBox
            {
                ReadOnly = true,
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            tab.TabPages[0].Controls.Add(txtSummary);

            // Risks tab
            lvRisks = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                FullRowSelect = true,
                HeaderStyle = ColumnHeaderStyle.Nonclickable
            };
            lvRisks.Columns.Add("Risk", -2);
            lvRisks.Columns.Add("Severity", 80);
            tab.TabPages[1].Controls.Add(lvRisks);

            // Clauses tab (flow panel)
            flowClauses = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(6),
                BackColor = Color.FromArgb(25, 25, 25)
            };
            tab.TabPages[2].Controls.Add(flowClauses);

            // Recommendations
            lbRecommendations = new ListBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White
            };
            tab.TabPages[3].Controls.Add(lbRecommendations);

            this.Controls.Add(tab);
        }

        /// <summary>
        /// Set AnalysisResultModel to viewer. Clears previous.
        /// </summary>
        public void SetAnalysis(AnalysisResultViewerModel analysis)
        {
            txtSummary.Clear();
            lvRisks.Items.Clear();
            flowClauses.Controls.Clear();
            lbRecommendations.Items.Clear();

            if (analysis == null) return;

            // Summary
            txtSummary.Text = analysis.summary ?? "";

            // Risks
            if (analysis.risks != null)
            {
                foreach (var r in analysis.risks)
                {
                    var item = new ListViewItem(new[] { r, "" });
                    // try to detect severity word inside the string like "(High)" or use risk_level
                    lvRisks.Items.Add(item);
                }
                // auto resize columns
                lvRisks.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }

            // Clauses: create small ClauseCard per clause
            if (analysis.clauses != null)
            {
                foreach (var c in analysis.clauses)
                {
                    var card = MakeClauseCard(c);
                    flowClauses.Controls.Add(card);
                }
            }

            // Recommendations
            if (analysis.recommendations != null)
            {
                foreach (var rec in analysis.recommendations)
                    lbRecommendations.Items.Add("• " + rec);
            }
        }

        // small internal clause card
        private Control MakeClauseCard(ClauseViewerModel c)
        {
            var pnl = new Panel
            {
                Width = flowClauses.ClientSize.Width - 30,
                Height = 120,
                BackColor = Color.FromArgb(40, 40, 40),
                Margin = new Padding(6),
                BorderStyle = BorderStyle.None,
            };

            pnl.Resize += (s, e) => { pnl.Width = flowClauses.ClientSize.Width - 30; };

            var title = new Label
            {
                Text = string.IsNullOrWhiteSpace(c.title) ? "Clause" : c.title,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(8, 8),
                AutoSize = true
            };
            pnl.Controls.Add(title);

            var badge = new Label
            {
                Text = (c.risk ?? "").ToUpper(),
                BackColor = GetRiskColor(c.risk),
                ForeColor = Color.White,
                AutoSize = true,
                Padding = new Padding(6, 2, 6, 2),
                Location = new Point(pnl.Width - 80, 8),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            pnl.Controls.Add(badge);
            badge.BringToFront();

            var snippet = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.FromArgb(230, 230, 230),
                Location = new Point(8, 34),
                Size = new Size(pnl.Width - 16, 60),
                Text = c.content ?? ""
            };
            snippet.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnl.Controls.Add(snippet);

            // Actions panel (jump, details)
            var btnJump = new Button
            {
                Text = "Jump to PDF",
                Width = 90,
                Height = 26,
                BackColor = Color.FromArgb(70, 70, 120),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(pnl.Width - 100, pnl.Height - 36),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnJump.FlatAppearance.BorderSize = 0;
            pnl.Controls.Add(btnJump);

            btnJump.Click += (s, e) =>
            {
                // ClauseModel may not contain page reference; if it has, caller should map via AnnotationMapper
                // We raise event with page number if available in title or content as convention.
                int page = 0;
                // try to parse pattern "Page: N" from content (if present)
                // If your analysis_result has pageNumber, modify ClauseModel to include it and use it here.
                ClauseSelected?.Invoke(this, new ClauseSelectedEventArgs(page, c.content ?? ""));
            };

            // whole panel click also raises selected
            pnl.Click += (s, e) => ClauseSelected?.Invoke(this, new ClauseSelectedEventArgs(0, c.content ?? ""));
            snippet.Click += (s, e) => ClauseSelected?.Invoke(this, new ClauseSelectedEventArgs(0, c.content ?? ""));
            title.Click += (s, e) => ClauseSelected?.Invoke(this, new ClauseSelectedEventArgs(0, c.content ?? ""));

            return pnl;
        }

        private Color GetRiskColor(string risk)
        {
            if (string.IsNullOrEmpty(risk)) return Color.Gray;
            var r = risk.ToLower();
            if (r.Contains("high")) return Color.FromArgb(200, 40, 40);
            if (r.Contains("medium")) return Color.FromArgb(230, 130, 20);
            if (r.Contains("low")) return Color.FromArgb(60, 160, 60);
            return Color.FromArgb(120, 120, 120);
        }

        /// <summary>
        /// Optional: clears the viewer
        /// </summary>
        public void Clear()
        {
            txtSummary.Clear();
            lvRisks.Items.Clear();
            flowClauses.Controls.Clear();
            lbRecommendations.Items.Clear();
        }
    }

    // Minimal models (if not in your Models folder, remove this block and reference your models)
    public class AnalysisResultViewerModel
    {
        public string risk_level { get; set; }
        public List<string> risks { get; set; }
        public List<ClauseViewerModel> clauses { get; set; }
        public List<string> recommendations { get; set; }
        public string summary { get; set; }
    }

    public class ClauseViewerModel
    {
        public string title { get; set; }
        public string content { get; set; }
        public string risk { get; set; }
    }
}
