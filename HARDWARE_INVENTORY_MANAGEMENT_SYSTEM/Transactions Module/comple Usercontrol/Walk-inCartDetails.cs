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
        private CheckoutPopUpContainer checkoutContainer;

        public Walk_inCartDetails()
        {
            InitializeComponent();
            connectionString = ConnectionString.DataSource;
            checkoutContainer = new CheckoutPopUpContainer();
            InitializeDataGridViewColumns();
            InitializeCustomerField();
        }

        private void InitializeCustomerField()
        {
            if (label1 != null)
            {
                label1.Text = "Customer";
                label1.Font = new Font("Lexend Light", 9F, FontStyle.Regular);
                label1.ForeColor = Color.FromArgb(64, 64, 64);
                label1.Location = new Point(10, 17);
            }

            if (guna2TextBox1 != null)
            {
                guna2TextBox1.PlaceholderText = "Optional";
                guna2TextBox1.Text = "";
                guna2TextBox1.BorderColor = Color.FromArgb(213, 218, 223);
                guna2TextBox1.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
                guna2TextBox1.TextChanged += Guna2TextBox1_TextChanged;
            }
        }

        private void Guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text.Length > 100)
            {
                guna2TextBox1.Text = guna2TextBox1.Text.Substring(0, 100);
                guna2TextBox1.SelectionStart = guna2TextBox1.Text.Length;
            }
        }

        public string GetCustomerName()
        {
            if (guna2TextBox1 != null && !string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                return guna2TextBox1.Text.Trim();
            }
            return "Walk-in Customer";
        }

        public void SetCustomerName(string customerName)
        {
            if (guna2TextBox1 != null)
            {
                guna2TextBox1.Text = string.IsNullOrWhiteSpace(customerName) ? "" : customerName;
            }
        }

        private void InitializeDataGridViewColumns()
        {
            dgvCartDetails.Columns.Clear();

            DataGridViewTextBoxColumn itemNameColumn = new DataGridViewTextBoxColumn();
            itemNameColumn.Name = "ItemName";
            itemNameColumn.HeaderText = "ITEM";
            itemNameColumn.DataPropertyName = "ItemName";
            itemNameColumn.Width = 140;
            itemNameColumn.ReadOnly = true;
            itemNameColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCartDetails.Columns.Add(itemNameColumn);

            DataGridViewTextBoxColumn quantityColumn = new DataGridViewTextBoxColumn();
            quantityColumn.Name = "Quantity";
            quantityColumn.HeaderText = "QTY";
            quantityColumn.DataPropertyName = "Quantity";
            quantityColumn.Width = 50;
            quantityColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCartDetails.Columns.Add(quantityColumn);

            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();
            priceColumn.Name = "Price";
            priceColumn.HeaderText = "PRICE";
            priceColumn.DataPropertyName = "Price";
            priceColumn.Width = 90;
            priceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            priceColumn.ReadOnly = true;
            dgvCartDetails.Columns.Add(priceColumn);

            DataGridViewImageColumn deleteColumn = new DataGridViewImageColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "";
            deleteColumn.Width = 20;
            deleteColumn.Image = Properties.Resources.trash;
            deleteColumn.DefaultCellStyle.NullValue = null;
            deleteColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            deleteColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCartDetails.Columns.Add(deleteColumn);

            dgvCartDetails.AutoGenerateColumns = false;
            dgvCartDetails.AllowUserToAddRows = false;
            dgvCartDetails.RowHeadersVisible = false;
            dgvCartDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCartDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCartDetails.ColumnHeadersHeight = 35;
        }

        private void Walk_inCartDetails_Load(object sender, EventArgs e)
        {
            dgvCartDetails.ClearSelection();
            SetupNumericUpDown();
            LoadSharedCartItems();
            ConfigureLayout();
        }

        private void ConfigureLayout()
        {
            if (dgvCartDetails != null)
            {
                dgvCartDetails.Anchor = AnchorStyles.None;
                int gridWidth = 300;
                int centerX = (this.Width - gridWidth) / 2;
                dgvCartDetails.Location = new Point(centerX, dgvCartDetails.Location.Y);
                dgvCartDetails.Width = gridWidth;
            }
            AdjustButtonPositions();
        }

        private void AdjustButtonPositions()
        {
            int buttonGap = 15;
            int totalButtonWidth = btnClear.Width + btnCheckout.Width + buttonGap;
            int startX = (this.Width - totalButtonWidth) / 2;

            btnClear.Location = new Point(startX, btnClear.Location.Y);
            btnCheckout.Location = new Point(startX + btnClear.Width + buttonGap, btnCheckout.Location.Y);

            btnClear.Anchor = AnchorStyles.Bottom;
            btnCheckout.Anchor = AnchorStyles.Bottom;
        }

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

            dgvCartDetails.CellClick += dgvCartDetails_CellClick;
            qtyUpDown.Leave += qtyUpDown_Leave;
            qtyUpDown.ValueChanged += qtyUpDown_ValueChanged;
            qtyUpDown.KeyDown += QtyUpDown_KeyDown;
        }

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

        private void dgvCartDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCartDetails.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to remove this item from the cart?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string productName = dgvCartDetails.Rows[e.RowIndex].Cells["ItemName"].Value?.ToString();
                    if (!string.IsNullOrEmpty(productName))
                    {
                        SharedCartManager.Instance.RemoveItemFromCart(productName);
                    }
                    dgvCartDetails.Rows.RemoveAt(e.RowIndex);
                    UpdateTotals();
                }
            }
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

                    string productName = dgvCartDetails.Rows[rowIndex].Cells["ItemName"].Value?.ToString();
                    int newQuantity = Convert.ToInt32(qtyUpDown.Value);

                    if (!string.IsNullOrEmpty(productName))
                    {
                        SharedCartManager.Instance.UpdateItemQuantity(productName, newQuantity);
                    }

                    UpdateTotals();
                    UpdateRowAppearance(rowIndex);
                }
            }
        }

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

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            CheckoutWalkIn();
        }

        private void CheckoutWalkIn()
        {
            try
            {
                if (!ValidateCheckout()) return;
                if (!ValidateQuantities()) return;
                if (!ValidateStockAvailability()) return;

                ShowCheckoutPopup();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during checkout: {ex.Message}", "Checkout Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateCheckout()
        {
            if (dgvCartDetails.Rows.Count == 0)
            {
                MessageBox.Show("Cart is empty. Please add items to checkout.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ShowCheckoutPopup()
        {
            decimal subtotal = CalculateSubtotal();
            decimal tax = CalculateTax(subtotal);
            decimal totalAmount = subtotal + tax;

            // Find the main form directly
            MainDashBoard mainForm = FindMainForm();
            if (mainForm != null)
            {
                CheckoutPopUpContainer checkoutContainer = new CheckoutPopUpContainer();
                checkoutContainer.ShowCheckoutPopUp(mainForm, totalAmount, subtotal, tax, "WalkIn", this);
            }
            else
            {
                MessageBox.Show("Unable to find main form.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private MainDashBoard FindMainForm()
        {
            Control parent = this.Parent;
            while (parent != null)
            {
                if (parent is MainDashBoard mainForm)
                    return mainForm;
                parent = parent.Parent;
            }
            return null;
        }

        public decimal GetCartTotal()
        {
            return CalculateSubtotal() + CalculateTax(CalculateSubtotal());
        }

        public List<CartItem> GetCartItems()
        {
            var items = new List<CartItem>();
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
        public void ProcessWalkInTransaction(string paymentMethod, decimal cashReceived, decimal change)
        {
            try
            {
                string customerName = GetCustomerName();
                decimal subtotal = CalculateSubtotal();
                decimal tax = CalculateTax(subtotal);
                decimal totalAmount = subtotal + tax;

                SaveWalkInTransactionToDatabase(customerName, subtotal, tax, totalAmount, paymentMethod, cashReceived, change);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing transaction: {ex.Message}", "Transaction Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private decimal CalculateTax(decimal subtotal)
        {
            return subtotal * 0.12m;
        }

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

                        string query = "SELECT current_stock FROM Products WHERE product_name = @ProductName AND active = 1";
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

        private void SaveWalkInTransactionToDatabase(string customerName, decimal subtotal, decimal tax, decimal totalAmount, string paymentMethod, decimal cashReceived, decimal change)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction dbTransaction = connection.BeginTransaction();

                    try
                    {
                        int customerId = GetOrCreateCustomer(connection, dbTransaction, customerName);

                        string insertTransactionQuery = @"
                            INSERT INTO Transactions (
                                transaction_date, 
                                customer_id, 
                                total_amount, 
                                cashier, 
                                payment_method, 
                                cash_received, 
                                change_amount
                            )
                            VALUES (
                                GETDATE(), 
                                @CustomerId, 
                                @TotalAmount, 
                                @Cashier, 
                                @PaymentMethod, 
                                @CashReceived, 
                                @ChangeAmount
                            );
                            SELECT CAST(SCOPE_IDENTITY() as int);";

                        SqlCommand transactionCmd = new SqlCommand(insertTransactionQuery, connection, dbTransaction);
                        transactionCmd.Parameters.AddWithValue("@CustomerId", customerId);
                        transactionCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        transactionCmd.Parameters.AddWithValue("@Cashier", 1);
                        transactionCmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                        transactionCmd.Parameters.AddWithValue("@CashReceived", cashReceived);
                        transactionCmd.Parameters.AddWithValue("@ChangeAmount", change);

                        int transactionId = (int)transactionCmd.ExecuteScalar();

                        foreach (DataGridViewRow row in dgvCartDetails.Rows)
                        {
                            if (row.IsNewRow) continue;

                            string productName = row.Cells["ItemName"].Value.ToString();
                            int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                            decimal price = decimal.Parse(row.Cells["Price"].Value.ToString().Replace("₱", "").Trim());

                            string getProductQuery = "SELECT ProductInternalID FROM Products WHERE product_name = @ProductName";
                            SqlCommand productCmd = new SqlCommand(getProductQuery, connection, dbTransaction);
                            productCmd.Parameters.AddWithValue("@ProductName", productName);
                            int productId = (int)productCmd.ExecuteScalar();

                            string insertItemQuery = @"
                                INSERT INTO TransactionItems (transaction_id, product_id, quantity, selling_price)
                                VALUES (@TransactionId, @ProductId, @Quantity, @SellingPrice);";

                            SqlCommand itemCmd = new SqlCommand(insertItemQuery, connection, dbTransaction);
                            itemCmd.Parameters.AddWithValue("@TransactionId", transactionId);
                            itemCmd.Parameters.AddWithValue("@ProductId", productId);
                            itemCmd.Parameters.AddWithValue("@Quantity", quantity);
                            itemCmd.Parameters.AddWithValue("@SellingPrice", price);
                            itemCmd.ExecuteNonQuery();

                            string updateStockQuery = @"
                                UPDATE Products 
                                SET current_stock = current_stock - @Quantity 
                                WHERE ProductInternalID = @ProductId";

                            SqlCommand stockCmd = new SqlCommand(updateStockQuery, connection, dbTransaction);
                            stockCmd.Parameters.AddWithValue("@Quantity", quantity);
                            stockCmd.Parameters.AddWithValue("@ProductId", productId);
                            stockCmd.ExecuteNonQuery();
                        }

                        dbTransaction.Commit();

                        MessageBox.Show($"Walk-in transaction completed successfully!\n\n" +
                                      $"Transaction ID: TRX-{transactionId:D5}\n" +
                                      $"Customer: {customerName}\n" +
                                      $"Payment Method: {paymentMethod}\n" +
                                      $"Total Amount: ₱{totalAmount:N2}",
                            "Transaction Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearCart();
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
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

        private int GetOrCreateCustomer(SqlConnection connection, SqlTransaction transaction, string customerName)
        {
            string checkCustomerQuery = "SELECT customer_id FROM Customers WHERE customer_name = @CustomerName";
            SqlCommand checkCmd = new SqlCommand(checkCustomerQuery, connection, transaction);
            checkCmd.Parameters.AddWithValue("@CustomerName", customerName);

            var existingCustomer = checkCmd.ExecuteScalar();
            if (existingCustomer != null)
            {
                return (int)existingCustomer;
            }

            string insertCustomerQuery = @"
                INSERT INTO Customers (customer_name, created_at)
                VALUES (@CustomerName, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() as int);";

            SqlCommand insertCmd = new SqlCommand(insertCustomerQuery, connection, transaction);
            insertCmd.Parameters.AddWithValue("@CustomerName", customerName);

            return (int)insertCmd.ExecuteScalar();
        }

        private void ClearCart()
        {
            dgvCartDetails.Rows.Clear();
            SharedCartManager.Instance.ClearCart();
            UpdateTotals();
            guna2TextBox1.Text = "";
            guna2TextBox1.PlaceholderText = "Optional";
        }

        public void UpdateTotals()
        {
            decimal subtotal = CalculateSubtotal();
            decimal tax = CalculateTax(subtotal);
            decimal total = subtotal + tax;

            label4.Text = $"₱ {subtotal:N2}";
            label3.Text = $"₱ {tax:N2}";
            label7.Text = $"₱ {total:N2}";

            label4.TextAlign = ContentAlignment.MiddleRight;
            label3.TextAlign = ContentAlignment.MiddleRight;
            label7.TextAlign = ContentAlignment.MiddleRight;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the cart?",
                "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearCart();
            }
        }

        public void AddItemToCart(string itemName, decimal price, int quantity = 1)
        {
            SharedCartManager.Instance.AddItemToCart(new CartItem
            {
                ProductName = itemName,
                Price = price,
                Quantity = quantity
            });

            LoadSharedCartItems();
        }

        public void AddProductToCartById(int productInternalId, int quantity = 1)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT product_name, SellingPrice, current_stock
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

        private void LoadSharedCartItems()
        {
            dgvCartDetails.Rows.Clear();
            var sharedItems = SharedCartManager.Instance.GetCartItems();

            foreach (var item in sharedItems)
            {
                dgvCartDetails.Rows.Add(item.ProductName, item.Quantity, $"₱{item.Price:N2}");
            }
            UpdateTotals();
        }

       

        }
    }
