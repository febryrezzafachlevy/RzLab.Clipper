namespace RZLab.AIAnalyzer
{
    partial class FrmVideoAnalyzerDetail
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
            panelProgress = new Panel();
            pnlVideo = new Panel();
            panel6 = new Panel();
            pnlBrowseFile = new Panel();
            lblStatus = new Label();
            label1 = new Label();
            lblVideoSummary = new Label();
            label6 = new Label();
            lblVideoHighlight = new Label();
            label5 = new Label();
            lblVideoScene = new Label();
            label2 = new Label();
            lblVideoTime = new Label();
            btnPlayVideo = new Button();
            label32 = new Label();
            lblVideoTitle = new Label();
            lblTitleSystemDrive = new Label();
            pnlHeader = new Panel();
            label4 = new Label();
            panel2 = new Panel();
            panel3 = new Panel();
            btnClose = new PictureBox();
            pnlContainer.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlBrowseFile.SuspendLayout();
            pnlHeader.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
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
            pnlContainer.Size = new Size(849, 546);
            pnlContainer.TabIndex = 0;
            // 
            // pnlBody
            // 
            pnlBody.BackColor = Color.FromArgb(33, 33, 33);
            pnlBody.Controls.Add(panelProgress);
            pnlBody.Controls.Add(pnlVideo);
            pnlBody.Controls.Add(panel6);
            pnlBody.Controls.Add(pnlBrowseFile);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(0, 34);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(5);
            pnlBody.Size = new Size(849, 512);
            pnlBody.TabIndex = 2;
            // 
            // panelProgress
            // 
            panelProgress.Dock = DockStyle.Top;
            panelProgress.Location = new Point(5, 478);
            panelProgress.Name = "panelProgress";
            panelProgress.Size = new Size(839, 31);
            panelProgress.TabIndex = 7;
            // 
            // pnlVideo
            // 
            pnlVideo.BorderStyle = BorderStyle.FixedSingle;
            pnlVideo.Dock = DockStyle.Top;
            pnlVideo.Location = new Point(5, 140);
            pnlVideo.Name = "pnlVideo";
            pnlVideo.Size = new Size(839, 338);
            pnlVideo.TabIndex = 6;
            // 
            // panel6
            // 
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(5, 128);
            panel6.Name = "panel6";
            panel6.Size = new Size(839, 12);
            panel6.TabIndex = 5;
            // 
            // pnlBrowseFile
            // 
            pnlBrowseFile.BackColor = Color.FromArgb(33, 33, 33);
            pnlBrowseFile.BorderStyle = BorderStyle.FixedSingle;
            pnlBrowseFile.Controls.Add(lblStatus);
            pnlBrowseFile.Controls.Add(label1);
            pnlBrowseFile.Controls.Add(lblVideoSummary);
            pnlBrowseFile.Controls.Add(label6);
            pnlBrowseFile.Controls.Add(lblVideoHighlight);
            pnlBrowseFile.Controls.Add(label5);
            pnlBrowseFile.Controls.Add(lblVideoScene);
            pnlBrowseFile.Controls.Add(label2);
            pnlBrowseFile.Controls.Add(lblVideoTime);
            pnlBrowseFile.Controls.Add(btnPlayVideo);
            pnlBrowseFile.Controls.Add(label32);
            pnlBrowseFile.Controls.Add(lblVideoTitle);
            pnlBrowseFile.Controls.Add(lblTitleSystemDrive);
            pnlBrowseFile.Dock = DockStyle.Top;
            pnlBrowseFile.Location = new Point(5, 5);
            pnlBrowseFile.Name = "pnlBrowseFile";
            pnlBrowseFile.Size = new Size(839, 123);
            pnlBrowseFile.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.ForeColor = Color.White;
            lblStatus.Location = new Point(391, 90);
            lblStatus.Margin = new Padding(2, 0, 2, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(205, 19);
            lblStatus.TabIndex = 36;
            lblStatus.Text = ": Not Ready";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(310, 90);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(77, 19);
            label1.TabIndex = 35;
            label1.Text = "Status : ";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblVideoSummary
            // 
            lblVideoSummary.Font = new Font("Segoe UI", 9F);
            lblVideoSummary.ForeColor = Color.White;
            lblVideoSummary.Location = new Point(391, 13);
            lblVideoSummary.Margin = new Padding(2, 0, 2, 0);
            lblVideoSummary.Name = "lblVideoSummary";
            lblVideoSummary.Size = new Size(434, 64);
            lblVideoSummary.TabIndex = 34;
            lblVideoSummary.Text = "Summary";
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 9F);
            label6.ForeColor = Color.White;
            label6.Location = new Point(310, 11);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(77, 19);
            label6.TabIndex = 33;
            label6.Text = "Summary : ";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblVideoHighlight
            // 
            lblVideoHighlight.Font = new Font("Segoe UI", 9F);
            lblVideoHighlight.ForeColor = Color.White;
            lblVideoHighlight.Location = new Point(101, 90);
            lblVideoHighlight.Margin = new Padding(2, 0, 2, 0);
            lblVideoHighlight.Name = "lblVideoHighlight";
            lblVideoHighlight.Size = new Size(205, 19);
            lblVideoHighlight.TabIndex = 32;
            lblVideoHighlight.Text = ": Video Highlight";
            lblVideoHighlight.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 9F);
            label5.ForeColor = Color.White;
            label5.Location = new Point(13, 90);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(79, 19);
            label5.TabIndex = 31;
            label5.Text = "Highlight";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblVideoScene
            // 
            lblVideoScene.Font = new Font("Segoe UI", 9F);
            lblVideoScene.ForeColor = Color.White;
            lblVideoScene.Location = new Point(101, 62);
            lblVideoScene.Margin = new Padding(2, 0, 2, 0);
            lblVideoScene.Name = "lblVideoScene";
            lblVideoScene.Size = new Size(205, 19);
            lblVideoScene.TabIndex = 30;
            lblVideoScene.Text = ": 12:00";
            lblVideoScene.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(13, 62);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(79, 19);
            label2.TabIndex = 29;
            label2.Text = "Scene";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblVideoTime
            // 
            lblVideoTime.Font = new Font("Segoe UI", 9F);
            lblVideoTime.ForeColor = Color.White;
            lblVideoTime.Location = new Point(101, 34);
            lblVideoTime.Margin = new Padding(2, 0, 2, 0);
            lblVideoTime.Name = "lblVideoTime";
            lblVideoTime.Size = new Size(205, 19);
            lblVideoTime.TabIndex = 28;
            lblVideoTime.Text = ": 12:00";
            lblVideoTime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnPlayVideo
            // 
            btnPlayVideo.FlatAppearance.MouseDownBackColor = Color.FromArgb(33, 33, 33);
            btnPlayVideo.FlatAppearance.MouseOverBackColor = Color.FromArgb(33, 33, 33);
            btnPlayVideo.FlatStyle = FlatStyle.Flat;
            btnPlayVideo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPlayVideo.ForeColor = Color.White;
            btnPlayVideo.Location = new Point(719, 83);
            btnPlayVideo.Margin = new Padding(2);
            btnPlayVideo.Name = "btnPlayVideo";
            btnPlayVideo.Size = new Size(106, 30);
            btnPlayVideo.TabIndex = 22;
            btnPlayVideo.Text = "Play Video";
            btnPlayVideo.UseVisualStyleBackColor = true;
            btnPlayVideo.Click += btnPlayVideo_Click;
            // 
            // label32
            // 
            label32.Font = new Font("Segoe UI", 9F);
            label32.ForeColor = Color.White;
            label32.Location = new Point(13, 34);
            label32.Margin = new Padding(2, 0, 2, 0);
            label32.Name = "label32";
            label32.Size = new Size(79, 19);
            label32.TabIndex = 27;
            label32.Text = "Timestamp";
            label32.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblVideoTitle
            // 
            lblVideoTitle.Font = new Font("Segoe UI", 9F);
            lblVideoTitle.ForeColor = Color.White;
            lblVideoTitle.Location = new Point(101, 11);
            lblVideoTitle.Margin = new Padding(2, 0, 2, 0);
            lblVideoTitle.Name = "lblVideoTitle";
            lblVideoTitle.Size = new Size(205, 19);
            lblVideoTitle.TabIndex = 26;
            lblVideoTitle.Text = ": Video Title";
            lblVideoTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitleSystemDrive
            // 
            lblTitleSystemDrive.Font = new Font("Segoe UI", 9F);
            lblTitleSystemDrive.ForeColor = Color.White;
            lblTitleSystemDrive.Location = new Point(13, 11);
            lblTitleSystemDrive.Margin = new Padding(2, 0, 2, 0);
            lblTitleSystemDrive.Name = "lblTitleSystemDrive";
            lblTitleSystemDrive.Size = new Size(79, 19);
            lblTitleSystemDrive.TabIndex = 25;
            lblTitleSystemDrive.Text = "Title";
            lblTitleSystemDrive.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(48, 63, 159);
            pnlHeader.Controls.Add(label4);
            pnlHeader.Controls.Add(panel2);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(10, 0, 0, 0);
            pnlHeader.Size = new Size(849, 34);
            pnlHeader.TabIndex = 1;
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
            label4.Text = "Video Analyzer Summary";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(798, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(51, 34);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnClose);
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(19, 0);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(2);
            panel3.Size = new Size(32, 34);
            panel3.TabIndex = 1;
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
            // FrmVideoAnalyzerDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(859, 556);
            Controls.Add(pnlContainer);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmVideoAnalyzerDetail";
            Padding = new Padding(5);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmVideoAnalyzerDetail";
            pnlContainer.ResumeLayout(false);
            pnlBody.ResumeLayout(false);
            pnlBrowseFile.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)btnClose).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlContainer;
        private Panel pnlHeader;
        private Label label4;
        private Panel panel2;
        private Panel panel3;
        private PictureBox btnClose;
        private Panel pnlBody;
        private Panel pnlBrowseFile;
        private Label lblVideoTime;
        private Label label32;
        private Label lblVideoTitle;
        private Label lblTitleSystemDrive;
        private Button btnPlayVideo;
        private Label lblVideoHighlight;
        private Label label5;
        private Label lblVideoScene;
        private Label label2;
        private Label lblVideoSummary;
        private Label label6;
        private Panel pnlVideo;
        private Panel panel6;
        private Panel panelProgress;
        private Label label1;
        private Label lblStatus;
    }
}