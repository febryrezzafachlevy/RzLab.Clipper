using RZLab.Clipper.Core.DocumentLegal;
using System;
using System.Drawing;
using System.Windows.Forms;

public class DocumentCard : Panel
{
    public DocumentDataModel Data { get; private set; }

    private Panel contentPanel;
    private Panel buttonPanel;

    private Label lblStatus;

    private Button btnAnalyze;
    private Button btnView;
    private Button btnDelete;

    public event EventHandler AnalyzeClicked;
    public event EventHandler ViewClicked;
    public event EventHandler DeleteClicked;

    public DocumentCard(DocumentDataModel data)
    {
        Data = data;

        Padding = new Padding(10, 15, 10, 5);
        BackColor = Color.FromArgb(55, 55, 55);
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        MinimumSize = new Size(660, 190);

        // Rounded corner
        this.Resize += (s, e) =>
        {
            this.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, Width, Height, 12, 12)
            );
        };

        lblStatus = new Label()
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = Color.FromArgb(70, 70, 70),
            Padding = new Padding(6, 2, 6, 2),
            Location = new Point(this.Width - 100, 10),
            Anchor = AnchorStyles.Top | AnchorStyles.Right
        };

        Controls.Add(lblStatus);
        if (data.analysis_result != null)
        {
            lblStatus.Text = "ANALYZED";
            lblStatus.BackColor = Color.FromArgb(0, 140, 60); // hijau
        }
        else
        {
            lblStatus.Text = "NOT ANALYZED";
            lblStatus.BackColor = Color.FromArgb(180, 90, 0); // oranye
        }
        // ============================================================
        // CONTENT PANEL (TOP)
        // ============================================================
        contentPanel = new Panel()
        {
            Dock = DockStyle.Fill,
            BackColor = Color.Transparent
        };

        Controls.Add(contentPanel);

        int y = 0;
        AddLabel(contentPanel, data.file_name, ref y, 10, FontStyle.Bold);
        AddLabel(contentPanel, "Type: " + data.document_type, ref y, 9);
        AddLabel(contentPanel, "Pages: " + data.metadata.page_count, ref y, 8);
        AddLabel(contentPanel, "Size: " + data.metadata.file_size_kb + " KB", ref y, 8);
        AddLabel(contentPanel, "Uploaded: " + data.uploaded_at.ToString("yyyy-MM-dd"), ref y, 8);

        // ============================================================
        // BUTTON PANEL (BOTTOM FIXED)
        // ============================================================
        buttonPanel = new Panel()
        {
            Dock = DockStyle.Bottom,
            Height = 48,
            BackColor = Color.FromArgb(40, 40, 40),
            Padding = new Padding(10, 5, 10, 5)
        };

        Controls.Add(buttonPanel);

        // Tombol View
        btnView = new Button()
        {
            Text = "View",
            Width = 80,
            Height = 30,
            BackColor = Color.FromArgb(70, 70, 70),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Anchor = AnchorStyles.Right | AnchorStyles.Bottom
        };
        btnView.FlatAppearance.BorderSize = 0;
        btnView.Click += (s, e) => ViewClicked?.Invoke(this, e);

        // Tombol Delete
        btnDelete = new Button()
        {
            Text = "Delete",
            Width = 80,
            Height = 30,
            BackColor = Color.FromArgb(120, 30, 30),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Anchor = AnchorStyles.Right | AnchorStyles.Bottom
        };
        btnDelete.FlatAppearance.BorderSize = 0;
        btnDelete.Click += (s, e) => DeleteClicked?.Invoke(this, e);

        btnAnalyze = new Button()
        {
            Text = "Analyze",
            Width = 80,
            Height = 30,
            BackColor = Color.FromArgb(70, 70, 120),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };

        btnAnalyze.FlatAppearance.BorderSize = 0;
        btnAnalyze.Click += (s, e) => AnalyzeClicked?.Invoke(this, e);

        // Tambahkan ke bottom panel
        buttonPanel.Controls.Add(btnView);
        buttonPanel.Controls.Add(btnDelete);
        buttonPanel.Controls.Add(btnAnalyze);

        // posisi default
        PositionButtons();

        // Reposition saat resize
        buttonPanel.Resize += (s, e) => PositionButtons();

        // Hover efek
        MouseEnter += (s, e) => BackColor = Color.FromArgb(45, 45, 45);
        MouseLeave += (s, e) => BackColor = Color.FromArgb(55, 55, 55);
    }

    private void PositionButtons()
    {
        btnDelete.Location = new Point(buttonPanel.Width - btnDelete.Width - 10, 8);
        btnView.Location = new Point(btnDelete.Left - btnView.Width - 8, 8);
        btnAnalyze.Location = new Point(btnView.Left - btnAnalyze.Width - 8, 8);
    }

    private void AddLabel(Panel panel, string text, ref int y, int size = 10, FontStyle style = FontStyle.Regular)
    {
        var lbl = new Label()
        {
            AutoSize = true,
            Text = text,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", size, style),
            Location = new Point(0, y)
        };

        y += lbl.Height + 2;
        panel.Controls.Add(lbl);
    }

    // Rounded rectangle helper
    [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn(
        int nLeftRect, int nTopRect,
        int nRightRect, int nBottomRect,
        int nWidthEllipse, int nHeightEllipse);
}
