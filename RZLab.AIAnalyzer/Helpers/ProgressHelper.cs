using System;
using System.Windows.Forms;

namespace RZLab.AIAnalyzer.Helpers
{
    public static class ProgressHelper
    {
        private static ProgressBar _bar;
        private static Label _percentLabel;
        private static Label _detailLabel;
        private static Label _logBox;
        private static Form _formInstance;

        // ======================
        // INITIALIZATION
        // ======================
        public static void Init(
            ProgressBar bar,
            Label percentLabel,
            Label detailLabel,
            Label logBox,
            Form form)
        {
            _bar = bar;
            _percentLabel = percentLabel;
            _detailLabel = detailLabel;
            _logBox = logBox;
            _formInstance = form;
        }


        // ======================
        // LOGGING FUNCTION
        // ======================
        public static void Log(string message)
        {
            if (_logBox == null) return;

            if (_logBox.InvokeRequired)
            {
                _logBox.Invoke(new Action(() => Append(message)));
            }
            else
            {
                Append(message);
            }
        }

        private static void Append(string message)
        {
            _logBox.Text = message;
            //_logBox.AppendText(message + Environment.NewLine);
            //_logBox.ScrollToCaret();
        }


        // ======================
        // PROGRESS UPDATER
        // ======================
        public static void SetProgress(int percent, string message = "", int step = -1)
        {
            if (_bar == null) return;

            if (_bar.InvokeRequired)
            {
                _bar.Invoke(new Action(() => ApplyProgress(percent, message, step)));
            }
            else
            {
                ApplyProgress(percent, message, step);
            }
        }

        private static void ApplyProgress(int percent, string message, int step)
        {
            percent = Math.Max(0, Math.Min(100, percent));

            _bar.Value = percent;

            if (_percentLabel != null)
                _percentLabel.Text = percent + "%";

            if (_detailLabel != null && step >= 0)
                _detailLabel.Text = step + " of 100";

            if (!string.IsNullOrWhiteSpace(message))
                Log(message);
        }
    }
}
