namespace RZLab.AIAnalyzer
{
    partial class FrmVideoAnalyzer
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
            pnlGrid = new Panel();
            lstView = new ListView();
            panel6 = new Panel();
            panel4 = new Panel();
            pnlBodyProgressExtract = new Panel();
            panel5 = new Panel();
            pbProgress = new ProgressBar();
            pnlExtractPercent = new Panel();
            lblProgressPercent = new Label();
            pnlHeaderProgressExtract = new Panel();
            lblStepProgress = new Label();
            lblTotalStatus = new Label();
            panel3 = new Panel();
            pnlBrowseFile = new Panel();
            label1 = new Label();
            lblVideoLength = new Label();
            label32 = new Label();
            lblVideoSize = new Label();
            lblTitleSystemDrive = new Label();
            btnProcess = new Button();
            btnPackagePath = new Button();
            txtVideoFilePath = new TextBox();
            pnlHeader = new Panel();
            label4 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            btnClose = new PictureBox();
            pnlContainer.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlGrid.SuspendLayout();
            panel4.SuspendLayout();
            pnlBodyProgressExtract.SuspendLayout();
            panel5.SuspendLayout();
            pnlExtractPercent.SuspendLayout();
            pnlHeaderProgressExtract.SuspendLayout();
            pnlBrowseFile.SuspendLayout();
            pnlHeader.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnClose).BeginInit();
            SuspendLayout();
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.White;
            pnlContainer.Controls.Add(pnlBody);
            pnlContainer.Controls.Add(pnlHeader);
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.Location = new Point(5, 5);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(768, 603);
            pnlContainer.TabIndex = 0;
            // 
            // pnlBody
            // 
            pnlBody.BackColor = Color.FromArgb(33, 33, 33);
            pnlBody.Controls.Add(pnlGrid);
            pnlBody.Controls.Add(panel6);
            pnlBody.Controls.Add(panel4);
            pnlBody.Controls.Add(panel3);
            pnlBody.Controls.Add(pnlBrowseFile);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(0, 34);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(10);
            pnlBody.Size = new Size(768, 569);
            pnlBody.TabIndex = 1;
            // 
            // pnlGrid
            // 
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            pnlGrid.Controls.Add(lstView);
            pnlGrid.Dock = DockStyle.Top;
            pnlGrid.Location = new Point(10, 185);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Size = new Size(748, 375);
            pnlGrid.TabIndex = 4;
            // 
            // lstView
            // 
            lstView.Dock = DockStyle.Fill;
            lstView.Location = new Point(0, 0);
            lstView.Name = "lstView";
            lstView.Size = new Size(746, 373);
            lstView.TabIndex = 0;
            lstView.UseCompatibleStateImageBehavior = false;
            lstView.DoubleClick += lstView_DoubleClick;
            // 
            // panel6
            // 
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(10, 173);
            panel6.Name = "panel6";
            panel6.Size = new Size(748, 12);
            panel6.TabIndex = 3;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(pnlBodyProgressExtract);
            panel4.Controls.Add(pnlHeaderProgressExtract);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(10, 120);
            panel4.Name = "panel4";
            panel4.Size = new Size(748, 53);
            panel4.TabIndex = 2;
            // 
            // pnlBodyProgressExtract
            // 
            pnlBodyProgressExtract.Controls.Add(panel5);
            pnlBodyProgressExtract.Dock = DockStyle.Top;
            pnlBodyProgressExtract.Location = new Point(0, 28);
            pnlBodyProgressExtract.Name = "pnlBodyProgressExtract";
            pnlBodyProgressExtract.Size = new Size(746, 11);
            pnlBodyProgressExtract.TabIndex = 42;
            // 
            // panel5
            // 
            panel5.Controls.Add(pbProgress);
            panel5.Controls.Add(pnlExtractPercent);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(746, 11);
            panel5.TabIndex = 1;
            // 
            // pbProgress
            // 
            pbProgress.Location = new Point(48, 0);
            pbProgress.Name = "pbProgress";
            pbProgress.Size = new Size(695, 11);
            pbProgress.TabIndex = 0;
            // 
            // pnlExtractPercent
            // 
            pnlExtractPercent.Controls.Add(lblProgressPercent);
            pnlExtractPercent.Dock = DockStyle.Left;
            pnlExtractPercent.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            pnlExtractPercent.Location = new Point(0, 0);
            pnlExtractPercent.Name = "pnlExtractPercent";
            pnlExtractPercent.Size = new Size(48, 11);
            pnlExtractPercent.TabIndex = 40;
            // 
            // lblProgressPercent
            // 
            lblProgressPercent.Dock = DockStyle.Fill;
            lblProgressPercent.Font = new Font("Segoe UI", 8F);
            lblProgressPercent.ForeColor = Color.White;
            lblProgressPercent.Location = new Point(0, 0);
            lblProgressPercent.Margin = new Padding(2, 0, 2, 0);
            lblProgressPercent.Name = "lblProgressPercent";
            lblProgressPercent.Size = new Size(48, 11);
            lblProgressPercent.TabIndex = 35;
            lblProgressPercent.Text = "100 %";
            lblProgressPercent.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlHeaderProgressExtract
            // 
            pnlHeaderProgressExtract.Controls.Add(lblStepProgress);
            pnlHeaderProgressExtract.Controls.Add(lblTotalStatus);
            pnlHeaderProgressExtract.Dock = DockStyle.Top;
            pnlHeaderProgressExtract.Location = new Point(0, 0);
            pnlHeaderProgressExtract.Name = "pnlHeaderProgressExtract";
            pnlHeaderProgressExtract.Size = new Size(746, 28);
            pnlHeaderProgressExtract.TabIndex = 41;
            // 
            // lblStepProgress
            // 
            lblStepProgress.Dock = DockStyle.Right;
            lblStepProgress.Font = new Font("Segoe UI", 8F);
            lblStepProgress.ForeColor = Color.White;
            lblStepProgress.Location = new Point(656, 0);
            lblStepProgress.Margin = new Padding(2, 0, 2, 0);
            lblStepProgress.Name = "lblStepProgress";
            lblStepProgress.Size = new Size(90, 28);
            lblStepProgress.TabIndex = 37;
            lblStepProgress.Text = "1 Of 100";
            lblStepProgress.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTotalStatus
            // 
            lblTotalStatus.Dock = DockStyle.Left;
            lblTotalStatus.Font = new Font("Segoe UI", 8F);
            lblTotalStatus.ForeColor = Color.White;
            lblTotalStatus.Location = new Point(0, 0);
            lblTotalStatus.Margin = new Padding(2, 0, 2, 0);
            lblTotalStatus.Name = "lblTotalStatus";
            lblTotalStatus.Size = new Size(615, 28);
            lblTotalStatus.TabIndex = 36;
            lblTotalStatus.Text = "Progress";
            lblTotalStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(10, 108);
            panel3.Name = "panel3";
            panel3.Size = new Size(748, 12);
            panel3.TabIndex = 1;
            // 
            // pnlBrowseFile
            // 
            pnlBrowseFile.BorderStyle = BorderStyle.FixedSingle;
            pnlBrowseFile.Controls.Add(label1);
            pnlBrowseFile.Controls.Add(lblVideoLength);
            pnlBrowseFile.Controls.Add(label32);
            pnlBrowseFile.Controls.Add(lblVideoSize);
            pnlBrowseFile.Controls.Add(lblTitleSystemDrive);
            pnlBrowseFile.Controls.Add(btnProcess);
            pnlBrowseFile.Controls.Add(btnPackagePath);
            pnlBrowseFile.Controls.Add(txtVideoFilePath);
            pnlBrowseFile.Dock = DockStyle.Top;
            pnlBrowseFile.Location = new Point(10, 10);
            pnlBrowseFile.Name = "pnlBrowseFile";
            pnlBrowseFile.Size = new Size(748, 98);
            pnlBrowseFile.TabIndex = 0;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(18, 13);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(94, 19);
            label1.TabIndex = 29;
            label1.Text = "Video File";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblVideoLength
            // 
            lblVideoLength.Font = new Font("Segoe UI", 9F);
            lblVideoLength.ForeColor = Color.White;
            lblVideoLength.Location = new Point(116, 66);
            lblVideoLength.Margin = new Padding(2, 0, 2, 0);
            lblVideoLength.Name = "lblVideoLength";
            lblVideoLength.Size = new Size(95, 19);
            lblVideoLength.TabIndex = 28;
            lblVideoLength.Text = "12:00";
            lblVideoLength.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            label32.Font = new Font("Segoe UI", 9F);
            label32.ForeColor = Color.White;
            label32.Location = new Point(18, 66);
            label32.Margin = new Padding(2, 0, 2, 0);
            label32.Name = "label32";
            label32.Size = new Size(94, 19);
            label32.TabIndex = 27;
            label32.Text = "Video Length";
            label32.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblVideoSize
            // 
            lblVideoSize.Font = new Font("Segoe UI", 9F);
            lblVideoSize.ForeColor = Color.White;
            lblVideoSize.Location = new Point(116, 43);
            lblVideoSize.Margin = new Padding(2, 0, 2, 0);
            lblVideoSize.Name = "lblVideoSize";
            lblVideoSize.Size = new Size(95, 19);
            lblVideoSize.TabIndex = 26;
            lblVideoSize.Text = "0 Mb";
            lblVideoSize.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitleSystemDrive
            // 
            lblTitleSystemDrive.Font = new Font("Segoe UI", 9F);
            lblTitleSystemDrive.ForeColor = Color.White;
            lblTitleSystemDrive.Location = new Point(18, 43);
            lblTitleSystemDrive.Margin = new Padding(2, 0, 2, 0);
            lblTitleSystemDrive.Name = "lblTitleSystemDrive";
            lblTitleSystemDrive.Size = new Size(94, 19);
            lblTitleSystemDrive.TabIndex = 25;
            lblTitleSystemDrive.Text = "Video Size";
            lblTitleSystemDrive.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnProcess
            // 
            btnProcess.FlatStyle = FlatStyle.Flat;
            btnProcess.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnProcess.ForeColor = Color.White;
            btnProcess.Location = new Point(647, 51);
            btnProcess.Margin = new Padding(2);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(84, 30);
            btnProcess.TabIndex = 22;
            btnProcess.Text = "Process";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // btnPackagePath
            // 
            btnPackagePath.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnPackagePath.FlatStyle = FlatStyle.Flat;
            btnPackagePath.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPackagePath.ForeColor = Color.White;
            btnPackagePath.Location = new Point(696, 11);
            btnPackagePath.Margin = new Padding(2);
            btnPackagePath.Name = "btnPackagePath";
            btnPackagePath.Size = new Size(35, 25);
            btnPackagePath.TabIndex = 20;
            btnPackagePath.Text = "...";
            btnPackagePath.UseVisualStyleBackColor = true;
            btnPackagePath.Click += btnSelect_Click;
            // 
            // txtVideoFilePath
            // 
            txtVideoFilePath.BackColor = Color.FromArgb(33, 33, 33);
            txtVideoFilePath.BorderStyle = BorderStyle.FixedSingle;
            txtVideoFilePath.Font = new Font("Segoe UI", 9F);
            txtVideoFilePath.ForeColor = Color.White;
            txtVideoFilePath.Location = new Point(116, 12);
            txtVideoFilePath.Margin = new Padding(2);
            txtVideoFilePath.Name = "txtVideoFilePath";
            txtVideoFilePath.ReadOnly = true;
            txtVideoFilePath.Size = new Size(569, 23);
            txtVideoFilePath.TabIndex = 19;
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
            pnlHeader.Size = new Size(768, 34);
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
            label4.Text = "Video Analyzer";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(717, 0);
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
            // FrmVideoAnalyzer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(778, 613);
            Controls.Add(pnlContainer);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmVideoAnalyzer";
            Padding = new Padding(5);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Video Analyzer";
            Load += FrmVideoAnalyzer_Load;
            pnlContainer.ResumeLayout(false);
            pnlBody.ResumeLayout(false);
            pnlGrid.ResumeLayout(false);
            panel4.ResumeLayout(false);
            pnlBodyProgressExtract.ResumeLayout(false);
            panel5.ResumeLayout(false);
            pnlExtractPercent.ResumeLayout(false);
            pnlHeaderProgressExtract.ResumeLayout(false);
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
        private Panel pnlBody;
        private Panel pnlHeader;
        private Label label4;
        private Panel panel1;
        private Panel panel2;
        private PictureBox btnClose;
        private Panel pnlBrowseFile;
        private Button btnPackagePath;
        public TextBox txtVideoFilePath;
        private Label label1;
        private Label lblVideoLength;
        private Label label32;
        private Label lblVideoSize;
        private Label lblTitleSystemDrive;
        private Button btnProcess;
        private Panel panel3;
        private Panel panel4;
        private Panel pnlHeaderProgressExtract;
        private Label lblStepProgress;
        private Label lblTotalStatus;
        private Panel pnlBodyProgressExtract;
        private Panel panel5;
        private ProgressBar pbProgress;
        private Panel pnlExtractPercent;
        private Label lblProgressPercent;
        private Panel pnlGrid;
        private Panel panel6;
        public ListView lstView;
    }
}