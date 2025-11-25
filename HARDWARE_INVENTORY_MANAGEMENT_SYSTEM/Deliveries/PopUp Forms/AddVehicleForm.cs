using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Krypton.Toolkit;
using Guna.UI2.WinForms;

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
        }

        private void InitializeForm()
        {
            // Initialize status combo box
            InitializeStatusComboBox();

            // Set up placeholder behavior
            SetupPlaceholders();

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

        private void SetupPlaceholders()
        {
            // Store original placeholder text and set initial state
            tbxVehicleName.Tag = "Enter Vehicle Name";
            VehicleModelTextBox.Tag = "Enter Vehicle Model";
            YearBoughtTextBox.Tag = "Enter Year";
            PlateNumberTextBox.Tag = "Enter Plate Number";

            // Apply initial placeholder text
            SetPlaceholderState(tbxVehicleName, true);
            SetPlaceholderState(VehicleModelTextBox, true);
            SetPlaceholderState(YearBoughtTextBox, true);
            SetPlaceholderState(PlateNumberTextBox, true);

            // Wire up focus events
            WireUpPlaceholderEvents(tbxVehicleName);
            WireUpPlaceholderEvents(VehicleModelTextBox);
            WireUpPlaceholderEvents(YearBoughtTextBox);
            WireUpPlaceholderEvents(PlateNumberTextBox);
        }

        private void WireUpPlaceholderEvents(Guna2TextBox textBox)
        {
            if (textBox == null) return;

            textBox.Enter += (s, e) => ClearPlaceholderIfNeeded(textBox);
            textBox.Leave += (s, e) => RestorePlaceholderIfNeeded(textBox);
            textBox.TextChanged += (s, e) => ValidateRealContent(textBox);
        }

        private void SetPlaceholderState(Guna2TextBox textBox, bool isPlaceholder)
        {
            if (textBox == null) return;

            if (isPlaceholder)
            {
                textBox.Text = textBox.Tag?.ToString() ?? string.Empty;
                textBox.ForeColor = Color.Gray;
                textBox.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            }
            else
            {
                textBox.ForeColor = Color.Black;
                textBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            }
        }

        private void ClearPlaceholderIfNeeded(Guna2TextBox textBox)
        {
            if (textBox == null) return;

            var placeholder = textBox.Tag?.ToString() ?? string.Empty;
            if (textBox.Text == placeholder)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
                textBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            }
        }

        private void RestorePlaceholderIfNeeded(Guna2TextBox textBox)
        {
            if (textBox == null) return;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                SetPlaceholderState(textBox, true);
            }
        }

        private void ValidateRealContent(Guna2TextBox textBox)
        {
            if (textBox == null) return;

            // If user types something and it matches placeholder while focused, clear it
            var placeholder = textBox.Tag?.ToString() ?? string.Empty;
            if (textBox.Text == placeholder && textBox.Focused)
            {
                textBox.Text = "";
            }
        }

        // Helper method to check if a textbox has real content (not placeholder)
        private bool HasRealContent(Guna2TextBox textBox)
        {
            if (textBox == null) return false;
            var placeholder = textBox.Tag?.ToString() ?? string.Empty;
            return !string.IsNullOrWhiteSpace(textBox.Text) && textBox.Text != placeholder;
        }

        // Helper method to get real text content
        private string GetRealText(Guna2TextBox textBox)
        {
            if (textBox == null) return string.Empty;
            var placeholder = textBox.Tag?.ToString() ?? string.Empty;
            return textBox.Text == placeholder ? "" : textBox.Text;
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
            }
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
                    ImageUploadBox.Text = Path.GetFileName(selectedImagePath);
                }
            }
        }

        public void LoadVehicleData(VehicleRecord vehicle)
        {
            currentVehicle = vehicle;

            // Load data into form fields (set real content, not placeholders)
            if (!string.IsNullOrWhiteSpace(vehicle.Brand))
            {
                tbxVehicleName.Text = vehicle.Brand;
                SetPlaceholderState(tbxVehicleName, false);
            }

            if (!string.IsNullOrWhiteSpace(vehicle.Model))
            {
                VehicleModelTextBox.Text = vehicle.Model;
                SetPlaceholderState(VehicleModelTextBox, false);
            }

            if (!string.IsNullOrWhiteSpace(vehicle.Capacity))
            {
                YearBoughtTextBox.Text = vehicle.Capacity;
                SetPlaceholderState(YearBoughtTextBox, false);
            }

            if (!string.IsNullOrWhiteSpace(vehicle.PlateNumber))
            {
                PlateNumberTextBox.Text = vehicle.PlateNumber;
                SetPlaceholderState(PlateNumberTextBox, false);
            }

            // Set status
            if (!string.IsNullOrEmpty(vehicle.Status))
            {
                VehicleStatusComboBox.SelectedItem = vehicle.Status;
            }

            VehicleRemarkTextBox.Text = vehicle.Remarks ?? "";

            // Load image info
            if (!string.IsNullOrEmpty(vehicle.ImagePath))
            {
                ImageUploadBox.Text = Path.GetFileName(vehicle.ImagePath);
                selectedImagePath = VehicleImageManager.GetFullImagePath(vehicle.ImagePath);
            }
        }

        private void SaveVehicle()
        {
            if (!ValidateInputs())
                return;

            try
            {
                VehicleRecord vehicle = new VehicleRecord
                {
                    Brand = GetRealText(tbxVehicleName).Trim(),
                    Model = GetRealText(VehicleModelTextBox).Trim(),
                    VehicleType = "Drop-Side Truck",
                    Capacity = GetRealText(YearBoughtTextBox).Trim(),
                    PlateNumber = GetRealText(PlateNumberTextBox).Trim(),
                    Status = VehicleStatusComboBox.SelectedItem?.ToString() ?? "Available",
                    Remarks = VehicleRemarkTextBox.Text.Trim()
                };

                // Check for duplicate plate number
                if (vehicleDataAccess.PlateNumberExists(vehicle.PlateNumber, currentVehicle?.VehicleInternalID ?? 0))
                {
                    MessageBox.Show("A vehicle with this plate number already exists!", "Duplicate Plate Number",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PlateNumberTextBox.Focus();
                    ClearPlaceholderIfNeeded(PlateNumberTextBox);
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
            if (!HasRealContent(tbxVehicleName))
            {
                MessageBox.Show("Please enter vehicle brand/name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxVehicleName.Focus();
                ClearPlaceholderIfNeeded(tbxVehicleName);
                return false;
            }

            if (!HasRealContent(VehicleModelTextBox))
            {
                MessageBox.Show("Please enter vehicle model", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                VehicleModelTextBox.Focus();
                ClearPlaceholderIfNeeded(VehicleModelTextBox);
                return false;
            }

            if (!HasRealContent(PlateNumberTextBox))
            {
                MessageBox.Show("Please enter plate number", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PlateNumberTextBox.Focus();
                ClearPlaceholderIfNeeded(PlateNumberTextBox);
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

        // Enhanced placeholder behavior for when form is shown
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.Visible)
            {
                // Ensure placeholders are properly set when form becomes visible
                if (string.IsNullOrWhiteSpace(GetRealText(tbxVehicleName)))
                    SetPlaceholderState(tbxVehicleName, true);
                if (string.IsNullOrWhiteSpace(GetRealText(VehicleModelTextBox)))
                    SetPlaceholderState(VehicleModelTextBox, true);
                if (string.IsNullOrWhiteSpace(GetRealText(YearBoughtTextBox)))
                    SetPlaceholderState(YearBoughtTextBox, true);
                if (string.IsNullOrWhiteSpace(GetRealText(PlateNumberTextBox)))
                    SetPlaceholderState(PlateNumberTextBox, true);
            }
        }


        // Event handlers for designer events
        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Optional: Custom panel painting if needed
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Handle picture box click if needed
        }

        private void label14_Click(object sender, EventArgs e)
        {
            // Handle label click if needed
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Handle picture box click if needed
        }

        private void AddressTextBox_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed for PlateNumberTextBox
            // This will be handled by our placeholder system
        }

        private void kryptonPanel2_Paint(object sender, PaintEventArgs e)
        {
            // Handle panel painting if needed
        }

        private void kryptonRichTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Handle image text box changes
        }

        // Override OnLoad to ensure placeholders are set when control loads
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Ensure placeholders are set after control is fully loaded
            if (!DesignMode)
            {
                SetupPlaceholders();
            }
        }

       
    }
}