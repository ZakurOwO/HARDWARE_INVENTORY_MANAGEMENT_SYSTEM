using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public partial class MainDashBoard : Form
    {
        private TransactionsMainPage transactionsPage;
        private Walk_inCartDetails cartDetails;
        

        public MainDashBoard()
        {
            InitializeComponent();
            
        }

        // Initialize dashboard with default page
        private void MainDashBoard_Load(object sender, EventArgs e)
        {
            try
            {
                if (sidePanel1 != null && sidePanel1.Controls.ContainsKey("DashboardBTN"))
                {
                    var dashboardBtn = sidePanel1.Controls["DashboardBTN"] as Button;
                    dashboardBtn?.PerformClick();
                }
                else
                {
                    // Fallback: Load dashboard directly
                    var dashboard = new Dashboard.DashboardMainPage();
                    dashboard.Dock = DockStyle.Fill;
                    MainContentPanel.Controls.Clear();
                    MainContentPanel.Controls.Add(dashboard);
                }
            }
            catch
            {
                // Ensure form loads even if dashboard fails
            }
        }

        // Public accessor for main content panel
        public Panel MainContentPanelAccess
        {
            get { return MainContentPanel; }
        }

        // Method to close all overlays (vehicle form, etc.)
        public void CloseAllOverlays()
        {
            // Close vehicle form container
            var overlayContainer = this.Controls.OfType<Panel>()
                .FirstOrDefault(p => p.Name == "scrollContainer" || p.Controls.OfType<AddVehicleForm>().Any());

            if (overlayContainer != null)
            {
                this.Controls.Remove(overlayContainer);
                overlayContainer.Dispose();
            }

            // Hide blur overlay
            if (pcbBlurOverlay != null)
            {
                pcbBlurOverlay.Visible = false;
            }

            // Close any other potential overlays
            var otherOverlays = this.Controls.OfType<Panel>()
                .Where(p => p.Size == new Size(583, 505) && p.Location == new Point(472, 100))
                .ToList();

            foreach (var overlay in otherOverlays)
            {
                this.Controls.Remove(overlay);
                overlay.Dispose();
            }
        }

        private void MainContentPanel_Paint(object sender, PaintEventArgs e)
        {
            // Your existing paint code if any
        }

        private void sidePanel2_Load(object sender, EventArgs e)
        {

        }

        public void OpenOverlayPanel(Form form)
        {
            try
            {
                // Create overlay panel
                Panel overlay = new Panel
                {
                    Name = "CustomerEditOverlay",
                    Size = new Size(600, 550),
                    BackColor = Color.White,
                    Location = new Point(
                        (this.Width - 600) / 2,
                        (this.Height - 550) / 2
                    )
                };

                // Optional blur overlay
                if (pcbBlurOverlay != null)
                    pcbBlurOverlay.Visible = true;

                form.TopLevel = false;
                form.Dock = DockStyle.Fill;
                overlay.Controls.Add(form);

                this.Controls.Add(overlay);
                overlay.BringToFront();
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening overlay panel: " + ex.Message);
            }
        }
        public void refreshbtnn()
        {
            try
            {
                // Find the Customer Main Page
                var customerPage = MainContentPanel.Controls
                    .OfType<Customer_Module.CustomerMainPage>()
                    .FirstOrDefault();

                customerPage?.RefreshCustomerList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing customer page: " + ex.Message);
            }
        }

    }
}