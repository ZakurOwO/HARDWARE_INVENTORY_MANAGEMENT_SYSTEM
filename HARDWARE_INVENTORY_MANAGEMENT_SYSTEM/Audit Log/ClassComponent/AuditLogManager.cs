using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log
{
    public class AuditLogManager
    {
        private string connectionString;

        public AuditLogManager()
        {
            this.connectionString = GetConnectionString();
        }

        private string GetConnectionString()
        {
            return HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.ConnectionString.DataSource;
        }

        public bool LogActivity(
      int userId,
      string username,
      string module,
      string activity,
      AuditActivityType activityType,
      string tableAffected = null,
      string recordId = null,
      string oldValues = null,
      string newValues = null,
      string ipAddress = null)
        {
            try
            {
                Console.WriteLine($"=== AuditLogManager.LogActivity START ===");
                Console.WriteLine($"  userId: {userId}");
                Console.WriteLine($"  username: {username}");
                Console.WriteLine($"  module: {module}");
                Console.WriteLine($"  activity: {activity}");
                Console.WriteLine($"  activityType: {activityType}");

                string query = @"
            INSERT INTO AuditLog (user_id, username, module, activity, activity_type, 
                                table_affected, record_id, old_values, new_values, ip_address)
            VALUES (@user_id, @username, @module, @activity, @activity_type, 
                   @table_affected, @record_id, @old_values, @new_values, @ip_address)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", userId);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@module", module);
                        cmd.Parameters.AddWithValue("@activity", activity);
                        cmd.Parameters.AddWithValue("@activity_type", activityType.ToString());
                        cmd.Parameters.AddWithValue("@table_affected", (object)tableAffected ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@record_id", (object)recordId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@old_values", (object)oldValues ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@new_values", (object)newValues ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ip_address", (object)ipAddress ?? DBNull.Value);

                        Console.WriteLine("Opening database connection...");
                        conn.Open();
                        Console.WriteLine("Connection opened successfully");

                        Console.WriteLine("Executing INSERT query...");
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Rows affected: {rowsAffected}");

                        Console.WriteLine("=== AuditLogManager.LogActivity END (SUCCESS) ===");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== AuditLogManager.LogActivity END (ERROR) ===");
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }
        public List<AuditLogEntry> GetAuditLogs(
            string searchTerm = null,
            string module = null,
            string activityType = null,
            int? userId = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int pageNumber = 1,
            int pageSize = 100)
        {
            List<AuditLogEntry> auditLogs = new List<AuditLogEntry>();

            try
            {
                StringBuilder query = new StringBuilder(@"
                    SELECT 
                        AuditID,
                        user_id,
                        username,
                        module,
                        activity,
                        activity_type,
                        table_affected,
                        record_id,
                        old_values,
                        new_values,
                        ip_address,
                        timestamp
                    FROM AuditLog
                    WHERE 1=1");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            query.Append(" AND (activity LIKE @searchTerm OR username LIKE @searchTerm OR module LIKE @searchTerm)");
                            cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");
                        }

                        if (!string.IsNullOrWhiteSpace(module))
                        {
                            query.Append(" AND module = @module");
                            cmd.Parameters.AddWithValue("@module", module);
                        }

                        if (!string.IsNullOrWhiteSpace(activityType))
                        {
                            query.Append(" AND activity_type = @activityType");
                            cmd.Parameters.AddWithValue("@activityType", activityType);
                        }

                        if (userId.HasValue)
                        {
                            query.Append(" AND user_id = @userId");
                            cmd.Parameters.AddWithValue("@userId", userId.Value);
                        }

                        if (startDate.HasValue)
                        {
                            query.Append(" AND timestamp >= @startDate");
                            cmd.Parameters.AddWithValue("@startDate", startDate.Value);
                        }

                        if (endDate.HasValue)
                        {
                            query.Append(" AND timestamp <= @endDate");
                            cmd.Parameters.AddWithValue("@endDate", endDate.Value);
                        }

                        query.Append(" ORDER BY timestamp DESC");
                        query.Append(" OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY");
                        cmd.Parameters.AddWithValue("@offset", (pageNumber - 1) * pageSize);
                        cmd.Parameters.AddWithValue("@pageSize", pageSize);

                        cmd.CommandText = query.ToString();
                        cmd.Connection = conn;

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                auditLogs.Add(new AuditLogEntry
                                {
                                    AuditID = reader["AuditID"].ToString(),
                                    UserId = Convert.ToInt32(reader["user_id"]),
                                    Username = reader["username"].ToString(),
                                    Activity = reader["activity"].ToString(),
                                    Module = reader["module"].ToString(),
                                    ActivityType = reader["activity_type"].ToString(),
                                    Timestamp = Convert.ToDateTime(reader["timestamp"]),
                                    TableAffected = reader["table_affected"] != DBNull.Value ? reader["table_affected"].ToString() : null,
                                    RecordID = reader["record_id"] != DBNull.Value ? reader["record_id"].ToString() : null,
                                    OldValues = reader["old_values"] != DBNull.Value ? reader["old_values"].ToString() : null,
                                    NewValues = reader["new_values"] != DBNull.Value ? reader["new_values"].ToString() : null,
                                    IPAddress = reader["ip_address"] != DBNull.Value ? reader["ip_address"].ToString() : null
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving audit logs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return auditLogs;
        }

        public bool ExportToCSV(string filePath, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                StringBuilder query = new StringBuilder(@"
                    SELECT 
                        AuditID,
                        username,
                        module,
                        activity,
                        activity_type,
                        timestamp,
                        table_affected,
                        record_id,
                        ip_address
                    FROM AuditLog
                    WHERE 1=1");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (startDate.HasValue)
                        {
                            query.Append(" AND timestamp >= @startDate");
                            cmd.Parameters.AddWithValue("@startDate", startDate.Value);
                        }

                        if (endDate.HasValue)
                        {
                            query.Append(" AND timestamp <= @endDate");
                            cmd.Parameters.AddWithValue("@endDate", endDate.Value);
                        }

                        query.Append(" ORDER BY timestamp DESC");
                        cmd.CommandText = query.ToString();
                        cmd.Connection = conn;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            using (StreamWriter writer = new StreamWriter(filePath))
                            {
                                writer.WriteLine("Audit ID,Username,Module,Activity,Activity Type,Timestamp,Table Affected,Record ID,IP Address");

                                while (reader.Read())
                                {
                                    writer.WriteLine(string.Format("{0},{1},{2},\"{3}\",{4},{5},{6},{7},{8}",
                                        reader["AuditID"],
                                        reader["username"],
                                        reader["module"],
                                        reader["activity"].ToString().Replace("\"", "\"\""),
                                        reader["activity_type"],
                                        reader["timestamp"],
                                        reader["table_affected"] != DBNull.Value ? reader["table_affected"] : "",
                                        reader["record_id"] != DBNull.Value ? reader["record_id"] : "",
                                        reader["ip_address"] != DBNull.Value ? reader["ip_address"] : ""
                                    ));
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting audit log: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}