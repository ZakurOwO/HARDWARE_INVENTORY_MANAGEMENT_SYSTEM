using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Customers_Report;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class CustomersReportPanel : UserControl
    {
        private CustomersPage1 customersPage;

        public CustomersReportPanel()
        {
            InitializeComponent();
            this.Load += CustomersReportPanel_load;
        }

        private void ShowCustomerPage()
        {
            panel1.Controls.Clear();

            customersPage = new CustomersPage1();
            customersPage.Dock = DockStyle.Fill;
            panel1.Controls.Add(customersPage);
        }

        private void CustomersReportPanel_load(object sender, EventArgs e)
        {
            ShowCustomerPage();
        }

        private void mainButton1_Load(object sender, EventArgs e)
        {
            // Generate report button
        }

        private void mainButton1_Click(object sender, EventArgs e)
        {
            // Refresh all data from database
            if (customersPage != null)
            {
                customersPage.RefreshAllData();
            }
        }

        // Remove all page navigation methods that are no longer needed:
        // - guna2Button5_Click
        // - guna2Button2_Click  
        // - guna2Button6_Click
        // - guna2Button4_Click
        // - ShowPage
        // - UpdatePageButtons
        // - StyleNavigationButtons
        // - UpdatePageInfo
    }
}