using System;
using System.Data;
using System.Data.SqlClient;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Supplier_Reports
{
    public class SupplierReportDataAccess
    {
        private readonly string connectionString = ConnectionString.DataSource;

        // -----------------------------
        // Supplier Directory (Page 1)
        // -----------------------------
        public DataTable GetSupplierDirectory()
        {
            string query = @"
                SELECT
                    SupplierID,
                    supplier_name AS SupplierName,
                    contact_person AS ContactPerson,
                    contact_number AS ContactInfo,
                    address AS Address,
                    'Active' AS Status
                FROM Suppliers
            ";

            return ExecuteQuery(query);
        }

        // -----------------------------
        // Purchase Orders Summary (Page 2)
        // -----------------------------
        public DataTable GetPurchaseOrderSummary()
        {
            string query = @"
                SELECT
                    PO.POID AS PONumber,
                    S.supplier_name AS SupplierName,
                    PO.po_date AS DateOrdered,
                    PO.expected_date AS DateReceived,
                    PO.total_amount AS TotalCost,
                    PO.status AS Status
                FROM PurchaseOrders PO
                INNER JOIN Suppliers S ON PO.supplier_id = S.supplier_id
            ";

            return ExecuteQuery(query);
        }

        // -----------------------------
        // Purchase Order Details (Page 3)
        // -----------------------------
        public DataTable GetPurchaseOrderDetails()
        {
            string query = @"
                SELECT
                    PO.POID AS PONumber,
                    P.product_name AS ProductName,
                    POI.quantity_ordered AS Quantity,
                    POI.unit_price AS UnitCost,
                    POI.total_amount AS Subtotal
                FROM PurchaseOrderItems POI
                INNER JOIN PurchaseOrders PO ON PO.po_id = POI.po_id
                INNER JOIN Products P ON P.ProductInternalID = POI.product_id
            ";

            return ExecuteQuery(query);
        }

        // -----------------------------
        // Helper method
        // -----------------------------
        private DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }
    }
}
