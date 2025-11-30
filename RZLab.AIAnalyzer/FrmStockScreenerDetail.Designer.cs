namespace RZLab.AIAnalyzer
{
    partial class FrmStockScreenerDetail
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
            panel2 = new Panel();
            panel3 = new Panel();
            pnlHeader = new Panel();
            panel6 = new Panel();
            lblCurrentValue = new Label();
            label32 = new Label();
            lblEmiten = new Label();
            lblTitleSystemDrive = new Label();
            pnlVideo = new Panel();
            rtbDescription = new RichTextBox();
            pnlBody = new Panel();
            pnlBrowseFile = new Panel();
            lblRR = new Label();
            label6 = new Label();
            lblRisk = new Label();
            lblRiskTitle = new Label();
            lblGrowth = new Label();
            label8 = new Label();
            lblMOAT = new Label();
            lblMOATTitle = new Label();
            lblScore = new Label();
            label15 = new Label();
            lblConfident = new Label();
            label12 = new Label();
            lblStopLoss = new Label();
            label10 = new Label();
            lblTakeProfit3 = new Label();
            label3 = new Label();
            lblTakeProfit2 = new Label();
            label7 = new Label();
            lblTakeProfit1 = new Label();
            label9 = new Label();
            lblEntry = new Label();
            label2 = new Label();
            pnlContainer = new Panel();
            ((System.ComponentModel.ISupportInitialize)btnClose).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlVideo.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlBrowseFile.SuspendLayout();
            pnlContainer.SuspendLayout();
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
            label4.Text = "Stock Screener Summary";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(680, 0);
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
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(48, 63, 159);
            pnlHeader.Controls.Add(label4);
            pnlHeader.Controls.Add(panel2);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(10, 0, 0, 0);
            pnlHeader.Size = new Size(731, 34);
            pnlHeader.TabIndex = 1;
            // 
            // panel6
            // 
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(5, 230);
            panel6.Name = "panel6";
            panel6.Size = new Size(721, 12);
            panel6.TabIndex = 5;
            // 
            // lblCurrentValue
            // 
            lblCurrentValue.Font = new Font("Segoe UI", 9F);
            lblCurrentValue.ForeColor = Color.White;
            lblCurrentValue.Location = new Point(130, 39);
            lblCurrentValue.Margin = new Padding(2, 0, 2, 0);
            lblCurrentValue.Name = "lblCurrentValue";
            lblCurrentValue.Size = new Size(124, 19);
            lblCurrentValue.TabIndex = 28;
            lblCurrentValue.Text = "0";
            lblCurrentValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            label32.Font = new Font("Segoe UI", 9F);
            label32.ForeColor = Color.White;
            label32.Location = new Point(33, 39);
            label32.Margin = new Padding(2, 0, 2, 0);
            label32.Name = "label32";
            label32.Size = new Size(88, 19);
            label32.TabIndex = 27;
            label32.Text = "Current Value : ";
            label32.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblEmiten
            // 
            lblEmiten.Font = new Font("Segoe UI", 9F);
            lblEmiten.ForeColor = Color.White;
            lblEmiten.Location = new Point(130, 11);
            lblEmiten.Margin = new Padding(2, 0, 2, 0);
            lblEmiten.Name = "lblEmiten";
            lblEmiten.Size = new Size(124, 19);
            lblEmiten.TabIndex = 26;
            lblEmiten.Text = "ADRO";
            lblEmiten.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitleSystemDrive
            // 
            lblTitleSystemDrive.Font = new Font("Segoe UI", 9F);
            lblTitleSystemDrive.ForeColor = Color.White;
            lblTitleSystemDrive.Location = new Point(33, 11);
            lblTitleSystemDrive.Margin = new Padding(2, 0, 2, 0);
            lblTitleSystemDrive.Name = "lblTitleSystemDrive";
            lblTitleSystemDrive.Size = new Size(88, 19);
            lblTitleSystemDrive.TabIndex = 25;
            lblTitleSystemDrive.Text = "Emiten : ";
            lblTitleSystemDrive.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlVideo
            // 
            pnlVideo.BorderStyle = BorderStyle.FixedSingle;
            pnlVideo.Controls.Add(rtbDescription);
            pnlVideo.Dock = DockStyle.Top;
            pnlVideo.Location = new Point(5, 242);
            pnlVideo.Name = "pnlVideo";
            pnlVideo.Size = new Size(721, 272);
            pnlVideo.TabIndex = 6;
            // 
            // rtbDescription
            // 
            rtbDescription.BackColor = Color.FromArgb(33, 33, 33);
            rtbDescription.BorderStyle = BorderStyle.FixedSingle;
            rtbDescription.Dock = DockStyle.Fill;
            rtbDescription.ForeColor = Color.White;
            rtbDescription.Location = new Point(0, 0);
            rtbDescription.Name = "rtbDescription";
            rtbDescription.ReadOnly = true;
            rtbDescription.Size = new Size(719, 270);
            rtbDescription.TabIndex = 0;
            rtbDescription.Text = "";
            // 
            // pnlBody
            // 
            pnlBody.BackColor = Color.FromArgb(33, 33, 33);
            pnlBody.Controls.Add(pnlVideo);
            pnlBody.Controls.Add(panel6);
            pnlBody.Controls.Add(pnlBrowseFile);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(0, 34);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(5);
            pnlBody.Size = new Size(731, 522);
            pnlBody.TabIndex = 2;
            // 
            // pnlBrowseFile
            // 
            pnlBrowseFile.BackColor = Color.FromArgb(33, 33, 33);
            pnlBrowseFile.BorderStyle = BorderStyle.FixedSingle;
            pnlBrowseFile.Controls.Add(lblRR);
            pnlBrowseFile.Controls.Add(label6);
            pnlBrowseFile.Controls.Add(lblRisk);
            pnlBrowseFile.Controls.Add(lblRiskTitle);
            pnlBrowseFile.Controls.Add(lblGrowth);
            pnlBrowseFile.Controls.Add(label8);
            pnlBrowseFile.Controls.Add(lblMOAT);
            pnlBrowseFile.Controls.Add(lblMOATTitle);
            pnlBrowseFile.Controls.Add(lblScore);
            pnlBrowseFile.Controls.Add(label15);
            pnlBrowseFile.Controls.Add(lblConfident);
            pnlBrowseFile.Controls.Add(label12);
            pnlBrowseFile.Controls.Add(lblStopLoss);
            pnlBrowseFile.Controls.Add(label10);
            pnlBrowseFile.Controls.Add(lblTakeProfit3);
            pnlBrowseFile.Controls.Add(label3);
            pnlBrowseFile.Controls.Add(lblTakeProfit2);
            pnlBrowseFile.Controls.Add(label7);
            pnlBrowseFile.Controls.Add(lblTakeProfit1);
            pnlBrowseFile.Controls.Add(label9);
            pnlBrowseFile.Controls.Add(lblEntry);
            pnlBrowseFile.Controls.Add(label2);
            pnlBrowseFile.Controls.Add(lblCurrentValue);
            pnlBrowseFile.Controls.Add(label32);
            pnlBrowseFile.Controls.Add(lblEmiten);
            pnlBrowseFile.Controls.Add(lblTitleSystemDrive);
            pnlBrowseFile.Dock = DockStyle.Top;
            pnlBrowseFile.Location = new Point(5, 5);
            pnlBrowseFile.Name = "pnlBrowseFile";
            pnlBrowseFile.Size = new Size(721, 225);
            pnlBrowseFile.TabIndex = 1;
            // 
            // lblRR
            // 
            lblRR.Font = new Font("Segoe UI", 9F);
            lblRR.ForeColor = Color.White;
            lblRR.Location = new Point(445, 162);
            lblRR.Margin = new Padding(2, 0, 2, 0);
            lblRR.Name = "lblRR";
            lblRR.Size = new Size(184, 19);
            lblRR.TabIndex = 52;
            lblRR.Text = "0";
            lblRR.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 9F);
            label6.ForeColor = Color.White;
            label6.Location = new Point(357, 162);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(79, 19);
            label6.TabIndex = 51;
            label6.Text = "Risk–Reward : ";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblRisk
            // 
            lblRisk.Font = new Font("Segoe UI", 9F);
            lblRisk.ForeColor = Color.White;
            lblRisk.Location = new Point(445, 85);
            lblRisk.Margin = new Padding(2, 0, 2, 0);
            lblRisk.Name = "lblRisk";
            lblRisk.Size = new Size(184, 19);
            lblRisk.TabIndex = 50;
            lblRisk.Text = "0";
            lblRisk.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblRiskTitle
            // 
            lblRiskTitle.Font = new Font("Segoe UI", 9F);
            lblRiskTitle.ForeColor = Color.White;
            lblRiskTitle.Location = new Point(357, 85);
            lblRiskTitle.Margin = new Padding(2, 0, 2, 0);
            lblRiskTitle.Name = "lblRiskTitle";
            lblRiskTitle.Size = new Size(79, 19);
            lblRiskTitle.TabIndex = 49;
            lblRiskTitle.Text = "Risk : ";
            lblRiskTitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblGrowth
            // 
            lblGrowth.Font = new Font("Segoe UI", 9F);
            lblGrowth.ForeColor = Color.White;
            lblGrowth.Location = new Point(445, 62);
            lblGrowth.Margin = new Padding(2, 0, 2, 0);
            lblGrowth.Name = "lblGrowth";
            lblGrowth.Size = new Size(184, 19);
            lblGrowth.TabIndex = 48;
            lblGrowth.Text = "0";
            lblGrowth.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            label8.Font = new Font("Segoe UI", 9F);
            label8.ForeColor = Color.White;
            label8.Location = new Point(357, 62);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(79, 19);
            label8.TabIndex = 47;
            label8.Text = "Growth : ";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblMOAT
            // 
            lblMOAT.Font = new Font("Segoe UI", 9F);
            lblMOAT.ForeColor = Color.White;
            lblMOAT.Location = new Point(445, 39);
            lblMOAT.Margin = new Padding(2, 0, 2, 0);
            lblMOAT.Name = "lblMOAT";
            lblMOAT.Size = new Size(184, 19);
            lblMOAT.TabIndex = 46;
            lblMOAT.Text = "0";
            lblMOAT.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMOATTitle
            // 
            lblMOATTitle.Font = new Font("Segoe UI", 9F);
            lblMOATTitle.ForeColor = Color.White;
            lblMOATTitle.Location = new Point(357, 39);
            lblMOATTitle.Margin = new Padding(2, 0, 2, 0);
            lblMOATTitle.Name = "lblMOATTitle";
            lblMOATTitle.Size = new Size(79, 19);
            lblMOATTitle.TabIndex = 45;
            lblMOATTitle.Text = "MOAT : ";
            lblMOATTitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblScore
            // 
            lblScore.Font = new Font("Segoe UI", 9F);
            lblScore.ForeColor = Color.White;
            lblScore.Location = new Point(445, 11);
            lblScore.Margin = new Padding(2, 0, 2, 0);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(184, 19);
            lblScore.TabIndex = 44;
            lblScore.Text = "0";
            lblScore.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            label15.Font = new Font("Segoe UI", 9F);
            label15.ForeColor = Color.White;
            label15.Location = new Point(357, 11);
            label15.Margin = new Padding(2, 0, 2, 0);
            label15.Name = "label15";
            label15.Size = new Size(79, 19);
            label15.TabIndex = 43;
            label15.Text = "Score : ";
            label15.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblConfident
            // 
            lblConfident.Font = new Font("Segoe UI", 9F);
            lblConfident.ForeColor = Color.White;
            lblConfident.Location = new Point(445, 140);
            lblConfident.Margin = new Padding(2, 0, 2, 0);
            lblConfident.Name = "lblConfident";
            lblConfident.Size = new Size(102, 19);
            lblConfident.TabIndex = 42;
            lblConfident.Text = "0";
            lblConfident.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            label12.Font = new Font("Segoe UI", 9F);
            label12.ForeColor = Color.White;
            label12.Location = new Point(357, 140);
            label12.Margin = new Padding(2, 0, 2, 0);
            label12.Name = "label12";
            label12.Size = new Size(79, 19);
            label12.TabIndex = 41;
            label12.Text = "Confident : ";
            label12.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStopLoss
            // 
            lblStopLoss.Font = new Font("Segoe UI", 9F);
            lblStopLoss.ForeColor = Color.White;
            lblStopLoss.Location = new Point(130, 85);
            lblStopLoss.Margin = new Padding(2, 0, 2, 0);
            lblStopLoss.Name = "lblStopLoss";
            lblStopLoss.Size = new Size(124, 19);
            lblStopLoss.TabIndex = 40;
            lblStopLoss.Text = "0";
            lblStopLoss.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            label10.Font = new Font("Segoe UI", 9F);
            label10.ForeColor = Color.White;
            label10.Location = new Point(33, 85);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(88, 19);
            label10.TabIndex = 39;
            label10.Text = "Stop Loss : ";
            label10.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTakeProfit3
            // 
            lblTakeProfit3.Font = new Font("Segoe UI", 9F);
            lblTakeProfit3.ForeColor = Color.White;
            lblTakeProfit3.Location = new Point(130, 185);
            lblTakeProfit3.Margin = new Padding(2, 0, 2, 0);
            lblTakeProfit3.Name = "lblTakeProfit3";
            lblTakeProfit3.Size = new Size(124, 19);
            lblTakeProfit3.TabIndex = 38;
            lblTakeProfit3.Text = "0";
            lblTakeProfit3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 9F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(33, 185);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(88, 19);
            label3.TabIndex = 37;
            label3.Text = "Take Profit 3 : ";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTakeProfit2
            // 
            lblTakeProfit2.Font = new Font("Segoe UI", 9F);
            lblTakeProfit2.ForeColor = Color.White;
            lblTakeProfit2.Location = new Point(130, 162);
            lblTakeProfit2.Margin = new Padding(2, 0, 2, 0);
            lblTakeProfit2.Name = "lblTakeProfit2";
            lblTakeProfit2.Size = new Size(124, 19);
            lblTakeProfit2.TabIndex = 36;
            lblTakeProfit2.Text = "0";
            lblTakeProfit2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI", 9F);
            label7.ForeColor = Color.White;
            label7.Location = new Point(30, 162);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(91, 19);
            label7.TabIndex = 35;
            label7.Text = "Take Profit 2 : ";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTakeProfit1
            // 
            lblTakeProfit1.Font = new Font("Segoe UI", 9F);
            lblTakeProfit1.ForeColor = Color.White;
            lblTakeProfit1.Location = new Point(130, 140);
            lblTakeProfit1.Margin = new Padding(2, 0, 2, 0);
            lblTakeProfit1.Name = "lblTakeProfit1";
            lblTakeProfit1.Size = new Size(124, 19);
            lblTakeProfit1.TabIndex = 34;
            lblTakeProfit1.Text = "0";
            lblTakeProfit1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            label9.Font = new Font("Segoe UI", 9F);
            label9.ForeColor = Color.White;
            label9.Location = new Point(31, 140);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(90, 19);
            label9.TabIndex = 33;
            label9.Text = "Take Profit 1 : ";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblEntry
            // 
            lblEntry.Font = new Font("Segoe UI", 9F);
            lblEntry.ForeColor = Color.White;
            lblEntry.Location = new Point(130, 62);
            lblEntry.Margin = new Padding(2, 0, 2, 0);
            lblEntry.Name = "lblEntry";
            lblEntry.Size = new Size(124, 19);
            lblEntry.TabIndex = 30;
            lblEntry.Text = "0";
            lblEntry.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(33, 62);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(88, 19);
            label2.TabIndex = 29;
            label2.Text = "Entry : ";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.White;
            pnlContainer.Controls.Add(pnlBody);
            pnlContainer.Controls.Add(pnlHeader);
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.Location = new Point(5, 5);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(731, 556);
            pnlContainer.TabIndex = 1;
            // 
            // FrmStockScreenerDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(741, 566);
            Controls.Add(pnlContainer);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmStockScreenerDetail";
            Padding = new Padding(5);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StockScreenerDetail";
            ((System.ComponentModel.ISupportInitialize)btnClose).EndInit();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlVideo.ResumeLayout(false);
            pnlBody.ResumeLayout(false);
            pnlBrowseFile.ResumeLayout(false);
            pnlContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox btnClose;
        private Label label4;
        private Panel panel2;
        private Panel panel3;
        private Panel pnlHeader;
        private Panel panel6;
        private Label lblCurrentValue;
        private Label label32;
        private Label lblEmiten;
        private Label lblTitleSystemDrive;
        private Panel pnlVideo;
        private Panel pnlBody;
        private Panel pnlBrowseFile;
        private Panel pnlContainer;
        private Label lblConfident;
        private Label label12;
        private Label lblStopLoss;
        private Label label10;
        private Label lblTakeProfit3;
        private Label label3;
        private Label lblTakeProfit2;
        private Label label7;
        private Label lblTakeProfit1;
        private Label label9;
        private Label lblEntry;
        private Label label2;
        private RichTextBox rtbDescription;
        private Label lblRisk;
        private Label lblRiskTitle;
        private Label lblGrowth;
        private Label label8;
        private Label lblMOAT;
        private Label lblMOATTitle;
        private Label lblScore;
        private Label label15;
        private Label lblRR;
        private Label label6;
    }
}