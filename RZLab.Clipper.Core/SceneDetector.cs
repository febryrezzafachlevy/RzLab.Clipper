using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace RZLab.Clipper.Core;
public class SceneDetector
{
    public async Task<List<TimeSpan>> DetectScenes(string video, double threshold = 0.07)
    {
        var list = new List<TimeSpan>();

        string ffmpegPath = Xabe.FFmpeg.FFmpeg.ExecutablesPath;
        string ffmpegExe = Path.Combine(ffmpegPath, "ffmpeg.exe");

        var psi = new ProcessStartInfo
        {
            FileName = ffmpegExe,
            Arguments = $"-i \"{video}\" -filter:v \"select='gt(scene,{threshold})',showinfo\" -f null -",
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var proc = Process.Start(psi);

        while (!proc.StandardError.EndOfStream)
        {
            string line = await proc.StandardError.ReadLineAsync();
            if (string.IsNullOrEmpty(line)) continue;

            // ffmpeg showinfo output contains: "pts_time:12.345"
            var match = Regex.Match(line, @"pts_time:(\d+\.\d+)");
            if (match.Success)
            {
                double sec = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                list.Add(TimeSpan.FromSeconds(sec));
            }
        }

        proc.WaitForExit();

        return list;
    }
}
