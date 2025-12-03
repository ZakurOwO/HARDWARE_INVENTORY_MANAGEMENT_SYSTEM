using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public partial class SupplierTable : UserControl
    {
        private readonly string connectionString = ConnectionString.DataSource;
        private readonly SupplierEditContainer supplierEditContainer = new SupplierEditContainer();

        public SupplierTable()
        {
            InitializeComponent();

            // Wire up event handlers programmatically
            this.Load += SupplierTable_Load;
            dgvSupplier.CellClick += dgvSupplier_CellClick;
        }

        private void SupplierTable_Load(object sender, EventArgs e)
        {
            LoadSuppliersFromDatabase();
        }

        public void LoadSuppliersFromDatabase()
        {
            try
            {
                dgvSupplier.Rows.Clear();

                string query = @"SELECT
                    supplier_id,
                    SupplierID,
                    supplier_name,
                    contact_person,
                    contact_number,
                    email,
                    address,
                    created_at,
                    updated_at
                FROM Suppliers
                ORDER BY created_at DESC";

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    DataTable suppliersData = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(suppliersData);

                    foreach (DataRow row in suppliersData.Rows)
                    {
                        string supplierID = row["SupplierID"].ToString();
                        string supplierName = row["supplier_name"].ToString();
                        string contactNumber = row["contact_number"].ToString();
                        string address = row["address"] != DBNull.Value ? row["address"].ToString() : "N/A";
                        DateTime createdAt = Convert.ToDateTime(row["created_at"]);

                        int rowIndex = dgvSupplier.Rows.Add(
                            Properties.Resources.Active1,
                            supplierName,
                            contactNumber,
                            address,
                            createdAt.ToString("yyyy-MM-dd"),
                            Properties.Resources.Edit_Blue,
                            Properties.Resources.Deactivate_Circle
                        );

                        dgvSupplier.Rows[rowIndex].Tag = supplierID;
                    }

                    dgvSupplier.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header clicks

            // Get the supplier ID from the row's Tag
            string supplierID = dgvSupplier.Rows[e.RowIndex].Tag?.ToString();

            if (supplierID == null) return;

            if (e.ColumnIndex == 5)
            {
                EditSupplier(supplierID);
            }
            else if (e.ColumnIndex == 6)
            {
                DeactivateSupplier(supplierID, e.RowIndex);
            }
        }

        private void EditSupplier(string supplierID)
        {
            try
            {
                SupplierRecord supplier = GetSupplierById(supplierID);
                if (supplier == null)
                {
                    MessageBox.Show("Unable to locate this supplier record.", "Not Found",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MainDashBoard main = this.FindForm() as MainDashBoard;
                if (main == null)
                {
                    MessageBox.Show("Unable to open edit form without main dashboard context.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                supplierEditContainer.ShowSupplierEditForm(main, this, supplier);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeactivateSupplier(string supplierID, int rowIndex)
        {
            var result = MessageBox.Show(
                $"Are you sure you want to delete this supplier?\n\nSupplier ID: {supplierID}\n\nThis action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                SupplierRecord supplier = GetSupplierById(supplierID);
                if (supplier == null)
                {
                    MessageBox.Show("Unable to find this supplier.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlTransaction transaction = null;
                    try
                    {
                        con.Open();
                        transaction = con.BeginTransaction();

                        using (SqlCommand deleteItems = new SqlCommand(@"DELETE FROM PurchaseOrderItems
                                WHERE po_id IN (SELECT po_id FROM PurchaseOrders WHERE supplier_id = @SupplierInternalID)", con, transaction))
                        using (SqlCommand deletePOs = new SqlCommand(@"DELETE FROM PurchaseOrders WHERE supplier_id = @SupplierInternalID", con, transaction))
                        using (SqlCommand deleteSupplier = new SqlCommand(@"DELETE FROM Suppliers WHERE SupplierID = @SupplierID", con, transaction))
                        {
                            deleteItems.Parameters.AddWithValue("@SupplierInternalID", supplier.SupplierInternalId);
                            deletePOs.Parameters.AddWithValue("@SupplierInternalID", supplier.SupplierInternalId);
                            deleteSupplier.Parameters.AddWithValue("@SupplierID", supplierID);

                            deleteItems.ExecuteNonQuery();
                            deletePOs.ExecuteNonQuery();

                            int rowsAffected = deleteSupplier.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                throw new InvalidOperationException("Supplier could not be deleted.");
                            }
                        }

                        SupplierAuditLogger.LogSupplierAudit(
                            con,
                            transaction,
                            activity: $"Deleted supplier {supplier.SupplierName}",
                            activityType: "DELETE",
                            recordId: supplier.SupplierID ?? supplier.SupplierInternalId.ToString(),
                            oldValues: SupplierAuditLogger.BuildSupplierState(supplier),
                            newValues: null);

                        transaction.Commit();

                        MessageBox.Show("Supplier deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        dgvSupplier.Rows.RemoveAt(rowIndex);
                    }
                    catch (SqlException ex)
                    {
                        transaction?.Rollback();
                        if (ex.Number == 547)
                        {
                            MessageBox.Show(
                                "Cannot delete this supplier because it has related records (Purchase Orders, etc.).\n\n" +
                                "Please delete or reassign related records first.",
                                "Cannot Delete",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                        else
                        {
                            MessageBox.Show($"Database error: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction?.Rollback();
                        MessageBox.Show($"Error: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private SupplierRecord GetSupplierById(string supplierID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(@"SELECT supplier_id, SupplierID, supplier_name, contact_person, contact_number, address, email
                    FROM Suppliers WHERE SupplierID = @SupplierID", con))
            {
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new SupplierRecord
                        {
                            SupplierInternalId = Convert.ToInt32(reader["supplier_id"]),
                            SupplierID = reader["SupplierID"].ToString(),
                            SupplierName = reader["supplier_name"].ToString(),
                            ContactPerson = reader["contact_person"].ToString(),
                            ContactNumber = reader["contact_number"].ToString(),
                            Address = reader["address"] != DBNull.Value ? reader["address"].ToString() : null,
                            Email = reader["email"] != DBNull.Value ? reader["email"].ToString() : null
                        };
                    }
                }
            }

            return null;
        }

        private Image Action_Set(string EditColBtn)
        {
            return Properties.Resources.Edit_Blue;
        }

        private Image Action_Set1(string DeActiColBtn)
        {
            return Properties.Resources.Deactivate_Circle;
        }

        private Image status_set(string status)
        {
            switch (status)
            {
                case "Active":
                    return Properties.Resources.Active1;
                case "Inactive":
                    return Properties.Resources.Inactive2;
                default:
                    return Properties.Resources.Active1;
            }
        }
    }
}