using System;
using System.Data.SqlClient;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public static class AuthenticationService
    {
        /// <summary>
        /// Authenticate user with username and password
        /// </summary>
        public static LoginResult AuthenticateUser(string username, string password)
        {
            try
            {
                // DEBUG: Show what password is being sent
                Console.WriteLine($"DEBUG - Username: {username}");
                Console.WriteLine($"DEBUG - Password entered: {password}");

                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();

                    // Query to check if user exists and get their details
                    // Changed password_hash to password for plain text
                    string query = @"
                        SELECT 
                            a.AccountID,
                            a.Fullname,
                            a.username,
                            a.password,  -- Changed from password_hash to password
                            a.Account_status,
                            r.role_name as Role
                        FROM Accounts a
                        INNER JOIN Roles r ON a.RoleID = r.RoleID
                        WHERE a.username = @username";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedPassword = reader["password"].ToString(); // Get plain text password
                                string accountStatus = reader["Account_status"].ToString();

                                // DEBUG: Show what's stored in database
                                Console.WriteLine($"DEBUG - Stored password: {storedPassword}");
                                Console.WriteLine($"DEBUG - Passwords match: {storedPassword == password}");

                                // Check if account is inactive
                                if (accountStatus.ToLower() != "active")
                                {
                                    return new LoginResult
                                    {
                                        IsAuthenticated = false,
                                        ErrorMessage = "Your account is inactive. Please contact the administrator."
                                    };
                                }

                                // Compare plain text passwords directly (no hashing)
                                if (storedPassword == password)
                                {
                                    return new LoginResult
                                    {
                                        IsAuthenticated = true,
                                        AccountID = reader["AccountID"].ToString(),
                                        FullName = reader["Fullname"].ToString(),
                                        Username = reader["username"].ToString(),
                                        Role = reader["Role"].ToString()
                                    };
                                }
                                else
                                {
                                    return new LoginResult
                                    {
                                        IsAuthenticated = false,
                                        ErrorMessage = "Invalid password. Please try again."
                                    };
                                }
                            }
                            else
                            {
                                return new LoginResult
                                {
                                    IsAuthenticated = false,
                                    ErrorMessage = "Username not found. Please check your username."
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"DEBUG - SQL Exception: {sqlEx.Message}");
                return new LoginResult
                {
                    IsAuthenticated = false,
                    ErrorMessage = $"Database error: {sqlEx.Message}"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG - General Exception: {ex.Message}");
                return new LoginResult
                {
                    IsAuthenticated = false,
                    ErrorMessage = $"Authentication error: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Change user password
        /// </summary>
        public static bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                // First verify the old password
                var loginResult = AuthenticateUser(username, oldPassword);

                if (!loginResult.IsAuthenticated)
                {
                    return false;
                }

                // Update to new password (store as plain text)
                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();

                    string query = "UPDATE Accounts SET password = @newPassword WHERE username = @username";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@newPassword", newPassword); // Store plain text

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reset user password (admin function)
        /// </summary>
        public static bool ResetPassword(string accountId, string newPassword)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();

                    string query = "UPDATE Accounts SET password = @newPassword WHERE AccountID = @accountId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@accountId", accountId);
                        cmd.Parameters.AddWithValue("@newPassword", newPassword); // Store plain text

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Result object for login attempts
    /// </summary>
    public class LoginResult
    {
        public bool IsAuthenticated { get; set; }
        public string AccountID { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string ErrorMessage { get; set; }
    }
}