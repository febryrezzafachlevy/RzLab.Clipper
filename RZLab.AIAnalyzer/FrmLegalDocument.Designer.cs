namespace RZLab.AIAnalyzer
{
    partial class FrmLegalDocument
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
            pnlContainer = new Panel();
            pnlBody = new Panel();
            pnlRightSide = new Panel();
            pnlAISummary = new Panel();
            panel6 = new Panel();
            panel5 = new Panel();
            pnlDocumentName = new Panel();
            lblDocumentName = new Label();
            picDocument = new PictureBox();
            lblStatus = new Label();
            pnlLeftSide = new Panel();
            flowDocument = new FlowLayoutPanel();
            panel3 = new Panel();
            pnlBrowseFile = new Panel();
            cmbDocumentType = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            btnProcess = new Button();
            btnPackagePath = new Button();
            txtJsonFilePath = new TextBox();
            pnlLoader = new Panel();
            pnlHeader = new Panel();
            btnAnalyze = new Button();
            btnRefresh = new Button();
            label4 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            btnClose = new PictureBox();
            pnlContainer.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlRightSide.SuspendLayout();
            panel5.SuspendLayout();
            pnlDocumentName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picDocument).BeginInit();
            pnlLeftSide.SuspendLayout();
            pnlBrowseFile.SuspendLayout();
            pnlHeader.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnClose).BeginInit();
            SuspendLayout();
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.FromArgb(33, 33, 33);
            pnlContainer.Controls.Add(pnlBody);
            pnlContainer.Controls.Add(pnlHeader);
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.Location = new Point(5, 5);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(1124, 645);
            pnlContainer.TabIndex = 0;
            // 
            // pnlBody
            // 
            pnlBody.Controls.Add(pnlRightSide);
            pnlBody.Controls.Add(pnlLeftSide);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(0, 34);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(5);
            pnlBody.Size = new Size(1124, 611);
            pnlBody.TabIndex = 2;
            // 
            // pnlRightSide
            // 
            pnlRightSide.Controls.Add(pnlAISummary);
            pnlRightSide.Controls.Add(panel6);
            pnlRightSide.Controls.Add(panel5);
            pnlRightSide.Dock = DockStyle.Fill;
            pnlRightSide.Location = new Point(411, 5);
            pnlRightSide.Name = "pnlRightSide";
            pnlRightSide.Padding = new Padding(10);
            pnlRightSide.Size = new Size(708, 601);
            pnlRightSide.TabIndex = 1;
            // 
            // pnlAISummary
            // 
            pnlAISummary.Dock = DockStyle.Fill;
            pnlAISummary.Location = new Point(10, 53);
            pnlAISummary.Name = "pnlAISummary";
            pnlAISummary.Size = new Size(688, 538);
            pnlAISummary.TabIndex = 13;
            // 
            // panel6
            // 
            panel6.BackColor = Color.Transparent;
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(10, 43);
            panel6.Name = "panel6";
            panel6.Size = new Size(688, 10);
            panel6.TabIndex = 12;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(pnlDocumentName);
            panel5.Controls.Add(lblStatus);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(10, 10);
            panel5.Name = "panel5";
            panel5.Size = new Size(688, 33);
            panel5.TabIndex = 8;
            // 
            // pnlDocumentName
            // 
            pnlDocumentName.Controls.Add(lblDocumentName);
            pnlDocumentName.Controls.Add(picDocument);
            pnlDocumentName.Dock = DockStyle.Left;
            pnlDocumentName.Location = new Point(0, 0);
            pnlDocumentName.Name = "pnlDocumentName";
            pnlDocumentName.Size = new Size(286, 31);
            pnlDocumentName.TabIndex = 31;
            // 
            // lblDocumentName
            // 
            lblDocumentName.Dock = DockStyle.Left;
            lblDocumentName.FlatStyle = FlatStyle.Flat;
            lblDocumentName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDocumentName.ForeColor = Color.WhiteSmoke;
            lblDocumentName.Location = new Point(37, 0);
            lblDocumentName.Name = "lblDocumentName";
            lblDocumentName.Size = new Size(237, 31);
            lblDocumentName.TabIndex = 1;
            lblDocumentName.Text = "Legal Document.pdf";
            lblDocumentName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // picDocument
            // 
            picDocument.Dock = DockStyle.Left;
            picDocument.Image = Properties.Resources.squared_menu_25px;
            picDocument.Location = new Point(0, 0);
            picDocument.Name = "picDocument";
            picDocument.Size = new Size(37, 31);
            picDocument.SizeMode = PictureBoxSizeMode.Zoom;
            picDocument.TabIndex = 0;
            picDocument.TabStop = false;
            // 
            // lblStatus
            // 
            lblStatus.BackColor = Color.Orange;
            lblStatus.Dock = DockStyle.Right;
            lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatus.ForeColor = Color.Black;
            lblStatus.Location = new Point(538, 0);
            lblStatus.Margin = new Padding(2, 0, 2, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(148, 31);
            lblStatus.TabIndex = 30;
            lblStatus.Text = "NOT ANALYZED";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlLeftSide
            // 
            pnlLeftSide.BorderStyle = BorderStyle.FixedSingle;
            pnlLeftSide.Controls.Add(flowDocument);
            pnlLeftSide.Controls.Add(panel3);
            pnlLeftSide.Controls.Add(pnlBrowseFile);
            pnlLeftSide.Dock = DockStyle.Left;
            pnlLeftSide.Location = new Point(5, 5);
            pnlLeftSide.Name = "pnlLeftSide";
            pnlLeftSide.Size = new Size(406, 601);
            pnlLeftSide.TabIndex = 0;
            // 
            // flowDocument
            // 
            flowDocument.AutoScroll = true;
            flowDocument.BorderStyle = BorderStyle.FixedSingle;
            flowDocument.Dock = DockStyle.Fill;
            flowDocument.FlowDirection = FlowDirection.TopDown;
            flowDocument.Location = new Point(3, 76);
            flowDocument.Margin = new Padding(0, 0, 0, 3);
            flowDocument.Name = "flowDocument";
            flowDocument.Padding = new Padding(5);
            flowDocument.Size = new Size(401, 523);
            flowDocument.TabIndex = 6;
            flowDocument.WrapContents = false;
            // 
            // panel3
            // 
            panel3.BackColor = Color.DarkGray;
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(0, 76);
            panel3.Name = "panel3";
            panel3.Size = new Size(3, 523);
            panel3.TabIndex = 5;
            // 
            // pnlBrowseFile
            // 
            pnlBrowseFile.BorderStyle = BorderStyle.FixedSingle;
            pnlBrowseFile.Controls.Add(cmbDocumentType);
            pnlBrowseFile.Controls.Add(label2);
            pnlBrowseFile.Controls.Add(label1);
            pnlBrowseFile.Controls.Add(btnProcess);
            pnlBrowseFile.Controls.Add(btnPackagePath);
            pnlBrowseFile.Controls.Add(txtJsonFilePath);
            pnlBrowseFile.Controls.Add(pnlLoader);
            pnlBrowseFile.Dock = DockStyle.Top;
            pnlBrowseFile.Location = new Point(0, 0);
            pnlBrowseFile.Name = "pnlBrowseFile";
            pnlBrowseFile.Size = new Size(404, 76);
            pnlBrowseFile.TabIndex = 2;
            // 
            // cmbDocumentType
            // 
            cmbDocumentType.FormattingEnabled = true;
            cmbDocumentType.Location = new Point(111, 41);
            cmbDocumentType.Margin = new Padding(3, 2, 3, 2);
            cmbDocumentType.Name = "cmbDocumentType";
            cmbDocumentType.Size = new Size(175, 23);
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
            btnProcess.Location = new Point(295, 40);
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
            btnPackagePath.Location = new Point(360, 12);
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
            txtJsonFilePath.Size = new Size(245, 23);
            txtJsonFilePath.TabIndex = 19;
            // 
            // pnlLoader
            // 
            pnlLoader.Location = new Point(2, 2);
            pnlLoader.Margin = new Padding(3, 2, 3, 2);
            pnlLoader.Name = "pnlLoader";
            pnlLoader.Size = new Size(1139, 571);
            pnlLoader.TabIndex = 35;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(48, 63, 159);
            pnlHeader.Controls.Add(btnAnalyze);
            pnlHeader.Controls.Add(btnRefresh);
            pnlHeader.Controls.Add(label4);
            pnlHeader.Controls.Add(panel1);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(10, 0, 0, 0);
            pnlHeader.Size = new Size(1124, 34);
            pnlHeader.TabIndex = 1;
            // 
            // btnAnalyze
            // 
            btnAnalyze.BackColor = Color.FromArgb(128, 128, 255);
            btnAnalyze.FlatAppearance.BorderSize = 0;
            btnAnalyze.FlatStyle = FlatStyle.Flat;
            btnAnalyze.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAnalyze.ForeColor = Color.White;
            btnAnalyze.Location = new Point(952, 7);
            btnAnalyze.Margin = new Padding(2);
            btnAnalyze.Name = "btnAnalyze";
            btnAnalyze.Size = new Size(100, 22);
            btnAnalyze.TabIndex = 38;
            btnAnalyze.Text = "Analyze";
            btnAnalyze.TextAlign = ContentAlignment.TopCenter;
            btnAnalyze.UseVisualStyleBackColor = false;
            btnAnalyze.Click += btnAnalyze_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(224, 224, 224);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.Black;
            btnRefresh.Location = new Point(848, 7);
            btnRefresh.Margin = new Padding(2);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 22);
            btnRefresh.TabIndex = 37;
            btnRefresh.Text = "Reload";
            btnRefresh.TextAlign = ContentAlignment.TopCenter;
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
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
            panel1.Location = new Point(1073, 0);
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
            // FrmLegalDocument
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1134, 655);
            ControlBox = false;
            Controls.Add(pnlContainer);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLegalDocument";
            Padding = new Padding(5);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmLegalDocument";
            pnlContainer.ResumeLayout(false);
            pnlBody.ResumeLayout(false);
            pnlRightSide.ResumeLayout(false);
            panel5.ResumeLayout(false);
            pnlDocumentName.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picDocument).EndInit();
            pnlLeftSide.ResumeLayout(false);
            pnlBrowseFile.ResumeLayout(false);
            pnlBrowseFile.PerformLayout();
            pnlHeader.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)btnClose).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlContainer;
        private Panel pnlHeader;
        private Label label4;
        private Panel panel1;
        private Panel panel2;
        private PictureBox btnClose;
        private Panel pnlBody;
        private Panel pnlLeftSide;
        private Panel pnlBrowseFile;
        private ComboBox cmbDocumentType;
        private Label label2;
        private Label label1;
        private Button btnProcess;
        private Button btnPackagePath;
        public TextBox txtJsonFilePath;
        private Panel pnlRightSide;
        private Panel panel3;
        private FlowLayoutPanel flowDocument;
        private Panel panel5;
        private Panel pnlLoader;
        private Label lblStatus;
        private Button btnAnalyze;
        private Button btnRefresh;
        private Panel pnlDocumentName;
        private PictureBox picDocument;
        private Label lblDocumentName;
        private Panel panel6;
        private Panel pnlAISummary;
    }
}