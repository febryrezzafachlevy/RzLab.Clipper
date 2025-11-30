using LibVLCSharp.Shared;
using RZLab.AIAnalyzer.Helpers;
using RZLab.Clipper.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xabe.FFmpeg;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.Design.AxImporter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace RZLab.AIAnalyzer
{
    public partial class FrmVideoAnalyzer : Form
    {
        private AppSettingModel _appSetting = new();
        string selectedVideo = "";
        private bool _isDragging = false;
        private Point _offset;
        private Xabe.FFmpeg.IMediaInfo? _videoInfo;
        public List<SummaryPoint> parsedList = new();

        public FrmVideoAnalyzer()
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

            Xabe.FFmpeg.FFmpeg.SetExecutablesPath(_appSetting.Paths.FFmpegPath);
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

            VIdeoAnalyzerGridHelper.Setup(lstView);
            Helpers.ProgressHelper.Init(
                pbProgress,           // progress bar
                lblProgressPercent,     // label "100%"
                lblStepProgress,      // label "1 of 100"
                lblTotalStatus,                 // textbox log
                this                   // form instance
            );
        }

        private async void btnSelect_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Video Files|*.mp4;*.mkv;*.mov;*.avi";

                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    selectedVideo = dlg.FileName;
                    txtVideoFilePath.Text = selectedVideo;

                    FileInfo fi = new FileInfo(dlg.FileName);
                    lblVideoSize.Text = $"{fi.Length / (1024 * 1024)} Mb";

                    _videoInfo = await FFmpeg.GetMediaInfo(dlg.FileName);
                    lblVideoLength.Text = _videoInfo.Duration.ToString(@"hh\:mm\:ss");

                    Helpers.ProgressHelper.Log("Video loaded.");
                }
            }
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtVideoFilePath.Text))
            {
                MessageBox.Show("Select a video file first.");
                return;
            }

            btnProcess.Enabled = false;

            var processor = new VideoProcessingService(this, _appSetting);

            await Task.Run(async () =>
            {
                await processor.RunAsync(txtVideoFilePath.Text);
            });

            btnProcess.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _offset = e.Location;
            }
        }
        private void Header_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                this.Location = new Point(
                    this.Left + e.X - _offset.X,
                    this.Top + e.Y - _offset.Y);
            }
        }

        private void Header_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Y > 40) // 40 adalah tinggi header
            {
                _isDragging = true;
                _offset = e.Location;
            }
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                this.Location = new Point(
                    this.Left + e.X - _offset.X,
                    this.Top + e.Y - _offset.Y);
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void FrmVideoAnalyzer_Load(object sender, EventArgs e)
        {
        }

        private void lstView_DoubleClick(object sender, EventArgs e)
        {
            var selectedGrid = VIdeoAnalyzerGridHelper.HandleDoubleClick(lstView);
            if(selectedGrid == null) return;

            var frm = new FrmVideoAnalyzerDetail(selectedVideo, selectedGrid, parsedList);
            frm.ShowDialog();
        }
    }
}
