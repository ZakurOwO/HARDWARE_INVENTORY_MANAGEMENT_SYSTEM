using System;
using System.Data.SqlClient;
using System.Text;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public class SupplierRecord
    {
        public int SupplierInternalId { get; set; }
        public string SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }

    public static class SupplierAuditLogger
    {
        private const string ModuleName = "Suppliers";
        private const string TableName = "Suppliers";
        private const string DefaultIp = "127.0.0.1";

        public static void LogSupplierAudit(
            SqlConnection connection,
            SqlTransaction transaction,
            string activity,
            string activityType,
            string recordId,
            string oldValues,
            string newValues)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            var (userId, username) = ResolveUserContext(connection, transaction);

            using (SqlCommand cmd = new SqlCommand(@"INSERT INTO AuditLog
                (user_id, username, module, activity, activity_type,
                 table_affected, record_id, old_values, new_values, ip_address)
                VALUES
                (@user_id, @username, @module, @activity, @activity_type,
                 @table_affected, @record_id, @old_values, @new_values, @ip_address);", connection, transaction))
            {
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@module", ModuleName);
                cmd.Parameters.AddWithValue("@activity", activity);
                cmd.Parameters.AddWithValue("@activity_type", activityType);
                cmd.Parameters.AddWithValue("@table_affected", TableName);
                cmd.Parameters.AddWithValue("@record_id", (object)recordId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@old_values", (object)oldValues ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@new_values", (object)newValues ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ip_address", DefaultIp);

                cmd.ExecuteNonQuery();
            }
        }

        public static string BuildSupplierState(SupplierRecord supplier)
        {
            if (supplier == null)
                return null;

            StringBuilder builder = new StringBuilder();
            builder.Append($"supplier_name={supplier.SupplierName};");
            builder.Append($"contact_person={supplier.ContactPerson};");
            builder.Append($"contact_number={supplier.ContactNumber};");
            builder.Append($"address={supplier.Address};");
            builder.Append($"email={supplier.Email}");
            return builder.ToString();
        }

        private static (int userId, string username) ResolveUserContext(SqlConnection connection, SqlTransaction transaction)
        {
            int candidateUserId = CurrentSession.CurrentUserId;
            string candidateUsername = CurrentSession.CurrentUsername;

            if (!string.IsNullOrWhiteSpace(candidateUsername) && ValidateUserByUsername(connection, transaction, candidateUsername, out int resolvedUserId))
            {
                return (resolvedUserId, candidateUsername);
            }

            if (candidateUserId > 0 && ValidateUserById(connection, transaction, candidateUserId))
            {
                return (candidateUserId, string.IsNullOrWhiteSpace(candidateUsername) ? CurrentSession.GetUsernameOrDefault() : candidateUsername);
            }

            int fallbackUserId = CurrentSession.GetUserIdOrDefault();
            if (ValidateUserById(connection, transaction, fallbackUserId))
            {
                return (fallbackUserId, string.IsNullOrWhiteSpace(candidateUsername) ? CurrentSession.GetUsernameOrDefault() : candidateUsername);
            }

            // Final fallback: attempt to resolve any existing user to satisfy FK
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 user_id, username FROM Users ORDER BY user_id", connection, transaction))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return (Convert.ToInt32(reader["user_id"]), reader["username"].ToString());
                }
            }

            // If no users exist, default values (may fail FK but ensures predictable behavior)
            return (fallbackUserId, CurrentSession.GetUsernameOrDefault());
        }

        private static bool ValidateUserById(SqlConnection connection, SqlTransaction transaction, int userId)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE user_id = @user_id", connection, transaction))
            {
                cmd.Parameters.AddWithValue("@user_id", userId);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private static bool ValidateUserByUsername(SqlConnection connection, SqlTransaction transaction, string username, out int userId)
        {
            userId = 0;
            using (SqlCommand cmd = new SqlCommand("SELECT user_id FROM Users WHERE username = @username", connection, transaction))
            {
                cmd.Parameters.AddWithValue("@username", username);
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    userId = Convert.ToInt32(result);
                    return true;
                }
            }
            return false;
        }
    }
}
