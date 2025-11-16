using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module;
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
    public partial class Settings_Signout: UserControl
    {
        public Settings_Signout()
        {
            InitializeComponent();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                SettingsMainPage settingsPage = new SettingsMainPage();
                mainForm.MainContentPanelAccess.Controls.Add(settingsPage);
            }
        }
    }
}
