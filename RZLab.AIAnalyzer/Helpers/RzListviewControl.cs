using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Reflection;

namespace RZLab.AIAnalyzer.Helpers;

[ToolboxItem(true)]
[DesignerCategory("Code")]
public class RzListView : ListView
{
    public RzListView()
    {
        this.OwnerDraw = true;
        this.View = View.Details;
        this.FullRowSelect = true;
        this.HideSelection = false;
        this.GridLines = false;
        this.BorderStyle = BorderStyle.None;

        this.BackColor = Color.FromArgb(32, 32, 32);
        this.ForeColor = Color.White;
        this.Font = new Font("Segoe UI", 9);

        // disable hover selection/hot tracking
        this.HoverSelection = false;
        this.HotTracking = false;

        // fixed row height via ImageList hack
        var img = new ImageList();
        img.ImageSize = new Size(1, 30); // adjust row height
        this.SmallImageList = img;

        // enable double buffering (private property)
        var prop = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
        prop?.SetValue(this, true, null);

        // events
        this.DrawColumnHeader += OnDrawColumnHeader;
        this.DrawItem += OnDrawItem;
        this.DrawSubItem += OnDrawSubItem;

        // make sure no unwanted mouse hover handlers affect visuals
        this.MouseMove += (s, e) => { /* no-op */ };
    }
    private void OnDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
    {
        e.DrawDefault = false;
        using (var b = new SolidBrush(Color.FromArgb(45, 45, 45)))
            e.Graphics.FillRectangle(b, e.Bounds);

        TextRenderer.DrawText(
            e.Graphics,
            e.Header.Text,
            new Font("Segoe UI Semibold", 9),
            new Rectangle(e.Bounds.X + 8, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height),
            Color.Gainsboro,
            TextFormatFlags.Left | TextFormatFlags.VerticalCenter
        );
    }

    // DRAW ITEM: draw background + draw ALL subitems text here (VERY IMPORTANT)
    private void OnDrawItem(object sender, DrawListViewItemEventArgs e)
    {
        // stop default drawing
        e.DrawDefault = false;

        // Choose row background (striping) and subtle selection
        Color rowColor = (e.ItemIndex % 2 == 0) ? Color.FromArgb(40, 40, 40) : Color.FromArgb(34, 34, 34);
        if (e.Item.Selected)
            rowColor = Color.FromArgb(58, 58, 70); // still readable

        using (var b = new SolidBrush(rowColor))
            e.Graphics.FillRectangle(b, e.Bounds);

        // bottom separator
        using (var pen = new Pen(Color.FromArgb(50, 50, 50)))
            e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);

        // --- DRAW ALL SUBITEMS TEXT HERE ---
        // Compute x positions from columns
        int x = e.Bounds.Left;
        for (int colIndex = 0; colIndex < this.Columns.Count; colIndex++)
        {
            int colWidth = this.Columns[colIndex].Width;
            Rectangle cellRect = new Rectangle(x, e.Bounds.Top, colWidth, e.Bounds.Height);

            // left padding
            Rectangle textRect = cellRect;
            textRect.X += 8;
            textRect.Width = Math.Max(0, textRect.Width - 8);

            // get the text (if subitem missing use empty)
            string text = "";
            if (colIndex < e.Item.SubItems.Count)
                text = e.Item.SubItems[colIndex].Text ?? "";

            // decide alignment: right for numeric-like columns (simple heuristic)
            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
            if (IsColumnNumeric(colIndex))
                flags |= TextFormatFlags.Right;
            else
                flags |= TextFormatFlags.Left;

            // ensure text color always contrasts (do not rely on system)
            Color textColor = Color.WhiteSmoke;
            TextRenderer.DrawText(e.Graphics, text, this.Font, textRect, textColor, flags);

            x += colWidth;
        }
    }

    // DrawSubItem should be NO-OP to prevent double drawing (framework may still call it)
    private void OnDrawSubItem(object sender, DrawListViewSubItemEventArgs e)
    {
        e.DrawDefault = false;
        // intentionally empty — all text drawn in OnDrawItem
    }

    // Simple heuristic: consider columns after first as numeric if header contains words like "Price" or "TP" or "SL" or "Volume"
    private bool IsColumnNumeric(int colIndex)
    {
        if (colIndex <= 0) return false;
        string header = this.Columns[colIndex].Text?.ToLower() ?? "";
        if (header.Contains("price") || header.Contains("tp") || header.Contains("sl") || header.Contains("volume") || header.Contains("ma"))
            return true;
        return false;
    }
}
