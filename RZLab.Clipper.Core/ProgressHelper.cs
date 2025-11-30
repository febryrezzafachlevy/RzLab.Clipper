using System;
using System.Threading;

namespace RZLab.Clipper.Core;
public static class ProgressHelper
{
    public static void WriteProgress(string title, int percent)
    {
        Console.CursorVisible = false;

        int barSize = 40;
        int filled = (int)(percent / 100.0 * barSize);

        string bar = "[" + new string('#', filled) + new string('-', barSize - filled) + "]";
        Console.Write($"\r{bar} {percent,3}% {title}...");
    }

    public static void Done(string title)
    {
        Console.WriteLine($"\r[########################################] 100% {title} DONE");
        Console.CursorVisible = true;
    }
}
