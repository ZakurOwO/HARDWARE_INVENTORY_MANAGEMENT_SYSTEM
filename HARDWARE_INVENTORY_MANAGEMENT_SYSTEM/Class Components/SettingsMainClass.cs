using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public static class SettingsMainClass
    {
        public static void ShowSettingsPanel(Panel parent)
        {
            // Check if the settings panel already exists
            var existing = parent.Controls.OfType<Settings_Signout>().FirstOrDefault();
            if (existing != null)
            {
                parent.Controls.Remove(existing);

                foreach (Control c in parent.Controls)
                    c.BringToFront();

                return;
            }

            // Create and position the new settings panel
            Settings_Signout settings = new Settings_Signout();
            settings.Location = new Point(parent.Width - settings.Width - 40, 60);

            parent.Controls.Add(settings);

            // Send other controls to the back
            foreach (Control c in parent.Controls)
                c.SendToBack();

            settings.BringToFront();

        }

    }
}
