using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles
{
    public partial class Settings_Signout : UserControl
    {
        public Settings_Signout()
        {
            InitializeComponent();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Settings button clicked!");

            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                Console.WriteLine("Main form found");

                // Clear the main content panel and add SettingsMainPage
                mainForm.MainContentPanelAccess.Controls.Clear();
                SettingsMainPage settingsPage = new SettingsMainPage();
                Console.WriteLine("SettingsMainPage created");

                settingsPage.Dock = DockStyle.Fill;
                mainForm.MainContentPanelAccess.Controls.Add(settingsPage);
                mainForm.MainContentPanelAccess.Visible = true;

                // Hide the original side panel
                var originalSidePanel = mainForm.Controls.OfType<SidePanel>().FirstOrDefault();
                if (originalSidePanel != null)
                {
                    originalSidePanel.Visible = false;
                    Console.WriteLine("Original side panel hidden");
                }

                // Create SettingsSidePanel in the same position as original side panel
                var settingsSide = new SettingsSidePanel();
                settingsSide.Name = "settingsSidePanel1";

                if (originalSidePanel != null)
                {
                    settingsSide.Location = originalSidePanel.Location;
                    settingsSide.Size = originalSidePanel.Size;
                }
                else
                {
                    settingsSide.Location = new Point(12, 12);
                    settingsSide.Size = new Size(197, 698);
                }

                Console.WriteLine("SettingsSidePanel created");

                // Wire up events
                settingsSide.AccountsClicked += (s, args) =>
                {
                    Console.WriteLine("AccountsClicked event fired!");
                    settingsPage.ShowAccounts();
                };

                settingsSide.HistoryClicked += (s, args) =>
                {
                    Console.WriteLine("HistoryClicked event fired!");
                    settingsPage.ShowHistory();
                };

                settingsSide.AuditLogClicked += (s, args) =>
                {
                    Console.WriteLine("AuditLogClicked event fired!");
                    settingsPage.ShowAuditLog();
                };

                Console.WriteLine("Events wired up");

                mainForm.Controls.Add(settingsSide);
                settingsSide.Visible = true;
                settingsSide.BringToFront();

                Console.WriteLine("SettingsSidePanel added to form");
            }
            else
            {
                Console.WriteLine("Main form NOT found!");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserSession.IsLoggedIn)
                {
                    AuditHelper.LogAuthenticationEvent(
                        $"User {UserSession.Username} signed out from the system",
                        AuditActivityType.LOGOUT,
                        userId: UserSession.UserId,
                        username: UserSession.Username,
                        accountId: UserSession.AccountID
                    );
                }
            }
            catch (Exception auditEx)
            {
                Console.WriteLine($"Audit log error on logout: {auditEx.Message}");
            }

            UserSession.ClearSession();

            var mainForm = this.FindForm() as MainDashBoard;
            var loginForm = Application.OpenForms.OfType<LoginForm>().FirstOrDefault() ?? new LoginForm();

            loginForm.Show();
            loginForm.BringToFront();

            if (mainForm != null)
            {
                mainForm.Close();
            }
        }

        // Backwards-compatibility handlers if the designer or existing code
        // wires up alternative event names. They simply forward to the
        // consolidated implementations above so builds don't fail when
        // metadata still references the old signatures.
        private void btnSignOut_Click(object sender, EventArgs e)
        {
            guna2Button1_Click(sender, e);
        }

        private void Settings_Signout_Load(object sender, EventArgs e)
        {
            if (!UserSession.IsAdmin())
            {
                btnSettings.Enabled = false;
                btnSettings.Visible = false;

                guna2Button1.Location = btnSettings.Location;
            }
        }
    }
}
