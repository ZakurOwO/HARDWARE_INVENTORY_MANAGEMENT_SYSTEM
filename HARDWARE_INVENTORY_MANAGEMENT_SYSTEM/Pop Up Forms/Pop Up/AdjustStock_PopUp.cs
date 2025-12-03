using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class AdjustStock_PopUp : UserControl
    {
        private string currentProductId;
        private string currentProductName;
        private string currentSKU;
        private string currentBrand;
        private int currentStock;
        private string currentImagePath;
        private int adjustmentValue;
        private int newTotalStock;

        public event EventHandler StockAdjusted;
        public event EventHandler Cancelled;

        public AdjustStock_PopUp()
        {
            InitializeComponent();
            SetupPictureBox();
            InitializeReasonComboBox();
            WireUpEvents();
            ResetAdjustment();
        }

        public void ShowAdjustStock(string productId, string productName, string sku, string brand, int stock, string imagePath)
        {
            currentProductId = productId;
            currentProductName = productName;
            currentSKU = sku;
            currentBrand = brand;
            currentStock = stock;
            currentImagePath = imagePath;

            UpdateStaticDisplay();
            LoadProductImage();
            ResetAdjustment();

            Visible = true;
            BringToFront();
        }

        public void HideAdjustStock()
        {
            Visible = false;
        }

        private void SetupPictureBox()
        {
            ItemPictureBoxAdjustStock.SizeMode = PictureBoxSizeMode.StretchImage;
            ItemPictureBoxAdjustStock.BorderStyle = BorderStyle.FixedSingle;
        }

        private void WireUpEvents()
        {
            guna2Button3.Click += CloseButton_Click;
            CancelBtn.Click += CloseButton_Click;
            ApplyBtn.Click += ApplyBtn_Click;
            AddAdjustStock.Click += AddAdjustStock_Click;
            MinusAdjustStock.Click += MinusAdjustStock_Click;
            DisplayNumAdjustStock.TextChanged += DisplayNumAdjustStock_TextChanged;
            ReasonComboBoxAdjustStock.SelectedIndexChanged += ReasonComboBoxAdjustStock_SelectedIndexChanged;
        }

        private void InitializeReasonComboBox()
        {
            ReasonComboBoxAdjustStock.Items.Clear();
            ReasonComboBoxAdjustStock.Items.Add("Select Reason");

            List<string> reasons = new List<string>();
            reasons.Add("DEFAULT");
            reasons.Add("New Stock Arrival");
            reasons.Add("Customer Sales");
            reasons.Add("Physical Count Adjustment");
            reasons.Add("Internal Use/Consumption");
            reasons.Add("Supplier Return");
            reasons.Add("Damaged/Defective Items");
            reasons.Add("Transfer Between Locations");
            reasons.Add("Promotional/Sample Use");
            reasons.Add("Emergency Withdrawal");
            reasons.Add("Seasonal Stock Preparation");
            reasons.Add("Expired Items Disposal");
            reasons.Add("Project Allocation");
            reasons.Add("Packaging Change");
            reasons.Add("Quality Control Rejection");
            reasons.Add("Overstock Reduction");
            reasons.Add("Found Miscounted Items");
            reasons.Add("Stock Rotation");
            reasons.Add("Bulk Order Fulfillment");
            reasons.Add("Maintenance/Repair Use");

            foreach (string reason in reasons)
            {
                ReasonComboBoxAdjustStock.Items.Add(reason);
            }

            ReasonComboBoxAdjustStock.MaxDropDownItems = 5;
            ReasonComboBoxAdjustStock.DropDownHeight = ReasonComboBoxAdjustStock.ItemHeight * 5;
            ReasonComboBoxAdjustStock.SelectedIndex = 0;
        }

        private void UpdateStaticDisplay()
        {
            ItemNameAdjustStock.Text = currentProductName;
            SKUAdjustStock.Text = currentSKU;
            brandAdjustStock.Text = currentBrand;
            currentStockDisplayAdjustStock.Text = currentStock.ToString();
        }

        private void LoadProductImage()
        {
            try
            {
                if (!string.IsNullOrEmpty(currentImagePath) && File.Exists(currentImagePath))
                {
                    Image productImage = Image.FromFile(currentImagePath);
                    SetPictureBoxImage(productImage);
                }
                else
                {
                    Image productImage = ProductImageManager.GetProductImage(currentImagePath);
                    SetPictureBoxImage(productImage);
                }
            }
            catch
            {
                SetDefaultImage();
            }
        }

        private void SetPictureBoxImage(Image image)
        {
            if (ItemPictureBoxAdjustStock.Image != null)
            {
                ItemPictureBoxAdjustStock.Image.Dispose();
            }

            ItemPictureBoxAdjustStock.Image = image;
            ItemPictureBoxAdjustStock.Refresh();
        }

        private void SetDefaultImage()
        {
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

            SetPictureBoxImage(defaultImage);
        }

        private void ResetAdjustment()
        {
            adjustmentValue = 0;
            newTotalStock = currentStock;
            DisplayNumAdjustStock.Text = "0";
            UpdateTotalsDisplay();
            ApplyBtn.Enabled = false;
            ReasonComboBoxAdjustStock.SelectedIndex = 0;
        }

        private void AddAdjustStock_Click(object sender, EventArgs e)
        {
            adjustmentValue++;
            RecalculateNewTotal();
        }

        private void MinusAdjustStock_Click(object sender, EventArgs e)
        {
            adjustmentValue--;
            RecalculateNewTotal();
        }

        private void DisplayNumAdjustStock_TextChanged(object sender, EventArgs e)
        {
            int parsedValue;
            if (int.TryParse(DisplayNumAdjustStock.Text, out parsedValue))
            {
                adjustmentValue = parsedValue;
            }
            else if (string.IsNullOrWhiteSpace(DisplayNumAdjustStock.Text))
            {
                adjustmentValue = 0;
            }

            RecalculateNewTotal();
        }

        private void RecalculateNewTotal()
        {
            newTotalStock = currentStock + adjustmentValue;
            UpdateTotalsDisplay();
        }

        private void UpdateTotalsDisplay()
        {
            AdjustmentDisplayAdjustStock.Text = adjustmentValue.ToString();
            NewTotalStockDisplayAdjustStock.Text = newTotalStock.ToString();

            if (newTotalStock < 0)
            {
                NewTotalStockDisplayAdjustStock.ForeColor = Color.Red;
            }
            else
            {
                NewTotalStockDisplayAdjustStock.ForeColor = Color.Black;
            }

            currentStockDisplayAdjustStock.Text = currentStock.ToString();
            ApplyBtn.Enabled = ReasonSelected() && newTotalStock >= 0;
        }

        private bool ReasonSelected()
        {
            return ReasonComboBoxAdjustStock.SelectedIndex > 0;
        }

        private void ReasonComboBoxAdjustStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyBtn.Enabled = ReasonSelected() && newTotalStock >= 0;
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            if (!ReasonSelected())
            {
                MessageBox.Show("Please select a reason for the stock adjustment.", "Reason Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newTotalStock < 0)
            {
                MessageBox.Show("Stock cannot be negative. Please adjust the quantity.", "Invalid Stock",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string reason = ReasonComboBoxAdjustStock.SelectedItem.ToString();
                string cleanReason = RemoveNonAscii(reason).Trim();
                int previousStock = currentStock;

                bool success = UpdateStockInDatabase(cleanReason);
                if (success)
                {
                    LogInventoryAudit(cleanReason, previousStock);
                    MessageBox.Show("Stock adjusted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (StockAdjusted != null)
                    {
                        StockAdjusted(this, EventArgs.Empty);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to adjust stock. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adjusting stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string RemoveNonAscii(string text)
        {
            return System.Text.RegularExpressions.Regex.Replace(text, "[^\\u0000-\\u007F]+", string.Empty);
        }

        private bool UpdateStockInDatabase(string reason)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(ConnectionString.DataSource);
                connection.Open();

                const string query = "UPDATE Products SET current_stock = @newStock WHERE ProductID = @productId";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@newStock", newTotalStock);
                command.Parameters.AddWithValue("@productId", currentProductId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    LogStockAdjustment(reason);
                    currentStock = newTotalStock;
                    UpdateStaticDisplay();
                    return true;
                }

                return false;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }

                if (connection != null)
                {
                    connection.Dispose();
                }
            }
        }

        private void LogInventoryAudit(string reason, int previousStock)
        {
            try
            {
                string oldValues = "Stock=" + previousStock;
                string newValues = "Stock=" + newTotalStock + "; Reason=" + reason;

                AuditHelper.LogWithDetails(
                    AuditModule.INVENTORY,
                    "Adjusted stock for " + currentProductName,
                    AuditActivityType.UPDATE,
                    "Products",
                    currentProductId,
                    oldValues,
                    newValues);
            }
            catch
            {
                // Audit failures should not break the flow
            }
        }

        private void LogStockAdjustment(string reason)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(ConnectionString.DataSource);
                connection.Open();

                const string query = "INSERT INTO StockAdjustmentLog (product_name, new_stock_quantity, adjustment_reason, adjusted_by, adjustment_date) VALUES (@productName, @newStock, @reason, @adjustedBy, @adjustmentDate)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@productName", currentProductName);
                command.Parameters.AddWithValue("@newStock", newTotalStock);
                command.Parameters.AddWithValue("@reason", reason);
                command.Parameters.AddWithValue("@adjustedBy", Environment.UserName);
                command.Parameters.AddWithValue("@adjustmentDate", DateTime.Now);

                command.ExecuteNonQuery();
            }
            catch
            {
                // Optional logging failure should not prevent main update
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }

                if (connection != null)
                {
                    connection.Dispose();
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (Cancelled != null)
            {
                Cancelled(this, EventArgs.Empty);
            }
        }

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
    }
}
