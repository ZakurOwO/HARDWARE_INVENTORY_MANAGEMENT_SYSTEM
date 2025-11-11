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
    public partial class Checkout_PopUp : UserControl
    {
        public Checkout_PopUp()
        {
            InitializeComponent();
        }

        private void cbxPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPaymentMethod.SelectedItem.ToString() == "Cash")
            {
                ShowCashControls();
            }
            else
            {
                HideCashControls();
            }
        }

        private void ShowCashControls()
        {
            
            cbxPaymentMethod.Width = 208;
            lblCashReceived.Visible = true;
            tbxCashReceived.Visible = true;
            pnlChangeAmount.Visible = true;
            lblChangeAmount.Visible = true;

            
        }

        private void HideCashControls()
        {
            
            cbxPaymentMethod.Width = 450;
            lblCashReceived.Visible = false;
            pnlChangeAmount.Visible = false;
        }

    }
}
