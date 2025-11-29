using System;
using System.Data;
using System.Data.SqlClient;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public class InventoryReportsDataAccess
    {
        private string connectionString;

        public InventoryReportsDataAccess()
        {
            connectionString = ConnectionString.DataSource;
        }

        #region Page 1 - Overview & Metrics

        public int GetTotalProducts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Products WHERE active = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public decimal GetInventoryValue()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT ISNULL(SUM(current_stock * SellingPrice), 0) 
                    FROM Products 
                    WHERE active = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                
                conn.Open();
                return (decimal)cmd.ExecuteScalar();
            }
        }

        public int GetLowStockCount()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Products 
                    WHERE active = 1 
                    AND current_stock <= reorder_point";
                SqlCommand cmd = new SqlCommand(query, conn);
                
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public int GetExpiryAlertCount()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT COUNT(DISTINCT product_id)
                    FROM ProductBatches
                    WHERE expiry_date IS NOT NULL
                    AND expiry_date <= DATEADD(DAY, 30, GETDATE())";
                SqlCommand cmd = new SqlCommand(query, conn);
                
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public DataTable GetInventoryOverview()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT TOP 10
                        p.ProductID,
                        p.product_name,
                        c.category_name,
                        p.current_stock,
                        p.reorder_point,
                        p.SellingPrice,
                        CASE 
                            WHEN p.current_stock <= 0 THEN 'Out of Stock'
                            WHEN p.current_stock <= p.reorder_point THEN 'Low Stock'
                            ELSE 'In Stock'
                        END as Status
                    FROM Products p
                    INNER JOIN Categories c ON p.category_id = c.CategoryID
                    WHERE p.active = 1
                    ORDER BY p.current_stock ASC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        #endregion

        #region Page 2 - Alerts & Warnings

        public DataTable GetLowStockAlerts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        p.ProductID,
                        p.product_name,
                        p.current_stock,
                        p.reorder_point,
                        s.supplier_name,
                        CASE 
                            WHEN p.current_stock <= 0 THEN 'Critical'
                            WHEN p.current_stock <= (p.reorder_point * 0.5) THEN 'Very Low'
                            ELSE 'Low'
                        END as Status
                    FROM Products p
                    LEFT JOIN PurchaseOrderItems poi ON p.ProductInternalID = poi.product_id
                    LEFT JOIN PurchaseOrders po ON poi.po_id = po.po_id
                    LEFT JOIN Suppliers s ON po.supplier_id = s.supplier_id
                    WHERE p.active = 1 
                    AND p.current_stock <= p.reorder_point
                    GROUP BY p.ProductID, p.product_name, p.current_stock, p.reorder_point, s.supplier_name
                    ORDER BY p.current_stock ASC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetExpiryAlerts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        p.ProductID,
                        p.product_name,
                        SUM(pb.quantity_received) as Quantity,
                        pb.expiry_date as ExpiryDate,
                        DATEDIFF(DAY, GETDATE(), pb.expiry_date) as DaysLeft,
                        CASE 
                            WHEN DATEDIFF(DAY, GETDATE(), pb.expiry_date) <= 0 THEN 'Expired'
                            WHEN DATEDIFF(DAY, GETDATE(), pb.expiry_date) <= 7 THEN 'Critical'
                            WHEN DATEDIFF(DAY, GETDATE(), pb.expiry_date) <= 30 THEN 'Warning'
                            ELSE 'Normal'
                        END as Status
                    FROM ProductBatches pb
                    INNER JOIN Products p ON pb.product_id = p.ProductInternalID
                    WHERE pb.expiry_date IS NOT NULL
                    AND pb.expiry_date <= DATEADD(DAY, 30, GETDATE())
                    GROUP BY p.ProductID, p.product_name, pb.expiry_date
                    ORDER BY pb.expiry_date ASC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        #endregion

        #region Page 3 - Stock In History

        public DataTable GetStockInHistory(DateTime? startDate = null, DateTime? endDate = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        d.DeliveryID as ReferenceNo,
                        d.delivery_date as Date,
                        p.product_name as ProductName,
                        s.supplier_name as Supplier,
                        di.quantity_received as QuantityIn,
                        CASE 
                            WHEN d.status = 'Completed' THEN 'Received'
                            WHEN d.status = 'Scheduled' THEN 'Pending'
                            ELSE d.status
                        END as Remarks
                    FROM DeliveryItems di
                    INNER JOIN Deliveries d ON di.delivery_id = d.delivery_id
                    INNER JOIN Products p ON di.product_id = p.ProductInternalID
                    LEFT JOIN PurchaseOrders po ON d.po_id = po.po_id
                    LEFT JOIN Suppliers s ON po.supplier_id = s.supplier_id
                    WHERE d.delivery_type = 'PO_Delivery'";

                if (startDate.HasValue)
                    query += " AND d.delivery_date >= @StartDate";
                if (endDate.HasValue)
                    query += " AND d.delivery_date <= @EndDate";

                query += " ORDER BY d.delivery_date DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                if (startDate.HasValue)
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                if (endDate.HasValue)
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        #endregion

        #region Page 4 - Stock Out History

        public DataTable GetStockOutHistory(DateTime? startDate = null, DateTime? endDate = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        t.TransactionID as ReferenceNo,
                        t.transaction_date as Date,
                        p.product_name as ProductName,
                        ti.quantity as QuantityOut,
                        CASE 
                            WHEN d.delivery_id IS NOT NULL THEN 'Delivery'
                            ELSE 'Walk-in Sale'
                        END as Reason,
                        CASE 
                            WHEN d.status = 'Completed' THEN 'Delivered'
                            WHEN d.status IS NULL THEN 'Completed'
                            ELSE d.status
                        END as Remarks
                    FROM TransactionItems ti
                    INNER JOIN Transactions t ON ti.transaction_id = t.transaction_id
                    INNER JOIN Products p ON ti.product_id = p.ProductInternalID
                    LEFT JOIN Deliveries d ON t.delivery_id = d.delivery_id
                    WHERE 1=1";

                if (startDate.HasValue)
                    query += " AND t.transaction_date >= @StartDate";
                if (endDate.HasValue)
                    query += " AND t.transaction_date <= @EndDate";

                query += " ORDER BY t.transaction_date DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                if (startDate.HasValue)
                    cmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                if (endDate.HasValue)
                    cmd.Parameters.AddWithValue("@EndDate", endDate.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        #endregion

        #region Helper Methods

        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}