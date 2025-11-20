using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public static class UserService
    {
        /// <summary>
        /// Load all roles from database
        /// </summary>
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

        /// <summary>
        /// Add a new user to the database
        /// </summary>
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
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@roleId", roleId);
                        cmd.Parameters.AddWithValue("@status", accountStatus);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
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

        /// <summary>
        /// Update an existing user in the database
        /// </summary>
        public static bool UpdateUser(string accountId, string fullname, string username,
            string address, string roleId, string accountStatus, string password = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();

                    string query;

                    // Include password update if provided
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
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@roleId", roleId);
                        cmd.Parameters.AddWithValue("@status", accountStatus);

                        if (!string.IsNullOrEmpty(password))
                        {
                            cmd.Parameters.AddWithValue("@password", SecurityHelper.HashPassword(password));
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
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

        /// <summary>
        /// Delete a user from the database
        /// </summary>
        public static bool DeleteUser(string accountId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();
                    string query = "DELETE FROM Accounts WHERE AccountID = @accountId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@accountId", accountId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
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
    }
}