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
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public partial class AddPurchaseOrderForm : UserControl
    {
        private int maxHeight = 225;
        private SqlConnection con;
        private string connectionString = ConnectionString.DataSource;

        public AddPurchaseOrderForm()
        {
            InitializeComponent();
            guna2Panel1.HorizontalScroll.Enabled = false;
            con = new SqlConnection(connectionString);

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

            // Initialize DataGridView
            InitializeDataGridView();

            // Hook up events for dynamic calculation
            guna2ComboBox1.SelectedIndexChanged += (s, e) => UpdateCalculations();
            guna2NumericUpDown3.ValueChanged += (s, e) => UpdateCalculations();

            // Initialize calculation display
            UpdateCalculations();
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
            guna2ComboBox2.Items.AddRange(new string[] { "Pending", "Approved", "Ordered", "Received", "Cancelled" });
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

            return true;
        }

        private bool IsPONumberUnique(string poNumber)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM PurchaseOrders WHERE po_number = @poNumber";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@poNumber", poNumber);

                    if (con.State == ConnectionState.Closed)
                        con.Open();

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
            if (!ValidateForm()) return;

            if (!IsPONumberUnique(CompanyNameTextBoxSupplier.Text))
            {
                MessageBox.Show("This PO number already exists. Please regenerate.",
                    "Duplicate PO Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GeneratePONumber();
                return;
            }

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    string poQuery = @"INSERT INTO PurchaseOrders 
                        (supplier_id, po_number, po_date, expected_date, status, created_by, total_amount) 
                        VALUES (@supplierId, @poNumber, @poDate, @expectedDate, @status, @createdBy, @totalAmount);
                        SELECT SCOPE_IDENTITY();";

                    int poId;
                    using (SqlCommand cmd = new SqlCommand(poQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@supplierId", guna2ComboBox3.SelectedValue);
                        cmd.Parameters.AddWithValue("@poNumber", CompanyNameTextBoxSupplier.Text.Trim());
                        cmd.Parameters.AddWithValue("@poDate", guna2DateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@expectedDate", ExpirationDataComboBox.Value);
                        cmd.Parameters.AddWithValue("@status", guna2ComboBox2.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@createdBy", 1);

                        decimal total = decimal.Parse(label11.Text.Replace("₱", "").Replace(",", "").Trim());
                        cmd.Parameters.AddWithValue("@totalAmount", total);

                        poId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    string itemQuery = @"INSERT INTO PurchaseOrderItems 
                        (po_id, product_id, quantity_ordered, unit_price, total_amount) 
                        VALUES (@poId, @productId, @quantity, @unitPrice, @totalAmount)";

                    foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
                    {
                        using (SqlCommand cmd = new SqlCommand(itemQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@poId", poId);
                            cmd.Parameters.AddWithValue("@productId", row.Tag);
                            cmd.Parameters.AddWithValue("@quantity", row.Cells["Quantity"].Value);

                            decimal unitPrice = decimal.Parse(row.Cells["UnitPrice"].Value.ToString().Replace(",", ""));
                            cmd.Parameters.AddWithValue("@unitPrice", unitPrice);

                            decimal itemTotal = decimal.Parse(row.Cells["Total"].Value.ToString().Replace(",", ""));
                            cmd.Parameters.AddWithValue("@totalAmount", itemTotal);

                            cmd.ExecuteNonQuery();
                        }
                    }

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
            catch (Exception ex)
            {
                MessageBox.Show("Error saving purchase order: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
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

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            btnAdd_Click(sender, e);
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            SavePurchaseOrder();
        }

        private void btnBlue_Click_1(object sender, EventArgs e)
        {
            SavePurchaseOrder();
        }

        private void btnWhite_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.Close();
            }
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