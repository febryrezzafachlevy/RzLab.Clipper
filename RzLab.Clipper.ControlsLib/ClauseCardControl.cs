using RZLab.Clipper.Core.DocumentLegal;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RzLab.Clipper.ControlsLib;

public class ClauseCardControl : UserControl
{
    private Label lblTitle;
    private Label lblContent;
    private Label lblRisk;
    private Button btnJump;

    public event EventHandler<int> JumpRequested;
    private int clausePage = 0;

    public ClauseCardControl()
    {
        DoubleBuffered = true;
        BackColor = Color.FromArgb(45, 45, 45);
        Padding = new Padding(20);
        Height = 160;
        MinimumSize = new Size(400, 120);

        // ===== Title =====
        lblTitle = new Label()
        {
            AutoSize = true,
            Font = new Font("Segoe UI Semibold", 12f),
            ForeColor = Color.White,
            MaximumSize = new Size(1200, 0)
        };

        // ===== Content =====
        lblContent = new Label()
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 10f),
            ForeColor = Color.Gainsboro,
            MaximumSize = new Size(1200, 0),
            Padding = new Padding(0, 5, 0, 0)
        };

        // ===== Risk Badge =====
        lblRisk = new Label()
        {
            AutoSize = true,
            Font = new Font("Segoe UI Semibold", 9),
            Padding = new Padding(10, 3, 10, 3),
            ForeColor = Color.White,
            BackColor = Color.Transparent
        };

        // ===== Jump Button =====
        btnJump = new Button()
        {
            Text = "Jump",
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9),
            ForeColor = Color.White,
            BackColor = Color.FromArgb(70, 70, 70),
            Height = 26,
            Width = 65
        };
        btnJump.FlatAppearance.BorderSize = 0;

        btnJump.Click += (s, e) =>
        {
            JumpRequested?.Invoke(this, clausePage);
        };

        Controls.Add(lblTitle);
        Controls.Add(lblContent);
        Controls.Add(lblRisk);
        Controls.Add(btnJump);

        Resize += (s, e) => RepositionElements();
        MouseEnter += (s, e) => this.BackColor = Color.FromArgb(55, 55, 55);
        MouseLeave += (s, e) => this.BackColor = Color.FromArgb(45, 45, 45);
    }

    // ============================================================
    // POSITIONING
    // ============================================================

    private void RepositionElements()
    {
        lblTitle.Location = new Point(20, 20);
        lblContent.Location = new Point(20, lblTitle.Bottom + 5);

        // Badge on top-right
        lblRisk.Location = new Point(Width - lblRisk.Width - 30, 18);

        // Jump button next to badge
        btnJump.Location = new Point(Width - btnJump.Width - 30, lblRisk.Bottom + 8);

        Height = lblContent.Bottom + 30;
    }

    // ============================================================
    // PREMIUM DRAWING (rounded corners + shadow)
    // ============================================================

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        int radius = 12;

        using (GraphicsPath path = RoundedRect(new Rectangle(0, 0, Width - 1, Height - 1), radius))
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(Color.FromArgb(70, 70, 70), 1))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        DrawShadow(e.Graphics);
    }

    private void DrawShadow(Graphics g)
    {
        using (var brush = new SolidBrush(Color.FromArgb(40, 0, 0, 0)))
        {
            g.FillRectangle(brush, 4, Height - 2, Width - 8, 4);
        }
    }

    private GraphicsPath RoundedRect(Rectangle rect, int radius)
    {
        int d = radius * 2;
        GraphicsPath path = new GraphicsPath();
        path.AddArc(rect.X, rect.Y, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }

    // ============================================================
    // SET DATA
    // ============================================================

    public void SetClause(ClauseModel model)
    {
        lblTitle.Text = model.title;
        lblContent.Text = model.content;
        clausePage = model.page;

        // ===== Risk Badge Color =====
        string level = (model.risk ?? "MEDIUM").ToUpper();

        lblRisk.Text = level;

        lblRisk.BackColor = level switch
        {
            "HIGH" => Color.FromArgb(185, 40, 40),
            "MEDIUM" => Color.FromArgb(230, 150, 20),
            "LOW" => Color.FromArgb(40, 150, 70),
            _ => Color.Gray
        };

        RepositionElements();
        Invalidate();
    }
}