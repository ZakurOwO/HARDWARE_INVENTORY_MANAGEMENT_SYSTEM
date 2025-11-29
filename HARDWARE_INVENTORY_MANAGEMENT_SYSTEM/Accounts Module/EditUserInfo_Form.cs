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

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    public partial class EditUserInfo_Form : UserControl
    {
        public event EventHandler CancelClicked;
        private DatabaseHelper.DatabaseUserModel originalUser;

        public EditUserInfo_Form()
        {
            InitializeComponent();
        }

        public class EditUserModel
        {
            public string AccountID { get; set; }
            public string CreatedBy { get; set; }
            public DateTime AccountCreated { get; set; }
            public DateTime LastUpdate { get; set; }

            public string FullName { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string Position { get; set; }
            public string Role { get; set; }
            public string Status { get; set; }
        }

        public void LoadUser(string accountId)
        {
            // Get data from database
            originalUser = DatabaseHelper.GetUserByAccountID(accountId);

            lblAccountID.Text = originalUser.AccountID;
            lblCreatedBy.Text = originalUser.CreatedBy;
            lblAccountCreated.Text = originalUser.AccountCreated.ToString("MM-dd-yy");
            lblLastUpdated.Text = originalUser.LastUpdate.ToString("MM-dd-yy");

            tbxFullName.Text = originalUser.FullName;
            tbxPhoneNum.Text = originalUser.PhoneNumber;
            tbxAddress.Text = originalUser.Address;
            tbxEmail.Text = originalUser.Email;
            tbxPosition.Text = originalUser.Position;

            cbxRole.SelectedItem = originalUser.Role;
            cbxAccountStatus.SelectedItem = originalUser.Status;
        }

        private bool HasUnsavedChanges()
        {
            if (tbxFullName.Text != originalUser.FullName) return true;
            if (tbxPhoneNum.Text != originalUser.PhoneNumber) return true;
            if (tbxAddress.Text != originalUser.Address) return true;
            if (tbxEmail.Text != originalUser.Email) return true;
            if (tbxPosition.Text != originalUser.Position) return true;

            if (cbxRole.SelectedItem.ToString() != originalUser.Role) return true;
            if (cbxAccountStatus.SelectedItem.ToString() != originalUser.Status) return true;

            return false;
        }


        private void closeButton1_Click(object sender, EventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (HasUnsavedChanges())
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to cancel? Any unsaved changes will be lost.",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.No)
                    return; // stay on form
            }

            CancelClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
