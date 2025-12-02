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
            darkTabControl1 = new RZLab.AIAnalyzer.Helpers.DarkTabControl();
            tabPage1 = new TabPage();
            webView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            tabPage2 = new TabPage();
            label4 = new Label();
            pnlHeader = new Panel();
            pnlContainer = new Panel();
            pnlBody = new Panel();
            pnlLoader = new Panel();
            pnlBodyContainer = new Panel();
            panel4 = new Panel();
            pnlBrowseFile = new Panel();
            lblDocumentType = new Label();
            lblDocumentName = new Label();
            label2 = new Label();
            label1 = new Label();
            btnRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)btnClose).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            pnlGrid.SuspendLayout();
            darkTabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView2).BeginInit();
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
            btnClose.Location = new Point(2, 3);
            btnClose.Margin = new Padding(3, 4, 3, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(33, 39);
            btnClose.SizeMode = PictureBoxSizeMode.Zoom;
            btnClose.TabIndex = 0;
            btnClose.TabStop = false;
            btnClose.Click += btnClose_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1204, 0);
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
            // pnlGrid
            // 
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            pnlGrid.Controls.Add(darkTabControl1);
            pnlGrid.Dock = DockStyle.Top;
            pnlGrid.Location = new Point(0, 125);
            pnlGrid.Margin = new Padding(3, 4, 3, 4);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Size = new Size(1240, 596);
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
            darkTabControl1.Size = new Size(1238, 594);
            darkTabControl1.SizeMode = TabSizeMode.Fixed;
            darkTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(33, 33, 33);
            tabPage1.Controls.Add(webView2);
            tabPage1.Location = new Point(4, 44);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1230, 546);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Main Document";
            // 
            // webView2
            // 
            webView2.AllowExternalDrop = true;
            webView2.CreationProperties = null;
            webView2.DefaultBackgroundColor = Color.FromArgb(33, 33, 33);
            webView2.Dock = DockStyle.Fill;
            webView2.Location = new Point(3, 3);
            webView2.Name = "webView2";
            webView2.Size = new Size(1224, 540);
            webView2.TabIndex = 0;
            webView2.ZoomFactor = 1D;
            webView2.NavigationCompleted += WebView2_NavigationCompleted;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(33, 33, 33);
            tabPage2.Location = new Point(4, 44);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1230, 546);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "AI Summary";
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
            pnlHeader.Size = new Size(1262, 45);
            pnlHeader.TabIndex = 0;
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
            pnlContainer.Size = new Size(1262, 792);
            pnlContainer.TabIndex = 3;
            // 
            // pnlBody
            // 
            pnlBody.BackColor = Color.FromArgb(33, 33, 33);
            pnlBody.Controls.Add(pnlLoader);
            pnlBody.Controls.Add(pnlBodyContainer);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(0, 45);
            pnlBody.Margin = new Padding(3, 4, 3, 4);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(11, 13, 11, 13);
            pnlBody.Size = new Size(1262, 747);
            pnlBody.TabIndex = 1;
            // 
            // pnlLoader
            // 
            pnlLoader.Location = new Point(11, 138);
            pnlLoader.Name = "pnlLoader";
            pnlLoader.Size = new Size(1240, 596);
            pnlLoader.TabIndex = 33;
            // 
            // pnlBodyContainer
            // 
            pnlBodyContainer.Controls.Add(pnlGrid);
            pnlBodyContainer.Controls.Add(panel4);
            pnlBodyContainer.Controls.Add(pnlBrowseFile);
            pnlBodyContainer.Dock = DockStyle.Fill;
            pnlBodyContainer.Location = new Point(11, 13);
            pnlBodyContainer.Name = "pnlBodyContainer";
            pnlBodyContainer.Size = new Size(1240, 721);
            pnlBodyContainer.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 109);
            panel4.Margin = new Padding(3, 4, 3, 4);
            panel4.Name = "panel4";
            panel4.Size = new Size(1240, 16);
            panel4.TabIndex = 2;
            // 
            // pnlBrowseFile
            // 
            pnlBrowseFile.BorderStyle = BorderStyle.FixedSingle;
            pnlBrowseFile.Controls.Add(btnRefresh);
            pnlBrowseFile.Controls.Add(lblDocumentType);
            pnlBrowseFile.Controls.Add(lblDocumentName);
            pnlBrowseFile.Controls.Add(label2);
            pnlBrowseFile.Controls.Add(label1);
            pnlBrowseFile.Dock = DockStyle.Top;
            pnlBrowseFile.Location = new Point(0, 0);
            pnlBrowseFile.Margin = new Padding(3, 4, 3, 4);
            pnlBrowseFile.Name = "pnlBrowseFile";
            pnlBrowseFile.Size = new Size(1240, 109);
            pnlBrowseFile.TabIndex = 1;
            // 
            // lblDocumentType
            // 
            lblDocumentType.Font = new Font("Segoe UI", 9F);
            lblDocumentType.ForeColor = Color.White;
            lblDocumentType.Location = new Point(165, 60);
            lblDocumentType.Margin = new Padding(2, 0, 2, 0);
            lblDocumentType.Name = "lblDocumentType";
            lblDocumentType.Size = new Size(242, 25);
            lblDocumentType.TabIndex = 32;
            lblDocumentType.Text = "Document";
            lblDocumentType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDocumentName
            // 
            lblDocumentName.Font = new Font("Segoe UI", 9F);
            lblDocumentName.ForeColor = Color.White;
            lblDocumentName.Location = new Point(165, 17);
            lblDocumentName.Margin = new Padding(2, 0, 2, 0);
            lblDocumentName.Name = "lblDocumentName";
            lblDocumentName.Size = new Size(411, 25);
            lblDocumentName.TabIndex = 31;
            lblDocumentName.Text = "Document";
            lblDocumentName.TextAlign = ContentAlignment.MiddleLeft;
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
            // btnRefresh
            // 
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(1117, 67);
            btnRefresh.Margin = new Padding(2, 3, 2, 3);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(114, 34);
            btnRefresh.TabIndex = 34;
            btnRefresh.Text = "Reload";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // FrmDocumentLegalDetail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1272, 802);
            Controls.Add(pnlContainer);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmDocumentLegalDetail";
            Padding = new Padding(5);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmDocumentLegalDetail";
            ((System.ComponentModel.ISupportInitialize)btnClose).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            pnlGrid.ResumeLayout(false);
            darkTabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView2).EndInit();
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
        private Helpers.DarkTabControl darkTabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2;
        private Panel pnlLoader;
        private Button btnRefresh;
    }
}