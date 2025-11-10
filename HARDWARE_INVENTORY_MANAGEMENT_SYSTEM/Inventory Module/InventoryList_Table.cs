using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class InventoryList_Table : UserControl
    {
        public InventoryList_Table()
        {
            InitializeComponent();
        }

        private void InventoryList_Table_Load(object sender, EventArgs e)
        {
            dgvInventoryList.Rows.Add("Plywood", null, "Woods", 200, 20, "Active");
            dgvInventoryList.Rows.Add("Cement", null, "Cement", 500, 50, "Active");
            dgvInventoryList.Rows.Add("Bricks", null, "Masonry", 1000, 100, "Active");
            dgvInventoryList.Rows.Add("Steel Rods", null, "Metals", 300, 30, "Active");
            dgvInventoryList.Rows.Add("Paint", null, "Finishes", 150, 15, "Active");
            dgvInventoryList.Rows.Add("Tiles", null, "Flooring", 400, 40, "Active");
            dgvInventoryList.Rows.Add("Glass Sheets", null, "Glazing", 250, 25, "Active");
            dgvInventoryList.Rows.Add("Insulation", null, "Thermal", 180, 18, "Active");
            dgvInventoryList.Rows.Add("Roofing Shingles", null, "Roofing", 350, 35, "Active");
            dgvInventoryList.Rows.Add("Drywall", null, "Wallboard", 220, 22, "Active");
            dgvInventoryList.Rows.Add("Concrete Blocks", null, "Masonry", 800, 80, "Active");
            dgvInventoryList.Rows.Add("Lumber", null, "Woods", 600, 60, "Active");
            dgvInventoryList.Rows.Add("Electrical Wiring", null, "Electrical", 120, 12, "Active");
            dgvInventoryList.Rows.Add("Plumbing Pipes", null, "Plumbing", 140, 14, "Active");
            dgvInventoryList.Rows.Add("HVAC Ducts", null, "HVAC", 160, 16, "Active");
            dgvInventoryList.Rows.Add("Fasteners", null, "Hardware", 900, 90, "Active");
            dgvInventoryList.Rows.Add("Flooring", null, "Miscellaneous", 50, 5, "Active");
            dgvInventoryList.Rows.Add("Wall Paint", null, "Finishes", 130, 13, "Active");
            dgvInventoryList.Rows.Add("Ceiling Tiles", null, "Ceiling",  70, 7, "Active");
            dgvInventoryList.Rows.Add("Doors", null, "Woodwork", 400, 40, "Active");
            dgvInventoryList.Rows.Add("Windows", null, "Glazing", 300, 30, "Active");

            dgvInventoryList.ClearSelection();
        }

        private void dgvInventoryList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
