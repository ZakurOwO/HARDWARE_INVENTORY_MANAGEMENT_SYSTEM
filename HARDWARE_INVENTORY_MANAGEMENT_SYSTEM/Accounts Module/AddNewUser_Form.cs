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
            con = new SqlConnection(@"Data Source=.;Initial Catalog=InventoryCapstone;Integrated Security=True");
            LoadUsers();
        }

        private void LoadUsers()
        {
            dt = new DataTable();
            da = new SqlDataAdapter("SELECT * FROM Users", con);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Fill(dt);
        }

        private void AddUser()
        {
            try
            {
                DataRow newRow = dt.NewRow();
                newRow["user_id"] = tbxTextBox.Text;
                newRow["first_name"] = guna2TextBox1.Text;
                newRow["last_name"] = guna2TextBox2.Text;
                newRow["username"] = guna2TextBox3.Text;
                newRow["password_hash"] = guna2TextBox4.Text;
                newRow["email"] = guna2TextBox5.Text;
                newRow["role_id"] = guna2ComboBox1.SelectedValue;
                newRow["status"] = cbxCombobox.SelectedItem.ToString();
                newRow["created_at"] = guna2TextBox6.Text;
                dt.Rows.Add(newRow);

                da.Update(dt);
                MessageBox.Show("User added successfully!");
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            tbxTextBox.Clear();
            guna2TextBox1.Clear();
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2TextBox4.Clear();
            guna2TextBox5.Clear();
            guna2ComboBox1.SelectedIndex = -1;
            cbxCombobox.SelectedIndex = -1;
            guna2TextBox6.Clear();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            AddUser();
        }
    }
}
