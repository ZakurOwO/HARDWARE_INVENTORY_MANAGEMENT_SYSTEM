using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public partial class AddCustomerForm : UserControl
    {
        // 🔹 Connection string for your InventoryCapstone database
        // ⚠️ Change Data Source if your SQL instance is different
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InventoryCapstone;Integrated Security=True";

        public AddCustomerForm()
        {
            InitializeComponent();
        }

        // 🟢 Main method to insert customer into the database
        private void AddCustomer()
        {
            try
            {
                // Use your actual textbox names
                string customerName = CompanyNameCustomerTextBox.Text.Trim();
                string contactNumber = EmailAddressTextBox.Text.Trim();
                string address = LocationTextBox.Text.Trim();

                if (string.IsNullOrEmpty(customerName))
                {
                    MessageBox.Show("Please enter the customer name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "INSERT INTO Customers (customer_name, contact_number, address) VALUES (@name, @contact, @address)";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", customerName);
                        cmd.Parameters.AddWithValue("@contact", contactNumber);
                        cmd.Parameters.AddWithValue("@address", address);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                MessageBox.Show("Customer added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear input fields after saving
                CompanyNameCustomerTextBox.Clear();
                EmailAddressTextBox.Clear();
                LocationTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🖼️ This will act as your Add Customer trigger (PictureBox click)
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddCustomer();
        }

        // 🔹 The rest of your original empty event handlers stay as-is
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void kryptonComboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
        private void label15_Click(object sender, EventArgs e) { }
        private void label16_Click(object sender, EventArgs e) { }
        private void label18_Click(object sender, EventArgs e) { }
        private void label19_Click(object sender, EventArgs e) { }
        private void label20_Click(object sender, EventArgs e) { }
        private void TexboxCompanyName_TextChanged(object sender, EventArgs e) { }
        private void kryptonRichTextBox2_TextChanged(object sender, EventArgs e) { }
        private void kryptonRichTextBox1_TextChanged(object sender, EventArgs e) { }
        private void kryptonRichTextBox3_TextChanged(object sender, EventArgs e) { }
        private void kryptonRichTextBox4_TextChanged(object sender, EventArgs e) { }
        private void kryptonTextBox1_TextChanged(object sender, EventArgs e) { }
        private void EditSupplierButton_Click(object sender, EventArgs e) { }
    }
}
