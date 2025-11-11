using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Deliveries_Report;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Supplier_Reports;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class DeliveriesReportPanel : UserControl
    {
        public DeliveriesReportPanel()
        {
            InitializeComponent();
        }

        private void DeliveriesReportPanel_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //Page 2
            Showpage2Deliveries();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            //Page 1
            Showpage1Deliveries();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

        }
        private void Showpage1Deliveries()
        {
            panel1.Controls.Clear();
            var page1Deliveries = new DeliveriesPage1();
            page1Deliveries.Dock = DockStyle.Fill;
            panel1.Controls.Add(page1Deliveries);
        }
        private void Showpage2Deliveries()
        {
            panel1.Controls.Clear();
            var page2Deliveries = new DeliveriesPage2();
            page2Deliveries.Dock = DockStyle.Fill;
            panel1.Controls.Add(page2Deliveries);
        }
        private void DeliveriesRepoerPanel_load(object sender, EventArgs e)
        {
            Showpage1Deliveries();
        }
    }
}
