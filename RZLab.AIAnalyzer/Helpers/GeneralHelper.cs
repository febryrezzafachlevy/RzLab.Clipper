using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZLab.AIAnalyzer
{
    public static class GeneralHelper
    {
        public static TimeSpan FixTimestamp(string ts)
        {
            var parts = ts.Split(':');

            int minutes = int.Parse(parts[0]);
            int seconds = int.Parse(parts[1]);

            // Jika detik lebih dari 59 → konversi otomatis
            if (seconds >= 60)
            {
                minutes += seconds / 60;
                seconds = seconds % 60;
            }

            return new TimeSpan(0, minutes, seconds);
        }
    }
}
