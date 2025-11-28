// ReportsDatabaseHelper.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report.class_components
{
    public class ReportsDatabaseHelper
    {
        private string connectionString = @"Data Source=localhost;Initial Catalog=TopazHardwareDb;Integrated Security=True;TrustServerCertificate=True;";

        // Method to execute queries and return DataTable
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

        // Get inventory metrics for dashboard
        public Dictionary<string, object> GetInventoryMetrics()
        {
            var metrics = new Dictionary<string, object>();

            string totalProductsQuery = "SELECT COUNT(*) FROM Products WHERE active = 1";
            string inventoryValueQuery = @"
                SELECT ISNULL(SUM(p.current_stock * p.SellingPrice), 0) 
                FROM Products p WHERE p.active = 1";
            string lowStockQuery = @"
                SELECT COUNT(*) 
                FROM Products p 
                WHERE p.active = 1 AND p.current_stock <= p.reorder_point AND p.current_stock > 0";
            string expiryQuery = @"
                SELECT COUNT(DISTINCT pb.product_id)
                FROM ProductBatches pb
                INNER JOIN Products p ON pb.product_id = p.ProductInternalID
                WHERE pb.expiry_date IS NOT NULL 
                AND pb.expiry_date BETWEEN GETDATE() AND DATEADD(day, 30, GETDATE())
                AND pb.quantity_received > 0";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Total Products
                    using (SqlCommand cmd = new SqlCommand(totalProductsQuery, connection))
                    {
                        metrics["TotalProducts"] = (int)cmd.ExecuteScalar();
                    }

                    // Inventory Value
                    using (SqlCommand cmd = new SqlCommand(inventoryValueQuery, connection))
                    {
                        metrics["InventoryValue"] = (decimal)cmd.ExecuteScalar();
                    }

                    // Low Stock Alerts
                    using (SqlCommand cmd = new SqlCommand(lowStockQuery, connection))
                    {
                        metrics["LowStockAlerts"] = (int)cmd.ExecuteScalar();
                    }

                    // Expiry Alerts
                    using (SqlCommand cmd = new SqlCommand(expiryQuery, connection))
                    {
                        metrics["ExpiryAlerts"] = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory metrics: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Set default values
                metrics["TotalProducts"] = 0;
                metrics["InventoryValue"] = 0m;
                metrics["LowStockAlerts"] = 0;
                metrics["ExpiryAlerts"] = 0;
            }

            return metrics;
        }

        // Get current stock report
        public DataTable GetCurrentStockReport()
        {
            string query = @"
                SELECT 
                    p.ProductID,
                    p.product_name AS 'Product Name',
                    c.category_name AS 'Category',
                    u.unit_name AS 'Unit',
                    p.current_stock AS 'Current Stock',
                    p.reorder_point AS 'Reorder Level',
                    p.SellingPrice AS 'Price',
                    CASE 
                        WHEN p.current_stock <= p.reorder_point AND p.current_stock > 0 THEN 'Low Stock'
                        WHEN p.current_stock = 0 THEN 'Out of Stock'
                        ELSE 'In Stock'
                    END AS 'Status'
                FROM Products p
                INNER JOIN Categories c ON p.category_id = c.CategoryID
                INNER JOIN Units u ON p.unit_id = u.UnitID
                WHERE p.active = 1
                ORDER BY p.current_stock ASC, p.product_name";

            return ExecuteQuery(query);
        }

        // Get low stock alerts
        public DataTable GetLowStockAlerts()
        {
            string query = @"
                SELECT 
                    p.ProductID AS 'Product ID',
                    p.product_name AS 'Product Name',
                    p.current_stock AS 'Current Stock',
                    p.reorder_point AS 'Reorder Level',
                    COALESCE(s.supplier_name, 'No Supplier') AS 'Supplier',
                    CASE 
                        WHEN p.current_stock = 0 THEN 'Out of Stock'
                        ELSE 'Low Stock'
                    END AS 'Status'
                FROM Products p
                LEFT JOIN (
                    SELECT DISTINCT po.supplier_id, poi.product_id
                    FROM PurchaseOrderItems poi
                    INNER JOIN PurchaseOrders po ON poi.po_id = po.po_id
                ) AS latest_po ON p.ProductInternalID = latest_po.product_id
                LEFT JOIN Suppliers s ON latest_po.supplier_id = s.supplier_id
                WHERE p.active = 1 AND p.current_stock <= p.reorder_point
                ORDER BY p.current_stock ASC";

            return ExecuteQuery(query);
        }

        // Get expiry alerts
        public DataTable GetExpiryAlerts()
        {
            string query = @"
                SELECT 
                    p.ProductID AS 'Product ID',
                    p.product_name AS 'Product Name',
                    pb.batch_number AS 'Batch No',
                    pb.quantity_received AS 'Quantity',
                    pb.expiry_date AS 'Expiry Date',
                    DATEDIFF(day, GETDATE(), pb.expiry_date) AS 'Days Left',
                    CASE 
                        WHEN pb.expiry_date < GETDATE() THEN 'Expired'
                        WHEN DATEDIFF(day, GETDATE(), pb.expiry_date) <= 7 THEN 'Critical'
                        WHEN DATEDIFF(day, GETDATE(), pb.expiry_date) <= 30 THEN 'Warning'
                        ELSE 'Normal'
                    END AS 'Status'
                FROM ProductBatches pb
                INNER JOIN Products p ON pb.product_id = p.ProductInternalID
                WHERE pb.expiry_date IS NOT NULL 
                AND pb.quantity_received > 0
                AND (pb.expiry_date < DATEADD(day, 30, GETDATE()))
                ORDER BY pb.expiry_date ASC";

            return ExecuteQuery(query);
        }

        // Get stock in history
        public DataTable GetStockInHistory()
        {
            string query = @"
                SELECT 
                    d.delivery_number AS 'Reference No',
                    FORMAT(d.delivery_date, 'MM/dd/yyyy') AS 'Date',
                    p.product_name AS 'Product Name',
                    s.supplier_name AS 'Supplier',
                    di.quantity_received AS 'Quantity In',
                    COALESCE(d.notes, 'N/A') AS 'Remarks'
                FROM Deliveries d
                INNER JOIN DeliveryItems di ON d.delivery_id = di.delivery_id
                INNER JOIN Products p ON di.product_id = p.ProductInternalID
                LEFT JOIN PurchaseOrders po ON d.po_id = po.po_id
                LEFT JOIN Suppliers s ON po.supplier_id = s.supplier_id
                WHERE d.delivery_type = 'PO_Delivery'
                ORDER BY d.delivery_date DESC";

            return ExecuteQuery(query);
        }

        // Get stock out history
        public DataTable GetStockOutHistory()
        {
            string query = @"
                SELECT 
                    t.TransactionID AS 'Reference No',
                    FORMAT(t.transaction_date, 'MM/dd/yyyy') AS 'Date',
                    p.product_name AS 'Product Name',
                    ti.quantity AS 'Quantity Out',
                    'Sale' AS 'Reason',
                    COALESCE(c.customer_name, 'Walk-in Customer') AS 'Remarks'
                FROM Transactions t
                INNER JOIN TransactionItems ti ON t.transaction_id = ti.transaction_id
                INNER JOIN Products p ON ti.product_id = p.ProductInternalID
                LEFT JOIN Customers c ON t.customer_id = c.customer_id
                ORDER BY t.transaction_date DESC";

            return ExecuteQuery(query);
        }
    }
}