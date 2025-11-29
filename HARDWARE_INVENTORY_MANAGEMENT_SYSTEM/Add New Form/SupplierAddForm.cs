using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public partial class SupplierAddForm : UserControl
    {
        private SqlConnection con;
        public event EventHandler CancelClicked;
        public event EventHandler SupplierAdded;
        private string connectionString = ConnectionString.DataSource;

        public SupplierAddForm()
        {
            InitializeComponent();

            // Initialize the connection
            con = new SqlConnection(connectionString);


            // Wire up all event handlers programmatically
            closeButton1.Click += CloseButton1_Click;
            AddSupplierFormBtn.Click += SaveButton_Click;
            CancelSupplierFormBtn.Click += CancelSupplierFormBtn_Click_1;

            SupplierDatePick.Value = DateTime.Now;

            // Hide unused status field (not in database schema)
            label8.Visible = false;
            label9.Visible = false;
            cbxCategory.Visible = false;
        }

        private bool ValidateInput()
        {
            // Required: Company Name
            if (string.IsNullOrWhiteSpace(CompanyNameTextBoxSupplier.Text))
            {
                MessageBox.Show("Company Name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CompanyNameTextBoxSupplier.Focus();
                return false;
            }

            // Required: Contact Person
            if (string.IsNullOrWhiteSpace(tbxContactPerson.Text))
            {
                MessageBox.Show("Contact Person is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxContactPerson.Focus();
                return false;
            }

            // Required: Contact Number
            if (string.IsNullOrWhiteSpace(ContactTxtBoxSupplier.Text))
            {
                MessageBox.Show("Contact Number is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ContactTxtBoxSupplier.Focus();
                return false;
            }

            // Validate email format if provided
            if (!string.IsNullOrWhiteSpace(tbxEmail.Text))
            {
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(tbxEmail.Text.Trim(), emailPattern))
                {
                    MessageBox.Show("Please enter a valid email address.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbxEmail.Focus();
                    return false;
                }
            }

            // Validate phone number format
            string phone = ContactTxtBoxSupplier.Text.Replace("-", "").Replace(" ", "").Trim();
            if (phone.Length < 10)
            {
                MessageBox.Show("Please enter a valid contact number (minimum 10 digits).",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ContactTxtBoxSupplier.Focus();
                return false;
            }

            return true;
        }

        private bool IsDuplicateSupplier(string companyName)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Suppliers WHERE LOWER(supplier_name) = LOWER(@CompanyName)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CompanyName", companyName.Trim());

                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking for duplicate: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void AddSupplier()
        {
            if (!ValidateInput())
                return;

            if (IsDuplicateSupplier(CompanyNameTextBoxSupplier.Text))
            {
                MessageBox.Show("A supplier with this company name already exists.",
                    "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CompanyNameTextBoxSupplier.Focus();
                return;
            }

            try
            {
                // Insert using the EXACT schema fields
                string query = @"INSERT INTO Suppliers 
                    (supplier_name, contact_person, contact_number, email, address) 
                    VALUES 
                    (@SupplierName, @ContactPerson, @ContactNumber, @Email, @Address)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SupplierName", CompanyNameTextBoxSupplier.Text.Trim());
                    cmd.Parameters.AddWithValue("@ContactPerson", tbxContactPerson.Text.Trim());
                    cmd.Parameters.AddWithValue("@ContactNumber", ContactTxtBoxSupplier.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email",
                        string.IsNullOrWhiteSpace(tbxEmail.Text) ? (object)DBNull.Value : tbxEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address",
                        string.IsNullOrWhiteSpace(LocationSupplierTextBox.Text) ? (object)DBNull.Value : LocationSupplierTextBox.Text.Trim());

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Supplier added successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SupplierAdded?.Invoke(this, EventArgs.Empty);
                        ClearFields();
                        CancelClicked?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void ClearFields()
        {
            CompanyNameTextBoxSupplier.Clear();
            ContactTxtBoxSupplier.Clear();
            LocationSupplierTextBox.Clear();
            tbxEmail.Clear();
            tbxContactPerson.Clear();
            SupplierDatePick.Value = DateTime.Now;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            AddSupplier();
        }

        private void CancelSupplierFormBtn_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CompanyNameTextBoxSupplier.Text) ||
                !string.IsNullOrWhiteSpace(tbxContactPerson.Text))
            {
                var result = MessageBox.Show(
                    "Are you sure you want to cancel? Any unsaved changes will be lost.",
                    "Confirm Cancel",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            CancelClicked?.Invoke(this, EventArgs.Empty);
        }

        private void CloseButton1_Click(object sender, EventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }

        private void label4_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
    }
}