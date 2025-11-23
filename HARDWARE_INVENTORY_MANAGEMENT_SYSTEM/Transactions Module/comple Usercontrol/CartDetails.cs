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
        private Walk_inCartDetails currentWalkInCart;
        private DeliveryCartDetails currentDeliveryCart;

        public CartDetails()
        {
            InitializeComponent();
        }

        // Initialize cart and setup event handlers
        private void CartDetails_Load(object sender, EventArgs e)
        {
            walkinOrDeliveryButton1.ShowWalkIn += WalkinOrDeliveryButton_ShowWalkIn;
            walkinOrDeliveryButton1.ShowDelivery += WalkinOrDeliveryButton_ShowDelivery;

            // Initialize the cart panel on load
            InitializeCartPanel();
        }

        // Initialize cart panel with toggle buttons
        private void InitializeCartPanel()
        {
            // Configure walkinOrDeliveryButton to be at specific location
            walkinOrDeliveryButton1.Location = new Point(37, 10);
            walkinOrDeliveryButton1.Height = 50; // Fixed height for the toggle buttons
            walkinOrDeliveryButton1.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // Configure panelContainer to fill the remaining space below buttons
            panelContainer.Location = new Point(0, walkinOrDeliveryButton1.Bottom + 10);
            panelContainer.Size = new Size(this.Width, this.Height - walkinOrDeliveryButton1.Bottom - 10);
            panelContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            panelContainer.BackColor = Color.White;
            panelContainer.Padding = new Padding(0);

            // Show Walk-In cart by default
            ShowWalkInControl();
        }

        // Handle Walk-In button click
        private void WalkinOrDeliveryButton_ShowWalkIn(object sender, EventArgs e)
        {
            ShowWalkInControl();
        }

        // Handle Delivery button click
        private void WalkinOrDeliveryButton_ShowDelivery(object sender, EventArgs e)
        {
            ShowDeliveryControl();
        }

        // Display Walk-In cart control
        private void ShowWalkInControl()
        {
            panelContainer.Controls.Clear();

            currentWalkInCart = new Walk_inCartDetails();
            currentWalkInCart.Dock = DockStyle.Fill;

            panelContainer.Controls.Add(currentWalkInCart);
            currentDeliveryCart = null; // Clear delivery reference
        }

        // Display Delivery cart control
        private void ShowDeliveryControl()
        {
            panelContainer.Controls.Clear();

            currentDeliveryCart = new DeliveryCartDetails();
            currentDeliveryCart.Dock = DockStyle.Fill;

            panelContainer.Controls.Add(currentDeliveryCart);
            currentWalkInCart = null; // Clear walk-in reference
        }

        // Get current Walk-In cart control
        public Walk_inCartDetails GetCurrentWalkInCart()
        {
            // Return the current walk-in cart if it's active
            if (currentWalkInCart != null)
            {
                return currentWalkInCart;
            }

            // Fallback: Search in panelContainer
            foreach (Control control in panelContainer.Controls)
            {
                if (control is Walk_inCartDetails walkInCart)
                {
                    currentWalkInCart = walkInCart;
                    return walkInCart;
                }
            }

            return null;
        }

        // Get current Delivery cart control
        public DeliveryCartDetails GetCurrentDeliveryCart()
        {
            // Return the current delivery cart if it's active
            if (currentDeliveryCart != null)
            {
                return currentDeliveryCart;
            }

            // Fallback: Search in panelContainer
            foreach (Control control in panelContainer.Controls)
            {
                if (control is DeliveryCartDetails deliveryCart)
                {
                    currentDeliveryCart = deliveryCart;
                    return deliveryCart;
                }
            }

            return null;
        }

        // Check which cart type is currently active
        public bool IsWalkInCartActive()
        {
            return currentWalkInCart != null;
        }

        public bool IsDeliveryCartActive()
        {
            return currentDeliveryCart != null;
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}