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

    public async Task<string> ExtractAudioAsync(string videoPath)
    {
        string outFile = Path.Combine(Path.GetDirectoryName(videoPath),
            Path.GetFileNameWithoutExtension(videoPath) + "_audio.wav");

        var conv = await FFmpeg.Conversions.FromSnippet.ExtractAudio(videoPath, outFile);
        await conv.Start();
        return outFile;
    }

    public async Task<string> ConvertToWhisperWav(string audioPath)
    {
        string outFile = Path.Combine(Path.GetDirectoryName(audioPath),
            Path.GetFileNameWithoutExtension(audioPath) + "_16k.wav");

        var conv = FFmpeg.Conversions.New()
            .AddParameter($"-i \"{audioPath}\" -ar 16000 -ac 1 -c:a pcm_s16le \"{outFile}\"");

        await conv.Start();
        return outFile;
    }

    public async Task<List<(TimeSpan, TimeSpan, string)>> TranscribeAsync(
        string wav, Action<int> onProgress)
    {
        var list = new List<(TimeSpan, TimeSpan, string)>();

        var factory = WhisperFactory.FromPath(whisperPath);
        using var processor = factory.CreateBuilder().WithLanguage("id").Build();

        using FileStream fs = File.OpenRead(wav);

        int p = 0;

        await foreach (var seg in processor.ProcessAsync(fs))
        {
            list.Add((seg.Start, seg.End, seg.Text));

            p += 3;
            if (p > 95) p = 95;
            onProgress?.Invoke(p);
        }

        onProgress?.Invoke(100);
        return list;
    }
}
