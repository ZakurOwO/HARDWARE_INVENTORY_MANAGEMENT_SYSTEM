using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log.ClassComponent
{
    public static class AuditActivityType
    {
        public const string LOGIN = "Login";
        public const string LOGOUT = "Logout";
        public const string CREATE = "Create";
        public const string UPDATE = "Update";
        public const string DELETE = "Delete";
        public const string VIEW = "View";
        public const string EXPORT = "Export";
        public const string IMPORT = "Import";
        public const string SEARCH = "Search";

        public static List<string> GetAllActivityTypes()
        {
            return new List<string>
            {
                LOGIN,
                LOGOUT,
                CREATE,
                UPDATE,
                DELETE,
                VIEW,
                EXPORT,
                IMPORT,
                SEARCH
            };
        }
    }
}