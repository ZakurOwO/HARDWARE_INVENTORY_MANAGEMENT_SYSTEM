using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log.ClassComponent
{
    public class AuditLogEntry
    {
        public int AuditId { get; set; }
        public string AuditID { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Module { get; set; }
        public string Activity { get; set; }
        public string ActivityType { get; set; }
        public string TableAffected { get; set; }
        public string RecordId { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string IpAddress { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime CreatedAt { get; set; }

        // Default constructor
        public AuditLogEntry()
        {
            Timestamp = DateTime.Now;
            CreatedAt = DateTime.Now;
        }

        // Constructor for sample data
        public AuditLogEntry(string username, string activity, string module, string activityType, string ipAddress)
        {
            Username = username;
            Activity = activity;
            Module = module;
            ActivityType = activityType;
            IpAddress = ipAddress;
            Timestamp = DateTime.Now;
            CreatedAt = DateTime.Now;
        }
    }
}