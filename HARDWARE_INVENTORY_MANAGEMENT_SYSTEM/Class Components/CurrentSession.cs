using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public static class CurrentSession
    {
        // Store the latest successful login result
        public static LoginResult LoggedInUser { get; private set; }

        public static void SetUser(LoginResult loginResult)
        {
            LoggedInUser = loginResult;
        }

        public static int GetUserIdOrDefault(int fallback = 1)
        {
            return LoggedInUser?.UserId ?? fallback;
        }

        public static string GetUsernameOrDefault(string fallback = "System")
        {
            return LoggedInUser?.Username ?? fallback;
        }

        public static string GetFullNameOrDefault(string fallback = "System")
        {
            return LoggedInUser?.FullName ?? fallback;
        }

        public static string GetRoleOrDefault(string fallback = "Unknown")
        {
            return LoggedInUser?.Role ?? fallback;
        }
    }
}
