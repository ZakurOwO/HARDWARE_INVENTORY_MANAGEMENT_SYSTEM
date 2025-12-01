using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using static InventoryDatabaseHelper;

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
            WireUpEvents();
        }

        private void WireUpEvents()
        {
            closeButton1.Click += closeButton1_Click;
        }

        private void ItemDescription_Form_Load(object sender, EventArgs e)
        {
            dgvProductHistory.ClearSelection();
        }

        // Method to populate the form with product data
        public void PopulateProductData(string productId, string productName, string sku, string category, int currentStock,
                                      decimal sellingPrice, string status, DateTime orderedDate,
                                      DateTime transitDate, DateTime receivedDate, DateTime availableDate,
                                      string brand, int minimumStock, decimal costPrice, string unit,
                                      string description, string imagePath)
        {
            currentProductId = productId;
            // Item Name
            ItemNameDesc.Text = productName;

            // SKU - format: "SKU: PLW-001"
            SKUDesc.Text = $"SKU: {sku}";

            // Category - format: "Category: Woods"
            CategoryDesc.Text = $"Category: {category}";

            // Current Stock - format: "Current Stock: 50"
            CurrentStockDesc.Text = $"Current Stock: {currentStock}";

            // Selling Price - format: "Selling Price: P 540.00"
            SellingPriceDesc.Text = $"Selling Price: P {sellingPrice:F2}";

            // Status - format: "Status: Available"
            StatusDesc.Text = $"Status: {status}";

            // Ordered - format: "Ordered: Sep 25, 2025 09:31"
            OrderedDesc.Text = $"Ordered: {orderedDate:MMM dd, yyyy HH:mm}";

            // In Transit - format: "In Transit: Sep 25, 2025 09:31"
            transitDesc.Text = $"In Transit: {transitDate:MMM dd, yyyy HH:mm}";

            // Received in Store - format: "Received in Store: Sep 25, 2025 09:31"
            receivedinstoreDesc.Text = $"Received in Store: {receivedDate:MMM dd, yyyy HH:mm}";

            // Available for Sale - format: "Available for Sale: Sep 25, 2025 09:31"
            availableforsaleDesc.Text = $"Available for Sale: {availableDate:MMM dd, yyyy HH:mm}";

            // Brand - format: "Brand: Charlotte Woods"
            brandDesc.Text = $"Brand: {brand}";

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

        public void PopulateProductData(ProductDetailData detail)
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

            LoadProductHistory(currentProductId, sku, productName);
        }

        private string FormatTimeline(string label, DateTime? value)
        {
            if (value.HasValue)
            {
                return $"{label}: {value.Value:MMM dd, yyyy HH:mm}";
            }

            PopulateProductData(
                productId: null,
                productName, sku, category, currentStock, sellingPrice, status,
                currentDate, currentDate, currentDate, currentDate,
                brand, minimumStock, costPrice, unit, description, imagePath
            );
        }

        // Overload used by InventoryMainPage with product identity but without date metadata
        public void PopulateProductData(string productId, string productName, string sku, string category, int currentStock,
                                      decimal sellingPrice, string status, string brand, int minimumStock,
                                      decimal costPrice, string unit, string description, string imagePath)
        {
            DateTime currentDate = DateTime.Now;

            PopulateProductData(
                productId: productId,
                productName: productName,
                sku: sku,
                category: category,
                currentStock: currentStock,
                sellingPrice: sellingPrice,
                status: status,
                orderedDate: currentDate,
                transitDate: currentDate,
                receivedDate: currentDate,
                availableDate: currentDate,
                brand: brand,
                minimumStock: minimumStock,
                costPrice: costPrice,
                unit: unit,
                description: description,
                imagePath: imagePath
            );
        }

        private void LoadProductImage()
        {
            try
            {
                if (ItemImageDesc.Image != null)
                {
                    ItemImageDesc.Image.Dispose();
                    ItemImageDesc.Image = null;
                }

                if (!string.IsNullOrEmpty(currentImagePath))
                {
                    Image productImage = ProductImageManager.GetProductImage(currentImagePath);
                    ItemImageDesc.Image = productImage;
                    ItemImageDesc.SizeMode = PictureBoxSizeMode.StretchImage;
                    ItemImageDesc.Refresh();
                }
                else
                {
                    SetDefaultImage();
                }
            }
            catch (Exception)
            {
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
            Bitmap defaultImage = new Bitmap(ItemImageDesc.Width, ItemImageDesc.Height);
            using (Graphics g = Graphics.FromImage(defaultImage))
            {
                g.Clear(Color.LightGray);
                using (Font font = new Font("Lexend semibold", 10, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.DarkGray))
                {
                    const string text = "No Image";
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
            CloseRequested?.Invoke(this, EventArgs.Empty);
            HideItemDescription();
        }

        public void ShowItemDescription()
        {
            Visible = true;
            BringToFront();
        }

        public void HideItemDescription()
        {
            Visible = false;
        }

        private void ItemImageDesc_Click(object sender, EventArgs e)
        {
            // Image display handled in LoadProductImage
        }
    }
}
