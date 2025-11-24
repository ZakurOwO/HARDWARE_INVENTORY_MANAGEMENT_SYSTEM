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
            Form popup = new Form();
            popup.FormBorderStyle = FormBorderStyle.None;
            popup.StartPosition = FormStartPosition.CenterScreen;
            popup.BackColor = Color.White;
            popup.Size = new Size(850, 600);

            AddPurchaseOrderForm addForm = new AddPurchaseOrderForm();
            addForm.Dock = DockStyle.Fill;

            popup.Controls.Add(addForm);
            popup.ShowDialog();
        }
        
    }
}
