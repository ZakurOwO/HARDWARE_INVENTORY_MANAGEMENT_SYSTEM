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
        private SqlConnection con;
        private DataTable suppliersData;
        private string connectionString = ConnectionString.DataSource;

        public SupplierTable()
        {
            InitializeComponent();

            // Initialize the connection
            con = new SqlConnection(connectionString);

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

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    suppliersData = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(suppliersData);

                    foreach (DataRow row in suppliersData.Rows)
                    {
                        string supplierID = row["SupplierID"].ToString();
                        string supplierName = row["supplier_name"].ToString();
                        string contactPerson = row["contact_person"].ToString();
                        string contactNumber = row["contact_number"].ToString();
                        string email = row["email"] != DBNull.Value ? row["email"].ToString() : "N/A";
                        string address = row["address"] != DBNull.Value ? row["address"].ToString() : "N/A";
                        DateTime createdAt = Convert.ToDateTime(row["created_at"]);

                        // Add row to DataGridView
                        int rowIndex = dgvSupplier.Rows.Add(
                            Properties.Resources.Active1, // Status image (always active for now)
                            supplierName,
                            contactNumber,
                            address,
                            createdAt.ToString("yyyy-MM-dd"),
                            Properties.Resources.Edit_Blue,
                            Properties.Resources.Deactivate_Circle
                        );

                        // Store the SupplierID in the row's Tag for later use
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

            // Edit button clicked (column index 5)
            if (e.ColumnIndex == 5)
            {
                EditSupplier(supplierID, e.RowIndex);
            }
            // Deactivate button clicked (column index 6)
            else if (e.ColumnIndex == 6)
            {
                DeactivateSupplier(supplierID, e.RowIndex);
            }
        }

        private void EditSupplier(string supplierID, int rowIndex)
        {
            try
            {
                // Get supplier details
                string query = @"SELECT * FROM Suppliers WHERE SupplierID = @SupplierID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string message = $"Edit Supplier:\n\n" +
                                       $"ID: {reader["SupplierID"]}\n" +
                                       $"Name: {reader["supplier_name"]}\n" +
                                       $"Contact Person: {reader["contact_person"]}\n" +
                                       $"Contact Number: {reader["contact_number"]}\n" +
                                       $"Email: {(reader["email"] != DBNull.Value ? reader["email"].ToString() : "N/A")}\n" +
                                       $"Address: {(reader["address"] != DBNull.Value ? reader["address"].ToString() : "N/A")}\n\n" +
                                       $"Edit functionality to be implemented.";

                        MessageBox.Show(message, "Supplier Details",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    reader.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
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
                try
                {
                    string query = "DELETE FROM Suppliers WHERE SupplierID = @SupplierID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Supplier deleted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Remove row from grid
                            dgvSupplier.Rows.RemoveAt(rowIndex);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) // Foreign key constraint violation
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
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
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