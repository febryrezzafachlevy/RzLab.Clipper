using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace RzLab.Clipper.ControlsLib;

[ToolboxItem(true)]
[DesignerCategory("Code")]
public class DarkTabControl : TabControl
{
    private readonly Color _selectedTabColor = Color.FromArgb(0, 122, 204);
    private readonly Color _unselectedTabColor = Color.FromArgb(45, 45, 48);
    private readonly Color _hoverTabColor = Color.FromArgb(62, 62, 64);
    private readonly Color _tabTextColor = Color.White;
    private readonly Font _tabFont = new Font("Segoe UI", 10, FontStyle.Regular);
    private int _hoveredTabIndex = -1;

    public DarkTabControl()
    {
        this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        this.DrawMode = TabDrawMode.OwnerDrawFixed;
        this.ItemSize = new Size(180, 40);
        this.SizeMode = TabSizeMode.Fixed;
        this.Alignment = TabAlignment.Top;
        this.Multiline = true;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        for (int i = 0; i < this.TabCount; i++)
        {
            if (this.GetTabRect(i).Contains(e.Location))
            {
                if (_hoveredTabIndex != i)
                {
                    _hoveredTabIndex = i;
                    this.Invalidate();
                }
                return;
            }
        }

        if (_hoveredTabIndex != -1)
        {
            _hoveredTabIndex = -1;
            this.Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var g = e.Graphics;
        g.Clear(_unselectedTabColor);

        // Draw tab headers
        for (int i = 0; i < this.TabCount; i++)
        {
            var tabRect = this.GetTabRect(i);
            var textRect = new Rectangle(tabRect.X + 12, tabRect.Y + 8,
                                       tabRect.Width - 24, tabRect.Height - 16);

            if (i == this.SelectedIndex)
            {
                // Selected tab
                g.FillRectangle(new SolidBrush(_selectedTabColor), tabRect);
                g.DrawString(this.TabPages[i].Text, _tabFont, Brushes.White, textRect);
            }
            else if (i == _hoveredTabIndex)
            {
                // Hovered tab
                g.FillRectangle(new SolidBrush(_hoverTabColor), tabRect);
                g.DrawString(this.TabPages[i].Text, _tabFont, Brushes.White, textRect);
            }
            else
            {
                // Unselected tab
                g.FillRectangle(new SolidBrush(_unselectedTabColor), tabRect);
                g.DrawString(this.TabPages[i].Text, _tabFont, new SolidBrush(Color.LightGray), textRect);
            }
        }

        // Draw border and content area
        //var contentRect = new Rectangle(0, this.ItemSize.Height,
        //                               this.Width - 1, this.Height - this.ItemSize.Height - 1);
        //g.FillRectangle(new SolidBrush(Color.FromArgb(37, 37, 38)), contentRect);
        //g.DrawRectangle(new Pen(_selectedTabColor, 1), contentRect);
    }
}
