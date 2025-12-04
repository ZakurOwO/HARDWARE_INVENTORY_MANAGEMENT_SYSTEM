using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public partial class EditCustomerForm : Form
    {
        private readonly Dictionary<string, List<string>> cityProvinceMap;
        private int customerId;
        private string originalCustomerName;

        public event EventHandler CustomerUpdated;
        public EditCustomerContainer Container { get; set; }

        public EditCustomerForm(int customerId, string customerName, string contactNumber, string address)
        {
            InitializeComponent();

            this.customerId = customerId;
            originalCustomerName = customerName;

            btnBlue.Click += btnBlue_Click;
            btnWhite.Click += btnWhite_Click;
            CityCombobox.SelectedIndexChanged += CityCombobox_SelectedIndexChanged;
            ProvinceCombobox.SelectedIndexChanged += ProvinceCombobox_SelectedIndexChanged;
            tbxContactNumber.Leave += (s, e) => tbxContactNumber.Text = FormatPhoneNumber(tbxContactNumber.Text);

            closeButton1.CloseClicked += (s, e) => Close();
            TopLevel = false;
            FormBorderStyle = FormBorderStyle.None;

            cityProvinceMap = BuildCityProvinceMap();
            InitializeComboBoxes();

            tbxCompanyName.Text = customerName;
            tbxContactNumber.Text = contactNumber ?? string.Empty;
            tbxAddress.Text = address ?? string.Empty;
            LoadCustomerDetails();
        }

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

        private void LoadCustomerDetails()
        {
            try
            {
                var details = Container?.GetCustomerDetails(customerId);
                if (details != null)
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

                    originalCustomerName = details.CompanyName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load customer details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

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
            string cleanPhone = Regex.Replace(phone, @"[^\d]", "");
            return cleanPhone.Length >= 10 && cleanPhone.Length <= 11;
        }

        private string FormatPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return string.Empty;

            string cleanPhone = Regex.Replace(phone, @"[^\d]", "");

            if (cleanPhone.Length == 10)
            {
                return $"+63{cleanPhone.Substring(1)}";
            }
            if (cleanPhone.Length == 11 && cleanPhone.StartsWith("0"))
            {
                return $"+63{cleanPhone.Substring(1)}";
            }
            if (cleanPhone.Length == 12 && cleanPhone.StartsWith("63"))
            {
                return $"+{cleanPhone}";
            }

            return phone.Trim();
        }

        private bool CheckDuplicateName(string customerName)
        {
            try
            {
                if (!string.Equals(customerName, originalCustomerName, StringComparison.OrdinalIgnoreCase))
                {
                    using (var con = new System.Data.SqlClient.SqlConnection(ConnectionString.DataSource))
                    using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Customers WHERE customer_name = @name AND customer_id <> @id", con))
                    {
                        cmd.Parameters.AddWithValue("@name", customerName);
                        cmd.Parameters.AddWithValue("@id", customerId);
                        con.Open();
                        int count = (int)cmd.ExecuteScalar();
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

            if (Container == null)
            {
                MessageBox.Show("Unable to update: container not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckDuplicateName(details.CompanyName))
            {
                MessageBox.Show("A customer with this name already exists. Please use a different name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Container.UpdateCustomer(details, out string errorMessage))
            {
                MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CustomerUpdated?.Invoke(this, EventArgs.Empty);
                Close();
            }
            else
            {
                MessageBox.Show($"Error updating customer: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CityCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CityCombobox.SelectedItem != null)
            {
                string selectedCity = CityCombobox.SelectedItem.ToString();
                string detectedProvince = DetectProvinceFromCity(selectedCity);

                if (!string.IsNullOrEmpty(detectedProvince))
                {
                    ProvinceCombobox.SelectedItem = detectedProvince;
                }
            }
        }

        private void ProvinceCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProvinceCombobox.SelectedItem != null)
            {
                string selectedProvince = ProvinceCombobox.SelectedItem.ToString();
                FilterCitiesByProvince(selectedProvince);
            }
        }

        private string DetectProvinceFromCity(string city)
        {
            foreach (var province in cityProvinceMap.Keys)
            {
                if (cityProvinceMap[province].Contains(city))
                {
                    return province;
                }
            }
            return null;
        }

        private void FilterCitiesByProvince(string province)
        {
            if (cityProvinceMap.ContainsKey(province))
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

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            UpdateCustomer();
        }

        private void btnWhite_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbxCompanyName_TextChanged(object sender, EventArgs e) { }
        private void tbxContactNumber_TextChanged(object sender, EventArgs e) { }
        private void tbxAddress_TextChanged(object sender, EventArgs e) { }
        private void tbxEmail_TextChanged(object sender, EventArgs e) { }
        private void tbxContactPerson_TextChanged(object sender, EventArgs e) { }
        private void closeButton1_Load(object sender, EventArgs e) { }
    }
}
