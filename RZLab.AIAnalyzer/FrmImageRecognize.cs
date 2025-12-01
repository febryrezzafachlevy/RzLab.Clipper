using LibVLCSharp.Shared;
using RZLab.AIAnalyzer.Helpers;
using RZLab.Clipper.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RZLab.AIAnalyzer
{
    public partial class FrmImageRecognize : Form
    {
        private AppSettingModel _appSetting = new();
        private bool _isDragging = false;
        private Point _offset;
        public FrmImageRecognize()
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

            ImageReaderAnalyzerGridHelper.Setup(lstView);
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

            pbProgress.Value = 0;
            lblProgressPercent.Text = "0 %";
            lblStepProgress.Text = $"0 of 0";

            rbImageOptionKTP.Checked = true;
            rbImageOptionSIM.Checked = false;
            rbImageOptionNota.Checked = false;
            rbImageOptionKuitansi.Checked = false;
        }

        private void btnPackagePath_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.webp;*.tiff";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtJsonFilePath.Text = dlg.FileName;
                    var fileInfo = new FileInfo(txtJsonFilePath.Text);
                    lblImageSize.Text = fileInfo.Length.ToString();
                    lblImageExtension.Text = fileInfo.Extension.ToString();
                }
            }
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            lstView.Items.Clear();

            if (string.IsNullOrEmpty(txtJsonFilePath.Text))
            {
                MessageBox.Show("Please select an image first.");
                return;
            }

            pbProgress.Value = 0;
            lblProgressPercent.Text = "0 %";
            lblStepProgress.Text = $"0 of 1";

            int imageType = 0;
            if(rbImageOptionKTP.Checked) imageType = 1;
            else if (rbImageOptionNota.Checked) imageType = 2;
            else if (rbImageOptionSIM.Checked) imageType = 3;
            else if (rbImageOptionKuitansi.Checked) imageType = 4;

            pbProgress.Minimum = 0;
            pbProgress.Maximum = 100;
            pbProgress.Value = 0;

            lblProgressPercent.Text = "0 %";
            lblStepProgress.Text = $"0 of 1";

            try
            {
                //var _reader = new ImageReaderAnalyzer(_appSetting.OpenAI.ApiKey, _appSetting.OpenAI.Model);
                string result = await ReadImageAsync(txtJsonFilePath.Text, imageType, progress => UpdateProgress(progress));
                DisplayResults(result, progress => UpdateProgress(progress));
                lblProgressPercent.Text = "100 %";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        void UpdateProgress(int progress)
        {
            lblProgressPercent.Invoke(new Action(() =>
            {
                lblProgressPercent.Text = $"{progress} %";
            }));
            pbProgress.Invoke(new Action(() =>
            {
                pbProgress.Value = progress;
            }));
        }

        private void DisplayResults(string result, Action<int> progress)
        {
            Task.Run(() =>
            {
                lstView.Invoke(new Action(() =>
                {
                    ImageReaderAnalyzerGridHelper.LoadData(lstView, result);
                    progress(100);
                }));
            });
        }

        private void btnPreviewImage_Click(object sender, EventArgs e)
        {

        }
        public async Task<string> ReadImageAsync(string imagePath, int imageType, Action<int> progress)
        {
            progress(10);
            var imageAnalyzer = new ImageReaderAnalyzer(_appSetting.OpenAI.ApiKey, _appSetting.OpenAI.Model);
            string result = await imageAnalyzer.ReadImageAsync(imagePath, imageType, progress);
            await Task.Delay(200);
            
            File.WriteAllText(
                Path.Combine(Path.GetDirectoryName(txtJsonFilePath.Text)!, Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5) + ".json"),
                Newtonsoft.Json.JsonConvert.SerializeObject(result));

            try
            {
                var parsed = JsonSerializer.Deserialize<object>(result);
                result = JsonSerializer.Serialize(parsed, new JsonSerializerOptions { WriteIndented = true });
            }
            catch
            {
                result = "Invalid JSON:\r\n\r\n" + result;
            }

            progress(70);
            return result;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
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
    }
}
