using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries;
using System.Data.SqlClient;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.ClassComponentTransaction;
using CartItemModel = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models.CartItem;


namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class DeliveryCartDetails : UserControl
    {
        NumericUpDown qtyUpDown = new NumericUpDown();
        private VehicleDataAccess vehicleDataAccess;
        private List<VehicleRecord> availableVehicles;
        private List<VehicleRecord> selectedVehicles = new List<VehicleRecord>();
        private Panel vehicleSelectionPanel;
        private CheckedListBox vehicleCheckedListBox;
        private Guna.UI2.WinForms.Guna2Button btnMultiSelectVehicles;
        private string connectionString;
        private CheckoutPopUpContainer checkoutContainer;

        public DeliveryCartDetails()
        {
            InitializeComponent();
            vehicleDataAccess = new VehicleDataAccess();
            connectionString = ConnectionString.DataSource;
            checkoutContainer = new CheckoutPopUpContainer();
            InitializeDataGridViewColumns();
        }

        private void InitializeDataGridViewColumns()
        {
            dgvCartDetails.Columns.Clear();

            DataGridViewTextBoxColumn productIdColumn = new DataGridViewTextBoxColumn();
            productIdColumn.Name = "ProductInternalID";
            productIdColumn.HeaderText = "Product ID";
            productIdColumn.DataPropertyName = "ProductInternalID";
            productIdColumn.Visible = false;
            dgvCartDetails.Columns.Add(productIdColumn);

            DataGridViewTextBoxColumn itemNameColumn = new DataGridViewTextBoxColumn();
            itemNameColumn.Name = "ItemName";
            itemNameColumn.HeaderText = "ITEM";
            itemNameColumn.DataPropertyName = "ItemName";
            itemNameColumn.Width = 200;
            itemNameColumn.ReadOnly = true;
            dgvCartDetails.Columns.Add(itemNameColumn);

            DataGridViewTextBoxColumn quantityColumn = new DataGridViewTextBoxColumn();
            quantityColumn.Name = "Quantity";
            quantityColumn.HeaderText = "QTY";
            quantityColumn.DataPropertyName = "Quantity";
            quantityColumn.Width = 80;
            quantityColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCartDetails.Columns.Add(quantityColumn);

            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();
            priceColumn.Name = "Price";
            priceColumn.HeaderText = "PRICE";
            priceColumn.DataPropertyName = "Price";
            priceColumn.Width = 100;
            priceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            priceColumn.ReadOnly = true;
            dgvCartDetails.Columns.Add(priceColumn);

            DataGridViewImageColumn deleteColumn = new DataGridViewImageColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "";
            deleteColumn.Width = 42;
            deleteColumn.Image = Properties.Resources.trash;
            deleteColumn.DefaultCellStyle.NullValue = null;
            deleteColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgvCartDetails.Columns.Add(deleteColumn);

            dgvCartDetails.AutoGenerateColumns = false;
            dgvCartDetails.AllowUserToAddRows = false;
            dgvCartDetails.RowHeadersVisible = false;
            dgvCartDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void DeliveryCartDetails_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadVehicles();
            dgvCartDetails.ClearSelection();
            SetupNumericUpDown();
            LoadSharedCartItems();
        }

        private void LoadCustomers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT customer_name FROM Customers ORDER BY customer_name";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    cbxChooseCustomer.Items.Clear();
                    while (reader.Read())
                    {
                        cbxChooseCustomer.Items.Add(reader["customer_name"].ToString());
                    }
                    reader.Close();

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

        private void CreateMultiVehicleSelector()
        {
            if (btnDropdown != null)
            {
                this.Controls.Remove(btnDropdown);
                btnDropdown.Dispose();
            }

            btnMultiSelectVehicles = new Guna.UI2.WinForms.Guna2Button();
            btnMultiSelectVehicles.Name = "btnMultiSelectVehicles";
            btnMultiSelectVehicles.BorderColor = Color.Gainsboro;
            btnMultiSelectVehicles.BorderRadius = 5;
            btnMultiSelectVehicles.BorderThickness = 1;
            btnMultiSelectVehicles.FillColor = Color.White;
            btnMultiSelectVehicles.Font = new Font("Lexend Light", 8.25F);
            btnMultiSelectVehicles.ForeColor = Color.FromArgb(68, 88, 112);
            btnMultiSelectVehicles.Image = Properties.Resources.direction_down_01;
            btnMultiSelectVehicles.ImageAlign = HorizontalAlignment.Right;
            btnMultiSelectVehicles.Location = new Point(80, 48);
            btnMultiSelectVehicles.Size = new Size(193, 29);
            btnMultiSelectVehicles.TabIndex = 22;
            btnMultiSelectVehicles.Text = "Select Vehicles";
            btnMultiSelectVehicles.TextAlign = HorizontalAlignment.Left;
            btnMultiSelectVehicles.Click += BtnMultiSelectVehicles_Click;

            this.Controls.Add(btnMultiSelectVehicles);
            btnMultiSelectVehicles.BringToFront();

            CreateVehicleSelectionPanel();
        }

        private void CreateVehicleSelectionPanel()
        {
            vehicleSelectionPanel = new Panel();
            vehicleSelectionPanel.Name = "vehicleSelectionPanel";
            vehicleSelectionPanel.BorderStyle = BorderStyle.FixedSingle;
            vehicleSelectionPanel.BackColor = Color.White;
            vehicleSelectionPanel.Location = new Point(80, 78);
            vehicleSelectionPanel.Size = new Size(193, 200);
            vehicleSelectionPanel.Visible = false;
            vehicleSelectionPanel.AutoScroll = true;

            vehicleCheckedListBox = new CheckedListBox();
            vehicleCheckedListBox.Name = "vehicleCheckedListBox";
            vehicleCheckedListBox.Dock = DockStyle.Fill;
            vehicleCheckedListBox.BorderStyle = BorderStyle.None;
            vehicleCheckedListBox.BackColor = Color.White;
            vehicleCheckedListBox.Font = new Font("Lexend Light", 8.25F);
            vehicleCheckedListBox.CheckOnClick = true;
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
            this.Controls.Add(vehicleSelectionPanel);
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
            this.BeginInvoke(new Action(() =>
            {
                UpdateSelectedVehicles();
                UpdateVehicleButtonText();
                UpdateCartTotals();
            }));
        }

        private void UpdateSelectedVehicles()
        {
            selectedVehicles.Clear();
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
            if (btnMultiSelectVehicles != null)
            {
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

                      int productId = GetProductIdFromRow(rowIndex);
                      int newQuantity = Convert.ToInt32(qtyUpDown.Value);

                      if (productId > 0)
                      {
                          SharedCartManager.Instance.UpdateItemQuantity(productId, newQuantity);
                      }

                      UpdateCartTotals();
                  }
              }
          }

        private void dgvCartDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCartDetails.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to remove this item from the cart?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int productId = GetProductIdFromRow(e.RowIndex);
                    if (productId > 0)
                    {
                        SharedCartManager.Instance.RemoveItemFromCart(productId);
                    }
                    dgvCartDetails.Rows.RemoveAt(e.RowIndex);
                    UpdateCartTotals();
                }
            }
        }

        private void UpdateCartTotals()
        {
            decimal subtotal = CalculateSubtotal();
            decimal taxRate = 0.12m;
            decimal tax = subtotal * taxRate;
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

        private decimal CalculateShippingFee()
        {
            decimal baseFee = 100.00m;
            if (selectedVehicles.Count > 1)
            {
                baseFee += (selectedVehicles.Count - 1) * 50.00m;
            }
            return baseFee;
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
                              AddItemToCart(productInternalId, productName, sellingPrice, quantity);
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

        public void AddItemToCart(int productInternalId, string itemName, decimal price, int quantity = 1)
        {
            if (productInternalId <= 0)
            {
                MessageBox.Show("Invalid product selected.", "Product Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (quantity <= 0)
            {
                MessageBox.Show("Quantity must be at least 1.", "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SharedCartManager.Instance.AddItemToCart(new CartItemModel
            {
                ProductInternalID = productInternalId,
                ProductName = itemName,
                Price = price,
                Quantity = quantity
            });

            LoadSharedCartItems();
        }
        public void ClearCart()
        {
            dgvCartDetails.Rows.Clear();
            SharedCartManager.Instance.ClearCart();
            UpdateCartTotals();
        }

          private void LoadSharedCartItems()
          {
              dgvCartDetails.Rows.Clear();
              var sharedItems = SharedCartManager.Instance.GetCartItems();

              foreach (var item in sharedItems)
              {
                  dgvCartDetails.Rows.Add(item.ProductInternalID, item.ProductName, item.Quantity, $"₱{item.Price:N2}");
              }
              UpdateCartTotals();
          }

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
            Control parent = this.Parent;
            while (parent != null)
            {
                if (parent is MainDashBoard mainForm)
                    return mainForm;
                parent = parent.Parent;
            }
            return null;
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

        public List<VehicleRecord> GetSelectedVehicles()
        {
            return selectedVehicles;
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

                SaveDeliveryTransactionToDatabase(customerName, subtotal, tax, shippingFee, totalAmount, paymentMethod, cashReceived, change);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing transaction: {ex.Message}", "Transaction Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private decimal CalculateTax(decimal subtotal)
        {
            return subtotal * 0.12m;
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (DataGridViewRow row in dgvCartDetails.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int productId = GetProductIdFromRow(row.Index);
                        int cartQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                        if (productId <= 0)
                        {
                            MessageBox.Show("Invalid product in cart.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        string query = "SELECT current_stock FROM Products WHERE ProductInternalID = @ProductId AND active = 1";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ProductId", productId);

                        var result = command.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("Product not found in database.",
                                "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        int availableStock = Convert.ToInt32(result);
                        if (cartQuantity > availableStock)
                        {
                            MessageBox.Show($"Insufficient stock. Available: {availableStock}, Requested: {cartQuantity}",
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

        private void SaveDeliveryTransactionToDatabase(string customerName, decimal subtotal, decimal tax, decimal shippingFee, decimal totalAmount, string paymentMethod, decimal cashReceived, decimal change)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        int customerId = GetOrCreateCustomer(connection, transaction, customerName);
                        var (cashierId, _) = ResolveAuditUser();

                        // Insert transaction with payment info
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

                        SqlCommand transactionCmd = new SqlCommand(insertTransactionQuery, connection, transaction);
                        transactionCmd.Parameters.AddWithValue("@CustomerId", customerId);
                        transactionCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        transactionCmd.Parameters.AddWithValue("@Cashier", cashierId);
                        transactionCmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                        transactionCmd.Parameters.AddWithValue("@CashReceived", cashReceived);
                        transactionCmd.Parameters.AddWithValue("@ChangeAmount", change);

                        int transactionId = (int)transactionCmd.ExecuteScalar();

                        LogAuditEntry(connection, transaction,
                            $"Created delivery transaction TRX-{transactionId:D5}",
                            AuditActivityType.CREATE,
                            "Transactions",
                            transactionId.ToString(),
                            null,
                            $"{{\"customer_id\":{customerId},\"total_amount\":{totalAmount},\"payment_method\":\"{paymentMethod}\",\"cash_received\":{cashReceived},\"change_amount\":{change}}}");

                        string deliveryNumber = GenerateDeliveryNumber(connection, transaction);
                        var deliveryHeader = InsertDeliveryHeader(connection, transaction, transactionId, customerName, deliveryNumber);
                        int deliveryId = deliveryHeader.deliveryId;

                        // Save transaction and delivery items
                        foreach (DataGridViewRow row in dgvCartDetails.Rows)
                        {
                            if (row.IsNewRow) continue;

                              int productId = GetProductIdFromRow(row.Index);
                              int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                              decimal price = ParsePrice(row.Cells["Price"].Value);

                              if (productId <= 0)
                              {
                                  throw new InvalidOperationException("Invalid product selected for transaction item.");
                              }

                              if (quantity <= 0)
                              {
                                  throw new InvalidOperationException("Quantity received must be greater than zero.");
                              }

                              int oldStock = GetCurrentStock(connection, transaction, productId);

                              int transactionItemId = InsertTransactionItem(connection, transaction, transactionId, productId, quantity, price);
                              var deliveryItem = InsertDeliveryItem(connection, transaction, deliveryId, productId, quantity);

                              LogAuditEntry(connection, transaction,
                                  $"Added delivery item to transaction TRX-{transactionId:D5}",
                                  AuditActivityType.CREATE,
                                  "TransactionItems",
                                  transactionItemId.ToString(),
                                  null,
                                  $"{{\"transaction_id\":{transactionId},\"product_id\":{productId},\"quantity\":{quantity},\"selling_price\":{price}}}");

                              LogAuditEntry(connection, transaction,
                                  $"Created delivery line item for delivery {deliveryHeader.deliveryCode}",
                                  AuditActivityType.CREATE,
                                  "DeliveryItems",
                                  deliveryItem.recordId,
                                  null,
                                  $"delivery_id={deliveryId};product_id={productId};quantity_received={quantity}");

                              UpdateProductStock(connection, transaction, productId, quantity, oldStock);
                          }

                        InsertVehicleAssignments(connection, transaction, deliveryId);

                        UpdateTransactionDeliveryLink(connection, transaction, transactionId, deliveryId, deliveryHeader.deliveryCode);

                        transaction.Commit();

                        MessageBox.Show($"Delivery transaction saved successfully!\n\nTransaction ID: TRX-{transactionId:D5}\nCustomer: {customerName}\nVehicles: {selectedVehicles.Count}\nPayment Method: {paymentMethod}\nTotal: ₱{totalAmount:N2}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearCart();
                        ClearVehicleSelection();
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
                MessageBox.Show($"Error saving delivery transaction: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateDeliveryNumber(SqlConnection connection, SqlTransaction transaction)
        {
            string query = @"
                SELECT CONCAT('DEL-', FORMAT(ISNULL(MAX(delivery_id), 0) + 1, '00000'))
                FROM Deliveries WITH (UPDLOCK, HOLDLOCK)";

            using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
            {
                return cmd.ExecuteScalar().ToString();
            }
        }

        private (int deliveryId, string deliveryCode) InsertDeliveryHeader(SqlConnection connection, SqlTransaction transaction, int transactionId, string customerName, string deliveryNumber)
        {
            string insertDeliveryQuery = @"
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

            using (SqlCommand deliveryCmd = new SqlCommand(insertDeliveryQuery, connection, transaction))
            {
                int? receiverId = UserSession.UserId > 0 ? UserSession.UserId : (int?)null;

                deliveryCmd.Parameters.AddWithValue("@TransactionId", transactionId);
                deliveryCmd.Parameters.AddWithValue("@DeliveryNumber", deliveryNumber);
                deliveryCmd.Parameters.AddWithValue("@Status", "Scheduled");
                deliveryCmd.Parameters.AddWithValue("@ReceivedBy", receiverId.HasValue ? (object)receiverId.Value : DBNull.Value);
                deliveryCmd.Parameters.AddWithValue("@DeliveryType", "Sales_Delivery");
                deliveryCmd.Parameters.AddWithValue("@CustomerName", string.IsNullOrWhiteSpace(customerName) ? (object)DBNull.Value : customerName);
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

        private int InsertTransactionItem(SqlConnection connection, SqlTransaction transaction, int transactionId, int productId, int quantity, decimal price)
        {
            string insertItemQuery = @"
                        INSERT INTO TransactionItems (transaction_id, product_id, quantity, selling_price)
                        VALUES (@TransactionId, @ProductId, @Quantity, @SellingPrice);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

            SqlCommand itemCmd = new SqlCommand(insertItemQuery, connection, transaction);
            itemCmd.Parameters.AddWithValue("@TransactionId", transactionId);
            itemCmd.Parameters.AddWithValue("@ProductId", productId);
            itemCmd.Parameters.AddWithValue("@Quantity", quantity);
            itemCmd.Parameters.AddWithValue("@SellingPrice", price);
            return (int)itemCmd.ExecuteScalar();
        }

        private (int internalId, string recordId) InsertDeliveryItem(SqlConnection connection, SqlTransaction transaction, int deliveryId, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity received must be greater than zero.");
            }

            string insertDeliveryItemQuery = @"
                        INSERT INTO DeliveryItems (delivery_id, product_id, quantity_received, batch_number)
                        OUTPUT inserted.del_item_id, inserted.DelItemID
                        VALUES (@DeliveryId, @ProductId, @Quantity, NULL);";

            using (SqlCommand deliveryItemCmd = new SqlCommand(insertDeliveryItemQuery, connection, transaction))
            {
                deliveryItemCmd.Parameters.AddWithValue("@DeliveryId", deliveryId);
                deliveryItemCmd.Parameters.AddWithValue("@ProductId", productId);
                deliveryItemCmd.Parameters.AddWithValue("@Quantity", quantity);

                using (var reader = deliveryItemCmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("Failed to insert delivery item.");
                    }

                    int id = reader.GetInt32(0);
                    string code = reader.IsDBNull(1) ? id.ToString() : reader.GetString(1);
                    return (id, code);
                }
            }
        }

        private void InsertVehicleAssignments(SqlConnection connection, SqlTransaction transaction, int deliveryId)
        {
            foreach (var vehicle in selectedVehicles)
            {
                if (vehicle == null || vehicle.VehicleId <= 0)
                {
                    continue;
                }

                string previousStatus = "Unknown";
                using (SqlCommand statusCmd = new SqlCommand("SELECT status FROM Vehicles WHERE vehicle_id = @VehicleId", connection, transaction))
                {
                    statusCmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
                    object statusResult = statusCmd.ExecuteScalar();
                    previousStatus = statusResult?.ToString() ?? previousStatus;
                }

                string insertAssignmentQuery = @"
        INSERT INTO VehicleAssignments (delivery_id, vehicle_id, driver_name, assignment_date, status)
        OUTPUT inserted.assignment_id, inserted.AssignmentID
        VALUES (@DeliveryId, @VehicleId, @DriverName, GETDATE(), 'Assigned');";

                SqlCommand assignmentCmd = new SqlCommand(insertAssignmentQuery, connection, transaction);
                assignmentCmd.Parameters.AddWithValue("@DeliveryId", deliveryId);
                assignmentCmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
                assignmentCmd.Parameters.AddWithValue("@DriverName", "Unassigned");
                int assignmentId;
                string assignmentCode;

                using (var reader = assignmentCmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("Failed to insert vehicle assignment.");
                    }

                    assignmentId = reader.GetInt32(0);
                    assignmentCode = reader.IsDBNull(1) ? assignmentId.ToString() : reader.GetString(1);
                }

                string updateVehicleQuery = "UPDATE Vehicles SET status = 'In Use' WHERE vehicle_id = @VehicleId";
                SqlCommand vehicleCmd = new SqlCommand(updateVehicleQuery, connection, transaction);
                vehicleCmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
                vehicleCmd.ExecuteNonQuery();

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

        private void UpdateTransactionDeliveryLink(SqlConnection connection, SqlTransaction transaction, int transactionId, int deliveryId, string deliveryCode)
        {
            string updateTransactionQuery = @"
                    UPDATE Transactions
                    SET delivery_id = @DeliveryId
                    WHERE transaction_id = @TransactionId";

            SqlCommand updateTransactionCmd = new SqlCommand(updateTransactionQuery, connection, transaction);
            updateTransactionCmd.Parameters.AddWithValue("@DeliveryId", deliveryId);
            updateTransactionCmd.Parameters.AddWithValue("@TransactionId", transactionId);
            updateTransactionCmd.ExecuteNonQuery();

            LogAuditEntry(connection, transaction,
                $"Linked transaction TRX-{transactionId:D5} to delivery {deliveryCode}",
                AuditActivityType.UPDATE,
                "Transactions",
                transactionId.ToString(),
                "{\"delivery_id\":null}",
                $"delivery_id={deliveryId}");
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

          private int GetProductIdFromRow(int rowIndex)
          {
              if (rowIndex < 0 || rowIndex >= dgvCartDetails.Rows.Count)
              {
                  return 0;
              }

              var value = dgvCartDetails.Rows[rowIndex].Cells["ProductInternalID"].Value;
              if (value == null)
              {
                  return 0;
              }

              return int.TryParse(value.ToString(), out int productId) ? productId : 0;
          }

          private decimal ParsePrice(object priceValue)
          {
              if (priceValue == null)
              {
                  return 0m;
              }

              string priceText = priceValue.ToString().Replace("₱", string.Empty).Trim();
              return decimal.TryParse(priceText, out decimal price) ? price : 0m;
          }

          private int GetCurrentStock(SqlConnection connection, SqlTransaction transaction, int productId)
          {
              string stockQuery = "SELECT current_stock FROM Products WHERE ProductInternalID = @ProductId";
              using (SqlCommand command = new SqlCommand(stockQuery, connection, transaction))
              {
                  command.Parameters.AddWithValue("@ProductId", productId);
                  object result = command.ExecuteScalar();
                  if (result == null)
                  {
                      throw new InvalidOperationException("Unable to resolve current stock for product.");
                  }

                  return Convert.ToInt32(result);
              }
          }

          private void UpdateProductStock(SqlConnection connection, SqlTransaction transaction, int productId, int quantity, int oldStock)
          {
              string updateStockQuery = @"
                          UPDATE Products
                          SET current_stock = current_stock - @Quantity
                          WHERE ProductInternalID = @ProductId";

              using (SqlCommand stockCmd = new SqlCommand(updateStockQuery, connection, transaction))
              {
                  stockCmd.Parameters.AddWithValue("@Quantity", quantity);
                  stockCmd.Parameters.AddWithValue("@ProductId", productId);
                  stockCmd.ExecuteNonQuery();
              }

              int newStock = oldStock - quantity;
              LogAuditEntry(connection, transaction,
                  $"Updated stock for product ID {productId}",
                  AuditActivityType.UPDATE,
                  "Products",
                  productId.ToString(),
                  $"{{\"current_stock\":{oldStock}}}",
                  $"{{\"current_stock\":{newStock}}}");
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

        private (int userId, string username) ResolveAuditUser()
        {
            int userId = UserSession.UserId > 0 ? UserSession.UserId : 1;
            string username = !string.IsNullOrWhiteSpace(UserSession.Username) ? UserSession.Username : "System";
            return (userId, username);
        }

        private void LogAuditEntry(SqlConnection connection, SqlTransaction transaction, string activity, AuditActivityType activityType, string tableAffected, string recordId, string oldValues, string newValues)
        {
            AuditHelper.LogWithTransaction(connection, transaction, AuditModule.DELIVERIES, activity, activityType, tableAffected, recordId, oldValues, newValues);
        }

        public bool ValidateCheckout()
        {
            if (dgvCartDetails.Rows.Count == 0 || (dgvCartDetails.Rows.Count == 1 && dgvCartDetails.Rows[0].IsNewRow))
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
    }
}