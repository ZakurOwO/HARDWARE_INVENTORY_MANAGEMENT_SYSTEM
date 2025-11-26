using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data
{
    public class CustomerDataAccess
    {
        private string connectionString = ConnectionString.DataSource;

        public DataTable GetAllCustomers()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            CustomerID,
                            customer_name,
                            contact_number,
                            address,
                            created_at
                        FROM Customers 
                        ORDER BY customer_name";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading customers: {ex.Message}");
            }

            return dt;
        }

        public int GetTotalCustomers()
        {
            int count = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM Customers";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting total customers: {ex.Message}");
            }
            return count;
        }

        public int GetActiveCustomers()
        {
            int count = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT COUNT(DISTINCT c.customer_id) 
                        FROM Customers c
                        INNER JOIN Transactions t ON c.customer_id = t.customer_id
                        WHERE t.transaction_date >= DATEADD(MONTH, -6, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting active customers: {ex.Message}");
            }
            return count;
        }

        public decimal GetTotalCustomerPurchases()
        {
            decimal total = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT ISNULL(SUM(total_amount), 0) FROM Transactions WHERE customer_id IS NOT NULL";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        total = Convert.ToDecimal(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting total purchases: {ex.Message}");
            }
            return total;
        }

        public decimal GetAverageCustomerSpend()
        {
            decimal average = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            CASE 
                                WHEN COUNT(DISTINCT customer_id) > 0 
                                THEN SUM(total_amount) / COUNT(DISTINCT customer_id) 
                                ELSE 0 
                            END
                        FROM Transactions 
                        WHERE customer_id IS NOT NULL";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        average = Convert.ToDecimal(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting average spend: {ex.Message}");
            }
            return average;
        }

        public DataTable GetCustomerPurchaseSummary()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            c.CustomerID,
                            c.customer_name,
                            c.contact_number,
                            c.address,
                            COUNT(t.transaction_id) as transaction_count,
                            ISNULL(SUM(t.total_amount), 0) as total_spent,
                            MAX(t.transaction_date) as last_purchase_date
                        FROM Customers c
                        LEFT JOIN Transactions t ON c.customer_id = t.customer_id
                        GROUP BY c.customer_id, c.CustomerID, c.customer_name, c.contact_number, c.address
                        ORDER BY total_spent DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading customer summary: {ex.Message}");
            }

            return dt;
        }

        public DataTable GetCustomerTransactionDetails()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            c.customer_name AS CustomerName,
                            t.TransactionID AS InvoiceNo,
                            t.transaction_date AS Date,
                            t.total_amount AS TotalAmount,
                            ISNULL(t.payment_method, 'N/A') AS PaymentMethod,
                            ISNULL(d.notes, 'No remarks') AS Remarks
                        FROM Transactions t
                        INNER JOIN Customers c ON t.customer_id = c.customer_id
                        LEFT JOIN Deliveries d ON t.delivery_id = d.delivery_id
                        WHERE c.customer_name != 'Walk-in Customer'
                        ORDER BY t.transaction_date DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading transaction details: {ex.Message}");
            }

            return dt;
        }

        public DataTable GetWalkInTransactions()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            'Walk-in Customer' AS CustomerName,
                            t.TransactionID AS InvoiceNo,
                            t.transaction_date AS Date,
                            t.total_amount AS TotalAmount,
                            ISNULL(t.payment_method, 'N/A') AS PaymentMethod,
                            'Walk-in sale' AS Remarks
                        FROM Transactions t
                        WHERE t.customer_id IS NULL OR t.customer_id = (SELECT customer_id FROM Customers WHERE customer_name = 'Walk-in Customer')
                        ORDER BY t.transaction_date DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading walk-in transactions: {ex.Message}");
            }

            return dt;
        }

        public DataTable GetAllTransactionDetails()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            c.customer_name AS CustomerName,
                            t.TransactionID AS InvoiceNo,
                            t.transaction_date AS Date,
                            t.total_amount AS TotalAmount,
                            ISNULL(t.payment_method, 'N/A') AS PaymentMethod,
                            ISNULL(d.notes, 
                                CASE 
                                    WHEN c.customer_name = 'Walk-in Customer' THEN 'Walk-in sale'
                                    ELSE 'No remarks' 
                                END) AS Remarks
                        FROM Transactions t
                        LEFT JOIN Customers c ON t.customer_id = c.customer_id
                        LEFT JOIN Deliveries d ON t.delivery_id = d.delivery_id
                        ORDER BY t.transaction_date DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading all transaction details: {ex.Message}");
            }

            return dt;
        }
    }
}