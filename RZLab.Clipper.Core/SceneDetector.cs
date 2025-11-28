using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xabe.FFmpeg;

public class SceneDetector
{
    public async Task<List<TimeSpan>> DetectScenes(string video, double threshold = 0.4)
    {
        var list = new List<TimeSpan>();

        var conv = FFmpeg.Conversions.New()
            .AddParameter($"-i \"{video}\" -filter:v \"select='gt(scene,{threshold})',showinfo\" -f null -");

        conv.OnDataReceived += (s, e) =>
        {
            if (string.IsNullOrEmpty(e.Data)) return;

            var match = Regex.Match(e.Data, @"pts_time:(\d+\.\d+)");
            if (match.Success)
            {
                double sec = double.Parse(match.Groups[1].Value.Replace(".", ","));
                list.Add(TimeSpan.FromSeconds(sec));
            }
        };

        await conv.Start();
        return list;
    }
}
