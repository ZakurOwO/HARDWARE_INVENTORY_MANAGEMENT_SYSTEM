using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public partial class DataGridTable : UserControl
    {
        public PageNumber PaginationControl { get; set; }

        public DataGridTable()
        {
            InitializeComponent();
        }

        private void DataGridTable_Load(object sender, EventArgs e)
        {
            // Add the text columns first
            AddDataColumns();
            // Then load the data
            LoadCustomerData();
        }

        private void AddDataColumns()
        {
            // Clear existing columns first (except action buttons which are in designer)
            // We'll remove the action buttons temporarily and re-add them at the end
            var editBtn = dgvCurrentStockReport.Columns["EditBtn"];
            var deactivateBtn = dgvCurrentStockReport.Columns["DeactivateBtn"];

            dgvCurrentStockReport.Columns.Clear();

            // Add Customer_Name column
            dgvCurrentStockReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Customer_Name",
                HeaderText = "Customer Name",
                FillWeight = 150
            });

            // Add Contact_Person column
            dgvCurrentStockReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Contact_Person",
                HeaderText = "Contact Person",
                FillWeight = 150
            });

            // Add Address column
            dgvCurrentStockReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "Address",
                FillWeight = 150
            });

            // Re-add the action buttons at the end (far right)
            dgvCurrentStockReport.Columns.Add(editBtn);
            dgvCurrentStockReport.Columns.Add(deactivateBtn);

            // Set AutoGenerateColumns to false to use our custom columns
            dgvCurrentStockReport.AutoGenerateColumns = false;
        }

        public void LoadCustomerData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
                {
                    string query = "SELECT customer_name, contact_number, address FROM Customers";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Map database columns to our DataGridView columns
                    MapDataToGridColumns();

                    if (PaginationControl != null)
                    {
                        PaginationControl.InitializePagination(dt, dgvCurrentStockReport, 10);
                        PaginationControl.PageChanged += (s, page) => RefreshGridData();
                        RefreshGridData();
                    }
                    else
                    {
                        dgvCurrentStockReport.DataSource = dt;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Database error: {sqlEx.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MapDataToGridColumns()
        {
            // Map database fields to our DataGridView columns
            if (dgvCurrentStockReport.Columns.Contains("Customer_Name"))
            {
                dgvCurrentStockReport.Columns["Customer_Name"].DataPropertyName = "customer_name";
            }

            if (dgvCurrentStockReport.Columns.Contains("Contact_Person"))
            {
                dgvCurrentStockReport.Columns["Contact_Person"].DataPropertyName = "contact_number";
            }

            if (dgvCurrentStockReport.Columns.Contains("Address"))
            {
                dgvCurrentStockReport.Columns["Address"].DataPropertyName = "address";
            }
        }

        private void RefreshGridData()
        {
            if (PaginationControl != null)
            {
                dgvCurrentStockReport.DataSource = PaginationControl.GetCurrentPageData();
            }
        }

        public void RefreshData()
        {
            LoadCustomerData();
        }

        private void dgvCurrentStockReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string customerName = dgvCurrentStockReport.Rows[e.RowIndex].Cells["Customer_Name"].Value?.ToString();

            if (string.IsNullOrEmpty(customerName)) return;

            if (e.ColumnIndex == dgvCurrentStockReport.Columns["EditBtn"].Index)
            {
                MessageBox.Show($"Edit customer: {customerName}", "Edit Customer");
            }
            else if (e.ColumnIndex == dgvCurrentStockReport.Columns["DeactivateBtn"].Index)
            {
                var result = MessageBox.Show($"Are you sure you want to deactivate {customerName}?",
                    "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeactivateCustomer(customerName);
                }
            }
        }

        private void DeactivateCustomer(string customerName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
                {
                    string query = "DELETE FROM Customers WHERE customer_name = @customerName";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@customerName", customerName);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Customer {customerName} has been removed.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing customer: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCurrentStockReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCurrentStockReport_CellClick(sender, e);
        }
    }
}