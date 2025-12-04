using RZLab.Clipper.Core.DocumentLegal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace RzLab.Clipper.ControlsLib;

public class LegalDocumentAnalysisViewer : UserControl
{
    private FlowLayoutPanel container;

    private SectionPanel summarySection;
    private SectionPanel risksSection;
    private SectionPanel clausesSection;
    private SectionPanel recomSection;

    public LegalDocumentAnalysisViewer()
    {
        BackColor = Color.FromArgb(30, 30, 30);
        Dock = DockStyle.Fill;

        container = new FlowLayoutPanel()
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            AutoSize = false,          // ⬅ FIX: jangan autosize
            Padding = new Padding(10)
        };

        Controls.Add(container);

        summarySection = new SectionPanel("Executive Summary");
        risksSection = new SectionPanel("Risk Assessment");
        clausesSection = new SectionPanel("Clause Insights");
        recomSection = new SectionPanel("Recommendations");

        container.Controls.Add(summarySection);
        container.Controls.Add(risksSection);
        container.Controls.Add(clausesSection);
        container.Controls.Add(recomSection);

        Resize += (s, e) => UpdateSectionWidth();
    }

    public void SetAnalysis(AnalysisResultModel model)
    {
        model ??= new AnalysisResultModel();

        summarySection.ClearContent();
        risksSection.ClearContent();
        clausesSection.ClearContent();
        recomSection.ClearContent();

        // SUMMARY
        summarySection.AddContent(CreateLabel(model.summary, bold: false));

        // RISKS
        foreach (var r in model.risks)
            risksSection.AddContent(CreateRiskCard(r.level, r.description));

        // CLAUSES
        foreach (var c in model.clauses)
            clausesSection.AddContent(CreateClauseCard(c.title, c.content));

        // RECOMMENDATIONS
        foreach (var r in model.recommendations)
            recomSection.AddContent(CreateLabel("• " + r));

        UpdateSectionWidth();
    }

    // ============================================================================================================
    // UI BUILDERS
    // ============================================================================================================

    private Label CreateLabel(string text, bool bold = false)
    {
        return new Label()
        {
            Text = text ?? "",
            AutoSize = true,
            MaximumSize = new Size(0, 0),
            ForeColor = Color.Gainsboro,
            Font = new Font("Segoe UI", 10f, bold ? FontStyle.Bold : FontStyle.Regular),
            Margin = new Padding(0, 0, 0, 8),
        };
    }

    private Panel CreateRiskCard(string level, string description)
    {
        Panel card = new Panel()
        {
            BackColor = Color.FromArgb(50, 50, 50),
            Margin = new Padding(0, 5, 0, 5),
            Padding = new Padding(10),
            AutoSize = true,
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
        };

        var badge = new Label()
        {
            Text = level.ToUpper(),
            AutoSize = true,
            BackColor = level.ToLower() switch
            {
                "high" => Color.FromArgb(200, 50, 50),
                "medium" => Color.FromArgb(220, 140, 30),
                "low" => Color.FromArgb(60, 180, 60),
                _ => Color.DimGray,
            },
            ForeColor = Color.White,
            Padding = new Padding(6, 2, 6, 2),
            Margin = new Padding(0, 0, 0, 8)
        };

        var text = CreateLabel(description);

        card.Controls.Add(badge);
        card.Controls.Add(text);

        return card;
    }

    private Panel CreateClauseCard(string title, string content)
    {
        Panel card = new Panel()
        {
            BackColor = Color.FromArgb(50, 50, 50),
            Margin = new Padding(0, 5, 0, 5),
            Padding = new Padding(10),
            AutoSize = true,
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
        };

        var lblTitle = CreateLabel(title, bold: true);
        var lblContent = CreateLabel(content);

        card.Controls.Add(lblTitle);
        card.Controls.Add(lblContent);

        return card;
    }

    // ============================================================================================================
    // WIDTH MANAGEMENT — FIX WIDTH ISSUE
    // ============================================================================================================

    private void UpdateSectionWidth()
    {
        if (container.Width <= 0) return;

        int w = container.ClientSize.Width - container.Padding.Horizontal - 20;
        if (w < 200) w = 200;

        foreach (SectionPanel sec in container.Controls.OfType<SectionPanel>())
            sec.SetContentWidth(w);
    }
}

// ============================================================================================================
// SECTION PANEL (HEADER + COLLAPSIBLE CONTENT)
// ============================================================================================================

public class SectionPanel : Panel
{
    private Label header;
    private FlowLayoutPanel contentPanel;

    public SectionPanel(string title)
    {
        BackColor = Color.FromArgb(40, 40, 40);
        AutoSize = true;                     // allow height grow
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Margin = new Padding(0, 10, 0, 5);
        Padding = new Padding(10);

        header = new Label()
        {
            Text = title,
            AutoSize = true,
            Font = new Font("Segoe UI", 11f, FontStyle.Bold),
            ForeColor = Color.White,
            Margin = new Padding(0, 0, 0, 10)
        };

        contentPanel = new FlowLayoutPanel()
        {
            AutoSize = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent,
            Margin = new Padding(0),
            Padding = new Padding(0)
        };

        Controls.Add(header);
        Controls.Add(contentPanel);
    }

    public void AddContent(Control c)
    {
        contentPanel.Controls.Add(c);
    }

    public void ClearContent()
    {
        contentPanel.Controls.Clear();
    }

    public void SetContentWidth(int width)
    {
        Width = width;

        int cw = width - 20;

        header.MaximumSize = new Size(cw, 0);

        foreach (Control c in contentPanel.Controls)
        {
            c.MaximumSize = new Size(cw, 0);
            c.Width = cw;
        }
    }
}