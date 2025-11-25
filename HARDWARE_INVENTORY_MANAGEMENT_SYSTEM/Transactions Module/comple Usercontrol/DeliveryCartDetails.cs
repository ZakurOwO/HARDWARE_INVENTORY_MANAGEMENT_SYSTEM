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

        public DeliveryCartDetails()
        {
            InitializeComponent();
            vehicleDataAccess = new VehicleDataAccess();
            connectionString = ConnectionString.DataSource;
            InitializeDataGridViewColumns();
        }

        private void InitializeDataGridViewColumns()
        {
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

            dgvCartDetails.AutoGenerateColumns = false;
            dgvCartDetails.AllowUserToAddRows = false;
            dgvCartDetails.RowHeadersVisible = false;
            dgvCartDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void DeliveryCartDetails_Load(object sender, EventArgs e)
        {
            LoadVehicles();
            dgvCartDetails.ClearSelection();
            SetupNumericUpDown();
            LoadSharedCartItems();
        }

        #region Vehicle Multi-Selection

        private void LoadVehicles()
        {
            try
            {
                availableVehicles = vehicleDataAccess.GetVehiclesByStatus("Available");
                CreateMultiVehicleSelector();
                Console.WriteLine($"✓ Loaded {availableVehicles.Count} available vehicles");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateMultiVehicleSelector()
        {
            // Remove the old button dropdown if exists
            if (btnDropdown != null)
            {
                this.Controls.Remove(btnDropdown);
                btnDropdown.Dispose();
            }

            // Create custom multi-select button
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

            // Create the dropdown panel (hidden by default)
            CreateVehicleSelectionPanel();
        }

        private void CreateVehicleSelectionPanel()
        {
            // Create panel for vehicle selection
            vehicleSelectionPanel = new Panel();
            vehicleSelectionPanel.Name = "vehicleSelectionPanel";
            vehicleSelectionPanel.BorderStyle = BorderStyle.FixedSingle;
            vehicleSelectionPanel.BackColor = Color.White;
            vehicleSelectionPanel.Location = new Point(80, 78);
            vehicleSelectionPanel.Size = new Size(193, 200);
            vehicleSelectionPanel.Visible = false;
            vehicleSelectionPanel.AutoScroll = true;

            // Create CheckedListBox
            vehicleCheckedListBox = new CheckedListBox();
            vehicleCheckedListBox.Name = "vehicleCheckedListBox";
            vehicleCheckedListBox.Dock = DockStyle.Fill;
            vehicleCheckedListBox.BorderStyle = BorderStyle.None;
            vehicleCheckedListBox.BackColor = Color.White;
            vehicleCheckedListBox.Font = new Font("Lexend Light", 8.25F);
            vehicleCheckedListBox.CheckOnClick = true;
            vehicleCheckedListBox.ItemCheck += VehicleCheckedListBox_ItemCheck;

            // Populate with vehicles
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
            // Toggle visibility of vehicle selection panel
            vehicleSelectionPanel.Visible = !vehicleSelectionPanel.Visible;

            if (vehicleSelectionPanel.Visible)
            {
                vehicleSelectionPanel.BringToFront();
            }
        }

        private void VehicleCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Use BeginInvoke to ensure the CheckedItems collection is updated
            this.BeginInvoke(new Action(() =>
            {
                UpdateSelectedVehicles();
                UpdateVehicleButtonText();
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

            Console.WriteLine($"✓ Selected {selectedVehicles.Count} vehicle(s)");
            foreach (var vehicle in selectedVehicles)
            {
                Console.WriteLine($"  - {vehicle.Brand} {vehicle.Model} ({vehicle.PlateNumber})");
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

        // Get all selected vehicles
        public List<VehicleRecord> GetSelectedVehicles()
        {
            return new List<VehicleRecord>(selectedVehicles);
        }

        // Get vehicle IDs as comma-separated string
        public string GetSelectedVehicleIds()
        {
            return string.Join(", ", selectedVehicles.Select(v => v.VehicleID));
        }

        // Check if any vehicles are selected
        public bool HasSelectedVehicles()
        {
            return selectedVehicles.Count > 0;
        }

        // Refresh vehicles list
        public void RefreshVehicles()
        {
            LoadVehicles();
        }

        #endregion

        #region Numeric UpDown & Quantity Management

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

                    // Update shared cart
                    string productName = dgvCartDetails.Rows[rowIndex].Cells["ItemName"].Value?.ToString();
                    int newQuantity = Convert.ToInt32(qtyUpDown.Value);

                    if (!string.IsNullOrEmpty(productName))
                    {
                        SharedCartManager.Instance.UpdateItemQuantity(productName, newQuantity);
                    }

                    UpdateCartTotals();
                }
            }
        }

        #endregion

        #region Cart Operations

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
            // Base shipping fee
            decimal baseFee = 100.00m;

            // Additional fee per extra vehicle (if multiple vehicles)
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

        public void AddItemToCart(string itemName, decimal price, int quantity = 1)
        {
            Console.WriteLine($"AddItemToCart called: {itemName}, {price}, {quantity}");

            // Add to shared cart first
            SharedCartManager.Instance.AddItemToCart(new CartItem
            {
                ProductName = itemName,
                Price = price,
                Quantity = quantity
            });

            // Then update local DataGridView
            LoadSharedCartItems();
        }

        public void ClearCart()
        {
            dgvCartDetails.Rows.Clear();
            SharedCartManager.Instance.ClearCart();
            UpdateCartTotals();
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

        public DataTable GetCartData()
        {
            DataTable cartData = new DataTable();
            cartData.Columns.Add("ProductName", typeof(string));
            cartData.Columns.Add("Quantity", typeof(int));
            cartData.Columns.Add("Price", typeof(decimal));
            cartData.Columns.Add("Total", typeof(decimal));

            foreach (DataGridViewRow row in dgvCartDetails.Rows)
            {
                if (row.IsNewRow) continue;

                string productName = row.Cells["ItemName"].Value?.ToString() ?? "";
                int quantity = Convert.ToInt32(row.Cells["Quantity"].Value ?? 0);
                string priceStr = row.Cells["Price"].Value?.ToString() ?? "₱0.00";
                priceStr = priceStr.Replace("₱", "").Trim();
                decimal price = decimal.Parse(priceStr);
                decimal total = quantity * price;

                cartData.Rows.Add(productName, quantity, price, total);
            }

            return cartData;
        }

        private void LoadSharedCartItems()
        {
            dgvCartDetails.Rows.Clear();
            var sharedItems = SharedCartManager.Instance.GetCartItems();

            foreach (var item in sharedItems)
            {
                dgvCartDetails.Rows.Add(item.ProductName, item.Quantity, $"₱{item.Price:N2}");
            }
            UpdateCartTotals();
        }

        #endregion

        #region Validation

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

        #endregion
    }
}