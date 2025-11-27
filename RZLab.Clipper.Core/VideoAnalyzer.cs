using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Whisper.net;
using Xabe.FFmpeg;

public class VideoAnalyzer
{
    private readonly string whisperPath;

    public VideoAnalyzer(string whisperModelPath)
    {
        whisperPath = whisperModelPath;
    }

    // Extract audio → WAV
    public async Task<string> ExtractAudioAsync(string videoPath)
    {
        string outFile = Path.Combine(
            Path.GetDirectoryName(videoPath),
            Path.GetFileNameWithoutExtension(videoPath) + "_audio.wav");

        var conv = await FFmpeg.Conversions.FromSnippet.ExtractAudio(videoPath, outFile);
        await conv.Start();
        return outFile;
    }

    // Convert WAV → Whisper-Ready WAV
    public async Task<string> ConvertToWhisperWav(string audioPath)
    {
        string outFile = Path.Combine(
            Path.GetDirectoryName(audioPath),
            Path.GetFileNameWithoutExtension(audioPath) + "_16k.wav");

        var conv = FFmpeg.Conversions.New()
            .AddParameter($"-i \"{audioPath}\" -ar 16000 -ac 1 -c:a pcm_s16le \"{outFile}\"");

        await conv.Start();
        return outFile;
    }

    // Transcribe
    public async Task<List<(TimeSpan Start, TimeSpan End, string Text)>> TranscribeAsync(string wavPath)
    {
        var list = new List<(TimeSpan, TimeSpan, string)>();

        var factory = WhisperFactory.FromPath(whisperPath);
        using var processor = factory.CreateBuilder().WithLanguage("id").Build();

        using FileStream fs = File.OpenRead(wavPath);

        await foreach (var seg in processor.ProcessAsync(fs))
        {
            list.Add((seg.Start, seg.End, seg.Text));
        }

        return list;
    }

    // Simple heuristic fallback jika AI tidak dipakai
    public List<Segment> DetectImportant(List<(TimeSpan Start, TimeSpan End, string Text)> segments)
    {
        var results = new List<Segment>();
        foreach (var s in segments)
        {
            if (s.Text.Length > 80)
                results.Add(new Segment(s.Start, s.End, "long_text"));
        }
        return results;
    }
}
