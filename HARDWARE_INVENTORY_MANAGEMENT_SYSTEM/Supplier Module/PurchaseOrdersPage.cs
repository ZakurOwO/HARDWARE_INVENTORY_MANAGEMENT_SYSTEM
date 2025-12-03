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
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.Class_Components_of_Suppliier;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Pop_Up_Forms.Edit_Form;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public partial class PurchaseOrdersPage : UserControl
    {
        private SqlConnection con;
        private string connectionString = ConnectionString.DataSource;

        public PurchaseOrdersPage()
        {
            InitializeComponent();
            con = new SqlConnection(connectionString);

            // Load purchase orders when the page loads
            LoadPurchaseOrdersFromDatabase();

            // Setup DataGridView click events
            dgvSupplier.CellContentClick += DgvSupplier_CellContentClick;
            dgvSupplier.CellClick += DgvSupplier_CellContentClick;
        }

        private void DgvSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DateTime? poDate = null;
            object poDateTag = dgvSupplier.Rows[e.RowIndex].Cells["DateCreated"].Tag;
            if (poDateTag is DateTime poDateValue)
            {
                poDate = poDateValue;
            }
            bool isLocked = poDate.HasValue && (DateTime.Now - poDate.Value) >= TimeSpan.FromHours(12);

            // Ensure action buttons respond for both cell click and content click events
            if (e.ColumnIndex == dgvSupplier.Columns["View"].Index)
            {
                string poNumber = dgvSupplier.Rows[e.RowIndex].Cells["POID"].Value.ToString();
                OpenEditPurchaseOrderForm(poNumber, isLocked);
            }
            else if (e.ColumnIndex == dgvSupplier.Columns["Cancel"].Index)
            {
                if (isLocked)
                {
                    MessageBox.Show("This purchase order is more than 12 hours old and can no longer be cancelled.",
                        "Cancellation Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string poId = dgvSupplier.Rows[e.RowIndex].Cells["POID"].Value.ToString();
                string status = dgvSupplier.Rows[e.RowIndex].Cells["Status"].Value.ToString();

                // Don't allow cancellation of already cancelled orders
                if (status == "Cancelled")
                {
                    MessageBox.Show("This purchase order is already cancelled.", "Already Cancelled",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var result = MessageBox.Show($"Are you sure you want to cancel Purchase Order {poId}?",
                    "Cancel Purchase Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    CancelPurchaseOrder(poId, e.RowIndex, poDate);
                }
            }
        }

        private void CancelPurchaseOrder(string poNumber, int rowIndex, DateTime? poDate)
        {
            if (poDate.HasValue && (DateTime.Now - poDate.Value) >= TimeSpan.FromHours(12))
            {
                MessageBox.Show("This purchase order is more than 12 hours old and can no longer be cancelled.",
                    "Cancellation Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    string query = "UPDATE PurchaseOrders SET status = 'Cancelled', updated_at = GETDATE() WHERE po_number = @poNumber";

                    using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@poNumber", poNumber);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            AuditHelper.LogWithTransaction(
                                con,
                                transaction,
                                AuditModule.SUPPLIERS,
                                $"Cancelled purchase order {poNumber}",
                                AuditActivityType.UPDATE,
                                "PurchaseOrders",
                                poNumber);

                            transaction.Commit();

                            MessageBox.Show($"Purchase Order {poNumber} has been cancelled successfully.",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dgvSupplier.Rows[rowIndex].Cells["Status"].Value = "Cancelled";
                            dgvSupplier.Rows[rowIndex].Cells["Cancel"].ReadOnly = true;
                            return;
                        }

                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cancelling purchase order: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void OpenEditPurchaseOrderForm(string poNumber, bool lockEditing)
        {
            try
            {
                Form popup = new Form();
                popup.FormBorderStyle = FormBorderStyle.None;
                popup.StartPosition = FormStartPosition.CenterScreen;
                popup.BackColor = Color.White;
                popup.Size = new Size(853, 560);

                EditPurchaseOrder editForm = new EditPurchaseOrder();
                editForm.Dock = DockStyle.Fill;
                editForm.LoadPurchaseOrder(poNumber, lockEditing);

                popup.Controls.Add(editForm);
                popup.FormClosed += (s, args) => LoadPurchaseOrdersFromDatabase();
                popup.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open purchase order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadPurchaseOrdersFromDatabase()
        {
            try
            {
                dgvSupplier.Rows.Clear();

                string query = @"SELECT 
                                    po.po_number,
                                    s.supplier_name,
                                    po.po_date,
                                    po.total_amount,
                                    po.status
                                FROM PurchaseOrders po
                                INNER JOIN Suppliers s ON po.supplier_id = s.supplier_id
                                ORDER BY po.po_date DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string poNumber = reader["po_number"].ToString();
                        string supplierName = reader["supplier_name"].ToString();
                        DateTime poDate = Convert.ToDateTime(reader["po_date"]);
                        decimal totalAmount = Convert.ToDecimal(reader["total_amount"]);
                        string status = reader["status"].ToString();

                        bool isLocked = (DateTime.Now - poDate) >= TimeSpan.FromHours(12);

                        // Add row to DataGridView
                        int rowIndex = dgvSupplier.Rows.Add(
                            poNumber,
                            supplierName,
                            poDate.ToString("MMM dd, yyyy"),
                            "₱ " + totalAmount.ToString("N2"),
                            status,
                            Properties.Resources.edit_rectangle, // View/Edit icon
                            Properties.Resources.Deactivate_Circle1 // Cancel icon
                        );

                        dgvSupplier.Rows[rowIndex].Cells["DateCreated"].Tag = poDate;
                        dgvSupplier.Rows[rowIndex].Cells["Cancel"].ReadOnly = isLocked;

                        // Color code the status
                        DataGridViewRow row = dgvSupplier.Rows[rowIndex];
                        switch (status)
                        {
                            case "Pending":
                                row.Cells["Status"].Style.ForeColor = Color.Orange;
                                break;
                            case "Approved":
                                row.Cells["Status"].Style.ForeColor = Color.Blue;
                                break;
                            case "Ordered":
                                row.Cells["Status"].Style.ForeColor = Color.Purple;
                                break;
                            case "Received":
                                row.Cells["Status"].Style.ForeColor = Color.Green;
                                break;
                            case "Cancelled":
                                row.Cells["Status"].Style.ForeColor = Color.Red;
                                break;
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading purchase orders: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void btnMainButtonIcon_Click(object sender, EventArgs e)
        {
            Form popup = new Form();
            popup.FormBorderStyle = FormBorderStyle.None;
            popup.StartPosition = FormStartPosition.CenterScreen;
            popup.BackColor = Color.White;
            popup.Size = new Size(850, 600);

            AddPurchaseOrderForm addForm = new AddPurchaseOrderForm();
            addForm.Dock = DockStyle.Fill;

            popup.Controls.Add(addForm);

            // Reload purchase orders after the form is closed
            popup.FormClosed += (s, args) => LoadPurchaseOrdersFromDatabase();

            popup.ShowDialog();
        }
    }
}