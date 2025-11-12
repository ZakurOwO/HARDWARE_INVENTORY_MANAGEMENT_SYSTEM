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

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class InventoryReportsPanel : UserControl
    {
        private int currentPage = 1;
        private int totalPages = 4;

        public InventoryReportsPanel()
        {
            InitializeComponent();
            this.Load += InventoryReportsPanel_Load;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            //page 1
            ShowPage(1);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //page 2
            ShowPage(2);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //page 3
            ShowPage(3);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            //page 4
            ShowPage(4);
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
                    pageControl = new InventoryPage1();
                    break;
                case 2:
                    pageControl = new InventoryPage2();
                    break;
                case 3:
                    pageControl = new InventoryPage3();
                    break;
                case 4:
                    pageControl = new InventoryPage4();
                    break;
            }

            if (pageControl != null)
            {
                pageControl.Dock = DockStyle.Fill;
                panel1.Controls.Add(pageControl);
            }

            currentPage = page;
        }

        private void InventoryReportsPanel_Load(object sender, EventArgs e)
        {
            ShowPage(currentPage);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
