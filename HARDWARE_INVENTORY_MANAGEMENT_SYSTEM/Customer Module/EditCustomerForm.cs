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
    public partial class EditCustomerForm : Form
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TopazHardwareDb;Integrated Security=True";

        private int customerId;
        private string originalCustomerName;

        public EditCustomerForm(int customerId, string customerName, string contactNumber, string address)
        {
            InitializeComponent();

            this.customerId = customerId;
            this.originalCustomerName = customerName;

            // Pre-fill the form with existing data
            tbxCompanyName.Text = customerName;
            tbxContactNumber.Text = contactNumber ?? "";
            tbxAddress.Text = address ?? "";

            closeButton1.CloseClicked += (s, e) => this.Close();
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void UpdateCustomer()
        {
            string customerName = tbxCompanyName.Text.Trim();
            string contactNumber = tbxContactNumber.Text.Trim();
            string address = tbxAddress.Text.Trim();

            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Please enter the customer name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Check if customer name already exists (excluding current customer)
                    if (customerName != originalCustomerName)
                    {
                        string checkQuery = "SELECT COUNT(*) FROM Customers WHERE customer_name = @customerName AND customer_id != @customerId";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                        checkCmd.Parameters.AddWithValue("@customerName", customerName);
                        checkCmd.Parameters.AddWithValue("@customerId", customerId);

                        con.Open();
                        int existingCount = (int)checkCmd.ExecuteScalar();
                        con.Close();

                        if (existingCount > 0)
                        {
                            MessageBox.Show("A customer with this name already exists. Please use a different name.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Update customer using direct SQL command
                    string updateQuery = @"
                        UPDATE Customers 
                        SET customer_name = @customerName, 
                            contact_number = @contactNumber, 
                            address = @address 
                        WHERE customer_id = @customerId";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@customerName", customerName);
                    updateCmd.Parameters.AddWithValue("@contactNumber", string.IsNullOrEmpty(contactNumber) ? (object)DBNull.Value : contactNumber);
                    updateCmd.Parameters.AddWithValue("@address", string.IsNullOrEmpty(address) ? (object)DBNull.Value : address);
                    updateCmd.Parameters.AddWithValue("@customerId", customerId);

                    con.Open();
                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer updated successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Close the form
                    }
                    else
                    {
                        MessageBox.Show("No changes were made to the customer.", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Update customer when the blue button is clicked
        private void btnBlue_Click(object sender, EventArgs e)
        {
            UpdateCustomer();
        }

        // Cancel and close the form when the white button is clicked
        private void btnWhite_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Event handler for close button
        private void closeButton1_Load(object sender, EventArgs e)
        {
            // Already handled in constructor
        }

        // Empty event handlers for other controls
        private void tbxCompanyName_TextChanged(object sender, EventArgs e) { }
        private void tbxContactNumber_TextChanged(object sender, EventArgs e) { }
        private void tbxAddress_TextChanged(object sender, EventArgs e) { }
    }
}