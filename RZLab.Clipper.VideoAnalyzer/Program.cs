using System;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;

class Program
{
    static async Task Main(string[] args)
    {
        var config = ConfigLoader.Load();

        string apiKey = config["OpenAI:ApiKey"];
        string model = config["OpenAI:Model"];
        string whisperPath = config["Paths:WhisperModelPath"];
        string ffmpegPath = config["Paths:FFmpegPath"];

        FFmpeg.SetExecutablesPath(ffmpegPath);

        string video = args.Length > 0 ? args[0] : @"C:\2_WORKSPACE\POC\SAMPLE_VIDEO\SAMPLE_1.mp4";

        var analyzer = new VideoAnalyzer(whisperPath);

        Console.WriteLine("Extracting audio...");
        var audio = await analyzer.ExtractAudioAsync(video);

        Console.WriteLine("Converting audio...");
        var whisperAudio = await analyzer.ConvertToWhisperWav(audio);

        Console.WriteLine("Transcribing...");
        var segments = await analyzer.TranscribeAsync(whisperAudio);

        string transcriptText = string.Join("\n",
            segments.Select(s => $"{s.Start}-{s.End}: {s.Text}"));

        var info = await FFmpeg.GetMediaInfo(video);
        TimeSpan duration = info.Duration;

        var summarizer = new AiSummarizer(apiKey, model);

        Console.WriteLine("Calling AI to extract segments...");
        List<Segment> important = await summarizer.ExtractAsync(transcriptText, duration);

        if (!important.Any())
        {
            Console.WriteLine("AI returned nothing. Using heuristic fallback...");
            important = analyzer.DetectImportant(segments);
        }

        Console.WriteLine("Cutting video...");
        var cutter = new FfmpegCutter();
        var outputs = await cutter.CutAsync(video, important);

        Console.WriteLine("DONE. Clips generated:");
        outputs.ForEach(Console.WriteLine);
    }
}
