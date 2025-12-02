namespace RZLab.AIAnalyzer
{
    partial class FrmImageRecognize
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            lblImageSize = new Label();
            lblTitleSystemDrive = new Label();
            btnProcess = new Button();
            txtJsonFilePath = new TextBox();
            pnlBrowseFile = new Panel();
            label2 = new Label();
            rbImageOptionKuitansi = new RadioButton();
            rbImageOptionNota = new RadioButton();
            rbImageOptionSIM = new RadioButton();
            rbImageOptionKTP = new RadioButton();
            btnPreviewImage = new Button();
            lblImageExtension = new Label();
            label3 = new Label();
            btnPackagePath = new Button();
            pnlContainer = new Panel();
            pnlBody = new Panel();
            pnlGrid = new Panel();
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
            pnlHeader = new Panel();
            label4 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            btnClose = new PictureBox();
            lstView = new RzLab.Clipper.ControlsLib.RzListView();
            pnlBrowseFile.SuspendLayout();
            pnlContainer.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlGrid.SuspendLayout();
            panel4.SuspendLayout();
            pnlBodyProgressExtract.SuspendLayout();
            panel5.SuspendLayout();
            pnlExtractPercent.SuspendLayout();
            pnlHeaderProgressExtract.SuspendLayout();
            pnlHeader.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnClose).BeginInit();
            SuspendLayout();
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
            label1.Text = "Image";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblImageSize
            // 
            lblImageSize.Font = new Font("Segoe UI", 9F);
            lblImageSize.ForeColor = Color.White;
            lblImageSize.Location = new Point(116, 79);
            lblImageSize.Margin = new Padding(2, 0, 2, 0);
            lblImageSize.Name = "lblImageSize";
            lblImageSize.Size = new Size(95, 19);
            lblImageSize.TabIndex = 26;
            lblImageSize.Text = "0 Mb";
            lblImageSize.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitleSystemDrive
            // 
            lblTitleSystemDrive.Font = new Font("Segoe UI", 9F);
            lblTitleSystemDrive.ForeColor = Color.White;
            lblTitleSystemDrive.Location = new Point(18, 79);
            lblTitleSystemDrive.Margin = new Padding(2, 0, 2, 0);
            lblTitleSystemDrive.Name = "lblTitleSystemDrive";
            lblTitleSystemDrive.Size = new Size(94, 19);
            lblTitleSystemDrive.TabIndex = 25;
            lblTitleSystemDrive.Text = "Image Size";
            lblTitleSystemDrive.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnProcess
            // 
            btnProcess.FlatStyle = FlatStyle.Flat;
            btnProcess.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnProcess.ForeColor = Color.White;
            btnProcess.Location = new Point(597, 92);
            btnProcess.Margin = new Padding(2);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(134, 30);
            btnProcess.TabIndex = 22;
            btnProcess.Text = "Process";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // txtJsonFilePath
            // 
            txtJsonFilePath.BackColor = Color.FromArgb(33, 33, 33);
            txtJsonFilePath.BorderStyle = BorderStyle.FixedSingle;
            txtJsonFilePath.Font = new Font("Segoe UI", 9F);
            txtJsonFilePath.ForeColor = Color.White;
            txtJsonFilePath.Location = new Point(116, 12);
            txtJsonFilePath.Margin = new Padding(2);
            txtJsonFilePath.Name = "txtJsonFilePath";
            txtJsonFilePath.ReadOnly = true;
            txtJsonFilePath.Size = new Size(569, 23);
            txtJsonFilePath.TabIndex = 19;
            // 
            // pnlBrowseFile
            // 
            pnlBrowseFile.BorderStyle = BorderStyle.FixedSingle;
            pnlBrowseFile.Controls.Add(label2);
            pnlBrowseFile.Controls.Add(rbImageOptionKuitansi);
            pnlBrowseFile.Controls.Add(rbImageOptionNota);
            pnlBrowseFile.Controls.Add(rbImageOptionSIM);
            pnlBrowseFile.Controls.Add(rbImageOptionKTP);
            pnlBrowseFile.Controls.Add(btnPreviewImage);
            pnlBrowseFile.Controls.Add(lblImageExtension);
            pnlBrowseFile.Controls.Add(label3);
            pnlBrowseFile.Controls.Add(label1);
            pnlBrowseFile.Controls.Add(lblImageSize);
            pnlBrowseFile.Controls.Add(lblTitleSystemDrive);
            pnlBrowseFile.Controls.Add(btnProcess);
            pnlBrowseFile.Controls.Add(btnPackagePath);
            pnlBrowseFile.Controls.Add(txtJsonFilePath);
            pnlBrowseFile.Dock = DockStyle.Top;
            pnlBrowseFile.Location = new Point(10, 10);
            pnlBrowseFile.Name = "pnlBrowseFile";
            pnlBrowseFile.Size = new Size(748, 133);
            pnlBrowseFile.TabIndex = 0;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(18, 51);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(94, 19);
            label2.TabIndex = 37;
            label2.Text = "Image Type";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // rbImageOptionKuitansi
            // 
            rbImageOptionKuitansi.AutoSize = true;
            rbImageOptionKuitansi.ForeColor = Color.White;
            rbImageOptionKuitansi.Location = new Point(274, 51);
            rbImageOptionKuitansi.Name = "rbImageOptionKuitansi";
            rbImageOptionKuitansi.Size = new Size(67, 19);
            rbImageOptionKuitansi.TabIndex = 36;
            rbImageOptionKuitansi.TabStop = true;
            rbImageOptionKuitansi.Text = "Kuitansi";
            rbImageOptionKuitansi.UseVisualStyleBackColor = true;
            // 
            // rbImageOptionNota
            // 
            rbImageOptionNota.AutoSize = true;
            rbImageOptionNota.ForeColor = Color.White;
            rbImageOptionNota.Location = new Point(217, 51);
            rbImageOptionNota.Name = "rbImageOptionNota";
            rbImageOptionNota.Size = new Size(51, 19);
            rbImageOptionNota.TabIndex = 35;
            rbImageOptionNota.TabStop = true;
            rbImageOptionNota.Text = "Nota";
            rbImageOptionNota.UseVisualStyleBackColor = true;
            // 
            // rbImageOptionSIM
            // 
            rbImageOptionSIM.AutoSize = true;
            rbImageOptionSIM.ForeColor = Color.White;
            rbImageOptionSIM.Location = new Point(166, 51);
            rbImageOptionSIM.Name = "rbImageOptionSIM";
            rbImageOptionSIM.Size = new Size(45, 19);
            rbImageOptionSIM.TabIndex = 34;
            rbImageOptionSIM.TabStop = true;
            rbImageOptionSIM.Text = "SIM";
            rbImageOptionSIM.UseVisualStyleBackColor = true;
            // 
            // rbImageOptionKTP
            // 
            rbImageOptionKTP.AutoSize = true;
            rbImageOptionKTP.ForeColor = Color.White;
            rbImageOptionKTP.Location = new Point(117, 51);
            rbImageOptionKTP.Name = "rbImageOptionKTP";
            rbImageOptionKTP.Size = new Size(46, 19);
            rbImageOptionKTP.TabIndex = 33;
            rbImageOptionKTP.TabStop = true;
            rbImageOptionKTP.Text = "KTP";
            rbImageOptionKTP.UseVisualStyleBackColor = true;
            // 
            // btnPreviewImage
            // 
            btnPreviewImage.FlatStyle = FlatStyle.Flat;
            btnPreviewImage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPreviewImage.ForeColor = Color.White;
            btnPreviewImage.Location = new Point(459, 92);
            btnPreviewImage.Margin = new Padding(2);
            btnPreviewImage.Name = "btnPreviewImage";
            btnPreviewImage.Size = new Size(134, 30);
            btnPreviewImage.TabIndex = 32;
            btnPreviewImage.Text = "Preview Image";
            btnPreviewImage.UseVisualStyleBackColor = true;
            btnPreviewImage.Click += btnPreviewImage_Click;
            // 
            // lblImageExtension
            // 
            lblImageExtension.Font = new Font("Segoe UI", 9F);
            lblImageExtension.ForeColor = Color.White;
            lblImageExtension.Location = new Point(116, 103);
            lblImageExtension.Margin = new Padding(2, 0, 2, 0);
            lblImageExtension.Name = "lblImageExtension";
            lblImageExtension.Size = new Size(95, 19);
            lblImageExtension.TabIndex = 31;
            lblImageExtension.Text = "png";
            lblImageExtension.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 9F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(18, 103);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(94, 19);
            label3.TabIndex = 30;
            label3.Text = "Extension";
            label3.TextAlign = ContentAlignment.MiddleRight;
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
            btnPackagePath.Click += btnPackagePath_Click;
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.White;
            pnlContainer.Controls.Add(pnlBody);
            pnlContainer.Controls.Add(pnlHeader);
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.Location = new Point(5, 5);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(768, 548);
            pnlContainer.TabIndex = 2;
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
            pnlBody.Size = new Size(768, 514);
            pnlBody.TabIndex = 1;
            // 
            // pnlGrid
            // 
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            pnlGrid.Controls.Add(lstView);
            pnlGrid.Dock = DockStyle.Top;
            pnlGrid.Location = new Point(10, 220);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Size = new Size(748, 281);
            pnlGrid.TabIndex = 4;
            // 
            // panel6
            // 
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(10, 208);
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
            panel4.Location = new Point(10, 155);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(5, 0, 5, 0);
            panel4.Size = new Size(748, 53);
            panel4.TabIndex = 2;
            // 
            // pnlBodyProgressExtract
            // 
            pnlBodyProgressExtract.Controls.Add(panel5);
            pnlBodyProgressExtract.Dock = DockStyle.Top;
            pnlBodyProgressExtract.Location = new Point(5, 28);
            pnlBodyProgressExtract.Name = "pnlBodyProgressExtract";
            pnlBodyProgressExtract.Size = new Size(736, 11);
            pnlBodyProgressExtract.TabIndex = 42;
            // 
            // panel5
            // 
            panel5.Controls.Add(pbProgress);
            panel5.Controls.Add(pnlExtractPercent);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(736, 11);
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
            pnlHeaderProgressExtract.Location = new Point(5, 0);
            pnlHeaderProgressExtract.Name = "pnlHeaderProgressExtract";
            pnlHeaderProgressExtract.Size = new Size(736, 28);
            pnlHeaderProgressExtract.TabIndex = 41;
            // 
            // lblStepProgress
            // 
            lblStepProgress.Dock = DockStyle.Right;
            lblStepProgress.Font = new Font("Segoe UI", 8F);
            lblStepProgress.ForeColor = Color.White;
            lblStepProgress.Location = new Point(646, 0);
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
            panel3.Location = new Point(10, 143);
            panel3.Name = "panel3";
            panel3.Size = new Size(748, 12);
            panel3.TabIndex = 1;
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
            label4.Text = "Image Reader";
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
            // lstView
            // 
            lstView.BackColor = Color.FromArgb(32, 32, 32);
            lstView.BorderStyle = BorderStyle.None;
            lstView.Dock = DockStyle.Fill;
            lstView.Font = new Font("Segoe UI", 9F);
            lstView.ForeColor = Color.White;
            lstView.FullRowSelect = true;
            lstView.Location = new Point(0, 0);
            lstView.Name = "lstView";
            lstView.OwnerDraw = true;
            lstView.Size = new Size(746, 279);
            lstView.TabIndex = 2;
            lstView.UseCompatibleStateImageBehavior = false;
            lstView.View = View.Details;
            // 
            // FrmImageRecognize
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(778, 558);
            Controls.Add(pnlContainer);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmImageRecognize";
            Padding = new Padding(5);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmImageRecognize";
            pnlBrowseFile.ResumeLayout(false);
            pnlBrowseFile.PerformLayout();
            pnlContainer.ResumeLayout(false);
            pnlBody.ResumeLayout(false);
            pnlGrid.ResumeLayout(false);
            panel4.ResumeLayout(false);
            pnlBodyProgressExtract.ResumeLayout(false);
            panel5.ResumeLayout(false);
            pnlExtractPercent.ResumeLayout(false);
            pnlHeaderProgressExtract.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)btnClose).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label lblImageSize;
        private Label lblTitleSystemDrive;
        private Button btnProcess;
        public TextBox txtJsonFilePath;
        private Panel pnlBrowseFile;
        private Button btnPackagePath;
        private Panel pnlContainer;
        private Panel pnlBody;
        private Panel pnlGrid;
        private Panel panel6;
        private Panel panel4;
        private Panel pnlBodyProgressExtract;
        private Panel panel5;
        private ProgressBar pbProgress;
        private Panel pnlExtractPercent;
        private Label lblProgressPercent;
        private Panel pnlHeaderProgressExtract;
        private Label lblStepProgress;
        private Label lblTotalStatus;
        private Panel panel3;
        private Panel pnlHeader;
        private Label label4;
        private Panel panel1;
        private Panel panel2;
        private PictureBox btnClose;
        private Button btnPreviewImage;
        private Label lblImageExtension;
        private Label label3;
        private RadioButton rbImageOptionKuitansi;
        private RadioButton rbImageOptionNota;
        private RadioButton rbImageOptionSIM;
        private RadioButton rbImageOptionKTP;
        private Label label2;
        private RzLab.Clipper.ControlsLib.RzListView lstView;
    }
}