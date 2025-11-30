using Guna.UI2.WinForms;
using Krypton.Toolkit;
using ScottPlot.DataViews;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class EditVehicle : UserControl
    {
        private VehicleDataAccess vehicleDataAccess;
        private VehicleRecord currentVehicle;
        private string selectedImagePath;

        public event EventHandler VehicleSaved;
        public event EventHandler CancelRequested;

        public EditVehicle()
        {
            InitializeComponent();
            vehicleDataAccess = new VehicleDataAccess();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Initialize status combo box
            InitializeStatusComboBox();

            // Wire up button events
            WireUpButtons();
        }

        private void InitializeStatusComboBox()
        {
            VehicleStatusComboBox.Items.Clear();
            VehicleStatusComboBox.Items.AddRange(new object[] {
                "Available",
                "On Delivery",
                "Maintenance",
                "In Use"
            });
            VehicleStatusComboBox.SelectedIndex = 0;
        }

        private void WireUpButtons()
        {
            // Directly wire the existing buttons
            if (btnBlue != null)
            {
                btnBlue.Click += (s, e) => SaveVehicle();
            }

            if (btnWhite != null)
            {
                btnWhite.Click += (s, e) => CancelRequested?.Invoke(this, EventArgs.Empty);
                closeButton1.Click += (s, e) => CancelRequested?.Invoke(this, EventArgs.Empty);
            }
        }


        public void LoadVehicleData(VehicleRecord vehicle)
        {
            currentVehicle = vehicle;

            // Load data into form fields
            if (!string.IsNullOrWhiteSpace(vehicle.Brand))
            {
                tbxVehicleName.Text = vehicle.Brand;
            }

            if (!string.IsNullOrWhiteSpace(vehicle.Model))
            {
                VehicleModelTextBox.Text = vehicle.Model;
            }

            if (!string.IsNullOrWhiteSpace(vehicle.Capacity))
            {
                YearBoughtTextBox.Text = vehicle.Capacity;
            }

            if (!string.IsNullOrWhiteSpace(vehicle.PlateNumber))
            {
                PlateNumberTextBox.Text = vehicle.PlateNumber;
            }

            // Set status
            if (!string.IsNullOrEmpty(vehicle.Status))
            {
                VehicleStatusComboBox.SelectedItem = vehicle.Status;
            }

            // REMOVED: VehicleRemarkTextBox.Text = vehicle.Remarks ?? "";

            // Load image info
            if (!string.IsNullOrEmpty(vehicle.ImagePath))
            {
                ImageUploadBox.Text = Path.GetFileName(vehicle.ImagePath);
                selectedImagePath = VehicleImageManager.GetFullImagePath(vehicle.ImagePath);
            }
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            SaveVehicle();
        }

        private void btnWhite_Click(object sender, EventArgs e)
        {
            CancelRequested?.Invoke(this, EventArgs.Empty);
        }

        private void SaveVehicle()
        {
            if (!ValidateInputs())
                return;

            try
            {
                VehicleRecord vehicle = new VehicleRecord
                {
                    Brand = GetText(tbxVehicleName).Trim(),
                    Model = GetText(VehicleModelTextBox).Trim(),
                    VehicleType = "Drop-Side Truck",
                    Capacity = GetText(YearBoughtTextBox).Trim(),
                    PlateNumber = GetText(PlateNumberTextBox).Trim(),
                    Status = VehicleStatusComboBox.SelectedItem?.ToString() ?? "Available"
                    // REMOVED: Remarks = VehicleRemarkTextBox.Text.Trim()
                };

                // Check for duplicate plate number
                if (vehicleDataAccess.PlateNumberExists(vehicle.PlateNumber, currentVehicle?.VehicleInternalID ?? 0))
                {
                    MessageBox.Show("A vehicle with this plate number already exists!", "Duplicate Plate Number",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PlateNumberTextBox.Focus();
                    return;
                }

                // Handle image upload
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    string savedImagePath = VehicleImageManager.SaveVehicleImage(selectedImagePath);
                    vehicle.ImagePath = savedImagePath;
                }
                else if (currentVehicle != null && !string.IsNullOrEmpty(currentVehicle.ImagePath))
                {
                    // Keep existing image if editing and no new image selected
                    vehicle.ImagePath = currentVehicle.ImagePath;
                }
                else
                {
                    // Use default image
                    vehicle.ImagePath = "2000-Isuzu-mini-dump-truck-970-3730728_1 1.png";
                }

                // Save to database
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
                    vehicleDataAccess.UpdateVehicle(vehicle);
                    MessageBox.Show("Vehicle updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                VehicleSaved?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving vehicle: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (!HasText(tbxVehicleName))
            {
                MessageBox.Show("Please enter vehicle brand/name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxVehicleName.Focus();
                return false;
            }

            if (!HasText(VehicleModelTextBox))
            {
                MessageBox.Show("Please enter vehicle model", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                VehicleModelTextBox.Focus();
                return false;
            }

            if (!HasText(PlateNumberTextBox))
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


        private bool HasText(Guna2TextBox tb) => tb != null && !string.IsNullOrWhiteSpace(tb.Text);
        private string GetText(Guna2TextBox tb) => tb?.Text ?? string.Empty;

        
    }
}