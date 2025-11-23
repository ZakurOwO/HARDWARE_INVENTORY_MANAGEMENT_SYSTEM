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

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class Walk_inCartDetails : UserControl
    {
        NumericUpDown qtyUpDown = new NumericUpDown();
        private string connectionString;

        public Walk_inCartDetails()
        {
            InitializeComponent();
            connectionString = ConnectionString.DataSource;
            InitializeDataGridViewColumns();

            // ALIGNMENT: Add padding to center content with more space on sides
            this.Padding = new Padding(20, 10, 20, 10);

            // Initialize customer field
            InitializeCustomerField();
        }

        // Initialize customer input field
        private void InitializeCustomerField()
        {
            // Make sure label1 shows "Customer" text
            if (label1 != null)
            {
                label1.Text = "Customer";
                label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                label1.ForeColor = Color.FromArgb(64, 64, 64);
                label1.Location = new Point(10, 17);
            }

            // Configure the customer textbox (guna2TextBox1)
            if (guna2TextBox1 != null)
            {
                guna2TextBox1.PlaceholderText = "Optional";
                guna2TextBox1.Text = "";
                guna2TextBox1.BorderColor = Color.FromArgb(213, 218, 223);
                guna2TextBox1.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);

                // Add event handler for when text changes
                guna2TextBox1.TextChanged += Guna2TextBox1_TextChanged;
            }
        }

        // Handle customer name text changes
        private void Guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            // You can add validation or formatting here if needed
            // For example, limit characters or format the name
            if (guna2TextBox1.Text.Length > 100)
            {
                guna2TextBox1.Text = guna2TextBox1.Text.Substring(0, 100);
                guna2TextBox1.SelectionStart = guna2TextBox1.Text.Length;
            }
        }

        // Get customer name from the textbox
        public string GetCustomerName()
        {
            if (guna2TextBox1 != null && !string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                return guna2TextBox1.Text.Trim();
            }
            return "Walk-in Customer"; // Default for walk-in
        }

        // Set customer name programmatically
        public void SetCustomerName(string customerName)
        {
            if (guna2TextBox1 != null)
            {
                guna2TextBox1.Text = string.IsNullOrWhiteSpace(customerName) ? "" : customerName;
            }
        }

        // Configure DataGridView columns
        private void InitializeDataGridViewColumns()
        {
            dgvCartDetails.Columns.Clear();

            // ALIGNMENT: Item Name Column - LEFT ALIGNED
            DataGridViewTextBoxColumn itemNameColumn = new DataGridViewTextBoxColumn();
            itemNameColumn.Name = "ItemName";
            itemNameColumn.HeaderText = "ITEM";
            itemNameColumn.DataPropertyName = "ItemName";
            itemNameColumn.Width = 140;
            itemNameColumn.ReadOnly = true;
            itemNameColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCartDetails.Columns.Add(itemNameColumn);

            // ALIGNMENT: Quantity Column - CENTER ALIGNED
            DataGridViewTextBoxColumn quantityColumn = new DataGridViewTextBoxColumn();
            quantityColumn.Name = "Quantity";
            quantityColumn.HeaderText = "QTY";
            quantityColumn.DataPropertyName = "Quantity";
            quantityColumn.Width = 50;
            quantityColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCartDetails.Columns.Add(quantityColumn);

            // ALIGNMENT: Price Column - RIGHT ALIGNED
            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();
            priceColumn.Name = "Price";
            priceColumn.HeaderText = "PRICE";
            priceColumn.DataPropertyName = "Price";
            priceColumn.Width = 90;
            priceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            priceColumn.ReadOnly = true;
            dgvCartDetails.Columns.Add(priceColumn);

            // ALIGNMENT: Delete Column - CENTER ALIGNED
            DataGridViewImageColumn deleteColumn = new DataGridViewImageColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "";
            deleteColumn.Width = 20;
            deleteColumn.Image = Properties.Resources.trash;
            deleteColumn.DefaultCellStyle.NullValue = null;
            deleteColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            deleteColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCartDetails.Columns.Add(deleteColumn);

            // Configure DataGridView settings
            dgvCartDetails.AutoGenerateColumns = false;
            dgvCartDetails.AllowUserToAddRows = false;
            dgvCartDetails.RowHeadersVisible = false;
            dgvCartDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ALIGNMENT: Center the DataGridView headers
            dgvCartDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCartDetails.ColumnHeadersHeight = 35;
        }

        // Initialize cart on load
        private void Walk_inCartDetails_Load(object sender, EventArgs e)
        {
            dgvCartDetails.ClearSelection();
            SetupNumericUpDown();
            UpdateTotals();

            // ALIGNMENT: Configure layout after load
            ConfigureLayout();
        }

        // ALIGNMENT: Configure the overall layout
        private void ConfigureLayout()
        {
            // Center the DataGridView with margins
            if (dgvCartDetails != null)
            {
                dgvCartDetails.Anchor = AnchorStyles.None;
                int gridWidth = 300;
                int centerX = (this.Width - gridWidth) / 2;
                dgvCartDetails.Location = new Point(centerX, dgvCartDetails.Location.Y);
                dgvCartDetails.Width = gridWidth;
            }

            // Adjust buttons to be centered
            AdjustButtonPositions();
        }

        // ALIGNMENT: Center the Clear and Checkout buttons
        private void AdjustButtonPositions()
        {
            // Calculate center position for buttons
            int buttonGap = 15;
            int totalButtonWidth = btnClear.Width + btnCheckout.Width + buttonGap;
            int startX = (this.Width - totalButtonWidth) / 2;

            // Position buttons in center
            btnClear.Location = new Point(startX, btnClear.Location.Y);
            btnCheckout.Location = new Point(startX + btnClear.Width + buttonGap, btnCheckout.Location.Y);

            // Make sure buttons maintain centered position
            btnClear.Anchor = AnchorStyles.Bottom;
            btnCheckout.Anchor = AnchorStyles.Bottom;
        }

        // Configure NumericUpDown control for quantity editing
        private void SetupNumericUpDown()
        {
            qtyUpDown.Minimum = 1;
            qtyUpDown.Maximum = 1000;
            qtyUpDown.Visible = false;
            qtyUpDown.BorderStyle = BorderStyle.None;
            qtyUpDown.TextAlign = HorizontalAlignment.Center;
            qtyUpDown.Font = dgvCartDetails.DefaultCellStyle.Font;
            qtyUpDown.BackColor = dgvCartDetails.DefaultCellStyle.BackColor;
            qtyUpDown.ForeColor = dgvCartDetails.DefaultCellStyle.ForeColor;
            qtyUpDown.AutoSize = false;

            dgvCartDetails.Controls.Add(qtyUpDown);

            qtyUpDown.Leave += qtyUpDown_Leave;
            qtyUpDown.ValueChanged += qtyUpDown_ValueChanged;
            qtyUpDown.KeyDown += QtyUpDown_KeyDown;
        }

        // Show NumericUpDown when quantity cell is clicked
        private void dgvCartDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvCartDetails.Columns["Quantity"].Index)
            {
                ShowNumericUpDown(e.RowIndex, e.ColumnIndex);
            }
            else
            {
                qtyUpDown.Visible = false;
            }
        }

        // Position and display NumericUpDown over quantity cell
        private void ShowNumericUpDown(int rowIndex, int columnIndex)
        {
            Rectangle rect = dgvCartDetails.GetCellDisplayRectangle(columnIndex, rowIndex, true);

            qtyUpDown.Height = rect.Height - 2;
            qtyUpDown.Width = rect.Width - 4;
            qtyUpDown.Location = new Point(rect.X + 2, rect.Y + (rect.Height - qtyUpDown.Height) / 2);

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

        // Handle delete button click
        private void dgvCartDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCartDetails.Columns[e.ColumnIndex].Name == "Delete")
            {
                DeleteCartItem(e.RowIndex);
            }
        }

        // Remove item from cart
        private void DeleteCartItem(int rowIndex)
        {
            if (MessageBox.Show("Are you sure you want to remove this item from the cart?",
                "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dgvCartDetails.Rows.RemoveAt(rowIndex);
                UpdateTotals();
            }
        }

        // Save quantity value when NumericUpDown loses focus
        private void qtyUpDown_Leave(object sender, EventArgs e)
        {
            SaveNumericValue();
        }

        // Update totals when quantity changes
        private void qtyUpDown_ValueChanged(object sender, EventArgs e)
        {
            SaveNumericValue();
        }

        // Handle keyboard shortcuts for NumericUpDown
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

        // Save NumericUpDown value to cell
        private void SaveNumericValue()
        {
            if (qtyUpDown.Tag is Point cellLocation)
            {
                int rowIndex = cellLocation.X;
                int columnIndex = cellLocation.Y;

                if (rowIndex >= 0 && rowIndex < dgvCartDetails.Rows.Count)
                {
                    dgvCartDetails.Rows[rowIndex].Cells[columnIndex].Value = qtyUpDown.Value;
                    UpdateTotals();
                    UpdateRowAppearance(rowIndex);
                }
            }
        }

        // Highlight rows with quantity > 10
        private void UpdateRowAppearance(int rowIndex)
        {
            if (dgvCartDetails.Rows[rowIndex].Cells["Quantity"].Value != null)
            {
                int quantity = Convert.ToInt32(dgvCartDetails.Rows[rowIndex].Cells["Quantity"].Value);
                if (quantity > 10)
                {
                    dgvCartDetails.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else
                {
                    dgvCartDetails.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        // Process checkout for walk-in customer
        private void btnCheckout_Click(object sender, EventArgs e)
        {
            CheckoutWalkIn();
        }

        // Handle walk-in checkout process
        private void CheckoutWalkIn()
        {
            try
            {
                if (dgvCartDetails.Rows.Count == 0)
                {
                    MessageBox.Show("Cart is empty. Please add items to checkout.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateQuantities())
                {
                    return;
                }

                decimal subtotal = CalculateSubtotal();
                decimal tax = CalculateTax(subtotal);
                decimal totalAmount = subtotal + tax;

                if (!ValidateStockAvailability())
                {
                    return;
                }

                // Get customer name
                string customerName = GetCustomerName();

                var result = MessageBox.Show(
                    $"Proceed with checkout?\n\nCustomer: {customerName}\nSubtotal: ₱{subtotal:N2}\nTax: ₱{tax:N2}\nTotal: ₱{totalAmount:N2}",
                    "Confirm Checkout",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveWalkInTransactionToDatabase(customerName, subtotal, tax, totalAmount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during checkout: {ex.Message}", "Checkout Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Validate all quantities are positive
        private bool ValidateQuantities()
        {
            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["Quantity"].Value == null ||
                    !int.TryParse(row.Cells["Quantity"].Value.ToString(), out int quantity) ||
                    quantity <= 0)
                {
                    MessageBox.Show("Please enter valid quantities for all items (minimum 1).",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        // Calculate cart subtotal
        private decimal CalculateSubtotal()
        {
            decimal subtotal = 0;
            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (row.IsNewRow) continue;

                if (decimal.TryParse(row.Cells["Price"].Value?.ToString().Replace("₱", "").Trim(), out decimal price) &&
                    int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int quantity))
                {
                    subtotal += price * quantity;
                }
            }
            return subtotal;
        }

        // Calculate tax (12%)
        private decimal CalculateTax(decimal subtotal)
        {
            return subtotal * 0.12m;
        }

        // Verify stock availability for all cart items
        private bool ValidateStockAvailability()
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

                        if (string.IsNullOrEmpty(productName))
                        {
                            MessageBox.Show("Invalid product name in cart.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

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

        // Save transaction to database with customer name
        private void SaveWalkInTransactionToDatabase(string customerName, decimal subtotal, decimal tax, decimal totalAmount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // 1. Get or create customer
                        int customerId = GetOrCreateCustomer(connection, transaction, customerName);

                        // 2. Create transaction record
                        string insertTransactionQuery = @"
                            INSERT INTO Transactions (transaction_date, customer_id, total_amount, cashier)
                            VALUES (GETDATE(), @CustomerId, @TotalAmount, @Cashier);
                            SELECT CAST(SCOPE_IDENTITY() as int);";

                        SqlCommand transactionCmd = new SqlCommand(insertTransactionQuery, connection, transaction);
                        transactionCmd.Parameters.AddWithValue("@CustomerId", customerId);
                        transactionCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        transactionCmd.Parameters.AddWithValue("@Cashier", 1); // Replace with actual user ID

                        int transactionId = (int)transactionCmd.ExecuteScalar();

                        // 3. Insert transaction items and update stock
                        foreach (DataGridViewRow row in dgvCartDetails.Rows)
                        {
                            if (row.IsNewRow) continue;

                            string productName = row.Cells["ItemName"].Value.ToString();
                            int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                            decimal price = decimal.Parse(row.Cells["Price"].Value.ToString().Replace("₱", "").Trim());

                            // Get product ID
                            string getProductQuery = "SELECT ProductInternalID FROM Products WHERE product_name = @ProductName";
                            SqlCommand productCmd = new SqlCommand(getProductQuery, connection, transaction);
                            productCmd.Parameters.AddWithValue("@ProductName", productName);
                            int productId = (int)productCmd.ExecuteScalar();

                            // Insert transaction item
                            string insertItemQuery = @"
                                INSERT INTO TransactionItems (transaction_id, product_id, quantity, selling_price)
                                VALUES (@TransactionId, @ProductId, @Quantity, @SellingPrice);";

                            SqlCommand itemCmd = new SqlCommand(insertItemQuery, connection, transaction);
                            itemCmd.Parameters.AddWithValue("@TransactionId", transactionId);
                            itemCmd.Parameters.AddWithValue("@ProductId", productId);
                            itemCmd.Parameters.AddWithValue("@Quantity", quantity);
                            itemCmd.Parameters.AddWithValue("@SellingPrice", price);
                            itemCmd.ExecuteNonQuery();

                            // Update stock
                            string updateStockQuery = @"
                                UPDATE Products 
                                SET current_stock = current_stock - @Quantity 
                                WHERE ProductInternalID = @ProductId";

                            SqlCommand stockCmd = new SqlCommand(updateStockQuery, connection, transaction);
                            stockCmd.Parameters.AddWithValue("@Quantity", quantity);
                            stockCmd.Parameters.AddWithValue("@ProductId", productId);
                            stockCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        MessageBox.Show($"Transaction saved successfully!\n\nTransaction ID: TRX-{transactionId:D5}\nCustomer: {customerName}\nTotal: ₱{totalAmount:N2}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearCart();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving transaction: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Get existing customer or create new one
        private int GetOrCreateCustomer(SqlConnection connection, SqlTransaction transaction, string customerName)
        {
            // Check if customer exists
            string checkCustomerQuery = "SELECT customer_id FROM Customers WHERE customer_name = @CustomerName";
            SqlCommand checkCmd = new SqlCommand(checkCustomerQuery, connection, transaction);
            checkCmd.Parameters.AddWithValue("@CustomerName", customerName);

            var existingCustomer = checkCmd.ExecuteScalar();
            if (existingCustomer != null)
            {
                return (int)existingCustomer;
            }

            // Create new customer
            string insertCustomerQuery = @"
                INSERT INTO Customers (customer_name, created_at)
                VALUES (@CustomerName, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() as int);";

            SqlCommand insertCmd = new SqlCommand(insertCustomerQuery, connection, transaction);
            insertCmd.Parameters.AddWithValue("@CustomerName", customerName);

            return (int)insertCmd.ExecuteScalar();
        }

        // Clear cart and reset totals
        private void ClearCart()
        {
            dgvCartDetails.Rows.Clear();
            UpdateTotals();
            guna2TextBox1.Text = "";
            guna2TextBox1.PlaceholderText = "Optional";
        }

        // ALIGNMENT: Update subtotal, tax, and total labels with right alignment
        public void UpdateTotals()
        {
            decimal subtotal = CalculateSubtotal();
            decimal tax = CalculateTax(subtotal);
            decimal total = subtotal + tax;

            // ALIGNMENT: Values are right-aligned in the labels
            label4.Text = $"₱ {subtotal:N2}";
            label3.Text = $"₱ {tax:N2}";
            label7.Text = $"₱ {total:N2}";

            // ALIGNMENT: Make sure labels are right-aligned
            label4.TextAlign = ContentAlignment.MiddleRight;
            label3.TextAlign = ContentAlignment.MiddleRight;
            label7.TextAlign = ContentAlignment.MiddleRight;
        }

        // Clear cart button click
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the cart?",
                "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearCart();
            }
        }

        // Add item to cart or update quantity if exists
        public void AddItemToCart(string itemName, decimal price, int quantity = 1)
        {
            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["ItemName"].Value?.ToString() == itemName)
                {
                    int currentQty = Convert.ToInt32(row.Cells["Quantity"].Value);
                    row.Cells["Quantity"].Value = currentQty + quantity;
                    UpdateTotals();
                    return;
                }
            }

            dgvCartDetails.Rows.Add(itemName, quantity, $"₱{price:N2}");
            UpdateTotals();
            dgvCartDetails.Refresh();
        }

        // Add product to cart by database ID
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

        // Remove specific item from cart
        public void RemoveItemFromCart(string itemName)
        {
            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["ItemName"].Value?.ToString() == itemName)
                {
                    dgvCartDetails.Rows.Remove(row);
                    UpdateTotals();
                    return;
                }
            }
        }

        // Update quantity for specific item
        public void UpdateItemQuantity(string itemName, int newQuantity)
        {
            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["ItemName"].Value?.ToString() == itemName)
                {
                    row.Cells["Quantity"].Value = newQuantity;
                    UpdateTotals();
                    return;
                }
            }
        }

        // Get total cart amount
        public decimal GetCartTotal()
        {
            return CalculateSubtotal() + CalculateTax(CalculateSubtotal());
        }

        // Get number of items in cart
        public int GetCartItemCount()
        {
            int count = 0;
            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (!row.IsNewRow) count++;
            }
            return count;
        }

        // Get all cart items as list
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
                            ProductName = row.Cells["ItemName"].Value.ToString(),
                            Quantity = Convert.ToInt32(row.Cells["Quantity"].Value),
                            Price = price
                        });
                    }
                }
            }

            return items;
        }
    }
}