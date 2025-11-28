using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles
{
    public partial class SettingsSidePanel : UserControl
    {
        private Button activeButton = null;

        public event EventHandler AccountsClicked;
        public event EventHandler HistoryClicked;
        public event EventHandler AuditLogClicked;

        public SettingsSidePanel()
        {
            InitializeComponent();
            this.Load += SettingsSidePanel_Load;

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

        private void SettingsSidePanel_Load(object sender, EventArgs e)
        {
            // Attach paint event to all buttons for active indicator
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Paint += SidebarButton_Paint;
                }
            }

            // Set Dashboard as default active button
            if (this.Controls.ContainsKey("AccountsBTN"))
            {
                HighlightButton((Button)this.Controls["AccountsBTN"]);
                AccountsClicked?.Invoke(this, EventArgs.Empty);
            }

        }

        // Highlight the selected button and reset others
        private void HighlightButton(Button clickedButton)
        {
            Color defaultBack = Color.FromArgb(204, 228, 248);
            Color activeBack = Color.FromArgb(184, 213, 229);
            Color activeText = Color.FromArgb(0, 87, 158);

            // Reset all buttons to default state
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

            // Apply active state to clicked button
            clickedButton.BackColor = activeBack;
            clickedButton.ForeColor = activeText;
            clickedButton.Font = new Font("Lexend SemiBold", 9, FontStyle.Regular);
            clickedButton.Tag = "active";
            activeButton = clickedButton;

            clickedButton.Invalidate();
            this.Invalidate();
        }

        // Draw blue line indicator on active button
        private void SidebarButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Tag != null && btn.Tag.ToString() == "active")
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 87, 158)))
                {
                    Rectangle lineRect = new Rectangle(0, 4, 4, btn.Height - 8);
                    e.Graphics.FillRectangle(brush, lineRect);
                }
            }
        }

        private void AccountsBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            
            AccountsClicked?.Invoke(this, EventArgs.Empty);
        }

        private void HistoryBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            
            HistoryClicked?.Invoke(this, EventArgs.Empty);
        }

        private void AuditLogBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            
            AuditLogClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
