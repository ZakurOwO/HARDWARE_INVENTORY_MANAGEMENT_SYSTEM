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

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class WalkinOrDeliveryButton : UserControl
    {
        public event EventHandler ShowWalkIn;
        public event EventHandler ShowDelivery;

        public WalkinOrDeliveryButton()
        {
            InitializeComponent();
        }

        private void WalkinOrDeliveryButton_Load(object sender, EventArgs e)
        {
            SelectTab(btnWalkIn);

        }

        private void SelectTab(Guna2Button selectedButton)
        {
            //reset buttons
            btnWalkIn.FillColor = Color.White;
            btnWalkIn.ForeColor = Color.Black;
            btnWalkIn.Font = new Font(btnWalkIn.Font, FontStyle.Regular);

            btnDelivery.FillColor = Color.White;
            btnDelivery.ForeColor = Color.Black;
            btnDelivery.Font = new Font(btnDelivery.Font, FontStyle.Regular);


            selectedButton.FillColor = Color.FromArgb(229, 240, 249); //light blue
            selectedButton.ForeColor = Color.FromArgb(42, 134, 205);   //dark blue
            selectedButton.Font = new Font(selectedButton.Font, FontStyle.Bold);
            selectedButton.BorderRadius = 3;
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            SelectTab(btnDelivery);
            ShowDelivery?.Invoke(this, EventArgs.Empty);
        }

        private void btnWalkIn_Click(object sender, EventArgs e)
        {
            SelectTab(btnWalkIn);
            ShowWalkIn?.Invoke(this, EventArgs.Empty);
        }

        
    }
}
