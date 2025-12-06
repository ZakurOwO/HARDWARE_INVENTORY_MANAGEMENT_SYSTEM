using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.ClassComponentTransaction;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module;
using CartItem = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.ClassComponentTransaction.SharedCartManager.CartItem;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class Walk_inCartDetails : UserControl
    {
        private readonly NumericUpDown qtyUpDown = new NumericUpDown();

        private readonly string connectionString;
        private readonly CheckoutPopUpContainer checkoutContainer;
        private readonly EventHandler cartUpdatedHandler;
        private bool deletionInProgress;



        public Walk_inCartDetails()
        {
            InitializeComponent();

            connectionString = ConnectionString.DataSource;
            checkoutContainer = new CheckoutPopUpContainer();

            InitializeDataGridViewColumns();
            InitializeCustomerField();

            dgvCartDetails.CellContentClick -= CartTable_CellContentClick; // ensure single subscription
            dgvCartDetails.CellContentClick += CartTable_CellContentClick;

            // Keep grid in sync with shared cart
            cartUpdatedHandler = (s, e) => LoadSharedCartItems();
            SharedCartManager.Instance.CartUpdated += cartUpdatedHandler;

            Disposed += (s, e) =>
            {
                SharedCartManager.Instance.CartUpdated -= cartUpdatedHandler;
            };
        }
     


        #region Initialization

        private void Walk_inCartDetails_Load(object sender, EventArgs e)
        {
            dgvCartDetails.ClearSelection();
            SetupNumericUpDown();
            LoadSharedCartItems();
            ConfigureLayout();
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
                guna2TextBox1.Text = string.Empty;
                guna2TextBox1.BorderColor = Color.FromArgb(213, 218, 223);
                guna2TextBox1.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
                guna2TextBox1.TextChanged += Guna2TextBox1_TextChanged;
            }
        }

        private void InitializeDataGridViewColumns()
        {
            dgvCartDetails.Columns.Clear();

            var productIdColumn = new DataGridViewTextBoxColumn
            {
                Name = "ProductInternalID",
                HeaderText = "Product ID",
                DataPropertyName = "ProductInternalID",
                Visible = false
            };
            dgvCartDetails.Columns.Add(productIdColumn);

            var itemNameColumn = new DataGridViewTextBoxColumn
            {
                Name = "ItemName",
                HeaderText = "ITEM",
                DataPropertyName = "ItemName",
                Width = 140,
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft }
            };
            dgvCartDetails.Columns.Add(itemNameColumn);

            var quantityColumn = new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "QTY",
                DataPropertyName = "Quantity",
                Width = 50,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            dgvCartDetails.Columns.Add(quantityColumn);

            var priceColumn = new DataGridViewTextBoxColumn
            {
                Name = "Price",
                HeaderText = "PRICE",
                DataPropertyName = "Price",
                Width = 90,
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            dgvCartDetails.Columns.Add(priceColumn);

            var deleteColumn = new DataGridViewImageColumn
            {
                Name = "Delete",
                HeaderText = string.Empty,
                Width = 20,
                Image = Properties.Resources.Delete__2_,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                DefaultCellStyle =
                {
                    NullValue = null,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            dgvCartDetails.Columns.Add(deleteColumn);

            dgvCartDetails.AutoGenerateColumns = false;
            dgvCartDetails.AllowUserToAddRows = false;
            dgvCartDetails.RowHeadersVisible = false;
            dgvCartDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCartDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCartDetails.ColumnHeadersHeight = 35;
        }

        private void ConfigureLayout()
        {
            if (dgvCartDetails != null)
            {
                dgvCartDetails.Anchor = AnchorStyles.None;
                int gridWidth = 300;
                int centerX = (Width - gridWidth) / 2;
                dgvCartDetails.Location = new Point(centerX, dgvCartDetails.Location.Y);
                dgvCartDetails.Width = gridWidth;
            }

            AdjustButtonPositions();
        }

        private void AdjustButtonPositions()
        {
            int buttonGap = 15;
            int totalButtonWidth = btnClear.Width + btnCheckout.Width + buttonGap;
            int startX = (Width - totalButtonWidth) / 2;

            btnClear.Location = new Point(startX, btnClear.Location.Y);
            btnCheckout.Location = new Point(startX + btnClear.Width + buttonGap, btnCheckout.Location.Y);

            btnClear.Anchor = AnchorStyles.Bottom;
            btnCheckout.Anchor = AnchorStyles.Bottom;
        }

        #endregion

        #region Customer helpers

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
                guna2TextBox1.Text = string.IsNullOrWhiteSpace(customerName) ? string.Empty : customerName;
            }
        }

        #endregion

        #region Quantity editing (NumericUpDown)

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
            if (e.RowIndex >= 0 &&
                e.ColumnIndex == dgvCartDetails.Columns["Quantity"].Index)
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
            if (!(qtyUpDown.Tag is Point cellLocation))
                return;

            int rowIndex = cellLocation.X;
            int columnIndex = cellLocation.Y;

            if (rowIndex < 0 || rowIndex >= dgvCartDetails.Rows.Count)
                return;

            dgvCartDetails.Rows[rowIndex].Cells[columnIndex].Value = qtyUpDown.Value;

            int productId = GetProductIdFromRow(rowIndex);
            int newQuantity = Convert.ToInt32(qtyUpDown.Value);
            int previousQuantity = SharedCartManager.Instance.GetItemQuantity(productId);

            if (productId > 0)
            {
                if (!SharedCartManager.Instance.UpdateItemQuantity(productId, newQuantity))
                {
                    // Revert if shared cart rejected this quantity
                    dgvCartDetails.Rows[rowIndex].Cells[columnIndex].Value = previousQuantity;
                    qtyUpDown.Value = previousQuantity;
                    return;
                }
            }

            LoadSharedCartItems(); // reflect enforced stock limits
            UpdateTotals();
            UpdateRowAppearance(rowIndex);
        }

        private void UpdateRowAppearance(int rowIndex)
        {
            if (dgvCartDetails.Rows[rowIndex].Cells["Quantity"].Value == null)
                return;

            int quantity = Convert.ToInt32(dgvCartDetails.Rows[rowIndex].Cells["Quantity"].Value);

            dgvCartDetails.Rows[rowIndex].DefaultCellStyle.BackColor =
                quantity > 10 ? Color.LightYellow : Color.White;
        }

        #endregion

        #region Cart / shared cart sync

        private void CartTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (deletionInProgress)
                return;

            if (!(sender is DataGridView grid) || e.RowIndex < 0 || e.RowIndex >= grid.Rows.Count)
                return;

            if (grid.Columns[e.ColumnIndex].Name != "Delete")
                return;

            int productId = GetProductIdFromRow(e.RowIndex);
            int.TryParse(grid.Rows[e.RowIndex].Cells["Quantity"].Value?.ToString(), out int quantityInCart);

            if (productId <= 0 || quantityInCart < 0)
                return;

            if (MessageBox.Show("Are you sure you want to remove this item from the cart?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                deletionInProgress = true;
                try
                {
                    if (SharedCartManager.Instance.RemoveItemFromCart(productId))
                    {
                        ReloadCartTable();
                        ReloadInventoryList();
                    }
                }
                finally
                {
                    deletionInProgress = false;
                }
            }
        }

        private void LoadSharedCartItems()
        {
            dgvCartDetails.Rows.Clear();

            var sharedItems = SharedCartManager.Instance.GetCartItems();
            foreach (var item in sharedItems)
            {
                dgvCartDetails.Rows.Add(
                    item.ProductInternalID,
                    item.ProductName,
                    item.Quantity,
                    $"₱{item.Price:N2}"
                );
            }

            UpdateTotals();
        }

        private void ReloadCartTable()
        {
            LoadSharedCartItems();
            UpdateTotals();
        }

        private void ReloadInventoryList()
        {
            SharedCartManager.Instance.RaiseInventoryUpdated();
        }

        private void ClearCart(bool restoreStock)
        {
            dgvCartDetails.Rows.Clear();
            SharedCartManager.Instance.ClearCart();
            UpdateTotals();

            guna2TextBox1.Text = string.Empty;
            guna2TextBox1.PlaceholderText = "Optional";

            ReloadInventoryList();
        }

        #endregion

        #region Totals / calculations

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

        private decimal CalculateSubtotal()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["Price"].Value == null || row.Cells["Quantity"].Value == null)
                    continue;

                string priceText = row.Cells["Price"].Value.ToString().Replace("₱", "").Trim();

                if (!decimal.TryParse(priceText, out decimal price))
                    continue;

                if (!int.TryParse(row.Cells["Quantity"].Value.ToString(), out int quantity))
                    continue;

                subtotal += price * quantity;
            }

            return subtotal;
        }

        private decimal CalculateTax(decimal subtotal) => subtotal * 0.12m;

        public decimal GetCartTotal()
        {
            decimal subtotal = CalculateSubtotal();
            return subtotal + CalculateTax(subtotal);
        }

        public List<CartItem> GetCartItems()
        {
            var items = new List<CartItem>();

            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["ItemName"].Value == null ||
                    row.Cells["Quantity"].Value == null ||
                    row.Cells["Price"].Value == null)
                {
                    continue;
                }

                string priceText = row.Cells["Price"].Value.ToString().Replace("₱", "").Trim();
                if (!decimal.TryParse(priceText, out decimal price))
                    continue;

                items.Add(new CartItem
                {
                    ProductName = row.Cells["ItemName"].Value.ToString(),
                    Quantity = Convert.ToInt32(row.Cells["Quantity"].Value),
                    Price = price
                });
            }

            return items;
        }

        #endregion

        #region Add items from inventory

        public void AddItemToCart(
            int productInternalId,
            string itemName,
            decimal price,
            int quantity = 1,
            string productId = "",
            string sku = "")
        {
            if (productInternalId <= 0)
            {
                MessageBox.Show("Invalid product selected.", "Product Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (quantity <= 0)
            {
                MessageBox.Show("Quantity must be at least 1.", "Quantity Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool added = SharedCartManager.Instance.AddItemToCart(new CartItem
            {
                ProductInternalId = productInternalId,
                ProductId = productId,
                Name = itemName,
                Sku = sku,
                UnitPrice = price,
                Quantity = quantity
            });

            if (added)
            {
                LoadSharedCartItems();
                UpdateTotals();
            }
        }

        public void AddProductToCartById(int productInternalId, int quantity = 1)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    const string query = @"
                        SELECT product_name, SellingPrice, current_stock, ProductID, SKU
                        FROM Products
                        WHERE ProductInternalID = @ProductInternalID
                          AND active = 1";

                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductInternalID", productInternalId);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Product not found.", "Product Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string productName = reader.GetString(0);
                        decimal sellingPrice = reader.GetDecimal(1);
                        int currentStock = reader.GetInt32(2);
                        string productId = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        string sku = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);

                        int reserved = SharedCartManager.Instance.GetItemQuantity(productInternalId);
                        int available = currentStock - reserved;

                        if (currentStock <= 0)
                        {
                            MessageBox.Show("Item is out of stock.", "Stock Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (quantity <= available)
                        {
                            AddItemToCart(productInternalId, productName, sellingPrice,
                                quantity, productId, sku);
                        }
                        else
                        {
                            MessageBox.Show(
                                $"Insufficient stock for {productName}. Available: {available}",
                                "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product to cart: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Checkout / validation / persistence

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

                // Proceed directly to payment popup; receipt will be shown after saving
                ShowCheckoutPopup();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during checkout: " + ex.Message, "Checkout Error",
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

        private bool ValidateStockAvailability()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var item in SharedCartManager.Instance.GetItems())
                    {
                        const string query =
                            "SELECT current_stock FROM Products WHERE ProductInternalID = @ProductId AND active = 1";

                        var command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ProductId", item.ProductInternalId);

                        var result = command.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("Product not found in database.",
                                "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        int availableStock = Convert.ToInt32(result);
                        if (item.Quantity > availableStock)
                        {
                            MessageBox.Show(
                                $"Insufficient stock. Available: {availableStock}, Requested: {item.Quantity}",
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

        private void ShowCheckoutPopup()
        {
            decimal subtotal = CalculateSubtotal();
            decimal tax = CalculateTax(subtotal);
            decimal totalAmount = subtotal + tax;

            MainDashBoard mainForm = FindMainForm();
            if (mainForm != null)
            {
                var container = new CheckoutPopUpContainer();
                container.ShowCheckoutPopUp(mainForm, totalAmount, subtotal, tax, "WalkIn", this);
            }
            else
            {
                MessageBox.Show("Unable to find main form.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private MainDashBoard FindMainForm()
        {
            Control parent = Parent;
            while (parent != null)
            {
                if (parent is MainDashBoard mainForm)
                    return mainForm;
                parent = parent.Parent;
            }
            return null;
        }

        public void ProcessWalkInTransaction(string paymentMethod, decimal cashReceived, decimal change)
        {
            try
            {
                string customerName = GetCustomerName();
                decimal subtotal = CalculateSubtotal();
                decimal tax = CalculateTax(subtotal);
                decimal totalAmount = subtotal + tax;

                ReceiptData receiptData;
                if (!SaveTransactionAndItems(customerName, subtotal, tax, totalAmount,
                    paymentMethod, cashReceived, change, out receiptData))
                {
                    return;
                }

                ReloadTransactionHistory();
                OpenReceiptPreview(receiptData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing transaction: {ex.Message}", "Transaction Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SaveTransactionAndItems(
            string customerName,
            decimal subtotal,
            decimal tax,
            decimal totalAmount,
            string paymentMethod,
            decimal cashReceived,
            decimal change,
            out ReceiptData receiptData)
        {
            receiptData = null;
            var cartItems = SharedCartManager.Instance.GetItems();
            if (cartItems == null || cartItems.Count == 0)
            {
                MessageBox.Show("Cart is empty. Please add items before checking out.",
                    "Checkout Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var dbTransaction = connection.BeginTransaction();

                    try
                    {
                        int customerId = GetOrCreateCustomer(connection, dbTransaction, customerName);
                        var (cashierId, username) = ResolveAuditUser();

                        const string insertTransactionQuery = @"
    INSERT INTO Transactions (
        transaction_date,
        customer_id,
        total_amount,
        cashier,
        payment_method,
        cash_received,
        change_amount,
        delivery_id
    )
    OUTPUT INSERTED.transaction_id
    VALUES (
        GETDATE(),
        @CustomerId,
        @TotalAmount,
        @Cashier,
        @PaymentMethod,
        @CashReceived,
        @ChangeAmount,
        NULL
    );";

                        var transactionCmd = new SqlCommand(insertTransactionQuery, connection, dbTransaction);
                        transactionCmd.Parameters.AddWithValue("@CustomerId", customerId);
                        transactionCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        transactionCmd.Parameters.AddWithValue("@Cashier", cashierId);
                        transactionCmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                        transactionCmd.Parameters.AddWithValue("@CashReceived", cashReceived);
                        transactionCmd.Parameters.AddWithValue("@ChangeAmount", change);

                        int transactionId = Convert.ToInt32(transactionCmd.ExecuteScalar());

                        var idLookupCmd = new SqlCommand(
                            "SELECT TransactionID, transaction_date FROM Transactions WHERE transaction_id = @Id",
                            connection, dbTransaction);
                        idLookupCmd.Parameters.AddWithValue("@Id", transactionId);

                        string transactionIdentifier = "TRX-" + transactionId.ToString("D5");
                        DateTime transactionDate = DateTime.Now;

                        using (var reader = idLookupCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                    transactionIdentifier = reader.GetString(0);
                                if (!reader.IsDBNull(1))
                                    transactionDate = reader.GetDateTime(1);
                            }
                        }

                        LogAuditEntry(connection, dbTransaction,
                            "Created transaction " + transactionIdentifier + " with " + cartItems.Count + " item(s), total " + totalAmount,
                            AuditActivityType.CREATE,
                            "Transactions",
                            transactionId.ToString(),
                            null,
                            "cashier=" + username + ";payment_method=" + paymentMethod + ";total_amount=" + totalAmount);

                        foreach (var item in cartItems)
                        {
                            var itemCmd = new SqlCommand(@"
                                INSERT INTO TransactionItems (transaction_id, product_id, quantity, selling_price)
                                VALUES (@TransactionId, @ProductId, @Quantity, @SellingPrice);",
                                connection, dbTransaction);

                            itemCmd.Parameters.AddWithValue("@TransactionId", transactionId);
                            itemCmd.Parameters.AddWithValue("@ProductId", item.ProductInternalId);
                            itemCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                            itemCmd.Parameters.AddWithValue("@SellingPrice", item.UnitPrice);
                            itemCmd.ExecuteNonQuery();

                            var updateStockCmd = new SqlCommand(@"
                                UPDATE Products
                                SET current_stock = current_stock - @Quantity
                                WHERE ProductInternalID = @ProductId
                                  AND current_stock >= @Quantity;",
                                connection, dbTransaction);

                            updateStockCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                            updateStockCmd.Parameters.AddWithValue("@ProductId", item.ProductInternalId);

                            int updated = updateStockCmd.ExecuteNonQuery();
                            if (updated == 0)
                            {
                                throw new InvalidOperationException("Insufficient stock for " + item.Name + ".");
                            }
                        }

                        dbTransaction.Commit();

                        receiptData = new ReceiptData
                        {
                            DocumentTitle = "Receipt",
                            TransactionType = "Walk-in",
                            PaymentMethod = paymentMethod,
                            TransactionId = transactionIdentifier,
                            TransactionDate = transactionDate,
                            Subtotal = subtotal,
                            Tax = tax,
                            Total = totalAmount,
                            Items = BuildReceiptItems(cartItems)
                        };

                        return true;
                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Checkout failed. No changes were saved.\n\nDetails: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private List<ReceiptItem> BuildReceiptItems(IReadOnlyList<CartItem> cartItems)
        {
            var items = new List<ReceiptItem>();
            for (int i = 0; i < cartItems.Count; i++)
            {
                CartItem item = cartItems[i];
                items.Add(new ReceiptItem
                {
                    ItemName = item.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }
            return items;
        }

        private void OpenReceiptPreview(ReceiptData receiptData)
        {
            if (receiptData == null)
                return;

            using (var dlg = new ReceiptPreviewForm(receiptData))
            {
                var result = dlg.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ResetForNewTransaction();
                }
            }
        }

        private void ResetForNewTransaction()
        {
            ClearCart(true);
            SetCustomerName(string.Empty);
            ReloadTransactionHistory();
        }

        private void ReloadTransactionHistory()
        {
            var main = FindMainForm();
            if (main == null)
                return;

            foreach (Control control in main.Controls)
            {
                ReloadHistoryRecursive(control);
            }
        }

        private void ReloadHistoryRecursive(Control control)
        {
            var history = control as History_Module.DatGridTableHistory;
            if (history != null)
            {
                history.LoadTransactionHistory();
                return;
            }

            foreach (Control child in control.Controls)
            {
                ReloadHistoryRecursive(child);
            }
        }

        #endregion

        #region DB helpers / audit

        private int GetOrCreateCustomer(SqlConnection connection, SqlTransaction transaction, string customerName)
        {
            const string checkCustomerQuery =
                "SELECT customer_id FROM Customers WHERE customer_name = @CustomerName";

            var checkCmd = new SqlCommand(checkCustomerQuery, connection, transaction);
            checkCmd.Parameters.AddWithValue("@CustomerName", customerName);

            var existingCustomer = checkCmd.ExecuteScalar();
            if (existingCustomer != null)
            {
                return (int)existingCustomer;
            }

            const string insertCustomerQuery = @"
                INSERT INTO Customers (customer_name, created_at)
                VALUES (@CustomerName, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() as int);";

            var insertCmd = new SqlCommand(insertCustomerQuery, connection, transaction);
            insertCmd.Parameters.AddWithValue("@CustomerName", customerName);

            return (int)insertCmd.ExecuteScalar();
        }

        private (int userId, string username) ResolveAuditUser()
        {
            int userId = UserSession.UserId > 0 ? UserSession.UserId : 1;
            string username = !string.IsNullOrWhiteSpace(UserSession.Username)
                ? UserSession.Username
                : "System";
            return (userId, username);
        }

        private void LogAuditEntry(
            SqlConnection connection,
            SqlTransaction transaction,
            string activity,
            AuditActivityType activityType,
            string tableAffected,
            string recordId,
            string oldValues,
            string newValues)
        {
            AuditHelper.LogWithTransaction(
                connection,
                transaction,
                AuditModule.SALES,
                activity,
                activityType,
                tableAffected,
                recordId,
                oldValues,
                newValues);
        }

        private int GetProductIdFromRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvCartDetails.Rows.Count)
                return 0;

            object value = dgvCartDetails.Rows[rowIndex].Cells["ProductInternalID"].Value;
            if (value == null)
                return 0;

            return int.TryParse(value.ToString(), out int productId) ? productId : 0;
        }

        private decimal ParsePrice(object priceValue)
        {
            if (priceValue == null)
                return 0m;

            string priceText = priceValue.ToString().Replace("₱", string.Empty).Trim();
            return decimal.TryParse(priceText, out decimal price) ? price : 0m;
        }

        #endregion

        #region UI actions

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the cart?",
                    "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearCart(true);
            }
        }

        #endregion
    }
}
