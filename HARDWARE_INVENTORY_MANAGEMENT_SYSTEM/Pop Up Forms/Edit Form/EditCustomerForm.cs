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
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System.Text.RegularExpressions;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public partial class EditCustomerForm : Form
    {
        // Use the shared connection string from Class_Components
        private string connectionString = ConnectionString.DataSource;

        // Dictionary to store city-province relationships
        private Dictionary<string, List<string>> cityProvinceMap;

        private int customerId;
        private string originalCustomerName;

        public event EventHandler CustomerUpdated;

        public EditCustomerForm(int customerId, string customerName, string contactNumber, string address)
        {
            InitializeComponent();

            this.customerId = customerId;
            this.originalCustomerName = customerName;

            // Connect button click events
            btnBlue.Click += btnBlue_Click;
            btnWhite.Click += btnWhite_Click;

            // Connect ComboBox events
            CityCombobox.SelectedIndexChanged += CityCombobox_SelectedIndexChanged;
            ProvinceCombobox.SelectedIndexChanged += ProvinceCombobox_SelectedIndexChanged;

            // Initialize city-province mapping
            InitializeCityProvinceMap();

            // Pre-fill the form with existing data
            tbxCompanyName.Text = customerName;
            tbxContactNumber.Text = contactNumber ?? "";
            tbxAddress.Text = address ?? "";

            closeButton1.CloseClicked += (s, e) => this.Close();
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;

            // Initialize combo boxes
            InitializeComboBoxes();
            LoadCustomerDetails();
            tbxContactNumber.Leave += (s, e) => tbxContactNumber.Text = FormatPhoneNumber(tbxContactNumber.Text);
        }

        // Initialize city-province relationships
        private void InitializeCityProvinceMap()
        {
            cityProvinceMap = new Dictionary<string, List<string>>
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

        // Initialize ComboBoxes when form loads
        private void InitializeComboBoxes()
        {
            try
            {
                // Ensure ComboBoxes are properly initialized
                if (CityCombobox != null && CityCombobox.Items.Count > 0)
                {
                    CityCombobox.SelectedIndex = -1; // No selection by default
                }

                if (ProvinceCombobox != null && ProvinceCombobox.Items.Count > 0)
                {
                    ProvinceCombobox.SelectedIndex = -1; // No selection by default
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing ComboBoxes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadCustomerDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT customer_name, contact_number, address FROM Customers WHERE customer_id = @customerId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@customerId", customerId);

                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tbxCompanyName.Text = reader["customer_name"]?.ToString() ?? tbxCompanyName.Text;
                            tbxContactNumber.Text = reader["contact_number"]?.ToString() ?? tbxContactNumber.Text;
                            tbxAddress.Text = reader["address"]?.ToString() ?? tbxAddress.Text;
                            originalCustomerName = tbxCompanyName.Text;
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load customer details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool ValidateInputs(out string customerName, out string formattedContactNumber, out string fullAddress)
        {
            customerName = tbxCompanyName.Text.Trim();
            string contactNumber = tbxContactNumber.Text.Trim();
            string address = tbxAddress.Text.Trim();
            string city = CityCombobox.SelectedItem?.ToString() ?? "";
            string province = ProvinceCombobox.SelectedItem?.ToString() ?? "";

            fullAddress = BuildFullAddress(address, city, province);
            formattedContactNumber = FormatPhoneNumber(contactNumber);

            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Please enter the customer name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxCompanyName.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(contactNumber) && !IsValidPhoneNumber(contactNumber))
            {
                MessageBox.Show("Please enter a valid contact number (10-11 digits).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxContactNumber.Focus();
                return false;
            }

            return true;
        }

        private string BuildFullAddress(string address, string city, string province)
        {
            string fullAddress = address;
            if (!string.IsNullOrEmpty(city))
                fullAddress += $", {city}";
            if (!string.IsNullOrEmpty(province))
                fullAddress += $", {province}";

            return fullAddress;
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
            if (cleanPhone.Length == 12 && cleanPhone.StartsWith("63"))
            {
                return $"+{cleanPhone}";
            }

            return phone.Trim();
        }

        private void UpdateCustomer()
        {
            if (!ValidateInputs(out string customerName, out string formattedContactNumber, out string fullAddress))
                return;

            return phone.Trim();
        }

        private void UpdateCustomer()
        {
            if (!ValidateInputs(out string customerName, out string formattedContactNumber, out string fullAddress))
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    if (customerName != originalCustomerName)
                    {
                        string checkQuery = "SELECT COUNT(*) FROM Customers WHERE customer_name = @customerName AND customer_id != @customerId";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                        checkCmd.Parameters.AddWithValue("@customerName", customerName);
                        checkCmd.Parameters.AddWithValue("@customerId", customerId);

                        int existingCount = (int)checkCmd.ExecuteScalar();

                        if (existingCount > 0)
                        {
                            MessageBox.Show("A customer with this name already exists. Please use a different name.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string updateQuery = @"
                        UPDATE Customers
                        SET customer_name = @customerName,
                            contact_number = @contactNumber,
                            address = @address
                        WHERE customer_id = @customerId";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@customerName", customerName);
                    updateCmd.Parameters.AddWithValue("@contactNumber", string.IsNullOrEmpty(formattedContactNumber) ? (object)DBNull.Value : formattedContactNumber);
                    updateCmd.Parameters.AddWithValue("@address", string.IsNullOrEmpty(fullAddress) ? (object)DBNull.Value : fullAddress);
                    updateCmd.Parameters.AddWithValue("@customerId", customerId);

                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer updated successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
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

        // ComboBox event handlers
        private void CityCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Auto-detect province based on city selection
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
            // Filter cities based on selected province
            if (ProvinceCombobox.SelectedItem != null)
            {
                string selectedProvince = ProvinceCombobox.SelectedItem.ToString();
                FilterCitiesByProvince(selectedProvince);
            }
        }

        // Detect province from city selection
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

        // Filter cities by selected province
        private void FilterCitiesByProvince(string province)
        {
            if (cityProvinceMap.ContainsKey(province))
            {
                CityCombobox.Items.Clear();
                CityCombobox.Items.AddRange(cityProvinceMap[province].ToArray());
            }
            else
            {
                // If province not in map, show all cities
                ResetCityComboBox();
            }
        }

        // Reset city combo box to show all cities
        private void ResetCityComboBox()
        {
            CityCombobox.Items.Clear();
            CityCombobox.Items.AddRange(GetAllCities().ToArray());
        }

        private List<string> GetAllCities()
        {
            var cities = new List<string>();
            foreach (var province in cityProvinceMap.Keys)
            {
                cities.AddRange(cityProvinceMap[province]);
            }
            return cities;
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
        private void tbxEmail_TextChanged(object sender, EventArgs e) { }
        private void tbxContactPerson_TextChanged(object sender, EventArgs e) { }
    }
}