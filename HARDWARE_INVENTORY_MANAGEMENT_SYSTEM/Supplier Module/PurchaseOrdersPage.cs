using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.Class_Components_of_Suppliier;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public partial class PurchaseOrdersPage : UserControl
    {

        public PurchaseOrdersPage()
        {
            InitializeComponent();
        }

        private void btnMainButtonIcon_Click(object sender, EventArgs e)
        {
            ShowAddPurchaseOrderForm();
        }
        private void ShowAddPurchaseOrderForm()
        {
            Form popup = new Form();
            popup.Text = "Add Purchase Order";
            popup.StartPosition = FormStartPosition.CenterScreen;
            popup.FormBorderStyle = FormBorderStyle.FixedDialog;
            popup.MaximizeBox = false;
            popup.MinimizeBox = false;
            popup.Size = new Size(850, 600);

            AddPurchaseOrderForm addForm = new AddPurchaseOrderForm();
            addForm.Dock = DockStyle.Fill;

            popup.Controls.Add(addForm);
            popup.ShowDialog();
        }
    }
}
