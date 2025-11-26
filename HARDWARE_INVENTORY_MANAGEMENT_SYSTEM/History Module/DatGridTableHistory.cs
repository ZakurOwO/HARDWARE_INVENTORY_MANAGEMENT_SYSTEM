using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Data.SqlClient;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module
{
    public partial class DatGridTableHistory : UserControl
    {
        private string connectionString;

        public DatGridTableHistory()
        {
            InitializeComponent();
            connectionString = ConnectionString.DataSource;
        }

        private Image status_set(string status)
        {
            switch (status.ToLower())
            {
                case "completed":
                case "delivered":
                case "paid":
                    return Properties.Resources.Completed;
                case "pending":
                case "processing":
                    return Properties.Resources.Pending1;
                case "canceled":
                case "cancelled":
                case "refunded":
                    return Properties.Resources.Canceled1;
                default:
                    return Properties.Resources.Pending1;
            }
        }

        private void DatGridTableHistory_Load(object sender, EventArgs e)
        {
            LoadTransactionHistory();
        }

        public void LoadTransactionHistory()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            t.TransactionID,
                            CASE 
                                WHEN d.delivery_id IS NOT NULL THEN 'Delivery'
                                ELSE 'Walk-in'
                            END as Category,
                            FORMAT(t.transaction_date, 'yyyy-MM-dd') as Date,
                            COALESCE(c.customer_name, 'Walk-in Customer') as CustomerSupplier,
                            (
                                SELECT TOP 1 p.product_name 
                                FROM TransactionItems ti 
                                INNER JOIN Products p ON ti.product_id = p.ProductInternalID 
                                WHERE ti.transaction_id = t.transaction_id
                            ) as Item,
                            t.total_amount as Amount,
                            CASE 
                                WHEN d.delivery_id IS NOT NULL THEN d.status
                                ELSE 'Completed'
                            END as Status
                        FROM Transactions t
                        LEFT JOIN Customers c ON t.customer_id = c.customer_id
                        LEFT JOIN Deliveries d ON t.transaction_id = d.transaction_id
                        ORDER BY t.transaction_date DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    dgvHistory.Rows.Clear();

                    while (reader.Read())
                    {
                        string transactionID = reader["TransactionID"]?.ToString() ?? "N/A";
                        string category = reader["Category"]?.ToString() ?? "N/A";
                        string date = reader["Date"]?.ToString() ?? "N/A";
                        string customerSupplier = reader["CustomerSupplier"]?.ToString() ?? "N/A";
                        string item = reader["Item"]?.ToString() ?? "N/A";
                        decimal amount = reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"]) : 0;
                        string status = reader["Status"]?.ToString() ?? "Completed";

                        // If there are multiple items, show "Multiple Items"
                        if (string.IsNullOrEmpty(item))
                        {
                            item = "Multiple Items";
                        }
                        else
                        {
                            // Check if there are more items
                            string checkMultipleQuery = @"
                                SELECT COUNT(*) 
                                FROM TransactionItems 
                                WHERE transaction_id = (SELECT transaction_id FROM Transactions WHERE TransactionID = @TransactionID)";

                            using (SqlConnection conn2 = new SqlConnection(connectionString))
                            {
                                SqlCommand cmd2 = new SqlCommand(checkMultipleQuery, conn2);
                                cmd2.Parameters.AddWithValue("@TransactionID", transactionID);
                                conn2.Open();
                                int itemCount = Convert.ToInt32(cmd2.ExecuteScalar());
                                if (itemCount > 1)
                                {
                                    item = $"{item} + {itemCount - 1} more";
                                }
                            }
                        }

                        dgvHistory.Rows.Add(
                            transactionID,
                            category,
                            date,
                            customerSupplier,
                            item,
                            $"₱{amount:N2}",
                            status_set(status)
                        );
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transaction history: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Fallback to sample data if database fails
                LoadSampleData();
            }
        }

        private void LoadSampleData()
        {
            dgvHistory.Rows.Add("TRX-00001", "Walk-in", "2025-01-15", "John Doe", "Plywood 1/2mm", "₱2,400.00", status_set("Completed"));
            dgvHistory.Rows.Add("TRX-00002", "Delivery", "2025-02-20", "Jane Smith", "Granite Slab", "₱1,500.00", status_set("Pending"));
            dgvHistory.Rows.Add("TRX-00003", "Walk-in", "2025-03-10", "Mike Johnson", "Steel Beams + 2 more", "₱3,750.00", status_set("Completed"));
            dgvHistory.Rows.Add("TRX-00004", "Delivery", "2025-04-05", "Emily Davis", "Tempered Glass Sheets", "₱1,250.00", status_set("Completed"));
            dgvHistory.Rows.Add("TRX-00005", "Walk-in", "2025-05-12", "David Wilson", "Concrete Blocks", "₋3,600.00", status_set("Pending"));
        }

        public void RefreshHistory()
        {
            LoadTransactionHistory();
        }

        private void dgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvHistory.Columns["Status"].Index)
            {
                // Handle status cell click if needed
                string transactionID = dgvHistory.Rows[e.RowIndex].Cells["TransactionID"].Value?.ToString();
                string status = GetStatusFromImage(dgvHistory.Rows[e.RowIndex].Cells["Status"].Value as Image);

                MessageBox.Show($"Transaction: {transactionID}\nCurrent Status: {status}",
                    "Transaction Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string GetStatusFromImage(Image statusImage)
        {
            if (statusImage == Properties.Resources.Completed)
                return "Completed";
            else if (statusImage == Properties.Resources.Pending1)
                return "Pending";
            else if (statusImage == Properties.Resources.Canceled1)
                return "Canceled";
            else
                return "Unknown";
        }

        private void dgvHistory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Show transaction details when double-clicked
                ShowTransactionDetails(e.RowIndex);
            }
        }

        private void ShowTransactionDetails(int rowIndex)
        {
            try
            {
                string transactionID = dgvHistory.Rows[rowIndex].Cells["TransactionID"].Value?.ToString();

                if (string.IsNullOrEmpty(transactionID) || transactionID == "N/A") return;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            t.TransactionID,
                            t.transaction_date,
                            COALESCE(c.customer_name, 'Walk-in Customer') as customer_name,
                            t.total_amount,
                            t.payment_method,
                            t.cash_received,
                            t.change_amount,
                            d.status as delivery_status,
                            d.delivery_type
                        FROM Transactions t
                        LEFT JOIN Customers c ON t.customer_id = c.customer_id
                        LEFT JOIN Deliveries d ON t.transaction_id = d.transaction_id
                        WHERE t.TransactionID = @TransactionID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TransactionID", transactionID);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string date = reader["transaction_date"] != DBNull.Value ?
                            Convert.ToDateTime(reader["transaction_date"]).ToString("MM/dd/yyyy HH:mm") : "N/A";
                        string customer = reader["customer_name"]?.ToString() ?? "N/A";
                        decimal totalAmount = reader["total_amount"] != DBNull.Value ?
                            Convert.ToDecimal(reader["total_amount"]) : 0;
                        string paymentMethod = reader["payment_method"]?.ToString() ?? "N/A";
                        decimal cashReceived = reader["cash_received"] != DBNull.Value ?
                            Convert.ToDecimal(reader["cash_received"]) : 0;
                        decimal change = reader["change_amount"] != DBNull.Value ?
                            Convert.ToDecimal(reader["change_amount"]) : 0;
                        string deliveryStatus = reader["delivery_status"]?.ToString() ?? "N/A";
                        string deliveryType = reader["delivery_type"]?.ToString() ?? "N/A";

                        string details = $"Transaction Details:\n\n" +
                                       $"Transaction ID: {transactionID}\n" +
                                       $"Date: {date}\n" +
                                       $"Customer: {customer}\n" +
                                       $"Total Amount: ₱{totalAmount:N2}\n" +
                                       $"Payment Method: {paymentMethod}\n" +
                                       $"Cash Received: ₱{cashReceived:N2}\n" +
                                       $"Change: ₱{change:N2}\n";

                        if (deliveryType != "N/A")
                        {
                            details += $"Delivery Type: {deliveryType}\n" +
                                     $"Delivery Status: {deliveryStatus}";
                        }

                        MessageBox.Show(details, "Transaction Details",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transaction details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to be called after successful checkout
        public void OnCheckoutCompleted()
        {
            RefreshHistory();
            MessageBox.Show("Transaction completed successfully and added to history!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}