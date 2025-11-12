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
        private int currentPage = 1;
        private int totalPages = 3;

        public SupplierReportsPanel()
        {
            InitializeComponent();
            this.Load += SupplierReportsPanel_load;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            //Page 1
            ShowPage(1);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //Page 2
            ShowPage(2);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //Page 3
            ShowPage(3);
        }

        private void guna2Button6_Click(object sender, EventArgs e) // "<"
        {
            if (currentPage > 1)
            {
                currentPage--;
                ShowPage(currentPage);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e) // ">"
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                ShowPage(currentPage);
            }
        }

        private void ShowPage(int page)
        {
            panel1.Controls.Clear();
            UserControl pageControl = null;

            switch (page)
            {
                case 1:
                    pageControl = new SupplierPage1();
                    break;
                case 2:
                    pageControl = new SupplierPage2();
                    break;
                case 3:
                    pageControl = new SupplierPage3();
                    break;
            }

            if (pageControl != null)
            {
                pageControl.Dock = DockStyle.Fill;
                panel1.Controls.Add(pageControl);
            }

            currentPage = page;
        }

        private void SupplierReportsPanel_load(object sender, EventArgs e)
        {
            ShowPage(currentPage);
        }
    }
}
