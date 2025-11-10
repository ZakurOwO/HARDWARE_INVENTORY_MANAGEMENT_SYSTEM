using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    public partial class AccountsTopBar : UserControl
    {
        public AccountsTopBar()
        {
            InitializeComponent();
        }

        private void AccountsTopBar_Click(object sender, EventArgs e)
        {

        }

        private void btnProfileMenu_Click(object sender, EventArgs e)
        {
            var mainForm = this.FindForm() as MainDashBoard;

            if (mainForm != null)
            {
               
                mainForm.ShowSettingsPanel(mainForm.MainContentPanelAccess);
            }
        }

    }
}
