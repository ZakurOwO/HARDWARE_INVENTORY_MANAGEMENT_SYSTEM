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
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Supplier_Reports;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class SupplierReportsPanel : UserControl
    {
        public SupplierReportsPanel()
        {
            InitializeComponent();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            //Page 1
            Showpage1Supplier();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //Page 2
            Showpage2Supplier();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //Page 3
            Showpage3Supplier();
        }
        private void Showpage1Supplier()
        {
            panel1.Controls.Clear();
            var page1Supplier = new SupplierPage1();
            page1Supplier.Dock = DockStyle.Fill;
            panel1.Controls.Add(page1Supplier);
        }
        private void Showpage2Supplier()
        {
            panel1.Controls.Clear();
            var page2Supplier = new SupplierPage2();
            page2Supplier.Dock = DockStyle.Fill;
            panel1.Controls.Add(page2Supplier);
        }
        private void Showpage3Supplier()
        {
            panel1.Controls.Clear();
            var page3Supplier = new SupplierPage3();
            page3Supplier.Dock = DockStyle.Fill;
            panel1.Controls.Add(page3Supplier);
        }
        private void SupplierReportsPanel_load(object sender, EventArgs e)
        {
            Showpage1Supplier();
        }

    }
}
