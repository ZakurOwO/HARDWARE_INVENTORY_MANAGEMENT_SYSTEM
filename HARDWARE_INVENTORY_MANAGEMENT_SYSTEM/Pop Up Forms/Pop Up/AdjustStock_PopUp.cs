using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class AdjustStock_PopUp : UserControl
    {
        private string currentProductName;
        private string currentSKU;
        private string currentBrand;
        private int currentStock;
        private string currentImagePath;
        private string currentProductId;
        private int adjustmentValue = 0;
        private int newTotalStock = 0;

        public event EventHandler StockAdjusted;
        public event EventHandler Cancelled;

        public AdjustStock_PopUp()
        {
            InitializeComponent();
            SetupPictureBox();
            LockAdjustmentDisplayLocation();
            WireUpEvents();
            PopulateReasonComboBox(); // Add this line
        }

        private void SetupPictureBox()
        {
            ItemPictureBoxAdjustStock.SizeMode = PictureBoxSizeMode.StretchImage;
            ItemPictureBoxAdjustStock.BorderStyle = BorderStyle.FixedSingle;
        }

        private void LockAdjustmentDisplayLocation()
        {
            // Lock AdjustmentDisplayAdjustStock at position 400, 52
            AdjustmentDisplayAdjustStock.Location = new Point(400, 52);
            AdjustmentDisplayAdjustStock.Anchor = AnchorStyles.None; // Remove any anchoring
        }

        private void WireUpEvents()
        {
            // Wire up the guna2Button3 click event
            guna2Button3.Click += guna2Button3_Click;
        }

        private void PopulateReasonComboBox()
        {
            // Clear existing items
            ReasonComboBoxAdjustStock.Items.Clear();

            // Add meaningful stock adjustment reasons
            List<string> reasons = new List<string>
    {
        "DEFAULT",
        "New Stock Arrival",
        "Customer Sales",
        "Physical Count Adjustment",
        "Internal Use/Consumption",
        "Supplier Return",
        "Damaged/Defective Items",
        "Transfer Between Locations",
        "Promotional/Sample Use",
        "Emergency Withdrawal",
        "Seasonal Stock Preparation",
        "Expired Items Disposal",
        "Project Allocation",
        "Packaging Change",
        "Quality Control Rejection",
        "Overstock Reduction",
        "Found Miscounted Items",
        "Stock Rotation",
        "Bulk Order Fulfillment",
        "Maintenance/Repair Use"
    };

            // Add reasons to combobox
            foreach (string reason in reasons)
            {
                ReasonComboBoxAdjustStock.Items.Add(reason);
            }

            // Set the DropDown height to show exactly 5 items
            ReasonComboBoxAdjustStock.MaxDropDownItems = 5;

            // Alternatively, you can set the DropDown height directly in pixels
            // This ensures exactly 5 items are visible with proper scrolling
            ReasonComboBoxAdjustStock.DropDownHeight = ReasonComboBoxAdjustStock.ItemHeight * 5;

            // Set default selection to the placeholder
            ReasonComboBoxAdjustStock.SelectedIndex = 0;
        }

        // Method to populate the popup with product data
        public void PopulateProductData(string productName, string sku, string brand, int stock, string imagePath)
        {
            currentProductName = productName;
            currentSKU = sku;
            currentBrand = brand;
            currentStock = stock;
            currentImagePath = imagePath;

            // Update the display
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            // Format: "Plywood - 1/2" (12mm)"
            ItemNameAdjustStock.Text = currentProductName;

            // Format: "SKU: PLW-WDS-001"
            SKUAdjustStock.Text = $"SKU: {currentSKU}";

            // Format: "Brand: VineWood"
            brandAdjustStock.Text = $"Brand: {currentBrand}";

            // Format: "Current Stock: 45 pcs"
            currentStockAdjustStock.Text = $"Current Stock: {currentStock} pcs";

            // Format: "Current Stock: 45"
            currentStockDisplayAdjustStock.Text = currentStock.ToString();

            // Load product image
            LoadProductImage();

            // Reset adjustment
            adjustmentValue = 0;
            newTotalStock = currentStock;
            UpdateAdjustmentDisplay();

            // Reset reason selection
            ReasonComboBoxAdjustStock.SelectedIndex = 0;
        }

        private void LoadProductImage()
        {
            try
            {
                // Clear previous image
                if (ItemPictureBoxAdjustStock.Image != null)
                {
                    ItemPictureBoxAdjustStock.Image.Dispose();
                    ItemPictureBoxAdjustStock.Image = null;
                }

                // Load new image
                Image productImage = ProductImageManager.GetProductImage(currentImagePath);

                // Replace the image content completely
                ItemPictureBoxAdjustStock.Image = productImage;

                // Force refresh to ensure proper display
                ItemPictureBoxAdjustStock.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product image: {ex.Message}");
                // Set default image
                SetDefaultImage();
            }
        }

        private void SetDefaultImage()
        {
            // Create a default "No Image" placeholder
            Bitmap defaultImage = new Bitmap(ItemPictureBoxAdjustStock.Width, ItemPictureBoxAdjustStock.Height);
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
            ItemPictureBoxAdjustStock.Image = defaultImage;
        }

        private void ItemNameAdjustStock_Click(object sender, EventArgs e)
        {
            // Already handled in PopulateProductData
        }

        private void SKUAdjustStock_Click(object sender, EventArgs e)
        {
            // Already handled in PopulateProductData
        }

        private void brandAdjustStock_Click(object sender, EventArgs e)
        {
            // Already handled in PopulateProductData
        }

        private void currentStockAdjustStock_Click(object sender, EventArgs e)
        {
            // Already handled in PopulateProductData
        }

        private void ItemPictureBoxAdjustStock_Click(object sender, EventArgs e)
        {
            // Image display handled in LoadProductImage
        }

        private void DisplayNumAdjustStock_TextChanged(object sender, EventArgs e)
        {
            // Parse the adjustment value
            if (int.TryParse(DisplayNumAdjustStock.Text, out int adjustment))
            {
                adjustmentValue = adjustment;
                newTotalStock = currentStock + adjustmentValue;
                UpdateAdjustmentDisplay();
            }
            else if (string.IsNullOrEmpty(DisplayNumAdjustStock.Text))
            {
                adjustmentValue = 0;
                newTotalStock = currentStock;
                UpdateAdjustmentDisplay();
            }
        }

        private void UpdateAdjustmentDisplay()
        {
            // Update adjustment display - it will stay at position 400, 52
            AdjustmentDisplayAdjustStock.Text = adjustmentValue.ToString();

            // Update new total stock display
            NewTotalStockDisplayAdjustStock.Text = newTotalStock.ToString();

            // Validate if new total is negative
            if (newTotalStock < 0)
            {
                NewTotalStockDisplayAdjustStock.ForeColor = Color.Red;
                ApplyBtn.Enabled = false;
            }
            else
            {
                NewTotalStockDisplayAdjustStock.ForeColor = Color.Black;
                ApplyBtn.Enabled = true;
            }
        }

        private void AddAdjustStock_Click(object sender, EventArgs e)
        {
            // Increase adjustment by 1
            adjustmentValue++;
            newTotalStock = currentStock + adjustmentValue;
            DisplayNumAdjustStock.Text = adjustmentValue.ToString();
            UpdateAdjustmentDisplay();
        }

        private void MinusAdjustStock_Click(object sender, EventArgs e)
        {
            // Decrease adjustment by 1
            adjustmentValue--;
            newTotalStock = currentStock + adjustmentValue;
            DisplayNumAdjustStock.Text = adjustmentValue.ToString();
            UpdateAdjustmentDisplay();
        }

        private void ReasonComboBoxAdjustStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable/disable Apply button based on reason selection
            if (ReasonComboBoxAdjustStock.SelectedIndex == 0) // "Select Reason" placeholder
            {
                ApplyBtn.Enabled = false;
            }
            else
            {
                ApplyBtn.Enabled = newTotalStock >= 0; // Only enable if stock is not negative
            }
        }

        private void currentStockDisplayAdjustStock_Click(object sender, EventArgs e)
        {
            // Display current stock - already handled
        }

        private void AdjustmentDisplayAdjustStock_Click(object sender, EventArgs e)
        {
            // Display adjustment - already handled
        }

        private void NewTotalStockDisplayAdjustStock_Click(object sender, EventArgs e)
        {
            // Display new total - already handled
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            // Go back to main page
            Cancelled?.Invoke(this, EventArgs.Empty);
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            // Validate reason selection
            if (ReasonComboBoxAdjustStock.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a reason for the stock adjustment.", "Reason Required",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ReasonComboBoxAdjustStock.Focus();
                return;
            }

            // Validate stock doesn't go negative
            if (newTotalStock < 0)
            {
                MessageBox.Show("Stock cannot be negative. Please adjust the quantity.", "Invalid Stock",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get the selected reason (without emoji for database storage)
                string selectedReason = ReasonComboBoxAdjustStock.SelectedItem.ToString();
                string cleanReason = RemoveEmojis(selectedReason).Trim();

                // Update stock in database
                bool success = UpdateStockInDatabase(currentProductName, newTotalStock, cleanReason);

                if (success)
                {
                    TryLogInventoryAdjustment(cleanReason);

                    MessageBox.Show($"Stock adjusted successfully!\nReason: {cleanReason}", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Trigger event to refresh main page
                    StockAdjusted?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Failed to adjust stock. Please try again.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adjusting stock: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string RemoveEmojis(string text)
        {
            // Remove emojis for database storage
            return System.Text.RegularExpressions.Regex.Replace(text, @"[^\u0000-\u007F]+", "");
        }

        private bool UpdateStockInDatabase(string productName, int newStock, string reason)
        {
            using (var connection = new SqlConnection(ConnectionString.DataSource))
            {
                connection.Open();

                // Update stock and log the reason (you might want to create a separate stock adjustment log table)
                string query = @"UPDATE Products SET current_stock = @newStock WHERE product_name = @productName";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@newStock", newStock);
                    cmd.Parameters.AddWithValue("@productName", productName);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    // You could also log this adjustment to a separate table here
                    if (rowsAffected > 0)
                    {
                        LogStockAdjustment(productName, newStock, reason);
                    }

                    return rowsAffected > 0;
                }
            }
        }

        private void TryLogInventoryAdjustment(string reason)
        {
            try
            {
                string oldValues = $"Stock={currentStock}";
                string newValues = $"Stock={newTotalStock}; Reason={reason}";

                string recordKey = !string.IsNullOrWhiteSpace(currentProductId) ? currentProductId : currentSKU;

                AuditHelper.LogWithDetails(
                    AuditModule.INVENTORY,
                    $"Adjusted stock for {currentProductName}",
                    AuditActivityType.UPDATE,
                    tableAffected: "Products",
                    recordId: recordKey,
                    oldValues: oldValues,
                    newValues: newValues
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Inventory stock audit failed: {ex.Message}");
            }
        }

        private void LogStockAdjustment(string productName, int newStock, string reason)
        {
            // Optional: Log stock adjustments to a separate table for audit trail
            try
            {
                using (var connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();
                    string query = @"
                        INSERT INTO StockAdjustmentLog 
                        (product_name, new_stock_quantity, adjustment_reason, adjusted_by, adjustment_date) 
                        VALUES 
                        (@productName, @newStock, @reason, @adjustedBy, @adjustmentDate)";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@productName", productName);
                        cmd.Parameters.AddWithValue("@newStock", newStock);
                        cmd.Parameters.AddWithValue("@reason", reason);
                        cmd.Parameters.AddWithValue("@adjustedBy", Environment.UserName); // or get from user session
                        cmd.Parameters.AddWithValue("@adjustmentDate", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Don't fail the main operation if logging fails, just continue
                System.Diagnostics.Debug.WriteLine($"Failed to log stock adjustment: {ex.Message}");
            }
        }

        // Method to show the popup (call this from main page)
        public void ShowAdjustStock(string productName, string sku, string brand, int stock, string imagePath, string productId)
        {
            currentProductId = productId;
            PopulateProductData(productName, sku, brand, stock, imagePath);
            this.Visible = true;
            this.BringToFront();
        }

        // Method to hide the popup
        public void HideAdjustStock()
        {
            this.Visible = false;
        }

        // Clean up resources when control is disposed
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (ItemPictureBoxAdjustStock.Image != null)
                {
                    ItemPictureBoxAdjustStock.Image.Dispose();
                    ItemPictureBoxAdjustStock.Image = null;
                }
            }
            base.Dispose(disposing);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}