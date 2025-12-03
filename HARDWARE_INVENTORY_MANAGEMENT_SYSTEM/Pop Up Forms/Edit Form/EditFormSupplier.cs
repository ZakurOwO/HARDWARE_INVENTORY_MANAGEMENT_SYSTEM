using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public partial class EditFormSupplier: UserControl
    {
        public event EventHandler SupplierUpdated;
        public event EventHandler CancelRequested;

        private SupplierRecord currentSupplier;
        private readonly string connectionString = ConnectionString.DataSource;

        public EditFormSupplier()
        {
            InitializeComponent();

            // Wire events
            CancelSupplierFormBtn.Click += (s, e) => CancelRequested?.Invoke(this, EventArgs.Empty);
            AddSupplierFormBtn.Click += SaveButton_Click;
            guna2Button3.Click += (s, e) => CancelRequested?.Invoke(this, EventArgs.Empty);

            // Hide unused status field (not part of schema)
            label8.Visible = false;
            label9.Visible = false;
            cbxCategory.Visible = false;
        }

        public void LoadSupplier(SupplierRecord supplier)
        {
            if (supplier == null)
            {
                MessageBox.Show("Unable to load supplier details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CancelRequested?.Invoke(this, EventArgs.Empty);
                return;
            }

            currentSupplier = supplier;

            SupplierID.Text = supplier.SupplierID ?? supplier.SupplierInternalId.ToString();
            lblSupplierID.Text = supplier.SupplierID ?? "N/A";
            CompanyNameTextBoxSupplier.Text = supplier.SupplierName;
            tbxContactPerson.Text = supplier.ContactPerson;
            ContactTxtBoxSupplier.Text = supplier.ContactNumber;
            tbxEmail.Text = supplier.Email;
            LocationSupplierTextBox.Text = supplier.Address;
            SupplierDatePick.Value = DateTime.Now;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        SupplierAuditLogger.LogSupplierAudit(
                            con,
                            transaction,
                            activity: $"Viewed supplier {supplier.SupplierName}",
                            activityType: "VIEW",
                            recordId: supplier.SupplierID ?? supplier.SupplierInternalId.ToString(),
                            oldValues: SupplierAuditLogger.BuildSupplierState(supplier),
                            newValues: null);
                        transaction.Commit();
                    }
                }
            }
            catch
            {
                // Viewing audit failures should not block UI
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            UpdateSupplier();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(CompanyNameTextBoxSupplier.Text))
            {
                MessageBox.Show("Company Name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CompanyNameTextBoxSupplier.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tbxContactPerson.Text))
            {
                MessageBox.Show("Contact Person is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxContactPerson.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ContactTxtBoxSupplier.Text))
            {
                MessageBox.Show("Contact Number is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ContactTxtBoxSupplier.Focus();
                return false;
            }

            return true;
        }

        private void UpdateSupplier()
        {
            if (!ValidateInput())
                return;

            if (currentSupplier == null)
            {
                MessageBox.Show("No supplier loaded to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SupplierRecord updatedSupplier = new SupplierRecord
            {
                SupplierInternalId = currentSupplier.SupplierInternalId,
                SupplierID = currentSupplier.SupplierID,
                SupplierName = CompanyNameTextBoxSupplier.Text.Trim(),
                ContactPerson = tbxContactPerson.Text.Trim(),
                ContactNumber = ContactTxtBoxSupplier.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(tbxEmail.Text) ? null : tbxEmail.Text.Trim(),
                Address = string.IsNullOrWhiteSpace(LocationSupplierTextBox.Text) ? null : LocationSupplierTextBox.Text.Trim()
            };

            string oldValues = SupplierAuditLogger.BuildSupplierState(currentSupplier);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    con.Open();
                    transaction = con.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand(@"UPDATE Suppliers
                            SET supplier_name = @SupplierName,
                                contact_person = @ContactPerson,
                                contact_number = @ContactNumber,
                                email = @Email,
                                address = @Address
                            WHERE SupplierID = @SupplierID", con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@SupplierName", updatedSupplier.SupplierName);
                        cmd.Parameters.AddWithValue("@ContactPerson", updatedSupplier.ContactPerson);
                        cmd.Parameters.AddWithValue("@ContactNumber", updatedSupplier.ContactNumber);
                        cmd.Parameters.AddWithValue("@Email", (object)updatedSupplier.Email ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Address", (object)updatedSupplier.Address ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SupplierID", currentSupplier.SupplierID);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                        {
                            throw new InvalidOperationException("No supplier record was updated. It may have been removed.");
                        }
                    }

                    SupplierAuditLogger.LogSupplierAudit(
                        con,
                        transaction,
                        activity: $"Updated supplier {updatedSupplier.SupplierName}",
                        activityType: "UPDATE",
                        recordId: updatedSupplier.SupplierID ?? updatedSupplier.SupplierInternalId.ToString(),
                        oldValues: oldValues,
                        newValues: SupplierAuditLogger.BuildSupplierState(updatedSupplier));

                    transaction.Commit();

                    currentSupplier = updatedSupplier;

                    MessageBox.Show("Supplier updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SupplierUpdated?.Invoke(this, EventArgs.Empty);
                    CancelRequested?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show($"Error updating supplier: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
