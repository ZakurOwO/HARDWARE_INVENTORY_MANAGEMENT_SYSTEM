using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    public partial class AddNewUser_Form : UserControl
    {
        private SqlConnection con;
        private SqlDataAdapter da;
        private DataTable dt;

        public AddNewUser_Form()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=.;Initial Catalog=TopazHardwareDb;Integrated Security=True");
            LoadRoles();
            LoadAccountStatus();
        }

        // Remove UserData class and NewUserData property

        // UPDATE THIS METHOD: Return DataTable instead of List<UserData>
        public static DataTable LoadExistingUsersFromDatabase()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=.;Initial Catalog=TopazHardwareDb;Integrated Security=True"))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            a.AccountID,
                            a.Fullname,
                            a.username,
                            a.Account_status,
                            a.Address,
                            a.created_at,
                            r.role_name as Role
                        FROM Accounts a
                        INNER JOIN Roles r ON a.RoleID = r.RoleID
                        ORDER BY a.created_at DESC";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users from database: {ex.Message}", "Database Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
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
            try
            {
                con.Open();
                string query = "SELECT RoleID, role_name FROM Roles ORDER BY role_name";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                RoleComboBox.Items.Clear();

                while (reader.Read())
                {
                    RoleComboBox.Items.Add(new ComboboxItem
                    {
                        Text = reader["role_name"].ToString(),
                        Value = reader["RoleID"].ToString()
                    });
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void AddUser()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=.;Initial Catalog=TopazHardwareDb;Integrated Security=True"))
                {
                    connection.Open();

                    // Remove AccountID from INSERT since it's computed
                    string query = @"INSERT INTO Accounts 
                            (Fullname, username, password_hash, Address, RoleID, Account_status) 
                            VALUES 
                            (@fullname, @username, @password, @address, @roleId, @status)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
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

                        if (IsUsernameExists(UserNameTxtbx.Text.Trim()))
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            UserNameTxtbx.Focus();
                            UserNameTxtbx.SelectAll();
                            return;
                        }

                        if (!string.IsNullOrWhiteSpace(EmailTxtbx.Text) && !IsValidGmail(EmailTxtbx.Text.Trim()))
                        {
                            MessageBox.Show("Please enter a valid Gmail address (must end with @gmail.com).", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            EmailTxtbx.Focus();
                            EmailTxtbx.SelectAll();
                            return;
                        }

                        cmd.Parameters.AddWithValue("@fullname", FullNameTxtbx.Text.Trim());
                        cmd.Parameters.AddWithValue("@username", UserNameTxtbx.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", HashPassword(PasswordTxtbx.Text));
                        cmd.Parameters.AddWithValue("@address", AddressTxtbx.Text.Trim());
                        cmd.Parameters.AddWithValue("@roleId", selectedRoleId);
                        cmd.Parameters.AddWithValue("@status", AccountStatusComboBox.SelectedItem?.ToString() ?? "Active");

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Get the generated AccountID
                            string generatedAccountId = GetGeneratedAccountId(connection);

                            MessageBox.Show($"User added successfully!\nAccount ID: {generatedAccountId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();

                            // Pass basic info to main form
                            OnUserAdded(generatedAccountId, FullNameTxtbx.Text.Trim(), selectedRoleName, AccountStatusComboBox.SelectedItem?.ToString() ?? "Active");
                        }
                        else
                        {
                            MessageBox.Show("Failed to add user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UserNameTxtbx.Focus();
                    UserNameTxtbx.SelectAll();
                }
                else if (sqlEx.Number == 515)
                {
                    MessageBox.Show("Database error: Required fields are missing. Please check your input.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Database error: " + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // NEW METHOD: Get the generated AccountID after insert
        private string GetGeneratedAccountId(SqlConnection connection)
        {
            try
            {
                string query = "SELECT TOP 1 AccountID FROM Accounts ORDER BY AccountInternalID DESC";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    return cmd.ExecuteScalar()?.ToString() ?? "ACC-00000";
                }
            }
            catch
            {
                return "ACC-00000";
            }
        }

        private bool IsValidGmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return email.Trim().ToLower().EndsWith("@gmail.com");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
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
            OnUserAdded(null, null, null, null);
        }

        // UPDATE THE EVENT: Pass individual values instead of UserData object
        public event EventHandler<(string AccountID, string FullName, string Role, string Status)> UserAdded;

        protected virtual void OnUserAdded(string accountId, string fullName, string role, string status)
        {
            UserAdded?.Invoke(this, (accountId, fullName, role, status));
        }

        private bool IsUsernameExists(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=.;Initial Catalog=TopazHardwareDb;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Accounts WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
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
            if (!string.IsNullOrWhiteSpace(EmailTxtbx.Text) && !IsValidGmail(EmailTxtbx.Text.Trim()))
            {
                MessageBox.Show("Please enter a valid Gmail address (must end with @gmail.com).", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EmailTxtbx.Focus();
                EmailTxtbx.SelectAll();
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ProceedBtn_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                AddUser();
            }
        }

        // Leave other event handlers empty
        private void closeButton1_Load(object sender, EventArgs e) { }
        private void RoleComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void AccountIDTxtbx_TextChanged(object sender, EventArgs e) { }
        private void FullNameTxtbx_TextChanged(object sender, EventArgs e) { }
        private void AddressTxtbx_TextChanged(object sender, EventArgs e) { }
        private void PasswordTxtbx_TextChanged(object sender, EventArgs e) { }
        private void EmailTxtbx_TextChanged(object sender, EventArgs e) { }
        private void AccountStatusComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void DateTextBox_TextChanged(object sender, EventArgs e) { }
    }
}