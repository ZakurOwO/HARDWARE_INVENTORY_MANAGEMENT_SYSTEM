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

        // Method to populate the form with product data
        public void PopulateProductData(
            string productId,
            string productName,
            string sku,
            string category,
            int currentStock,
            decimal sellingPrice,
            string status,
            DateTime orderedDate,
            DateTime transitDate,
            DateTime receivedDate,
            DateTime availableDate,
            string brand,
            int minimumStock,
            decimal costPrice,
            string unit,
            string description,
            string imagePath)
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

            // Minimum Stock - format: "Minimum Stock: 20 pcs"
            MinimumStockDesc.Text = $"Minimum Stock: {minimumStock} pcs";

            // Cost Price - format: "Cost Price: P 500.00"
            CostPriceDesc.Text = $"Cost Price: P {costPrice:F2}";

            // Unit - format: "Unit: Piece"
            UnitDesc.Text = $"Unit: {unit}";

            // Description - format: "Description: Plywood 1/2 mm width"
            DescriptionTextBoxLabelDesc.Text = $"Description: {description}";

            // Load product image
            currentImagePath = imagePath;
            LoadProductImage();

            LoadProductHistory(currentProductId, sku, productName);
        }

        public void DisplayProductDetails(InventoryProductDetails details, List<DateTime> timelineDates)
        {
            if (details == null)
            {
                return;
            }

            string statusText = details.Active ? "Available" : "Inactive";
            int minimumStock = details.ReorderPoint;

            PopulateProductData(
                productId: details.ProductId,
                productName: details.ProductName,
                sku: details.SKU,
                category: details.CategoryName,
                currentStock: details.CurrentStock,
                sellingPrice: details.SellingPrice,
                status: statusText,
                orderedDate: DateTime.Now,   // you can replace with real dates if you have them
                transitDate: DateTime.Now,
                receivedDate: DateTime.Now,
                availableDate: DateTime.Now,
                brand: string.IsNullOrWhiteSpace(details.Description) ? "N/A" : details.Description,
                minimumStock: minimumStock,
                costPrice: details.SellingPrice,
                unit: string.IsNullOrWhiteSpace(details.UnitName) ? "Unit" : details.UnitName,
                description: string.IsNullOrWhiteSpace(details.Description) ? "No description provided" : details.Description,
                imagePath: details.ImagePath
            );

            UpdateStatusTimeline(timelineDates);
            LoadProductHistory(details.ProductId, details.SKU, details.ProductName);
        }

        // Overloaded method for simpler usage (if some dates are not available)
        public void PopulateProductData(
            string productName,
            string sku,
            string category,
            int currentStock,
            decimal sellingPrice,
            string status,
            string brand,
            int minimumStock,
            decimal costPrice,
            string unit,
            string description,
            string imagePath)
        {
            DateTime currentDate = DateTime.Now;

            PopulateProductData(
                productId: null,
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

        // Overload used by InventoryMainPage with product identity but without date metadata
        public void PopulateProductData(
            string productId,
            string productName,
            string sku,
            string category,
            int currentStock,
            decimal sellingPrice,
            string status,
            string brand,
            int minimumStock,
            decimal costPrice,
            string unit,
            string description,
            string imagePath)
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

                    Image directionIcon = GetDirectionIcon(direction);
                    dgvProductHistory.Rows.Add(directionIcon, quantity, reference, timestampText);
                }

                lblEmptyHistory.Visible = history.Rows.Count == 0;
                dgvProductHistory.Visible = history.Rows.Count > 0;
                dgvProductHistory.ClearSelection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load product history: {ex.Message}");
                lblEmptyHistory.Visible = true;
                dgvProductHistory.Visible = false;
            }
        }

        private Image GetDirectionIcon(string direction)
        {
            int size = 16;
            var bmp = new Bitmap(size, size);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);

                bool isIn = string.Equals(direction, "IN", StringComparison.OrdinalIgnoreCase);
                bool isOut = string.Equals(direction, "OUT", StringComparison.OrdinalIgnoreCase);

                // Default to gray if unknown
                Color arrowColor = isIn ? Color.Green : (isOut ? Color.Red : Color.Gray);

                using (Pen pen = new Pen(arrowColor, 2))
                {
                    int midX = size / 2;
                    int topY = 2;
                    int bottomY = size - 2;

                    if (isIn)
                    {
                        // Up arrow
                        g.DrawLine(pen, midX, bottomY, midX, topY);
                        g.DrawLine(pen, midX, topY, midX - 4, topY + 4);
                        g.DrawLine(pen, midX, topY, midX + 4, topY + 4);
                    }
                    else
                    {
                        // Down arrow
                        g.DrawLine(pen, midX, topY, midX, bottomY);
                        g.DrawLine(pen, midX, bottomY, midX - 4, bottomY - 4);
                        g.DrawLine(pen, midX, bottomY, midX + 4, bottomY - 4);
                    }
                }
            }

            return bmp;
        }

        private void UpdateStatusTimeline(List<DateTime> timelineDates)
        {
            var ordered = timelineDates != null && timelineDates.Count > 0 ? timelineDates[0] : (DateTime?)null;
            var transit = timelineDates != null && timelineDates.Count > 1 ? timelineDates[1] : (DateTime?)null;
            var received = timelineDates != null && timelineDates.Count > 2 ? timelineDates[2] : (DateTime?)null;
            var available = timelineDates != null && timelineDates.Count > 3 ? timelineDates[3] : (DateTime?)null;

            SetTimelineLabel(OrderedDesc, ordered, "Ordered");
            SetTimelineLabel(transitDesc, transit, "In Transit");
            SetTimelineLabel(receivedinstoreDesc, received, "Received in Store");
            SetTimelineLabel(availableforsaleDesc, available, "Available for Sale");

            bool hasOrdered = ordered.HasValue;
            bool hasTransit = transit.HasValue;
            bool hasReceived = received.HasValue;
            bool hasAvailable = available.HasValue;

            guna2CircleButton1.Visible = hasOrdered;
            guna2CircleButton2.Visible = hasTransit;
            guna2CircleButton3.Visible = hasReceived;
            guna2CircleButton4.Visible = hasAvailable;

            panel2.Visible = hasTransit;
            panel3.Visible = hasReceived;
            panel4.Visible = hasAvailable;
        }

        private void SetTimelineLabel(Label label, DateTime? dateValue, string prefix)
        {
            if (dateValue.HasValue)
            {
                label.Text = $"{prefix}: {dateValue:MMM dd, yyyy HH:mm}";
                label.Visible = true;
            }
            else
            {
                label.Text = $"{prefix}: -";
                label.Visible = false;
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
                    g.DrawString(
                        text,
                        font,
                        brush,
                        (defaultImage.Width - textSize.Width) / 2,
                        (defaultImage.Height - textSize.Height) / 2
                    );
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
