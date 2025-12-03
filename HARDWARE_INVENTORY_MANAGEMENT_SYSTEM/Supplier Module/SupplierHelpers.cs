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

            int userId = CurrentSession.CurrentUserId > 0
                ? CurrentSession.CurrentUserId
                : CurrentSession.GetUserIdOrDefault();

            string username = !string.IsNullOrWhiteSpace(CurrentSession.CurrentUsername)
                ? CurrentSession.CurrentUsername
                : CurrentSession.GetUsernameOrDefault();

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
    }
}
