// ==================================================================
// COMPLETE AdjustStock_PopUp.cs FILE
// This replaces your entire AdjustStock_PopUp.cs file
// ==================================================================

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

            this.Visible = true;
            this.BringToFront();
            this.Focus();
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

            // Make the textbox read-only so users can only use +/- buttons
            DisplayNumAdjustStock.ReadOnly = true;
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
            // Update the adjustment display (can be positive or negative)
            AdjustmentDisplayAdjustStock.Text = adjustmentValue.ToString();

            // Color the adjustment based on whether it's adding or removing stock
            if (adjustmentValue > 0)
            {
                AdjustmentDisplayAdjustStock.ForeColor = Color.Green;
            }
            else if (adjustmentValue < 0)
            {
                AdjustmentDisplayAdjustStock.ForeColor = Color.Red;
            }
            else
            {
                AdjustmentDisplayAdjustStock.ForeColor = Color.Black;
            }

            // Update new total stock
            NewTotalStockDisplayAdjustStock.Text = newTotalStock.ToString();
            NewTotalStockDisplayAdjustStock.ForeColor = newTotalStock < 0 ? Color.Red : Color.Black;

            // Update current stock display
            currentStockDisplayAdjustStock.Text = currentStock.ToString();

            // Enable Apply button only if reason is selected and new stock is valid
            ApplyBtn.Enabled = ReasonSelected() && newTotalStock >= 0;
        }

        #endregion

        #region Event Handlers

        private void AddAdjustStock_Click(object sender, EventArgs e)
        {
            // Only increment by exactly 1
            adjustmentValue = adjustmentValue + 1;

            // Update the display
            suppressTextChange = true;
            DisplayNumAdjustStock.Text = adjustmentValue.ToString();
            suppressTextChange = false;

            // Recalculate totals
            RecalculateNewTotal();
        }

        private void MinusAdjustStock_Click(object sender, EventArgs e)
        {
            // Only decrement by exactly 1
            adjustmentValue = adjustmentValue - 1;

            // Update the display
            suppressTextChange = true;
            DisplayNumAdjustStock.Text = adjustmentValue.ToString();
            suppressTextChange = false;

            // Recalculate totals
            RecalculateNewTotal();
        }

        private void DisplayNumAdjustStock_TextChanged(object sender, EventArgs e)
        {
            if (suppressTextChange)
            {
                return;
            }

            int parsedValue;
            if (int.TryParse(DisplayNumAdjustStock.Text, out parsedValue))
            {
                adjustmentValue = parsedValue;
                RecalculateNewTotal();
            }
            else
            {
                suppressTextChange = true;
                DisplayNumAdjustStock.Text = adjustmentValue.ToString();
                suppressTextChange = false;
            }
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

            if (adjustmentValue == 0)
            {
                MessageBox.Show("Please enter an adjustment value.", "No Adjustment",
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
                {
                    connection.Open();

                    // First, check if StockAdjustmentLog table exists
                    string checkTableQuery = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'StockAdjustmentLog')
                        BEGIN
                            CREATE TABLE StockAdjustmentLog (
                                log_id INT IDENTITY(1,1) PRIMARY KEY,
                                product_name VARCHAR(255) NOT NULL,
                                previous_stock INT NOT NULL,
                                adjustment_amount INT NOT NULL,
                                new_stock_quantity INT NOT NULL,
                                adjustment_reason VARCHAR(255),
                                adjusted_by VARCHAR(100),
                                adjustment_date DATETIME DEFAULT GETDATE()
                            )
                        END";

                    using (var checkCommand = new SqlCommand(checkTableQuery, connection))
                    {
                        checkCommand.ExecuteNonQuery();
                    }

                    // Now insert the log entry
                    string insertQuery = @"
                        INSERT INTO StockAdjustmentLog 
                        (product_name, previous_stock, adjustment_amount, new_stock_quantity, adjustment_reason, adjusted_by, adjustment_date) 
                        VALUES 
                        (@productName, @previousStock, @adjustmentAmount, @newStock, @reason, @adjustedBy, @adjustmentDate)";

                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@productName", currentProductName);
                        command.Parameters.AddWithValue("@previousStock", currentStock - adjustmentValue);
                        command.Parameters.AddWithValue("@adjustmentAmount", adjustmentValue);
                        command.Parameters.AddWithValue("@newStock", newTotalStock);
                        command.Parameters.AddWithValue("@reason", reason);
                        command.Parameters.AddWithValue("@adjustedBy", Environment.UserName);
                        command.Parameters.AddWithValue("@adjustmentDate", DateTime.Now);

                        command.ExecuteNonQuery();
                    }
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