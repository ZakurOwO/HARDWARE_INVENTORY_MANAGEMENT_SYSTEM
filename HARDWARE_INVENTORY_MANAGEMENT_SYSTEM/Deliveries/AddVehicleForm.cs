using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class AddVehicleForm : UserControl
    {
        private SqlConnection con;
        private SqlDataAdapter daVehicles;
        private DataTable dtVehicles;

        public AddVehicleForm()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=.;Initial Catalog=InventoryCapstone;Integrated Security=True");
            LoadVehicles();
        }

        private void LoadVehicles()
        {
            dtVehicles = new DataTable();
            daVehicles = new SqlDataAdapter("SELECT * FROM Vehicles", con);
            SqlCommandBuilder cb = new SqlCommandBuilder(daVehicles);
            daVehicles.Fill(dtVehicles);
        }

        private void AddVehicle()
        {
            try
            {
                DataRow newRow = dtVehicles.NewRow();
                newRow["brand"] = VehiclesNameTextBox.Text;
                newRow["model"] = VehicleModelTextBox.Text;
                newRow["year_bought"] = YearBoughtTextBox.Text;
                newRow["plate_number"] = PlateNumberTextBox.Text;
                newRow["status"] = VehicleStatusComboBox.SelectedItem.ToString();
                newRow["image_path"] = kryptonRichTextBox1.Text;
                newRow["remark"] = VehicleRemarkTextBox.Text;

                dtVehicles.Rows.Add(newRow);
                daVehicles.Update(dtVehicles);

                MessageBox.Show("Vehicle added successfully!");
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            VehiclesNameTextBox.Clear();
            VehicleModelTextBox.Clear();
            YearBoughtTextBox.Clear();
            PlateNumberTextBox.Clear();
            VehicleStatusComboBox.SelectedIndex = -1;
            kryptonRichTextBox1.Clear();
            VehicleRemarkTextBox.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddVehicle();
        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void label14_Click(object sender, EventArgs e) { }

        private void pictureBox2_Click(object sender, EventArgs e) { }

        private void AddressTextBox_TextChanged(object sender, EventArgs e) { }

        private void kryptonPanel2_Paint(object sender, PaintEventArgs e) { }

        private void kryptonRichTextBox1_TextChanged(object sender, EventArgs e) { }
    }
}
