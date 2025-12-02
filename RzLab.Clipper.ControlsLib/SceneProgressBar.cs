using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace RzLab.Clipper.ControlsLib
{
    public class SceneVideoProgressBar : Control
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public long Maximum { get; set; } = 100;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public long Value { get; set; } = 0;

        // SINGLE marker only
        public long? Marker { get; private set; } = null;

        public event Action<long> OnSeek;

        public SceneVideoProgressBar()
        {
            DoubleBuffered = true;
            Height = 20;
            Cursor = Cursors.Hand;

            MouseDown += (s, e) =>
            {
                if (Maximum <= 0) return;
                double ratio = (double)e.X / Width;
                long newTime = (long)(Maximum * ratio);
                Value = newTime;
                OnSeek?.Invoke(newTime);
                Invalidate();
            };
        }

        public void SetMarker(TimeSpan time)
        {
            Marker = (long)time.TotalMilliseconds;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);

            // background bar
            Rectangle bar = new Rectangle(0, Height / 3, Width, Height / 3);
            using (var bg = new SolidBrush(Color.FromArgb(45, 45, 45)))
                g.FillRectangle(bg, bar);

            // progress fill
            if (Maximum > 0)
            {
                double ratio = (double)Value / Maximum;
                int w = (int)(ratio * Width);

                using (var fill = new SolidBrush(Color.FromArgb(50, 120, 255)))
                    g.FillRectangle(fill, new Rectangle(0, Height / 3, w, Height / 3));
            }

            // draw SINGLE marker
            if (Marker.HasValue && Maximum > 0)
            {
                double pos = (double)Marker.Value / Maximum;
                int x = (int)(pos * Width);

                using (var p = new Pen(Color.Gold, 2))
                    g.DrawLine(p, x, 0, x, Height);
            }

            using (var border = new Pen(Color.Gray))
                g.DrawRectangle(border, 0, Height / 3, Width - 1, Height / 3);
        }
    }
}
