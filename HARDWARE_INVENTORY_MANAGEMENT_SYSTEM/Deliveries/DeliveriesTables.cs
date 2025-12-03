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
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class DeliveriesTables : UserControl
    {
        private string connectionString;
        private ContextMenuStrip deliveriesContextMenu;

        public DeliveriesTables()
        {
            InitializeComponent();
            connectionString = ConnectionString.DataSource;

            InitializeContextMenu();

            // Right-click selection support
            dgvDeliveries.CellMouseDown += dgvDeliveries_CellMouseDown;

            LoadDeliveryData();
        }

        // ========================================
        //  LOAD & REFRESH
        // ========================================

        private void LoadDeliveryData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT
                            d.delivery_id,
                            d.DeliveryID,
                            d.delivery_number,
                            d.delivery_date,
                            d.status,
                            d.delivery_type,
                            COALESCE(t.TransactionID, po.POID, '') AS ReferenceId,
                            COALESCE(c.customer_name, d.customer_name, s.supplier_name, 'N/A') AS PartyName,
                            d.created_at,
                            d.updated_at
                        FROM Deliveries d
                        LEFT JOIN Transactions t ON d.transaction_id = t.transaction_id
                        LEFT JOIN Customers c ON t.customer_id = c.customer_id
                        LEFT JOIN PurchaseOrders po ON d.po_id = po.po_id
                        LEFT JOIN Suppliers s ON po.supplier_id = s.supplier_id
                        ORDER BY d.delivery_date DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    dgvDeliveries.Rows.Clear();

                    while (reader.Read())
                    {
                        int deliveryInternalId = reader["delivery_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["delivery_id"]);
                        string deliveryID = reader["DeliveryID"]?.ToString() ?? string.Empty;
                        string referenceId = reader["ReferenceId"]?.ToString() ?? string.Empty;
                        string deliveryNumber = reader["delivery_number"]?.ToString() ?? string.Empty;
                        string deliveryDate = FormatDate(reader["delivery_date"]);
                        string status = reader["status"]?.ToString() ?? string.Empty;
                        string partyName = reader["PartyName"]?.ToString() ?? string.Empty;
                        string createdAt = FormatDate(reader["created_at"]);
                        string updatedAt = FormatDate(reader["updated_at"]);

                        dgvDeliveries.Rows.Add(
                            deliveryInternalId,
                            deliveryID,
                            referenceId,
                            deliveryNumber,
                            deliveryDate,
                            status,
                            partyName,
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

        // Designer still uses this; we keep it
        private void dgvDeliveries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // No inline button actions yet – context menu handles actions.
        }

        public void RefreshData()
        {
            LoadDeliveryData();
        }

        // ========================================
        //  SEARCH
        // ========================================

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
                            d.delivery_id,
                            d.DeliveryID,
                            d.delivery_number,
                            d.delivery_date,
                            d.status,
                            d.delivery_type,
                            COALESCE(t.TransactionID, po.POID, '') AS ReferenceId,
                            COALESCE(c.customer_name, d.customer_name, s.supplier_name, 'N/A') AS PartyName,
                            d.created_at,
                            d.updated_at
                        FROM Deliveries d
                        LEFT JOIN Transactions t ON d.transaction_id = t.transaction_id
                        LEFT JOIN Customers c ON t.customer_id = c.customer_id
                        LEFT JOIN PurchaseOrders po ON d.po_id = po.po_id
                        LEFT JOIN Suppliers s ON po.supplier_id = s.supplier_id
                        WHERE (CAST(d.DeliveryID AS NVARCHAR(50)) LIKE @SearchText
                             OR d.delivery_number LIKE @SearchText
                             OR d.customer_name LIKE @SearchText
                             OR s.supplier_name LIKE @SearchText
                             OR CAST(t.TransactionID AS NVARCHAR(50)) LIKE @SearchText
                             OR CAST(po.POID AS NVARCHAR(50)) LIKE @SearchText)
                        ORDER BY d.delivery_date DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SearchText", $"%{searchText}%");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    dgvDeliveries.Rows.Clear();

                    while (reader.Read())
                    {
                        int deliveryInternalId = reader["delivery_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["delivery_id"]);
                        string deliveryID = reader["DeliveryID"]?.ToString() ?? string.Empty;
                        string referenceId = reader["ReferenceId"]?.ToString() ?? string.Empty;
                        string deliveryNumber = reader["delivery_number"]?.ToString() ?? string.Empty;
                        string deliveryDate = FormatDate(reader["delivery_date"]);
                        string status = reader["status"]?.ToString() ?? string.Empty;
                        string partyName = reader["PartyName"]?.ToString() ?? string.Empty;
                        string createdAt = FormatDate(reader["created_at"]);
                        string updatedAt = FormatDate(reader["updated_at"]);

                        dgvDeliveries.Rows.Add(
                            deliveryInternalId,
                            deliveryID,
                            referenceId,
                            deliveryNumber,
                            deliveryDate,
                            status,
                            partyName,
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

        private void LogDeliveryAudit(string activity, AuditActivityType activityType, string recordId)
        {
            int userId = 1;                  // TODO: use actual logged-in user id
            string username = "System";      // TODO: use actual logged-in username

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(@"
        INSERT INTO AuditLog
            (user_id, username, module, activity, activity_type,
             table_affected, record_id, old_values, new_values, ip_address)
        VALUES
            (@user_id, @username, @module, @activity, @activity_type,
             @table_affected, @record_id, @old_values, @new_values, @ip_address);", conn))
            {
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@module", "Deliveries");
                cmd.Parameters.AddWithValue("@activity", activity);
                cmd.Parameters.AddWithValue("@activity_type", activityType.ToString());
                cmd.Parameters.AddWithValue("@table_affected", "Deliveries");
                cmd.Parameters.AddWithValue("@record_id", (object)recordId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@old_values", DBNull.Value);
                cmd.Parameters.AddWithValue("@new_values", DBNull.Value);
                cmd.Parameters.AddWithValue("@ip_address", "127.0.0.1");

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ========================================
        //  DOUBLE CLICK = QUICK DETAILS
        // ========================================

        private void dgvDeliveries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
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

                LogDeliveryAudit($"Viewed delivery {deliveryID}", AuditActivityType.VIEW, deliveryID);
            }
        }

        // ========================================
        //  CONTEXT MENU (STATUS / DELETE)
        // ========================================

        private void InitializeContextMenu()
        {
            deliveriesContextMenu = new ContextMenuStrip();

            deliveriesContextMenu.Items.Add("Mark as Scheduled", null,
                (s, e) => ChangeSelectedStatus("Scheduled"));
            deliveriesContextMenu.Items.Add("Mark as In Transit", null,
                (s, e) => ChangeSelectedStatus("In Transit"));
            deliveriesContextMenu.Items.Add("Mark as Completed", null,
                (s, e) => ChangeSelectedStatus("Completed"));
            deliveriesContextMenu.Items.Add("Mark as Cancelled", null,
                (s, e) => ChangeSelectedStatus("Cancelled"));

            deliveriesContextMenu.Items.Add(new ToolStripSeparator());
            deliveriesContextMenu.Items.Add("Delete Delivery", null, DeleteSelectedDelivery);

            dgvDeliveries.ContextMenuStrip = deliveriesContextMenu;
        }

        private void dgvDeliveries_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvDeliveries.ClearSelection();
                dgvDeliveries.Rows[e.RowIndex].Selected = true;

                if (e.ColumnIndex >= 0)
                {
                    dgvDeliveries.CurrentCell = dgvDeliveries.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
            }
        }

        // --- STATUS CHANGE ---

        private void ChangeSelectedStatus(string newStatus)
        {
            var row = dgvDeliveries.CurrentRow;
            if (row == null) return;

            if (!int.TryParse(row.Cells["DeliveryInternalId"].Value?.ToString(), out int deliveryId) || deliveryId <= 0)
                return;

            string deliveryNumber = row.Cells["Delivery_Number"].Value?.ToString() ?? string.Empty;
            string oldStatus = row.Cells["Status"].Value?.ToString() ?? string.Empty;

            if (string.Equals(oldStatus, newStatus, StringComparison.OrdinalIgnoreCase))
                return; // nothing to change

            var result = MessageBox.Show(
                $"Change status of delivery {deliveryNumber} from '{oldStatus}' to '{newStatus}'?",
                "Confirm Status Change",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            UpdateDeliveryStatus(deliveryId, deliveryNumber, oldStatus, newStatus);
        }

        private void UpdateDeliveryStatus(int deliveryId, string deliveryNumber, string oldStatus, string newStatus)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        string updateQuery = @"
                            UPDATE Deliveries
                            SET status = @Status,
                                updated_at = GETDATE()
                            WHERE delivery_id = @DeliveryId";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Status", newStatus);
                            cmd.Parameters.AddWithValue("@DeliveryId", deliveryId);
                            cmd.ExecuteNonQuery();
                        }

                        string deliveryDisplayId = GetDeliveryDisplayId(connection, transaction, deliveryId);
                        string oldValues = $"status={oldStatus}";
                        string newValues = $"status={newStatus}";

                        AuditHelper.LogWithTransaction(
                            connection,
                            transaction,
                            AuditModule.DELIVERIES,
                            $"Updated status for delivery {deliveryDisplayId} to {newStatus}",
                            AuditActivityType.UPDATE,
                            "Deliveries",
                            deliveryDisplayId,
                            oldValues,
                            newValues);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                LoadDeliveryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating delivery status: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- DELETE DELIVERY ---

        private void DeleteSelectedDelivery(object sender, EventArgs e)
        {
            var row = dgvDeliveries.CurrentRow;
            if (row == null) return;

            if (!int.TryParse(row.Cells["DeliveryInternalId"].Value?.ToString(), out int deliveryId) || deliveryId <= 0)
                return;

            string deliveryNumber = row.Cells["Delivery_Number"].Value?.ToString() ?? string.Empty;

            var result = MessageBox.Show(
                $"Are you sure you want to delete delivery {deliveryNumber}?\n" +
                "This will also remove its line items and vehicle assignments.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            DeleteDelivery(deliveryId, deliveryNumber);
        }

        private void DeleteDelivery(int deliveryId, string deliveryNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Grab some info for the audit log
                        string selectQuery = @"
                            SELECT DeliveryID, status, customer_name
                            FROM Deliveries
                            WHERE delivery_id = @DeliveryId";

                        string status = null;
                        string customerName = null;
                        string deliveryDisplayId = null;

                        using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection, transaction))
                        {
                            selectCmd.Parameters.AddWithValue("@DeliveryId", deliveryId);
                            using (var reader = selectCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    deliveryDisplayId = reader["DeliveryID"]?.ToString();
                                    status = reader["status"]?.ToString();
                                    customerName = reader["customer_name"]?.ToString();
                                }
                            }
                        }

                        string oldValuesJson = $"status={status};customer_name={customerName}";

                        string deleteBatches = "DELETE FROM ProductBatches WHERE delivery_id = @DeliveryId";
                        using (SqlCommand cmd = new SqlCommand(deleteBatches, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DeliveryId", deliveryId);
                            cmd.ExecuteNonQuery();
                        }

                        string deleteAssignments = "DELETE FROM VehicleAssignments WHERE delivery_id = @DeliveryId";
                        using (SqlCommand cmd = new SqlCommand(deleteAssignments, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DeliveryId", deliveryId);
                            cmd.ExecuteNonQuery();
                        }

                        string deleteItems = "DELETE FROM DeliveryItems WHERE delivery_id = @DeliveryId";
                        using (SqlCommand cmd = new SqlCommand(deleteItems, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DeliveryId", deliveryId);
                            cmd.ExecuteNonQuery();
                        }

                        string deleteDelivery = "DELETE FROM Deliveries WHERE delivery_id = @DeliveryId";
                        using (SqlCommand cmd = new SqlCommand(deleteDelivery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@DeliveryId", deliveryId);
                            cmd.ExecuteNonQuery();
                        }

                        AuditHelper.LogWithTransaction(
                            connection,
                            transaction,
                            AuditModule.DELIVERIES,
                            $"Deleted delivery {deliveryDisplayId ?? deliveryNumber}",
                            AuditActivityType.UPDATE,
                            "Deliveries",
                            deliveryDisplayId ?? deliveryId.ToString(),
                            oldValuesJson,
                            null);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                LoadDeliveryData();

                MessageBox.Show("Delivery deleted successfully.", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting delivery: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string FormatDate(object dateValue)
        {
            if (dateValue == null || dateValue == DBNull.Value)
            {
                return string.Empty;
            }

            if (DateTime.TryParse(dateValue.ToString(), out DateTime parsed))
            {
                return parsed.ToString("MM/dd/yyyy");
            }

            return string.Empty;
        }

        private string GetDeliveryDisplayId(SqlConnection connection, SqlTransaction transaction, int deliveryId)
        {
            string query = "SELECT DeliveryID FROM Deliveries WHERE delivery_id = @DeliveryId";
            using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@DeliveryId", deliveryId);
                object result = cmd.ExecuteScalar();
                return result?.ToString() ?? deliveryId.ToString();
            }
        }
    }
}
