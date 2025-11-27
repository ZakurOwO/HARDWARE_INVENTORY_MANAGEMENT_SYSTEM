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
        private TransactionsMainPage transactionsPage;
        private decimal totalAmount;
        private decimal subtotal;
        private decimal tax;
        private string transactionType; // "WalkIn" or "Delivery"
        private object cartData; // Can be DeliveryCartDetails or Walk_inCartDetails

        public void ShowCheckoutPopUp(TransactionsMainPage transactionsPage, decimal total, decimal subTotal, decimal taxAmount, string type, object cart)
        {
            this.transactionsPage = transactionsPage;
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

            // SCROLL CONTAINER (same as customer forms)
            scrollContainer = new Panel();
            scrollContainer.Size = new Size(515, 402);
            scrollContainer.Location = new Point(472, 100);   // SAME POSITION AS CUSTOMER FORMS
            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            scrollContainer.BackColor = Color.White;

            // Add checkout popup into scroll container
            scrollContainer.Controls.Add(checkoutPopUp);
            checkoutPopUp.Size = new Size(570, 490);
            checkoutPopUp.Location = new Point(5, 5);

            // Find the parent form to add the overlay and container
            var parentForm = transactionsPage.FindForm();
            if (parentForm is MainDashBoard mainForm)
            {
                // Overlay
                mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.CustomerOvelay; // Or create a specific checkout overlay
                mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
                mainForm.pcbBlurOverlay.Visible = true;
                mainForm.pcbBlurOverlay.BringToFront();

                // Bring container
                mainForm.Controls.Add(scrollContainer);
                scrollContainer.BringToFront();
            }
            else
            {
                // Fallback: add directly to transactions page
                transactionsPage.Controls.Add(scrollContainer);
                scrollContainer.BringToFront();
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
                // Process walk-in transaction with payment details
                walkInCart.ProcessWalkInTransaction(paymentMethod, cashReceived, change);
            }
        }

        private void ProcessDeliveryCheckout(string paymentMethod, decimal cashReceived, decimal change)
        {
            var deliveryCart = cartData as DeliveryCartDetails;
            if (deliveryCart != null)
            {
                // Process delivery transaction with payment details
                deliveryCart.ProcessDeliveryTransaction(paymentMethod, cashReceived, change);
            }
        }

        public void CloseCheckoutPopUp()
        {
            // Hide the overlay if we found a MainDashBoard
            var parentForm = transactionsPage?.FindForm();
            if (parentForm is MainDashBoard mainForm)
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