using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module
{
    public partial class HistoryTopBar : UserControl
    {
        private ProfileMenuPainter profileMenuPainter = ProfileMenuPainter.CreateFromUserSession(); // ← CHANGE THIS LINE

        public HistoryTopBar()
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

        private void btnProfileMenu_Paint(object sender, PaintEventArgs e)
        {
            profileMenuPainter?.Draw(e); // ← Added null check
        }
    }
}