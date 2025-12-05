using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public static class ReportQueries
    {
        private static readonly string ConnectionString = Class_Components.ConnectionString.DataSource;

        public static ReportTable BuildInventoryCurrentStockReport()
        {
            ReportTable report = CreateBaseReport("Inventory Current Stock Report", "Active products with current stock");
            report.Headers.AddRange(new[]
            {
                "SKU",
                "Product ID",
                "Product Name",
                "Category",
                "Unit",
                "Current Stock",
                "Reorder Point",
                "Selling Price"
            });

            string query = @"
                SELECT
                    p.SKU,
                    p.ProductID,
                    p.product_name,
                    c.category_name,
                    u.unit_name,
                    p.current_stock,
                    p.reorder_point,
                    p.SellingPrice
                FROM Products p
                INNER JOIN Categories c ON p.category_id = c.CategoryID
                INNER JOIN Units u ON p.unit_id = u.UnitID
                WHERE p.active = 1
                ORDER BY p.product_name";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<string> row = new List<string>();
                    row.Add(reader[0].ToString());
                    row.Add(reader[1].ToString());
                    row.Add(reader[2].ToString());
                    row.Add(reader[3].ToString());
                    row.Add(reader[4].ToString());
                    row.Add(Convert.ToInt32(reader[5]).ToString(CultureInfo.InvariantCulture));
                    row.Add(Convert.ToInt32(reader[6]).ToString(CultureInfo.InvariantCulture));
                    row.Add(Convert.ToDecimal(reader[7]).ToString("N2", CultureInfo.InvariantCulture));
                    report.Rows.Add(row);
                }
                reader.Close();
            }

            return report;
        }

        public static ReportTable BuildSalesByProductReport(DateTime? startDate, DateTime? endDate)
        {
            ReportTable report = CreateBaseReport("Sales by Product Report", BuildDateRangeSubtitle(startDate, endDate));
            report.Headers.AddRange(new[]
            {
                "Product ID",
                "Product Name",
                "Category",
                "Quantity Sold",
                "Unit Price",
                "Total Sales"
            });

            string query = @"
                SELECT
                    p.ProductID,
                    p.product_name,
                    c.category_name,
                    SUM(ti.quantity) AS QtySold,
                    ti.selling_price,
                    SUM(ti.quantity * ti.selling_price) AS TotalSales
                FROM TransactionItems ti
                INNER JOIN Products p ON ti.product_id = p.ProductInternalID
                INNER JOIN Categories c ON p.category_id = c.CategoryID
                INNER JOIN Transactions t ON ti.transaction_id = t.transaction_id
                WHERE 1 = 1";

            AppendDateFilters(ref query, startDate, endDate, "t.transaction_date");

            query += @"
                GROUP BY p.ProductID, p.product_name, c.category_name, ti.selling_price
                ORDER BY TotalSales DESC";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                AddDateParameters(cmd, startDate, endDate);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<string> row = new List<string>();
                    row.Add(reader[0].ToString());
                    row.Add(reader[1].ToString());
                    row.Add(reader[2].ToString());
                    row.Add(Convert.ToInt32(reader[3]).ToString(CultureInfo.InvariantCulture));
                    row.Add(Convert.ToDecimal(reader[4]).ToString("N2", CultureInfo.InvariantCulture));
                    row.Add(Convert.ToDecimal(reader[5]).ToString("N2", CultureInfo.InvariantCulture));
                    report.Rows.Add(row);
                }
                reader.Close();
            }

            return report;
        }

        public static ReportTable BuildSalesByCustomerReport(DateTime? startDate, DateTime? endDate)
        {
            ReportTable report = CreateBaseReport("Sales by Customer Report", BuildDateRangeSubtitle(startDate, endDate));
            report.Headers.AddRange(new[]
            {
                "Customer ID",
                "Customer Name",
                "Contact",
                "Orders",
                "Total Quantity",
                "Total Sales"
            });

            string query = @"
                SELECT
                    c.CustomerID,
                    c.customer_name,
                    c.contact_number,
                    COUNT(DISTINCT t.transaction_id) AS Orders,
                    ISNULL(SUM(ti.quantity), 0) AS TotalQty,
                    ISNULL(SUM(ti.quantity * ti.selling_price), 0) AS TotalSales
                FROM Transactions t
                INNER JOIN Customers c ON t.customer_id = c.customer_id
                LEFT JOIN TransactionItems ti ON t.transaction_id = ti.transaction_id
                WHERE 1 = 1";

            AppendDateFilters(ref query, startDate, endDate, "t.transaction_date");

            query += @"
                GROUP BY c.CustomerID, c.customer_name, c.contact_number
                ORDER BY TotalSales DESC";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                AddDateParameters(cmd, startDate, endDate);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<string> row = new List<string>();
                    row.Add(reader[0].ToString());
                    row.Add(reader[1].ToString());
                    row.Add(reader.IsDBNull(2) ? "" : reader[2].ToString());
                    row.Add(Convert.ToInt32(reader[3]).ToString(CultureInfo.InvariantCulture));
                    row.Add(Convert.ToInt32(reader[4]).ToString(CultureInfo.InvariantCulture));
                    row.Add(Convert.ToDecimal(reader[5]).ToString("N2", CultureInfo.InvariantCulture));
                    report.Rows.Add(row);
                }
                reader.Close();
            }

            return report;
        }

        public static ReportTable BuildSalesSummaryReport(DateTime? startDate, DateTime? endDate)
        {
            ReportTable report = CreateBaseReport("Sales Summary Report", BuildDateRangeSubtitle(startDate, endDate));
            report.Headers.AddRange(new[]
            {
                "Date",
                "Transactions",
                "Quantity Sold",
                "Total Sales",
                "Avg Sale / Transaction"
            });

            string query = @"
                SELECT
                    CAST(t.transaction_date AS DATE) AS SalesDate,
                    COUNT(DISTINCT t.transaction_id) AS TransactionCount,
                    ISNULL(SUM(ti.quantity), 0) AS TotalQty,
                    ISNULL(SUM(ti.quantity * ti.selling_price), 0) AS TotalSales
                FROM Transactions t
                LEFT JOIN TransactionItems ti ON t.transaction_id = ti.transaction_id
                WHERE 1 = 1";

            AppendDateFilters(ref query, startDate, endDate, "t.transaction_date");

            query += @"
                GROUP BY CAST(t.transaction_date AS DATE)
                ORDER BY SalesDate";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                AddDateParameters(cmd, startDate, endDate);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime date = reader.GetDateTime(0);
                    int txCount = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                    int qty = reader.IsDBNull(2) ? 0 : Convert.ToInt32(reader[2]);
                    decimal totalSales = reader.IsDBNull(3) ? 0m : Convert.ToDecimal(reader[3]);
                    decimal avgSale = txCount == 0 ? 0m : totalSales / txCount;

                    List<string> row = new List<string>();
                    row.Add(date.ToString("yyyy-MM-dd"));
                    row.Add(txCount.ToString(CultureInfo.InvariantCulture));
                    row.Add(qty.ToString(CultureInfo.InvariantCulture));
                    row.Add(totalSales.ToString("N2", CultureInfo.InvariantCulture));
                    row.Add(avgSale.ToString("N2", CultureInfo.InvariantCulture));
                    report.Rows.Add(row);
                }
                reader.Close();
            }

            return report;
        }

        public static ReportTable BuildDeliverySummaryReport(DateTime? startDate, DateTime? endDate)
        {
            ReportTable report = CreateBaseReport("Delivery Summary Report", BuildDateRangeSubtitle(startDate, endDate));
            report.Headers.AddRange(new[] { "Delivery Date", "Status", "Deliveries", "Total Items" });

            string query = @"
                SELECT
                    CAST(d.delivery_date AS DATE) AS DeliveryDate,
                    d.status,
                    COUNT(*) AS Deliveries,
                    ISNULL(SUM(di.quantity_received), 0) AS TotalItems
                FROM Deliveries d
                LEFT JOIN DeliveryItems di ON d.delivery_id = di.delivery_id
                WHERE 1 = 1";

            AppendDateFilters(ref query, startDate, endDate, "d.delivery_date");

            query += @"
                GROUP BY CAST(d.delivery_date AS DATE), d.status
                ORDER BY DeliveryDate";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                AddDateParameters(cmd, startDate, endDate);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<string> row = new List<string>();
                    row.Add(((DateTime)reader[0]).ToString("yyyy-MM-dd"));
                    row.Add(reader[1].ToString());
                    row.Add(Convert.ToInt32(reader[2]).ToString(CultureInfo.InvariantCulture));
                    row.Add(Convert.ToInt32(reader[3]).ToString(CultureInfo.InvariantCulture));
                    report.Rows.Add(row);
                }
                reader.Close();
            }

            return report;
        }

        public static ReportTable BuildVehicleUtilizationReport(DateTime? startDate, DateTime? endDate)
        {
            ReportTable report = CreateBaseReport("Vehicle Utilization Report", BuildDateRangeSubtitle(startDate, endDate));
            report.Headers.AddRange(new[] { "Vehicle ID", "Plate Number", "Brand", "Model", "Vehicle Type", "Status", "Assignments" });

            string query = @"
                SELECT
                    v.VehicleID,
                    v.plate_number,
                    v.brand,
                    v.model,
                    v.vehicle_type,
                    v.status,
                    COUNT(va.assignment_id) AS Assignments
                FROM Vehicles v
                LEFT JOIN VehicleAssignments va ON v.vehicle_id = va.vehicle_id
                WHERE 1 = 1";

            AppendDateFilters(ref query, startDate, endDate, "va.assignment_date");

            query += @"
                GROUP BY v.VehicleID, v.plate_number, v.brand, v.model, v.vehicle_type, v.status
                ORDER BY v.VehicleID";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                AddDateParameters(cmd, startDate, endDate);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<string> row = new List<string>();
                    row.Add(reader[0].ToString());
                    row.Add(reader[1].ToString());
                    row.Add(reader.IsDBNull(2) ? string.Empty : reader[2].ToString());
                    row.Add(reader.IsDBNull(3) ? string.Empty : reader[3].ToString());
                    row.Add(reader.IsDBNull(4) ? string.Empty : reader[4].ToString());
                    row.Add(reader.IsDBNull(5) ? string.Empty : reader[5].ToString());
                    row.Add(Convert.ToInt32(reader[6]).ToString(CultureInfo.InvariantCulture));
                    report.Rows.Add(row);
                }
                reader.Close();
            }

            return report;
        }

        public static ReportTable BuildVehicleUsageTimelineReport(DateTime? startDate, DateTime? endDate)
        {
            ReportTable report = CreateBaseReport("Vehicle Usage Timeline", BuildDateRangeSubtitle(startDate, endDate));
            report.Headers.AddRange(new[] { "Assignment Date", "Plate Number", "Deliveries" });

            string query = @"
                SELECT
                    CAST(va.assignment_date AS DATE) AS AssignmentDate,
                    v.plate_number,
                    COUNT(*) AS Deliveries
                FROM VehicleAssignments va
                INNER JOIN Vehicles v ON va.vehicle_id = v.vehicle_id
                WHERE 1 = 1";

            AppendDateFilters(ref query, startDate, endDate, "va.assignment_date");

            query += @"
                GROUP BY CAST(va.assignment_date AS DATE), v.plate_number
                ORDER BY AssignmentDate";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                AddDateParameters(cmd, startDate, endDate);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<string> row = new List<string>();
                    row.Add(((DateTime)reader[0]).ToString("yyyy-MM-dd"));
                    row.Add(reader[1].ToString());
                    row.Add(Convert.ToInt32(reader[2]).ToString(CultureInfo.InvariantCulture));
                    report.Rows.Add(row);
                }
                reader.Close();
            }

            return report;
        }

        private static ReportTable CreateBaseReport(string title, string subtitle)
        {
            ReportTable report = new ReportTable();
            report.Title = title;
            report.Subtitle = subtitle;
            return report;
        }

        private static string BuildDateRangeSubtitle(DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue && !endDate.HasValue)
            {
                return "All Dates";
            }

            string start = startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : "...";
            string end = endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") : "...";
            return "Date Range: " + start + " - " + end;
        }

        private static void AppendDateFilters(ref string query, DateTime? startDate, DateTime? endDate, string columnName)
        {
            if (startDate.HasValue)
            {
                query += " AND " + columnName + " >= @StartDate";
            }

            if (endDate.HasValue)
            {
                query += " AND " + columnName + " <= @EndDate";
            }
        }

        private static void AddDateParameters(SqlCommand cmd, DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue)
            {
                cmd.Parameters.AddWithValue("@StartDate", startDate.Value);
            }

            if (endDate.HasValue)
            {
                cmd.Parameters.AddWithValue("@EndDate", endDate.Value);
            }
        }
    }
}
