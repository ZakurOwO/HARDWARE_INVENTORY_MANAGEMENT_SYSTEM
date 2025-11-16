using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.SettingsTab
{
    public partial class TabModules: UserControl
    {
        public event EventHandler ShowAccounts;
        public event EventHandler ShowHistory;
        public event EventHandler ShowAuditLog;

        public TabModules()
        {
            InitializeComponent();
        }


        private void SelectedTab(Guna.UI2.WinForms.Guna2Button selectedButton)
        {
            //reset buttons
            btnAccounts.FillColor = Color.White;
            btnAccounts.ForeColor = Color.Black;
            btnAccounts.Font = new Font(btnAccounts.Font, FontStyle.Regular);

            btnHistory.FillColor = Color.White;
            btnHistory.ForeColor = Color.Black;
            btnHistory.Font = new Font(btnHistory.Font, FontStyle.Regular);

            btnHistory.FillColor = Color.White;
            btnHistory.ForeColor = Color.Black;
            btnHistory.Font = new Font(btnHistory.Font, FontStyle.Regular);

            btnAuditLog.FillColor = Color.White;
            btnAuditLog.ForeColor = Color.Black;
            btnAuditLog.Font = new Font(btnAuditLog.Font, FontStyle.Regular);

            //highlight selected button
            selectedButton.FillColor = Color.FromArgb(229, 240, 249); //light blue
            selectedButton.ForeColor = Color.FromArgb(42, 134, 205);   //dark blue
            selectedButton.Font = new Font(selectedButton.Font, FontStyle.Bold);
            selectedButton.BorderRadius = 5;
        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            SelectedTab(btnAccounts);
            ShowAccounts?.Invoke(this, EventArgs.Empty);
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            SelectedTab(btnHistory);
            ShowHistory?.Invoke(this, EventArgs.Empty);
        }

        private void btnAuditLog_Click(object sender, EventArgs e)
        {
            SelectedTab(btnAuditLog);
            ShowAuditLog?.Invoke(this, EventArgs.Empty);
        }

        private void TabModules_Load(object sender, EventArgs e)
        {
            SelectedTab(btnAccounts);
        }
    }
}
