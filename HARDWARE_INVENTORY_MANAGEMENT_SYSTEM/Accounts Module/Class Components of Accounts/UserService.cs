using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public static class UserService
    {
        public static List<ComboboxItem> LoadRoles()
        {
            List<ComboboxItem> roles = new List<ComboboxItem>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();
                    string query = "SELECT RoleID, role_name FROM Roles ORDER BY role_name";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                roles.Add(new ComboboxItem
                                {
                                    Text = reader["role_name"].ToString(),
                                    Value = reader["RoleID"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading roles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return roles;
        }

        public static bool AddUser(string fullname, string username, string password,
            string address, string roleId, string accountStatus)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();

                    string query = @"INSERT INTO Accounts 
                            (Fullname, username, password_hash, Address, RoleID, Account_status) 
                            VALUES 
                            (@fullname, @username, @password, @address, @roleId, @status)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@fullname", fullname);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", SecurityHelper.HashPassword(password));
                        cmd.Parameters.AddWithValue("@address", address ?? "");
                        cmd.Parameters.AddWithValue("@roleId", roleId);
                        cmd.Parameters.AddWithValue("@status", accountStatus);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            string newAccountId = DatabaseHelper.GetGeneratedAccountId(connection);
                            string roleName = GetRoleName(connection, roleId);

                            try
                            {
                                AuditHelper.Log(
                                    AuditModule.ACCOUNTS,
                                    $"Created new user account: {username} ({fullname}) with role {roleName}",
                                    AuditActivityType.CREATE,
                                    tableAffected: "Accounts",
                                    recordId: newAccountId
                                );
                            }
                            catch (Exception auditEx)
                            {
                                Console.WriteLine($"Audit log error: {auditEx.Message}");
                            }

                            return true;
                        }

                        return false;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.",
                        "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (sqlEx.Number == 515)
                {
                    MessageBox.Show("Database error: Required fields are missing. Please check your input.",
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Database error: {sqlEx.Message}",
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool UpdateUser(string accountId, string fullname, string username,
            string address, string roleId, string accountStatus, string password = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();

                    string oldValues = GetAccountDetails(connection, accountId);

                    string query;

                    if (!string.IsNullOrEmpty(password))
                    {
                        query = @"UPDATE Accounts 
                                SET Fullname = @fullname, 
                                    username = @username, 
                                    password_hash = @password,
                                    Address = @address, 
                                    RoleID = @roleId, 
                                    Account_status = @status
                                WHERE AccountID = @accountId";
                    }
                    else
                    {
                        query = @"UPDATE Accounts 
                                SET Fullname = @fullname, 
                                    username = @username, 
                                    Address = @address, 
                                    RoleID = @roleId, 
                                    Account_status = @status
                                WHERE AccountID = @accountId";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@accountId", accountId);
                        cmd.Parameters.AddWithValue("@fullname", fullname);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@address", address ?? "");
                        cmd.Parameters.AddWithValue("@roleId", roleId);
                        cmd.Parameters.AddWithValue("@status", accountStatus);

                        if (!string.IsNullOrEmpty(password))
                        {
                            cmd.Parameters.AddWithValue("@password", SecurityHelper.HashPassword(password));
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            string roleName = GetRoleName(connection, roleId);
                            string newValues = $"Fullname: {fullname}, Username: {username}, Role: {roleName}, Status: {accountStatus}";
                            if (!string.IsNullOrEmpty(password))
                            {
                                newValues += ", Password: [CHANGED]";
                            }

                            try
                            {
                                AuditHelper.LogWithDetails(
                                    AuditModule.ACCOUNTS,
                                    $"Updated user account: {username} ({fullname})",
                                    AuditActivityType.UPDATE,
                                    tableAffected: "Accounts",
                                    recordId: accountId,
                                    oldValues: oldValues,
                                    newValues: newValues
                                );
                            }
                            catch (Exception auditEx)
                            {
                                Console.WriteLine($"Audit log error: {auditEx.Message}");
                            }

                            return true;
                        }

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool DeleteUser(string accountId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();

                    string getUserQuery = "SELECT username, Fullname, Account_status FROM Accounts WHERE AccountID = @accountId";
                    string username = "";
                    string fullname = "";

                    using (SqlCommand getCmd = new SqlCommand(getUserQuery, connection))
                    {
                        getCmd.Parameters.AddWithValue("@accountId", accountId);
                        using (SqlDataReader reader = getCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                username = reader["username"].ToString();
                                fullname = reader["Fullname"].ToString();
                            }
                        }
                    }

                    string query = "DELETE FROM Accounts WHERE AccountID = @accountId";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@accountId", accountId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            try
                            {
                                AuditHelper.Log(
                                    AuditModule.ACCOUNTS,
                                    $"Deleted user account: {username} ({fullname})",
                                    AuditActivityType.DELETE,
                                    tableAffected: "Accounts",
                                    recordId: accountId
                                );
                            }
                            catch (Exception auditEx)
                            {
                                Console.WriteLine($"Audit log error: {auditEx.Message}");
                            }

                            return true;
                        }

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static string GetAccountDetails(SqlConnection connection, string accountId)
        {
            try
            {
                string query = @"SELECT a.Fullname, a.username, r.role_name, a.Account_status 
                               FROM Accounts a 
                               INNER JOIN Roles r ON a.RoleID = r.RoleID 
                               WHERE a.AccountID = @accountId";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return $"Fullname: {reader["Fullname"]}, Username: {reader["username"]}, " +
                                   $"Role: {reader["role_name"]}, Status: {reader["Account_status"]}";
                        }
                    }
                }
            }
            catch
            {
            }

            return "";
        }

        private static string GetRoleName(SqlConnection connection, string roleId)
        {
            try
            {
                string query = "SELECT role_name FROM Roles WHERE RoleID = @roleId";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@roleId", roleId);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? roleId;
                }
            }
            catch
            {
                return roleId;
            }
        }
    }
}