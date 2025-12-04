using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.ClassComponentTransaction;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.ClassComponentTransaction;
using CartItem = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.ClassComponentTransaction.SharedCartManager.CartItem;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class DeliveryCartDetails : UserControl
    {
        private readonly NumericUpDown qtyUpDown = new NumericUpDown();

        private VehicleDataAccess vehicleDataAccess;
        private List<VehicleRecord> availableVehicles;
        private readonly List<VehicleRecord> selectedVehicles = new List<VehicleRecord>();

        private Panel vehicleSelectionPanel;
        private CheckedListBox vehicleCheckedListBox;
        private Guna.UI2.WinForms.Guna2Button btnMultiSelectVehicles;

        private readonly string connectionString;
        private readonly CheckoutPopUpContainer checkoutContainer;
        private readonly EventHandler cartUpdatedHandler;

        public DeliveryCartDetails()
        {
            InitializeComponent();

            vehicleDataAccess = new VehicleDataAccess();
            connectionString = ConnectionString.DataSource;
            checkoutContainer = new CheckoutPopUpContainer();

            InitializeDataGridViewColumns();

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

        private void DeliveryCartDetails_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadVehicles();

            dgvCartDetails.ClearSelection();
            SetupNumericUpDown();
            LoadSharedCartItems();
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
                Width = 200,
                ReadOnly = true
            };
            dgvCartDetails.Columns.Add(itemNameColumn);

            var quantityColumn = new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "QTY",
                DataPropertyName = "Quantity",
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            dgvCartDetails.Columns.Add(quantityColumn);

            var priceColumn = new DataGridViewTextBoxColumn
            {
                Name = "Price",
                HeaderText = "PRICE",
                DataPropertyName = "Price",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            dgvCartDetails.Columns.Add(priceColumn);

            var deleteColumn = new DataGridViewImageColumn
            {
                Name = "Delete",
                HeaderText = string.Empty,
                Width = 42,
                Image = Properties.Resources.trash,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                DefaultCellStyle = { NullValue = null }
            };
            dgvCartDetails.Columns.Add(deleteColumn);

            dgvCartDetails.AutoGenerateColumns = false;
            dgvCartDetails.AllowUserToAddRows = false;
            dgvCartDetails.RowHeadersVisible = false;
            dgvCartDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

        private void LoadCustomers()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    const string query = "SELECT customer_name FROM Customers ORDER BY customer_name";
                    var command = new SqlCommand(query, connection);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        cbxChooseCustomer.Items.Clear();
                        while (reader.Read())
                        {
                            cbxChooseCustomer.Items.Add(reader["customer_name"].ToString());
                        }
                    }

                    if (cbxChooseCustomer.Items.Count > 0)
                    {
                        cbxChooseCustomer.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVehicles()
        {
            try
            {
                availableVehicles = vehicleDataAccess.GetVehiclesByStatus("Available");
                CreateMultiVehicleSelector();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Vehicle selection UI

        private void CreateMultiVehicleSelector()
        {
            if (btnDropdown != null)
            {
                Controls.Remove(btnDropdown);
                btnDropdown.Dispose();
            }

            btnMultiSelectVehicles = new Guna.UI2.WinForms.Guna2Button
            {
                Name = "btnMultiSelectVehicles",
                BorderColor = Color.Gainsboro,
                BorderRadius = 5,
                BorderThickness = 1,
                FillColor = Color.White,
                Font = new Font("Lexend Light", 8.25F),
                ForeColor = Color.FromArgb(68, 88, 112),
                Image = Properties.Resources.direction_down_01,
                ImageAlign = HorizontalAlignment.Right,
                Location = new Point(80, 48),
                Size = new Size(193, 29),
                TabIndex = 22,
                Text = "Select Vehicles",
                TextAlign = HorizontalAlignment.Left
            };

            btnMultiSelectVehicles.Click += BtnMultiSelectVehicles_Click;

            Controls.Add(btnMultiSelectVehicles);
            btnMultiSelectVehicles.BringToFront();

            CreateVehicleSelectionPanel();
        }

        private void CreateVehicleSelectionPanel()
        {
            vehicleSelectionPanel = new Panel
            {
                Name = "vehicleSelectionPanel",
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Location = new Point(80, 78),
                Size = new Size(193, 200),
                Visible = false,
                AutoScroll = true
            };

            vehicleCheckedListBox = new CheckedListBox
            {
                Name = "vehicleCheckedListBox",
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Font = new Font("Lexend Light", 8.25F),
                CheckOnClick = true
            };
            vehicleCheckedListBox.ItemCheck += VehicleCheckedListBox_ItemCheck;

            if (availableVehicles != null && availableVehicles.Count > 0)
            {
                foreach (var vehicle in availableVehicles)
                {
                    string displayText = $"{vehicle.Brand} {vehicle.Model} - {vehicle.PlateNumber}";
                    vehicleCheckedListBox.Items.Add(displayText);
                }
            }
            else
            {
                vehicleCheckedListBox.Items.Add("No vehicles available");
                vehicleCheckedListBox.Enabled = false;
            }

            vehicleSelectionPanel.Controls.Add(vehicleCheckedListBox);
            Controls.Add(vehicleSelectionPanel);
            vehicleSelectionPanel.BringToFront();
        }

        private void BtnMultiSelectVehicles_Click(object sender, EventArgs e)
        {
            vehicleSelectionPanel.Visible = !vehicleSelectionPanel.Visible;
            if (vehicleSelectionPanel.Visible)
            {
                vehicleSelectionPanel.BringToFront();
            }
        }

        private void VehicleCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Delay until the check state is actually updated
            BeginInvoke(new Action(() =>
            {
                UpdateSelectedVehicles();
                UpdateVehicleButtonText();
                UpdateCartTotals();
            }));
        }

        private void UpdateSelectedVehicles()
        {
            selectedVehicles.Clear();

            if (availableVehicles == null || vehicleCheckedListBox == null)
                return;

            for (int i = 0; i < vehicleCheckedListBox.CheckedIndices.Count; i++)
            {
                int index = vehicleCheckedListBox.CheckedIndices[i];
                if (index < availableVehicles.Count)
                {
                    selectedVehicles.Add(availableVehicles[index]);
                }
            }
        }

        private void UpdateVehicleButtonText()
        {
            if (btnMultiSelectVehicles == null) return;

            if (selectedVehicles.Count == 0)
            {
                btnMultiSelectVehicles.Text = "Select Vehicles";
            }
            else if (selectedVehicles.Count == 1)
            {
                var vehicle = selectedVehicles[0];
                btnMultiSelectVehicles.Text = $"{vehicle.Brand} {vehicle.Model}";
            }
            else
            {
                btnMultiSelectVehicles.Text = $"{selectedVehicles.Count} Vehicles Selected";
            }
        }

        private void ClearVehicleSelection()
        {
            selectedVehicles.Clear();

            if (vehicleCheckedListBox != null)
            {
                for (int i = 0; i < vehicleCheckedListBox.Items.Count; i++)
                {
                    vehicleCheckedListBox.SetItemChecked(i, false);
                }
            }

            UpdateVehicleButtonText();
        }

        #endregion

        #region Quantity editing (NumericUpDown)

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

            LoadSharedCartItems(); // so any enforced limits are reflected immediately
            UpdateCartTotals();
        }

        #endregion

        #region Cart / shared cart sync

        private void CartTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(sender is DataGridView grid) || e.RowIndex < 0)
                return;

            if (grid.Columns[e.ColumnIndex].Name != "Delete")
                return;

            int productId = GetProductIdFromRow(e.RowIndex);
            bool v = int.TryParse(grid.Rows[e.RowIndex].Cells["Quantity"].Value?.ToString(), out int quantityInCart);

            if (productId <= 0 || quantityInCart < 0)
                return;

            if (MessageBox.Show("Are you sure you want to remove this item from the cart?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (SharedCartManager.Instance.RemoveItemFromCart(productId))
                {
                    ReloadCartTable();
                    ReloadInventoryList();
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

            UpdateCartTotals();
        }

        private void ReloadCartTable()
        {
            LoadSharedCartItems();
            UpdateCartTotals();
        }

        private void ReloadInventoryList()
        {
            SharedCartManager.Instance.RaiseInventoryUpdated();
        }

        public void ClearCart(bool restoreStock)
        {
            dgvCartDetails.Rows.Clear();
            SharedCartManager.Instance.ClearCart();
            UpdateCartTotals();
            ReloadInventoryList();

        }

        #endregion

        #region Add items from inventory

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
                            AddItemToCart(productInternalId, productName, sellingPrice, quantity, productId, sku);
                        }
                        else
                        {
                            MessageBox.Show($"Insufficient stock for {productName}. Available: {available}",
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

            // Use CartItem instead of CartItemModel
            bool added = SharedCartManager.Instance.AddItemToCart(new CartItem
            {
                ProductInternalID = productInternalId,
                ProductName = itemName,
                ProductID = productId,
                Price = price,
                Quantity = quantity
            });

            if (added)
            {
                LoadSharedCartItems();
                UpdateCartTotals();
            }
        }

        #endregion

        #region Totals / calculations

        private void UpdateCartTotals()
        {
            decimal subtotal = CalculateSubtotal();
            decimal tax = CalculateTax(subtotal);
            decimal shippingFee = CalculateShippingFee();
            decimal total = subtotal + tax + shippingFee;

            label6.Text = $"₱{subtotal:N2}";
            label8.Text = $"₱{tax:N2}";
            label5.Text = $"₱{shippingFee:N2}";
            label9.Text = $"₱{total:N2}";
        }

        private decimal CalculateSubtotal()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["Quantity"].Value == null || row.Cells["Price"].Value == null)
                    continue;

                int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                string priceText = row.Cells["Price"].Value.ToString().Replace("₱", "").Trim();

                if (decimal.TryParse(priceText, out decimal price))
                {
                    subtotal += quantity * price;
                }
            }

            return subtotal;
        }

        private decimal CalculateTax(decimal subtotal) => subtotal * 0.12m;

        private decimal CalculateShippingFee()
        {
            decimal baseFee = 100.00m;
            if (selectedVehicles.Count > 1)
            {
                baseFee += (selectedVehicles.Count - 1) * 50.00m;
            }
            return baseFee;
        }

        public string GetSelectedCustomerName()
        {
            return cbxChooseCustomer.SelectedItem?.ToString() ?? "Walk-in Customer";
        }

        public decimal CalculateTotalAmount()
        {
            decimal subtotal = CalculateSubtotal();
            decimal tax = CalculateTax(subtotal);
            decimal shippingFee = CalculateShippingFee();
            return subtotal + tax + shippingFee;
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
                if (!decimal.TryParse(priceText, out decimal price)) continue;

                items.Add(new CartItem
                {
                    ProductName = row.Cells["ItemName"].Value.ToString(),
                    Quantity = Convert.ToInt32(row.Cells["Quantity"].Value),
                    Price = price
                });
            }

            return items;
        }

        public List<VehicleRecord> GetSelectedVehicles() => selectedVehicles;

        #endregion

        #region Checkout / validation

        private void btnBlue_Click(object sender, EventArgs e)
        {
            CheckoutDelivery();
        }

        private void CheckoutDelivery()
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
            if (dgvCartDetails.Rows.Count == 0 ||
                (dgvCartDetails.Rows.Count == 1 && dgvCartDetails.Rows[0].IsNewRow))
            {
                MessageBox.Show("Cart is empty! Please add items before checkout.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbxChooseCustomer.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a customer!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxChooseCustomer.Focus();
                return false;
            }

            if (selectedVehicles.Count == 0)
            {
                MessageBox.Show("Please select at least one vehicle for delivery!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        const string query = "SELECT current_stock FROM Products WHERE ProductInternalID = @ProductId AND active = 1";
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
            decimal shippingFee = CalculateShippingFee();
            decimal totalAmount = subtotal + tax + shippingFee;

            MainDashBoard mainForm = FindMainForm();
            if (mainForm != null)
            {
                checkoutContainer.ShowCheckoutPopUp(mainForm, totalAmount, subtotal, tax, "Delivery", this);
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

        public void ProcessDeliveryTransaction(string paymentMethod, decimal cashReceived, decimal change)
        {
            if (!ValidateCheckout() || !ValidateQuantities() || !ValidateStockAvailability())
            {
                return;
            }

            try
            {
                string customerName = GetSelectedCustomerName();
                decimal subtotal = CalculateSubtotal();
                decimal tax = CalculateTax(subtotal);
                decimal shippingFee = CalculateShippingFee();
                decimal totalAmount = subtotal + tax + shippingFee;

                SaveDeliveryTransactionToDatabase(customerName, subtotal, tax, shippingFee,
                    totalAmount, paymentMethod, cashReceived, change);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing transaction: {ex.Message}", "Transaction Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Database persistence

        private void SaveDeliveryTransactionToDatabase(
            string customerName,
            decimal subtotal,
            decimal tax,
            decimal shippingFee,
            decimal totalAmount,
            string paymentMethod,
            decimal cashReceived,
            decimal change)
        {
            var cartItems = SharedCartManager.Instance.GetItems();
            if (cartItems == null || cartItems.Count == 0)
            {
                MessageBox.Show("Cart is empty. Please add items before checking out.",
                    "Checkout Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();

                    try
                    {
                        int customerId = GetOrCreateCustomer(connection, transaction, customerName);
                        var (cashierId, _) = ResolveAuditUser();
                        string transactionIdentifier = $"TRX-{DateTime.Now:yyyyMMddHHmmssfff}";

                        const string insertTransactionQuery = @"
                            INSERT INTO Transactions (
                                TransactionID,
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
                                @TransactionID,
                                GETDATE(),
                                @CustomerId,
                                @TotalAmount,
                                @Cashier,
                                @PaymentMethod,
                                @CashReceived,
                                @ChangeAmount,
                                NULL
                            );";

                        var transactionCmd = new SqlCommand(insertTransactionQuery, connection, transaction);
                        transactionCmd.Parameters.AddWithValue("@TransactionID", transactionIdentifier);
                        transactionCmd.Parameters.AddWithValue("@CustomerId", customerId);
                        transactionCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        transactionCmd.Parameters.AddWithValue("@Cashier", cashierId);
                        transactionCmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                        transactionCmd.Parameters.AddWithValue("@CashReceived", cashReceived);
                        transactionCmd.Parameters.AddWithValue("@ChangeAmount", change);

                        int transactionId = (int)transactionCmd.ExecuteScalar();

                        LogAuditEntry(connection, transaction,
                            $"Created delivery transaction {transactionIdentifier}",
                            AuditActivityType.CREATE,
                            "Transactions",
                            transactionId.ToString(),
                            null,
                            $"customer_id={customerId};total_amount={totalAmount};payment_method={paymentMethod};cash_received={cashReceived};change_amount={change}");

                        string deliveryNumber = GenerateDeliveryNumber(connection, transaction);
                        var deliveryHeader = InsertDeliveryHeader(connection, transaction, transactionId, customerName, deliveryNumber);
                        int deliveryId = deliveryHeader.deliveryId;

                        foreach (var item in cartItems)
                        {
                            int transactionItemId = InsertTransactionItem(
                                connection, transaction, transactionId,
                                item.ProductInternalId, item.Quantity, item.UnitPrice);

                            var deliveryItem = InsertDeliveryItem(
                                connection, transaction, deliveryId,
                                item.ProductInternalId, item.Quantity);

                            var updateStockCmd = new SqlCommand(@"
                                UPDATE Products
                                SET current_stock = current_stock - @Quantity
                                WHERE ProductInternalID = @ProductId
                                  AND current_stock >= @Quantity;",
                                connection, transaction);

                            updateStockCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                            updateStockCmd.Parameters.AddWithValue("@ProductId", item.ProductInternalId);

                            int updated = updateStockCmd.ExecuteNonQuery();
                            if (updated == 0)
                            {
                                throw new InvalidOperationException($"Insufficient stock for {item.Name}.");
                            }

                            LogAuditEntry(connection, transaction,
                                $"Added delivery item to transaction {transactionIdentifier}",
                                AuditActivityType.CREATE,
                                "TransactionItems",
                                transactionItemId.ToString(),
                                null,
                                $"transaction_id={transactionId};product_id={item.ProductInternalId};quantity={item.Quantity};selling_price={item.UnitPrice}");

                            LogAuditEntry(connection, transaction,
                                $"Created delivery line item for delivery {deliveryHeader.deliveryCode}",
                                AuditActivityType.CREATE,
                                "DeliveryItems",
                                deliveryItem.recordId,
                                null,
                                $"delivery_id={deliveryId};product_id={item.ProductInternalId};quantity_received={item.Quantity}");
                        }

                        InsertVehicleAssignments(connection, transaction, deliveryId);
                        UpdateTransactionDeliveryLink(connection, transaction, transactionId, deliveryId, deliveryHeader.deliveryCode);

                        transaction.Commit();

                        MessageBox.Show(
                            $"Delivery transaction saved successfully!\n\n" +
                            $"Transaction ID: {transactionIdentifier}\n" +
                            $"Customer: {customerName}\n" +
                            $"Vehicles: {selectedVehicles.Count}\n" +
                            $"Payment Method: {paymentMethod}\n" +
                            $"Total: ₱{totalAmount:N2}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SharedCartManager.Instance.ClearCart();
                        LoadSharedCartItems();
                        ReloadInventoryList();
                        ClearVehicleSelection();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Checkout failed. No changes were saved.\n\nDetails: {ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateDeliveryNumber(SqlConnection connection, SqlTransaction transaction)
        {
            const string query = @"
                SELECT CONCAT('DEL-', FORMAT(ISNULL(MAX(delivery_id), 0) + 1, '00000'))
                FROM Deliveries WITH (UPDLOCK, HOLDLOCK)";

            using (var cmd = new SqlCommand(query, connection, transaction))
            {
                return cmd.ExecuteScalar().ToString();
            }
        }

        private (int deliveryId, string deliveryCode) InsertDeliveryHeader(
            SqlConnection connection,
            SqlTransaction transaction,
            int transactionId,
            string customerName,
            string deliveryNumber)
        {
            const string insertDeliveryQuery = @"
                INSERT INTO Deliveries (
                    transaction_id,
                    po_id,
                    delivery_number,
                    delivery_date,
                    status,
                    received_by,
                    delivery_type,
                    customer_name,
                    delivery_address,
                    contact_number,
                    notes
                )
                OUTPUT inserted.delivery_id, inserted.DeliveryID
                VALUES (
                    @TransactionId,
                    NULL,
                    @DeliveryNumber,
                    GETDATE(),
                    @Status,
                    @ReceivedBy,
                    @DeliveryType,
                    @CustomerName,
                    @DeliveryAddress,
                    @ContactNumber,
                    @Notes
                );";

            using (var deliveryCmd = new SqlCommand(insertDeliveryQuery, connection, transaction))
            {
                int? receiverId = UserSession.UserId > 0 ? UserSession.UserId : (int?)null;

                deliveryCmd.Parameters.AddWithValue("@TransactionId", transactionId);
                deliveryCmd.Parameters.AddWithValue("@DeliveryNumber", deliveryNumber);
                deliveryCmd.Parameters.AddWithValue("@Status", "Scheduled");
                deliveryCmd.Parameters.AddWithValue("@ReceivedBy", receiverId.HasValue ? (object)receiverId.Value : DBNull.Value);
                deliveryCmd.Parameters.AddWithValue("@DeliveryType", "Sales_Delivery");
                deliveryCmd.Parameters.AddWithValue("@CustomerName",
                    string.IsNullOrWhiteSpace(customerName) ? (object)DBNull.Value : customerName);
                deliveryCmd.Parameters.AddWithValue("@DeliveryAddress", DBNull.Value);
                deliveryCmd.Parameters.AddWithValue("@ContactNumber", DBNull.Value);
                deliveryCmd.Parameters.AddWithValue("@Notes", DBNull.Value);

                using (var reader = deliveryCmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("Failed to create delivery header.");
                    }

                    int deliveryId = reader.GetInt32(0);
                    string deliveryCode = reader.IsDBNull(1) ? deliveryId.ToString() : reader.GetString(1);

                    LogAuditEntry(connection, transaction,
                        $"Created delivery record for transaction TRX-{transactionId:D5}",
                        AuditActivityType.CREATE,
                        "Deliveries",
                        deliveryCode,
                        null,
                        $"transaction_id={transactionId};delivery_number={deliveryNumber};status=Scheduled");

                    return (deliveryId, deliveryCode);
                }
            }
        }

        private int InsertTransactionItem(
            SqlConnection connection,
            SqlTransaction transaction,
            int transactionId,
            int productId,
            int quantity,
            decimal price)
        {
            const string insertItemQuery = @"
                INSERT INTO TransactionItems (transaction_id, product_id, quantity, selling_price)
                VALUES (@TransactionId, @ProductId, @Quantity, @SellingPrice);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            var itemCmd = new SqlCommand(insertItemQuery, connection, transaction);
            itemCmd.Parameters.AddWithValue("@TransactionId", transactionId);
            itemCmd.Parameters.AddWithValue("@ProductId", productId);
            itemCmd.Parameters.AddWithValue("@Quantity", quantity);
            itemCmd.Parameters.AddWithValue("@SellingPrice", price);

            return (int)itemCmd.ExecuteScalar();
        }

        private (int internalId, string recordId) InsertDeliveryItem(
            SqlConnection connection,
            SqlTransaction transaction,
            int deliveryId,
            int productId,
            int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity received must be greater than zero.");

            const string insertDeliveryItemQuery = @"
                INSERT INTO DeliveryItems (delivery_id, product_id, quantity_received, batch_number)
                OUTPUT inserted.del_item_id, inserted.DelItemID
                VALUES (@DeliveryId, @ProductId, @Quantity, NULL);";

            using (var deliveryItemCmd = new SqlCommand(insertDeliveryItemQuery, connection, transaction))
            {
                deliveryItemCmd.Parameters.AddWithValue("@DeliveryId", deliveryId);
                deliveryItemCmd.Parameters.AddWithValue("@ProductId", productId);
                deliveryItemCmd.Parameters.AddWithValue("@Quantity", quantity);

                using (var reader = deliveryItemCmd.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new InvalidOperationException("Failed to insert delivery item.");

                    int id = reader.GetInt32(0);
                    string code = reader.IsDBNull(1) ? id.ToString() : reader.GetString(1);
                    return (id, code);
                }
            }
        }

        private void InsertVehicleAssignments(
            SqlConnection connection,
            SqlTransaction transaction,
            int deliveryId)
        {
            foreach (var vehicle in selectedVehicles)
            {
                if (vehicle == null || vehicle.VehicleId <= 0)
                    continue;

                string previousStatus = "Unknown";

                using (var statusCmd = new SqlCommand(
                           "SELECT status FROM Vehicles WHERE vehicle_id = @VehicleId",
                           connection, transaction))
                {
                    statusCmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
                    object statusResult = statusCmd.ExecuteScalar();
                    previousStatus = statusResult?.ToString() ?? previousStatus;
                }

                const string insertAssignmentQuery = @"
                    INSERT INTO VehicleAssignments (delivery_id, vehicle_id, driver_name, assignment_date, status)
                    OUTPUT inserted.assignment_id, inserted.AssignmentID
                    VALUES (@DeliveryId, @VehicleId, @DriverName, GETDATE(), 'Assigned');";

                int assignmentId;
                string assignmentCode;

                using (var assignmentCmd = new SqlCommand(insertAssignmentQuery, connection, transaction))
                {
                    assignmentCmd.Parameters.AddWithValue("@DeliveryId", deliveryId);
                    assignmentCmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
                    assignmentCmd.Parameters.AddWithValue("@DriverName", "Unassigned");

                    using (var reader = assignmentCmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new InvalidOperationException("Failed to insert vehicle assignment.");

                        assignmentId = reader.GetInt32(0);
                        assignmentCode = reader.IsDBNull(1) ? assignmentId.ToString() : reader.GetString(1);
                    }
                }

                const string updateVehicleQuery = "UPDATE Vehicles SET status = 'In Use' WHERE vehicle_id = @VehicleId";
                using (var vehicleCmd = new SqlCommand(updateVehicleQuery, connection, transaction))
                {
                    vehicleCmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
                    vehicleCmd.ExecuteNonQuery();
                }

                LogAuditEntry(connection, transaction,
                    $"Assigned vehicle {vehicle.VehicleId} to delivery {deliveryId}",
                    AuditActivityType.CREATE,
                    "VehicleAssignments",
                    assignmentCode,
                    null,
                    $"delivery_id={deliveryId};vehicle_id={vehicle.VehicleId}");

                LogAuditEntry(connection, transaction,
                    $"Updated vehicle {vehicle.VehicleId} status to In Use",
                    AuditActivityType.UPDATE,
                    "Vehicles",
                    vehicle.VehicleId.ToString(),
                    $"status={previousStatus}",
                    "status=In Use");
            }
        }

        private void UpdateTransactionDeliveryLink(
            SqlConnection connection,
            SqlTransaction transaction,
            int transactionId,
            int deliveryId,
            string deliveryCode)
        {
            const string updateTransactionQuery = @"
                UPDATE Transactions
                SET delivery_id = @DeliveryId
                WHERE transaction_id = @TransactionId";

            var updateTransactionCmd = new SqlCommand(updateTransactionQuery, connection, transaction);
            updateTransactionCmd.Parameters.AddWithValue("@DeliveryId", deliveryId);
            updateTransactionCmd.Parameters.AddWithValue("@TransactionId", transactionId);
            updateTransactionCmd.ExecuteNonQuery();

            LogAuditEntry(connection, transaction,
                $"Linked transaction TRX-{transactionId:D5} to delivery {deliveryCode}",
                AuditActivityType.UPDATE,
                "Transactions",
                transactionId.ToString(),
                "delivery_id=null",
                $"delivery_id={deliveryId}");
        }

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

        #endregion

        #region Helpers / audit

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
                AuditModule.DELIVERIES,
                activity,
                activityType,
                tableAffected,
                recordId,
                oldValues,
                newValues);
        }

        #endregion
    }
}
