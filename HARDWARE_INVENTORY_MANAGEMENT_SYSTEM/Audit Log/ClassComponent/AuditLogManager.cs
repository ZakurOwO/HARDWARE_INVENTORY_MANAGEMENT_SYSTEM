using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log.ClassComponent
{
    public class AuditLogManager
    {
        private string connectionString;

        public AuditLogManager()
        {
            // Use direct connection string - remove ConfigurationManager dependency
            connectionString = "Data Source=.;Initial Catalog=TopazHardwareDb;Integrated Security=True;";
        }

        public List<AuditLogEntry> GetAuditLogs()
        {
            var auditLogs = new List<AuditLogEntry>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            audit_id,
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
                            timestamp,
                            created_at
                        FROM AuditLog 
                        ORDER BY timestamp DESC";

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var auditLog = new AuditLogEntry
                            {
                                AuditId = Convert.ToInt32(reader["audit_id"]),
                                AuditID = reader["AuditID"].ToString(),
                                UserId = Convert.ToInt32(reader["user_id"]),
                                Username = reader["username"].ToString(),
                                Module = reader["module"].ToString(),
                                Activity = reader["activity"].ToString(),
                                ActivityType = reader["activity_type"].ToString(),
                                TableAffected = reader["table_affected"] == DBNull.Value ? null : reader["table_affected"].ToString(),
                                RecordId = reader["record_id"] == DBNull.Value ? null : reader["record_id"].ToString(),
                                OldValues = reader["old_values"] == DBNull.Value ? null : reader["old_values"].ToString(),
                                NewValues = reader["new_values"] == DBNull.Value ? null : reader["new_values"].ToString(),
                                IpAddress = reader["ip_address"] == DBNull.Value ? null : reader["ip_address"].ToString(),
                                Timestamp = Convert.ToDateTime(reader["timestamp"]),
                                CreatedAt = Convert.ToDateTime(reader["created_at"])
                            };

                            auditLogs.Add(auditLog);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading audit logs from database: {ex.Message}");
                // Return sample data for testing if database is not available
                auditLogs = GetSampleAuditLogs();
            }

            return auditLogs;
        }

        private List<AuditLogEntry> GetSampleAuditLogs()
        {
            return new List<AuditLogEntry>
            {
                new AuditLogEntry("admin", "User logged in", "Authentication", "LOGIN", "192.168.1.100"),
                new AuditLogEntry("admin", "Viewed audit logs", "Audit Log", "VIEW", "192.168.1.100"),
                new AuditLogEntry("john_doe", "Created new product", "Inventory", "CREATE", "192.168.1.101"),
                new AuditLogEntry("jane_smith", "Updated user permissions", "User Management", "UPDATE", "192.168.1.102")
            };
        }

        public List<AuditLogEntry> SearchAuditLogs(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAuditLogs();

            var auditLogs = new List<AuditLogEntry>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            audit_id,
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
                            timestamp,
                            created_at
                        FROM AuditLog 
                        WHERE username LIKE @searchTerm 
                           OR module LIKE @searchTerm 
                           OR activity LIKE @searchTerm 
                           OR activity_type LIKE @searchTerm
                           OR table_affected LIKE @searchTerm
                        ORDER BY timestamp DESC";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var auditLog = new AuditLogEntry
                                {
                                    AuditId = Convert.ToInt32(reader["audit_id"]),
                                    AuditID = reader["AuditID"].ToString(),
                                    UserId = Convert.ToInt32(reader["user_id"]),
                                    Username = reader["username"].ToString(),
                                    Module = reader["module"].ToString(),
                                    Activity = reader["activity"].ToString(),
                                    ActivityType = reader["activity_type"].ToString(),
                                    TableAffected = reader["table_affected"] == DBNull.Value ? null : reader["table_affected"].ToString(),
                                    RecordId = reader["record_id"] == DBNull.Value ? null : reader["record_id"].ToString(),
                                    OldValues = reader["old_values"] == DBNull.Value ? null : reader["old_values"].ToString(),
                                    NewValues = reader["new_values"] == DBNull.Value ? null : reader["new_values"].ToString(),
                                    IpAddress = reader["ip_address"] == DBNull.Value ? null : reader["ip_address"].ToString(),
                                    Timestamp = Convert.ToDateTime(reader["timestamp"]),
                                    CreatedAt = Convert.ToDateTime(reader["created_at"])
                                };

                                auditLogs.Add(auditLog);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error searching audit logs: {ex.Message}");
                auditLogs = GetSampleAuditLogs().Where(log =>
                    log.Username.Contains(searchTerm) ||
                    log.Activity.Contains(searchTerm) ||
                    log.Module.Contains(searchTerm)).ToList();
            }

            return auditLogs;
        }

        public void LogActivity(int userId, string username, string module, string activity,
                              string activityType, string tableAffected = null, string recordId = null,
                              string oldValues = null, string newValues = null, string ipAddress = null)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO AuditLog 
                        (user_id, username, module, activity, activity_type, table_affected, record_id, old_values, new_values, ip_address)
                        VALUES 
                        (@userId, @username, @module, @activity, @activityType, @tableAffected, @recordId, @oldValues, @newValues, @ipAddress)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        // Use Convert to ensure proper parameter types
                        command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                        command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                        command.Parameters.Add("@module", SqlDbType.VarChar).Value = module;
                        command.Parameters.Add("@activity", SqlDbType.VarChar).Value = activity;
                        command.Parameters.Add("@activityType", SqlDbType.VarChar).Value = activityType;
                        command.Parameters.Add("@tableAffected", SqlDbType.VarChar).Value = (object)tableAffected ?? DBNull.Value;
                        command.Parameters.Add("@recordId", SqlDbType.VarChar).Value = (object)recordId ?? DBNull.Value;
                        command.Parameters.Add("@oldValues", SqlDbType.Text).Value = (object)oldValues ?? DBNull.Value;
                        command.Parameters.Add("@newValues", SqlDbType.Text).Value = (object)newValues ?? DBNull.Value;
                        command.Parameters.Add("@ipAddress", SqlDbType.VarChar).Value = (object)ipAddress ?? DBNull.Value;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error logging activity: {ex.Message}");
            }
        }
    }
}