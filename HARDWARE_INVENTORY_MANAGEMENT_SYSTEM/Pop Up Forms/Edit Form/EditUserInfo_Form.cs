using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    public partial class EditUserInfo_Form : UserControl
    {
        public event EventHandler CancelClicked;
        public event EventHandler<string> UserUpdated;

        private readonly Dictionary<string, string> roleMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private DataRow currentUserRow;

        public EditUserInfo_Form()
        {
            InitializeComponent();
            WireEvents();
        }

        private void WireEvents()
        {
            btnUpdate.Click += BtnUpdate_Click;
            btnCancel.Click += BtnCancel_Click;
            closeButton1.Click += BtnCancel_Click;
        }

        public void LoadUser(DataRow userRow)
        {
            currentUserRow = userRow;
            if (userRow == null)
            {
                return;
            }

            lblAccountID.Text = userRow.Field<string>("AccountID") ?? "";
            lblAccountCreated.Text = FormatDate(userRow["created_at"]);
            lblLastUpdated.Text = FormatDate(userRow["created_at"]);
            lblCreatedBy.Text = UserSession.Username ?? "N/A";

            tbxFullName.Text = userRow.Field<string>("Fullname") ?? string.Empty;
            tbxAddress.Text = userRow.Field<string>("Address") ?? string.Empty;
            tbxEmail.Text = userRow.Field<string>("username") ?? string.Empty;
            tbxPhoneNum.Text = string.Empty;
            tbxPosition.Text = userRow.Field<string>("Role") ?? string.Empty;

            LoadRoles(userRow.Field<string>("RoleID"), userRow.Field<string>("Role"));
            LoadAccountStatus(userRow.Field<string>("Account_status"));
        }

        private void LoadRoles(string roleId, string roleName)
        {
            cbxRole.Items.Clear();
            roleMap.Clear();

            var roles = UserService.LoadRoles();
            foreach (var role in roles)
            {
                cbxRole.Items.Add(role.Text);
                roleMap[role.Text] = role.Value;
            }

            if (!string.IsNullOrWhiteSpace(roleName) && cbxRole.Items.Contains(roleName))
            {
                cbxRole.SelectedItem = roleName;
            }
            else if (!string.IsNullOrWhiteSpace(roleId))
            {
                foreach (var kvp in roleMap)
                {
                    if (string.Equals(kvp.Value, roleId, StringComparison.OrdinalIgnoreCase))
                    {
                        cbxRole.SelectedItem = kvp.Key;
                        break;
                    }
                }
            }
            else if (cbxRole.Items.Count > 0)
            {
                cbxRole.SelectedIndex = 0;
            }
        }

        private void LoadAccountStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                cbxAccountStatus.SelectedIndex = 0;
                return;
            }

            int index = cbxAccountStatus.Items.IndexOf(status);
            cbxAccountStatus.SelectedIndex = index >= 0 ? index : 0;
        }

        private string FormatDate(object value)
        {
            if (value is DateTime dt)
            {
                return dt.ToString("MM-dd-yy");
            }

            DateTime parsed;
            if (DateTime.TryParse(Convert.ToString(value), out parsed))
            {
                return parsed.ToString("MM-dd-yy");
            }

            return "";
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (currentUserRow == null)
            {
                return;
            }

            if (!ValidateInputs())
            {
                return;
            }

            string accountId = currentUserRow.Field<string>("AccountID");
            string fullName = tbxFullName.Text.Trim();
            string username = tbxEmail.Text.Trim();
            string address = tbxAddress.Text.Trim();
            string roleId = ResolveSelectedRoleId();
            string status = cbxAccountStatus.SelectedItem?.ToString() ?? "Active";

            bool updated = UserService.UpdateUser(accountId, fullName, username, address, roleId, status);

            if (updated)
            {
                MessageBox.Show("Account updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UserUpdated?.Invoke(this, accountId);
            }
        }

        private string ResolveSelectedRoleId()
        {
            var selected = cbxRole.SelectedItem?.ToString();
            if (!string.IsNullOrWhiteSpace(selected) && roleMap.TryGetValue(selected, out var roleId))
            {
                return roleId;
            }

            return currentUserRow?.Field<string>("RoleID") ?? string.Empty;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(tbxFullName.Text))
            {
                MessageBox.Show("Please enter full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxFullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tbxEmail.Text))
            {
                MessageBox.Show("Please enter username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxEmail.Focus();
                return false;
            }

            if (cbxRole.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxRole.Focus();
                return false;
            }

            if (cbxAccountStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an account status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxAccountStatus.Focus();
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
