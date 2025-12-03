using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log
{
    public class AuditLogEntry
    {
        public string AuditID { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Module { get; set; }
        public string Activity { get; set; }
        public string ActivityType { get; set; }
        public string TableAffected { get; set; }
        public string RecordID { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string IPAddress { get; set; }
        public DateTime Timestamp { get; set; }

        public string ActivityDateDisplay =>
        Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }
}