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
    public partial class AddNewUserButton : UserControl
    {
        public event EventHandler AddUserClicked;

        public AddNewUserButton()
        {
            InitializeComponent();

            // Wire the internal button's click event
            btnAddNewUser.Click += AddButton_Click;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddUserClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}