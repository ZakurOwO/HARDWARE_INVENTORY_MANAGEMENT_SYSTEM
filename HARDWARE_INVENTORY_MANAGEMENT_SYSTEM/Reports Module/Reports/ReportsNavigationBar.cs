using System;
using System.Drawing;
using System.Windows.Forms;

using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class ReportsNavigationBar : UserControl
    {
        public event EventHandler ShowInventory;
        public event EventHandler ShowSales;
        public event EventHandler ShowCustomers;
        public event EventHandler ShowSuppliers;
        public event EventHandler ShowDeliveries;

        public ReportsNavigationBar()
        {
            InitializeComponent();
        }

        private void ReportsNavigationBar_Load(object sender, EventArgs e)
        {
            SelectedTab(btnInventory);
        }

        private void SelectedTab(Guna.UI2.WinForms.Guna2Button selectedButton)
        {
            //reset buttons
            btnInventory.FillColor = Color.White;
            btnInventory.ForeColor = Color.Black;
            btnInventory.Font = new Font(btnInventory.Font, FontStyle.Regular);

            btnSales.FillColor = Color.White;
            btnSales.ForeColor = Color.Black;
            btnSales.Font = new Font(btnSales.Font, FontStyle.Regular);

            btnCustomers.FillColor = Color.White;
            btnCustomers.ForeColor = Color.Black;
            btnCustomers.Font = new Font(btnCustomers.Font, FontStyle.Regular);

            btnSuppliers.FillColor = Color.White;
            btnSuppliers.ForeColor = Color.Black;
            btnSuppliers.Font = new Font(btnSuppliers.Font, FontStyle.Regular);

            btnDeliveries.FillColor = Color.White;
            btnDeliveries.ForeColor = Color.Black;
            btnDeliveries.Font = new Font(btnDeliveries.Font, FontStyle.Regular);

            //highlight selected button
            selectedButton.FillColor = Color.FromArgb(229, 240, 249); //light blue
            selectedButton.ForeColor = Color.FromArgb(42, 134, 205);   //dark blue
            selectedButton.Font = new Font(selectedButton.Font, FontStyle.Bold);
            selectedButton.BorderRadius = 5;
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            SelectedTab(btnInventory);
            ShowInventory?.Invoke(this, EventArgs.Empty);
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            SelectedTab(btnSales);
            ShowSales?.Invoke(this, EventArgs.Empty);
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            SelectedTab(btnCustomers);
            ShowCustomers?.Invoke(this, EventArgs.Empty);
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            SelectedTab(btnSuppliers);
            ShowSuppliers?.Invoke(this, EventArgs.Empty);
        }

        private void btnDeliveries_Click(object sender, EventArgs e)
        {
            SelectedTab(btnDeliveries);
            ShowDeliveries?.Invoke(this, EventArgs.Empty);
        }
    }
}
