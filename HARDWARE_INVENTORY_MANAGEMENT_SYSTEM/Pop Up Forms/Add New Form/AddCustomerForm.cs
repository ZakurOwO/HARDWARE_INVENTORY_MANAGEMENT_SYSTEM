using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public partial class AddCustomerForm : Form
    {
        private readonly AddCustomerContainer container;
        private readonly Dictionary<string, List<string>> cityProvinceMap;

        public event EventHandler CustomerAdded;

        public AddCustomerForm(AddCustomerContainer host = null)
        {
            container = host;
            InitializeComponent();

            btnBlue.Click += btnBlue_Click;
            btnWhite.Click += btnWhite_Click;
            CityCombobox.SelectedIndexChanged += CityCombobox_SelectedIndexChanged;
            ProvinceCombobox.SelectedIndexChanged += ProvinceCombobox_SelectedIndexChanged;

            if (closeButton1 != null)
            {
                closeButton1.CloseClicked += (s, e) => Close();
            }

            cityProvinceMap = CustomerLocationMap();
            InitializeComboBoxes();
        }

        private Dictionary<string, List<string>> CustomerLocationMap()
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
                // Bulacan, Cavite, Laguna, Rizal…
                // (rest of your map stays exactly the same)
            };
        }

        private void InitializeComboBoxes()
        {
            try
            {
                CityCombobox.Items.Clear();
                ProvinceCombobox.Items.Clear();

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
        private void CityCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CityCombobox.SelectedItem == null) return;

            string city = CityCombobox.SelectedItem.ToString();
            string province = cityProvinceMap.FirstOrDefault(x => x.Value.Contains(city)).Key;

            if (!string.IsNullOrEmpty(province))
                ProvinceCombobox.SelectedItem = province;
        }

        private void ProvinceCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProvinceCombobox.SelectedItem == null) return;

            string province = ProvinceCombobox.SelectedItem.ToString();
            CityCombobox.Items.Clear();

            if (cityProvinceMap.ContainsKey(province))
                CityCombobox.Items.AddRange(cityProvinceMap[province].ToArray());

            CityCombobox.SelectedIndex = -1;
        }

        private string FormatPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return string.Empty;

            string digits = Regex.Replace(phone, @"\D", "");

            if (digits.StartsWith("0"))
                digits = digits.Substring(1);

            if (!digits.StartsWith("63"))
                digits = "63" + digits;

            return "+" + digits;
        }




        private void ResetCityComboBox()
        {
            CityCombobox.Items.Clear();
            CityCombobox.Items.AddRange(cityProvinceMap.Values.SelectMany(c => c).ToArray());
        }

        private bool ValidateInputs(out CustomerDetailsModel details)
        {
            details = new CustomerDetailsModel
            {
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
                MessageBox.Show("Company name is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(details.ContactNumber))
            {
                MessageBox.Show("Contact number is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(details.AddressLine))
            {
                MessageBox.Show("Address is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(details.Province))
            {
                MessageBox.Show("Please select a province.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(details.City))
            {
                MessageBox.Show("Please select a city.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(details.Status))
            {
                MessageBox.Show("Please select a status.");
                return false;
            }

            details.ContactNumber = FormatPhoneNumber(details.ContactNumber);

            return true;
        }

        private void AddCustomer()
        {
            if (!ValidateInputs(out CustomerDetailsModel details))
                return;

            if (container == null)
            {
                MessageBox.Show("Unable to save: container not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (container.AddCustomer(details, out string errorMessage))
            {
                MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CustomerAdded?.Invoke(this, EventArgs.Empty);
                Close();
            }
            else
            {
                MessageBox.Show($"Failed to add customer: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBlue_Click(object sender, EventArgs e) => AddCustomer();
        private void btnWhite_Click(object sender, EventArgs e) => Close();

        private void AddCustomerForm_Load(object sender, EventArgs e)
        {
            InitializeComboBoxes();
        }

        private void tbxContactNumber_Leave(object sender, EventArgs e)
        {
            tbxContactNumber.Text = FormatPhoneNumber(tbxContactNumber.Text);
        }

        private void tbxAddress_TextChanged(object sender, EventArgs e)
        {
            // keep event for designer compatibility
        }

        private void tbxCompanyName_TextChanged(object sender, EventArgs e) { }
        private void tbxContactPerson_TextChanged(object sender, EventArgs e) { }
        private void tbxContactNumber_TextChanged(object sender, EventArgs e) { }
        private void tbxEmail_TextChanged(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void closeButton1_Load(object sender, EventArgs e) { }
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
        private void tbxCompanyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                AddCustomer();
                e.Handled = true;
            }
        }
    }
}
