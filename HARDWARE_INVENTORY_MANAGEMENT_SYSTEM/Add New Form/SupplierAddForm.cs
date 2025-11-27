using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public partial class SupplierAddForm : UserControl
    {
        private SqlConnection con;
        private SqlDataAdapter da;
        private DataTable dt;

        public event EventHandler CancelClicked;

        public SupplierAddForm()
        {
            InitializeComponent();
            //con = new SqlConnection(@"Data Source=.;Initial Catalog=InventoryCapstone;Integrated Security=True");
            con = new SqlConnection(@"Data Source=ACHILLES\SQLEXPRESS;Initial Catalog=TopazHardwareDb;Integrated Security=True;TrustServerCertificate=True;");
            LoadSuppliers();
            closeButton1.Click += CloseButton1_Click;
        }

        private void LoadSuppliers()
        {
            try
            {
                dt = new DataTable();
                da = new SqlDataAdapter("SELECT * FROM Suppliers", con);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);

                con.Open();
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message);
            }
        }

        private void AddSupplier()
        {
            try
            {
                DataRow newRow = dt.NewRow();
                newRow["supplier_name"] = CompanyNameTextBoxSupplier.Text;
                newRow["contact_number"] = ContactTxtBoxSupplier.Text;
                newRow["address"] = LocationSupplierTextBox.Text;
                newRow["email"] = tbxEmail.Text;
                newRow["contact_person"] = tbxContactPerson.Text;
                
                dt.Rows.Add(newRow);

                con.Open();
                da.Update(dt);
                con.Close();

                MessageBox.Show("Supplier added successfully!");
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }
        }

        private void ClearFields()
        {
            CompanyNameTextBoxSupplier.Clear();
            ContactTxtBoxSupplier.Clear();
            LocationSupplierTextBox.Clear();
            tbxEmail.Clear();
            tbxContactPerson.Clear();

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            AddSupplier();
        }

        private void ContactPersonTextBox_TextChanged(object sender, EventArgs e) { }

        private void ZipCodeTextBox_TextChanged(object sender, EventArgs e) { }

        private void ProvinceComboBox_SelectedIndexChanged(object sender, EventArgs e) { }

        private void CityComboBox_SelectedIndexChanged(object sender, EventArgs e) { }

        private void AddressTextBox_TextChanged(object sender, EventArgs e) { }

        private void PhoneNumberTextBox_TextChanged(object sender, EventArgs e) { }

        private void label7_Click(object sender, EventArgs e) { }

        private void EmailAddressTextBox_TextChanged(object sender, EventArgs e) { }

        private void label20_Click(object sender, EventArgs e) { }

        private void label18_Click(object sender, EventArgs e) { }

        private void label19_Click(object sender, EventArgs e) { }

        private void label16_Click(object sender, EventArgs e) { }

        private void label15_Click(object sender, EventArgs e) { }

        private void label12_Click(object sender, EventArgs e) { }

        private void label13_Click(object sender, EventArgs e) { }

        private void label10_Click(object sender, EventArgs e) { }

        private void label11_Click(object sender, EventArgs e) { }

        private void label8_Click(object sender, EventArgs e) { }

        private void label9_Click(object sender, EventArgs e) { }

        private void label6_Click(object sender, EventArgs e) { }

        private void CompanyNameTextBox_TextChanged(object sender, EventArgs e) { }

        private void label4_Click(object sender, EventArgs e) { }

        private void label5_Click(object sender, EventArgs e) { }

        private void label3_Click(object sender, EventArgs e) { }

        private void label2_Click(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void CustomerRemarkTextBox_TextChanged(object sender, EventArgs e) { }

        private void pictureBox2_Click(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void CancelSupplierFormBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void CancelSupplierFormBtn_Click_1(object sender, EventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }

        private void closeButton1_Load(object sender, EventArgs e)
        {

        }
        private void CloseButton1_Click(object sender, EventArgs e)
        {
            // reuse the same event that the Cancel button raises
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
