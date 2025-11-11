using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Customers_Report;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Sales_Report;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class CustomersReportPanel : UserControl
    {
        public CustomersReportPanel()
        {
            InitializeComponent();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            //Page 1
            Showpage1Customers();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //Page 2
            Showpage2Customers();
        }

        private void Showpage1Customers()
        {
            panel1.Controls.Clear();
            var page1Customers = new CustomersPage1();
            page1Customers.Dock = DockStyle.Fill;
            panel1.Controls.Add(page1Customers);
        }
        private void Showpage2Customers()
        {
            panel1.Controls.Clear();
            var page2Customers = new CustomersPage2();
            page2Customers.Dock = DockStyle.Fill;
            panel1.Controls.Add(page2Customers);
        }

        private void CustomersReportPanel_load(object sender, EventArgs e)
        {
            Showpage1Customers();
        }
    }
}
