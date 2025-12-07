using RZLab.Clipper.Core.DocumentLegal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RzLab.Clipper.ControlsLib;

public class LegalDocumentClauseCardControl : Panel
{
    public LegalDocumentClauseCardControl(List<ClauseModel> clauses)
    {
        BackColor = Color.FromArgb(45, 45, 45);
        Padding = new Padding(15);
        Margin = new Padding(0, 5, 0, 5);
        Dock = DockStyle.Top;
        Height = 300;

        var pnlContent = AddItemClause(clauses);
        var header = AddHeader("Clause");

        Controls.Add(pnlContent);
        Controls.Add(header);
    }

    private Panel AddItemClause(List<ClauseModel> caluses)
    {
        var card = new FlowLayoutPanel()
        {
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true,               // wajib agar turun ke baris baru
            Margin = new Padding(0, 5, 0, 5),
            Padding = new Padding(10, 5, 10, 5),
            Dock = DockStyle.Top,
            Height = 500,
            BackColor = Color.Transparent,
        };

        foreach (var c in caluses)
        {
            var pnlContent = AddClause(c.title, c.content, c.risk);
            card.Controls.Add(pnlContent);
        }

        return card;
    }
    private Panel AddClause(string title, string summaryText, string risk)
    {
        var card = new Panel()
        {
            Width = 300,
            Height = 130,
            Margin = new Padding(0),
            Padding = new Padding(5),
            BackColor = Color.Transparent,
        };
        var pnlContent = AddContent(summaryText);
        var header = AddHeader(title, risk);

        card.Controls.Add(pnlContent);
        card.Controls.Add(header);

        return card;
    }
    private Panel AddHeader(string title, string risk)
    {
        var header = new Panel()
        {
            Dock = DockStyle.Top,
            Height = 30,
            Margin = new Padding(0),
            Padding = new Padding(5)
        };

        header.Controls.Add(AddSourceInfo(risk));
        header.Controls.Add(AddTitle(title));
        header.Controls.Add(AddIcontTitle());
        return header;
    }
    private Panel AddHeader(string title)
    {
        var header = new Panel()
        {
            Dock = DockStyle.Top,
            Height = 30,
            Margin = new Padding(0),
            Padding = new Padding(5)
        };
        header.Controls.Add(AddTitle(title));
        header.Controls.Add(AddIcontTitle());
        return header;
    }
    private Panel AddContent(string content)
    {
        var pnlContent = new Panel()
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(5),
            AutoScroll = true,
        };
        var lblContent = new Label()
        {
            Dock = DockStyle.Fill,
            Text = content,
            Font = new Font("Segoe UI", 10f),
            ForeColor = Color.Gainsboro,
            Margin = new Padding(0, 10, 0, 0)
        };
        pnlContent.Controls.Add(lblContent);
        return pnlContent;
    }
    private Panel AddIcontTitle()
    {
        var pnlIcon = new Panel()
        {
            Dock = DockStyle.Left,
            Width = 30,
        };
        var picIcon = new PictureBox()
        {
            Dock = DockStyle.Fill,
            Image = CreateSummaryIcon(),
            SizeMode = PictureBoxSizeMode.Zoom,
            Width = 18,
            Height = 18,
        };
        pnlIcon.Controls.Add(picIcon);


        return pnlIcon;
    }
    private Panel AddTitle(string Title)
    {
        var pnlTitle = new Panel()
        {
            Dock = DockStyle.Left,
            Width = 150,
        };
        var lblTitle = new Label()
        {
            Dock = DockStyle.Fill,
            Text = Title,
            Font = new Font("Segoe UI Semibold", 11f),
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleLeft,
        };
        pnlTitle.Controls.Add(lblTitle);

        return pnlTitle;
    }
    private Panel AddSourceInfo(string risk)
    {
        Color c = risk.ToUpper() switch
        {
            "HIGH" => Color.FromArgb(180, 40, 40),
            "MEDIUM" => Color.FromArgb(230, 140, 10),
            "LOW" => Color.FromArgb(40, 150, 60),
            _ => Color.Gray
        };
        var pnlSourceInfo = new Panel()
        {
            Dock = DockStyle.Right,
            Width = 100,
            BackColor = c,
        };

        var lblSourceInfo = new Label()
        {
            Dock = DockStyle.Fill,
            Text = risk.ToUpper(),
            Font = new Font("Segoe UI", 9f, FontStyle.Bold),
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleCenter,
        };
        pnlSourceInfo.Controls.Add(lblSourceInfo);

        return pnlSourceInfo;
    }

    // Simple gray icon (document-like)
    private Image CreateSummaryIcon()
    {
        Bitmap bmp = new Bitmap(24, 24);
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);

            Pen p = new Pen(Color.White, 2);

            // Draw rectangular document
            g.DrawRectangle(p, 4, 4, 16, 18);

            // Lines inside document
            g.DrawLine(p, 7, 9, 17, 9);
            g.DrawLine(p, 7, 13, 17, 13);
            g.DrawLine(p, 7, 17, 17, 17);
        }

        return bmp;
    }
}
