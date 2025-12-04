using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public partial class AddCustomerForm : Form
    {
        // Use the shared connection string from Class_Components
        private string connectionString = ConnectionString.DataSource;

        // Dictionary to store city-province relationships
        private Dictionary<string, List<string>> cityProvinceMap;

        public event EventHandler CustomerAdded;

        public AddCustomerForm()
        {
            InitializeComponent();

            // Connect the button click events in constructor
            btnBlue.Click += btnBlue_Click;
            btnWhite.Click += btnWhite_Click;

            // Connect ComboBox events
            CityCombobox.SelectedIndexChanged += CityCombobox_SelectedIndexChanged;
            ProvinceCombobox.SelectedIndexChanged += ProvinceCombobox_SelectedIndexChanged;

            // Also connect the close button if it exists
            if (closeButton1 != null)
            {
                closeButton1.CloseClicked += (s, e) => this.Close();
            }

            // Initialize city-province mapping
            InitializeCityProvinceMap();
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

        // Method to add a customer
        private void AddCustomer()
        {
            string companyName = tbxCompanyName.Text.Trim();
            string contactPerson = tbxContactPerson.Text.Trim();
            string contactNumber = tbxContactNumber.Text.Trim();
            string email = tbxEmail.Text.Trim();
            string address = tbxAddress.Text.Trim();
            string city = CityCombobox.SelectedItem?.ToString() ?? "";
            string province = ProvinceCombobox.SelectedItem?.ToString() ?? "";

            // Build full address
            string fullAddress = address;
            if (!string.IsNullOrEmpty(city))
                fullAddress += $", {city}";
            if (!string.IsNullOrEmpty(province))
                fullAddress += $", {province}";

            // Validation
            if (string.IsNullOrEmpty(companyName))
            {
                MessageBox.Show("Please enter the company name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxCompanyName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(contactPerson))
            {
                MessageBox.Show("Please enter the contact person.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxContactPerson.Focus();
                return;
            }

            // Email validation (optional field)
            if (!string.IsNullOrEmpty(email) && !IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxEmail.Focus();
                return;
            }

            // Phone number validation (optional field)
            if (!string.IsNullOrEmpty(contactNumber) && !IsValidPhoneNumber(contactNumber))
            {
                MessageBox.Show("Please enter a valid contact number (10-11 digits).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxContactNumber.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Customers (customer_name, contact_number, address) 
                                   VALUES (@name, @contact, @address)";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        // Use company name as customer_name in database
                        command.Parameters.AddWithValue("@name", companyName);
                        command.Parameters.AddWithValue("@contact", string.IsNullOrEmpty(contactNumber) ? (object)DBNull.Value : contactNumber);
                        command.Parameters.AddWithValue("@address", string.IsNullOrEmpty(fullAddress) ? (object)DBNull.Value : fullAddress);

                        con.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer added successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CustomerAdded?.Invoke(this, EventArgs.Empty);
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add customer.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                string errorMessage = $"Database error: {sqlEx.Message}";

                if (sqlEx.Number == 18456) // Login failed
                {
                    errorMessage += "\n\nPlease check your SQL Server authentication.";
                }
                else if (sqlEx.Message.Contains("Invalid object name"))
                {
                    errorMessage += "\n\nThe Customers table doesn't exist. Please run the database setup script.";
                }
                else if (sqlEx.Message.Contains("Cannot open database"))
                {
                    errorMessage += "\n\nCannot connect to TopazHardwareDb. Please check if the database exists.";
                }

                MessageBox.Show(errorMessage, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            tbxCompanyName.Text = "";
            tbxContactPerson.Text = "";
            tbxContactNumber.Text = "";
            tbxEmail.Text = "";
            tbxAddress.Text = "";
            CityCombobox.SelectedIndex = -1;
            ProvinceCombobox.SelectedIndex = -1;

            // Reset to default state
            ResetCityComboBox();
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

                // Add search functionality to ComboBoxes
                AddComboBoxSearch();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing ComboBoxes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Add search functionality to ComboBoxes
        private void AddComboBoxSearch()
        {
            // Make ComboBoxes searchable
            CityCombobox.DropDownStyle = ComboBoxStyle.DropDown;
            ProvinceCombobox.DropDownStyle = ComboBoxStyle.DropDown;

            // Add event handlers for searching
            CityCombobox.KeyUp += (s, e) => SearchComboBox(CityCombobox, e);
            ProvinceCombobox.KeyUp += (s, e) => SearchComboBox(ProvinceCombobox, e);

            // Add placeholder text
            CityCombobox.Text = "Type to search or select...";
            ProvinceCombobox.Text = "Type to search or select...";

            // Handle got focus to clear placeholder
            CityCombobox.GotFocus += (s, e) => { if (CityCombobox.Text == "Type to search or select...") CityCombobox.Text = ""; };
            ProvinceCombobox.GotFocus += (s, e) => { if (ProvinceCombobox.Text == "Type to search or select...") ProvinceCombobox.Text = ""; };

            // Handle lost focus to show placeholder if empty
            CityCombobox.LostFocus += (s, e) => { if (string.IsNullOrEmpty(CityCombobox.Text)) CityCombobox.Text = "Type to search or select..."; };
            ProvinceCombobox.LostFocus += (s, e) => { if (string.IsNullOrEmpty(ProvinceCombobox.Text)) ProvinceCombobox.Text = "Type to search or select..."; };
        }

        // Search functionality for ComboBox
        private void SearchComboBox(Guna.UI2.WinForms.Guna2ComboBox comboBox, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // If Enter is pressed, try to select the first matching item
                if (!string.IsNullOrEmpty(comboBox.Text))
                {
                    string searchText = comboBox.Text.ToLower();
                    foreach (var item in comboBox.Items)
                    {
                        if (item.ToString().ToLower().Contains(searchText))
                        {
                            comboBox.SelectedItem = item;
                            break;
                        }
                    }
                }
                return;
            }

            // Filter items based on text
            string filter = comboBox.Text.ToLower();
            var filteredItems = new List<object>();

            // Get all original items (you might want to store these separately)
            var allItems = comboBox == CityCombobox ? GetAllCities() : GetAllProvinces();

            foreach (var item in allItems)
            {
                if (item.ToLower().Contains(filter))
                {
                    filteredItems.Add(item);
                }
            }

            // Update the combo box items
            comboBox.Items.Clear();
            comboBox.Items.AddRange(filteredItems.ToArray());
            comboBox.DroppedDown = true;

            // Keep the text and cursor position
            comboBox.Text = filter;
            comboBox.SelectionStart = filter.Length;
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

        private List<string> GetAllProvinces()
        {
            return new List<string>(cityProvinceMap.Keys);
        }

        // Blue button - Add customer
        private void btnBlue_Click(object sender, EventArgs e)
        {
            AddCustomer();
        }

        // White button - Cancel/Close form
        private void btnWhite_Click(object sender, EventArgs e)
        {
            this.Close();
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

            // Update address preview
            UpdateAddressPreview();
        }

        private void ProvinceCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Filter cities based on selected province
            if (ProvinceCombobox.SelectedItem != null)
            {
                string selectedProvince = ProvinceCombobox.SelectedItem.ToString();
                FilterCitiesByProvince(selectedProvince);
            }

            // Update address preview
            UpdateAddressPreview();
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

        // Update address preview (optional feature)
        private void UpdateAddressPreview()
        {
            string address = tbxAddress.Text;
            string city = CityCombobox.SelectedItem?.ToString() ?? "";
            string province = ProvinceCombobox.SelectedItem?.ToString() ?? "";

            string preview = address;
            if (!string.IsNullOrEmpty(city)) preview += $", {city}";
            if (!string.IsNullOrEmpty(province)) preview += $", {province}";

            // Optional: Show preview in a tooltip or status label
            // You can add a label to your form to show this preview
        }

        // Validation methods
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

        private bool IsValidPhoneNumber(string phone)
        {
            // Remove common characters and check if it's a valid phone number
            string cleanPhone = System.Text.RegularExpressions.Regex.Replace(phone, @"[^\d]", "");
            return cleanPhone.Length >= 10 && cleanPhone.Length <= 11;
        }

        // Auto-format phone number
        private void FormatPhoneNumber()
        {
            string phone = tbxContactNumber.Text;
            if (string.IsNullOrEmpty(phone)) return;

            string cleanPhone = System.Text.RegularExpressions.Regex.Replace(phone, @"[^\d]", "");

            if (cleanPhone.Length == 10)
            {
                tbxContactNumber.Text = $"+63{cleanPhone.Substring(1)}";
            }
            else if (cleanPhone.Length == 11 && cleanPhone.StartsWith("0"))
            {
                tbxContactNumber.Text = $"+63{cleanPhone.Substring(1)}";
            }
            else if (cleanPhone.Length == 12 && cleanPhone.StartsWith("63"))
            {
                tbxContactNumber.Text = $"+{cleanPhone}";
            }
        }

        // Test database connection
        private bool TestDatabaseConnection()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot connect to database: {ex.Message}", "Connection Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Debug method to test database insert
        private void TestDatabaseInsert()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string testQuery = "INSERT INTO Customers (customer_name, contact_number, address) VALUES (@name, @contact, @address)";
                    using (SqlCommand cmd = new SqlCommand(testQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@name", "Test Customer " + DateTime.Now.ToString("HHmmss"));
                        cmd.Parameters.AddWithValue("@contact", "123456789");
                        cmd.Parameters.AddWithValue("@address", "Test Address");

                        con.Open();
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Test insert successful! Database is working.",
                                "Test Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Test insert failed: {ex.Message}",
                    "Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Debug method to check ComboBox selections
        private void DebugComboBoxSelections()
        {
            string city = CityCombobox.SelectedItem?.ToString() ?? "No selection";
            string province = ProvinceCombobox.SelectedItem?.ToString() ?? "No selection";

            string debugInfo = $"City: {city}\nProvince: {province}";
            MessageBox.Show(debugInfo, "ComboBox Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Form load event
        private void AddCustomerForm_Load(object sender, EventArgs e)
        {
            InitializeComboBoxes();
        }

        // Add Enter key support
        private void tbxCompanyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                AddCustomer();
                e.Handled = true;
            }
        }

        // Auto-format phone number when focus is lost
        private void tbxContactNumber_Leave(object sender, EventArgs e)
        {
            FormatPhoneNumber();
        }

        // Public method to test the form from container
        public void TestForm()
        {
            DebugComboBoxSelections();
            TestDatabaseInsert();
        }

        // All the required event handlers that are referenced in designer
        private void label8_Click(object sender, EventArgs e) { }
        private void tbxCompanyName_TextChanged(object sender, EventArgs e) { }
        private void tbxContactNumber_TextChanged(object sender, EventArgs e) { }
        private void tbxContactPerson_TextChanged(object sender, EventArgs e) { }
        private void tbxEmail_TextChanged(object sender, EventArgs e) { }
        private void tbxAddress_TextChanged(object sender, EventArgs e)
        {
            UpdateAddressPreview();
        }
        private void closeButton1_Load(object sender, EventArgs e) { }

        // Keep other existing empty event handlers...
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void kryptonComboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
        private void label15_Click(object sender, EventArgs e) { }
        private void label16_Click(object sender, EventArgs e) { }
        private void label18_Click(object sender, EventArgs e) { }
        private void label19_Click(object sender, EventArgs e) { }
        private void label20_Click(object sender, EventArgs e) { }
        private void TexboxCompanyName_TextChanged(object sender, EventArgs e) { }
        private void kryptonRichTextBox2_TextChanged(object sender, EventArgs e) { }
        private void kryptonRichTextBox1_TextChanged(object sender, EventArgs e) { }
        private void kryptonRichTextBox3_TextChanged(object sender, EventArgs e) { }
        private void kryptonRichTextBox4_TextChanged(object sender, EventArgs e) { }
        private void kryptonTextBox1_TextChanged(object sender, EventArgs e) { }
        private void AddFormButton_Click(object sender, EventArgs e) { }
        private void tbxCityMunicipality_TextChanged(object sender, EventArgs e) { }
        private void tbxProvince_TextChanged(object sender, EventArgs e) { }
        private void FormCancelButton_Click(object sender, EventArgs e) { }
    }
}