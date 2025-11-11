using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Sales_Report;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class SalesPage : UserControl
    {
        public SalesPage()
        {
            InitializeComponent();
            this.Load += SalesReportPanel_load;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            //page 1
            Showpage1Sales();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //page 2
            Showpage2Sales();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //page 3
            Showpage3Sales();
        }
        private void Showpage1Sales()
        {
            panel1.Controls.Clear();
            var page1Sales = new SalesPage1();
            page1Sales.Dock = DockStyle.Fill;
            panel1.Controls.Add(page1Sales);
        }
        private void Showpage2Sales()
        {
            panel1.Controls.Clear();
            var page2Sales = new SalesPage2();
            page2Sales.Dock = DockStyle.Fill;
            panel1.Controls.Add(page2Sales);
        }
        private void Showpage3Sales()
        {
            panel1.Controls.Clear();
            var page3Sales = new SalesPage3();
            page3Sales.Dock = DockStyle.Fill;
            panel1.Controls.Add(page3Sales);
        }
        
        private void SalesReportPanel_load(object sender, EventArgs e)
        {
            Showpage1Sales();
        }
    }
}
