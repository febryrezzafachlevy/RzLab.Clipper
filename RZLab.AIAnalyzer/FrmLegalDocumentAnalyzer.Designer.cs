namespace RZLab.AIAnalyzer
{
    partial class FrmLegalDocumentAnalyzer
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
            label4 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            pnlHeader = new Panel();
            pnlContainer = new Panel();
            pnlBody = new Panel();
            pnlBodyContainer = new Panel();
            darkTabControl1 = new RzLab.Clipper.ControlsLib.DarkTabControl();
            tabPage1 = new TabPage();
            webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            tabPage2 = new TabPage();
            rtbAISummary = new RichTextBox();
            panel3 = new Panel();
            flowDocument = new FlowLayoutPanel();
            panel4 = new Panel();
            pnlBrowseFile = new Panel();
            cmbDocumentType = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            btnProcess = new Button();
            btnPackagePath = new Button();
            txtJsonFilePath = new TextBox();
            pnlLoader = new Panel();
            ((System.ComponentModel.ISupportInitialize)btnClose).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlContainer.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlBodyContainer.SuspendLayout();
            darkTabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView2).BeginInit();
            tabPage2.SuspendLayout();
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
            // label4
            // 
            label4.Dock = DockStyle.Left;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(10, 0);
            label4.Name = "label4";
            label4.Size = new Size(234, 34);
            label4.TabIndex = 9;
            label4.Text = "Legal Document Analyzer";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1103, 0);
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
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(48, 63, 159);
            pnlHeader.Controls.Add(label4);
            pnlHeader.Controls.Add(panel1);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(10, 0, 0, 0);
            pnlHeader.Size = new Size(1154, 34);
            pnlHeader.TabIndex = 0;
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.White;
            pnlContainer.Controls.Add(pnlBody);
            pnlContainer.Controls.Add(pnlHeader);
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.Location = new Point(5, 5);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(1154, 594);
            pnlContainer.TabIndex = 3;
            // 
            // pnlBody
            // 
            pnlBody.BackColor = Color.FromArgb(33, 33, 33);
            pnlBody.Controls.Add(pnlBodyContainer);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(0, 34);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(10);
            pnlBody.Size = new Size(1154, 560);
            pnlBody.TabIndex = 1;
            // 
            // pnlBodyContainer
            // 
            pnlBodyContainer.Controls.Add(darkTabControl1);
            pnlBodyContainer.Controls.Add(panel3);
            pnlBodyContainer.Controls.Add(flowDocument);
            pnlBodyContainer.Controls.Add(panel4);
            pnlBodyContainer.Controls.Add(pnlBrowseFile);
            pnlBodyContainer.Dock = DockStyle.Fill;
            pnlBodyContainer.Location = new Point(10, 10);
            pnlBodyContainer.Margin = new Padding(3, 2, 3, 2);
            pnlBodyContainer.Name = "pnlBodyContainer";
            pnlBodyContainer.Size = new Size(1134, 540);
            pnlBodyContainer.TabIndex = 0;
            // 
            // darkTabControl1
            // 
            darkTabControl1.Controls.Add(tabPage1);
            darkTabControl1.Controls.Add(tabPage2);
            darkTabControl1.Dock = DockStyle.Fill;
            darkTabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            darkTabControl1.ItemSize = new Size(180, 40);
            darkTabControl1.Location = new Point(322, 88);
            darkTabControl1.Multiline = true;
            darkTabControl1.Name = "darkTabControl1";
            darkTabControl1.SelectedIndex = 0;
            darkTabControl1.Size = new Size(812, 452);
            darkTabControl1.SizeMode = TabSizeMode.Fixed;
            darkTabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(33, 33, 33);
            tabPage1.Controls.Add(webView2);
            tabPage1.Location = new Point(4, 44);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(1);
            tabPage1.Size = new Size(804, 404);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Main Document";
            // 
            // webView2
            // 
            webView2.AllowExternalDrop = true;
            webView2.CreationProperties = null;
            webView2.DefaultBackgroundColor = Color.White;
            webView2.Dock = DockStyle.Fill;
            webView2.Location = new Point(1, 1);
            webView2.Name = "webView2";
            webView2.Size = new Size(802, 402);
            webView2.TabIndex = 0;
            webView2.ZoomFactor = 1D;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(33, 33, 33);
            tabPage2.Controls.Add(rtbAISummary);
            tabPage2.Location = new Point(4, 44);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(1);
            tabPage2.Size = new Size(804, 404);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "AI Summary";
            // 
            // rtbAISummary
            // 
            rtbAISummary.BackColor = Color.FromArgb(33, 33, 33);
            rtbAISummary.BorderStyle = BorderStyle.None;
            rtbAISummary.Dock = DockStyle.Fill;
            rtbAISummary.ForeColor = Color.WhiteSmoke;
            rtbAISummary.Location = new Point(1, 1);
            rtbAISummary.Name = "rtbAISummary";
            rtbAISummary.Size = new Size(802, 402);
            rtbAISummary.TabIndex = 0;
            rtbAISummary.Text = "";
            // 
            // panel3
            // 
            panel3.BackColor = Color.DarkGray;
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(319, 88);
            panel3.Name = "panel3";
            panel3.Size = new Size(3, 452);
            panel3.TabIndex = 4;
            // 
            // flowDocument
            // 
            flowDocument.AutoScroll = true;
            flowDocument.BorderStyle = BorderStyle.FixedSingle;
            flowDocument.Dock = DockStyle.Left;
            flowDocument.FlowDirection = FlowDirection.TopDown;
            flowDocument.Location = new Point(0, 88);
            flowDocument.Margin = new Padding(0, 0, 0, 3);
            flowDocument.Name = "flowDocument";
            flowDocument.Padding = new Padding(5);
            flowDocument.Size = new Size(319, 452);
            flowDocument.TabIndex = 3;
            flowDocument.WrapContents = false;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 76);
            panel4.Name = "panel4";
            panel4.Size = new Size(1134, 12);
            panel4.TabIndex = 2;
            // 
            // pnlBrowseFile
            // 
            pnlBrowseFile.BorderStyle = BorderStyle.FixedSingle;
            pnlBrowseFile.Controls.Add(pnlLoader);
            pnlBrowseFile.Controls.Add(cmbDocumentType);
            pnlBrowseFile.Controls.Add(label2);
            pnlBrowseFile.Controls.Add(label1);
            pnlBrowseFile.Controls.Add(btnProcess);
            pnlBrowseFile.Controls.Add(btnPackagePath);
            pnlBrowseFile.Controls.Add(txtJsonFilePath);
            pnlBrowseFile.Dock = DockStyle.Top;
            pnlBrowseFile.Location = new Point(0, 0);
            pnlBrowseFile.Name = "pnlBrowseFile";
            pnlBrowseFile.Size = new Size(1134, 76);
            pnlBrowseFile.TabIndex = 1;
            // 
            // cmbDocumentType
            // 
            cmbDocumentType.FormattingEnabled = true;
            cmbDocumentType.Location = new Point(111, 41);
            cmbDocumentType.Margin = new Padding(3, 2, 3, 2);
            cmbDocumentType.Name = "cmbDocumentType";
            cmbDocumentType.Size = new Size(177, 23);
            cmbDocumentType.TabIndex = 31;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(11, 43);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(94, 19);
            label2.TabIndex = 30;
            label2.Text = "Document Type";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(13, 13);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(65, 19);
            label1.TabIndex = 29;
            label1.Text = "Document";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnProcess
            // 
            btnProcess.FlatStyle = FlatStyle.Flat;
            btnProcess.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnProcess.ForeColor = Color.White;
            btnProcess.Location = new Point(293, 40);
            btnProcess.Margin = new Padding(2);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(100, 26);
            btnProcess.TabIndex = 22;
            btnProcess.Text = "Upload";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // btnPackagePath
            // 
            btnPackagePath.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnPackagePath.FlatStyle = FlatStyle.Flat;
            btnPackagePath.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPackagePath.ForeColor = Color.White;
            btnPackagePath.Location = new Point(561, 11);
            btnPackagePath.Margin = new Padding(2);
            btnPackagePath.Name = "btnPackagePath";
            btnPackagePath.Size = new Size(35, 25);
            btnPackagePath.TabIndex = 20;
            btnPackagePath.Text = "...";
            btnPackagePath.UseVisualStyleBackColor = true;
            btnPackagePath.Click += btnPackagePath_Click;
            // 
            // txtJsonFilePath
            // 
            txtJsonFilePath.BackColor = Color.FromArgb(33, 33, 33);
            txtJsonFilePath.BorderStyle = BorderStyle.FixedSingle;
            txtJsonFilePath.Font = new Font("Segoe UI", 9F);
            txtJsonFilePath.ForeColor = Color.White;
            txtJsonFilePath.Location = new Point(111, 12);
            txtJsonFilePath.Margin = new Padding(2);
            txtJsonFilePath.Name = "txtJsonFilePath";
            txtJsonFilePath.ReadOnly = true;
            txtJsonFilePath.Size = new Size(447, 23);
            txtJsonFilePath.TabIndex = 19;
            // 
            // pnlLoader
            // 
            pnlLoader.Location = new Point(151, 28);
            pnlLoader.Margin = new Padding(3, 2, 3, 2);
            pnlLoader.Name = "pnlLoader";
            pnlLoader.Size = new Size(1131, 452);
            pnlLoader.TabIndex = 34;
            // 
            // FrmLegalDocumentAnalyzer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1164, 604);
            ControlBox = false;
            Controls.Add(pnlContainer);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmLegalDocumentAnalyzer";
            Padding = new Padding(5);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmLegalDocumentAnalyzer";
            ((System.ComponentModel.ISupportInitialize)btnClose).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlContainer.ResumeLayout(false);
            pnlBody.ResumeLayout(false);
            pnlBodyContainer.ResumeLayout(false);
            darkTabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView2).EndInit();
            tabPage2.ResumeLayout(false);
            pnlBrowseFile.ResumeLayout(false);
            pnlBrowseFile.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox btnClose;
        private Label label4;
        private Panel panel1;
        private Panel panel2;
        private Panel pnlHeader;
        private Panel pnlContainer;
        private Panel pnlBody;
        private Panel pnlBodyContainer;
        private Panel panel4;
        private Panel pnlBrowseFile;
        private ComboBox cmbDocumentType;
        private Label label2;
        private Label label1;
        private Button btnProcess;
        private Button btnPackagePath;
        public TextBox txtJsonFilePath;
        private Panel pnlLoader;
        private FlowLayoutPanel flowDocument;
        private Panel panel3;
        private RzLab.Clipper.ControlsLib.DarkTabControl darkTabControl1;
        private TabPage tabPage1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
        private TabPage tabPage2;
        private RichTextBox rtbAISummary;
    }
}