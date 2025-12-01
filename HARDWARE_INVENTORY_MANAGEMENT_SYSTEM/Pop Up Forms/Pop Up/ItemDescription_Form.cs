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
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.Class_Compnents_Of_Inventory;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class ItemDescription_Form : UserControl
    {
        private string currentImagePath;
        private string currentProductId;
        public event EventHandler CloseRequested; // Add this event

        public ItemDescription_Form()
        {
            InitializeComponent();
            WireUpEvents(); // Add this method call
        }

        private void WireUpEvents()
        {
            // Wire up the close button click event
            closeButton1.Click += closeButton1_Click;
        }

        private void ItemDescription_Form_Load(object sender, EventArgs e)
        {
            dgvProductHistory.ClearSelection();
        }

        public void LoadProductFromDatabase(string productId)
        {
            try
            {
                var detail = InventoryDatabaseHelper.GetProductDetails(productId);
                PopulateProductData(detail);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load product details: {ex.Message}");
            }
        }

        public void PopulateProductData(InventoryDatabaseHelper.ProductDetailData detail)
        {
            if (detail == null)
                return;

            currentProductId = detail.ProductId;
            ItemNameDesc.Text = detail.ProductName;
            SKUDesc.Text = $"SKU: {detail.SKU}";
            CategoryDesc.Text = $"Category: {detail.Category}";
            CurrentStockDesc.Text = $"Current Stock: {detail.CurrentStock}";
            SellingPriceDesc.Text = $"Selling Price: P {detail.SellingPrice:F2}";
            StatusDesc.Text = $"Status: {detail.Status}";

            OrderedDesc.Text = FormatTimeline("Ordered", detail.OrderedDate);
            transitDesc.Text = FormatTimeline("In Transit", detail.TransitDate);
            receivedinstoreDesc.Text = FormatTimeline("Received in Store", detail.ReceivedDate);
            availableforsaleDesc.Text = FormatTimeline("Available for Sale", detail.AvailableDate);

            brandDesc.Text = $"Brand: {detail.Description}";
            MinimumStockDesc.Text = $"Minimum Stock: {detail.ReorderPoint} pcs";
            CostPriceDesc.Text = $"Cost Price: P {detail.CostPrice:F2}";
            UnitDesc.Text = $"Unit: {detail.Unit}";
            DescriptionTextBoxLabelDesc.Text = $"Description: {detail.Description}";

            // additional info (supplier/batch/delivery)
            if (!string.IsNullOrWhiteSpace(detail.SupplierName))
            {
                brandDesc.Text = $"Supplier: {detail.SupplierName}";
            }

            currentImagePath = detail.ImagePath;
            LoadProductImage();

            LoadProductHistory(currentProductId, detail.SKU, detail.ProductName);
        }

        private string FormatTimeline(string label, DateTime? value)
        {
            if (value.HasValue)
            {
                return $"{label}: {value.Value:MMM dd, yyyy HH:mm}";
            }

            return $"{label}: Pending";
        }

        private void LoadProductImage()
        {
            try
            {
                // Clear previous image
                if (ItemImageDesc.Image != null)
                {
                    ItemImageDesc.Image.Dispose();
                    ItemImageDesc.Image = null;
                }

                if (!string.IsNullOrEmpty(currentImagePath))
                {
                    // Load new image
                    Image productImage = ProductImageManager.GetProductImage(currentImagePath);

                    // Replace the image content completely
                    ItemImageDesc.Image = productImage;
                    ItemImageDesc.SizeMode = PictureBoxSizeMode.StretchImage;

                    // Force refresh to ensure proper display
                    ItemImageDesc.Refresh();
                }
                else
                {
                    SetDefaultImage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product image: {ex.Message}");
                SetDefaultImage();
            }
        }

        private void LoadProductHistory(string productId, string sku, string productName)
        {
            try
            {
                var history = InventoryDatabaseHelper.GetProductHistory(productId, sku, productName);

                dgvProductHistory.Rows.Clear();

                foreach (DataRow row in history.Rows)
                {
                    string direction = row["Direction"]?.ToString();
                    string quantity = row["QuantityChange"]?.ToString();
                    string reference = row["Reference"]?.ToString();

                    string timestampText = string.Empty;
                    if (row["Timestamp"] is DateTime timestamp)
                    {
                        timestampText = timestamp.ToString("MMM dd, yyyy HH:mm");
                    }

                    dgvProductHistory.Rows.Add(direction, quantity, reference, timestampText);
                }

                dgvProductHistory.ClearSelection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load product history: {ex.Message}");
            }
        }

        private void SetDefaultImage()
        {
            // Create a default "No Image" placeholder
            Bitmap defaultImage = new Bitmap(ItemImageDesc.Width, ItemImageDesc.Height);
            using (Graphics g = Graphics.FromImage(defaultImage))
            {
                g.Clear(Color.LightGray);
                using (Font font = new Font("Lexend semibold", 10, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.DarkGray))
                {
                    string text = "No Image";
                    SizeF textSize = g.MeasureString(text, font);
                    g.DrawString(text, font, brush,
                        (defaultImage.Width - textSize.Width) / 2,
                        (defaultImage.Height - textSize.Height) / 2);
                }
            }
            ItemImageDesc.Image = defaultImage;
            ItemImageDesc.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void closeButton1_Click(object sender, EventArgs e)
        {
            // Go back to mainpage default view.
            CloseRequested?.Invoke(this, EventArgs.Empty);
            HideItemDescription();
        }

        private void ItemNameDesc_Click(object sender, EventArgs e)
        {
            //Itemname based on what I click on view
        }

        private void SKUDesc_Click(object sender, EventArgs e)
        {
            //SKU based on what I click on view and format is SKU: PLW-001
        }

        private void CategoryDesc_Click(object sender, EventArgs e)
        {
            //CategoryDesc based on what I click on view Category: Woods
        }

        private void CurrentStockDesc_Click(object sender, EventArgs e)
        {
            //CurrentStockDesc based on what I click on view Current Stock: 50
        }

        private void SellingPriceDesc_Click(object sender, EventArgs e)
        {
            //SellingPriceDesc based on what I click on view Selling Price: P 540.00
        }

        private void StatusDesc_Click(object sender, EventArgs e)
        {
            //StatusDesc based on what I click on view Status: Available
        }

        private void OrderedDesc_Click(object sender, EventArgs e)
        {
            //OrderedDesc based on what I click on view Ordered: Sep 25, 2025  09:31
        }

        private void transitDesc_Click(object sender, EventArgs e)
        {
            //transitDesc based on what I click on view In Transit: Sep 25, 2025  09:31
        }

        private void receivedinstoreDesc_Click(object sender, EventArgs e)
        {
            //receivedinstoreDesc based on what I click on view Received in Store: Sep 25, 2025  09:31
        }

        private void availableforsaleDesc_Click(object sender, EventArgs e)
        {
            //availableforsaleDesc based on what I click on view Available for Sale: Sep 25, 2025  09:31
        }

        private void brandDesc_Click(object sender, EventArgs e)
        {
            //brandDesc based on what I click on view Brand:  Charlotte Woods
        }

        private void MinimumStockDesc_Click(object sender, EventArgs e)
        {
            //MinimumStockDesc based on what I click on view Minimum Stock:  20 pcs
        }

        private void CostPriceDesc_Click(object sender, EventArgs e)
        {
            //CostPriceDesc based on what I click on view Cost Cost Price:  P 500.00
        }

        private void UnitDesc_Click(object sender, EventArgs e)
        {
            //UnitDesc based on what I click on view Unit:  Piece
        }

        private void DescriptionTextBoxLabelDesc_Click(object sender, EventArgs e)
        {
            //DescriptionTextBoxLabelDesc based on what I click on view Description:  Plywood 1/2 mm width
        }

        private void dgvProductHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //ignore this for now
        }

        // Remove the closeButton1_Load method completely

        // Method to show this form
        public void ShowItemDescription()
        {
            this.Visible = true;
            this.BringToFront();
        }

        // Method to hide this form
        public void HideItemDescription()
        {
            this.Visible = false;
        }

        private void ItemImageDesc_Click(object sender, EventArgs e)
        {
            //the image of what they are in the database
            // Image display is handled in LoadProductImage method
        }

     
        
    }
}