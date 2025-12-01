using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public static class UserSession
    {
        public static int UserId { get; set; }  // ADD THIS
        public static string AccountID { get; set; }
        public static string FullName { get; set; }
        public static string Role { get; set; }
        public static string Username { get; set; }
        public static bool IsLoggedIn { get; set; }
        public static DateTime LoginTime { get; set; }  // ADD THIS

        static UserSession()
        {
            IsLoggedIn = false;
        }

        public static void ClearSession()
        {
            UserId = 0;  // ADD THIS
            AccountID = null;
            FullName = null;
            Role = null;
            Username = null;
            IsLoggedIn = false;
            LoginTime = DateTime.MinValue;  // ADD THIS
        }
    }
}