using System;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public class CheckoutPopUpContainer
    {
        private Panel scrollContainer;
        private Checkout_PopUp checkoutPopUp;
        private MainDashBoard mainForm; // Changed from TransactionsMainPage to MainDashBoard
        private decimal totalAmount;
        private decimal subtotal;
        private decimal tax;
        private string transactionType;
        private object cartData;


        // Updated method to accept MainDashBoard instead of TransactionsMainPage
        public void ShowCheckoutPopUp(MainDashBoard mainForm, decimal total, decimal subTotal, decimal taxAmount, string type, object cart)
        {
            this.mainForm = mainForm;
            totalAmount = total;
            subtotal = subTotal;
            tax = taxAmount;
            transactionType = type;
            cartData = cart;

            // Create the Checkout_PopUp UserControl
            checkoutPopUp = new Checkout_PopUp();
            checkoutPopUp.Dock = DockStyle.None;

            // Set the values in the checkout popup
            checkoutPopUp.SetAmounts(subtotal, tax, totalAmount);
            checkoutPopUp.ProceedToPayClicked += CheckoutPopUp_ProceedToPayClicked;

            // SCROLL CONTAINER
            scrollContainer = new Panel();
            scrollContainer.Size = new Size(515, 402);
            scrollContainer.Location = new Point(472, 100);
            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            scrollContainer.BackColor = Color.White;

            // Add checkout popup into scroll container
            scrollContainer.Controls.Add(checkoutPopUp);
            checkoutPopUp.Size = new Size(570, 490);
            checkoutPopUp.Location = new Point(5, 5);

            // Use the mainForm directly (no need to find it)
            if (mainForm != null)
            {
                // Overlay
                mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.CustomerOvelay;
                mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
                mainForm.pcbBlurOverlay.Visible = true;
                mainForm.pcbBlurOverlay.BringToFront();

                // Bring container
                mainForm.Controls.Add(scrollContainer);
                scrollContainer.BringToFront();
                checkoutPopUp.CloseRequested += (s, e) => CloseCheckoutPopUp();
            }
        }

        private void CheckoutPopUp_ProceedToPayClicked(object sender, CheckoutEventArgs e)
        {
            try
            {
                // Process payment based on transaction type
                if (transactionType == "WalkIn")
                {
                    ProcessWalkInCheckout(e.PaymentMethod, e.CashReceived, e.Change);
                }
                else if (transactionType == "Delivery")
                {
                    ProcessDeliveryCheckout(e.PaymentMethod, e.CashReceived, e.Change);
                }

                // Close the popup after successful payment
                CloseCheckoutPopUp();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing payment: {ex.Message}", "Payment Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessWalkInCheckout(string paymentMethod, decimal cashReceived, decimal change)
        {
            var walkInCart = cartData as Walk_inCartDetails;
            if (walkInCart != null)
            {
                walkInCart.ProcessWalkInTransaction(paymentMethod, cashReceived, change);
            }
        }

        private void ProcessDeliveryCheckout(string paymentMethod, decimal cashReceived, decimal change)
        {
            var deliveryCart = cartData as DeliveryCartDetails;
            if (deliveryCart != null)
            {
                deliveryCart.ProcessDeliveryTransaction(paymentMethod, cashReceived, change);
            }
        }

        public void CloseCheckoutPopUp()
        {
            // Hide the overlay if we have mainForm
            if (mainForm != null)
            {
                mainForm.pcbBlurOverlay.Visible = false;
            }

            if (checkoutPopUp != null)
            {
                checkoutPopUp.ProceedToPayClicked -= CheckoutPopUp_ProceedToPayClicked;
                checkoutPopUp.Dispose();
                checkoutPopUp = null;
            }

            if (scrollContainer != null)
            {
                scrollContainer.Controls.Clear();
                scrollContainer.Parent?.Controls.Remove(scrollContainer);
                scrollContainer.Dispose();
                scrollContainer = null;
            }
        }
    }

    // Event arguments for checkout
    public class CheckoutEventArgs : EventArgs
    {
        public string PaymentMethod { get; set; }
        public decimal CashReceived { get; set; }
        public decimal Change { get; set; }
    }
}