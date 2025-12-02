namespace RZLab.AIAnalyzer
{
    partial class FrmDocumentLegalAnalyzer
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
            pnlBody = new Panel();
            pnlBodyContainer = new Panel();
            pnlGrid = new Panel();
            flowCards = new FlowLayoutPanel();
            panel4 = new Panel();
            pnlBrowseFile = new Panel();
            cmbDocumentType = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            btnProcess = new Button();
            btnPackagePath = new Button();
            txtJsonFilePath = new TextBox();
            pnlLoader = new Panel();
            pnlContainer = new Panel();
            pnlHeader = new Panel();
            label4 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            btnClose = new PictureBox();
            pnlBody.SuspendLayout();
            pnlBodyContainer.SuspendLayout();
            pnlGrid.SuspendLayout();
            pnlBrowseFile.SuspendLayout();
            pnlContainer.SuspendLayout();
            pnlHeader.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnClose).BeginInit();
            SuspendLayout();
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
            pnlBody.Size = new Size(650, 490);
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
            pnlBodyContainer.Size = new Size(630, 470);
            pnlBodyContainer.TabIndex = 0;
            // 
            // pnlGrid
            // 
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            pnlGrid.Controls.Add(flowCards);
            pnlGrid.Dock = DockStyle.Top;
            pnlGrid.Location = new Point(0, 94);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Size = new Size(630, 376);
            pnlGrid.TabIndex = 5;
            // 
            // flowCards
            // 
            flowCards.AutoScroll = true;
            flowCards.BackColor = Color.FromArgb(33, 33, 33);
            flowCards.Dock = DockStyle.Fill;
            flowCards.FlowDirection = FlowDirection.TopDown;
            flowCards.Location = new Point(0, 0);
            flowCards.Margin = new Padding(3, 2, 3, 2);
            flowCards.Name = "flowCards";
            flowCards.Padding = new Padding(13, 11, 13, 11);
            flowCards.Size = new Size(628, 374);
            flowCards.TabIndex = 0;
            flowCards.WrapContents = false;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 82);
            panel4.Name = "panel4";
            panel4.Size = new Size(630, 12);
            panel4.TabIndex = 2;
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
            pnlBrowseFile.Dock = DockStyle.Top;
            pnlBrowseFile.Location = new Point(0, 0);
            pnlBrowseFile.Name = "pnlBrowseFile";
            pnlBrowseFile.Size = new Size(630, 82);
            pnlBrowseFile.TabIndex = 1;
            // 
            // cmbDocumentType
            // 
            cmbDocumentType.FormattingEnabled = true;
            cmbDocumentType.Location = new Point(129, 48);
            cmbDocumentType.Margin = new Padding(3, 2, 3, 2);
            cmbDocumentType.Name = "cmbDocumentType";
            cmbDocumentType.Size = new Size(203, 23);
            cmbDocumentType.TabIndex = 31;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(18, 50);
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
            // btnProcess
            // 
            btnProcess.FlatStyle = FlatStyle.Flat;
            btnProcess.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnProcess.ForeColor = Color.White;
            btnProcess.Location = new Point(514, 45);
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
            btnPackagePath.Location = new Point(579, 11);
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
            txtJsonFilePath.Location = new Point(129, 12);
            txtJsonFilePath.Margin = new Padding(2);
            txtJsonFilePath.Name = "txtJsonFilePath";
            txtJsonFilePath.ReadOnly = true;
            txtJsonFilePath.Size = new Size(447, 23);
            txtJsonFilePath.TabIndex = 19;
            // 
            // pnlLoader
            // 
            pnlLoader.Location = new Point(10, 10);
            pnlLoader.Margin = new Padding(3, 2, 3, 2);
            pnlLoader.Name = "pnlLoader";
            pnlLoader.Size = new Size(630, 469);
            pnlLoader.TabIndex = 34;
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.White;
            pnlContainer.Controls.Add(pnlBody);
            pnlContainer.Controls.Add(pnlHeader);
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.Location = new Point(4, 4);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(650, 524);
            pnlContainer.TabIndex = 2;
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
            pnlHeader.Size = new Size(650, 34);
            pnlHeader.TabIndex = 0;
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
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(599, 0);
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
            // FrmDocumentLegalAnalyzer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(658, 532);
            ControlBox = false;
            Controls.Add(pnlContainer);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmDocumentLegalAnalyzer";
            Padding = new Padding(4);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmDocumentLegalAnalyzer";
            pnlBody.ResumeLayout(false);
            pnlBodyContainer.ResumeLayout(false);
            pnlGrid.ResumeLayout(false);
            pnlBrowseFile.ResumeLayout(false);
            pnlBrowseFile.PerformLayout();
            pnlContainer.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)btnClose).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlBody;
        private Panel pnlContainer;
        private Panel pnlHeader;
        private Label label4;
        private Panel panel1;
        private Panel panel2;
        private PictureBox btnClose;
        private Panel pnlLineBody;
        private Panel pnlBodyContainer;
        private Panel pnlBrowseFile;
        private Label label1;
        private Button btnProcess;
        private Button btnPackagePath;
        public TextBox txtJsonFilePath;
        private Panel panel4;
        private Panel pnlGrid;
        private Panel pnlRightBody;
        private RzLab.Clipper.ControlsLib.DarkTabControl TabDocument;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Label label2;
        private ComboBox cmbDocumentType;
        private FlowLayoutPanel flowCards;
        private Panel pnlLoader;
    }
}