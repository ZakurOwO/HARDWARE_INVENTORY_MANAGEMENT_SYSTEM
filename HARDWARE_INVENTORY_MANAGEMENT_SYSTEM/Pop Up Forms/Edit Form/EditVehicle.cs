using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System.Data.SqlClient;

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
            InitializeStatusComboBox();
            WireUpButtons();
        }

        private void InitializeStatusComboBox()
        {
            VehicleStatusComboBox.Items.Clear();
            VehicleStatusComboBox.Items.AddRange(new object[]
            {
                "Available",
                "On Delivery",
                "Maintenance",
                "In Use"
            });
            VehicleStatusComboBox.SelectedIndex = 0;
        }

        private void WireUpButtons()
        {
            if (btnBlue != null)
                btnBlue.Click += (s, e) => SaveVehicle();

            if (btnWhite != null)
                btnWhite.Click += (s, e) => CancelRequested?.Invoke(this, EventArgs.Empty);

            if (closeButton1 != null)
                closeButton1.Click += (s, e) => CancelRequested?.Invoke(this, EventArgs.Empty);
        }

        public void LoadVehicleData(VehicleRecord vehicle)
        {
            currentVehicle = vehicle;

            tbxVehicleName.Text = vehicle.Brand ?? string.Empty;
            VehicleModelTextBox.Text = vehicle.Model ?? string.Empty;
            YearBoughtTextBox.Text = vehicle.Capacity ?? string.Empty;
            PlateNumberTextBox.Text = vehicle.PlateNumber ?? string.Empty;

            VehicleStatusComboBox.SelectedItem = vehicle.Status ?? "Available";

            if (!string.IsNullOrEmpty(vehicle.ImagePath))
            {
                ImageUploadBox.Text = Path.GetFileName(vehicle.ImagePath);
                selectedImagePath = VehicleImageManager.GetFullImagePath(vehicle.ImagePath);
            }
        }

        private void btnBlue_Click(object sender, EventArgs e) => SaveVehicle();
        private void btnWhite_Click(object sender, EventArgs e) => CancelRequested?.Invoke(this, EventArgs.Empty);

        private void SaveVehicle()
        {
            if (!ValidateInputs())
                return;

            try
            {
                VehicleRecord updatedVehicle = new VehicleRecord
                {
                    VehicleInternalID = currentVehicle.VehicleInternalID,
                    VehicleID = currentVehicle.VehicleID,
                    Brand = GetText(tbxVehicleName).Trim(),
                    Model = GetText(VehicleModelTextBox).Trim(),
                    VehicleType = "Drop-Side Truck",
                    Capacity = GetText(YearBoughtTextBox).Trim(),
                    PlateNumber = GetText(PlateNumberTextBox).Trim(),
                    Status = VehicleStatusComboBox.SelectedItem?.ToString() ?? "Available"
                };

                if (vehicleDataAccess.PlateNumberExists(updatedVehicle.PlateNumber, currentVehicle.VehicleInternalID))
                {
                    MessageBox.Show("A vehicle with this plate number already exists!",
                        "Duplicate Plate Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PlateNumberTextBox.Focus();
                    return;
                }

                // Handle image
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    string savedImagePath = VehicleImageManager.SaveVehicleImage(selectedImagePath);
                    updatedVehicle.ImagePath = savedImagePath;
                }
                else
                {
                    updatedVehicle.ImagePath = currentVehicle.ImagePath ?? "2000-Isuzu-mini-dump-truck-970-3730728_1 1.png";
                }

                // Build old/new values for audit
                string oldValues = BuildVehicleState(currentVehicle);
                string newValues = BuildVehicleState(updatedVehicle);

                vehicleDataAccess.UpdateVehicle(updatedVehicle);
                LogVehicleAudit(
                    activity: $"Updated vehicle {updatedVehicle.PlateNumber}",
                    activityType: "UPDATE",
                    recordId: updatedVehicle.PlateNumber,
                    oldValues: oldValues,
                    newValues: newValues
                );

                MessageBox.Show("Vehicle updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                VehicleSaved?.Invoke(this, EventArgs.Empty);
                currentVehicle = updatedVehicle;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving vehicle: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogVehicleAudit(
            string activity,
            string activityType,
            string recordId,
            string oldValues,
            string newValues)
        {
            int userId = UserSession.UserId;
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
                cmd.Parameters.AddWithValue("@ip_address", "127.0.0.1");

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private string BuildVehicleState(VehicleRecord v)
        {
            if (v == null) return null;
            return $"Brand={v.Brand}; Model={v.Model}; Type={v.VehicleType}; " +
                   $"Capacity={v.Capacity}; PlateNumber={v.PlateNumber}; " +
                   $"Status={v.Status}; ImagePath={v.ImagePath}";
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
