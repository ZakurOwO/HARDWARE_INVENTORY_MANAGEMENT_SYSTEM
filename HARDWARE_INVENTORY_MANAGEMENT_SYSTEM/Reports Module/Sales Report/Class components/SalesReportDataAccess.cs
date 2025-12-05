using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Sales_Report
{
    /// <summary>
    /// Data access layer for sales reports
    /// </summary>
    public class SalesReportDataAccess
    {
        private readonly string connectionString;

        private static void AddDateRangeParameters(SqlCommand command, DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue)
            {
                command.Parameters.AddWithValue("@StartDate", startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                command.Parameters.AddWithValue("@EndDate", endDate.Value.Date.AddDays(1).AddSeconds(-1));
            }
        }

        public SalesReportDataAccess()
        {
            connectionString = ConnectionString.DataSource;
        }

        #region Sales by Product (Page 1)

        public List<SalesProductReport> GetSalesByProduct(DateTime? startDate = null, DateTime? endDate = null)
        {
            var salesData = new List<SalesProductReport>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            p.ProductID,
                            p.product_name,
                            c.category_name,
                            SUM(ti.quantity) as QuantitySold,
                            ti.selling_price as UnitPrice,
                            SUM(ti.quantity * ti.selling_price) as TotalSales
                        FROM TransactionItems ti
                        INNER JOIN Products p ON ti.product_id = p.ProductInternalID
                        INNER JOIN Categories c ON p.category_id = c.CategoryID
                        INNER JOIN Transactions t ON ti.transaction_id = t.transaction_id
                        WHERE 1=1";

                    if (startDate.HasValue)
                        query += " AND t.transaction_date >= @StartDate";
                    if (endDate.HasValue)
                        query += " AND t.transaction_date <= @EndDate";

                    query += @"
                        GROUP BY p.ProductID, p.product_name, c.category_name, ti.selling_price
                        ORDER BY SUM(ti.quantity * ti.selling_price) DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        AddDateRangeParameters(command, startDate, endDate);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            salesData.Add(new SalesProductReport
                            {
                                ProductID = reader.GetString(0),
                                ProductName = reader.GetString(1),
                                Category = reader.GetString(2),
                                QuantitySold = reader.GetInt32(3),
                                UnitPrice = reader.GetDecimal(4),
                                TotalSales = reader.GetDecimal(5)
                            });
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving sales by product: {ex.Message}", ex);
            }

            return salesData;
        }

        #endregion

        #region Sales by Customer (Page 2)

        public List<SalesCustomerReport> GetSalesByCustomer(DateTime? startDate = null, DateTime? endDate = null)
        {
            var salesData = new List<SalesCustomerReport>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            c.CustomerID,
                            c.customer_name,
                            c.contact_number,
                            COUNT(DISTINCT t.transaction_id) as TotalOrders,
                            SUM(ti.quantity) as TotalQuantity,
                            SUM(t.total_amount) as TotalSales
                        FROM Transactions t
                        INNER JOIN Customers c ON t.customer_id = c.customer_id
                        LEFT JOIN TransactionItems ti ON t.transaction_id = ti.transaction_id
                        WHERE 1=1";

                    if (startDate.HasValue)
                        query += " AND t.transaction_date >= @StartDate";
                    if (endDate.HasValue)
                        query += " AND t.transaction_date <= @EndDate";

                    query += @"
                        GROUP BY c.CustomerID, c.customer_name, c.contact_number
                        ORDER BY SUM(t.total_amount) DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        AddDateRangeParameters(command, startDate, endDate);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            salesData.Add(new SalesCustomerReport
                            {
                                CustomerID = reader.GetString(0),
                                CustomerName = reader.GetString(1),
                                Contact = reader.IsDBNull(2) ? "N/A" : reader.GetString(2),
                                TotalOrders = reader.GetInt32(3),
                                TotalQuantity = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                TotalSales = reader.GetDecimal(5)
                            });
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving sales by customer: {ex.Message}", ex);
            }

            return salesData;
        }

        #endregion

        #region Daily/Monthly Sales Summary (Page 3)

        public List<SalesSummaryReport> GetDailySalesSummary(DateTime? startDate = null, DateTime? endDate = null)
        {
            var salesData = new List<SalesSummaryReport>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            CAST(t.transaction_date AS DATE) as SalesDate,
                            COUNT(DISTINCT t.transaction_id) as TransactionCount,
                            ISNULL(SUM(ti.quantity), 0) as TotalQuantitySold,
                            SUM(t.total_amount) as TotalSales,
                            SUM(t.total_amount) * 0.3 as TotalProfit,
                            AVG(t.total_amount) as AvgSalePerTransaction
                        FROM Transactions t
                        LEFT JOIN TransactionItems ti ON t.transaction_id = ti.transaction_id
                        WHERE 1=1";

                    if (startDate.HasValue)
                        query += " AND t.transaction_date >= @StartDate";
                    if (endDate.HasValue)
                        query += " AND t.transaction_date <= @EndDate";

                    query += @"
                        GROUP BY CAST(t.transaction_date AS DATE)
                        ORDER BY CAST(t.transaction_date AS DATE) DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        AddDateRangeParameters(command, startDate, endDate);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            salesData.Add(new SalesSummaryReport
                            {
                                Date = reader.GetDateTime(0),
                                NoOfTransactions = reader.GetInt32(1),
                                TotalQuantitySold = reader.GetInt32(2),
                                TotalSales = reader.GetDecimal(3),
                                TotalProfit = reader.GetDecimal(4),
                                AvgSalePerTransaction = reader.GetDecimal(5)
                            });
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving daily sales summary: {ex.Message}", ex);
            }

            return salesData;
        }

        public List<SalesSummaryReport> GetMonthlySalesSummary(DateTime? startDate = null, DateTime? endDate = null)
        {
            var salesData = new List<SalesSummaryReport>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            DATEFROMPARTS(YEAR(t.transaction_date), MONTH(t.transaction_date), 1) as SalesMonth,
                            COUNT(DISTINCT t.transaction_id) as TransactionCount,
                            ISNULL(SUM(ti.quantity), 0) as TotalQuantitySold,
                            SUM(t.total_amount) as TotalSales,
                            SUM(t.total_amount) * 0.3 as TotalProfit,
                            AVG(t.total_amount) as AvgSalePerTransaction
                        FROM Transactions t
                        LEFT JOIN TransactionItems ti ON t.transaction_id = ti.transaction_id
                        WHERE 1=1";

                    if (startDate.HasValue)
                        query += " AND t.transaction_date >= @StartDate";
                    if (endDate.HasValue)
                        query += " AND t.transaction_date <= @EndDate";

                    query += @"
                        GROUP BY YEAR(t.transaction_date), MONTH(t.transaction_date)
                        ORDER BY YEAR(t.transaction_date) DESC, MONTH(t.transaction_date) DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        AddDateRangeParameters(command, startDate, endDate);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            salesData.Add(new SalesSummaryReport
                            {
                                Date = reader.GetDateTime(0),
                                NoOfTransactions = reader.GetInt32(1),
                                TotalQuantitySold = reader.GetInt32(2),
                                TotalSales = reader.GetDecimal(3),
                                TotalProfit = reader.GetDecimal(4),
                                AvgSalePerTransaction = reader.GetDecimal(5)
                            });
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving monthly sales summary: {ex.Message}", ex);
            }

            return salesData;
        }

        #endregion

        #region Key Metrics (Page 1)

        public SalesKeyMetrics GetKeyMetrics()
        {
            var metrics = new SalesKeyMetrics();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Total Revenue (All Time)
                    string revenueQuery = "SELECT ISNULL(SUM(total_amount), 0) FROM Transactions";
                    using (SqlCommand cmd = new SqlCommand(revenueQuery, connection))
                    {
                        metrics.TotalRevenue = (decimal)cmd.ExecuteScalar();
                    }

                    // Total Transactions (All Time)
                    string transactionQuery = "SELECT COUNT(*) FROM Transactions";
                    using (SqlCommand cmd = new SqlCommand(transactionQuery, connection))
                    {
                        metrics.TotalTransactions = (int)cmd.ExecuteScalar();
                    }

                    // Average Order Value
                    if (metrics.TotalTransactions > 0)
                    {
                        metrics.AvgOrderValue = metrics.TotalRevenue / metrics.TotalTransactions;
                    }

                    // Growth Rate (Month-over-Month)
                    string growthQuery = @"
                        WITH MonthlyRevenue AS (
                            SELECT 
                                YEAR(transaction_date) as Year,
                                MONTH(transaction_date) as Month,
                                SUM(total_amount) as Revenue
                            FROM Transactions
                            WHERE transaction_date >= DATEADD(MONTH, -2, GETDATE())
                            GROUP BY YEAR(transaction_date), MONTH(transaction_date)
                        )
                        SELECT 
                            CASE 
                                WHEN LAG(Revenue) OVER (ORDER BY Year, Month) = 0 THEN 0
                                ELSE ((Revenue - LAG(Revenue) OVER (ORDER BY Year, Month)) 
                                      / LAG(Revenue) OVER (ORDER BY Year, Month)) * 100
                            END as GrowthRate
                        FROM MonthlyRevenue
                        ORDER BY Year DESC, Month DESC";

                    using (SqlCommand cmd = new SqlCommand(growthQuery, connection))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            metrics.GrowthRate = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving key metrics: {ex.Message}", ex);
            }

            return metrics;
        }

        #endregion
    }

    #region Report Models

    public class SalesProductReport
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int QuantitySold { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalSales { get; set; }
    }

    public class SalesCustomerReport
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Contact { get; set; }
        public int TotalOrders { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalSales { get; set; }
    }

    public class SalesSummaryReport
    {
        public DateTime Date { get; set; }
        public int NoOfTransactions { get; set; }
        public int TotalQuantitySold { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal AvgSalePerTransaction { get; set; }
    }

    public class SalesKeyMetrics
    {
        public decimal TotalRevenue { get; set; }
        public int TotalTransactions { get; set; }
        public decimal AvgOrderValue { get; set; }
        public decimal GrowthRate { get; set; }
    }

    #endregion
}