using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class DeliveriesTables : UserControl
    {
        private string connectionString;

        public DeliveriesTables()
        {
            InitializeComponent();
            connectionString = ConnectionString.DataSource;
            LoadDeliveryData();
        }

        private void LoadDeliveryData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            d.DeliveryID,
                            t.TransactionID,
                            d.delivery_number,
                            d.delivery_date,
                            d.status,
                            d.customer_name,
                            d.created_at,
                            d.updated_at
                        FROM Deliveries d
                        LEFT JOIN Transactions t ON d.transaction_id = t.transaction_id
                        WHERE d.delivery_type = 'Sales_Delivery'
                        ORDER BY d.delivery_date DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    dgvDeliveries.Rows.Clear();

                    while (reader.Read())
                    {
                        // Get values that match your actual DataGridView columns
                        string deliveryID = reader["DeliveryID"]?.ToString() ?? string.Empty;
                        string transactionID = reader["TransactionID"]?.ToString() ?? string.Empty;
                        string deliveryNumber = reader["delivery_number"]?.ToString() ?? string.Empty;
                        string deliveryDate = reader["delivery_date"]?.ToString() ?? string.Empty;
                        string status = reader["status"]?.ToString() ?? string.Empty;
                        string customerName = reader["customer_name"]?.ToString() ?? string.Empty;
                        string createdAt = reader["created_at"]?.ToString() ?? string.Empty;
                        string updatedAt = reader["updated_at"]?.ToString() ?? string.Empty;

                        // Format dates if they are DateTime objects
                        if (DateTime.TryParse(deliveryDate, out DateTime deliveryDateValue))
                        {
                            deliveryDate = deliveryDateValue.ToString("MM/dd/yyyy");
                        }

                        if (DateTime.TryParse(createdAt, out DateTime createdAtValue))
                        {
                            createdAt = createdAtValue.ToString("MM/dd/yyyy");
                        }

                        if (DateTime.TryParse(updatedAt, out DateTime updatedAtValue))
                        {
                            updatedAt = updatedAtValue.ToString("MM/dd/yyyy");
                        }

                        // Add row with data that matches your column structure
                        dgvDeliveries.Rows.Add(
                            deliveryID,
                            transactionID,
                            deliveryNumber,
                            deliveryDate,
                            status,
                            customerName,
                            createdAt,
                            updatedAt
                        );
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading delivery data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDeliveries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // You can add action handling here if needed
            // For now, it's empty since you don't have an action column
        }

        public void RefreshData()
        {
            LoadDeliveryData();
        }

        // Optional: Add search functionality
        private void searchField1_TextChanged(object sender, EventArgs e)
        {
            SearchDeliveries(searchField1.Text);
        }

        private void SearchDeliveries(string searchText)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            d.DeliveryID,
                            t.TransactionID,
                            d.delivery_number,
                            d.delivery_date,
                            d.status,
                            d.customer_name,
                            d.created_at,
                            d.updated_at
                        FROM Deliveries d
                        LEFT JOIN Transactions t ON d.transaction_id = t.transaction_id
                        WHERE d.delivery_type = 'Sales_Delivery'
                        AND (d.DeliveryID LIKE @SearchText 
                             OR d.delivery_number LIKE @SearchText 
                             OR d.customer_name LIKE @SearchText
                             OR t.TransactionID LIKE @SearchText)
                        ORDER BY d.delivery_date DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    dgvDeliveries.Rows.Clear();

                    while (reader.Read())
                    {
                        string deliveryID = reader["DeliveryID"]?.ToString() ?? string.Empty;
                        string transactionID = reader["TransactionID"]?.ToString() ?? string.Empty;
                        string deliveryNumber = reader["delivery_number"]?.ToString() ?? string.Empty;
                        string deliveryDate = reader["delivery_date"]?.ToString() ?? string.Empty;
                        string status = reader["status"]?.ToString() ?? string.Empty;
                        string customerName = reader["customer_name"]?.ToString() ?? string.Empty;
                        string createdAt = reader["created_at"]?.ToString() ?? string.Empty;
                        string updatedAt = reader["updated_at"]?.ToString() ?? string.Empty;

                        // Format dates
                        if (DateTime.TryParse(deliveryDate, out DateTime deliveryDateValue))
                        {
                            deliveryDate = deliveryDateValue.ToString("MM/dd/yyyy");
                        }

                        if (DateTime.TryParse(createdAt, out DateTime createdAtValue))
                        {
                            createdAt = createdAtValue.ToString("MM/dd/yyyy");
                        }

                        if (DateTime.TryParse(updatedAt, out DateTime updatedAtValue))
                        {
                            updatedAt = updatedAtValue.ToString("MM/dd/yyyy");
                        }

                        dgvDeliveries.Rows.Add(
                            deliveryID,
                            transactionID,
                            deliveryNumber,
                            deliveryDate,
                            status,
                            customerName,
                            createdAt,
                            updatedAt
                        );
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching deliveries: {ex.Message}", "Search Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Optional: Add double-click to view details
        private void dgvDeliveries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get all data from the selected row
                DataGridViewRow row = dgvDeliveries.Rows[e.RowIndex];

                string deliveryID = row.Cells["DeliveryID"].Value?.ToString() ?? "N/A";
                string transactionID = row.Cells["Transaction_ID"].Value?.ToString() ?? "N/A";
                string deliveryNumber = row.Cells["Delivery_Number"].Value?.ToString() ?? "N/A";
                string deliveryDate = row.Cells["Delivery_Date"].Value?.ToString() ?? "N/A";
                string status = row.Cells["Status"].Value?.ToString() ?? "N/A";
                string customerName = row.Cells["Customer_name"].Value?.ToString() ?? "N/A";
                string createdAt = row.Cells["Created_At"].Value?.ToString() ?? "N/A";
                string updatedAt = row.Cells["updated_at"].Value?.ToString() ?? "N/A";

                string details = $"Delivery ID: {deliveryID}\n" +
                               $"Transaction ID: {transactionID}\n" +
                               $"Delivery Number: {deliveryNumber}\n" +
                               $"Delivery Date: {deliveryDate}\n" +
                               $"Status: {status}\n" +
                               $"Customer: {customerName}\n" +
                               $"Created: {createdAt}\n" +
                               $"Last Updated: {updatedAt}";

                MessageBox.Show(details, "Delivery Details",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}