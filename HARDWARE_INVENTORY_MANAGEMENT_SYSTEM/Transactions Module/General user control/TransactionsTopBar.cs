using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class TransactionsTopBar : UserControl
    {
        private ProfileMenuPainter profileMenuPainter = ProfileMenuPainter.CreateFromUserSession();

        public TransactionsTopBar()
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
            profileMenuPainter?.Draw(e);
        }
    }
}