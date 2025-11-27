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

        private void UpdateCustomer()
        {
            string customerName = tbxCompanyName.Text.Trim();
            string contactNumber = tbxContactNumber.Text.Trim();
            string address = tbxAddress.Text.Trim();
            string city = CityCombobox.SelectedItem?.ToString() ?? "";
            string province = ProvinceCombobox.SelectedItem?.ToString() ?? "";

            // Build full address
            string fullAddress = address;
            if (!string.IsNullOrEmpty(city))
                fullAddress += $", {city}";
            if (!string.IsNullOrEmpty(province))
                fullAddress += $", {province}";

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
                    updateCmd.Parameters.AddWithValue("@address", string.IsNullOrEmpty(fullAddress) ? (object)DBNull.Value : fullAddress);
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