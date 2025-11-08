using Guna.UI2.WinForms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module;
using Krypton.Toolkit;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class DeliveriesSlideButtons : UserControl
    {
        public event EventHandler ShowDeliveries;
        public event EventHandler ShowVehicles;
        public DeliveriesSlideButtons()
        {
            InitializeComponent();

        }

        private void btnDeliveries_Click(object sender, EventArgs e)
        {
            SelectTab(btnDeliveries);
            ShowDeliveries?.Invoke(this, EventArgs.Empty);
        }

        private void btnVehicles_Click(object sender, EventArgs e)
        {
            SelectTab(btnVehicles);
            ShowVehicles?.Invoke(this, EventArgs.Empty);
        }

        private void SelectTab(Guna2Button selectedButton)
        {
            //reset buttons
            btnDeliveries.FillColor = Color.White;
            btnDeliveries.ForeColor = Color.Black;
            btnDeliveries.Font = new Font(btnDeliveries.Font, FontStyle.Regular);

            btnVehicles.FillColor = Color.White;
            btnVehicles.ForeColor = Color.Black;
            btnVehicles.Font = new Font(btnVehicles.Font, FontStyle.Regular);


            selectedButton.FillColor = Color.FromArgb(229, 240, 249); //light blue
            selectedButton.ForeColor = Color.FromArgb(42, 134, 205);   //dark blue
            selectedButton.Font = new Font(selectedButton.Font, FontStyle.Bold);
            selectedButton.BorderRadius = 3;
        }

        private void DeliveriesSlideButtons_Load(object sender, EventArgs e)
        {
            SelectTab(btnDeliveries);
        }

        
    }
}
