using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log.ClassComponent
{
    public static class AuditHelper
    {
        // Only one ExportToCsv method
        public static string ExportToCsv(List<AuditLogEntry> auditLogs, string fileName)
        {
            try
            {
                var csv = new StringBuilder();

                // Add header
                csv.AppendLine("Username,Activity,Module,Activity Type,Timestamp,Table Affected,Record ID,IP Address");

                // Add data
                foreach (var log in auditLogs)
                {
                    var escapedUsername = EscapeCsvField(log.Username ?? "");
                    var escapedActivity = EscapeCsvField(log.Activity ?? "");
                    var escapedModule = EscapeCsvField(log.Module ?? "");
                    var escapedActivityType = EscapeCsvField(log.ActivityType ?? "");
                    var escapedTable = EscapeCsvField(log.TableAffected ?? "");
                    var escapedRecordId = EscapeCsvField(log.RecordId ?? "");
                    var escapedIp = EscapeCsvField(log.IpAddress ?? "");

                    csv.AppendLine($"\"{escapedUsername}\",\"{escapedActivity}\",\"{escapedModule}\",\"{escapedActivityType}\",\"{log.Timestamp:yyyy-MM-dd HH:mm:ss}\",\"{escapedTable}\",\"{escapedRecordId}\",\"{escapedIp}\"");
                }

                var directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var filePath = Path.Combine(directory, $"{fileName}_{DateTime.Now:yyyyMMddHHmmss}.csv");

                File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8);
                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error exporting to CSV: {ex.Message}", ex);
            }
        }

        private static string EscapeCsvField(string field)
        {
            if (field.Contains("\"") || field.Contains(",") || field.Contains("\n") || field.Contains("\r"))
            {
                field = field.Replace("\"", "\"\"");
                return field;
            }
            return field;
        }

        // Simple logging method without database dependencies
        public static void LogActivity(string user, string activity, string module, string details = "", string ipAddress = "")
        {
            // Just write to debug for now
            System.Diagnostics.Debug.WriteLine($"AUDIT: {user} - {activity} - {module} - {details} - {ipAddress}");
        }

        public static string GetUserIpAddress()
        {
            try
            {
                var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}