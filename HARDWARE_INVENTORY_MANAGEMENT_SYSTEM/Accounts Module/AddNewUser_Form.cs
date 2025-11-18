using System;
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
            LoadRoles(); // Load only Administrator and Staff roles
            LoadUsers();
        }

        private void LoadRoles()
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
            con.Close();
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

        private void LoadUsers()
        {
            // Your existing code
        }

        private void AddUser()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=.;Initial Catalog=TopazHardwareDb;Integrated Security=True"))
                {
                    connection.Open();

                    string query = @"INSERT INTO Users 
                                    (username, password_hash, first_name, last_name, role_id, status, email) 
                                    VALUES 
                                    (@username, @password, @firstName, @lastName, @roleId, @status, @email)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Get the selected role ID from combobox
                        string selectedRoleId = "";
                        if (RoleComboBox.SelectedItem is ComboboxItem selectedItem)
                        {
                            selectedRoleId = selectedItem.Value;
                        }

                        cmd.Parameters.AddWithValue("@username", guna2TextBox3.Text);
                        cmd.Parameters.AddWithValue("@password", HashPassword(guna2TextBox4.Text)); // Always hash passwords!
                        cmd.Parameters.AddWithValue("@firstName", guna2TextBox1.Text);
                        cmd.Parameters.AddWithValue("@lastName", guna2TextBox2.Text);
                        cmd.Parameters.AddWithValue("@roleId", selectedRoleId);
                        cmd.Parameters.AddWithValue("@status", cbxCombobox.SelectedItem?.ToString() ?? "Active");
                        cmd.Parameters.AddWithValue("@email", guna2TextBox5.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User added successfully!");
                            ClearFields();
                            OnUserAdded();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add user.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Simple password hashing method
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
            guna2TextBox1.Clear();
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2TextBox4.Clear();
            guna2TextBox5.Clear();
            RoleComboBox.SelectedIndex = -1;
            cbxCombobox.SelectedIndex = -1;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Validate inputs before adding user
            if (ValidateInputs())
            {
                AddUser();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox2.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox3.Text) ||
                string.IsNullOrWhiteSpace(guna2TextBox4.Text) ||
                RoleComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.");
                return false;
            }
            return true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            OnUserAdded();
        }

        private void closeButton1_Click(object sender, EventArgs e)
        {
            OnUserAdded();
        }

        public event EventHandler UserAdded;

        protected virtual void OnUserAdded()
        {
            UserAdded?.Invoke(this, EventArgs.Empty);
        }

        private void closeButton1_Load(object sender, EventArgs e)
        {
            // You can remove this if not needed
        }

        private void RoleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // You can add any additional logic here when role selection changes
        }
    }
}