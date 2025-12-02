using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using Timer = System.Windows.Forms.Timer;

namespace RzLab.Clipper.ControlsLib;
public class ModernSpinner : Control
{
    private Timer timer;
    private float angle = 0f;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SpinnerColor { get; set; } = Color.White;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Thickness { get; set; } = 3;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Radius { get; set; } = 28;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Speed { get; set; } = 6; // rotation speed

    public ModernSpinner()
    {
        DoubleBuffered = true;
        Size = new Size(60, 60);

        timer = new Timer();
        timer.Interval = 16;   // ~60 FPS
        timer.Tick += (s, e) =>
        {
            angle += Speed;
            if (angle >= 360) angle = 0;
            Invalidate();
        };
        timer.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // spinner arc definition
        var rect = new Rectangle(
            Width / 2 - Radius / 2,
            Height / 2 - Radius / 2,
            Radius,
            Radius
        );

        using (Pen p = new Pen(SpinnerColor, Thickness))
        {
            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.Round;

            // draw 270-degree arc with rotation
            e.Graphics.TranslateTransform(Width / 2, Height / 2);
            e.Graphics.RotateTransform(angle);
            e.Graphics.TranslateTransform(-Width / 2, -Height / 2);

            e.Graphics.DrawArc(p, rect, 0, 270);
        }
    }
}
