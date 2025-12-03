using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
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

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Pop_Up_Forms.Edit_Form
{
    public partial class EditPurchaseOrder : UserControl
    {

        private SqlConnection con;
        private string connectionString = ConnectionString.DataSource;
        private int currentPoId = -1;
        private DateTime? currentCreatedAt = null;
        private bool editingLocked = false;

        #region UI Rules

        public EditPurchaseOrder()
        {
            InitializeComponent();
            con = new SqlConnection(connectionString);
            LoadSuppliers();
            LoadProducts();

            // Hook events
            cbxStatus.SelectedIndexChanged += (s, e) => ApplyPOStatusRules(cbxStatus.Text);
            cbxTax.SelectedIndexChanged += (s, e) => UpdateTotals();
            nudShippingFee.ValueChanged += (s, e) => UpdateTotals();
            dgvPurchaseItems.CellContentClick += dgvPurchaseItems_CellContentClick;
        }

        public void LoadPurchaseOrder(string poNumber)
        {
            if (string.IsNullOrWhiteSpace(poNumber))
            {
                MessageBox.Show("Invalid purchase order number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(@"SELECT po_id, po_number, po_date, expected_date, supplier_id, status, total_amount
                                                               FROM PurchaseOrders
                                                               WHERE po_number = @poNumber", connection))
                    {
                        cmd.Parameters.AddWithValue("@poNumber", poNumber);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Purchase order not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            currentPoId = reader.GetInt32(reader.GetOrdinal("po_id"));
                            tbxOrderNumber.Text = reader["po_number"].ToString();
                            dtpOrderDate.Value = Convert.ToDateTime(reader["po_date"]);

                            if (reader["expected_date"] != DBNull.Value)
                            {
                                dtpExpectedDelivery.Value = Convert.ToDateTime(reader["expected_date"]);
                            }

                            cbxSupplier.SelectedValue = Convert.ToInt32(reader["supplier_id"]);

                            string status = reader["status"].ToString();
                            if (!cbxStatus.Items.Contains(status))
                            {
                                cbxStatus.Items.Add(status);
                            }

                            cbxStatus.SelectedItem = status;
                        }
                    }

                    LoadPurchaseOrderItems(connection);
                    UpdateTotals();
                    ApplyPOStatusRules(cbxStatus.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPurchaseOrderItems(SqlConnection connection)
        {
            dgvPurchaseItems.Rows.Clear();

            if (currentPoId <= 0)
            {
                return;
            }

            using (SqlCommand cmd = new SqlCommand(@"SELECT poi.product_id, p.product_name, poi.quantity_ordered, poi.unit_price, poi.total_amount
                                                      FROM PurchaseOrderItems poi
                                                      INNER JOIN Products p ON poi.product_id = p.ProductInternalID
                                                      WHERE poi.po_id = @poId", connection))
            {
                cmd.Parameters.AddWithValue("@poId", currentPoId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string productName = reader["product_name"].ToString();
                        int quantity = Convert.ToInt32(reader["quantity_ordered"]);
                        decimal unitPrice = Convert.ToDecimal(reader["unit_price"]);
                        decimal total = Convert.ToDecimal(reader["total_amount"]);

                        int rowIndex = dgvPurchaseItems.Rows.Add(productName, quantity, unitPrice.ToString("N2"), total.ToString("N2"), "Delete");
                        dgvPurchaseItems.Rows[rowIndex].Tag = Convert.ToInt32(reader["product_id"]);
                    }
                }
            }
        }

        private void LoadSuppliers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT supplier_id, supplier_name FROM Suppliers ORDER BY supplier_name", connection))
            {
                DataTable suppliers = new DataTable();
                connection.Open();
                suppliers.Load(cmd.ExecuteReader());

                cbxSupplier.DisplayMember = "supplier_name";
                cbxSupplier.ValueMember = "supplier_id";
                cbxSupplier.DataSource = suppliers;
            }
        }

        private void LoadProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT ProductInternalID, product_name FROM Products ORDER BY product_name", connection))
            {
                DataTable products = new DataTable();
                connection.Open();
                products.Load(cmd.ExecuteReader());

                cbxProduct.DisplayMember = "product_name";
                cbxProduct.ValueMember = "ProductInternalID";
                cbxProduct.DataSource = products;
            }
        }

        private void ApplyPOStatusRules(string status)
        {
            // Always read-only
            tbxOrderNumber.Enabled = false;
            dtpOrderDate.Enabled = false;

            switch (status)
            {
                case "Draft":
                    cbxSupplier.Enabled = true;
                    dgvPurchaseItems.Enabled = true;
                    nudUnitPrice.Enabled = true;
                    nudQuantity.Enabled = true;
                    cbxTax.Enabled = true;
                    nudShippingFee.Enabled = true;
                    dtpExpectedDelivery.Enabled = true;
                    rtxNotes.Enabled = true;
                    cbxPaymentStatus.Enabled = true;
                    break;

                case "Approved":
                    cbxSupplier.Enabled = false;
                    dgvPurchaseItems.Enabled = false;  // no adding/deleting products
                    nudUnitPrice.Enabled = false;
                    nudQuantity.Enabled = false;
                    cbxTax.Enabled = false;
                    nudShippingFee.Enabled = false;

                    dtpExpectedDelivery.Enabled = true;  // still editable
                    rtxNotes.Enabled = true;
                    cbxPaymentStatus.Enabled = true;
                    break;

                case "Received":
                    // Everything locked EXCEPT notes & payment
                    cbxSupplier.Enabled = false;
                    dgvPurchaseItems.Enabled = false;
                    nudUnitPrice.Enabled = false;
                    nudQuantity.Enabled = false;
                    cbxTax.Enabled = false;
                    nudShippingFee.Enabled = false;
                    dtpExpectedDelivery.Enabled = false;

                    rtxNotes.Enabled = true;
                    cbxPaymentStatus.Enabled = true;
                    break;

                case "Completed":
                    // 100% Locked
                    foreach (Control c in this.Controls)
                        c.Enabled = false;
                    break;
            }
        }

        #endregion

/*
        private void LoadPurchaseOrder(int poId)
        {
            var po = database.GetPO(poId);

            // Load UI with values...
            tbxOrderNumber.Text = po.OrderNumber;
            dtpOrderDate.Value = po.OrderDate;
            cbxSupplier.SelectedValue = po.SupplierId;
            cbxStatus.Text = po.Status;
            cbxPaymentStatus.Text = po.PaymentStatus;

            // Apply logic
            ApplyPOStatusRules(po.Status);
        }
*/
 
        private void cbxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyPOStatusRules(cbxStatus.Text);
        }

        private decimal ComputeSubtotal()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
            {
                if (row.Cells["Total"].Value != null &&
                    decimal.TryParse(row.Cells["Total"].Value.ToString(), out decimal value))
                {
                    subtotal += value;
                }
            }

            return subtotal;
        }

        private void UpdateTotals()
        {
            decimal subtotal = 0;

            // SUM TOTAL OF ALL ITEMS
            foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
            {
                if (row.Cells["Total"].Value != null &&
                    decimal.TryParse(row.Cells["Total"].Value.ToString(), out decimal value))
                {
                    subtotal += value;
                }
            }

            // TAX LOGIC
            decimal taxRate = 0;

            string taxOption = cbxTax.SelectedItem?.ToString() ?? "0%";

            if (taxOption == "12%")
                taxRate = 0.12m;
            else if (taxOption == "0%")
                taxRate = 0.00m;
            else if (taxOption == "8%")
                taxRate = 0.08m;
            else if (taxOption == "3%")
                taxRate = 0.03m;

            decimal tax = subtotal * taxRate;

            decimal shippingFee = nudShippingFee.Value;

            decimal grandTotal = subtotal + tax + shippingFee;

            lblSubtotal.Text = subtotal.ToString("N2");
            lblTax.Text = tax.ToString("N2");
            lblShipping.Text = shippingFee.ToString("N2");
            lblGrandTotal.Text = grandTotal.ToString("N2");
        }

        
        private void AddItem()
        {
            if (cbxProduct.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a product.");
                return;
            }

            string productName = cbxProduct.Text;
            int productId = (int)cbxProduct.SelectedValue;
            int qty = (int)nudQuantity.Value;
            decimal unitPrice = nudUnitPrice.Value;
            decimal total = qty * unitPrice;

            if (qty <= 0 || unitPrice <= 0)
            {
                MessageBox.Show("Quantity and unit price must be greater than zero.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // PREVENT DUPLICATES
            foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
            {
                if (row.Cells["Product"].Value.ToString() == productName)
                {
                    MessageBox.Show("This product is already added.");
                    return;
                }
            }

            dgvPurchaseItems.Rows.Add(productName, qty, unitPrice.ToString("N2"), total.ToString("N2"), "Delete");
            dgvPurchaseItems.Rows[dgvPurchaseItems.Rows.Count - 1].Tag = productId;

            UpdateTotals();
        }

        private void dgvPurchaseItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvPurchaseItems.Columns["Action"].Index)
            {
                dgvPurchaseItems.Rows.RemoveAt(e.RowIndex);
                UpdateTotals();
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(tbxOrderNumber.Text))
            {
                MessageBox.Show("Order number is required.");
                return false;
            }

            if (cbxSupplier.SelectedIndex == -1)
            {
                MessageBox.Show("Select a supplier.");
                return false;
            }

            if (cbxStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Select a status.");
                return false;
            }

            if (dgvPurchaseItems.Rows.Count == 0)
            {
                MessageBox.Show("Add at least one purchase item.");
                return false;
            }

            return true;
        }

        private void SavePurchaseOrder()
        {
            if (currentPoId <= 0)
            {
                MessageBox.Show("Load a purchase order before saving changes.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidateForm()) return;

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                using (SqlTransaction tr = con.BeginTransaction())
                {
                    decimal total = 0;
                    foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
                    {
                        if (row.Cells["Total"].Value != null && decimal.TryParse(row.Cells["Total"].Value.ToString(), out decimal rowTotal))
                        {
                            total += rowTotal;
                        }
                    }

                    string updateSql = @"UPDATE PurchaseOrders
                                            SET supplier_id = @sup,
                                                po_date = @date,
                                                expected_date = @exp,
                                                status = @status,
                                                total_amount = @total,
                                                updated_at = GETDATE()
                                            WHERE po_id = @poId";

                    SqlCommand cmdHeader = new SqlCommand(updateSql, con, tr);
                    cmdHeader.Parameters.AddWithValue("@sup", cbxSupplier.SelectedValue);
                    cmdHeader.Parameters.AddWithValue("@date", dtpOrderDate.Value);
                    cmdHeader.Parameters.AddWithValue("@exp", dtpExpectedDelivery.Value);
                    cmdHeader.Parameters.AddWithValue("@status", cbxStatus.Text);
                    cmdHeader.Parameters.AddWithValue("@total", total);
                    cmdHeader.Parameters.AddWithValue("@poId", currentPoId);
                    cmdHeader.ExecuteNonQuery();

                    using (SqlCommand deleteItems = new SqlCommand("DELETE FROM PurchaseOrderItems WHERE po_id = @poId", con, tr))
                    {
                        deleteItems.Parameters.AddWithValue("@poId", currentPoId);
                        deleteItems.ExecuteNonQuery();
                    }

                    string itemSql = @"INSERT INTO PurchaseOrderItems
                                        (po_id, product_id, quantity_ordered, unit_price, total_amount)
                                        VALUES (@po, @prod, @qty, @price, @total);";

                    foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
                    {
                        if (row.Tag == null) continue;

                        SqlCommand cmdItem = new SqlCommand(itemSql, con, tr);
                        cmdItem.Parameters.AddWithValue("@po", currentPoId);
                        cmdItem.Parameters.AddWithValue("@prod", row.Tag);
                        cmdItem.Parameters.AddWithValue("@qty", row.Cells["Quantity"].Value);
                        cmdItem.Parameters.AddWithValue("@price", decimal.Parse(row.Cells["UnitPrice"].Value.ToString()));
                        cmdItem.Parameters.AddWithValue("@total", decimal.Parse(row.Cells["Total"].Value.ToString()));
                        cmdItem.ExecuteNonQuery();
                    }

                    AuditHelper.LogWithTransaction(
                        con,
                        tr,
                        AuditModule.SUPPLIERS,
                        $"Updated purchase order {tbxOrderNumber.Text.Trim()}",
                        AuditActivityType.UPDATE,
                        "PurchaseOrders",
                        currentPoId.ToString());

                    tr.Commit();
                    MessageBox.Show("Purchase Order saved successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving PO: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void ParentContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
