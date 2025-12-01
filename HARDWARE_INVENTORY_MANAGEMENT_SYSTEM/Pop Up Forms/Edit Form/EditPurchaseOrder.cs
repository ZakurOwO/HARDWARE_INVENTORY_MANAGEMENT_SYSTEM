using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
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

        #region UI Rules

        public EditPurchaseOrder()
        {
            InitializeComponent();
            con = new SqlConnection(connectionString);
            // Hook events
            cbxStatus.SelectedIndexChanged += (s, e) => ApplyPOStatusRules(cbxStatus.Text);
            cbxTax.SelectedIndexChanged += (s, e) => UpdateTotals();
            nudShippingFee.ValueChanged += (s, e) => UpdateTotals();
            dgvPurchaseItems.CellContentClick += dgvPurchaseItems_CellContentClick;
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
            if (!ValidateForm()) return;

            try
            {
                con.Open();
                SqlTransaction tr = con.BeginTransaction();

                // INSERT PURCHASE ORDER HEADER
                string headerSql = @"
            INSERT INTO PurchaseOrders
            (po_number, po_date, expected_date, supplier_id, status, payment_status, notes, tax_type, shipping_fee, total_amount)
            VALUES (@num, @date, @exp, @sup, @status, @pay, @notes, @tax, @ship, @total);
            SELECT SCOPE_IDENTITY();";

                decimal total = decimal.Parse(lblGrandTotal.Text);

                SqlCommand cmdHeader = new SqlCommand(headerSql, con, tr);
                cmdHeader.Parameters.AddWithValue("@num", tbxOrderNumber.Text.Trim());
                cmdHeader.Parameters.AddWithValue("@date", dtpOrderDate.Value);
                cmdHeader.Parameters.AddWithValue("@exp", dtpExpectedDelivery.Value);
                cmdHeader.Parameters.AddWithValue("@sup", cbxSupplier.SelectedValue);
                cmdHeader.Parameters.AddWithValue("@status", cbxStatus.Text);
                cmdHeader.Parameters.AddWithValue("@pay", cbxPaymentStatus.Text);
                cmdHeader.Parameters.AddWithValue("@notes", rtxNotes.Text);
                cmdHeader.Parameters.AddWithValue("@tax", cbxTax.Text);
                cmdHeader.Parameters.AddWithValue("@ship", nudShippingFee.Value);
                cmdHeader.Parameters.AddWithValue("@total", total);

                int poId = Convert.ToInt32(cmdHeader.ExecuteScalar());

                // INSERT ITEMS
                string itemSql = @"
            INSERT INTO PurchaseOrderItems
            (po_id, product_id, quantity, unit_price, total_amount)
            VALUES (@po, @prod, @qty, @price, @total);";

                foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
                {
                    SqlCommand cmdItem = new SqlCommand(itemSql, con, tr);
                    cmdItem.Parameters.AddWithValue("@po", poId);
                    cmdItem.Parameters.AddWithValue("@prod", row.Tag);
                    cmdItem.Parameters.AddWithValue("@qty", row.Cells["Quantity"].Value);
                    cmdItem.Parameters.AddWithValue("@price", decimal.Parse(row.Cells["UnitPrice"].Value.ToString()));
                    cmdItem.Parameters.AddWithValue("@total", decimal.Parse(row.Cells["Total"].Value.ToString()));
                    cmdItem.ExecuteNonQuery();
                }

                tr.Commit();
                MessageBox.Show("Purchase Order saved successfully.");
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
    }
}
