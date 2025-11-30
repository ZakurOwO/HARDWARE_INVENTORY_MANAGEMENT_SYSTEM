using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log.ClassComponent
{
    public static class AuditModule
    {
        public const string INVENTORY = "Inventory";
        public const string USER_MANAGEMENT = "User Management";
        public const string SYSTEM = "System";
        public const string AUDIT_LOG = "Audit Log";
        public const string REPORTING = "Reporting";

        public static List<string> GetAllModules()
        {
            return new List<string>
            {
                INVENTORY,
                USER_MANAGEMENT,
                SYSTEM,
                AUDIT_LOG,
                REPORTING
            };
        }
    }
}