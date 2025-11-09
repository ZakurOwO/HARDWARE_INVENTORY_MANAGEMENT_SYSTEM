using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class CartDetails : UserControl
    {

        public CartDetails()
        {
            InitializeComponent();
            
        }

        private void CartDetails_Load(object sender, EventArgs e)
        {
            walkinOrDeliveryButton1.ShowWalkIn += WalkinOrDeliveryButton_ShowWalkIn;
            walkinOrDeliveryButton1.ShowDelivery += WalkinOrDeliveryButton_ShowDelivery;

            // Show Walk-In by default
            ShowWalkInControl();
        }

        private void WalkinOrDeliveryButton_ShowWalkIn(object sender, EventArgs e)
        {
            ShowWalkInControl();
        }

        private void WalkinOrDeliveryButton_ShowDelivery(object sender, EventArgs e)
        {
            ShowDeliveryControl();
        }
 
        private void ShowWalkInControl()
        {
            panelContainer.Controls.Clear();
            var walkInUC = new Walk_inCartDetails();
            walkInUC.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(walkInUC);
        }

        private void ShowDeliveryControl()
        {
            panelContainer.Controls.Clear();
            var deliveryUC = new DeliveryCartDetails();
            deliveryUC.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(deliveryUC);
        }

        private void walkinOrDeliveryButton1_Load(object sender, EventArgs e)
        {

        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
