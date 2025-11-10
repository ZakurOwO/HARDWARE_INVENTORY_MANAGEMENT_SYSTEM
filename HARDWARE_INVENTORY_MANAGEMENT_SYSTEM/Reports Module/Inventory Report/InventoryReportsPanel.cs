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
        public InventoryReportsPanel()
        {
            InitializeComponent();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            //page 1
            ShowPage1Inventory();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //page 2
            ShowPage2Inventory();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //page 3
            ShowPage3Inventory();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            //page 4
            ShowPage4Inventory();
        }

        private void ShowPage1Inventory()
        {
            panel1.Controls.Clear();
            var page1Inventory = new InventoryPage1();
            page1Inventory.Dock = DockStyle.Fill;
            panel1.Controls.Add(page1Inventory);
        }
        private void ShowPage2Inventory()
        {
            panel1.Controls.Clear();
            var page2Inventory = new InventoryPage2();
            page2Inventory.Dock = DockStyle.Fill;
            panel1.Controls.Add(page2Inventory);
        }
        private void ShowPage3Inventory()
        {
            panel1.Controls.Clear();
            var page3Inventory = new InventoryPage3();
            page3Inventory.Dock = DockStyle.Fill;
            panel1.Controls.Add(page3Inventory);
        }
        private void ShowPage4Inventory()
        {
            panel1.Controls.Clear();
            var page4Inventory = new InventoryPage4();
            page4Inventory.Dock = DockStyle.Fill;
            panel1.Controls.Add(page4Inventory);
        }

        private void InventoryReportsPanel_Load(object sender, EventArgs e)
        {
            ShowPage1Inventory();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
