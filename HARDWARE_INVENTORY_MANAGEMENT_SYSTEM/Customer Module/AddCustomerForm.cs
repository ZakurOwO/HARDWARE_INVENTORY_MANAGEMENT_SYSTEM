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
    public partial class AddCustomerForm : Form
    {
        // Connection string to InventoryCapstone
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InventoryCapstone;Integrated Security=True";

        public AddCustomerForm()
        {
            InitializeComponent();
            closeButton1.CloseClicked += (s, e) => this.Close();
            this.TopLevel = false;                // allow embedding
            this.FormBorderStyle = FormBorderStyle.None;
        }

        // Method to add a customer using SqlDataAdapter
        private void AddCustomer()
        {
            string customerName = tbxCompanyName.Text.Trim();
            string contactNumber = tbxEmail.Text.Trim();
            string address = tbxAddress.Text.Trim();

            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Please enter the customer name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Create a DataTable to hold new customer row
                    DataTable dt = new DataTable();
                    dt.Columns.Add("customer_name", typeof(string));
                    dt.Columns.Add("contact_number", typeof(string));
                    dt.Columns.Add("address", typeof(string));

                    // Add a new row to the DataTable
                    DataRow newRow = dt.NewRow();
                    newRow["customer_name"] = customerName;
                    newRow["contact_number"] = contactNumber;
                    newRow["address"] = address;
                    dt.Rows.Add(newRow);

                    // Create SqlDataAdapter
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        // Define the InsertCommand
                        da.InsertCommand = new SqlCommand(
                            "INSERT INTO Customers (customer_name, contact_number, address) VALUES (@name, @contact, @address)",
                            con
                        );

                        da.InsertCommand.Parameters.Add("@name", SqlDbType.NVarChar, 100, "customer_name");
                        da.InsertCommand.Parameters.Add("@contact", SqlDbType.NVarChar, 20, "contact_number");
                        da.InsertCommand.Parameters.Add("@address", SqlDbType.NVarChar, 255, "address");

                        con.Open();
                        da.Update(dt); // This inserts the new row
                        con.Close();
                    }
                }

                MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the textboxes
                tbxCompanyName.Clear();
                tbxEmail.Clear();
                tbxAddress.Clear();
                tbxCityMunicipality.Clear();
                tbxProvince.Clear();
                tbxContactNumber.Clear();
                tbxContactPerson.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Trigger AddCustomer when the PictureBox is clicked
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddCustomer();
        }

        // Leave other event handlers empty (original code)
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
        private void AddFormButton_Click(object sender, EventArgs e) { }

        private void tbxCompanyName_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxContactPerson_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxContactNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxCityMunicipality_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxProvince_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormCancelButton_Click(object sender, EventArgs e)
        {

        }

        private void closeButton1_Load(object sender, EventArgs e)
        {

        }

        private void btnWhite_Click(object sender, EventArgs e)
        {

        }
    }
}
