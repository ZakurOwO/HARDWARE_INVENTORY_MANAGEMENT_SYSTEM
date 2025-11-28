using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Dashboard;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles
{
    public partial class SettingsMainPage : UserControl
    {

        private SettingsSidePanel settingsSidePanel;
        public SettingsMainPage()
        {
            InitializeComponent();
            
        }

        private void SettingsMainPage_Load(object sender, EventArgs e)
        {
            
            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                settingsSidePanel = mainForm.Controls.OfType<SettingsSidePanel>().FirstOrDefault();
                if (settingsSidePanel == null)
                {
                    // If side panel not yet added, try to find by name or SidePanel type
                    settingsSidePanel = mainForm.Controls.OfType<SettingsSidePanel>().FirstOrDefault();
                }

                if (settingsSidePanel != null)
                {
                    // avoid duplicate subscription
                    settingsSidePanel.ShowAccounts -= PnlNavBar_ShowAccounts;
                    settingsSidePanel.ShowAccounts += PnlNavBar_ShowAccounts;

                    settingsSidePanel.ShowHistory -= PnlNavBar_ShowHistory;
                    settingsSidePanel.ShowHistory += PnlNavBar_ShowHistory;

                    settingsSidePanel.ShowAuditLog -= PnlNavBar_ShowAuditLog;
                    settingsSidePanel.ShowAuditLog += PnlNavBar_ShowAuditLog;
                }

               
            }

            ShowAccountsControl();
        }

        private void PnlNavBar_ShowAccounts(object sender, EventArgs e)
        {
            ShowAccountsControl();
        }

        private void PnlNavBar_ShowHistory(object sender, EventArgs e)
        {
            ShowHistoryControl();
        }

        private void PnlNavBar_ShowAuditLog(object sender, EventArgs e)
        {
            ShowAuditLogControl();
        }

        private void ShowAccountsControl()
        {
            pnlDisplaySettings.Controls.Clear();
            var accountsUC = new AccountsMainPage();
            accountsUC.Dock = DockStyle.Fill;
            pnlDisplaySettings.Controls.Add(accountsUC);
        }

        private void ShowHistoryControl()
        {
            pnlDisplaySettings.Controls.Clear();
            var historyUC = new HistoryMainPage();
            historyUC.Dock = DockStyle.Fill;
            pnlDisplaySettings.Controls.Add(historyUC);
        }

        private void ShowAuditLogControl()
        {
            pnlDisplaySettings.Controls.Clear();
            var auditLogUC = new AuditLogMainPage();
            auditLogUC.Dock = DockStyle.Fill;
            pnlDisplaySettings.Controls.Add(auditLogUC);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm == null) return;

            // 1. Remove Settings main page (this)
            mainForm.MainContentPanel.Controls.Remove(this);

            // 2. Remove the settings side panel
            var settingsPanel = mainForm.Controls.OfType<SettingsSidePanel>().FirstOrDefault();
            if (settingsPanel != null)
                mainForm.Controls.Remove(settingsPanel);

            // 3. Bring back the original side panel
            var originalPanel = new SidePanel();  // your normal left panel
            originalPanel.Location = new Point(12, 12);
            originalPanel.Size = new Size(216, 698);
            mainForm.Controls.Add(originalPanel);
            originalPanel.BringToFront();

            // 4. Load dashboard as default
            var dashboard = new DashboardMainPage();
            dashboard.Dock = DockStyle.Fill;
            mainForm.MainContentPanel.Controls.Clear();
            mainForm.MainContentPanel.Controls.Add(dashboard);
            dashboard.BringToFront();
        }

    }
    
}