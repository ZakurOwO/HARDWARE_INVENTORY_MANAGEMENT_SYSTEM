using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public class EditCustomerContainer
    {
        private Panel scrollContainer;
        private EditCustomerForm editCustomerForm;
        private MainDashBoard mainForm;

        public void ShowEditCustomerForm(MainDashBoard main, int customerId, string customerName, string contactNumber, string address)
        {
            mainForm = main;

            editCustomerForm = new EditCustomerForm(customerId, customerName, contactNumber, address)
            {
                Container = this
            };
            editCustomerForm.TopLevel = false;
            editCustomerForm.FormBorderStyle = FormBorderStyle.None;
            editCustomerForm.Dock = DockStyle.None;

            scrollContainer = new Panel
            {
                Size = new Size(583, 505),
                Location = new Point(472, 100),
                BorderStyle = BorderStyle.FixedSingle
            };

            scrollContainer.Controls.Add(editCustomerForm);
            editCustomerForm.Size = new Size(583, 813);
            editCustomerForm.Location = new Point(0, 0);
            editCustomerForm.Show();

            if (mainForm?.pcbBlurOverlay != null)
            {
                mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.CustomerOvelay;
                mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
                mainForm.pcbBlurOverlay.Visible = true;
                mainForm.pcbBlurOverlay.BringToFront();
            }

            mainForm?.Controls.Add(scrollContainer);
            scrollContainer.BringToFront();

            editCustomerForm.FormClosed += (s, e) =>
            {
                CloseEditCustomerForm();
                RefreshCustomerList();
            };

            editCustomerForm.CustomerUpdated += (s, e) =>
            {
                RefreshCustomerList();
            };
        }

        public bool UpdateCustomer(CustomerDetailsModel customer, out string errorMessage)
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

                    string[] requiredColumns = { "customer_id", "customer_name", "contact_number", "address" };
                    if (!requiredColumns.All(columns.Contains))
                    {
                        errorMessage = "Customers table is missing required columns.";
                        return false;
                    }

                    List<string> setClauses = new List<string>
                    {
                        "customer_name = @customer_name",
                        "contact_number = @contact_number",
                        "address = @address"
                    };

                    void AddOptional(string columnName)
                    {
                        if (columns.Contains(columnName))
                        {
                            setClauses.Add($"{columnName} = @{columnName}");
                        }
                    }

                    AddOptional("contact_person");
                    AddOptional("email");
                    AddOptional("city");
                    AddOptional("province");
                    AddOptional("status");

                    string query = $"UPDATE Customers SET {string.Join(",", setClauses)} WHERE customer_id = @customer_id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@customer_name", customer.CompanyName);
                        cmd.Parameters.AddWithValue("@contact_number", string.IsNullOrWhiteSpace(customer.ContactNumber) ? (object)DBNull.Value : customer.ContactNumber);
                        cmd.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(customer.BuildFullAddress()) ? (object)DBNull.Value : customer.BuildFullAddress());
                        cmd.Parameters.AddWithValue("@customer_id", customer.CustomerId);

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

        public CustomerDetailsModel GetCustomerDetails(int customerId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE customer_id = @customerId", con))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new CustomerDetailsModel
                                {
                                    CustomerId = customerId,
                                    CompanyName = reader["customer_name"]?.ToString(),
                                    ContactPerson = SafeRead(reader, "contact_person"),
                                    ContactNumber = reader["contact_number"]?.ToString(),
                                    Email = SafeRead(reader, "email"),
                                    AddressLine = reader["address"]?.ToString(),
                                    City = SafeRead(reader, "city"),
                                    Province = SafeRead(reader, "province"),
                                    Status = SafeRead(reader, "status")
                                };
                            }
                        }
                    }
                }
            }
            catch
            {
                // handled upstream with user-friendly messaging
            }
            return null;
        }

        public void CloseEditCustomerForm()
        {
            if (mainForm != null)
                mainForm.pcbBlurOverlay.Visible = false;

            if (editCustomerForm != null)
            {
                editCustomerForm.Dispose();
                editCustomerForm = null;
            }

            if (scrollContainer != null)
            {
                scrollContainer.Controls.Clear();
                scrollContainer.Parent?.Controls.Remove(scrollContainer);
                scrollContainer.Dispose();
                scrollContainer = null;
            }
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

        private string SafeRead(SqlDataReader reader, string column)
        {
            try
            {
                return reader[column]?.ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}
