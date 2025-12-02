using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using RZLab.Clipper.Core;
using System.Diagnostics;
using RzLab.Clipper.ControlsLib;


namespace RZLab.AIAnalyzer
{
    public partial class FrmVideoAnalyzerDetail : Form
    {
        private LibVLC _libVLC = new();
        private MediaPlayer? _mediaPlayer;
        private readonly string _currentVideoFile;
        private readonly SummaryPoint _summaryPoint;
        public List<SummaryPoint> _parsedList = new();

        private System.Windows.Forms.Timer? _timer;
        private SceneVideoProgressBar? videoTimeline;
        private TimeSpan _start;
        private string? _currentCutPath = null;

        public FrmVideoAnalyzerDetail(string currentVideoFile, SummaryPoint summaryPoint, List<SummaryPoint> points)
        {
            InitializeComponent();

            _currentVideoFile = currentVideoFile;
            _summaryPoint = summaryPoint;
            _parsedList = points;

            _start = GeneralHelper.FixTimestamp(_summaryPoint.Timestamp);

            Initialize();
        }

        void Initialize()
        {
            Core.Initialize(); // libvlc init

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            var videoView = new LibVLCSharp.WinForms.VideoView
            {
                MediaPlayer = _mediaPlayer,
                Dock = DockStyle.Fill
            };

            pnlVideo.Controls.Add(videoView);

            InitializeForm();
            InitializeProgressVideo();
        }

        void InitializeForm()
        {
            lblVideoTime.Text = _summaryPoint.Timestamp;
            lblVideoTitle.Text = _summaryPoint.Title;
            lblVideoScene.Text = _summaryPoint.Scene;
            lblVideoHighlight.Text = _summaryPoint.Highlight;
            lblVideoSummary.Text = _summaryPoint.Summary;
        }

        async void InitializeProgressVideo()
        {
            // INIT TIMELINE BAR
            videoTimeline = new SceneVideoProgressBar
            {
                Dock = DockStyle.Fill
            };
            panelProgress.Controls.Add(videoTimeline);

            // TIMER
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 100;
            _timer.Tick += VideoTimer_Tick;

            // Seek event
            videoTimeline.OnSeek += (ms) =>
            {
                if (_mediaPlayer != null)
                    _mediaPlayer.Time = ms;
            };

            await Task.Run(async () =>
            {
                await CutFromTimelineAsync();
            });
        }

        private async Task<bool> CutFromTimelineAsync()
        {
            try
            {
                SetControl("Loading");

                var scenes = _parsedList.Select(x => GeneralHelper.FixTimestamp(x.Timestamp)).ToList();
                TimeSpan? nextScene = GetNextScene(_start, scenes);

                if (nextScene == null)
                {
                    MessageBox.Show("Tidak ada scene berikutnya.");
                    return false;
                }

                long durationMs = (long)(nextScene.Value - _start).TotalMilliseconds;

                string outFile = Path.Combine(
                    Path.GetDirectoryName(_currentVideoFile)!,
                    $"cut_scene_{DateTime.Now:HHmmss}.mp4");

                string ss = _start.ToString(@"hh\:mm\:ss\.fff");
                string dur = TimeSpan.FromMilliseconds(durationMs).ToString(@"hh\:mm\:ss\.fff");

                var conv = Xabe.FFmpeg.FFmpeg.Conversions.New()
                    .AddParameter($"-ss {ss} -i \"{_currentVideoFile}\" -t {dur} -c copy \"{outFile}\"");

                await conv.Start();

                _currentCutPath = outFile;

                SetControl("Ready");

                return true;
            }
            catch (Exception ex)
            {
                SetControl("Not Ready");
                _currentCutPath = null;
                MessageBox.Show("Error cutting video:\n" + ex.Message);
                return false;
            }
        }
        private TimeSpan? GetNextScene(TimeSpan current, List<TimeSpan> scenes)
        {
            foreach (var s in scenes)
                if (s > current)
                    return s;

            return null; // jika tidak ada scene berikutnya
        }

        private void btnPlayVideo_Click(object sender, EventArgs e)
        {
            PlayVideoAsync();
            Task.Delay(500).Wait();
        }
        void PlayVideoAsync()
        {
            if (_mediaPlayer == null) return;

            var ts = GeneralHelper.FixTimestamp(_summaryPoint.Timestamp);

            if (_currentCutPath == null) return;

            var sec = ts.TotalSeconds;

            _mediaPlayer.Stop();
            _mediaPlayer.Time = (long)(sec * 1000);  // milliseconds

            _mediaPlayer.Media = new Media(_libVLC, new Uri(_currentCutPath));
            _mediaPlayer.Play();

            _timer!.Start();

            TimeSpan marker = GeneralHelper.FixTimestamp(_summaryPoint.Timestamp);

            // Set markers
            videoTimeline!.SetMarker(marker);
        }
        private void VideoTimer_Tick(object? sender, EventArgs e)
        {
            if (_mediaPlayer == null || !_mediaPlayer.IsPlaying)
                return;

            long current = _mediaPlayer.Time;
            long total = _mediaPlayer.Length;

            if (total > 0)
            {
                videoTimeline!.Maximum = total;
                videoTimeline.Value = current;
                videoTimeline.Invalidate();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _mediaPlayer!.Stop();
            this.Close();
        }

        public void SetControl(string message)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action(() => lblStatus.Text = message));
            }
            else
            {
                lblStatus.Text = message;
            }

            if (btnPlayVideo.InvokeRequired)
            {
                btnPlayVideo.Invoke(new Action(() =>
                {
                    btnPlayVideo.Text = message == "Loading" ? "Loading" : "Play Video";
                    btnPlayVideo.Enabled = message == "Loading" ? false : true;
                }));
            }
            else
            {
                btnPlayVideo.Text = message == "Loading" ? "Loading" : "Play Video";
                btnPlayVideo.Enabled = message == "Loading" ? false : true;
            }
        }
    }
}
