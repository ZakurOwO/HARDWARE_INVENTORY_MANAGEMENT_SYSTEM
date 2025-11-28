using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public partial class SidePanel : UserControl
    {
        private Button activeButton = null;

        // Add this property to control visibility
        public bool IsHidden { get; private set; } = false;

        public SidePanel()
        {
            InitializeComponent();
            this.Load += SidePanel_Load;
        }

        // Apply rounded corners to the panel
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

        // Initialize panel and set default active button
        private void SidePanel_Load(object sender, EventArgs e)
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
            if (this.Controls.ContainsKey("DashboardBTN"))
            {
                HighlightButton((Button)this.Controls["DashboardBTN"]);
            }

            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                Dashboard.DashboardMainPage dashboard = new Dashboard.DashboardMainPage();
                dashboard.Dock = DockStyle.Fill;
                mainForm.MainContentPanelAccess.Controls.Add(dashboard);
            }
        }

        // Add this method to hide the side panel
        public void HidePanel()
        {
            this.Visible = false;
            this.IsHidden = true;
        }

        // Add this method to show the side panel
        public void ShowPanel()
        {
            this.Visible = true;
            this.IsHidden = false;
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

        // Helper method to close all overlays
        private void CloseAllOverlays()
        {
            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.CloseAllOverlays();
            }
        }

        // Navigation: Dashboard
        private void DashboardBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            CloseAllOverlays(); // Close any open overlays

            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                Dashboard.DashboardMainPage dashboard = new Dashboard.DashboardMainPage();
                dashboard.Dock = DockStyle.Fill;
                mainForm.MainContentPanelAccess.Controls.Add(dashboard);
            }
        }

        // Navigation: Inventory
        private void InventoryBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            CloseAllOverlays(); // Close any open overlays

            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                InventoryMainPage inventory = new InventoryMainPage();
                inventory.Dock = DockStyle.Fill;
                mainForm.MainContentPanelAccess.Controls.Add(inventory);
            }
        }

        // Navigation: Transactions
        private void TransactionBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            CloseAllOverlays(); // Close any open overlays

            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();

                // Just create and add the transaction page
                // It will handle cart creation internally
                TransactionsMainPage transactionPage = new TransactionsMainPage();
                transactionPage.Dock = DockStyle.Fill;
                mainForm.MainContentPanelAccess.Controls.Add(transactionPage);
            }
        }

        // Navigation: Customers
        private void CustomerBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            CloseAllOverlays(); // Close any open overlays

            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                CustomerMainPage customerForm = new CustomerMainPage();
                customerForm.Dock = DockStyle.Fill;
                mainForm.MainContentPanelAccess.Controls.Add(customerForm);
            }
        }

        // Navigation: Suppliers
        private void SupplierBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            CloseAllOverlays(); // Close any open overlays

            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                SuppplierMainPage supplierForm = new SuppplierMainPage();
                supplierForm.Dock = DockStyle.Fill;
                mainForm.MainContentPanelAccess.Controls.Add(supplierForm);
            }
        }

        // Navigation: Deliveries
        private void DeliveriesBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            CloseAllOverlays(); // Close any open overlays

            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                DeliveriesMainPage vehicleForm = new DeliveriesMainPage();
                vehicleForm.Dock = DockStyle.Fill;
                mainForm.MainContentPanelAccess.Controls.Add(vehicleForm);
            }
        }

        // Navigation: Reports
        private void ReportBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            CloseAllOverlays(); // Close any open overlays

            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                ReportsMainPage reports = new ReportsMainPage();
                reports.Dock = DockStyle.Fill;
                mainForm.MainContentPanelAccess.Controls.Add(reports);
            }
        }

        // Add this method for Settings button
        private void SettingsBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            CloseAllOverlays();

            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                // Hide this side panel
                this.HidePanel();

                // Show settings main page
                mainForm.MainContentPanelAccess.Controls.Clear();
                var settingsMainPage = new UserControlFiles.SettingsMainPage();
                settingsMainPage.Dock = DockStyle.Fill;
                mainForm.MainContentPanelAccess.Controls.Add(settingsMainPage);

                // The SettingsMainPage will handle showing the SettingsSidePanel
            }
        }
    }
}