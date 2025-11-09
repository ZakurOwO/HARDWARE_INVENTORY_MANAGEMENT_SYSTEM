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

        private Image Action_Set(string EditColBtn)
        {
            switch (EditColBtn)
            {
                case "EditBtn":
                default:
                    return Properties.Resources.edit_rectangle1;
            }
           


        }

        private Image Action_Set1(string DeActiColBtn)
        {
            switch (DeActiColBtn)
            {
                case "DeactivateBtn":
                default:
                    return Properties.Resources.edit_rectangle1;
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

            dgvSupplier.Rows.Add(status_set("Available"), Action_Set("EditBtn"), Action_Set1("DeactivateBtn"));
        }
        

        private void dgvSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
