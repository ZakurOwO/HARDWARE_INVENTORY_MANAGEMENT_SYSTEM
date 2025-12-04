using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public partial class DataGridTable : UserControl
    {
        public PageNumber PaginationControl { get; set; }
        private EditCustomerContainer editCustomerContainer = new EditCustomerContainer();
        private DataTable customerData;
        private string currentSearchTerm = string.Empty;
        private bool isInitialized;

        public DataGridTable()
        {
            InitializeComponent();
        }

        private void DataGridTable_Load(object sender, EventArgs e)
        {
            if (isInitialized) return;

            AddDataColumns();
            RefreshData();
            dgvCustomers.ClearSelection();
            isInitialized = true;
        }

        private void AddDataColumns()
        {
            dgvCustomers.Columns.Clear();

            // Add Customer_ID column (hidden)
            DataGridViewTextBoxColumn customerIdColumn = new DataGridViewTextBoxColumn
            {
                Name = "Customer_ID",
                HeaderText = "ID",
                DataPropertyName = "customer_id",
                Visible = false
            };
            customerIdColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns.Add(customerIdColumn);

            // Add Customer_Name column
            DataGridViewTextBoxColumn customerNameColumn = new DataGridViewTextBoxColumn
            {
                Name = "Customer_Name",
                HeaderText = "Customer Name",
                DataPropertyName = "customer_name",
                FillWeight = 150,
                Width = 150
            };
            customerNameColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns.Add(customerNameColumn);

            // Add Contact_Person column
            DataGridViewTextBoxColumn contactPersonColumn = new DataGridViewTextBoxColumn
            {
                Name = "Contact_Person",
                HeaderText = "Contact Number",
                DataPropertyName = "contact_number",
                FillWeight = 120,
                Width = 120
            };
            contactPersonColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns.Add(contactPersonColumn);

            // Add Address column
            DataGridViewTextBoxColumn addressColumn = new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "Address",
                DataPropertyName = "address",
                FillWeight = 200,
                Width = 200
            };
            addressColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns.Add(addressColumn);

            // Add Edit button column
            DataGridViewImageColumn editColumn = new DataGridViewImageColumn
            {
                Name = "Editbtn",
             
                Image = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Edit_Icon,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                FillWeight = 20
            };
            editColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns.Add(editColumn);

            // Add Deactivate button column
            DataGridViewImageColumn deactivateColumn = new DataGridViewImageColumn
            {
                Name = "DeactivateBtn",
            
                Image = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Deactivate_Circle2,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                FillWeight = 20
            };
            deactivateColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.Columns.Add(deactivateColumn);

            dgvCustomers.AutoGenerateColumns = false;

       
            dgvCustomers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.AllowUserToResizeColumns = false;
        }
        public void LoadCustomerData(string searchText = null)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
                {
                    string query = @"SELECT customer_id, customer_name, contact_number, address
                                     FROM Customers
                                     WHERE (@search IS NULL OR @search = '')
                                        OR (customer_name LIKE @pattern OR contact_number LIKE @pattern OR address LIKE @pattern)
                                     ORDER BY customer_name";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    string searchTerm = searchText?.Trim() ?? string.Empty;
                    da.SelectCommand.Parameters.AddWithValue("@search", searchTerm);
                    da.SelectCommand.Parameters.AddWithValue("@pattern", $"%{searchTerm}%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    customerData = dt;
                    currentSearchTerm = searchTerm;

                    if (PaginationControl != null)
                    {
                        PaginationControl.PageChanged -= PaginationControl_PageChanged;
                        PaginationControl.InitializePagination(dt, dgvCustomers, 10);
                        PaginationControl.PageChanged += PaginationControl_PageChanged;
                        RefreshGridData();
                    }
                    else
                    {
                        dgvCustomers.DataSource = dt;
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

            dgvCustomers.ClearSelection();
        }

        private void PaginationControl_PageChanged(object sender, int page)
        {
            RefreshGridData();
        }

        private void RefreshGridData()
        {
            if (PaginationControl != null)
            {
                dgvCustomers.DataSource = PaginationControl.GetCurrentPageData();
            }
        }

        private DataTable FilterCustomerData(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return customerData;

            string term = searchText.Trim();
            var filteredRows = customerData
                .AsEnumerable()
                .Where(row => ContainsCaseInsensitive(row.Field<string>("customer_name"), term)
                           || ContainsCaseInsensitive(row.Field<string>("contact_number"), term)
                           || ContainsCaseInsensitive(row.Field<string>("address"), term));

            if (!filteredRows.Any())
                return customerData.Clone();

            return filteredRows.CopyToDataTable();
        }

        private bool ContainsCaseInsensitive(string source, string term)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(term))
                return false;

            return source.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public void RefreshData(string searchText = null)
        {
            LoadCustomerData(searchText ?? currentSearchTerm);
        }

        public void ApplySearch(string searchText)
        {
            if (customerData == null)
            {
                LoadCustomerData(searchText);
                return;
            }

            var filtered = FilterCustomerData(searchText);

            if (PaginationControl != null)
            {
                PaginationControl.UpdateData(filtered, resetToFirstPage: true);
                RefreshGridData();
            }
            else
            {
                dgvCustomers.DataSource = filtered;
            }

            currentSearchTerm = searchText?.Trim() ?? string.Empty;
        }

        private void dgvCurrentStockReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Get the customer data from the row
            int customerId = Convert.ToInt32(dgvCustomers.Rows[e.RowIndex].Cells["Customer_ID"].Value);
            string customerName = dgvCustomers.Rows[e.RowIndex].Cells["Customer_Name"].Value?.ToString();
            string contactNumber = dgvCustomers.Rows[e.RowIndex].Cells["Contact_Person"].Value?.ToString();
            string address = dgvCustomers.Rows[e.RowIndex].Cells["Address"].Value?.ToString();

            if (string.IsNullOrEmpty(customerName)) return;

            if (e.ColumnIndex == dgvCustomers.Columns["Editbtn"].Index)
            {
                EditCustomer(customerId, customerName, contactNumber, address);
            }
            else if (e.ColumnIndex == dgvCustomers.Columns["DeactivateBtn"].Index)
            {
                DeactivateCustomer(customerId, customerName);
            }
        }

        private void EditCustomer(int customerId, string customerName, string contactNumber, string address)
        {
            try
            {
                // Find the main form
                var mainForm = this.FindForm() as MainDashBoard;

                if (mainForm != null)
                {
                    // Use the EditCustomerContainer to show the edit form
                    editCustomerContainer.ShowEditCustomerForm(mainForm, customerId, customerName, contactNumber, address);
                }
                else
                {
                    MessageBox.Show("Could not find the main form.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening edit form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeactivateCustomer(int customerId, string customerName)
        {
            try
            {
                var result = MessageBox.Show($"Are you sure you want to deactivate '{customerName}'?",
                    "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
                    {
                        string query = "DELETE FROM Customers WHERE customer_id = @customerId";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@customerId", customerId);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Customer '{customerName}' has been removed successfully.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 547) // Foreign key constraint violation
                {
                    MessageBox.Show($"Cannot delete customer '{customerName}' because they have existing transactions or deliveries.",
                        "Constraint Violation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show($"Error removing customer: {sqlEx.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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