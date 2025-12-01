using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    /// <summary>
    /// Static class to store user session information
    /// </summary>
    public static class UserSession
    {
        public static int UserId { get; set; }  // ADD THIS - Store the actual AccountInternalID
        public static string AccountID { get; set; }
        public static string FullName { get; set; }
        public static string Username { get; set; }
        public static string Role { get; set; }
        public static bool IsLoggedIn { get; set; }
        public static DateTime LoginTime { get; set; }

        static UserSession()
        {
            IsLoggedIn = false;
        }

        /// <summary>
        /// Initialize user session with login data
        /// </summary>
        public static void InitializeSession(int userId, string accountId, string fullName, string username, string role)
        {
            UserId = userId;  // ADD THIS
            AccountID = accountId;
            FullName = fullName;
            Username = username;
            Role = role;
            IsLoggedIn = true;
            LoginTime = DateTime.Now;
        }

        /// <summary>
        /// Clear all session data
        /// </summary>
        public static void ClearSession()
        {
            UserId = 0;  // ADD THIS
            AccountID = null;
            FullName = null;
            Username = null;
            Role = null;
            IsLoggedIn = false;
            LoginTime = DateTime.MinValue;
        }

        /// <summary>
        /// Check if user has specific role
        /// </summary>
        public static bool HasRole(string roleName)
        {
            if (!IsLoggedIn || string.IsNullOrEmpty(Role))
                return false;

            return Role.Equals(roleName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Check if user is admin
        /// </summary>
        public static bool IsAdmin()
        {
            return HasRole("Admin") || HasRole("Administrator");
        }

        /// <summary>
        /// Check if user is manager
        /// </summary>
        public static bool IsManager()
        {
            return HasRole("Manager");
        }

        /// <summary>
        /// Get session duration
        /// </summary>
        public static TimeSpan GetSessionDuration()
        {
            if (!IsLoggedIn)
                return TimeSpan.Zero;

            return DateTime.Now - LoginTime;
        }

        /// <summary>
        /// Get user display name (for UI)
        /// </summary>
        public static string GetDisplayName()
        {
            if (string.IsNullOrEmpty(FullName))
                return "Guest";

            return FullName;
        }

        /// <summary>
        /// Get greeting message based on time of day
        /// </summary>
        public static string GetGreeting()
        {
            int hour = DateTime.Now.Hour;
            string timeGreeting;

            if (hour < 12)
                timeGreeting = "Good Morning";
            else if (hour < 18)
                timeGreeting = "Good Afternoon";
            else
                timeGreeting = "Good Evening";

            return $"{timeGreeting}, {GetDisplayName()}!";
        }
    }
}