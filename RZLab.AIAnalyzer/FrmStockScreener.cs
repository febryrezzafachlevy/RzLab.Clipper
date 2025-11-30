using Newtonsoft.Json;
using RZLab.AIAnalyzer.Helpers;
using RZLab.Clipper.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xabe.FFmpeg;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RZLab.AIAnalyzer
{
    public partial class FrmStockScreener : Form
    {
        private AppSettingModel _appSetting = new();
        private bool _isDragging = false;
        private Point _offset;

        private List<EmitenScrenerSahamModel> _emitenScreenerParamList = new();
        private List<EmitenScrenerSahamResultModel> _emitenScreenerResultList = new();

        public FrmStockScreener()
        {
            InitializeComponent();

            LoadAppSetting();
            Initialize();
        }
        void LoadAppSetting()
        {
            var config = ConfigLoader.Load();
            _appSetting.OpenAI = new();
            _appSetting.Paths = new();

            _appSetting.OpenAI.ApiKey = config["OpenAI:ApiKey"];
            _appSetting.OpenAI.Model = config["OpenAI:Model"];
            _appSetting.Paths.FFmpegPath = config["Paths:FFmpegPath"];
            _appSetting.Paths.WhisperModelPath = config["Paths:WhisperModelPath"];
        }
        void Initialize()
        {
            pnlHeader.MouseDown += Header_MouseDown;
            pnlHeader.MouseMove += Header_MouseMove;
            pnlHeader.MouseUp += Header_MouseUp;

            // Event untuk memindahkan form dari body (opsional)
            this.MouseDown += Form_MouseDown;
            this.MouseMove += Form_MouseMove;
            this.MouseUp += Form_MouseUp;

            StockAnalyzerGridHelper.Setup(lstView);
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            var data = JsonConvert.DeserializeObject<AIScrenerSahamModel>(File.ReadAllText(txtJsonFilePath.Text));
            if (data == null) return;

            // Reset progress UI
            pbProgress.Value = 0;
            lblProgressPercent.Text = "0 %";
            lblStepProgress.Text = $"0 of {data.Emitens.Count}";
            _emitenScreenerParamList = data.Emitens;

            var results = await RunScreeningAsync(data.Emitens);
            //var results = JsonConvert.DeserializeObject<List<EmitenScrenerSahamResultModel>>(
            //    File.ReadAllText(Path.Combine(Path.GetDirectoryName(txtJsonFilePath.Text)!, "SavedDataEmiten2.json")));

            //_emitenScreenerParamList = results.Select(x => new EmitenScrenerSahamModel
            //{
            //    Name = x.Emiten,
            //    CurrentValue = x.Entry
            //}).ToList();
            _emitenScreenerResultList = results!;

            DisplayResults(results!);

            //File.WriteAllText(
            //    Path.Combine(Path.GetDirectoryName(txtJsonFilePath.Text)!, "SavedDataEmiten2.json"),
            //    JsonConvert.SerializeObject(results));
        }

        private void DisplayResults(List<EmitenScrenerSahamResultModel> results)
        {
            Task.Run(() =>
            {
                lstView.Invoke(new Action(() =>
                {
                    StockAnalyzerGridHelper.LoadData(lstView, results);
                }));
            });
        }

        public async Task<List<EmitenScrenerSahamResultModel>> RunScreeningAsync(List<EmitenScrenerSahamModel> list)
        {
            var results = new List<EmitenScrenerSahamResultModel>();
            int total = list.Count;
            int current = 0;

            pbProgress.Minimum = 0;
            pbProgress.Maximum = total;
            pbProgress.Value = 0;

            lblProgressPercent.Text = "0 %";
            lblStepProgress.Text = $"0 of {total}";

            var stockAnalyzer = new StockAnalyzer(_appSetting.OpenAI.ApiKey, _appSetting.OpenAI.Model);

            foreach (var e in list)
            {
                JsonDocument doc = await stockAnalyzer.AskAsync(e);
                EmitenScrenerSahamResultModel model = StockAnalyzerGridHelper.ParseResult(doc);
                results.Add(model);

                // --- UPDATE PROGRESS ---
                current++;
                pbProgress.Value = current;

                int percent = (int)((double)current / total * 100);
                lblProgressPercent.Text = percent + " %";
                lblStepProgress.Text = $"{current} of {total}";
                lblProgressPercent.Refresh();
                lblStepProgress.Refresh();

                await Task.Delay(200); // hindari rate limit
            }

            return results;
        }

        private void Header_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _offset = e.Location;
            }
        }
        private void Header_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                this.Location = new Point(
                    this.Left + e.X - _offset.X,
                    this.Top + e.Y - _offset.Y);
            }
        }

        private void Header_MouseUp(object? sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void Form_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Y > 40) // 40 adalah tinggi header
            {
                _isDragging = true;
                _offset = e.Location;
            }
        }

        private void Form_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                this.Location = new Point(
                    this.Left + e.X - _offset.X,
                    this.Top + e.Y - _offset.Y);
            }
        }

        private void Form_MouseUp(object? sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void btnPackagePath_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Json Files|*.json;";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtJsonFilePath.Text = dlg.FileName;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void lstView_DoubleClick(object sender, EventArgs e)
        {
            if (lstView.SelectedItems.Count == 0) return;
            var row = lstView.SelectedItems[0];
            var emiten = row.SubItems[0].Text;

            var emitenParam = _emitenScreenerParamList.FirstOrDefault(x => x.Name == emiten);
            var emitenResult = _emitenScreenerResultList.FirstOrDefault(x => x.Emiten == emiten);

            var frm = new FrmStockScreenerDetail(emitenParam, emitenResult);
            frm.ShowDialog();
        }
    }
}
