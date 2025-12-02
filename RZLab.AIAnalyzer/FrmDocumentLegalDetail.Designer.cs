namespace RZLab.AIAnalyzer
{
    partial class FrmDocumentLegalDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnClose = new PictureBox();
            panel1 = new Panel();
            panel2 = new Panel();
            pnlGrid = new Panel();
            darkTabControl1 = new RzLab.Clipper.ControlsLib.DarkTabControl();
            tabPage1 = new TabPage();
            webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            tabPage2 = new TabPage();
            rtbAISummary = new RichTextBox();
            label4 = new Label();
            pnlHeader = new Panel();
            pnlContainer = new Panel();
            pnlBody = new Panel();
            pnlBodyContainer = new Panel();
            panel4 = new Panel();
            pnlBrowseFile = new Panel();
            btnAnalyze = new Button();
            lblStatus = new Label();
            btnRefresh = new Button();
            lblDocumentType = new Label();
            lblDocumentName = new Label();
            label2 = new Label();
            label1 = new Label();
            pnlLoader = new Panel();
            ((System.ComponentModel.ISupportInitialize)btnClose).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            pnlGrid.SuspendLayout();
            darkTabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView2).BeginInit();
            tabPage2.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlContainer.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlBodyContainer.SuspendLayout();
            pnlBrowseFile.SuspendLayout();
            SuspendLayout();
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(211, 47, 47);
            btnClose.Cursor = Cursors.Hand;
            btnClose.Dock = DockStyle.Fill;
            btnClose.Image = Properties.Resources.close_100px;
            btnClose.Location = new Point(2, 2);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(28, 30);
            btnClose.SizeMode = PictureBoxSizeMode.Zoom;
            btnClose.TabIndex = 0;
            btnClose.TabStop = false;
            btnClose.Click += btnClose_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1054, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(51, 34);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnClose);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(19, 0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(2);
            panel2.Size = new Size(32, 34);
            panel2.TabIndex = 1;
            // 
            // pnlGrid
            // 
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            pnlGrid.Controls.Add(darkTabControl1);
            pnlGrid.Dock = DockStyle.Top;
            pnlGrid.Location = new Point(0, 94);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Size = new Size(1085, 448);
            pnlGrid.TabIndex = 5;
            // 
            // darkTabControl1
            // 
            darkTabControl1.Controls.Add(tabPage1);
            darkTabControl1.Controls.Add(tabPage2);
            darkTabControl1.Dock = DockStyle.Fill;
            darkTabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            darkTabControl1.ItemSize = new Size(180, 40);
            darkTabControl1.Location = new Point(0, 0);
            darkTabControl1.Multiline = true;
            darkTabControl1.Name = "darkTabControl1";
            darkTabControl1.SelectedIndex = 0;
            darkTabControl1.Size = new Size(1083, 446);
            darkTabControl1.SizeMode = TabSizeMode.Fixed;
            darkTabControl1.TabIndex = 0;
            darkTabControl1.SelectedIndexChanged += darkTabControl1_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(33, 33, 33);
            tabPage1.Controls.Add(webView2);
            tabPage1.Location = new Point(4, 44);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1075, 398);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Main Document";
            // 
            // webView2
            // 
            webView2.AllowExternalDrop = true;
            webView2.CreationProperties = null;
            webView2.DefaultBackgroundColor = Color.White;
            webView2.Dock = DockStyle.Fill;
            webView2.Location = new Point(3, 3);
            webView2.Name = "webView2";
            webView2.Size = new Size(1069, 392);
            webView2.TabIndex = 0;
            webView2.ZoomFactor = 1D;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(33, 33, 33);
            tabPage2.Controls.Add(rtbAISummary);
            tabPage2.Location = new Point(4, 44);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1075, 398);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "AI Summary";
            // 
            // rtbAISummary
            // 
            rtbAISummary.BackColor = Color.FromArgb(33, 33, 33);
            rtbAISummary.BorderStyle = BorderStyle.None;
            rtbAISummary.Dock = DockStyle.Fill;
            rtbAISummary.ForeColor = Color.WhiteSmoke;
            rtbAISummary.Location = new Point(3, 3);
            rtbAISummary.Name = "rtbAISummary";
            rtbAISummary.Size = new Size(1069, 392);
            rtbAISummary.TabIndex = 0;
            rtbAISummary.Text = "";
            // 
            // label4
            // 
            label4.Dock = DockStyle.Left;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(10, 0);
            label4.Name = "label4";
            label4.Size = new Size(234, 34);
            label4.TabIndex = 9;
            label4.Text = "Document Legal Analyzer";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(48, 63, 159);
            pnlHeader.Controls.Add(label4);
            pnlHeader.Controls.Add(panel1);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(10, 0, 0, 0);
            pnlHeader.Size = new Size(1105, 34);
            pnlHeader.TabIndex = 0;
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.White;
            pnlContainer.Controls.Add(pnlBody);
            pnlContainer.Controls.Add(pnlHeader);
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.Location = new Point(4, 4);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(1105, 594);
            pnlContainer.TabIndex = 3;
            // 
            // pnlBody
            // 
            pnlBody.BackColor = Color.FromArgb(33, 33, 33);
            pnlBody.Controls.Add(pnlBodyContainer);
            pnlBody.Controls.Add(pnlLoader);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(0, 34);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(10);
            pnlBody.Size = new Size(1105, 560);
            pnlBody.TabIndex = 1;
            // 
            // pnlBodyContainer
            // 
            pnlBodyContainer.Controls.Add(pnlGrid);
            pnlBodyContainer.Controls.Add(panel4);
            pnlBodyContainer.Controls.Add(pnlBrowseFile);
            pnlBodyContainer.Dock = DockStyle.Fill;
            pnlBodyContainer.Location = new Point(10, 10);
            pnlBodyContainer.Margin = new Padding(3, 2, 3, 2);
            pnlBodyContainer.Name = "pnlBodyContainer";
            pnlBodyContainer.Size = new Size(1085, 540);
            pnlBodyContainer.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 82);
            panel4.Name = "panel4";
            panel4.Size = new Size(1085, 12);
            panel4.TabIndex = 2;
            // 
            // pnlBrowseFile
            // 
            pnlBrowseFile.BorderStyle = BorderStyle.FixedSingle;
            pnlBrowseFile.Controls.Add(btnAnalyze);
            pnlBrowseFile.Controls.Add(lblStatus);
            pnlBrowseFile.Controls.Add(btnRefresh);
            pnlBrowseFile.Controls.Add(lblDocumentType);
            pnlBrowseFile.Controls.Add(lblDocumentName);
            pnlBrowseFile.Controls.Add(label2);
            pnlBrowseFile.Controls.Add(label1);
            pnlBrowseFile.Dock = DockStyle.Top;
            pnlBrowseFile.Location = new Point(0, 0);
            pnlBrowseFile.Name = "pnlBrowseFile";
            pnlBrowseFile.Size = new Size(1085, 82);
            pnlBrowseFile.TabIndex = 1;
            // 
            // btnAnalyze
            // 
            btnAnalyze.FlatStyle = FlatStyle.Flat;
            btnAnalyze.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAnalyze.ForeColor = Color.White;
            btnAnalyze.Location = new Point(972, 45);
            btnAnalyze.Margin = new Padding(2);
            btnAnalyze.Name = "btnAnalyze";
            btnAnalyze.Size = new Size(100, 26);
            btnAnalyze.TabIndex = 36;
            btnAnalyze.Text = "Analyze";
            btnAnalyze.UseVisualStyleBackColor = true;
            btnAnalyze.Click += btnAnalyze_Click;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.ForeColor = Color.White;
            lblStatus.Location = new Point(933, 9);
            lblStatus.Margin = new Padding(2, 0, 2, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(149, 19);
            lblStatus.TabIndex = 35;
            lblStatus.Text = "Document";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnRefresh
            // 
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(868, 45);
            btnRefresh.Margin = new Padding(2);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 26);
            btnRefresh.TabIndex = 34;
            btnRefresh.Text = "Reload";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // lblDocumentType
            // 
            lblDocumentType.Font = new Font("Segoe UI", 9F);
            lblDocumentType.ForeColor = Color.White;
            lblDocumentType.Location = new Point(144, 45);
            lblDocumentType.Margin = new Padding(2, 0, 2, 0);
            lblDocumentType.Name = "lblDocumentType";
            lblDocumentType.Size = new Size(212, 19);
            lblDocumentType.TabIndex = 32;
            lblDocumentType.Text = "Document";
            lblDocumentType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDocumentName
            // 
            lblDocumentName.Font = new Font("Segoe UI", 9F);
            lblDocumentName.ForeColor = Color.White;
            lblDocumentName.Location = new Point(144, 13);
            lblDocumentName.Margin = new Padding(2, 0, 2, 0);
            lblDocumentName.Name = "lblDocumentName";
            lblDocumentName.Size = new Size(360, 19);
            lblDocumentName.TabIndex = 31;
            lblDocumentName.Text = "Document";
            lblDocumentName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(18, 45);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(105, 19);
            label2.TabIndex = 30;
            label2.Text = "Document Type";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(18, 13);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(105, 19);
            label1.TabIndex = 29;
            label1.Text = "Document";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlLoader
            // 
            pnlLoader.Location = new Point(10, 104);
            pnlLoader.Margin = new Padding(3, 2, 3, 2);
            pnlLoader.Name = "pnlLoader";
            pnlLoader.Size = new Size(1085, 447);
            pnlLoader.TabIndex = 33;
            // 
            // FrmDocumentLegalDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1113, 602);
            Controls.Add(pnlContainer);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmDocumentLegalDetail";
            Padding = new Padding(4);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmDocumentLegalDetail";
            ((System.ComponentModel.ISupportInitialize)btnClose).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            pnlGrid.ResumeLayout(false);
            darkTabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView2).EndInit();
            tabPage2.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlContainer.ResumeLayout(false);
            pnlBody.ResumeLayout(false);
            pnlBodyContainer.ResumeLayout(false);
            pnlBrowseFile.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox btnClose;
        private Panel panel1;
        private Panel panel2;
        private Panel pnlGrid;
        private Label label4;
        private Panel pnlHeader;
        private Panel pnlContainer;
        private Panel pnlBody;
        private Panel pnlBodyContainer;
        private Panel panel4;
        private Panel pnlBrowseFile;
        private Label label2;
        private Label label1;
        private Label lblDocumentType;
        private Label lblDocumentName;
        private RzLab.Clipper.ControlsLib.DarkTabControl darkTabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
        private Panel pnlLoader;
        private Button btnRefresh;
        private Label lblStatus;
        private Button btnAnalyze;
        private RichTextBox rtbAISummary;
    }
}