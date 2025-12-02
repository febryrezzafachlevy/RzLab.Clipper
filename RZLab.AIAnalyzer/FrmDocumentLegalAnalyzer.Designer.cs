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
            pnlContainer = new Panel();
            pnlHeader = new Panel();
            label4 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            btnClose = new PictureBox();
            pnlLoader = new Panel();
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
            pnlBody.Location = new Point(0, 45);
            pnlBody.Margin = new Padding(3, 4, 3, 4);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(11, 13, 11, 13);
            pnlBody.Size = new Size(742, 651);
            pnlBody.TabIndex = 1;
            // 
            // pnlBodyContainer
            // 
            pnlBodyContainer.Controls.Add(pnlGrid);
            pnlBodyContainer.Controls.Add(panel4);
            pnlBodyContainer.Controls.Add(pnlBrowseFile);
            pnlBodyContainer.Dock = DockStyle.Fill;
            pnlBodyContainer.Location = new Point(11, 13);
            pnlBodyContainer.Name = "pnlBodyContainer";
            pnlBodyContainer.Size = new Size(720, 625);
            pnlBodyContainer.TabIndex = 0;
            // 
            // pnlGrid
            // 
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            pnlGrid.Controls.Add(flowCards);
            pnlGrid.Dock = DockStyle.Top;
            pnlGrid.Location = new Point(0, 125);
            pnlGrid.Margin = new Padding(3, 4, 3, 4);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Size = new Size(720, 500);
            pnlGrid.TabIndex = 5;
            // 
            // flowCards
            // 
            flowCards.AutoScroll = true;
            flowCards.BackColor = Color.FromArgb(33, 33, 33);
            flowCards.Dock = DockStyle.Fill;
            flowCards.FlowDirection = FlowDirection.TopDown;
            flowCards.Location = new Point(0, 0);
            flowCards.Name = "flowCards";
            flowCards.Padding = new Padding(15);
            flowCards.Size = new Size(718, 498);
            flowCards.TabIndex = 0;
            flowCards.WrapContents = false;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 109);
            panel4.Margin = new Padding(3, 4, 3, 4);
            panel4.Name = "panel4";
            panel4.Size = new Size(720, 16);
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
            pnlBrowseFile.Margin = new Padding(3, 4, 3, 4);
            pnlBrowseFile.Name = "pnlBrowseFile";
            pnlBrowseFile.Size = new Size(720, 109);
            pnlBrowseFile.TabIndex = 1;
            // 
            // cmbDocumentType
            // 
            cmbDocumentType.FormattingEnabled = true;
            cmbDocumentType.Location = new Point(147, 57);
            cmbDocumentType.Name = "cmbDocumentType";
            cmbDocumentType.Size = new Size(231, 28);
            cmbDocumentType.TabIndex = 31;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(21, 60);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(120, 25);
            label2.TabIndex = 30;
            label2.Text = "Document Type";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(21, 17);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(120, 25);
            label1.TabIndex = 29;
            label1.Text = "Document";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnProcess
            // 
            btnProcess.FlatStyle = FlatStyle.Flat;
            btnProcess.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnProcess.ForeColor = Color.White;
            btnProcess.Location = new Point(588, 54);
            btnProcess.Margin = new Padding(2, 3, 2, 3);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(114, 34);
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
            btnPackagePath.Location = new Point(662, 15);
            btnPackagePath.Margin = new Padding(2, 3, 2, 3);
            btnPackagePath.Name = "btnPackagePath";
            btnPackagePath.Size = new Size(40, 33);
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
            txtJsonFilePath.Location = new Point(147, 16);
            txtJsonFilePath.Margin = new Padding(2, 3, 2, 3);
            txtJsonFilePath.Name = "txtJsonFilePath";
            txtJsonFilePath.ReadOnly = true;
            txtJsonFilePath.Size = new Size(511, 27);
            txtJsonFilePath.TabIndex = 19;
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.White;
            pnlContainer.Controls.Add(pnlBody);
            pnlContainer.Controls.Add(pnlHeader);
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.Location = new Point(5, 5);
            pnlContainer.Margin = new Padding(3, 4, 3, 4);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(742, 696);
            pnlContainer.TabIndex = 2;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(48, 63, 159);
            pnlHeader.Controls.Add(label4);
            pnlHeader.Controls.Add(panel1);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(3, 4, 3, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(11, 0, 0, 0);
            pnlHeader.Size = new Size(742, 45);
            pnlHeader.TabIndex = 0;
            // 
            // label4
            // 
            label4.Dock = DockStyle.Left;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(11, 0);
            label4.Name = "label4";
            label4.Size = new Size(267, 45);
            label4.TabIndex = 9;
            label4.Text = "Document Legal Analyzer";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(684, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(58, 45);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnClose);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(21, 0);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(2, 3, 2, 3);
            panel2.Size = new Size(37, 45);
            panel2.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(211, 47, 47);
            btnClose.Cursor = Cursors.Hand;
            btnClose.Dock = DockStyle.Fill;
            btnClose.Image = Properties.Resources.close_100px;
            btnClose.Location = new Point(2, 3);
            btnClose.Margin = new Padding(3, 4, 3, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(33, 39);
            btnClose.SizeMode = PictureBoxSizeMode.Zoom;
            btnClose.TabIndex = 0;
            btnClose.TabStop = false;
            btnClose.Click += btnClose_Click;
            // 
            // pnlLoader
            // 
            pnlLoader.Location = new Point(11, 13);
            pnlLoader.Name = "pnlLoader";
            pnlLoader.Size = new Size(720, 625);
            pnlLoader.TabIndex = 34;
            // 
            // FrmDocumentLegalAnalyzer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(752, 706);
            Controls.Add(pnlContainer);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmDocumentLegalAnalyzer";
            Padding = new Padding(5);
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
        private Helpers.DarkTabControl TabDocument;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Label label2;
        private ComboBox cmbDocumentType;
        private FlowLayoutPanel flowCards;
        private Panel pnlLoader;
    }
}