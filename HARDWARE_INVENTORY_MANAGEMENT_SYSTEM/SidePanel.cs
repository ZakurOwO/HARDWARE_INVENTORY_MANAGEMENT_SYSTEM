using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public partial class SidePanel : UserControl
    {
        private Button activeButton = null;

        public SidePanel()
        {
            InitializeComponent();
            ApplyRoundedButtons();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int radius = 20;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(Width - radius, Height - radius, radius, radius, 0, 90);
                path.AddArc(0, Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                this.Region = new Region(path);
            }
        }

        private void SidePanel_Load(object sender, EventArgs e)
        {
            HideAllHighlightLines();

            // Default selection when dashboard is shown first
            HighlightButton(DashboardBTN);
            ShowHighlightLine("HighlightLine1");
        }

        private void ApplyRoundedButtons()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Region = new Region(GetRoundRect(btn.ClientRectangle, 15));
                    btn.Font = new Font("Lexend", 9, FontStyle.Regular);
                    btn.ForeColor = Color.Black;
                    btn.BackColor = Color.FromArgb(209, 228, 242);
                    btn.Paint += SidebarButton_Paint;
                }
            }
        }

        private GraphicsPath GetRoundRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void HideAllHighlightLines()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.Name.StartsWith("HighlightLine"))
                    ctrl.Visible = false;
            }
        }

        private void ShowHighlightLine(string name)
        {
            HideAllHighlightLines();
            Control line = this.Controls[name];
            if (line != null)
                line.Visible = true;
        }

        private void HighlightButton(Button clickedButton)
        {
            Color defaultBack = Color.FromArgb(209, 228, 242);
            Color activeBack = Color.FromArgb(184, 213, 229);
            Color activeText = Color.FromArgb(0, 87, 158);

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = defaultBack;
                    btn.ForeColor = Color.Black;
                    btn.Font = new Font("Lexend", 12, FontStyle.Regular);
                    btn.Tag = "inactive";
                }
            }

            clickedButton.BackColor = activeBack;
            clickedButton.ForeColor = activeText;
            clickedButton.Font = new Font("Lexend SemiBold", 12, FontStyle.Regular);
            clickedButton.Tag = "active";

            activeButton = clickedButton;
        }


        private void SidebarButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Tag != null && btn.Tag.ToString() == "active")
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 87, 158)))
                {
                 
                    Rectangle lineRect = new Rectangle(2, 0, 5, btn.Height);
                    e.Graphics.FillRectangle(brush, lineRect);
                }
            }
        }



        private void DashboardBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowHighlightLine("HighlightLine1");
        }

        private void AccountBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowHighlightLine("HighlightLine2");
        }

        private void InventoryBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowHighlightLine("HighlightLine3");
        }

        private void TransactionBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowHighlightLine("HighlightLine4");
        }

        private void CustomerBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowHighlightLine("HighlightLine5");
        }

        private void SupplierBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowHighlightLine("HighlightLine6");
        }

        private void DeliveriesBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowHighlightLine("HighlightLine7");
        }

        private void ReportBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowHighlightLine("HighlightLine8");
        }

        private void HistoryBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowHighlightLine("HighlightLine9");
        }

        private void AuditlogBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowHighlightLine("HighlightLine10");
        }
    }
}
