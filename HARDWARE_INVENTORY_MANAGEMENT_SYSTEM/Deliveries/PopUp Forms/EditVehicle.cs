using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Krypton.Toolkit;

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

            // Set up placeholder behavior
            SetupPlaceholders();

            // Wire up button events
            WireUpButtons();
        }

        private void InitializeStatusComboBox()
        {
            EditVehicleStatusComboBox.Items.Clear();
            EditVehicleStatusComboBox.Items.AddRange(new object[] {
                "Available",
                "On Delivery",
                "Maintenance",
                "In Use"
            });
            EditVehicleStatusComboBox.SelectedIndex = 0;
        }

        private void SetupPlaceholders()
        {
            // Store original placeholder text and set initial state
            EditVehiclesNameTextBox.Tag = "Enter Vehicle Name";
            EditVehicleModel.Tag = "Enter Vehicle Model";
            EditYearBoughtTextBox.Tag = "Enter Year";
            EditPlateNumberTextBox.Tag = "Enter Plate Number";

            // Apply initial placeholder text
            SetPlaceholderState(EditVehiclesNameTextBox, true);
            SetPlaceholderState(EditVehicleModel, true);
            SetPlaceholderState(EditYearBoughtTextBox, true);
            SetPlaceholderState(EditPlateNumberTextBox, true);

            // Wire up focus events
            WireUpPlaceholderEvents(EditVehiclesNameTextBox);
            WireUpPlaceholderEvents(EditVehicleModel);
            WireUpPlaceholderEvents(EditYearBoughtTextBox);
            WireUpPlaceholderEvents(EditPlateNumberTextBox);
        }

        private void WireUpPlaceholderEvents(KryptonRichTextBox textBox)
        {
            textBox.Enter += (s, e) => ClearPlaceholderIfNeeded(textBox);
            textBox.Leave += (s, e) => RestorePlaceholderIfNeeded(textBox);
            textBox.TextChanged += (s, e) => ValidateRealContent(textBox);
        }

        private void SetPlaceholderState(KryptonRichTextBox textBox, bool isPlaceholder)
        {
            if (isPlaceholder)
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.StateCommon.Content.Color1 = Color.Gray;
                textBox.StateCommon.Content.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            }
            else
            {
                textBox.StateCommon.Content.Color1 = Color.Black;
                textBox.StateCommon.Content.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            }
        }

        private void ClearPlaceholderIfNeeded(KryptonRichTextBox textBox)
        {
            if (textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
                textBox.StateCommon.Content.Color1 = Color.Black;
                textBox.StateCommon.Content.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            }
        }

        private void RestorePlaceholderIfNeeded(KryptonRichTextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                SetPlaceholderState(textBox, true);
            }
        }

        private void ValidateRealContent(KryptonRichTextBox textBox)
        {
            // If user types something and it matches placeholder, clear it
            if (textBox.Text == textBox.Tag.ToString() && textBox.Focused)
            {
                textBox.Text = "";
            }
        }

        // Helper method to check if a textbox has real content (not placeholder)
        private bool HasRealContent(KryptonRichTextBox textBox)
        {
            return !string.IsNullOrWhiteSpace(textBox.Text) && textBox.Text != textBox.Tag.ToString();
        }

        // Helper method to get real text content
        private string GetRealText(KryptonRichTextBox textBox)
        {
            return textBox.Text == textBox.Tag.ToString() ? "" : textBox.Text;
        }

        private void WireUpButtons()
        {
            // Wire up image upload button
            if (EditUploadImageButton != null)
            {
                EditUploadImageButton.Click += EditUploadImageButton_Click;
                EditUploadImageButton.Cursor = Cursors.Hand;
            }
        }

        private void EditUploadImageButton_Click(object sender, EventArgs e)
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

        public void LoadVehicleData(VehicleRecord vehicle)
        {
            currentVehicle = vehicle;

            // Load data into form fields (set real content, not placeholders)
            if (!string.IsNullOrWhiteSpace(vehicle.Brand))
            {
                EditVehiclesNameTextBox.Text = vehicle.Brand;
                SetPlaceholderState(EditVehiclesNameTextBox, false);
            }

            if (!string.IsNullOrWhiteSpace(vehicle.Model))
            {
                EditVehicleModel.Text = vehicle.Model;
                SetPlaceholderState(EditVehicleModel, false);
            }

            if (!string.IsNullOrWhiteSpace(vehicle.Capacity))
            {
                EditYearBoughtTextBox.Text = vehicle.Capacity;
                SetPlaceholderState(EditYearBoughtTextBox, false);
            }

            if (!string.IsNullOrWhiteSpace(vehicle.PlateNumber))
            {
                EditPlateNumberTextBox.Text = vehicle.PlateNumber;
                SetPlaceholderState(EditPlateNumberTextBox, false);
            }

            // Set status
            if (!string.IsNullOrEmpty(vehicle.Status))
            {
                EditVehicleStatusComboBox.SelectedItem = vehicle.Status;
            }

            EditVehicleRemarkTextBox.Text = vehicle.Remarks ?? "";

            // Load image info
            if (!string.IsNullOrEmpty(vehicle.ImagePath))
            {
                kryptonRichTextBox1.Text = Path.GetFileName(vehicle.ImagePath);
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
                    Brand = GetRealText(EditVehiclesNameTextBox).Trim(),
                    Model = GetRealText(EditVehicleModel).Trim(),
                    VehicleType = "Drop-Side Truck",
                    Capacity = GetRealText(EditYearBoughtTextBox).Trim(),
                    PlateNumber = GetRealText(EditPlateNumberTextBox).Trim(),
                    Status = EditVehicleStatusComboBox.SelectedItem?.ToString() ?? "Available",
                    Remarks = EditVehicleRemarkTextBox.Text.Trim()
                };

                // Check for duplicate plate number
                if (vehicleDataAccess.PlateNumberExists(vehicle.PlateNumber, currentVehicle?.VehicleInternalID ?? 0))
                {
                    MessageBox.Show("A vehicle with this plate number already exists!", "Duplicate Plate Number",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EditPlateNumberTextBox.Focus();
                    ClearPlaceholderIfNeeded(EditPlateNumberTextBox);
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

                // Update vehicle (always updating since this is EditVehicle form)
                if (currentVehicle != null)
                {
                    vehicle.VehicleInternalID = currentVehicle.VehicleInternalID;
                    vehicle.VehicleID = currentVehicle.VehicleID;
                    vehicleDataAccess.UpdateVehicle(vehicle);
                    MessageBox.Show("Vehicle updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No vehicle loaded to edit!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
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
            if (!HasRealContent(EditVehiclesNameTextBox))
            {
                MessageBox.Show("Please enter vehicle brand/name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EditVehiclesNameTextBox.Focus();
                ClearPlaceholderIfNeeded(EditVehiclesNameTextBox);
                return false;
            }

            if (!HasRealContent(EditVehicleModel))
            {
                MessageBox.Show("Please enter vehicle model", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EditVehicleModel.Focus();
                ClearPlaceholderIfNeeded(EditVehicleModel);
                return false;
            }

            if (!HasRealContent(EditPlateNumberTextBox))
            {
                MessageBox.Show("Please enter plate number", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EditPlateNumberTextBox.Focus();
                ClearPlaceholderIfNeeded(EditPlateNumberTextBox);
                return false;
            }

            if (EditVehicleStatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select vehicle status", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EditVehicleStatusComboBox.Focus();
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
                if (string.IsNullOrWhiteSpace(GetRealText(EditVehiclesNameTextBox)))
                    SetPlaceholderState(EditVehiclesNameTextBox, true);
                if (string.IsNullOrWhiteSpace(GetRealText(EditVehicleModel)))
                    SetPlaceholderState(EditVehicleModel, true);
                if (string.IsNullOrWhiteSpace(GetRealText(EditYearBoughtTextBox)))
                    SetPlaceholderState(EditYearBoughtTextBox, true);
                if (string.IsNullOrWhiteSpace(GetRealText(EditPlateNumberTextBox)))
                    SetPlaceholderState(EditPlateNumberTextBox, true);
            }
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