using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class Checkout_PopUp : UserControl
    {
        public event EventHandler<CheckoutEventArgs> ProceedToPayClicked;
        public event EventHandler CloseRequested; // ADD THIS LINE - missing event

        public Checkout_PopUp()
        {
            InitializeComponent();
            InitializePaymentMethods();
        }

        private void InitializePaymentMethods()
        {
            cbxPaymentMethod.Items.Clear();
            cbxPaymentMethod.Items.Add("Cash");
            cbxPaymentMethod.Items.Add("Credit Card");
            cbxPaymentMethod.Items.Add("Debit Card");
            cbxPaymentMethod.Items.Add("GCash");
            cbxPaymentMethod.Items.Add("Maya");
            cbxPaymentMethod.SelectedIndex = 0;
        }

        public void SetAmounts(decimal subtotal, decimal tax, decimal total)
        {
            if (label5 != null) label5.Text = $"₱{subtotal:N2}";
            if (label6 != null) label6.Text = $"₱{tax:N2}";
            if (Totallbl != null) Totallbl.Text = $"₱{total:N2}";
        }

        private void cbxPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPaymentMethod.SelectedItem?.ToString() == "Cash")
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
            CalculateChange();
        }

        private void HideCashControls()
        {
            cbxPaymentMethod.Width = 450;
            lblCashReceived.Visible = false;
            tbxCashReceived.Visible = false;
            pnlChangeAmount.Visible = false;
            lblChangeAmount.Visible = false;
        }

        private void CalculateChange()
        {
            if (decimal.TryParse(tbxCashReceived.Text, out decimal cashReceived))
            {
                if (decimal.TryParse(Totallbl.Text.Replace("₱", "").Trim(), out decimal totalAmount))
                {
                    decimal change = cashReceived - totalAmount;
                    lblChangeAmount.Text = $"₱{change:N2}";

                    if (change >= 0)
                    {
                        lblChangeAmount.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblChangeAmount.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void tbxCashReceived_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private void tbxCashReceived_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ProcessPayment();
        }

        private void ProcessPayment()
        {
            string paymentMethod = cbxPaymentMethod.SelectedItem?.ToString() ?? "Cash";
            decimal cashReceived = 0;
            decimal change = 0;

            if (paymentMethod == "Cash")
            {
                if (!decimal.TryParse(tbxCashReceived.Text, out cashReceived) || cashReceived <= 0)
                {
                    MessageBox.Show("Please enter a valid cash amount.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal totalAmount = decimal.Parse(Totallbl.Text.Replace("₱", "").Trim());
                change = cashReceived - totalAmount;

                if (change < 0)
                {
                    MessageBox.Show("Insufficient cash received.", "Payment Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Raise the event to notify the container
            ProceedToPayClicked?.Invoke(this, new CheckoutEventArgs
            {
                PaymentMethod = paymentMethod,
                CashReceived = cashReceived,
                Change = change
            });
        }

        // Existing event handlers
        private void label4_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void lblChangeAmount_Click(object sender, EventArgs e) { }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Simply raise the CloseRequested event
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}