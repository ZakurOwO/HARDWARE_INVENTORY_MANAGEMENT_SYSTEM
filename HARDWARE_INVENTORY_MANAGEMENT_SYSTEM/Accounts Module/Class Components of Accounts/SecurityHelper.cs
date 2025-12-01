using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public static class SecurityHelper
    {
        public static string HashPassword(string password)
        {
            return password;
        }

        public static bool IsValidGmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return email.Trim().ToLower().EndsWith("@gmail.com");
        }
    }
}