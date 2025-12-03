using RZLab.Clipper.Core.DocumentLegal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RzLab.Clipper.ControlsLib;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class AnalysisViewerControl : UserControl
{
    private TabControl tab;
    private TabPage tabSummary, tabRisks, tabClauses, tabRecom;

    private AISummaryPanel summaryPanel;
    private FlowLayoutPanel panelRisks;
    private FlowLayoutPanel panelClauses;
    private FlowLayoutPanel panelRecom;

    public event EventHandler<int> PdfJumpRequested;
    
    public AnalysisViewerControl()
    {
        this.Dock = DockStyle.Fill;
        this.BackColor = Color.FromArgb(32, 32, 32);

        InitTabs();
        InitSummaryTab();
        InitRisksTab();
        InitClausesTab();
        InitRecommendationsTab();
    }

    //===========================================================
    // INITIALIZATION
    //===========================================================

    private void InitTabs()
    {
        tab = new TabControl()
        {
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10, FontStyle.Regular),
            Padding = new Point(10, 5)
        };

        tabSummary = new TabPage("Summary") { BackColor = Color.FromArgb(32, 32, 32) };
        tabRisks = new TabPage("Risks") { BackColor = Color.FromArgb(32, 32, 32) };
        tabClauses = new TabPage("Clauses") { BackColor = Color.FromArgb(32, 32, 32) };
        tabRecom = new TabPage("Recommendations") { BackColor = Color.FromArgb(32, 32, 32) };

        tab.Controls.AddRange(new[] { tabSummary, tabRisks, tabClauses, tabRecom });
        tab.SelectedIndexChanged += (s, e) =>
        {
            if (tab.SelectedTab == tabClauses)
                ForceRedrawClauses();
        };
        this.Controls.Add(tab);
    }

    private void InitSummaryTab()
    {
        summaryPanel = new AISummaryPanel()
        {
            Dock = DockStyle.Fill,
        };
        tabSummary.Controls.Add(summaryPanel);
    }

    private void InitRisksTab()
    {
        panelRisks = new FlowLayoutPanel()
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(10),
            BackColor = Color.FromArgb(32, 32, 32)
        };

        tabRisks.Controls.Add(panelRisks);
    }

    private void InitClausesTab()
    {
        panelClauses = new FlowLayoutPanel()
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(10),
            BackColor = Color.FromArgb(32, 32, 32)
        };

        tabClauses.Controls.Add(panelClauses);
    }

    private void InitRecommendationsTab()
    {
        panelRecom = new FlowLayoutPanel()
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(10),
            BackColor = Color.FromArgb(32, 32, 32)
        };

        tabRecom.Controls.Add(panelRecom);
    }

    //===========================================================
    // SETTER UTAMA
    //===========================================================

    public void SetAnalysis(AnalysisResultModel model)
    {
        if (model == null)
        {
            summaryPanel.SetSummary(null);
            panelRisks.Controls.Clear();
            panelClauses.Controls.Clear();
            panelRecom.Controls.Clear();
            return;
        }

        summaryPanel.SetSummary(model);
        SetRisks(model.risks);
        SetClauses(model.clauses);
        SetRecommendations(model.recommendations);
    }

    //===========================================================
    // SUMMARY
    //===========================================================

    public void SetSummary(AnalysisResultModel model)
    {
        summaryPanel.SetSummary(model);
    }

    //===========================================================
    // RISKS
    //===========================================================

    public void SetRisks(List<RiskModel> risks)
    {
        panelRisks.Controls.Clear();

        if (risks == null || risks.Count == 0)
        {
            panelRisks.Controls.Add(NoText("No risks detected."));
            return;
        }

        foreach (var risk in risks)
        {
            var card = BuildRiskCard(risk);
            panelRisks.Controls.Add(card);
        }
    }

    public static Panel AddRiskCard(RiskModel risk, Panel panelRisks)
    {
        var panel = new Panel()
        {
            Width = panelRisks.Width - 35,
            Height = 90,
            Margin = new Padding(5),
            BackColor = Color.FromArgb(45, 45, 45),
            Padding = new Padding(10)
        };

        string level = (risk.level ?? "medium").ToUpper();

        Color c = level switch
        {
            "HIGH" => Color.FromArgb(180, 40, 40),
            "MEDIUM" => Color.FromArgb(230, 140, 10),
            "LOW" => Color.FromArgb(40, 150, 60),
            _ => Color.Gray
        };

        var lbl = new Label()
        {
            Text = $"[{level}]  {risk.description}",
            AutoSize = false,
            Width = panel.Width - 20,
            Height = 60,
            ForeColor = Color.WhiteSmoke,
            Font = new Font("Segoe UI", 10),
        };

        var badge = new Label()
        {
            AutoSize = true,
            BackColor = c,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            Padding = new Padding(6, 2, 6, 2),
            Text = level
        };

        badge.Location = new Point(panel.Width - badge.Width - 20, 10);
        lbl.Location = new Point(10, 35);

        panel.Controls.Add(badge);
        panel.Controls.Add(lbl);

        return panel;
    }

    private Control BuildRiskCard(RiskModel risk)
    {
        var panel = new Panel()
        {
            Width = panelRisks.Width - 35,
            Height = 90,
            Margin = new Padding(5),
            BackColor = Color.FromArgb(45, 45, 45),
            Padding = new Padding(10)
        };

        string level = (risk.level ?? "medium").ToUpper();

        Color c = level switch
        {
            "HIGH" => Color.FromArgb(180, 40, 40),
            "MEDIUM" => Color.FromArgb(230, 140, 10),
            "LOW" => Color.FromArgb(40, 150, 60),
            _ => Color.Gray
        };

        var lbl = new Label()
        {
            Text = $"[{level}]  {risk.description}",
            AutoSize = false,
            Width = panel.Width - 20,
            Height = 60,
            ForeColor = Color.WhiteSmoke,
            Font = new Font("Segoe UI", 10),
        };

        var badge = new Label()
        {
            AutoSize = true,
            BackColor = c,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            Padding = new Padding(6, 2, 6, 2),
            Text = level
        };

        badge.Location = new Point(panel.Width - badge.Width - 20, 10);
        lbl.Location = new Point(10, 35);

        panel.Controls.Add(badge);
        panel.Controls.Add(lbl);

        return panel;
    }

    //===========================================================
    // CLAUSES
    //===========================================================

    public static Panel AddClauses(List<ClauseModel> clauses, Panel panelClauses)
    {
        panelClauses.Controls.Clear();

        if (clauses == null || clauses.Count == 0)
        {
            panelClauses.Controls.Add(NoText("No clauses found.", panelClauses));
            return panelClauses;
        }

        foreach (var c in clauses)
        {
            var card = new ClauseCardControl();
            card.Width = panelClauses.Width - 40;
            //card.Width = 500;
            //card.Height = 200;
            //card.BackColor = Color.Red; // DEBUG
            //card.BorderStyle = BorderStyle.FixedSingle; // DEBUG
            //card.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            card.SetClause(c);

            //card.JumpRequested += (s, page) =>
            //{
            //    PdfJumpRequested?.Invoke(this, page);
            //};


            panelClauses.Controls.Add(card);
        }

        // Fix layout immediately
        ForceRedrawClauses(panelClauses);

        return panelClauses;
    }

    public void SetClauses(List<ClauseModel> clauses)
    {
        panelClauses.Controls.Clear();

        if (clauses == null || clauses.Count == 0)
        {
            panelClauses.Controls.Add(NoText("No clauses found."));
            return;
        }

        foreach (var c in clauses)
        {
            var card = new ClauseCardControl();
            card.Width = panelClauses.Width - 40; 
            //card.Width = 500;
            //card.Height = 200;
            //card.BackColor = Color.Red; // DEBUG
            //card.BorderStyle = BorderStyle.FixedSingle; // DEBUG
            //card.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            card.SetClause(c);

            card.JumpRequested += (s, page) =>
            {
                PdfJumpRequested?.Invoke(this, page);
            };


            panelClauses.Controls.Add(card);
        }

        // Fix layout immediately
        ForceRedrawClauses();
    }

    //===========================================================
    // RECOMMENDATIONS
    //===========================================================

    public void SetRecommendations(List<string> recom)
    {
        panelRecom.Controls.Clear();

        if (recom == null || recom.Count == 0)
        {
            panelRecom.Controls.Add(NoText("No recommendations."));
            return;
        }

        foreach (var r in recom)
        {
            var lbl = new Label()
            {
                AutoSize = true,
                MaximumSize = new Size(panelRecom.Width - 30, 0),
                ForeColor = Color.Gainsboro,
                Font = new Font("Segoe UI", 10),
                Padding = new Padding(8),
                Text = "• " + r
            };
            lbl.BackColor = Color.FromArgb(50, 50, 50);

            panelRecom.Controls.Add(lbl);
        }
    }

    //===========================================================
    // UTIL
    //===========================================================
    private static Label NoText(string text, Panel panel)
    {
        return new Label()
        {
            Text = text,
            AutoSize = true,
            Padding = new Padding(10),
            ForeColor = Color.Gray,
            Font = new Font("Segoe UI", 10)
        };
    }
    private Label NoText(string text)
    {
        return new Label()
        {
            Text = text,
            AutoSize = true,
            Padding = new Padding(10),
            ForeColor = Color.Gray,
            Font = new Font("Segoe UI", 10)
        };
    }
    private void ForceRedrawClauses()
    {
        if (panelClauses.Controls.Count == 0)
            return;

        foreach (Control ctrl in panelClauses.Controls)
        {
            ctrl.Width = panelClauses.ClientSize.Width - 40;
        }

        panelClauses.PerformLayout();
        panelClauses.Invalidate();
        panelClauses.Refresh();
    }
    private static void ForceRedrawClauses(Panel panelClauses)
    {
        if (panelClauses.Controls.Count == 0)
            return;

        foreach (Control ctrl in panelClauses.Controls)
        {
            ctrl.Width = panelClauses.ClientSize.Width - 40;
        }

        panelClauses.PerformLayout();
        panelClauses.Invalidate();
        panelClauses.Refresh();
    }
}
