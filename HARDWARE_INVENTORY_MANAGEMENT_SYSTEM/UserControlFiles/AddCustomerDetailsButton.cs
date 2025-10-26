using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles
{
    public partial class AddCustomerDetailsButton : UserControl
    {
        public AddCustomerDetailsButton()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;
            this.Size = new Size(260, 50); // you can adjust
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw rounded blue rectangle
            int borderRadius = 25;
            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                int diameter = borderRadius * 2;
                path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
                path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
                path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
                path.CloseFigure();

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 102, 204))) // blue color
                    g.FillPath(brush, path);
            }

            // Draw circular + icon on the left
            int circleSize = 24;
            int circleX = 15;
            int circleY = (this.Height - circleSize) / 2;

            using (Pen pen = new Pen(Color.White, 2))
            {
                g.DrawEllipse(pen, circleX, circleY, circleSize, circleSize);
                // Draw plus sign
                int centerX = circleX + circleSize / 2;
                int centerY = circleY + circleSize / 2;
                g.DrawLine(pen, centerX, centerY - 5, centerX, centerY + 5);
                g.DrawLine(pen, centerX - 5, centerY, centerX + 5, centerY);
            }

            // Draw text
            using (Font font = new Font("Segoe UI Semibold", 10f, FontStyle.Bold))
            using (SolidBrush textBrush = new SolidBrush(Color.White))
            {
                string text = "Add new Customer Details";
                SizeF textSize = g.MeasureString(text, font);
                float textX = circleX + circleSize + 15;
                float textY = (this.Height - textSize.Height) / 2;
                g.DrawString(text, font, textBrush, textX, textY);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Invalidate(); 
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Invalidate();
        }

        // Optional click event
        public event EventHandler ButtonClick;
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            ButtonClick?.Invoke(this, e);
        }
    }
}
