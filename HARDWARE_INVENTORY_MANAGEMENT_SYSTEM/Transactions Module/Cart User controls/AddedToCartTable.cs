using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class AddedToCartTable : UserControl
    {
        NumericUpDown qtyUpDown = new NumericUpDown();
        private string connectionString;

        public AddedToCartTable()
        {
            InitializeComponent();
            connectionString = ConnectionString.DataSource;
            InitializeDataGridViewColumns();
        }

        private void InitializeDataGridViewColumns()
        {
            // Clear existing columns
            dgvCartDetails.Columns.Clear();

            // Item Name Column
            DataGridViewTextBoxColumn itemNameColumn = new DataGridViewTextBoxColumn();
            itemNameColumn.Name = "ItemName";
            itemNameColumn.HeaderText = "ITEM";
            itemNameColumn.DataPropertyName = "ItemName";
            itemNameColumn.Width = 200;
            itemNameColumn.ReadOnly = true;
            dgvCartDetails.Columns.Add(itemNameColumn);

            // Quantity Column
            DataGridViewTextBoxColumn quantityColumn = new DataGridViewTextBoxColumn();
            quantityColumn.Name = "Quantity";
            quantityColumn.HeaderText = "QTY";
            quantityColumn.DataPropertyName = "Quantity";
            quantityColumn.Width = 80;
            quantityColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCartDetails.Columns.Add(quantityColumn);

            // Price Column
            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();
            priceColumn.Name = "Price";
            priceColumn.HeaderText = "PRICE";
            priceColumn.DataPropertyName = "Price";
            priceColumn.Width = 100;
            priceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            priceColumn.ReadOnly = true;
            dgvCartDetails.Columns.Add(priceColumn);

            // Delete Column
            DataGridViewImageColumn deleteColumn = new DataGridViewImageColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "";
            deleteColumn.Width = 42;
            deleteColumn.Image = Properties.Resources.trash;
            deleteColumn.DefaultCellStyle.NullValue = null;
            deleteColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgvCartDetails.Columns.Add(deleteColumn);

            // Style the DataGridView
            dgvCartDetails.AutoGenerateColumns = false;
            dgvCartDetails.AllowUserToAddRows = false;
            dgvCartDetails.RowHeadersVisible = false;
            dgvCartDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void AddedToCartTable_Load(object sender, EventArgs e)
        {
            // Start with empty cart - don't auto-load products
            dgvCartDetails.ClearSelection();

            // Setup NumericUpDown for quantity editing
            SetupNumericUpDown();
        }

        private void SetupNumericUpDown()
        {
            qtyUpDown.Minimum = 1;
            qtyUpDown.Maximum = 1000;
            qtyUpDown.Visible = false;

            // Style
            qtyUpDown.BorderStyle = BorderStyle.None;
            qtyUpDown.TextAlign = HorizontalAlignment.Center;
            qtyUpDown.Font = dgvCartDetails.DefaultCellStyle.Font;
            qtyUpDown.BackColor = dgvCartDetails.DefaultCellStyle.BackColor;
            qtyUpDown.ForeColor = dgvCartDetails.DefaultCellStyle.ForeColor;

            // Ensure manual sizing and positioning
            qtyUpDown.AutoSize = false;
            dgvCartDetails.Controls.Add(qtyUpDown);

            // Event handlers
            qtyUpDown.Leave += qtyUpDown_Leave;
            qtyUpDown.ValueChanged += qtyUpDown_ValueChanged;
            qtyUpDown.KeyDown += QtyUpDown_KeyDown;
        }

        private void dgvCartDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Only show NumericUpDown for the QTY column
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvCartDetails.Columns["Quantity"].Index)
            {
                ShowNumericUpDown(e.RowIndex, e.ColumnIndex);
            }
            else
            {
                qtyUpDown.Visible = false;
            }
        }

        private void ShowNumericUpDown(int rowIndex, int columnIndex)
        {
            Rectangle rect = dgvCartDetails.GetCellDisplayRectangle(columnIndex, rowIndex, true);

            // Calculate perfect fit
            int cellHeight = rect.Height;
            qtyUpDown.Height = cellHeight - 2;
            qtyUpDown.Width = rect.Width - 4;

            qtyUpDown.Location = new Point(rect.X + 2, rect.Y + (rect.Height - qtyUpDown.Height) / 2);

            // Set current value
            if (dgvCartDetails.Rows[rowIndex].Cells[columnIndex].Value != null)
            {
                qtyUpDown.Value = Convert.ToDecimal(dgvCartDetails.Rows[rowIndex].Cells[columnIndex].Value);
            }
            else
            {
                qtyUpDown.Value = qtyUpDown.Minimum;
            }

            qtyUpDown.Tag = new Point(rowIndex, columnIndex);
            qtyUpDown.Visible = true;
            qtyUpDown.BringToFront();
            qtyUpDown.Focus();
            qtyUpDown.Select(0, qtyUpDown.Value.ToString().Length);
        }

        private void QtyUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveNumericValue();
                qtyUpDown.Visible = false;
                dgvCartDetails.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                qtyUpDown.Visible = false;
                dgvCartDetails.Focus();
            }
        }

        private void qtyUpDown_Leave(object sender, EventArgs e)
        {
            SaveNumericValue();
        }

        private void qtyUpDown_ValueChanged(object sender, EventArgs e)
        {
            SaveNumericValue();
        }

        private void SaveNumericValue()
        {
            if (qtyUpDown.Tag is Point cellLocation)
            {
                int rowIndex = cellLocation.X;
                int columnIndex = cellLocation.Y;

                if (rowIndex >= 0 && rowIndex < dgvCartDetails.Rows.Count)
                {
                    dgvCartDetails.Rows[rowIndex].Cells[columnIndex].Value = qtyUpDown.Value;
                    UpdateRowTotal(rowIndex);
                }
            }
        }

        private void UpdateRowTotal(int rowIndex)
        {
            // Update row total if needed
            if (rowIndex >= 0 && rowIndex < dgvCartDetails.Rows.Count)
            {
                var row = dgvCartDetails.Rows[rowIndex];
                if (row.Cells["Quantity"].Value != null && row.Cells["Price"].Value != null)
                {
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    string priceText = row.Cells["Price"].Value.ToString().Replace("₱", "").Trim();
                    if (decimal.TryParse(priceText, out decimal price))
                    {
                        decimal total = quantity * price;
                        // You can display this in another column if you add a "Total" column
                    }
                }
            }
        }
         void UpdateTotals()
        {
            decimal subtotal = CalculateSubtotal();
            decimal tax = CalculateTax(subtotal);
            decimal total = subtotal + tax;

        }

        private void DebugCartContents()
        {
            Console.WriteLine($"=== CART DEBUG INFO ===");
            Console.WriteLine($"Total rows in DataGridView: {dgvCartDetails.Rows.Count}");
            Console.WriteLine($"Cart item count (GetCartItemCount): {GetCartItemCount()}");

            int visibleRows = 0;
            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (!row.IsNewRow)
                {
                    visibleRows++;
                    Console.WriteLine($"Row {visibleRows}: {row.Cells["ItemName"]?.Value}, " +
                                    $"Qty: {row.Cells["Quantity"]?.Value}, " +
                                    $"Price: {row.Cells["Price"]?.Value}");
                }
            }
            Console.WriteLine($"Visible rows: {visibleRows}");
            Console.WriteLine($"=========================");
        }

        private void dgvCartDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCartDetails.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to remove this item from the cart?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dgvCartDetails.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        // Public methods to interact with the cart from other forms

        /// <summary>
        /// Adds a product to the cart from the database
        /// </summary>
       public void AddItemToCart(string itemName, decimal price, int quantity = 1)
{
    Console.WriteLine($"AddItemToCart called: {itemName}, {price}, {quantity}");
    
    // Check if item already exists in cart
    foreach (DataGridViewRow row in dgvCartDetails.Rows)
    {
        if (row.IsNewRow) continue;

        if (row.Cells["ItemName"].Value?.ToString() == itemName)
        {
            // Item exists, update quantity
            int currentQty = Convert.ToInt32(row.Cells["Quantity"].Value);
            row.Cells["Quantity"].Value = currentQty + quantity;
            UpdateTotals();
            Console.WriteLine($"Updated existing item: {itemName}, new quantity: {currentQty + quantity}");
            DebugCartContents(); // Debug
            return;
        }
    }

    // Item doesn't exist, add new row
    Console.WriteLine($"Adding new item to cart: {itemName}");
    dgvCartDetails.Rows.Add(itemName, quantity, $"₱{price:N2}");
    UpdateTotals();
    
    // Force UI refresh
    dgvCartDetails.Refresh();
    DebugCartContents(); // Debug
    
    Console.WriteLine($"Item added successfully. Total items: {GetCartItemCount()}");
}

        /// <summary>
        /// Adds a product to cart by ProductInternalID from database
        /// </summary>
        // In Walk_inCartDetails class
        public void AddProductToCartById(int productInternalId, int quantity = 1)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT 
                    product_name,
                    SellingPrice,
                    current_stock
                FROM Products 
                WHERE ProductInternalID = @ProductInternalID 
                AND active = 1 AND current_stock > 0";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductInternalID", productInternalId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string productName = reader.GetString(0);
                        decimal sellingPrice = reader.GetDecimal(1);
                        int currentStock = reader.GetInt32(2);

                        if (quantity <= currentStock)
                        {
                            AddItemToCart(productName, sellingPrice, quantity);

                            // Optional: Show success message
                            // MessageBox.Show($"Added {productName} to cart!", "Success", 
                            //     MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Insufficient stock for {productName}. Available: {currentStock}",
                                "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Product not found or out of stock.", "Product Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product to cart: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Adds a product to cart by product name from database
        /// </summary>
        public void AddProductToCartByName(string productName, int quantity = 1)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            product_name,
                            SellingPrice,
                            current_stock
                        FROM Products 
                        WHERE product_name = @ProductName 
                        AND active = 1 AND current_stock > 0";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductName", productName);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string name = reader.GetString(0);
                        decimal sellingPrice = reader.GetDecimal(1);
                        int currentStock = reader.GetInt32(2);

                        // Check if requested quantity is available
                        if (quantity <= currentStock)
                        {
                            AddItemToCart(name, sellingPrice, quantity);
                        }
                        else
                        {
                            MessageBox.Show($"Insufficient stock for {name}. Available: {currentStock}",
                                "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Product not found or out of stock.", "Product Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product to cart: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RemoveItemFromCart(string itemName)
        {
            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["ItemName"].Value?.ToString() == itemName)
                {
                    dgvCartDetails.Rows.Remove(row);
                    return;
                }
            }
        }

        public void ClearCart()
        {
            dgvCartDetails.Rows.Clear();
        }

        public decimal CalculateSubtotal()
        {
            decimal subtotal = 0;
            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["Quantity"].Value != null && row.Cells["Price"].Value != null)
                {
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    string priceText = row.Cells["Price"].Value.ToString().Replace("₱", "").Trim();
                    if (decimal.TryParse(priceText, out decimal price))
                    {
                        subtotal += quantity * price;
                    }
                }
            }
            return subtotal;
        }

        public decimal CalculateTax(decimal taxRate = 0.12m)
        {
            decimal subtotal = CalculateSubtotal();
            return subtotal * taxRate;
        }

        public decimal CalculateTotal(decimal taxRate = 0.12m)
        {
            return CalculateSubtotal() + CalculateTax(taxRate);
        }

        public int GetCartItemCount()
        {
            int count = 0;
            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (!row.IsNewRow) count++;
            }
            return count;
        }

        /// <summary>
        /// Gets all cart items as a list for processing orders
        /// </summary>
        public List<CartItem> GetCartItems()
        {
            List<CartItem> items = new List<CartItem>();

            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["ItemName"].Value != null &&
                    row.Cells["Quantity"].Value != null &&
                    row.Cells["Price"].Value != null)
                {
                    string priceText = row.Cells["Price"].Value.ToString().Replace("₱", "").Trim();
                    if (decimal.TryParse(priceText, out decimal price))
                    {
                        items.Add(new CartItem
                        {
                            Name = row.Cells["ItemName"].Value.ToString(),
                            UnitPrice = price,
                            Quantity = Convert.ToInt32(row.Cells["Quantity"].Value)
                        });
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Validates if all items in cart have sufficient stock in database
        /// </summary>
        public bool ValidateStockAvailability()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (DataGridViewRow row in dgvCartDetails.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string productName = row.Cells["ItemName"].Value?.ToString();
                        int cartQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                        if (string.IsNullOrEmpty(productName)) continue;

                        string query = @"
                            SELECT current_stock 
                            FROM Products 
                            WHERE product_name = @ProductName AND active = 1";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ProductName", productName);

                        var result = command.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show($"Product '{productName}' not found in database.",
                                "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        int availableStock = Convert.ToInt32(result);
                        if (cartQuantity > availableStock)
                        {
                            MessageBox.Show($"Insufficient stock for '{productName}'. Available: {availableStock}, Requested: {cartQuantity}",
                                "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validating stock: {ex.Message}", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }

}