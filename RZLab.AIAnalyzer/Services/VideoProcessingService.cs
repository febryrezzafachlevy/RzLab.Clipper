using RZLab.AIAnalyzer.Helpers;
using RZLab.Clipper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZLab.AIAnalyzer
{
    public class VideoProcessingService
    {
        private readonly FrmVideoAnalyzer _frmParent;
        private readonly AppSettingModel _appSetting = new();

        public VideoProcessingService(FrmVideoAnalyzer frmParent, AppSettingModel appSetting)
        {
            _frmParent = frmParent;
            _appSetting = appSetting;
        }


        // ======================================
        // MAIN PROCESS PIPELINE
        // ======================================
        public async Task RunAsync(string videoFile)
        {
            Helpers.ProgressHelper.SetProgress(0, "Initializing...", 0);

            // Set FFmpeg path
            Xabe.FFmpeg.FFmpeg.SetExecutablesPath(_appSetting.Paths.FFmpegPath);

            var analyzer = new RZLab.Clipper.Core.VideoAnalyzer(_appSetting.Paths.WhisperModelPath);
            var sceneDetector = new SceneDetector();

            // ------------------------------
            // 1. Extract audio
            // ------------------------------
            Helpers.ProgressHelper.SetProgress(5, "Extracting audio...", 5);
            string audioFile = await analyzer.ExtractAudioAsync(videoFile);

            // ------------------------------
            // 2. Convert audio to WAV
            // ------------------------------
            Helpers.ProgressHelper.SetProgress(15, "Converting audio format...", 15);
            string wavFile = await analyzer.ConvertToWhisperWav(audioFile);

            // ------------------------------
            // 3. Transcribe Whisper
            // ------------------------------
            Helpers.ProgressHelper.SetProgress(20, "Transcribing audio...", 20);

            var segments = await analyzer.TranscribeAsync(wavFile, (p) =>
            {
                Helpers.ProgressHelper.SetProgress(20 + (p / 2),
                    $"Transcribing... {p}%", 20 + (p / 2));
            });

            string transcript = string.Join("\n", segments.Select(s =>
                $"{(int)s.Item1.TotalSeconds}-{(int)s.Item2.TotalSeconds}: {s.Item3}"));

            var mediaInfo = await Xabe.FFmpeg.FFmpeg.GetMediaInfo(videoFile);

            // ------------------------------
            // 4. Scene Detection
            // ------------------------------
            Helpers.ProgressHelper.SetProgress(70, "Detecting scene changes...", 70);

            List<TimeSpan> scenes = await sceneDetector.DetectScenes(videoFile, 0.07);

            // ------------------------------
            // 5. Call AI Summary
            // ------------------------------
            Helpers.ProgressHelper.SetProgress(85, "Generating AI summary...", 85);

            var ai = new VideoSummaryAI(_appSetting.OpenAI.ApiKey, _appSetting.OpenAI.Model);
            string summaryText = await ai.GenerateSummary(
                transcript,
                mediaInfo.Duration,
                scenes
            );

            // ------------------------------
            // 6. Parse & Load to ListView
            // ------------------------------
            Helpers.ProgressHelper.SetProgress(95, "Parsing AI result...", 95);

            var parsed = VIdeoAnalyzerGridHelper.ParseSummary(summaryText);

            await LoadSummaryToUI(parsed);

            Helpers.ProgressHelper.SetProgress(100, "Completed.", 100);
        }


        // ======================================
        // THREAD-SAFE LOAD TO LISTVIEW
        // ======================================
        private Task LoadSummaryToUI(List<SummaryPoint> parsed)
        {
            return Task.Run(() =>
            {
                _frmParent.Invoke(new Action(() =>
                {
                    _frmParent.parsedList = parsed;
                    VIdeoAnalyzerGridHelper.LoadSummary(_frmParent.lstView, parsed);
                }));
            });
        }
    }
}
