namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public static class UserSession
    {
        public static string AccountID { get; set; }
        public static string FullName { get; set; }
        public static string Role { get; set; }
        public static string Username { get; set; }
        public static bool IsLoggedIn { get; set; }

        static UserSession()
        {
            IsLoggedIn = false;
        }

        public static void ClearSession()
        {
            AccountID = null;
            FullName = null;
            Role = null;
            Username = null;
            IsLoggedIn = false;
        }
    }
}