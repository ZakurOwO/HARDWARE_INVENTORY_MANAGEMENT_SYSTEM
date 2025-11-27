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

                // hide existing side panel if present
                var originalSide = mainForm.Controls.Cast<Control>()
                                      .FirstOrDefault(c => c.Name == "sidePanel1" || c.GetType().Name == "SidePanel");
                if (originalSide != null)
                {
                    originalSide.Visible = false;
                }

                // show/create settings side panel in the same place
                var settingsSide = mainForm.Controls.OfType<SettingsSidePanel>().FirstOrDefault();
                if (settingsSide == null)
                {
                    settingsSide = new SettingsSidePanel();
                    settingsSide.Name = "settingsSidePanel1";

                    // position/size to match original side panel (fallback to defaults)
                    if (originalSide != null)
                    {
                        settingsSide.Location = originalSide.Location;
                        settingsSide.Size = originalSide.Size;
                    }
                    else
                    {
                        settingsSide.Location = new Point(22, 23);
                        settingsSide.Size = new Size(202, 670);
                    }

                    mainForm.Controls.Add(settingsSide);
                }

                settingsSide.BringToFront();
                settingsSide.Visible = true;

            }


        }
    }
}
