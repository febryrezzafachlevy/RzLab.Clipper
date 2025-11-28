using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;

class Program
{
    static string VIDEO_FILE = @"C:\2_WORKSPACE\POC\SAMPLE_VIDEO\SAMPLE_1.mp4";
    static async Task Main(string[] args)
    {
        var config = ConfigLoader.Load();

        string apiKey = config["OpenAI:ApiKey"];
        string model = config["OpenAI:Model"];
        string whisper = config["Paths:WhisperModelPath"];
        string ffmpeg = config["Paths:FFmpegPath"];

        FFmpeg.SetExecutablesPath(ffmpeg);

        string video = args.Length > 0 ? args[0] : VIDEO_FILE;

        var analyzer = new VideoAnalyzer(whisper);

        // --- 1. Extract Audio ---
        ProgressHelper.WriteProgress("Extracting audio", 0);
        var audio = await analyzer.ExtractAudioAsync(video);
        ProgressHelper.Done("Extracting audio");

        // --- 2. Convert Audio ---
        ProgressHelper.WriteProgress("Converting audio", 0);
        var whisperAudio = await analyzer.ConvertToWhisperWav(audio);
        ProgressHelper.Done("Converting audio");

        // --- 3. Transcribe ---
        ProgressHelper.WriteProgress("Transcribing", 0);
        var segments = await analyzer.TranscribeAsync(
            whisperAudio,
            p => ProgressHelper.WriteProgress("Transcribing", p)
        );
        ProgressHelper.Done("Transcribing");

        string transcriptText = string.Join("\n",
            segments.Select(s => $"{s.Item1}-{s.Item2}: {s.Item3}"));

        var info = await FFmpeg.GetMediaInfo(video);
        var duration = info.Duration;

        // --- 4. Scene Detection ---
        Console.WriteLine("Detecting scenes...");
        var sceneDetector = new SceneDetector();
        var scenes = await sceneDetector.DetectScenes(video);

        // --- 5. AI Summary ---
        Console.WriteLine("Generating advanced summary...");
        var ai = new VideoSummaryAI(apiKey, model);
        var summary = await ai.GenerateSummary(transcriptText, duration, scenes);

        // --- OUTPUT ---
        Console.WriteLine("\n===========================");
        Console.WriteLine("FINAL SUMMARY");
        Console.WriteLine("===========================\n");
        Console.WriteLine(summary);
    }
    //static async Task CreateClipVideo(IConfiguration config, string[] args)
    //{
    //    string apiKey = config["OpenAI:ApiKey"];
    //    string model = config["OpenAI:Model"];
    //    string whisperPath = config["Paths:WhisperModelPath"];
    //    string ffmpegPath = config["Paths:FFmpegPath"];

    //    FFmpeg.SetExecutablesPath(ffmpegPath);

    //    string video = args.Length > 0 ? args[0] : @"D:\LIBRARY\VideoAnalysis\VIDEO\SAMPLE_2.mp4";

    //    var analyzer = new VideoAnalyzer(whisperPath);

    //    // ========== 1 . EXTRACT AUDIO ==========
    //    ProgressHelper.WriteProgress("Extracting audio", 0);
    //    var audio = await analyzer.ExtractAudioAsync(video);
    //    ProgressHelper.WriteProgress("Extracting audio", 50);
    //    await Task.Delay(300); // buat animasi
    //    ProgressHelper.Done("Extracting audio");

    //    // ========== 2 . CONVERT AUDIO ==========
    //    ProgressHelper.WriteProgress("Converting audio", 0);
    //    var whisperAudio = await analyzer.ConvertToWhisperWav(audio);
    //    ProgressHelper.WriteProgress("Converting audio", 60);
    //    await Task.Delay(300);
    //    ProgressHelper.Done("Converting audio");

    //    // ========== 3 . TRANSCRIBING ==========
    //    ProgressHelper.WriteProgress("Transcribing", 0);

    //    var segments = await analyzer.TranscribeAsync(whisperAudio, (p) =>
    //    {
    //        ProgressHelper.WriteProgress("Transcribing", p);
    //    });
    //    ProgressHelper.Done("Transcribing");

    //    string transcriptText = string.Join("\n",
    //        segments.Select(s => $"{s.Start}-{s.End}: {s.Text}"));

    //    var info = await FFmpeg.GetMediaInfo(video);
    //    TimeSpan duration = info.Duration;

    //    var summarizer = new AiSummarizer(apiKey, model);

    //    // ========== 4 . CALLING AI ==========
    //    ProgressHelper.WriteProgress("Calling AI to extract segments", 0);
    //    var important = await summarizer.ExtractAsync(transcriptText, duration);
    //    ProgressHelper.WriteProgress("Calling AI to extract segments", 80);
    //    await Task.Delay(300);
    //    ProgressHelper.Done("Calling AI to extract segments");

    //    if (!important.Any())
    //    {
    //        Console.WriteLine("AI returned nothing. Using heuristic fallback...");
    //        important = analyzer.DetectImportant(segments);
    //    }

    //    // ========== 5 . CUTTING VIDEO ==========
    //    var cutter = new FfmpegCutter();
    //    ProgressHelper.WriteProgress("Cutting video", 0);

    //    var outputs = await cutter.CutAsync(video, important);

    //    ProgressHelper.WriteProgress("Cutting video", 70);
    //    await Task.Delay(300);
    //    ProgressHelper.Done("Cutting video");

    //    Console.WriteLine("DONE. Clips generated:");
    //    outputs.ForEach(Console.WriteLine);
    //}
}
