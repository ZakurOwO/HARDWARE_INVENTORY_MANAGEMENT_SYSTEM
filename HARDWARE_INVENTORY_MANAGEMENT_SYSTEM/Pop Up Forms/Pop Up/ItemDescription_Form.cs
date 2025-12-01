using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.Class_Compnents_Of_Inventory;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class ItemDescription_Form : UserControl
    {
        private string currentImagePath;
        private string currentProductId;
        public event EventHandler CloseRequested;

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

            brandDesc.Text = !string.IsNullOrWhiteSpace(detail.SupplierName)
                ? $"Supplier: {detail.SupplierName}"
                : $"Brand: {detail.Description}";
            MinimumStockDesc.Text = $"Minimum Stock: {detail.ReorderPoint} pcs";
            CostPriceDesc.Text = $"Cost Price: P {detail.CostPrice:F2}";
            UnitDesc.Text = $"Unit: {detail.Unit}";
            DescriptionTextBoxLabelDesc.Text = $"Description: {detail.Description}";

            currentImagePath = detail.ImagePath;
            LoadProductImage();

            LoadProductHistory(currentProductId, detail.SKU, detail.ProductName);
        }

        private string FormatTimeline(string label, DateTime? value)
        {
            return value.HasValue
                ? $"{label}: {value.Value:MMM dd, yyyy HH:mm}"
                : $"{label}: Pending";
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
                DataTable history = InventoryDatabaseHelper.GetProductHistory(productId, sku, productName);

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
