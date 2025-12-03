using RZLab.Clipper.Core.DocumentLegal;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RzLab.Clipper.ControlsLib;
public class DocumentCardControl : Panel
{
    private PictureBox iconFile;
    private Label lblName;
    private Label lblMeta;
    private Button btnMenu;

    public event EventHandler MenuClicked;

    public DocumentCardControl()
    {
        this.Height = 60;
        this.Dock = DockStyle.Top;
        this.BackColor = Color.FromArgb(55, 55, 55);
        this.Padding = new Padding(12);

        this.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        this.Margin = new Padding(0, 0, 0, 8);
        this.MinimumSize = new Size(300, 60);
        DoubleBuffered = true;

        // Soft hover
        this.MouseEnter += (s, e) => BackColor = Color.FromArgb(45, 45, 45);
        this.MouseLeave += (s, e) => BackColor = Color.FromArgb(55, 55, 55);

        // ICON
        iconFile = new PictureBox()
        {
            Size = new Size(32, 32),
            Location = new Point(10, 10),
            SizeMode = PictureBoxSizeMode.Zoom,
        };

        // TITLE
        lblName = new Label()
        {
            AutoSize = true,
            Font = new Font("Segoe UI Semibold", 9),
            ForeColor = Color.White,
            Location = new Point(45, 8)
        };

        // META TEXT
        lblMeta = new Label()
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 8),
            ForeColor = Color.White,
            Location = new Point(45, 30)
        };

        // MENU BUTTON (⋮)
        btnMenu = new Button()
        {
            Text = "⋮",
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            Width = 30,
            Height = 30,
            BackColor = Color.FromArgb(70, 70, 70),
        };
        btnMenu.FlatAppearance.BorderSize = 0;
        btnMenu.Location = new Point(this.Width - 40, 13);
        btnMenu.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        btnMenu.Click += (s, e) =>
        {
            MenuClicked?.Invoke(this, EventArgs.Empty);
        };

        this.Controls.Add(iconFile);
        this.Controls.Add(lblName);
        this.Controls.Add(lblMeta);
        this.Controls.Add(btnMenu);
    }

    public void SetData(string fileName, string fileType, DateTime uploadedAt, int relevantFacts, Image image)
    {
        lblName.Text = fileName;

        TimeSpan diff = DateTime.Now - uploadedAt;

        string timeAgo =
            diff.TotalDays >= 1 ? $"{(int)diff.TotalDays} days ago" :
            diff.TotalHours >= 1 ? $"{(int)diff.TotalHours} hours ago" :
            $"{(int)diff.TotalMinutes} minutes ago";

        lblMeta.Text = $"{timeAgo}    • {relevantFacts} pages";

        // Auto-choose icon
        iconFile.Image = image;
    }
}
