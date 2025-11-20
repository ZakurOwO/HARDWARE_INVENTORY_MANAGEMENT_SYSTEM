using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public static class DatabaseHelper
    {
        /// <summary>
        /// Load all existing users from database as DataTable
        /// </summary>
        public static DataTable LoadExistingUsersFromDatabase()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            a.AccountID,
                            a.Fullname,
                            a.username,
                            a.Account_status,
                            a.Address,
                            a.created_at,
                            r.role_name as Role
                        FROM Accounts a
                        INNER JOIN Roles r ON a.RoleID = r.RoleID
                        ORDER BY a.created_at DESC";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users from database: {ex.Message}", "Database Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        /// <summary>
        /// Get the next available AccountID
        /// </summary>
        public static string GetNextAccountId()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();
                    string query = "SELECT TOP 1 AccountID FROM Accounts ORDER BY AccountInternalID DESC";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string lastAccountId = result.ToString();
                            // Extract numeric part (e.g., "ACC-00001" -> 1)
                            string numericPart = lastAccountId.Replace("ACC-", "");
                            if (int.TryParse(numericPart, out int lastNumber))
                            {
                                int nextNumber = lastNumber + 1;
                                return $"ACC-{nextNumber:D5}";
                            }
                        }

                        // If no records exist, start from ACC-00001
                        return "ACC-00001";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting next AccountID: {ex.Message}");
                return "ACC-00001";
            }
        }

        /// <summary>
        /// Get the generated AccountID after insert
        /// </summary>
        public static string GetGeneratedAccountId(SqlConnection connection)
        {
            try
            {
                string query = "SELECT TOP 1 AccountID FROM Accounts ORDER BY AccountInternalID DESC";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    return cmd.ExecuteScalar()?.ToString() ?? "ACC-00000";
                }
            }
            catch
            {
                return "ACC-00000";
            }
        }

        /// <summary>
        /// Check if username already exists in database
        /// </summary>
        public static bool IsUsernameExists(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Accounts WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking username: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Test database connection
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}