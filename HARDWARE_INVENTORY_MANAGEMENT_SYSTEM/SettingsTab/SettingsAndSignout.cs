using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
    }
}