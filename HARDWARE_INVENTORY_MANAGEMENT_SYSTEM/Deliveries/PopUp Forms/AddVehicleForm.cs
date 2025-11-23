using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class AddVehicleForm : UserControl
    {
        private VehicleDataAccess vehicleDataAccess;
        private VehicleRecord currentVehicle;
        private string selectedImagePath;
        public event EventHandler VehicleSaved;
        public event EventHandler CancelRequested;

        public AddVehicleForm()
        {
            InitializeComponent();
            vehicleDataAccess = new VehicleDataAccess();
            InitializeForm();

            // Make the form clearly visible for testing
            this.BackColor = Color.LightBlue;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void InitializeForm()
        {
            SetPlaceholderBehavior();
            UploadImageButton.Click += UploadImageButton_Click;

            SetPlaceholderBehavior();
            UploadImageButton.Click += UploadImageButton_Click;

            // Add these lines - replace with your actual button names:
            btnBlue.Click += SaveButton_Click;
            btnWhite.Click += CancelButton_Click;
        }

        private void SetPlaceholderBehavior()
        {
            // Brand (using VehiclesNameTextBox as Brand field)
            VehiclesNameTextBox.Enter += (s, e) => {
                if (VehiclesNameTextBox.Text == "Enter Vehicle Name")
                    VehiclesNameTextBox.Text = "";
            };
            VehiclesNameTextBox.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(VehiclesNameTextBox.Text))
                    VehiclesNameTextBox.Text = "Enter Vehicle Name";
            };

            // Model
            VehicleModelTextBox.Enter += (s, e) => {
                if (VehicleModelTextBox.Text == "Enter Vehicle Model")
                    VehicleModelTextBox.Text = "";
            };
            VehicleModelTextBox.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(VehicleModelTextBox.Text))
                    VehicleModelTextBox.Text = "Enter Vehicle Model";
            };

            // Capacity (Year Bought field repurposed as Capacity)
            YearBoughtTextBox.Enter += (s, e) => {
                if (YearBoughtTextBox.Text == "Enter Year" || YearBoughtTextBox.Text == "Enter Capacity")
                    YearBoughtTextBox.Text = "";
            };
            YearBoughtTextBox.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(YearBoughtTextBox.Text))
                    YearBoughtTextBox.Text = "Enter Capacity";
            };

            // Plate Number
            PlateNumberTextBox.Enter += (s, e) => {
                if (PlateNumberTextBox.Text == "Enter Plate Number")
                    PlateNumberTextBox.Text = "";
            };
            PlateNumberTextBox.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(PlateNumberTextBox.Text))
                    PlateNumberTextBox.Text = "Enter Plate Number";
            };
        }

        private void UploadImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Select Vehicle Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName;
                    kryptonRichTextBox1.Text = Path.GetFileName(selectedImagePath);
                }
            }
        }

        // Public method that can be called from button click
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveVehicle();
        }

        // Public method for cancel/back functionality
        private void CancelButton_Click(object sender, EventArgs e)
        {
            CancelRequested?.Invoke(this, EventArgs.Empty);
        }

        public void LoadVehicleData(VehicleRecord vehicle)
        {
            currentVehicle = vehicle;

            VehiclesNameTextBox.Text = vehicle.Brand;
            VehicleModelTextBox.Text = vehicle.Model;
            YearBoughtTextBox.Text = vehicle.Capacity;
            PlateNumberTextBox.Text = vehicle.PlateNumber;
            VehicleStatusComboBox.SelectedItem = vehicle.Status;
            VehicleRemarkTextBox.Text = vehicle.Remarks ?? "";

            // Note: VehicleType would need to be added as a dropdown or textbox
        }

        public void SaveVehicle()
        {
            if (!ValidateInputs())
                return;

            try
            {
                VehicleRecord vehicle = new VehicleRecord
                {
                    Brand = VehiclesNameTextBox.Text,
                    Model = VehicleModelTextBox.Text,
                    VehicleType = "Drop-Side Truck", // You may want to add a combo box for this
                    Capacity = YearBoughtTextBox.Text,
                    PlateNumber = PlateNumberTextBox.Text,
                    Status = VehicleStatusComboBox.SelectedItem?.ToString() ?? "Available",
                    Remarks = VehicleRemarkTextBox.Text
                };

                // Handle image upload
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    string savedImagePath = VehicleImageManager.SaveVehicleImage(selectedImagePath);
                    vehicle.ImagePath = savedImagePath;
                }
                else
                {
                    // Use default image if no image selected
                    vehicle.ImagePath = "2000-Isuzu-mini-dump-truck-970-3730728_1 1.png";
                }

                if (currentVehicle == null)
                {
                    vehicleDataAccess.AddVehicle(vehicle);
                    MessageBox.Show("Vehicle added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    vehicle.VehicleInternalID = currentVehicle.VehicleInternalID;
                    vehicle.VehicleID = currentVehicle.VehicleID;
                    if (!string.IsNullOrEmpty(currentVehicle.ImagePath) && string.IsNullOrEmpty(selectedImagePath))
                    {
                        vehicle.ImagePath = currentVehicle.ImagePath;
                    }
                    vehicleDataAccess.UpdateVehicle(vehicle);
                    MessageBox.Show("Vehicle updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                VehicleSaved?.Invoke(this, EventArgs.Empty);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving vehicle: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (VehiclesNameTextBox.Text == "Enter Vehicle Name" ||
                string.IsNullOrWhiteSpace(VehiclesNameTextBox.Text))
            {
                MessageBox.Show("Please enter vehicle brand/name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                VehiclesNameTextBox.Focus();
                return false;
            }

            if (VehicleModelTextBox.Text == "Enter Vehicle Model" ||
                string.IsNullOrWhiteSpace(VehicleModelTextBox.Text))
            {
                MessageBox.Show("Please enter vehicle model", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                VehicleModelTextBox.Focus();
                return false;
            }

            if (PlateNumberTextBox.Text == "Enter Plate Number" ||
                string.IsNullOrWhiteSpace(PlateNumberTextBox.Text))
            {
                MessageBox.Show("Please enter plate number", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PlateNumberTextBox.Focus();
                return false;
            }

            if (VehicleStatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select vehicle status", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                VehicleStatusComboBox.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            VehiclesNameTextBox.Text = "Enter Vehicle Name";
            VehicleModelTextBox.Text = "Enter Vehicle Model";
            YearBoughtTextBox.Text = "Enter Capacity";
            PlateNumberTextBox.Text = "Enter Plate Number";
            VehicleStatusComboBox.SelectedIndex = -1;
            VehicleRemarkTextBox.Text = "";
            kryptonRichTextBox1.Text = "";
            selectedImagePath = null;
            currentVehicle = null;
        }

        // Keep existing event handlers
        private void kryptonPanel1_Paint(object sender, PaintEventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void label14_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void AddressTextBox_TextChanged(object sender, EventArgs e) { }
        private void kryptonPanel2_Paint(object sender, PaintEventArgs e) { }
        private void kryptonRichTextBox1_TextChanged(object sender, EventArgs e) { }
    }
}