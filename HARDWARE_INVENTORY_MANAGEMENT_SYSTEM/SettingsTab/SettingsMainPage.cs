using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.Class_Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles
{
    public partial class SettingsMainPage : UserControl
    {
        private ProfileMenuPainter profileMenuPainter = ProfileMenuPainter.CreateFromUserSession();

        public SettingsMainPage()
        {
            InitializeComponent();
            ProfileMenu.Paint += ProfileBtn_Paint; // Add this line
        }

        private void SettingsMainPage_Load(object sender, EventArgs e)
        {
            tabModules1.ShowAccounts += PnlNavBar_ShowAccounts;
            tabModules1.ShowHistory += PnlNavBar_ShowHistory;
            tabModules1.ShowAuditLog += PnlNavBar_ShowAuditLog;

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

        private void ProfileBtn_Click(object sender, EventArgs e)
        {
            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                SettingsMainClass.ShowSettingsPanel(mainForm.MainContentPanelAccess);
            }
        }

        private void ProfileBtn_Paint(object sender, PaintEventArgs e)
        {
            profileMenuPainter?.Draw(e);
        }
    }
}