using System;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class ItemDescription_PopUp : UserControl
    {
        private Product _currentProduct;
        private int _quantity = 1;
        public event EventHandler<(Product Product, int Quantity)> AddToCartRequested;

        public ItemDescription_PopUp()
        {
            InitializeComponent();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            // Quantity buttons
            guna2Button1.Click += (s, e) => DecreaseQuantity();
            guna2Button2.Click += (s, e) => IncreaseQuantity();
            guna2Button3.Click += (s, e) => AddToCart();

            // Quantity textbox validation
            guna2TextBox1.TextChanged += (s, e) => ValidateQuantityInput();
            guna2TextBox1.KeyPress += (s, e) => HandleQuantityKeyPress(e);
        }

        public void SetProductDetails(Product product)
        {
            if (product == null) return;

            _currentProduct = product;

            // Update UI with product details
            lblItemName.Text = product.ProductName;
            lblPrice.Text = $"₱{product.SellingPrice:N2}";
            label9.Text = string.IsNullOrEmpty(product.Description) ?
                "No description available." : product.Description;
            label6.Text = product.CategoryName ?? "Uncategorized";
            label7.Text = $"{product.CurrentStock} pcs left";

            // Load product image
            var productImage = GetResizedProductImage(product.ImagePath, 194, 108);
            pictureBox1.Image = productImage;

            // Reset quantity
            _quantity = 1;
            guna2TextBox1.Text = _quantity.ToString();

            // Update stock color
            UpdateStockDisplay(product.CurrentStock);
        }

        private Image GetResizedProductImage(string imageFileName, int width, int height)
        {
            Image originalImage = ProductImageManager.GetProductImage(imageFileName);
            return ResizeImage(originalImage, width, height);
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            if (image == null)
                return Properties.Resources.plywood1;

            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                g.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }

        private void UpdateStockDisplay(int stock)
        {
            if (stock <= 0)
            {
                label7.ForeColor = Color.Red;
                guna2Button3.Enabled = false;
                guna2Button3.BackColor = Color.Gray;
                label7.Text = "Out of stock";
            }
            else if (stock <= 5)
            {
                label7.ForeColor = Color.Orange;
                guna2Button3.Enabled = true;
                guna2Button3.BackColor = Color.FromArgb(0, 110, 196);
                label7.Text = $"{stock} pcs left (Low stock)";
            }
            else
            {
                label7.ForeColor = Color.Green;
                guna2Button3.Enabled = true;
                guna2Button3.BackColor = Color.FromArgb(0, 110, 196);
                label7.Text = $"{stock} pcs left";
            }
        }

        private void IncreaseQuantity()
        {
            if (_currentProduct != null && _quantity < _currentProduct.CurrentStock)
            {
                _quantity++;
                guna2TextBox1.Text = _quantity.ToString();
            }
            else if (_currentProduct != null && _quantity >= _currentProduct.CurrentStock)
            {
                MessageBox.Show($"Cannot exceed available stock of {_currentProduct.CurrentStock}",
                    "Stock Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DecreaseQuantity()
        {
            if (_quantity > 1)
            {
                _quantity--;
                guna2TextBox1.Text = _quantity.ToString();
            }
        }

        private void ValidateQuantityInput()
        {
            if (int.TryParse(guna2TextBox1.Text, out int newQuantity) && newQuantity > 0)
            {
                if (_currentProduct != null && newQuantity <= _currentProduct.CurrentStock)
                {
                    _quantity = newQuantity;
                }
                else if (_currentProduct != null && newQuantity > _currentProduct.CurrentStock)
                {
                    _quantity = _currentProduct.CurrentStock;
                    guna2TextBox1.Text = _currentProduct.CurrentStock.ToString();
                    MessageBox.Show($"Quantity adjusted to available stock: {_currentProduct.CurrentStock}",
                        "Stock Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                guna2TextBox1.Text = _quantity.ToString();
            }
        }

        private void HandleQuantityKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AddToCart()
        {
            if (_currentProduct == null)
            {
                MessageBox.Show("No product selected.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Invalid Quantity",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_quantity > _currentProduct.CurrentStock)
            {
                MessageBox.Show($"Insufficient stock. Only {_currentProduct.CurrentStock} available.",
                    "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AddToCartRequested?.Invoke(this, (_currentProduct, _quantity));

            MessageBox.Show($"Added {_quantity} x {_currentProduct.ProductName} to cart!",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public (Product Product, int Quantity) GetCartItem()
        {
            return (_currentProduct, _quantity);
        }

        public void Clear()
        {
            _currentProduct = null;
            _quantity = 1;
            lblItemName.Text = "Product Name";
            lblPrice.Text = "₱0.00";
            label9.Text = "Product description will appear here";
            label6.Text = "Category";
            label7.Text = "0 pcs left";
            guna2TextBox1.Text = "1";
            pictureBox1.Image = Properties.Resources.plywood1;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // Just hide this popup, don't close the entire form
            this.Visible = false;
            Console.WriteLine("Popup closed - hidden only");
        }
    }
}