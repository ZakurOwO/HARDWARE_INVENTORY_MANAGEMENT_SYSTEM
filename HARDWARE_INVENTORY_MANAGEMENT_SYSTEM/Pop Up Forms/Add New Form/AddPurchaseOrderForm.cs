using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public partial class AddPurchaseOrderForm : UserControl
    {
        private int maxHeight = 225;
        private SqlConnection con;
        private string connectionString = ConnectionString.DataSource;

        private readonly string _notesPlaceholder = "Additional Notes...";
        private readonly Color _placeholderColor = Color.Gray;
        private readonly Color _normalColor = Color.Black;
        public AddPurchaseOrderForm()
        {
            InitializeComponent();
            guna2Panel1.HorizontalScroll.Enabled = false;
            con = new SqlConnection(connectionString);

            kryptonRichTextBox2.Text = _notesPlaceholder;
            kryptonRichTextBox2.ForeColor = _placeholderColor;
            kryptonRichTextBox2.Enter += kryptonRichTextBox2_Enter;
            kryptonRichTextBox2.Leave += kryptonRichTextBox2_Leave;

            // Initialize form
            GeneratePONumber();
            LoadSuppliers();
            LoadProducts();
            LoadStatusOptions();
            LoadPaymentStatusOptions();
            LoadTaxOptions();

            // Set default values
            guna2DateTimePicker1.Value = DateTime.Now;
            ExpirationDataComboBox.Value = DateTime.Now.AddDays(7);
            guna2NumericUpDown1.Value = 1;
            guna2NumericUpDown1.Minimum = 1;
            guna2NumericUpDown2.DecimalPlaces = 2;
            guna2NumericUpDown3.Value = 12;
            guna2NumericUpDown3.DecimalPlaces = 2;

            // Make Order Number read-only
            CompanyNameTextBoxSupplier.ReadOnly = true;
            CompanyNameTextBoxSupplier.FillColor = Color.WhiteSmoke;

            // Disable product selection until supplier is selected
            cbxCategory.Enabled = false;
            guna2NumericUpDown1.Enabled = false;
            guna2NumericUpDown2.Enabled = false;
            btnAdd.Enabled = false;

            // Initialize DataGridView
            InitializeDataGridView();

            // Hook up supplier selection change event
            guna2ComboBox3.SelectedIndexChanged += Guna2ComboBox3_SelectedIndexChanged;

            // Hook up product dropdown click event
            cbxCategory.DropDown += CbxCategory_DropDown;

            guna2ComboBox2.SelectedIndexChanged += (s, e) => ApplyStatusBusinessRulesForAdd();

            // Hook up events for dynamic calculation
            guna2ComboBox1.SelectedIndexChanged += (s, e) => UpdateCalculations();
            guna2NumericUpDown3.ValueChanged += (s, e) => UpdateCalculations();

            // Initialize calculation display
            UpdateCalculations();
            GeneratePONumber();
            LoadSuppliers();
        }
        private void kryptonRichTextBox2_Enter(object sender, EventArgs e)
        {
            if (kryptonRichTextBox2.Text == _notesPlaceholder)
            {
                kryptonRichTextBox2.Text = string.Empty;
                kryptonRichTextBox2.ForeColor = _normalColor;
            }
        }

        private void kryptonRichTextBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(kryptonRichTextBox2.Text))
            {
                kryptonRichTextBox2.ForeColor = _placeholderColor;
                kryptonRichTextBox2.Text = _notesPlaceholder;
            }
        }

        private void Guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable product selection controls when supplier is selected
            if (guna2ComboBox3.SelectedIndex != -1)
            {
                cbxCategory.Enabled = true;
                guna2NumericUpDown1.Enabled = true;
                guna2NumericUpDown2.Enabled = true;
                btnAdd.Enabled = true;
            }
            else
            {
                cbxCategory.Enabled = false;
                guna2NumericUpDown1.Enabled = false;
                guna2NumericUpDown2.Enabled = false;
                btnAdd.Enabled = false;
            }
        }

        private void CbxCategory_DropDown(object sender, EventArgs e)
        {
            // Check if supplier is selected when trying to open product dropdown
            if (guna2ComboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a supplier first before selecting products.",
                    "Supplier Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // Prevent dropdown from opening
                ((ComboBox)sender).DroppedDown = false;

                // Focus on supplier combo box
                guna2ComboBox3.Focus();
            }
        }

        private void GeneratePONumber()
        {
            try
            {
                string datePrefix = DateTime.Now.ToString("yyyyMMdd");
                string poPrefix = $"PO-{datePrefix}-";

                string query = @"SELECT TOP 1 po_number FROM PurchaseOrders 
                       WHERE po_number LIKE @poPrefix 
                       ORDER BY po_id DESC";

                if (con.State == ConnectionState.Closed)
                    con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@poPrefix", poPrefix + "%");
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string lastPO = result.ToString();
                        string[] parts = lastPO.Split('-');

                        if (parts.Length == 3 && int.TryParse(parts[2], out int lastNumber))
                        {
                            int newNumber = lastNumber + 1;
                            CompanyNameTextBoxSupplier.Text = $"{poPrefix}{newNumber:D5}";
                        }
                        else
                        {
                            CompanyNameTextBoxSupplier.Text = $"{poPrefix}00001";
                        }
                    }
                    else
                    {
                        CompanyNameTextBoxSupplier.Text = $"{poPrefix}00001";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating PO number: " + ex.Message);
                CompanyNameTextBoxSupplier.Text = $"PO-{DateTime.Now:yyyyMMdd}-{DateTime.Now:HHmmss}";
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void InitializeDataGridView()
        {
            dgvPurchaseItems.CellContentClick += DgvPurchaseItems_CellContentClick;
        }

        private void DgvPurchaseItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvPurchaseItems.Columns["Action"].Index)
            {
                var result = MessageBox.Show("Remove this item?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    dgvPurchaseItems.Rows.RemoveAt(e.RowIndex);
                    UpdateCalculations();
                    AdjustLayout();
                }
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                string query = "SELECT supplier_id, supplier_name FROM Suppliers ORDER BY supplier_name";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    guna2ComboBox3.DisplayMember = "supplier_name";
                    guna2ComboBox3.ValueMember = "supplier_id";
                    guna2ComboBox3.DataSource = dt;
                    guna2ComboBox3.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void LoadProducts()
        {
            try
            {
                string query = @"SELECT ProductInternalID, product_name, SellingPrice 
                               FROM Products WHERE active = 1 ORDER BY product_name";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    cbxCategory.DisplayMember = "product_name";
                    cbxCategory.ValueMember = "ProductInternalID";
                    cbxCategory.DataSource = dt;
                    cbxCategory.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void LoadStatusOptions()
        {
            guna2ComboBox2.Items.Clear();
            guna2ComboBox2.Items.AddRange(new string[] { "Pending", "Approved", "Ordered", "Received" });
            guna2ComboBox2.SelectedIndex = 0;
        }

        private void LoadPaymentStatusOptions()
        {
            guna2ComboBox4.Items.AddRange(new string[] { "Unpaid", "Partial", "Paid" });
            guna2ComboBox4.SelectedIndex = 0;
        }

        private void LoadTaxOptions()
        {
            guna2ComboBox1.Items.AddRange(new string[] { "VAT (12%)", "No Tax (0%)", "Custom", "3%" });
            guna2ComboBox1.SelectedIndex = 0;
        }

        private void ApplyStatusBusinessRulesForAdd()
        {
            if (guna2ComboBox2.SelectedItem == null)
            {
                return;
            }

            switch (guna2ComboBox2.SelectedItem.ToString())
            {
                case "Approved":
                    if (ExpirationDataComboBox.Value < guna2DateTimePicker1.Value.AddDays(1))
                    {
                        ExpirationDataComboBox.Value = guna2DateTimePicker1.Value.AddDays(7);
                    }
                    break;
                case "Ordered":
                    if (ExpirationDataComboBox.Value < DateTime.Now)
                    {
                        ExpirationDataComboBox.Value = DateTime.Now.AddDays(7);
                    }
                    break;
                case "Received":
                    ExpirationDataComboBox.Value = DateTime.Now;
                    break;
            }
        }

        private void AdjustGridHeight()
        {
            int header = dgvPurchaseItems.ColumnHeadersHeight;
            int rowHeight = dgvPurchaseItems.RowTemplate.Height;
            int rowCount = dgvPurchaseItems.Rows.Count;

            int estimatedHeight = header + (rowCount * rowHeight) + 2;

            if (estimatedHeight < maxHeight)
            {
                dgvPurchaseItems.Height = estimatedHeight;
                dgvPurchaseItems.ScrollBars = ScrollBars.None;
            }
            else
            {
                dgvPurchaseItems.Height = maxHeight;
                dgvPurchaseItems.ScrollBars = ScrollBars.Vertical;
            }
        }

        private void AdjustLayout()
        {
            AdjustGridHeight();
            calculationPanel.Top = dgvPurchaseItems.Bottom + 10;

            int neededHeight = calculationPanel.Bottom + 15;
            parentPanel.Height = Math.Min(neededHeight, parentPanel.MaximumSize.Height);

            panel2.Top = parentPanel.Bottom + 20;
            ParentContainer.Height = panel2.Bottom + 20;
        }

        private void UpdateCalculations()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    string totalStr = row.Cells["Total"].Value.ToString().Replace(",", "");
                    if (decimal.TryParse(totalStr, out decimal rowTotal))
                    {
                        subtotal += rowTotal;
                    }
                }
            }

            decimal taxRate = 0;
            if (guna2ComboBox1.SelectedItem != null)
            {
                string taxOption = guna2ComboBox1.SelectedItem.ToString();

                if (taxOption == "VAT (12%)")
                {
                    taxRate = 0.12m;
                    guna2NumericUpDown3.Value = 12;
                    guna2NumericUpDown3.Enabled = false;
                }
                else if (taxOption == "No Tax (0%)")
                {
                    taxRate = 0;
                    guna2NumericUpDown3.Value = 0;
                    guna2NumericUpDown3.Enabled = false;
                }
                else if (taxOption == "Custom")
                {
                    taxRate = guna2NumericUpDown3.Value / 100;
                    guna2NumericUpDown3.Enabled = true;
                }
            }
            else
            {
                taxRate = guna2NumericUpDown3.Value / 100;
            }

            decimal tax = subtotal * taxRate;
            decimal shippingFee = 0;
            decimal grandTotal = subtotal + tax + shippingFee;

            label14.Text = "₱ " + subtotal.ToString("N2");
            label13.Text = "₱ " + tax.ToString("N2");
            label12.Text = "₱ " + shippingFee.ToString("N2");
            label11.Text = "₱ " + grandTotal.ToString("N2");
        }

        private bool ValidateForm()
        {
            if (guna2ComboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a supplier.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2ComboBox3.Focus();
                return false;
            }

            if (guna2ComboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a status.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2ComboBox2.Focus();
                return false;
            }

            if (dgvPurchaseItems.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one item to the purchase order.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
            {
                if (row.Cells["Quantity"].Value == null ||
                    !int.TryParse(row.Cells["Quantity"].Value.ToString(), out int qty) ||
                    qty <= 0)
                {
                    MessageBox.Show("Each item must have a valid quantity greater than 0.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (row.Cells["UnitPrice"].Value == null ||
                    !TryParseCurrency(row.Cells["UnitPrice"].Value.ToString(), out decimal unitPrice) ||
                    unitPrice <= 0)
                {
                    MessageBox.Show("Each item must have a valid unit price greater than 0.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private bool IsPONumberUnique(string poNumber)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM PurchaseOrders WHERE po_number = @poNumber";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@poNumber", poNumber);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    int count = (int)cmd.ExecuteScalar();
                    return count == 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void SavePurchaseOrder()
        {

            string notes = kryptonRichTextBox2.Text == _notesPlaceholder
                ? string.Empty
                : kryptonRichTextBox2.Text;
            ApplyStatusBusinessRulesForAdd();
            if (!ValidateForm()) return;

            if (!IsPONumberUnique(CompanyNameTextBoxSupplier.Text))
            {
                MessageBox.Show("This PO number already exists. Please regenerate.",
                    "Duplicate PO Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GeneratePONumber();
                return;
            }

            decimal totalAmount;
            if (!TryParseCurrency(label11.Text, out totalAmount) || totalAmount <= 0)
            {
                MessageBox.Show("Unable to compute total amount. Please review your items.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string poQuery = @"INSERT INTO PurchaseOrders
                            (supplier_id, po_number, po_date, expected_date, status, created_by, total_amount)
                            OUTPUT INSERTED.po_id
                            VALUES (@supplierId, @poNumber, @poDate, @expectedDate, @status, @createdBy, @totalAmount)";

                            int poId;
                            using (SqlCommand cmd = new SqlCommand(poQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@supplierId", guna2ComboBox3.SelectedValue);
                                cmd.Parameters.AddWithValue("@poNumber", CompanyNameTextBoxSupplier.Text.Trim());
                                cmd.Parameters.AddWithValue("@poDate", guna2DateTimePicker1.Value);
                                cmd.Parameters.AddWithValue("@expectedDate", ExpirationDataComboBox.Value);
                                cmd.Parameters.AddWithValue("@status", guna2ComboBox2.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@createdBy", UserSession.UserId);
                                cmd.Parameters.AddWithValue("@totalAmount", totalAmount);

                                poId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            string itemQuery = @"INSERT INTO PurchaseOrderItems
                            (po_id, product_id, quantity_ordered, unit_price, total_amount)
                            VALUES (@poId, @productId, @quantity, @unitPrice, @totalAmount)";

                            foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
                            {
                                if (row.Tag == null)
                                {
                                    throw new InvalidOperationException("One or more items are missing product information.");
                                }

                                if (!int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int quantity) || quantity <= 0)
                                {
                                    throw new InvalidOperationException("Invalid quantity detected for one of the items.");
                                }

                                if (!TryParseCurrency(row.Cells["UnitPrice"].Value?.ToString(), out decimal unitPrice) || unitPrice <= 0)
                                {
                                    throw new InvalidOperationException("Invalid unit price detected for one of the items.");
                                }

                                if (!TryParseCurrency(row.Cells["Total"].Value?.ToString(), out decimal itemTotal) || itemTotal <= 0)
                                {
                                    throw new InvalidOperationException("Invalid item total detected for one of the items.");
                                }

                                using (SqlCommand cmd = new SqlCommand(itemQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@poId", poId);
                                    cmd.Parameters.AddWithValue("@productId", row.Tag);
                                    cmd.Parameters.AddWithValue("@quantity", quantity);
                                    cmd.Parameters.AddWithValue("@unitPrice", unitPrice);
                                    cmd.Parameters.AddWithValue("@totalAmount", itemTotal);

                                    cmd.ExecuteNonQuery();
                                }
                            }

                            AuditHelper.LogWithTransaction(
                                connection,
                                transaction,
                                AuditModule.SUPPLIERS,
                                $"Created purchase order {CompanyNameTextBoxSupplier.Text}",
                                AuditActivityType.CREATE,
                                "PurchaseOrders",
                                poId.ToString(),
                                null,
                                BuildAuditNewValues());

                            transaction.Commit();
                            MessageBox.Show($"Purchase Order {CompanyNameTextBoxSupplier.Text} created successfully!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Form parentForm = this.FindForm();
                            if (parentForm != null)
                            {
                                parentForm.Close();
                            }
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving purchase order: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string BuildAuditNewValues()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"PO Number: {CompanyNameTextBoxSupplier.Text}");
            builder.AppendLine($"Supplier ID: {guna2ComboBox3.SelectedValue}");
            builder.AppendLine($"Status: {guna2ComboBox2.SelectedItem}");
            builder.AppendLine($"PO Date: {guna2DateTimePicker1.Value:yyyy-MM-dd}");
            builder.AppendLine($"Expected Date: {ExpirationDataComboBox.Value:yyyy-MM-dd}");

            foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
            {
                string productName = row.Cells["Product"]?.Value?.ToString();
                string quantity = row.Cells["Quantity"]?.Value?.ToString();
                string unitPrice = row.Cells["UnitPrice"]?.Value?.ToString();
                string total = row.Cells["Total"]?.Value?.ToString();

                builder.AppendLine($"Item: {productName}, Qty: {quantity}, Unit Price: {unitPrice}, Total: {total}");
            }

            builder.AppendLine($"Grand Total: {label11.Text}");
            return builder.ToString();
        }

        private bool TryParseCurrency(string input, out decimal value)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                value = 0;
                return false;
            }

            string sanitized = input.Replace("₱", string.Empty).Replace(",", string.Empty).Trim();
            return decimal.TryParse(sanitized, NumberStyles.Number | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value);
        }

        // EVENT HANDLERS - KEEP WHATEVER METHOD NAMES YOUR DESIGNER USES
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbxCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a product.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (guna2NumericUpDown1.Value <= 0)
            {
                MessageBox.Show("Quantity must be greater than 0.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (guna2NumericUpDown2.Value <= 0)
            {
                MessageBox.Show("Unit price must be greater than 0.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productName = cbxCategory.Text;
            int productId = Convert.ToInt32(cbxCategory.SelectedValue);
            int quantity = (int)guna2NumericUpDown1.Value;
            decimal unitPrice = guna2NumericUpDown2.Value;
            decimal total = quantity * unitPrice;

            foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
            {
                if (row.Cells["Product"].Value?.ToString() == productName)
                {
                    MessageBox.Show("This product is already added. Please modify the existing entry.",
                        "Duplicate Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            dgvPurchaseItems.Rows.Add(productName, quantity, unitPrice.ToString("N2"), total.ToString("N2"), "🗑");
            dgvPurchaseItems.Rows[dgvPurchaseItems.Rows.Count - 1].Tag = productId;

            UpdateCalculations();
            AdjustLayout();

            cbxCategory.SelectedIndex = -1;
            guna2NumericUpDown1.Value = 1;
            guna2NumericUpDown2.Value = 0;
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            SavePurchaseOrder();
        }

        private void btnWhite_Click_1(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.Close();
            }
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.Close();
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label14_Click(object sender, EventArgs e)
        {
        }

        private void label13_Click(object sender, EventArgs e)
        {
        }

        private void label12_Click(object sender, EventArgs e)
        {
        }

        private void label11_Click(object sender, EventArgs e)
        {
        }
    }
}