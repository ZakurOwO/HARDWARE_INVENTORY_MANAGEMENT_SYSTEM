using System;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.Class_Components
{
    public class ProfileMenuPainter
    {
        private string _displayName;
        private string _role;

        public ProfileMenuPainter(string displayName, string role)
        {
            _displayName = displayName;
            _role = role;
        }

        // Static method to create painter with formatted name from UserSession
        public static ProfileMenuPainter CreateFromUserSession()
        {
            string userFullName = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserSession.FullName;
            string formattedName = FormatName(userFullName);
            string userRole = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserSession.Role ?? "User";

            return new ProfileMenuPainter(formattedName, userRole);
        }

        // Static method to format name
        private static string FormatName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return "User";

            string[] nameParts = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (nameParts.Length == 0)
                return "User";

            if (nameParts.Length == 1)
                return nameParts[0];

            string firstName = nameParts[0];
            string lastInitial = nameParts[nameParts.Length - 1][0] + ".";

            return $"{firstName} {lastInitial}";
        }

        public void Draw(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int textX = 47;
            int textY = 5;
            int spacing = 14;

            // Draw Name
            using (Font nameFont = new Font("Lexend Semibold", 8f))
            using (SolidBrush nameBrush = new SolidBrush(Color.Black))
            {
                g.DrawString(_displayName, nameFont, nameBrush, textX, textY);
            }

            // Draw Role
            using (Font roleFont = new Font("Lexend Light", 8f))
            using (SolidBrush roleBrush = new SolidBrush(Color.Gray))
            {
                g.DrawString(_role, roleFont, roleBrush, textX, textY + spacing);
            }
        }
    }
}