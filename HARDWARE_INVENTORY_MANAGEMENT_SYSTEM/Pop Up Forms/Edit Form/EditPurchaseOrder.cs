using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using Guna.UI2.WinForms;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Pop_Up_Forms.Edit_Form
{
    public partial class EditPurchaseOrder : UserControl
    {
        // -----------------------------
        //  FIELDS / STATE
        // -----------------------------
        private readonly string connectionString = ConnectionString.DataSource;
        private SqlConnection con;

        private int currentPoId = -1;
        private DateTime? currentCreatedAt = null;
        private bool editingLocked = false;
        private bool lockFromList = false;

        private string originalStatus = string.Empty;
        private DateTime? originalExpectedDate = null;
        private string originalAuditSnapshot = string.Empty;

        // Notes placeholder
        private readonly string notesPlaceholder = "Additional Notes.....";
        private readonly Color placeholderColor = Color.Silver;
        private readonly Color normalColor = Color.Black;

        private readonly List<string> allowedStatuses = new List<string>
        {
            "Pending",
            "Approved",
            "Ordered",
            "Received"
        };

        // -----------------------------
        //  CTOR
        // -----------------------------
        public EditPurchaseOrder()
        {
            InitializeComponent();

            con = new SqlConnection(connectionString);

            LoadSuppliers();
            LoadProducts();
            LoadStatusOptions();

            // wire events
            cbxStatus.SelectedIndexChanged += cbxStatus_SelectedIndexChanged;

            cbxTax.SelectedIndexChanged += cbxTax_SelectedIndexChanged;
            nudShippingFee.ValueChanged += nudShippingFee_ValueChanged;

            dgvPurchaseItems.CellContentClick += dgvPurchaseItems_CellContentClick;

            btnAdd.Click += BtnAdd_Click;
            btnBlue.Click += BtnBlue_Click;
            btnWhite.Click += BtnCancel_Click;
            guna2Button3.Click += BtnCancel_Click; // X button on UI

            rtxNotes.Enter += RtxNotes_Enter;
            rtxNotes.Leave += RtxNotes_Leave;

            this.Paint += ParentContainer_Paint;

            EnsureNotesPlaceholder();
        }

        // -----------------------------
        //  PUBLIC API
        // -----------------------------
        /// <summary>
        /// Load an existing PO for editing.
        /// enforceLock = true if opened from history/list where edits should be blocked (view-only).
        /// </summary>
        public void LoadPurchaseOrder(string poNumber, bool enforceLock)
        {
            if (string.IsNullOrWhiteSpace(poNumber))
            {
                MessageBox.Show("Invalid purchase order number.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // remember how this form was opened
            lockFromList = enforceLock;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT po_id, po_number, po_date, expected_date, supplier_id, status, total_amount
                        FROM PurchaseOrders
                        WHERE po_number = @poNumber", connection))
                    {
                        cmd.Parameters.AddWithValue("@poNumber", poNumber);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Purchase order not found.", "Not Found",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            currentPoId = reader.GetInt32(reader.GetOrdinal("po_id"));
                            tbxOrderNumber.Text = reader["po_number"].ToString();

                            currentCreatedAt = Convert.ToDateTime(reader["po_date"]);
                            dtpOrderDate.Value = currentCreatedAt.Value;

                            if (reader["expected_date"] != DBNull.Value)
                            {
                                dtpExpectedDelivery.Value = Convert.ToDateTime(reader["expected_date"]);
                                originalExpectedDate = dtpExpectedDelivery.Value;
                            }

                            cbxSupplier.SelectedValue = Convert.ToInt32(reader["supplier_id"]);

                            string status = reader["status"].ToString();
                            originalStatus = status;
                            ConfigureStatusSelection(status);
                        }
                    }

                    // load items
                    LoadPurchaseOrderItems(connection);
                    UpdateTotals();

                    // apply status rules
                    ApplyPOStatusRules(cbxStatus.Text);
                    ApplyStatusBusinessRules(cbxStatus.Text);

                    // snapshot audit
                    originalAuditSnapshot = BuildAuditSnapshot();

                    // apply locking (12h rule + view-only mode)
                    ApplyEditLockIfNeeded();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading purchase order: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -----------------------------
        //  LOADING HELPERS
        // -----------------------------
        private void LoadPurchaseOrderItems(SqlConnection connection)
        {
            dgvPurchaseItems.Rows.Clear();

            if (currentPoId <= 0)
                return;

            using (SqlCommand cmd = new SqlCommand(@"
                SELECT poi.product_id,
                       p.product_name,
                       poi.quantity_ordered,
                       poi.unit_price,
                       poi.total_amount
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

                        int rowIndex = dgvPurchaseItems.Rows.Add(
                            productName,
                            quantity,
                            unitPrice.ToString("N2"),
                            total.ToString("N2"),
                            "Delete");

                        dgvPurchaseItems.Rows[rowIndex].Tag = Convert.ToInt32(reader["product_id"]);
                    }
                }
            }
        }

        private void LoadSuppliers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT supplier_id, supplier_name FROM Suppliers ORDER BY supplier_name", connection))
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
            using (SqlCommand cmd = new SqlCommand(
                "SELECT ProductInternalID, product_name FROM Products ORDER BY product_name", connection))
            {
                DataTable products = new DataTable();
                connection.Open();
                products.Load(cmd.ExecuteReader());

                cbxProduct.DisplayMember = "product_name";
                cbxProduct.ValueMember = "ProductInternalID";
                cbxProduct.DataSource = products;
            }
        }

        private void LoadStatusOptions()
        {
            cbxStatus.Items.Clear();
            cbxStatus.Items.AddRange(allowedStatuses.ToArray());
        }

        private void ConfigureStatusSelection(string status)
        {
            LoadStatusOptions();

            if (allowedStatuses.Contains(status))
            {
                cbxStatus.SelectedItem = status;
                cbxStatus.Enabled = true;
            }
            else
            {
                cbxStatus.Items.Add(status);
                cbxStatus.SelectedItem = status;
                cbxStatus.Enabled = false;
            }
        }

        // -----------------------------
        //  STATUS / LOCK RULES
        // -----------------------------
        private void ApplyPOStatusRules(string status)
        {
            // Order number & date are always read-only in edit
            tbxOrderNumber.Enabled = false;
            dtpOrderDate.Enabled = false;

            switch (status)
            {
                case "Pending":
                    cbxSupplier.Enabled = true;
                    dgvPurchaseItems.Enabled = true;
                    nudUnitPrice.Enabled = true;
                    nudQuantity.Enabled = true;
                    cbxTax.Enabled = true;
                    nudShippingFee.Enabled = true;
                    dtpExpectedDelivery.Enabled = true;
                    rtxNotes.Enabled = true;
                    cbxPaymentStatus.Enabled = true;
                    btnAdd.Enabled = true;
                    btnBlue.Enabled = true;
                    break;

                case "Approved":
                    cbxSupplier.Enabled = false;
                    dgvPurchaseItems.Enabled = false;
                    nudUnitPrice.Enabled = false;
                    nudQuantity.Enabled = false;
                    cbxTax.Enabled = false;
                    nudShippingFee.Enabled = false;

                    dtpExpectedDelivery.Enabled = true;
                    rtxNotes.Enabled = true;
                    cbxPaymentStatus.Enabled = true;
                    btnAdd.Enabled = false;
                    btnBlue.Enabled = true;
                    break;

                case "Ordered":
                    cbxSupplier.Enabled = false;
                    dgvPurchaseItems.Enabled = false;
                    nudUnitPrice.Enabled = false;
                    nudQuantity.Enabled = false;
                    cbxTax.Enabled = false;
                    nudShippingFee.Enabled = false;
                    dtpExpectedDelivery.Enabled = true;
                    rtxNotes.Enabled = true;
                    cbxPaymentStatus.Enabled = true;
                    btnAdd.Enabled = false;
                    btnBlue.Enabled = true;
                    break;

                case "Received":
                    cbxSupplier.Enabled = false;
                    dgvPurchaseItems.Enabled = false;
                    nudUnitPrice.Enabled = false;
                    nudQuantity.Enabled = false;
                    cbxTax.Enabled = false;
                    nudShippingFee.Enabled = false;
                    dtpExpectedDelivery.Enabled = false;

                    rtxNotes.Enabled = true;
                    cbxPaymentStatus.Enabled = true;
                    btnAdd.Enabled = false;
                    btnBlue.Enabled = true;
                    break;

                default:
                    DisableEditingControls();
                    break;
            }
        }

        private void ApplyStatusBusinessRules(string status)
        {
            switch (status)
            {
                case "Approved":
                    if (dtpExpectedDelivery.Value < dtpOrderDate.Value.AddDays(1))
                        dtpExpectedDelivery.Value = dtpOrderDate.Value.AddDays(7);
                    break;

                case "Ordered":
                    if (dtpExpectedDelivery.Value < DateTime.Now)
                        dtpExpectedDelivery.Value = DateTime.Now.AddDays(7);
                    break;

                case "Received":
                    dtpExpectedDelivery.Value = DateTime.Now;
                    break;
            }
        }

        private void ApplyEditLockIfNeeded()
        {
            // Case 1: Opened from list/history with enforceLock = true
            // -> View-only, NO popup.
            if (lockFromList)
            {
                editingLocked = true;
                DisableEditingControls();
                return;
            }

            // Case 2: Opened normally, apply 12-hour rule and show popup if expired
            editingLocked = !IsEditWindowOpen();

            if (editingLocked)
            {
                DisableEditingControls();
                MessageBox.Show(
                    "This purchase order is more than 12 hours old and is view-only.",
                    "Update Locked",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void DisableEditingControls()
        {
            cbxSupplier.Enabled = false;
            cbxProduct.Enabled = false;
            dgvPurchaseItems.Enabled = false;
            nudUnitPrice.Enabled = false;
            nudQuantity.Enabled = false;
            cbxTax.Enabled = false;
            nudShippingFee.Enabled = false;
            dtpExpectedDelivery.Enabled = false;
            rtxNotes.Enabled = false;
            cbxPaymentStatus.Enabled = false;
            cbxStatus.Enabled = false;
            btnAdd.Enabled = false;
            btnBlue.Enabled = false;
        }

        /// <summary>
        /// 12-hour edit window check.
        /// </summary>
        private bool IsEditWindowOpen()
        {
            if (!currentCreatedAt.HasValue)
                return false;

            TimeSpan diff = DateTime.Now - currentCreatedAt.Value;
            return diff.TotalHours < 12;
        }

        // -----------------------------
        //  NOTES PLACEHOLDER
        // -----------------------------
        private void EnsureNotesPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(rtxNotes.Text) || rtxNotes.Text == notesPlaceholder)
            {
                rtxNotes.Text = notesPlaceholder;
                rtxNotes.ForeColor = placeholderColor;
            }
            else
            {
                rtxNotes.ForeColor = normalColor;
            }
        }

        private void RtxNotes_Enter(object sender, EventArgs e)
        {
            if (rtxNotes.Text == notesPlaceholder)
            {
                rtxNotes.Text = string.Empty;
                rtxNotes.ForeColor = normalColor;
            }
        }

        private void RtxNotes_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtxNotes.Text))
            {
                rtxNotes.Text = notesPlaceholder;
                rtxNotes.ForeColor = placeholderColor;
            }
        }

        // -----------------------------
        //  AUDIT SNAPSHOT
        // -----------------------------
        private string BuildAuditSnapshot()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("PO Number: " + tbxOrderNumber.Text);
            builder.AppendLine("Supplier: " + cbxSupplier.Text);
            builder.AppendLine("PO Date: " + dtpOrderDate.Value.ToString("yyyy-MM-dd HH:mm"));
            builder.AppendLine("Expected Date: " + dtpExpectedDelivery.Value.ToString("yyyy-MM-dd HH:mm"));
            builder.AppendLine("Status: " + cbxStatus.Text);
            builder.AppendLine("Payment Status: " + cbxPaymentStatus.Text);

            foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
            {
                string productName = row.Cells["Product"] != null ? Convert.ToString(row.Cells["Product"].Value) : string.Empty;
                string quantity = row.Cells["Quantity"] != null ? Convert.ToString(row.Cells["Quantity"].Value) : string.Empty;
                string unitPrice = row.Cells["UnitPrice"] != null ? Convert.ToString(row.Cells["UnitPrice"].Value) : string.Empty;
                string total = row.Cells["Total"] != null ? Convert.ToString(row.Cells["Total"].Value) : string.Empty;

                builder.AppendLine("Item: " + productName + ", Qty: " + quantity + ", Unit Price: " + unitPrice + ", Total: " + total);
            }

            builder.AppendLine("Grand Total: " + lblGrandTotal.Text);
            return builder.ToString();
        }

        // -----------------------------
        //  TOTALS
        // -----------------------------
        private decimal ComputeSubtotal()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    decimal value;
                    if (decimal.TryParse(row.Cells["Total"].Value.ToString(), out value))
                    {
                        subtotal += value;
                    }
                }
            }

            return subtotal;
        }

        private void UpdateTotals()
        {
            decimal subtotal = ComputeSubtotal();

            decimal taxRate = 0;
            string taxOption = cbxTax.SelectedItem != null ? cbxTax.SelectedItem.ToString() : "0%";

            switch (taxOption)
            {
                case "12%":
                    taxRate = 0.12m; break;
                case "8%":
                    taxRate = 0.08m; break;
                case "3%":
                    taxRate = 0.03m; break;
                case "0%":
                default:
                    taxRate = 0m; break;
            }

            decimal tax = subtotal * taxRate;
            decimal shippingFee = nudShippingFee.Value;
            decimal grandTotal = subtotal + tax + shippingFee;

            lblSubtotal.Text = subtotal.ToString("N2");
            lblTax.Text = tax.ToString("N2");
            lblShipping.Text = shippingFee.ToString("N2");
            lblGrandTotal.Text = grandTotal.ToString("N2");
        }

        // event wrappers for UpdateTotals
        private void cbxTax_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void nudShippingFee_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        // -----------------------------
        //  GRID / ADD ITEM
        // -----------------------------
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void AddItem()
        {
            if (cbxProduct.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a product.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productName = cbxProduct.Text;
            int productId = (int)cbxProduct.SelectedValue;
            int qty = (int)nudQuantity.Value;
            decimal unitPrice = nudUnitPrice.Value;
            decimal total = qty * unitPrice;

            if (qty <= 0 || unitPrice <= 0)
            {
                MessageBox.Show("Quantity and unit price must be greater than zero.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // prevent duplicates
            foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
            {
                if (row.Cells["Product"].Value != null &&
                    row.Cells["Product"].Value.ToString() == productName)
                {
                    MessageBox.Show("This product is already added.", "Duplicate",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            int idx = dgvPurchaseItems.Rows.Add(
                productName,
                qty,
                unitPrice.ToString("N2"),
                total.ToString("N2"),
                "Delete");

            dgvPurchaseItems.Rows[idx].Tag = productId;

            UpdateTotals();
        }

        private void dgvPurchaseItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvPurchaseItems.Columns["Action"] != null &&
                e.ColumnIndex == dgvPurchaseItems.Columns["Action"].Index)
            {
                dgvPurchaseItems.Rows.RemoveAt(e.RowIndex);
                UpdateTotals();
            }
        }

        // -----------------------------
        //  VALIDATION & SAVE
        // -----------------------------
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

        private void BtnBlue_Click(object sender, EventArgs e)
        {
            SavePurchaseOrder();
        }

        private void SavePurchaseOrder()
        {
            // hard block: if locked, do not allow saving at all
            if (editingLocked || !IsEditWindowOpen())
            {
                MessageBox.Show(
                    "This purchase order is more than 12 hours old and cannot be updated.",
                    "Update Locked",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (currentPoId <= 0)
            {
                MessageBox.Show("Load a purchase order before saving changes.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (!ValidateForm())
                return;

            ApplyStatusBusinessRules(cbxStatus.Text);

            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                using (SqlTransaction tr = con.BeginTransaction())
                {
                    // header total
                    decimal total = ComputeSubtotal();

                    const string updateSql = @"
                        UPDATE PurchaseOrders
                        SET supplier_id = @sup,
                            po_date = @date,
                            expected_date = @exp,
                            status = @status,
                            total_amount = @total,
                            updated_at = GETDATE()
                        WHERE po_id = @poId";

                    using (SqlCommand cmdHeader = new SqlCommand(updateSql, con, tr))
                    {
                        cmdHeader.Parameters.AddWithValue("@sup", cbxSupplier.SelectedValue);
                        cmdHeader.Parameters.AddWithValue("@date", dtpOrderDate.Value);
                        cmdHeader.Parameters.AddWithValue("@exp", dtpExpectedDelivery.Value);
                        cmdHeader.Parameters.AddWithValue("@status", cbxStatus.Text);
                        cmdHeader.Parameters.AddWithValue("@total", total);
                        cmdHeader.Parameters.AddWithValue("@poId", currentPoId);
                        cmdHeader.ExecuteNonQuery();
                    }

                    // delete old items
                    using (SqlCommand deleteItems =
                        new SqlCommand("DELETE FROM PurchaseOrderItems WHERE po_id = @poId", con, tr))
                    {
                        deleteItems.Parameters.AddWithValue("@poId", currentPoId);
                        deleteItems.ExecuteNonQuery();
                    }

                    // insert new items
                    const string itemSql = @"
                        INSERT INTO PurchaseOrderItems
                            (po_id, product_id, quantity_ordered, unit_price, total_amount)
                        VALUES (@po, @prod, @qty, @price, @total);";

                    foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
                    {
                        if (row.Tag == null)
                            continue;

                        using (SqlCommand cmdItem = new SqlCommand(itemSql, con, tr))
                        {
                            cmdItem.Parameters.AddWithValue("@po", currentPoId);
                            cmdItem.Parameters.AddWithValue("@prod", row.Tag);
                            cmdItem.Parameters.AddWithValue("@qty", row.Cells["Quantity"].Value);

                            decimal price = decimal.Parse(row.Cells["UnitPrice"].Value.ToString());
                            decimal rowTotal = decimal.Parse(row.Cells["Total"].Value.ToString());

                            cmdItem.Parameters.AddWithValue("@price", price);
                            cmdItem.Parameters.AddWithValue("@total", rowTotal);
                            cmdItem.ExecuteNonQuery();
                        }
                    }

                    string newAudit = BuildAuditSnapshot();

                    AuditHelper.LogWithTransaction(
                        con,
                        tr,
                        AuditModule.SUPPLIERS,
                        "Updated purchase order " + tbxOrderNumber.Text.Trim(),
                        AuditActivityType.UPDATE,
                        "PurchaseOrders",
                        currentPoId.ToString(),
                        originalAuditSnapshot,
                        newAudit);

                    tr.Commit();

                    MessageBox.Show("Purchase Order saved successfully.");
                    originalAuditSnapshot = newAudit;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving purchase order: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        // -----------------------------
        //  CLOSE HANDLING (X + CANCEL)
        // -----------------------------
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.Close();
            }
        }

        // -----------------------------
        //  OTHER EVENTS
        // -----------------------------
        private void cbxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyPOStatusRules(cbxStatus.Text);
            ApplyStatusBusinessRules(cbxStatus.Text);
        }

        private void ParentContainer_Paint(object sender, PaintEventArgs e)
        {
            // no-op, kept for designer compatibility
        }

        private void X_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.Close();
            }
        }

        private void cancel_click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.Close();
            }
        }
    }
}
