using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Management_System
{
    internal class RoundedPannel : Panel
    {
        // Properties to customize the look in the Property Window
        public int BorderRadius { get; set; } = 20;
        public float BorderThickness { get; set; } = 2.0f;
        public Color BorderColor { get; set; } = Color.Gray;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            // We use radius * 2 for the arc diameter to ensure proper curvature
            using (GraphicsPath path = GetRoundPath(rect, BorderRadius))
            {
                // 1. Clip the region so the background color and children follow the curves
                this.Region = new Region(path);

                // 2. Draw the border
                using (Pen pen = new Pen(BorderColor, BorderThickness))
                {
                    // Align the pen to the inside so the border isn't cut off
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private GraphicsPath GetRoundPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2f;

            // Ensure the diameter doesn't exceed control dimensions
            if (d > rect.Width) d = rect.Width;
            if (d > rect.Height) d = rect.Height;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90); // Top Left
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90); // Top Right
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90); // Bottom Right
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90); // Bottom Left
            path.CloseFigure();

            return path;
        }

        // Ensures the panel redraws correctly when resized
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            this.Invalidate();
        }
    }
}
