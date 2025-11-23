using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public partial class SuppliersPage : UserControl
    {
        private SupplierAddContainer SupplierAddContainer = new SupplierAddContainer();

        public SuppliersPage()
        {
            InitializeComponent();
        }

        private void btnMainButtonIcon_Click(object sender, EventArgs e)
        {
            MainDashBoard main = this.FindForm() as MainDashBoard;

            if (main != null)
            {
                SupplierAddContainer.ShowSupplierAddForm(main);
            }
        }
    }
}
