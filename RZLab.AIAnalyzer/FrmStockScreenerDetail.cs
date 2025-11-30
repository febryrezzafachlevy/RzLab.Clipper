using RZLab.Clipper.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RZLab.AIAnalyzer
{
    public partial class FrmStockScreenerDetail : Form
    {
        private readonly EmitenScrenerSahamModel _emitenScrenerParam;
        private readonly EmitenScrenerSahamResultModel _emitenScrenerResult;
        public FrmStockScreenerDetail(EmitenScrenerSahamModel emitenScrenerParam, EmitenScrenerSahamResultModel emitenScrenerResult)
        {
            InitializeComponent();

            _emitenScrenerParam = emitenScrenerParam;
            _emitenScrenerResult = emitenScrenerResult;

            Initialize();
        }


        void Initialize()
        {
            lblEmiten.Text = _emitenScrenerResult.Emiten;
            lblCurrentValue.Text = _emitenScrenerParam.CurrentValue.ToString("N2");
            lblEntry.Text = _emitenScrenerResult.Entry.ToString("N2");
            lblStopLoss.Text = _emitenScrenerResult.SL.ToString("N2");
            lblTakeProfit1.Text = _emitenScrenerResult.TP1.ToString("N2");
            lblTakeProfit2.Text = _emitenScrenerResult.TP2.ToString("N2");
            lblTakeProfit3.Text = _emitenScrenerResult.TP3.ToString("N2");
            lblConfident.Text = _emitenScrenerResult.Confidence ?? "";
            lblScore.Text = _emitenScrenerResult.MultibaggerScore.ToString("N2");
            lblMOAT.Text = _emitenScrenerResult.MoatType??"";
            lblGrowth.Text = _emitenScrenerResult.GrowthType ?? "";
            lblRisk.Text = _emitenScrenerResult.RiskLevel ?? "";
            lblRR.Text = _emitenScrenerResult.RR1.ToString() + " / " + _emitenScrenerResult.RR2.ToString() + " / " + _emitenScrenerResult.RR3.ToString();

            if (!string.IsNullOrEmpty(_emitenScrenerResult.Reason))
            {
                var reasons = _emitenScrenerResult.Reason.Split(";");
                foreach (var reason in reasons)
                {
                    rtbDescription.AppendText(reason);
                }
                FormatRichTextBox(rtbDescription);
                AddLeftAccent(rtbDescription);
                HighlightKeywords(rtbDescription);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormatRichTextBox(RichTextBox rtb)
        {
            rtb.ReadOnly = false; // sementara untuk formatting
            rtb.SuspendLayout();

            rtb.BackColor = Color.FromArgb(25, 25, 25);  // background gelap modern
            rtb.ForeColor = Color.Gainsboro;             // warna teks lebih soft
            rtb.BorderStyle = BorderStyle.FixedSingle;

            rtb.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            // Bersihkan format sebelumnya agar konsisten
            rtb.SelectAll();
            rtb.SelectionIndent = 20;       // padding kiri
            rtb.SelectionRightIndent = 10;
            rtb.SelectionHangingIndent = 10;
            rtb.DeselectAll();

            // Terapkan bullet jika diinginkan
            rtb.SelectAll();
            rtb.SelectionBullet = true;
            rtb.DeselectAll();

            rtb.ResumeLayout();
            rtb.ReadOnly = true;
        }

        private void AddLeftAccent(RichTextBox rtb)
        {
            rtb.SelectionStart = 0;
            rtb.SelectionLength = rtb.Text.Length;

            rtb.SelectionBackColor = Color.FromArgb(30, 30, 30);

            // Tambahkan indent lebih besar untuk efek bar
            rtb.SelectionIndent = 25;
            rtb.SelectionHangingIndent = 10;

            // Buat garis kiri dengan karakter block
            rtb.SelectionColor = Color.Gainsboro;

            // Tambah prefix
            string[] lines = rtb.Text.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = "│  " + lines[i].Trim();
            }

            rtb.Text = string.Join("\n", lines);
        }
        private void HighlightKeywords(RichTextBox rtb)
        {
            string[] keywords = { "undervalued", "pertumbuhan", "ROE", "ROIC", "margin", "risiko", "cash", "kompeten" };
            Color highlightColor = Color.LightSkyBlue;

            foreach (string word in keywords)
            {
                int start = 0;
                while ((start = rtb.Text.IndexOf(word, start, StringComparison.InvariantCultureIgnoreCase)) >= 0)
                {
                    rtb.Select(start, word.Length);
                    rtb.SelectionColor = highlightColor;
                    start += word.Length;
                }
            }

            rtb.DeselectAll();
        }
    }
}
