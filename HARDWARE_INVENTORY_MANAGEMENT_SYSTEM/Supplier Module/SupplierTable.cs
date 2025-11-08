using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public partial class SupplierTable: UserControl
    {
        public SupplierTable()
        {
            InitializeComponent();
        }

        private Image Action_Set(string Action)
        {
            switch (Action)
            {
                case "menu_circle_vertical":
                default:
                    return Properties.Resources.menu_circle_vertical;
            }
           


        }

        private Image status_set(string status)
        {
            switch (status)
            {
                case "Available":
                    return Properties.Resources.AvailableStatus;
                case "Pending":
                    return Properties.Resources.Pending;
                case "Canceled":
                    return Properties.Resources.Canceled;
                default:
                    return Properties.Resources.AvailableStatus;
            }

        }

        private void SupplierTable_Load(object sender, EventArgs e)
        {

            dgvSupplier.Rows.Add(status_set("Available"),Action_Set("menu_circle_vertical"));
        }
        

        private void dgvSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
