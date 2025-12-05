using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public partial class EditCustomerForm : Form
    {
        private readonly Dictionary<string, List<string>> cityProvinceMap;
        private readonly int customerId;
        private string originalCustomerName;

        public event EventHandler CustomerUpdated;

        // Keep this type because your project uses it, but we will not REQUIRE it to have methods
        public EditCustomerContainer Container { get; set; }

        public EditCustomerForm(int customerId, string customerName, string contactNumber, string address)
        {
            InitializeComponent();

            this.customerId = customerId;
            originalCustomerName = customerName ?? string.Empty;

            // UI hooks
            btnBlue.Click += btnBlue_Click;
            btnWhite.Click += btnWhite_Click;

            CityCombobox.SelectedIndexChanged += CityCombobox_SelectedIndexChanged;
            ProvinceCombobox.SelectedIndexChanged += ProvinceCombobox_SelectedIndexChanged;

            tbxContactNumber.Leave += (s, e) => tbxContactNumber.Text = FormatPhoneNumber(tbxContactNumber.Text);

            closeButton1.CloseClicked += (s, e) => Close();
            TopLevel = false;
            FormBorderStyle = FormBorderStyle.None;

            // Location data
            cityProvinceMap = BuildCityProvinceMap();
            InitializeComboBoxes();

            // Initial values from caller (fast display)
            tbxCompanyName.Text = customerName ?? string.Empty;
            tbxContactNumber.Text = contactNumber ?? string.Empty;
            tbxAddress.Text = address ?? string.Empty;

            // Load full record from DB
            LoadCustomerDetails();
        }

        #region Models (local)
        // Local model so this file compiles even if CustomerDetailsModel isn't present elsewhere.
        // If you already have CustomerDetailsModel in your project, you can delete this class and use yours.
        private sealed class CustomerDetailsModel
        {
            public int CustomerId { get; set; }
            public string CompanyName { get; set; }
            public string ContactPerson { get; set; }
            public string ContactNumber { get; set; }
            public string Email { get; set; }
            public string AddressLine { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
            public string Status { get; set; }
        }
        #endregion

        #region Province/City Setup
        private Dictionary<string, List<string>> BuildCityProvinceMap()
        {
            return new Dictionary<string, List<string>>
            {
                ["NCR"] = new List<string>
                {
                    "Caloocan", "Las Piñas", "Makati", "Malabon", "Mandaluyong",
                    "Manila", "Marikina", "Muntinlupa", "Navotas", "Parañaque",
                    "Pasay", "Pasig", "Quezon City", "San Juan", "Taguig",
                    "Valenzuela", "Pateros"
                },
                ["Bulacan"] = new List<string>
                {
                    "Angat", "Balagtas", "Baliuag", "Bocaue", "Bulakan",
                    "Bustos", "Calumpit", "Doña Remedios Trinidad", "Guiguinto",
                    "Hagonoy", "Malolos City", "Marilao", "Meycauayan City",
                    "Norzagaray", "Obando", "Pandi", "Paombong", "Plaridel",
                    "Pulilan", "San Ildefonso", "San Jose del Monte City",
                    "San Miguel", "San Rafael", "Santa Maria"
                },
                ["Cavite"] = new List<string>
                {
                    "Alfonso", "Amadeo", "Bacoor", "Carmona", "Cavite City",
                    "Dasmariñas", "General Emilio Aguinaldo", "General Mariano Alvarez",
                    "General Trias", "Imus", "Indang", "Kawit", "Magallanes",
                    "Maragondon", "Mendez", "Naic", "Noveleta", "Rosario",
                    "Silang", "Tagaytay", "Tanza", "Ternate", "Trece Martires"
                },
                ["Laguna"] = new List<string>
                {
                    "Alaminos", "Bay", "Biñan", "Cabuyao", "Calamba",
                    "Calauan", "Cavinti", "Famy", "Kalayaan", "Liliw",
                    "Los Baños", "Luisiana", "Lumban", "Mabitac", "Magdalena",
                    "Majayjay", "Nagcarlan", "Paete", "Pagsanjan", "Pakil",
                    "Pangil", "Pila", "Rizal", "San Pablo", "San Pedro",
                    "Santa Cruz", "Santa Maria", "Santa Rosa", "Siniloan", "Victoria"
                },
                ["Rizal"] = new List<string>
                {
                    "Antipolo City", "Angono", "Baras", "Binangonan", "Cainta",
                    "Cardona", "Jalajala", "Morong", "Pililla", "Rodriguez",
                    "San Mateo", "Tanay", "Taytay", "Teresa"
                }
            };
        }

        private void InitializeComboBoxes()
        {
            try
            {
                ProvinceCombobox.Items.Clear();
                CityCombobox.Items.Clear();

                ProvinceCombobox.Items.AddRange(cityProvinceMap.Keys.ToArray());
                ProvinceCombobox.SelectedIndex = -1;

                ResetCityComboBox();
                CityCombobox.SelectedIndex = -1;

                cbxStatus.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing location fields: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ResetCityComboBox()
        {
            CityCombobox.Items.Clear();
            CityCombobox.Items.AddRange(cityProvinceMap.Values.SelectMany(c => c).ToArray());
        }

        private void FilterCitiesByProvince(string province)
        {
            if (!string.IsNullOrWhiteSpace(province) && cityProvinceMap.ContainsKey(province))
            {
                CityCombobox.Items.Clear();
                CityCombobox.Items.AddRange(cityProvinceMap[province].ToArray());
            }
            else
            {
                ResetCityComboBox();
            }
            CityCombobox.SelectedIndex = -1;
        }

        private string DetectProvinceFromCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city)) return null;

            foreach (var province in cityProvinceMap.Keys)
            {
                if (cityProvinceMap[province].Contains(city))
                    return province;
            }
            return null;
        }
        #endregion

        #region Load/Save Customer (Safe fallback DB)
        private void LoadCustomerDetails()
        {
            try
            {
                // 1) Try container (if it has the method) WITHOUT compile-time dependency
                var fromContainer = TryGetCustomerDetailsFromContainer(customerId);
                if (fromContainer != null)
                {
                    ApplyDetailsToForm(fromContainer);
                    originalCustomerName = fromContainer.CompanyName ?? originalCustomerName;
                    return;
                }

                // 2) Fallback to DB direct load
                var dbDetails = GetCustomerDetailsFromDb(customerId);
                if (dbDetails != null)
                {
                    ApplyDetailsToForm(dbDetails);
                    originalCustomerName = dbDetails.CompanyName ?? originalCustomerName;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private CustomerDetailsModel TryGetCustomerDetailsFromContainer(int id)
        {
            try
            {
                if (Container == null) return null;

                // dynamic lets you call it if it exists, but doesn't break compilation if it doesn't
                dynamic d = Container;
                var details = d.GetCustomerDetails(id);

                if (details == null) return null;

                return new CustomerDetailsModel
                {
                    CustomerId = id,
                    CompanyName = details.CompanyName,
                    ContactPerson = details.ContactPerson,
                    ContactNumber = details.ContactNumber,
                    Email = details.Email,
                    AddressLine = details.AddressLine,
                    City = details.City,
                    Province = details.Province,
                    Status = details.Status
                };
            }
            catch
            {
                return null;
            }
        }

        private bool TryUpdateCustomerViaContainer(CustomerDetailsModel details, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                if (Container == null) return false;

                dynamic d = Container;
                bool ok = d.UpdateCustomer(details, out errorMessage);
                return ok;
            }
            catch
            {
                return false;
            }
        }

        // Adjust column names here to match your Customers table schema:
        // customer_id, customer_name, contact_person, contact_number, email, address, city, province, status
        private CustomerDetailsModel GetCustomerDetailsFromDb(int id)
        {
            using (var con = new SqlConnection(ConnectionString.DataSource))
            using (var cmd = new SqlCommand(@"
                SELECT TOP 1
                    customer_id,
                    customer_name,
                    contact_person,
                    contact_number,
                    email,
                    address,
                    city,
                    province,
                    status
                FROM Customers
                WHERE customer_id = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;

                    return new CustomerDetailsModel
                    {
                        CustomerId = Convert.ToInt32(r["customer_id"]),
                        CompanyName = r["customer_name"]?.ToString(),
                        ContactPerson = r["contact_person"]?.ToString(),
                        ContactNumber = r["contact_number"]?.ToString(),
                        Email = r["email"]?.ToString(),
                        AddressLine = r["address"]?.ToString(),
                        City = r["city"]?.ToString(),
                        Province = r["province"]?.ToString(),
                        Status = r["status"]?.ToString()
                    };
                }
            }
        }

        private bool UpdateCustomerInDb(CustomerDetailsModel details, out string errorMessage)
        {
            errorMessage = null;

            try
            {
                using (var con = new SqlConnection(ConnectionString.DataSource))
                using (var cmd = new SqlCommand(@"
                    UPDATE Customers
                    SET
                        customer_name = @name,
                        contact_person = @person,
                        contact_number = @contact,
                        email = @email,
                        address = @address,
                        city = @city,
                        province = @province,
                        status = @status
                    WHERE customer_id = @id;", con))
                {
                    cmd.Parameters.AddWithValue("@id", details.CustomerId);
                    cmd.Parameters.AddWithValue("@name", details.CompanyName);
                    cmd.Parameters.AddWithValue("@person", details.ContactPerson);
                    cmd.Parameters.AddWithValue("@contact", details.ContactNumber);
                    cmd.Parameters.AddWithValue("@email", (object)details.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@address", details.AddressLine);
                    cmd.Parameters.AddWithValue("@city", details.City);
                    cmd.Parameters.AddWithValue("@province", details.Province);
                    cmd.Parameters.AddWithValue("@status", details.Status);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        private void ApplyDetailsToForm(CustomerDetailsModel details)
        {
            tbxCompanyName.Text = details.CompanyName ?? tbxCompanyName.Text;
            tbxContactPerson.Text = details.ContactPerson ?? string.Empty;
            tbxContactNumber.Text = details.ContactNumber ?? string.Empty;
            tbxEmail.Text = details.Email ?? string.Empty;
            tbxAddress.Text = details.AddressLine ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(details.Province) && ProvinceCombobox.Items.Contains(details.Province))
            {
                ProvinceCombobox.SelectedItem = details.Province;
                FilterCitiesByProvince(details.Province);
            }

            if (!string.IsNullOrWhiteSpace(details.City) && CityCombobox.Items.Contains(details.City))
            {
                CityCombobox.SelectedItem = details.City;
            }

            if (!string.IsNullOrWhiteSpace(details.Status) && cbxStatus.Items.Contains(details.Status))
            {
                cbxStatus.SelectedItem = details.Status;
            }
        }
        #endregion

        #region Validation / Formatting
        private bool ValidateInputs(out CustomerDetailsModel details)
        {
            details = new CustomerDetailsModel
            {
                CustomerId = customerId,
                CompanyName = tbxCompanyName.Text.Trim(),
                ContactPerson = tbxContactPerson.Text.Trim(),
                ContactNumber = tbxContactNumber.Text.Trim(),
                Email = tbxEmail.Text.Trim(),
                AddressLine = tbxAddress.Text.Trim(),
                City = CityCombobox.SelectedItem?.ToString(),
                Province = ProvinceCombobox.SelectedItem?.ToString(),
                Status = cbxStatus.SelectedItem?.ToString()
            };

            if (string.IsNullOrWhiteSpace(details.CompanyName))
            {
                MessageBox.Show("Please enter the customer name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxCompanyName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(details.ContactPerson))
            {
                MessageBox.Show("Please enter the contact person.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxContactPerson.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(details.ContactNumber))
            {
                MessageBox.Show("Please enter the contact number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxContactNumber.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(details.Email) && !IsValidEmail(details.Email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxEmail.Focus();
                return false;
            }

            if (!IsValidPhoneNumber(details.ContactNumber))
            {
                MessageBox.Show("Please enter a valid contact number (10-11 digits).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxContactNumber.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(details.AddressLine))
            {
                MessageBox.Show("Please enter the address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxAddress.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(details.Province))
            {
                MessageBox.Show("Please select a province.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ProvinceCombobox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(details.City))
            {
                MessageBox.Show("Please select a city/municipality.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CityCombobox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(details.Status))
            {
                MessageBox.Show("Please select a status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxStatus.Focus();
                return false;
            }

            details.ContactNumber = FormatPhoneNumber(details.ContactNumber);
            return true;
        }

        private bool IsValidPhoneNumber(string phone)
        {
            string cleanPhone = Regex.Replace(phone ?? "", @"[^\d]", "");
            return cleanPhone.Length >= 10 && cleanPhone.Length <= 11;
        }

        private string FormatPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return string.Empty;

            string cleanPhone = Regex.Replace(phone, @"[^\d]", "");

            // 11 digits starting with 0 => +63 + last 10 digits
            if (cleanPhone.Length == 11 && cleanPhone.StartsWith("0"))
                return $"+63{cleanPhone.Substring(1)}";

            // 10 digits (e.g. 9xxxxxxxxx) => +63 + same
            if (cleanPhone.Length == 10)
                return $"+63{cleanPhone}";

            // 12 digits starting with 63 => +63...
            if (cleanPhone.Length == 12 && cleanPhone.StartsWith("63"))
                return $"+{cleanPhone}";

            return phone.Trim();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Duplicate Check / Update
        private bool CheckDuplicateName(string customerName)
        {
            try
            {
                if (!string.Equals(customerName, originalCustomerName, StringComparison.OrdinalIgnoreCase))
                {
                    using (var con = new SqlConnection(ConnectionString.DataSource))
                    using (var cmd = new SqlCommand(
                               "SELECT COUNT(*) FROM Customers WHERE customer_name = @name AND customer_id <> @id", con))
                    {
                        cmd.Parameters.AddWithValue("@name", customerName);
                        cmd.Parameters.AddWithValue("@id", customerId);

                        con.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch
            {
                // handled by caller
            }
            return false;
        }

        private void UpdateCustomer()
        {
            if (!ValidateInputs(out CustomerDetailsModel details))
                return;

            if (CheckDuplicateName(details.CompanyName))
            {
                MessageBox.Show("A customer with this name already exists. Please use a different name.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1) Try container if available
            if (TryUpdateCustomerViaContainer(details, out string containerError))
            {
                MessageBox.Show("Customer updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CustomerUpdated?.Invoke(this, EventArgs.Empty);
                Close();
                return;
            }

            // 2) Fallback to DB update
            bool ok = UpdateCustomerInDb(details, out string dbError);
            if (ok)
            {
                MessageBox.Show("Customer updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CustomerUpdated?.Invoke(this, EventArgs.Empty);
                Close();
                return;
            }

            MessageBox.Show($"Error updating customer: {containerError ?? dbError ?? "Unknown error"}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region Events
        private void CityCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CityCombobox.SelectedItem == null) return;

            string selectedCity = CityCombobox.SelectedItem.ToString();
            string detectedProvince = DetectProvinceFromCity(selectedCity);

            if (!string.IsNullOrEmpty(detectedProvince) && ProvinceCombobox.Items.Contains(detectedProvince))
            {
                ProvinceCombobox.SelectedItem = detectedProvince;
            }
        }

        private void ProvinceCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProvinceCombobox.SelectedItem == null) return;

            string selectedProvince = ProvinceCombobox.SelectedItem.ToString();
            FilterCitiesByProvince(selectedProvince);
        }

        private void btnBlue_Click(object sender, EventArgs e) => UpdateCustomer();
        private void btnWhite_Click(object sender, EventArgs e) => Close();

        // Designer-generated empty handlers (keep if designer references them)
        private void tbxCompanyName_TextChanged(object sender, EventArgs e) { }
        private void tbxContactNumber_TextChanged(object sender, EventArgs e) { }
        private void tbxAddress_TextChanged(object sender, EventArgs e) { }
        private void tbxEmail_TextChanged(object sender, EventArgs e) { }
        private void tbxContactPerson_TextChanged(object sender, EventArgs e) { }
        private void closeButton1_Load(object sender, EventArgs e) { }
        #endregion
    }
}
