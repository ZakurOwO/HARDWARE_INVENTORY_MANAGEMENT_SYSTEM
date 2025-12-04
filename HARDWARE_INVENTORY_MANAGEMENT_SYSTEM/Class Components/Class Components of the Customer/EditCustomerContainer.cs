using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public class EditCustomerContainer
    {
        public CustomerDetailsModel GetCustomerDetails(int customerId)
        {
            if (customerId <= 0) return null;

            using (var con = new SqlConnection(ConnectionString.DataSource))
            {
                con.Open();
                var columns = GetCustomerColumns(con);

                var selectCols = new List<string>();
                void AddIfExists(string col)
                {
                    if (columns.Contains(col))
                        selectCols.Add(col);
                }

                AddIfExists("customer_id");
                AddIfExists("customer_name");
                AddIfExists("contact_person");
                AddIfExists("contact_number");
                AddIfExists("email");
                AddIfExists("address");
                AddIfExists("city");
                AddIfExists("province");
                AddIfExists("status");

                string query = $"SELECT {string.Join(",", selectCols)} FROM Customers WHERE customer_id = @id";

                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", customerId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read()) return null;

                        var details = new CustomerDetailsModel
                        {
                            CustomerId = customerId
                        };

                        if (columns.Contains("customer_name"))
                            details.CompanyName = reader["customer_name"] as string;
                        if (columns.Contains("contact_person"))
                            details.ContactPerson = reader["contact_person"] as string;
                        if (columns.Contains("contact_number"))
                            details.ContactNumber = reader["contact_number"] as string;
                        if (columns.Contains("email"))
                            details.Email = reader["email"] as string;
                        if (columns.Contains("address"))
                            details.AddressLine = reader["address"] as string;
                        if (columns.Contains("city"))
                            details.City = reader["city"] as string;
                        if (columns.Contains("province"))
                            details.Province = reader["province"] as string;
                        if (columns.Contains("status"))
                            details.Status = reader["status"] as string;

                        return details;
                    }
                }
            }
        }

        public bool UpdateCustomer(CustomerDetailsModel details, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (details == null || details.CustomerId <= 0)
            {
                errorMessage = "Invalid customer details.";
                return false;
            }

            try
            {
                using (var con = new SqlConnection(ConnectionString.DataSource))
                {
                    con.Open();
                    var columns = GetCustomerColumns(con);

                    var sets = new List<string>();
                    void AddSet(string col, string param)
                    {
                        if (columns.Contains(col))
                            sets.Add($"{col} = {param}");
                    }

                    AddSet("customer_name", "@customer_name");
                    AddSet("contact_person", "@contact_person");
                    AddSet("contact_number", "@contact_number");
                    AddSet("email", "@email");
                    AddSet("address", "@address");
                    AddSet("city", "@city");
                    AddSet("province", "@province");
                    AddSet("status", "@status");

                    if (sets.Count == 0)
                    {
                        errorMessage = "No updatable columns found in Customers table.";
                        return false;
                    }

                    string query = $"UPDATE Customers SET {string.Join(",", sets)} WHERE customer_id = @id";

                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", details.CustomerId);
                        cmd.Parameters.AddWithValue("@customer_name", details.CompanyName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@contact_person", details.ContactPerson ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@contact_number", string.IsNullOrWhiteSpace(details.ContactNumber) ? (object)DBNull.Value : details.ContactNumber);
                        cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(details.Email) ? (object)DBNull.Value : details.Email);
                        cmd.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(details.BuildFullAddress()) ? (object)DBNull.Value : details.BuildFullAddress());
                        cmd.Parameters.AddWithValue("@city", string.IsNullOrWhiteSpace(details.City) ? (object)DBNull.Value : details.City);
                        cmd.Parameters.AddWithValue("@province", string.IsNullOrWhiteSpace(details.Province) ? (object)DBNull.Value : details.Province);
                        cmd.Parameters.AddWithValue("@status", string.IsNullOrWhiteSpace(details.Status) ? (object)DBNull.Value : details.Status);

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
        public void ShowEditCustomerForm(MainDashBoard main, int customerId, string customerName, string contactNumber, string address)
        {
            try
            {
                // Create the form
                EditCustomerForm editForm = new EditCustomerForm(customerId, customerName, contactNumber, address);

                // Connect container
                editForm.Container = this;

                // Open as overlay
                main.OpenOverlayPanel(editForm);

                // When user saves, refresh table
                editForm.CustomerUpdated += (s, e) =>
                {
                    main.refreshbtnn();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening edit customer form: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private HashSet<string> GetCustomerColumns(SqlConnection con)
        {
            var columns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (var cmd = new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Customers'", con))
            using (var reader = cmd.ExecuteReader())
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
