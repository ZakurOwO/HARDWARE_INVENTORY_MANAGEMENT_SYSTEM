using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public partial class SidePanel : UserControl
    {
        private Button activeButton = null;

        public SidePanel()
        {
            InitializeComponent();
            this.Load += SidePanel_Load;
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
            // Attach paint event to all buttons
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Paint += SidebarButton_Paint; // Attach paint handler
                }
            }

            // Automatically highlight Dashboard when loaded
            if (this.Controls.ContainsKey("DashboardBTN"))
            {
                HighlightButton((Button)this.Controls["DashboardBTN"]);
            }
        }

        private void HighlightButton(Button clickedButton)
        {
            Color defaultBack = Color.FromArgb(209, 228, 242);
            Color activeBack = Color.FromArgb(184, 213, 229);
            Color activeText = Color.FromArgb(0, 87, 158);

            // Reset all buttons
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = defaultBack;
                    btn.ForeColor = Color.Black;
                    btn.Font = new Font("Lexend", 9, FontStyle.Regular);
                    btn.Tag = "inactive";
                    btn.Invalidate();
                }
            }

            // Highlight clicked button
            clickedButton.BackColor = activeBack;
            clickedButton.ForeColor = activeText;
            clickedButton.Font = new Font("Lexend SemiBold", 9, FontStyle.Regular);
            clickedButton.Tag = "active";
            activeButton = clickedButton;

            // Redraw everything
            clickedButton.Invalidate();
            this.Invalidate();
        }

        private void SidebarButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Tag != null && btn.Tag.ToString() == "active")
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 87, 158)))
                {
                    // Draw the vertical highlight line slightly inset
                    Rectangle lineRect = new Rectangle(0, 4, 4, btn.Height - 8);
                    e.Graphics.FillRectangle(brush, lineRect);
                }
            }
        }

        // Button click handlers
        private void DashboardBTN_Click(object sender, EventArgs e) => HighlightButton((Button)sender);
        private void AccountBTN_Click(object sender, EventArgs e) => HighlightButton((Button)sender);
        private void InventoryBTN_Click(object sender, EventArgs e) => HighlightButton((Button)sender);
        private void TransactionBTN_Click(object sender, EventArgs e) => HighlightButton((Button)sender);
        private void CustomerBTN_Click(object sender, EventArgs e) => HighlightButton((Button)sender);
        private void SupplierBTN_Click(object sender, EventArgs e) => HighlightButton((Button)sender);
        private void DeliveriesBTN_Click(object sender, EventArgs e) => HighlightButton((Button)sender);
        private void ReportBTN_Click(object sender, EventArgs e) => HighlightButton((Button)sender);
        private void HistoryBTN_Click(object sender, EventArgs e) => HighlightButton((Button)sender);
        private void AuditlogBTN_Click(object sender, EventArgs e) => HighlightButton((Button)sender);
    }
}
