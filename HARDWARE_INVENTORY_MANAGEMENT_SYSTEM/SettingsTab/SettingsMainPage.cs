using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module;
using System;
using System.Linq;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles
{
    public partial class SettingsMainPage : UserControl
    {
        public SettingsMainPage()
        {
            InitializeComponent();
            Console.WriteLine("SettingsMainPage constructor called");
        }

        private void SettingsMainPage_Load(object sender, EventArgs e)
        {
            Console.WriteLine("SettingsMainPage_Load called");
            ShowAccounts(); // Show default page
        }

        public void ShowAccounts()
        {
            Console.WriteLine("ShowAccounts method executing");
            pnlDisplaySettings.Controls.Clear();
            AccountsMainPage accountsMainPage = new AccountsMainPage();
            accountsMainPage.Dock = DockStyle.Fill;
            pnlDisplaySettings.Controls.Add(accountsMainPage);
            Console.WriteLine("Accounts page shown");
        }

        public void ShowHistory()
        {
            Console.WriteLine("ShowHistory method executing");
            pnlDisplaySettings.Controls.Clear();
            HistoryMainPage historyMainPage = new HistoryMainPage();
            historyMainPage.Dock = DockStyle.Fill;
            pnlDisplaySettings.Controls.Add(historyMainPage);
            Console.WriteLine("History page shown");
        }

        public void ShowAuditLog()
        {
            Console.WriteLine("ShowAuditLog method executing");
            pnlDisplaySettings.Controls.Clear();
            AuditLogMainPage auditLogMainPage = new AuditLogMainPage();
            auditLogMainPage.Dock = DockStyle.Fill;
            pnlDisplaySettings.Controls.Add(auditLogMainPage);
            Console.WriteLine("AuditLog page shown");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Back button clicked");

            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm == null) return;

            // Remove Settings main page (this)
            mainForm.MainContentPanelAccess.Controls.Remove(this);

            // Remove the settings side panel
            var settingsPanel = mainForm.Controls.OfType<SettingsSidePanel>().FirstOrDefault();
            if (settingsPanel != null)
            {
                mainForm.Controls.Remove(settingsPanel);
                settingsPanel.Dispose();
                Console.WriteLine("SettingsSidePanel removed");
            }

            // Show ALL controls that were hidden
            foreach (Control control in mainForm.Controls)
            {
                control.Visible = true;
            }

            // Load dashboard
            var dashboard = new Dashboard.DashboardMainPage();
            dashboard.Dock = DockStyle.Fill;
            mainForm.MainContentPanelAccess.Controls.Clear();
            mainForm.MainContentPanelAccess.Controls.Add(dashboard);
            Console.WriteLine("Dashboard restored");
        }
    }
}