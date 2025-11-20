using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public partial class MainDashBoard : Form
    {
        public MainDashBoard()
        {
            InitializeComponent();
        }


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
                    // Fallback: directly add the Dashboard control if button not found.
                    var dashboard = new Dashboard.DashboardMainPage();
                    dashboard.Dock = DockStyle.Fill;
                    MainContentPanel.Controls.Clear();
                    MainContentPanel.Controls.Add(dashboard);
                }
            }
            catch
            {
                // Swallow any exceptions to avoid blocking form load; fallback handled above.
            }
        }
        

        public Panel MainContentPanelAccess
        {
            get { return MainContentPanel; }
        }

        private void MainContentPanel_Paint(object sender, PaintEventArgs e)
        {

        }


      




    }
}
