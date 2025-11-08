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
            dgvInventoryList.Rows.Add("0938", "Hammer Solid", null, "Solid lamang", "bente pi", "isa");
        }

        private void dgvInventoryList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
