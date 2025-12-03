using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RZLab.Clipper.Core.DocumentLegal;

namespace RzLab.Clipper.ControlsLib;

public class AnalysisViewerNoTabsControl : FlowLayoutPanel
{
    private SectionPanel summarySection;
    private SectionPanel risksSection;
    private SectionPanel clausesSection;
    private SectionPanel recomSection;

    public event EventHandler<int> PdfJumpRequested;

    public AnalysisViewerNoTabsControl()
    {
        DoubleBuffered = true;
        FlowDirection = FlowDirection.TopDown;
        WrapContents = false;
        AutoScroll = true;
        Padding = new Padding(6);
        BackColor = Color.FromArgb(30, 30, 30);

        // create sections
        summarySection = new SectionPanel("Summary");
        risksSection = new SectionPanel("Risks");
        clausesSection = new SectionPanel("Clauses");
        recomSection = new SectionPanel("Recommendations");

        // by default expand summary & clauses
        summarySection.SetExpanded(true);
        risksSection.SetExpanded(true);
        clausesSection.SetExpanded(true);
        recomSection.SetExpanded(true);

        // add to root
        Controls.Add(summarySection);
        Controls.Add(risksSection);
        Controls.Add(clausesSection);
        Controls.Add(recomSection);

        // When a clause jump button clicked, bubble event
        clausesSection.ChildButtonClicked += (s, a) =>
        {
            // a is object - we pass int page if supplied
            if (a is int pg) PdfJumpRequested?.Invoke(this, pg);
            else if (a is string str && int.TryParse(str, out int p2)) PdfJumpRequested?.Invoke(this, p2);
        };

        // resize hooking to set child width
        SizeChanged += (s, e) => ResizeChildren();
    }

    private void ResizeChildren()
    {
        int w = ClientSize.Width - Padding.Horizontal - 10;
        foreach (Control c in Controls)
        {
            c.Width = Math.Max(300, w);
        }
    }

    // ----------------------------
    // Public API
    // ----------------------------
    public void SetAnalysis(AnalysisResultModel model)
    {
        SuspendLayout();
        try
        {
            if (model == null)
            {
                summarySection.SetBody(ControlForText("No analysis available."));
                risksSection.SetBody(ControlForText("No risks."));
                clausesSection.SetBody(ControlForText("No clauses."));
                recomSection.SetBody(ControlForText("No recommendations."));
                return;
            }

            // SUMMARY
            var txt = $"Overall risk: {model.risk_level ?? "unknown"}\n\n{(model.summary ?? "")}";
            var summaryCtrl = new Label()
            {
                Text = txt,
                AutoSize = false,
                Dock = DockStyle.Fill,
                ForeColor = Color.WhiteSmoke,
                Font = new Font("Segoe UI", 10),
                Padding = new Padding(6)
            };
            summaryCtrl.MaximumSize = new Size(1000, 0);
            summarySection.SetBody(summaryCtrl);
            summarySection.SetBadge((model.risk_level ?? "").ToUpper());

            // RISKS
            if (model.risks == null || model.risks.Count == 0)
            {
                risksSection.SetBody(ControlForText("No risks detected."));
                risksSection.SetBadge("0");
            }
            else
            {
                var container = new FlowLayoutPanel()
                {
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    Dock = DockStyle.Top
                };

                foreach (var r in model.risks)
                {
                    var p = BuildRiskCard(r);
                    container.Controls.Add(p);
                }
                risksSection.SetBody(container);
                risksSection.SetBadge(model.risks.Count.ToString());
            }

            // CLAUSES
            if (model.clauses == null || model.clauses.Count == 0)
            {
                clausesSection.SetBody(ControlForText("No clauses found."));
                clausesSection.SetBadge("0");
            }
            else
            {
                var container = new FlowLayoutPanel()
                {
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    AutoSize = true,
                    Dock = DockStyle.Top,
                    BackColor = Color.Transparent
                };

                foreach (var c in model.clauses)
                {
                    var card = BuildClauseCard(c);
                    // when jump clicked inside clause card, clause card will call ChildButtonClicked with page
                    container.Controls.Add(card);
                }

                clausesSection.SetBody(container);
                clausesSection.SetBadge(model.clauses.Count.ToString());
            }

            // RECOMMENDATIONS
            if (model.recommendations == null || model.recommendations.Count == 0)
            {
                recomSection.SetBody(ControlForText("No recommendations."));
                recomSection.SetBadge("0");
            }
            else
            {
                var flow = new FlowLayoutPanel()
                {
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    Dock = DockStyle.Top
                };
                foreach (var r in model.recommendations)
                {
                    var lbl = new Label()
                    {
                        AutoSize = false,
                        Text = "• " + r,
                        ForeColor = Color.Gainsboro,
                        Font = new Font("Segoe UI", 10),
                        Padding = new Padding(6),
                        Dock = DockStyle.Top,
                        MaximumSize = new Size(1000, 0)
                    };
                    flow.Controls.Add(lbl);
                }
                recomSection.SetBody(flow);
                recomSection.SetBadge(model.recommendations.Count.ToString());
            }
        }
        finally
        {
            ResumeLayout();
            ResizeChildren();
        }
    }

    // ----------------------------
    // Helpers: UI pieces
    // ----------------------------
    private Control ControlForText(string t)
    {
        var l = new Label()
        {
            Text = t,
            AutoSize = false,
            Dock = DockStyle.Fill,
            ForeColor = Color.Gainsboro,
            Font = new Font("Segoe UI", 10),
            Padding = new Padding(6)
        };
        l.MaximumSize = new Size(1200, 0);
        return l;
    }

    private Control BuildRiskCard(RiskModel r)
    {
        var panel = new Panel()
        {
            BackColor = Color.FromArgb(45, 45, 45),
            Padding = new Padding(10),
            Margin = new Padding(6),
            AutoSize = true,
            Width = Math.Max(300, ClientSize.Width - 40)
        };

        var lblTitle = new Label()
        {
            Text = $"[{(r.level ?? "MEDIUM").ToUpper()}]",
            ForeColor = Color.White,
            Font = new Font("Segoe UI Semibold", 9),
            AutoSize = true,
            Dock = DockStyle.Top
        };

        var lblDesc = new Label()
        {
            Text = r.description ?? "",
            ForeColor = Color.Gainsboro,
            Font = new Font("Segoe UI", 9),
            AutoSize = false,
            Dock = DockStyle.Top,
            MaximumSize = new Size(panel.Width - 20, 0),
            Padding = new Padding(0, 6, 0, 0)
        };

        panel.Controls.Add(lblDesc);
        panel.Controls.Add(lblTitle);
        return panel;
    }

    private Control BuildClauseCard(ClauseModel c)
    {
        // simple card with title, content and Jump button aligned right
        var panel = new Panel()
        {
            BackColor = Color.FromArgb(45, 45, 45),
            Padding = new Padding(12),
            Margin = new Padding(6),
            AutoSize = true,
            Width = Math.Max(300, ClientSize.Width - 40)
        };

        var title = new Label()
        {
            Text = c.title ?? "Clause",
            Font = new Font("Segoe UI Semibold", 10),
            ForeColor = Color.White,
            AutoSize = true
        };

        var badge = new Label()
        {
            Text = (c.risk ?? "MEDIUM").ToUpper(),
            BackColor = c.risk?.ToLower() == "high" ? Color.FromArgb(185, 40, 40)
                        : c.risk?.ToLower() == "low" ? Color.FromArgb(40, 150, 70)
                        : Color.FromArgb(230, 150, 20),
            ForeColor = Color.White,
            Padding = new Padding(8, 3, 8, 3),
            AutoSize = true
        };

        var headerPanel = new Panel() { Dock = DockStyle.Top, Height = 26, BackColor = Color.Transparent };
        headerPanel.Controls.Add(title);
        headerPanel.Controls.Add(badge);
        title.Location = new Point(0, 0);
        badge.Location = new Point(panel.Width - badge.Width - 8, 0);
        badge.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        var content = new Label()
        {
            Text = c.content ?? "",
            ForeColor = Color.Gainsboro,
            Font = new Font("Segoe UI", 9),
            AutoSize = false,
            MaximumSize = new Size(panel.Width - 20, 0),
            Dock = DockStyle.Top,
            Padding = new Padding(0, 8, 0, 0)
        };

        var btnJump = new Button()
        {
            Text = "Jump to",
            AutoSize = true,
            BackColor = Color.FromArgb(70, 70, 120),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnJump.FlatAppearance.BorderSize = 0;
        btnJump.Click += (s, e) =>
        {
            // bubble via section event
            clausesSection.RaiseChildButtonClicked(c.page);
        };

        // footer with right-aligned button
        var footer = new Panel() { Dock = DockStyle.Top, Height = 36, BackColor = Color.Transparent };
        footer.Controls.Add(btnJump);
        btnJump.Location = new Point(panel.Width - btnJump.Width - 8, 4);
        btnJump.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        // add controls in order
        panel.Controls.Add(footer);
        panel.Controls.Add(content);
        panel.Controls.Add(headerPanel);

        return panel;
    }

    // ----------------------------
    // SectionPanel (inner helper)
    // ----------------------------
    private class SectionPanel : Panel
    {
        private Label lblHeader;
        private Label lblBadge;
        private Panel bodyContainer;
        private PictureBox chevron;
        public event EventHandler<object> ChildButtonClicked; // bubble

        public SectionPanel(string title)
        {
            BackColor = Color.Transparent;
            AutoSize = false;
            Margin = new Padding(4);
            Padding = new Padding(0);
            Width = 300;

            lblHeader = new Label()
            {
                Text = title,
                Font = new Font("Segoe UI Semibold", 11),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(8, 8)
            };

            lblBadge = new Label()
            {
                Text = "",
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Padding = new Padding(8, 3, 8, 3),
                AutoSize = true,
                Location = new Point(220, 6),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            chevron = new PictureBox()
            {
                Size = new Size(18, 18),
                BackColor = Color.Transparent,
                Location = new Point(300, 8),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            // simple chevron: draw in paint
            chevron.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var p = new Pen(Color.White, 2))
                {
                    e.Graphics.DrawLines(p, new Point[] { new Point(4, 6), new Point(9, 11), new Point(14, 6) });
                }
            };

            // header container:
            var headerPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 36,
                BackColor = Color.FromArgb(40, 40, 40),
                Padding = new Padding(8)
            };
            headerPanel.Controls.Add(lblHeader);
            headerPanel.Controls.Add(lblBadge);
            headerPanel.Controls.Add(chevron);

            headerPanel.Click += (s, e) => ToggleExpanded();

            bodyContainer = new Panel()
            {
                Dock = DockStyle.Top,
                BackColor = Color.Transparent,
                AutoSize = false,
                AutoScroll = false,
                Height = 10
            };

            Controls.Add(bodyContainer);
            Controls.Add(headerPanel);
        }

        public void SetBadge(string text)
        {
            lblBadge.Text = text ?? "";
        }

        public void SetBody(Control c)
        {
            bodyContainer.Controls.Clear();
            c.Dock = DockStyle.Top;
            bodyContainer.Controls.Add(c);
            bodyContainer.Height = c.Height + 10;
        }

        public void SetExpanded(bool expanded)
        {
            bodyContainer.Visible = expanded;
            chevron.Invalidate();
        }

        public void ToggleExpanded()
        {
            bodyContainer.Visible = !bodyContainer.Visible;
            chevron.Invalidate();
        }

        // allow child controls to bubble click event payload
        public void RaiseChildButtonClicked(object payload)
        {
            ChildButtonClicked?.Invoke(this, payload);
        }
    }
}

