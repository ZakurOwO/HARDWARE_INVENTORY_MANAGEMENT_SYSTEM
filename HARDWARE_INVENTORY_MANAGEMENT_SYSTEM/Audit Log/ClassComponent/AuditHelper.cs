using System;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log
{
    public static class AuditHelper
    {
        private static AuditLogManager auditLogManager = new AuditLogManager();

        /// <summary>
        /// Write a simple audit entry using the current session context.
        /// </summary>
        public static void Log(string module, string activity, AuditActivityType activityType,
            string tableAffected = null, string recordId = null)
        {
            try
            {
                if (!TryResolveUserContext(null, null, recordId, out int userId, out string username, out string resolvedRecordId))
                {
                    Console.WriteLine("WARNING - Unable to resolve user context for audit entry");
                    return;
                }

                WriteAuditEntry(userId, username, module, activity, activityType, tableAffected, resolvedRecordId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in AuditHelper.Log: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Specialized helper for authentication events so login/logout always persist.
        /// </summary>
        public static void LogAuthenticationEvent(
            string activity,
            AuditActivityType activityType,
            int? userId = null,
            string username = null,
            string accountId = null,
            string module = null)
        {
            try
            {
                if (!TryResolveUserContext(userId, username, accountId, out int resolvedUserId, out string resolvedUsername, out string resolvedRecordId))
                {
                    Console.WriteLine("WARNING - Unable to resolve authentication user context, skipping audit");
                    return;
                }

                string resolvedModule = ResolveAuthModule(activityType, module);

                WriteAuditEntry(
                    resolvedUserId,
                    resolvedUsername,
                    resolvedModule,
                    activity,
                    activityType,
                    tableAffected: "Accounts",
                    recordId: resolvedRecordId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in AuditHelper.LogAuthenticationEvent: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Backwards-compatible overload that accepts a module name first.
        /// </summary>
        public static void LogAuthenticationEvent(
            string module,
            string activity,
            AuditActivityType activityType,
            int? userId = null,
            string username = null,
            string accountId = null)
        {
            LogAuthenticationEvent(activity, activityType, userId, username, accountId, module);
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
                if (!TryResolveUserContext(null, null, recordId, out int userId, out string username, out string resolvedRecordId))
                {
                    Console.WriteLine("WARNING - Unable to resolve user context for detailed audit entry");
                    return;
                }

                WriteAuditEntry(
                    userId,
                    username,
                    module,
                    activity,
                    activityType,
                    tableAffected,
                    resolvedRecordId,
                    oldValues,
                    newValues);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in AuditHelper.LogWithDetails: {ex.Message}");
            }
        }

        private static void WriteAuditEntry(
            int userId,
            string username,
            string module,
            string activity,
            AuditActivityType activityType,
            string tableAffected,
            string recordId,
            string oldValues = null,
            string newValues = null)
        {
            Console.WriteLine($"DEBUG AuditHelper.WriteAuditEntry - UserId: {userId}, Username: {username}, Module: {module}, Activity: {activity}");

            bool success = auditLogManager.LogActivity(
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

            Console.WriteLine($"DEBUG AuditHelper.WriteAuditEntry - Audit saved: {success}");
        }

        private static string ResolveAuthModule(AuditActivityType activityType, string providedModule)
        {
            if (!string.IsNullOrWhiteSpace(providedModule))
            {
                return providedModule;
            }

            return activityType switch
            {
                AuditActivityType.LOGIN => AuditModule.LOGIN,
                AuditActivityType.LOGOUT => AuditModule.SIGN_OUT,
                _ => AuditModule.AUTHENTICATION
            };
        }

        private static bool TryResolveUserContext(
            int? userIdOverride,
            string usernameOverride,
            string recordIdOverride,
            out int userId,
            out string username,
            out string recordId)
        {
            userId = userIdOverride ?? UserSession.UserId;
            username = usernameOverride ?? UserSession.Username;
            recordId = recordIdOverride ?? UserSession.AccountID;

            if (userId <= 0 || string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("WARNING - Missing user ID or username for audit log");
                return false;
            }

            return true;
        }
    }
}
