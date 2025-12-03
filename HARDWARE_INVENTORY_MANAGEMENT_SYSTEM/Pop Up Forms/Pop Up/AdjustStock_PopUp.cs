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
        private int currentProductInternalId;
        private string currentProductId;
        private string currentProductName;
        private string currentSKU;
        private string currentBrand;
        private int currentStock;
        private string currentImagePath;

        private int adjustmentValue;
        private int newTotalStock;
        private bool suppressTextChange;

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

        #region Public API

        public void ShowAdjustStock(
            string productId,
            string productName,
            string sku,
            string brand,
            int stock,
            string imagePath)
        {
            int productInternalId;
            TryLoadProductContext(productId, out productInternalId);

            ShowAdjustStock(productInternalId, productId, productName, sku, brand, stock, imagePath);
        }

        #region Public API

        public void ShowAdjustStock(
            int productInternalId,
            string productId,
            string productName,
            string sku,
            string brand,
            int stock,
            string imagePath)
        {
            ApplyProductContext(productInternalId, productId, productName, sku, brand, stock, imagePath);

            Visible = true;
            BringToFront();
            Focus();
        }

        public void HideAdjustStock()
        {
            Visible = false;
        }

        #endregion

        #region Initialization

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
            var reasons = new List<string>
            {
                "Select Reason",
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

            ReasonComboBoxAdjustStock.Items.Clear();
            ReasonComboBoxAdjustStock.Items.AddRange(reasons.ToArray());
            ReasonComboBoxAdjustStock.MaxDropDownItems = 5;
            ReasonComboBoxAdjustStock.DropDownHeight = ReasonComboBoxAdjustStock.ItemHeight * 5;
            ReasonComboBoxAdjustStock.SelectedIndex = 0;
        }

        private void ApplyProductContext(
            int productInternalId,
            string productId,
            string productName,
            string sku,
            string brand,
            int stock,
            string imagePath)
        {
            currentProductInternalId = productInternalId;
            currentProductId = productId ?? string.Empty;
            currentProductName = productName ?? string.Empty;
            currentSKU = sku ?? string.Empty;
            currentBrand = brand ?? string.Empty;
            currentStock = stock;
            currentImagePath = imagePath;

            UpdateStaticDisplay();
            LoadProductImage();
            ResetAdjustment();
        }

        private bool TryLoadProductContext(string productId, out int productInternalId)
        {
            productInternalId = 0;

            if (string.IsNullOrWhiteSpace(productId))
            {
                return false;
            }

            using (var connection = new SqlConnection(ConnectionString.DataSource))
            using (var command = new SqlCommand(
                "SELECT ProductInternalID FROM Products WHERE ProductID = @productId",
                connection))
            {
                command.Parameters.AddWithValue("@productId", productId);

                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out productInternalId))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region UI Updates

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
                    SetPictureBoxImage(Image.FromFile(currentImagePath));
                }
                else
                {
                    SetPictureBoxImage(ProductImageManager.GetProductImage(currentImagePath));
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
            using (var defaultImage = new Bitmap(ItemPictureBoxAdjustStock.Width, ItemPictureBoxAdjustStock.Height))
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
                SetPictureBoxImage((Image)defaultImage.Clone());
            }
        }

        private void ResetAdjustment()
        {
            suppressTextChange = true;
            adjustmentValue = 0;
            newTotalStock = currentStock;
            DisplayNumAdjustStock.Text = "0";
            suppressTextChange = false;
            UpdateTotalsDisplay();
            ApplyBtn.Enabled = false;
            ReasonComboBoxAdjustStock.SelectedIndex = 0;
        }

        private void UpdateTotalsDisplay()
        {
            AdjustmentDisplayAdjustStock.Text = adjustmentValue.ToString();
            NewTotalStockDisplayAdjustStock.Text = newTotalStock.ToString();
            NewTotalStockDisplayAdjustStock.ForeColor = newTotalStock < 0 ? Color.Red : Color.Black;
            currentStockDisplayAdjustStock.Text = currentStock.ToString();
            ApplyBtn.Enabled = ReasonSelected() && newTotalStock >= 0;
        }

        #endregion

        #region Event Handlers

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
            if (suppressTextChange)
            {
                return;
            }

            if (int.TryParse(DisplayNumAdjustStock.Text, out int parsedValue))
            {
                adjustmentValue = parsedValue;
            }
            else if (string.IsNullOrWhiteSpace(DisplayNumAdjustStock.Text))
            {
                adjustmentValue = 0;
            }

            RecalculateNewTotal();
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
                    StockAdjusted?.Invoke(this, EventArgs.Empty);
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Calculation Helpers

        private void RecalculateNewTotal()
        {
            newTotalStock = currentStock + adjustmentValue;
            UpdateTotalsDisplay();
        }

        private bool ReasonSelected()
        {
            return ReasonComboBoxAdjustStock.SelectedIndex > 0;
        }

        private string RemoveNonAscii(string text)
        {
            return System.Text.RegularExpressions.Regex.Replace(text, "[^\\u0000-\\u007F]+", string.Empty);
        }

        #endregion

        #region Data Operations

        private bool UpdateStockInDatabase(string reason)
        {
            using (var connection = new SqlConnection(ConnectionString.DataSource))
            using (var command = new SqlCommand(
                "UPDATE Products SET current_stock = @newStock WHERE ProductInternalID = @productInternalId OR ProductID = @productId",
                connection))
            {
                command.Parameters.AddWithValue("@newStock", newTotalStock);
                command.Parameters.AddWithValue("@productInternalId", currentProductInternalId);
                command.Parameters.AddWithValue("@productId", currentProductId);

                connection.Open();
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
            try
            {
                using (var connection = new SqlConnection(ConnectionString.DataSource))
                using (var command = new SqlCommand(
                    "INSERT INTO StockAdjustmentLog (product_name, new_stock_quantity, adjustment_reason, adjusted_by, adjustment_date) " +
                    "VALUES (@productName, @newStock, @reason, @adjustedBy, @adjustmentDate)",
                    connection))
                {
                    command.Parameters.AddWithValue("@productName", currentProductName);
                    command.Parameters.AddWithValue("@newStock", newTotalStock);
                    command.Parameters.AddWithValue("@reason", reason);
                    command.Parameters.AddWithValue("@adjustedBy", Environment.UserName);
                    command.Parameters.AddWithValue("@adjustmentDate", DateTime.Now);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                // Optional logging failure should not prevent main update
            }
        }

        #endregion

        #region Disposal

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

        #endregion
    }
}
