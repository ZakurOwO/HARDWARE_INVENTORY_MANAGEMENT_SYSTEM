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
    public partial class SuppplierMainPage : UserControl
    {
        public SuppplierMainPage()
        {
            InitializeComponent();
        }

        private void SupplierNavBar1_ShowSuppliers(object sender, EventArgs e)
        {
            ShowSuppliers();
        }

        private void SupplierNavBar1_ShowPO(object sender, EventArgs e)
        {
            ShowPurchaseOrders();
        }

        private void ShowSuppliers()
        {
            pnlContainer.Controls.Clear();
            var suppliersTable = new SuppliersPage();
            suppliersTable.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(suppliersTable);
        }

        private void ShowPurchaseOrders()
        {
            pnlContainer.Controls.Clear();
            var purchaseOrdersTable = new PurchaseOrdersPage();
            purchaseOrdersTable.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(purchaseOrdersTable);
        }

        private void SuppplierMainPage_Load(object sender, EventArgs e)
        {

            supplierNavBar1.ShowSuppliers += SupplierNavBar1_ShowSuppliers;
            supplierNavBar1.ShowPO += SupplierNavBar1_ShowPO;
            ShowSuppliers();
        }
    }
}