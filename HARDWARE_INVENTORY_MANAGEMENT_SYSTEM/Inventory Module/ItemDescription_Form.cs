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
    public partial class ItemDescription_Form : UserControl
    {
        public ItemDescription_Form()
        {
            InitializeComponent();

            dgvProductHistory.Rows.Add(null, "Sold 5 units", "SALE-7809", "Oct 30,2025 08:20");
            dgvProductHistory.Rows.Add(null, "Restocked 10 units", "RESTOCK-4521", "Nov 02,2025 14:15");
            dgvProductHistory.Rows.Add(null, "Sold 3 units", "SALE-7810", "Nov 05,2025 10:05");
            dgvProductHistory.Rows.Add(null, "Returned 2 units", "RETURN-1234", "Nov 10,2025 16:30");
            dgvProductHistory.Rows.Add(null, "Sold 4 units", "SALE-7811", "Nov 12,2025 11:45");
            dgvProductHistory.Rows.Add(null, "Restocked 8 units", "RESTOCK-4522", "Nov 15,2025 09:20");
            dgvProductHistory.Rows.Add(null, "Sold 6 units", "SALE-7812", "Nov 18,2025 13:10");
            dgvProductHistory.Rows.Add(null, "Returned 1 unit", "RETURN-1235", "Nov 20,2025 15:55");
            dgvProductHistory.Rows.Add(null, "Sold 7 units", "SALE-7813", "Nov 22,2025 12:30");
        }

        private void ItemDescription_Form_Load(object sender, EventArgs e)
        {
            dgvProductHistory.ClearSelection();
        }
    }
}
