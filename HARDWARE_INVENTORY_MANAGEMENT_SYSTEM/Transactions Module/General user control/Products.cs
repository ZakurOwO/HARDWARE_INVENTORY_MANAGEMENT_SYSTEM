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

        // Event for fallback method
        public event EventHandler<Product> ProductAddedToCart;

        public Products()
        {
            InitializeComponent();
            pbxProductImage.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        // Properties
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
                    MessageBox.Show($"Added {Product_Name} to cart!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // Product name click - show details
        private void lblProductName_Click(object sender, EventArgs e)
        {
            ShowProductDetails();
        }

        // Price label click - show details
        private void lblPrice_Click(object sender, EventArgs e)
        {
            ShowProductDetails();
        }

        // Product image click - show details
        private void pbxProductImage_Click(object sender, EventArgs e)
        {
            ShowProductDetails();
        }

        // Show product details dialog
        private void ShowProductDetails()
        {
            if (ProductData != null)
            {
                MessageBox.Show($"Product: {Product_Name}\nPrice: ₱{Price:N2}\nStock: {Stock}\nSKU: {ProductData.SKU}",
                              "Product Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}