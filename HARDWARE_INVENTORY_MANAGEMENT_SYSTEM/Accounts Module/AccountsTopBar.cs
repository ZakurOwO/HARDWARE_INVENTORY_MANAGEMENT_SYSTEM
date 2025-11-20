using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    public partial class AccountsTopBar : UserControl
    {
        private ProfileMenuPainter profileMenuPainter;

        public AccountsTopBar()
        {
            InitializeComponent();
            InitializeProfilePainter();
        }

        private void InitializeProfilePainter()
        {
            // Get the logged-in user's name and role from UserSession
            string userFullName = UserSession.FullName;
            string formattedName = FormatName(userFullName);
            string userRole = UserSession.Role ?? "User"; // Use actual role from database

            profileMenuPainter = new ProfileMenuPainter(formattedName, userRole);
        }

        // Method to format full name to "Firstname LastInitial."
        private string FormatName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return "User";

            string[] nameParts = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (nameParts.Length == 0)
                return "User";

            if (nameParts.Length == 1)
                return nameParts[0]; // Only first name

            // Format as "Firstname L."
            string firstName = nameParts[0];
            string lastInitial = nameParts[nameParts.Length - 1][0] + ".";

            return $"{firstName} {lastInitial}";
        }

        private void AccountsTopBar_Click(object sender, EventArgs e) { }

        private void AccountsTopBar_Load(object sender, EventArgs e) { }

        private void AccountName_Click(object sender, EventArgs e) { }

        private void btnProfileMenu_Click(object sender, EventArgs e)
        {
            var mainForm = this.FindForm() as MainDashBoard;

            if (mainForm != null)
            {
                SettingsMainClass.ShowSettingsPanel(mainForm.MainContentPanelAccess);
            }
        }

        private void btnProfileMenu_Paint(object sender, PaintEventArgs e)
        {
            profileMenuPainter?.Draw(e);
        }
    }
}