using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public partial class SupplierNavBar : UserControl
    {
        public event EventHandler ShowSuppliers;
        public event EventHandler ShowPO;
        public SupplierNavBar()
        {
            InitializeComponent();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            SelectTab(btnSuppliers);
            ShowSuppliers?.Invoke(this, EventArgs.Empty);
        }

        private void btnPurchaseOrders_Click(object sender, EventArgs e)
        {
            SelectTab(btnPurchaseOrders);
            ShowPO?.Invoke(this, EventArgs.Empty);
        }

        private void SelectTab(Guna2Button selectedButton)
        {
            //reset buttons
            btnSuppliers.FillColor = Color.White;
            btnSuppliers.ForeColor = Color.Black;
            btnSuppliers.Font = new Font(btnSuppliers.Font, FontStyle.Regular);

            btnPurchaseOrders.FillColor = Color.White;
            btnPurchaseOrders.ForeColor = Color.Black;
            btnPurchaseOrders.Font = new Font(btnPurchaseOrders.Font, FontStyle.Regular);


            selectedButton.FillColor = Color.FromArgb(229, 240, 249); //light blue
            selectedButton.ForeColor = Color.FromArgb(42, 134, 205);   //dark blue
            selectedButton.Font = new Font(selectedButton.Font, FontStyle.Bold);
            selectedButton.BorderRadius = 3;
        }

        private void SupplierNavBar_Load(object sender, EventArgs e)
        {
            SelectTab(btnSuppliers);
        }
    }
}
