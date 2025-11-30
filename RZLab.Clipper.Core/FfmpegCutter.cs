using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace RZLab.Clipper.Core;
public class FfmpegCutter
{
    public async Task<List<string>> CutAsync(string inputVideo, List<Segment> segments)
    {
        var outputList = new List<string>();
        string folder = Path.Combine(
            Path.GetDirectoryName(inputVideo),
            "clips");

        Directory.CreateDirectory(folder);

        int index = 0;

        foreach (var seg in segments)
        {
            string outFile = Path.Combine(folder, $"clip_{index:000}.mp4");

            var conv = FFmpeg.Conversions.New()
                .AddParameter($"-ss {seg.Start} -i \"{inputVideo}\" -t {seg.End - seg.Start} -c copy \"{outFile}\"",
                Xabe.FFmpeg.ParameterPosition.PreInput);

            await conv.Start();

            outputList.Add(outFile);
            index++;
        }

        return outputList;
    }
}
