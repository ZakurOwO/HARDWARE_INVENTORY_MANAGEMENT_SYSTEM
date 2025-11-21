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
        public event EventHandler<Product> ProductAddedToCart;

        // Public properties to set product data
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

        public Product ProductData { get; set; }

        public Products()
        {
            InitializeComponent();
            // Set the image display to center
            pbxProductImage.SizeMode = PictureBoxSizeMode.CenterImage;
            // Remove the hover events that change background color
            RemoveHoverEvents();
        }

        private void RemoveHoverEvents()
        {
           
        }

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

        private void lblProductName_Click(object sender, EventArgs e)
        {
            OnProductClicked();
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (ProductData != null && Stock > 0)
            {
                ProductAddedToCart?.Invoke(this, ProductData);
            }
            else if (Stock <= 0)
            {
                MessageBox.Show("This product is out of stock!", "Out of Stock",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lblPrice_Click(object sender, EventArgs e)
        {
            OnProductClicked();
        }

        private void pbxProductImage_Click(object sender, EventArgs e)
        {
            OnProductClicked();
        }

        private void OnProductClicked()
        {
            if (ProductData != null)
            {
                MessageBox.Show($"Product: {Product_Name}\nPrice: {Price:C}\nStock: {Stock}\nSKU: {ProductData.SKU}",
                              "Product Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Public method to update product data
        public void UpdateProduct(Product product)
        {
            ProductData = product;
            Product_Name = product.ProductName;
            Price = product.SellingPrice;
            Stock = product.CurrentStock;

            // Load product image using the original method
            var productImage = ProductImageManager.GetProductImage(product.ImagePath);
            Image = productImage;
        }
    }
}