using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    public partial class AddNewUser_Form : UserControl
    {
        public event EventHandler CancelClicked;
        public AddNewUser_Form()
        {
            InitializeComponent();
            LoadRoles();
            LoadAccountStatus();
            LoadNextAccountID();
            closeButton1.Click += closeButton1_Load_1;
            ClearBtn.Click += ClearBtn_Click;
        }

        private void LoadNextAccountID()
        {
            string nextAccountId = DatabaseHelper.GetNextAccountId();
            AccountIDTxtbx.Text = nextAccountId;
            AccountIDTxtbx.ReadOnly = true;
            AccountIDTxtbx.BackColor = SystemColors.Control;
        }

        private void LoadAccountStatus()
        {
            AccountStatusComboBox.Items.Clear();
            AccountStatusComboBox.Items.Add("Active");
            AccountStatusComboBox.Items.Add("Inactive");
            AccountStatusComboBox.SelectedIndex = 0;
        }

        private void LoadRoles()
        {
            var roles = UserService.LoadRoles();
            RoleComboBox.Items.Clear();
            foreach (var role in roles)
            {
                RoleComboBox.Items.Add(role);
            }
        }

        private void AddUser()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
            {
                connection.Open();

                string selectedRoleId = "";
                string selectedRoleName = "";

                if (RoleComboBox.SelectedItem is ComboboxItem selectedItem)
                {
                    selectedRoleId = selectedItem.Value;
                    selectedRoleName = selectedItem.Text;
                }
                else
                {
                    MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (DatabaseHelper.IsUsernameExists(UserNameTxtbx.Text.Trim()))
                {
                    MessageBox.Show("Username already exists. Please choose a different username.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UserNameTxtbx.Focus();
                    UserNameTxtbx.SelectAll();
                    return;
                }

                bool success = UserService.AddUser(
                    FullNameTxtbx.Text.Trim(),
                    UserNameTxtbx.Text.Trim(),
                    PasswordTxtbx.Text,
                    AddressTxtbx.Text.Trim(),
                    selectedRoleId,
                    AccountStatusComboBox.SelectedItem?.ToString() ?? "Active"
                );

                if (success)
                {
                    string generatedAccountId = DatabaseHelper.GetGeneratedAccountId(connection);

                    MessageBox.Show($"User added successfully!\nAccount ID: {generatedAccountId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();

                    OnUserAdded(generatedAccountId, FullNameTxtbx.Text.Trim(), selectedRoleName, AccountStatusComboBox.SelectedItem?.ToString() ?? "Active");
                }
            }
        }

        private void ClearFields()
        {
            FullNameTxtbx.Clear();
            AddressTxtbx.Clear();
            UserNameTxtbx.Clear();
            PasswordTxtbx.Clear();
            EmailTxtbx.Clear();
            RoleComboBox.SelectedIndex = -1;
            AccountStatusComboBox.SelectedIndex = 0;
            LoadNextAccountID();
            FullNameTxtbx.Focus();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                AddUser();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(FullNameTxtbx.Text))
            {
                MessageBox.Show("Please enter full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FullNameTxtbx.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(AddressTxtbx.Text))
            {
                MessageBox.Show("Please enter address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AddressTxtbx.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(UserNameTxtbx.Text))
            {
                MessageBox.Show("Please enter username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UserNameTxtbx.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(PasswordTxtbx.Text))
            {
                MessageBox.Show("Please enter password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PasswordTxtbx.Focus();
                return false;
            }

            if (PasswordTxtbx.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PasswordTxtbx.Focus();
                PasswordTxtbx.SelectAll();
                return false;
            }

            if (RoleComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RoleComboBox.Focus();
                return false;
            }

            return true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            ClearFields();
            OnUserAdded(null, null, null, null);
        }

        private void closeButton1_Click(object sender, EventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
            //OnUserAdded(null, null, null, null);
        }

        public event EventHandler<(string AccountID, string FullName, string Role, string Status)> UserAdded;

        protected virtual void OnUserAdded(string accountId, string fullName, string role, string status)
        {
            UserAdded?.Invoke(this, (accountId, fullName, role, status));
        }

        private void PasswordTxtbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (ValidateInputs())
                {
                    AddUser();
                }
                e.Handled = true;
            }
        }

        private void EmailTxtbx_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EmailTxtbx.Text) && !SecurityHelper.IsValidGmail(EmailTxtbx.Text.Trim()))
            {
                MessageBox.Show("Please enter a valid Gmail address (must end with @gmail.com).", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EmailTxtbx.Focus();
                EmailTxtbx.SelectAll();
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }

        private void ProceedBtn_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                AddUser();
            }
        }

        // Event handlers
        private void AccountIDTxtbx_TextChanged(object sender, EventArgs e) { }
        private void closeButton1_Load(object sender, EventArgs e) { }
        private void RoleComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void FullNameTxtbx_TextChanged(object sender, EventArgs e) { }
        private void AddressTxtbx_TextChanged(object sender, EventArgs e) { }
        private void PasswordTxtbx_TextChanged(object sender, EventArgs e) { }
        private void EmailTxtbx_TextChanged(object sender, EventArgs e) { }
        private void AccountStatusComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void DateTextBox_TextChanged(object sender, EventArgs e) { }

        private void closeButton1_Load_1(object sender, EventArgs e)
        {

        }
    }
}