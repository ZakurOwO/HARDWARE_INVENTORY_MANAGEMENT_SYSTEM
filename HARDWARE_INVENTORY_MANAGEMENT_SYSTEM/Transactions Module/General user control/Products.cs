using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class Products : UserControl
    {
        private Walk_inCartDetails _cartTable;
        private Product productData;
        private ItemDescription_PopUp _itemDescriptionPopup;

        // Events
        public event EventHandler<Product> ProductAddedToCart;
        public event EventHandler<Product> ProductDetailsRequested;

        public Products()
        {
            InitializeComponent();
            pbxProductImage.SizeMode = PictureBoxSizeMode.Zoom;
            InitializeItemDescriptionPopup();
        }

        private void InitializeItemDescriptionPopup()
        {
            _itemDescriptionPopup = new ItemDescription_PopUp();
            _itemDescriptionPopup.AddToCartRequested += (sender, e) =>
            {
                HandlePopupAddToCart(e.Product, e.Quantity);
            };
        }

        // Properties (keep your existing properties)
        public string Product_Name
        {
            get { return lblProductName.Text; }
            set { lblProductName.Text = value; }
        }

        public decimal Price
        {
            get
            {
                decimal price;
                return decimal.TryParse(lblPrice.Text.Replace("₱", "").Trim(), out price) ? price : 0;
            }
            set { lblPrice.Text = $"₱{value:N2}"; }
        }

        public int Stock
        {
            get
            {
                return ProductData?.CurrentStock ?? 0;
            }
            set
            {
                if (ProductData != null)
                    ProductData.CurrentStock = value;
                UpdateStockColor(value);
            }
        }

        public Image Image
        {
            get { return pbxProductImage.Image; }
            set { pbxProductImage.Image = value; }
        }

        public Product ProductData
        {
            get { return productData; }
            set { productData = value; }
        }

        // Set cart reference - called by TransactionsMainPage
        public void SetCartReference(Walk_inCartDetails cartTable)
        {
            _cartTable = cartTable;
        }

        // Update stock color based on availability
        private void UpdateStockColor(int stock)
        {
            if (stock <= 0)
            {
                lblProductName.ForeColor = Color.Red;
                btnAddToCart.Enabled = false;
                btnAddToCart.BackColor = Color.Gray;
            }
            else if (stock <= 5)
            {
                lblProductName.ForeColor = Color.Orange;
                btnAddToCart.Enabled = true;
                btnAddToCart.BackColor = Color.FromArgb(0, 123, 255);
            }
            else
            {
                lblProductName.ForeColor = Color.Green;
                btnAddToCart.Enabled = true;
                btnAddToCart.BackColor = Color.FromArgb(0, 123, 255);
            }
        }

        // Add to cart button click handler
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            AddProductToCart();
        }

        // Add product to cart logic
        private void AddProductToCart()
        {
            // Validate product data
            if (ProductData == null)
            {
                MessageBox.Show("Product data is missing!", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check stock availability
            if (Stock <= 0)
            {
                MessageBox.Show("This product is out of stock!", "Out of Stock",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Try to add to cart using direct reference
            if (_cartTable != null)
            {
                try
                {
                    _cartTable.AddProductToCartById(ProductData.ProductInternalID, 1);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding to cart: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Fallback: Fire event if cart reference is null
            if (ProductAddedToCart != null)
            {
                ProductAddedToCart.Invoke(this, ProductData);
            }
            else
            {
                MessageBox.Show("Cart is not available. Please try reloading the page.", "Cart Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Handle add to cart from popup
        private void HandlePopupAddToCart(Product product, int quantity)
        {
            if (product == null) return;

            if (_cartTable != null)
            {
                try
                {
                    // Add the specified quantity
                    for (int i = 0; i < quantity; i++)
                    {
                        _cartTable.AddProductToCartById(product.ProductInternalID, 1);
                    }

                  
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                MessageBox.Show("Cart is not available. Please try reloading the page.", "Cart Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Show product details in popup
        public void ShowProductDetailsPopup(Control parentControl)
        {
            if (ProductData == null) return;

            // Set product details in popup
            _itemDescriptionPopup.SetProductDetails(ProductData);

            // Position the popup (you can adjust this)
            _itemDescriptionPopup.Location = new Point(
                (parentControl.Width - _itemDescriptionPopup.Width) / 2,
                (parentControl.Height - _itemDescriptionPopup.Height) / 2
            );

            // Add to parent control if not already added
            if (!parentControl.Controls.Contains(_itemDescriptionPopup))
            {
                parentControl.Controls.Add(_itemDescriptionPopup);
                _itemDescriptionPopup.BringToFront();
            }

            _itemDescriptionPopup.Visible = true;
        }

        // Hide product details popup
        public void HideProductDetailsPopup()
        {
            if (_itemDescriptionPopup != null)
            {
                _itemDescriptionPopup.Visible = false;
            }
        }

        // Product name click - show details popup
        private void lblProductName_Click(object sender, EventArgs e)
        {
            ShowProductDetails();
        }

        // Price label click - show details popup
        private void lblPrice_Click(object sender, EventArgs e)
        {
            ShowProductDetails();
        }

        // Product image click - show details popup
        private void pbxProductImage_Click(object sender, EventArgs e)
        {
            ShowProductDetails();
        }

        // Show product details - trigger event for parent to handle
        private void ShowProductDetails()
        {
            if (ProductData != null)
            {
                ProductDetailsRequested?.Invoke(this, ProductData);
            }
        }

        // Update product data and refresh display
        public void UpdateProduct(Product product)
        {
            ProductData = product;
            Product_Name = product.ProductName;
            Price = product.SellingPrice;
            Stock = product.CurrentStock;

            var productImage = ProductImageManager.GetProductImage(product.ImagePath);
            Image = productImage;
        }

        // Add a new method to set delivery cart reference
        public void SetDeliveryCartReference(DeliveryCartDetails deliveryCart)
        {
            // You can implement this if you want separate references
            // For now, both cart types have the same method signature
        }
    }
}