using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log
{
    public static class AuditHelper
    {
        private static AuditLogManager auditLogManager = new AuditLogManager();

        public static void Log(string module, string activity, AuditActivityType activityType,
            string tableAffected = null, string recordId = null)
        {
            try
            {
                // Use the correct UserSession from Class_Components namespace
                var userSession = typeof(HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.UserSession);
                bool isLoggedIn = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.UserSession.IsLoggedIn;

                // DEBUG logging
                Console.WriteLine($"DEBUG AuditHelper.Log - IsLoggedIn: {isLoggedIn}");

                if (isLoggedIn)
                {
                    int userId = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.UserSession.UserId;
                    string username = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.UserSession.Username ?? "Unknown";

                    Console.WriteLine($"DEBUG AuditHelper.Log - UserId: {userId}, Username: {username}, Activity: {activity}");

                    bool success = auditLogManager.LogActivity(
                        userId,
                        username,
                        module,
                        activity,
                        activityType,
                        tableAffected,
                        recordId
                    );

                    Console.WriteLine($"DEBUG AuditHelper.Log - Audit saved: {success}");
                }
                else
                {
                    Console.WriteLine("WARNING - User not logged in, skipping audit log");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in AuditHelper.Log: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        public static void LogWithDetails(
            string module,
            string activity,
            AuditActivityType activityType,
            string tableAffected = null,
            string recordId = null,
            string oldValues = null,
            string newValues = null)
        {
            try
            {
                if (HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.UserSession.IsLoggedIn)
                {
                    int userId = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.UserSession.UserId;
                    string username = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.UserSession.Username ?? "Unknown";

                    auditLogManager.LogActivity(
                        userId,
                        username,
                        module,
                        activity,
                        activityType,
                        tableAffected,
                        recordId,
                        oldValues,
                        newValues
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in AuditHelper.LogWithDetails: {ex.Message}");
            }
        }
    }
}