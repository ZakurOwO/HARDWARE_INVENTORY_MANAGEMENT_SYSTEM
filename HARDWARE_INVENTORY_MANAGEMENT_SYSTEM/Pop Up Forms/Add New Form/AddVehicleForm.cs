using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Krypton.Toolkit;
using Guna.UI2.WinForms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System.Data.SqlClient;

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
            // We let the designer handle btnBlue.Click -> btnBlue_Click
            // so we only wire cancel/close here.

            if (btnWhite != null)
            {
                btnWhite.Click += (s, e) => CancelRequested?.Invoke(this, EventArgs.Empty);
            }

            if (closeButton1 != null)
            {
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

            // Load image info
            if (!string.IsNullOrEmpty(vehicle.ImagePath))
            {
                ImageUploadBox.Text = Path.GetFileName(vehicle.ImagePath);
                selectedImagePath = VehicleImageManager.GetFullImagePath(vehicle.ImagePath);
            }
        }

        // ======================================
        //  MAIN SAVE + AUDIT
        // ======================================
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
                    VehicleType = "Drop-Side Truck", // hard-coded for now
                    Capacity = GetText(YearBoughtTextBox).Trim(),
                    PlateNumber = GetText(PlateNumberTextBox).Trim(),
                    Status = VehicleStatusComboBox.SelectedItem?.ToString() ?? "Available"
                };

                // Check for duplicate plate number
                if (vehicleDataAccess.PlateNumberExists(
                        vehicle.PlateNumber,
                        currentVehicle?.VehicleInternalID ?? 0))
                {
                    MessageBox.Show("A vehicle with this plate number already exists!",
                        "Duplicate Plate Number",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
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

                // -----------------------------
                //   SAVE + AUDIT
                // -----------------------------
                if (currentVehicle == null)
                {
                    // ADD
                    vehicleDataAccess.AddVehicle(vehicle);

                    LogVehicleAudit(
                        activity: $"Added vehicle {vehicle.PlateNumber}",
                        activityType: "CREATE",
                        recordId: vehicle.PlateNumber,
                        oldValues: null,
                        newValues: BuildVehicleState(vehicle));

                    MessageBox.Show("Vehicle added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // UPDATE (build old state first)
                    string oldValues = BuildVehicleState(currentVehicle);

                    vehicle.VehicleInternalID = currentVehicle.VehicleInternalID;
                    vehicle.VehicleID = currentVehicle.VehicleID;

                    vehicleDataAccess.UpdateVehicle(vehicle);

                    LogVehicleAudit(
                        activity: $"Updated vehicle {vehicle.PlateNumber}",
                        activityType: "UPDATE",
                        recordId: vehicle.PlateNumber,
                        oldValues: oldValues,
                        newValues: BuildVehicleState(vehicle));

                    MessageBox.Show("Vehicle updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // keep currentVehicle in sync if you re-use this control
                    currentVehicle = vehicle;
                }

                VehicleSaved?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving vehicle: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ======================================
        //  VALIDATION
        // ======================================
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

        // ======================================
        //  AUDIT HELPERS
        // ======================================
        private void LogVehicleAudit(
            string activity,
            string activityType,   // "CREATE", "UPDATE", "DELETE"
            string recordId,       // e.g. plate number or VehicleID
            string oldValues,
            string newValues)
        {
            // Use the real logged-in user from UserSession
            int userId = UserSession.UserId;                 // from your session class
            string username = UserSession.Username ?? "Unknown";

            using (SqlConnection conn = new SqlConnection(ConnectionString.DataSource))
            using (SqlCommand cmd = new SqlCommand(@"
                INSERT INTO AuditLog
                (user_id, username, module, activity, activity_type,
                 table_affected, record_id, old_values, new_values, ip_address)
                VALUES
                (@user_id, @username, @module, @activity, @activity_type,
                 @table_affected, @record_id, @old_values, @new_values, @ip_address);", conn))
            {
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@module", "Vehicles");
                cmd.Parameters.AddWithValue("@activity", activity);
                cmd.Parameters.AddWithValue("@activity_type", activityType);
                cmd.Parameters.AddWithValue("@table_affected", "Vehicles");
                cmd.Parameters.AddWithValue("@record_id", (object)recordId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@old_values", (object)oldValues ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@new_values", (object)newValues ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ip_address", "127.0.0.1"); // TODO: real IP if needed

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private string BuildVehicleState(VehicleRecord v)
        {
            if (v == null) return null;

            return
                $"Brand={v.Brand};" +
                $"Model={v.Model};" +
                $"Type={v.VehicleType};" +
                $"Capacity={v.Capacity};" +
                $"PlateNumber={v.PlateNumber};" +
                $"Status={v.Status};" +
                $"ImagePath={v.ImagePath}";
        }

        // ======================================
        //  SMALL HELPERS / EVENTS
        // ======================================
        private bool HasText(Guna2TextBox tb) =>
            tb != null && !string.IsNullOrWhiteSpace(tb.Text);

        private string GetText(Guna2TextBox tb) =>
            tb?.Text ?? string.Empty;

        // Event handlers for designer events
        private void closeButton1_Load(object sender, EventArgs e) { }

        private void btnWhite_Click(object sender, EventArgs e)
        {
            CancelRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            // Designer should be wired to this handler
            SaveVehicle();
        }
    }
}
