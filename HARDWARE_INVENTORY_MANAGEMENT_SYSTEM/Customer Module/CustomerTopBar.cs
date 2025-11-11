using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System;
using System.Windows.Forms;
namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public partial class CustomerTopBar : UserControl
    {
        public CustomerTopBar()
        {
            InitializeComponent();
        }

        private void btnProfileMenu_Click(object sender, EventArgs e)
        {
            var mainForm = this.FindForm() as MainDashBoard;

            if (mainForm != null)
            {
                SettingsMainClass.ShowSettingsPanel(mainForm.MainContentPanelAccess);
            }
        }
        private ProfileMenuPainter profileMenuPainter = new ProfileMenuPainter();
        private void btnProfileMenu_Paint(object sender, PaintEventArgs e)
        {

            profileMenuPainter.Draw(e);
        }


    }
}
