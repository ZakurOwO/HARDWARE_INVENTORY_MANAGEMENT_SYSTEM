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
        public event EventHandler CancelRequested;
        public event EventHandler SupplierAdded;
        private string connectionString = ConnectionString.DataSource;

        public SupplierAddForm()
        {
            InitializeComponent();

            // Wire up all event handlers programmatically
            closeButton1.Click += (s, e) => CancelRequested?.Invoke(this, EventArgs.Empty);
            AddSupplierFormBtn.Click += SaveButton_Click;
            CancelSupplierFormBtn.Click += (s, e) => CancelRequested?.Invoke(this, EventArgs.Empty);

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
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM Suppliers WHERE LOWER(supplier_name) = LOWER(@CompanyName)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CompanyName", companyName.Trim());

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

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    // Insert using the EXACT schema fields
                    string query = @"INSERT INTO Suppliers
                        (supplier_name, contact_person, contact_number, email, address)
                        OUTPUT INSERTED.SupplierID, INSERTED.supplier_id
                        VALUES
                        (@SupplierName, @ContactPerson, @ContactNumber, @Email, @Address)";

                    con.Open();
                    transaction = con.BeginTransaction();

                    SupplierRecord newSupplier = new SupplierRecord();

                    using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@SupplierName", CompanyNameTextBoxSupplier.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactPerson", tbxContactPerson.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactNumber", ContactTxtBoxSupplier.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email",
                            string.IsNullOrWhiteSpace(tbxEmail.Text) ? (object)DBNull.Value : tbxEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address",
                            string.IsNullOrWhiteSpace(LocationSupplierTextBox.Text) ? (object)DBNull.Value : LocationSupplierTextBox.Text.Trim());

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                newSupplier.SupplierID = reader["SupplierID"].ToString();
                                newSupplier.SupplierInternalId = Convert.ToInt32(reader["supplier_id"]);
                            }
                        }

                        newSupplier.SupplierName = CompanyNameTextBoxSupplier.Text.Trim();
                        newSupplier.ContactPerson = tbxContactPerson.Text.Trim();
                        newSupplier.ContactNumber = ContactTxtBoxSupplier.Text.Trim();
                        newSupplier.Email = string.IsNullOrWhiteSpace(tbxEmail.Text) ? null : tbxEmail.Text.Trim();
                        newSupplier.Address = string.IsNullOrWhiteSpace(LocationSupplierTextBox.Text) ? null : LocationSupplierTextBox.Text.Trim();
                    }

                    SupplierAuditLogger.LogSupplierAudit(
                        con,
                        transaction,
                        activity: $"Added supplier {newSupplier.SupplierName}",
                        activityType: "CREATE",
                        recordId: newSupplier.SupplierID ?? newSupplier.SupplierInternalId.ToString(),
                        oldValues: null,
                        newValues: SupplierAuditLogger.BuildSupplierState(newSupplier));

                    transaction.Commit();

                    MessageBox.Show("Supplier added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SupplierAdded?.Invoke(this, EventArgs.Empty);
                    ClearFields();
                    CancelRequested?.Invoke(this, EventArgs.Empty); // Close after add
                }
                catch (SqlException ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show($"Database error: {ex.Message}",
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show($"An error occurred: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

            CancelRequested?.Invoke(this, EventArgs.Empty);
        }


        private void label4_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void closeButton1_Click(object sender, EventArgs e)
        {
            CancelRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}