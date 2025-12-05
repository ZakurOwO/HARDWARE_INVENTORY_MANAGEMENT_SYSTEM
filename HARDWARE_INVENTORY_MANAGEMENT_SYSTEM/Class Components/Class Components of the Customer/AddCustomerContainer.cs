using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    /// <summary>
    /// Hosts the AddCustomerForm and handles persistence + cleanup.
    /// </summary>
    public class AddCustomerContainer
    {
        private Panel scrollContainer;
        private AddCustomerForm addCustomerForm;
        private MainDashBoard mainForm;

        public AddCustomerForm ShowAddCustomerForm(MainDashBoard main)
        {
            try
            {
                mainForm = main;

                addCustomerForm = new AddCustomerForm(this);
                addCustomerForm.TopLevel = false;
                addCustomerForm.FormBorderStyle = FormBorderStyle.None;
                addCustomerForm.Dock = DockStyle.Fill;
                addCustomerForm.CustomerAdded += AddCustomerForm_CustomerAdded;

                scrollContainer = new Panel
                {
                    Size = new Size(583, 505),
                    Location = new Point((main.Width - 583) / 2, (main.Height - 505) / 2),
                    BorderStyle = BorderStyle.FixedSingle,
                    AutoScroll = true
                };

                scrollContainer.Controls.Add(addCustomerForm);
                addCustomerForm.Dock = DockStyle.Fill;

                addCustomerForm.Show();
                addCustomerForm.BringToFront();

                if (mainForm?.pcbBlurOverlay != null)
                {
                    mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.CustomerOvelay;
                    mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
                    mainForm.pcbBlurOverlay.Visible = true;
                    mainForm.pcbBlurOverlay.BringToFront();
                }

                mainForm?.Controls.Add(scrollContainer);
                scrollContainer.BringToFront();

                addCustomerForm.FormClosed += OnAddCustomerFormClosed;

                return addCustomerForm;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing customer form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Inserts a customer record using available schema columns.
        /// </summary>
        public bool AddCustomer(CustomerDetailsModel customer, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (customer == null)
            {
                errorMessage = "Missing customer details.";
                return false;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
                {
                    con.Open();
                    HashSet<string> columns = GetCustomerColumns(con);

                    // Ensure required schema exists
                    string[] requiredColumns = { "customer_name", "contact_number", "address" };
                    if (!requiredColumns.All(columns.Contains))
                    {
                        errorMessage = "Customers table is missing required columns.";
                        return false;
                    }

                    List<string> columnNames = new List<string> { "customer_name", "contact_number", "address" };
                    List<string> parameterNames = new List<string> { "@customer_name", "@contact_number", "@address" };

                    void AddOptional(string columnName, string parameterName)
                    {
                        if (columns.Contains(columnName))
                        {
                            columnNames.Add(columnName);
                            parameterNames.Add(parameterName);
                        }
                    }

                    AddOptional("contact_person", "@contact_person");
                    AddOptional("email", "@email");
                    AddOptional("city", "@city");
                    AddOptional("province", "@province");
                    AddOptional("status", "@status");

                    string query = $"INSERT INTO Customers ({string.Join(",", columnNames)}) VALUES ({string.Join(",", parameterNames)})";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@customer_name", customer.CompanyName);
                        cmd.Parameters.AddWithValue("@contact_number", string.IsNullOrWhiteSpace(customer.ContactNumber) ? (object)DBNull.Value : customer.ContactNumber);
                        cmd.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(customer.BuildFullAddress()) ? (object)DBNull.Value : customer.BuildFullAddress());

                        if (columns.Contains("contact_person"))
                            cmd.Parameters.AddWithValue("@contact_person", string.IsNullOrWhiteSpace(customer.ContactPerson) ? (object)DBNull.Value : customer.ContactPerson);
                        if (columns.Contains("email"))
                            cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(customer.Email) ? (object)DBNull.Value : customer.Email);
                        if (columns.Contains("city"))
                            cmd.Parameters.AddWithValue("@city", string.IsNullOrWhiteSpace(customer.City) ? (object)DBNull.Value : customer.City);
                        if (columns.Contains("province"))
                            cmd.Parameters.AddWithValue("@province", string.IsNullOrWhiteSpace(customer.Province) ? (object)DBNull.Value : customer.Province);
                        if (columns.Contains("status"))
                            cmd.Parameters.AddWithValue("@status", string.IsNullOrWhiteSpace(customer.Status) ? (object)DBNull.Value : customer.Status);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public void CloseAddCustomerForm()
        {
            if (mainForm != null && mainForm.pcbBlurOverlay != null)
            {
                mainForm.pcbBlurOverlay.Visible = false;
            }

            if (addCustomerForm != null)
            {
                addCustomerForm.CustomerAdded -= AddCustomerForm_CustomerAdded;
                addCustomerForm.FormClosed -= OnAddCustomerFormClosed;
                if (!addCustomerForm.IsDisposed)
                {
                    addCustomerForm.Dispose();
                }
                addCustomerForm = null;
            }

            if (scrollContainer != null)
            {
                scrollContainer.Controls.Clear();
                scrollContainer.Parent?.Controls.Remove(scrollContainer);
                scrollContainer.Dispose();
                scrollContainer = null;
            }
        }

        private void AddCustomerForm_CustomerAdded(object sender, EventArgs e)
        {
            RefreshCustomerList();
        }

        private void OnAddCustomerFormClosed(object sender, FormClosedEventArgs e)
        {
            CloseAddCustomerForm();
        }

        private void RefreshCustomerList()
        {
            var customerMainPage = FindControlRecursive<CustomerMainPage>(mainForm);
            customerMainPage?.RefreshCustomerList();
        }

        private T FindControlRecursive<T>(Control parent) where T : Control
        {
            if (parent == null) return null;

            foreach (Control control in parent.Controls)
            {
                if (control is T found)
                    return found;

                var child = FindControlRecursive<T>(control);
                if (child != null)
                    return child;
            }
            return null;
        }

        private HashSet<string> GetCustomerColumns(SqlConnection con)
        {
            HashSet<string> columns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (SqlCommand cmd = new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Customers'", con))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    columns.Add(reader.GetString(0));
                }
            }
            return columns;
        }
    }
}
