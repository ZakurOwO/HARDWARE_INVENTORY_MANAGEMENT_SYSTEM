using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Deliveries_Report
{
    public partial class DeliveriesPage2 : UserControl
    {
        private DeliveriesDataAccess deliveriesData;
        private DataTable vehicleData;

        public DeliveriesPage2()
        {
            InitializeComponent();
            deliveriesData = new DeliveriesDataAccess();
            //LoadVehicleData();
        }

        //private void LoadVehicleData()
        //{
        //    try
        //    {
        //        vehicleData = deliveriesData.GetVehicleUtilization();
        //        dgvVehicleUtilization.DataSource = vehicleData;
        //        FormatVehicleGrid();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error loading vehicle data: {ex.Message}", "Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void FormatVehicleGrid()
        //{
        //    if (dgvVehicleUtilization.Columns.Count > 0)
        //    {
        //        dgvVehicleUtilization.Columns["PlateNumber"].HeaderText = "Plate Number";
        //        dgvVehicleUtilization.Columns["Brand"].HeaderText = "Brand";
        //        dgvVehicleUtilization.Columns["Model"].HeaderText = "Model";
        //        dgvVehicleUtilization.Columns["VehicleType"].HeaderText = "Vehicle Type";
        //        dgvVehicleUtilization.Columns["Status"].HeaderText = "Status";
        //        dgvVehicleUtilization.Columns["TotalAssignments"].HeaderText = "Total Assignments";

        //        // Color coding for status
        //        foreach (DataGridViewRow row in dgvVehicleUtilization.Rows)
        //        {
        //            if (row.Cells["Status"]?.Value != null)
        //            {
        //                string status = row.Cells["Status"].Value.ToString();
        //                row.DefaultCellStyle.BackColor = GetVehicleStatusColor(status);
        //            }
        //        }

        //        dgvVehicleUtilization.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //    }
        //}

        private Color GetVehicleStatusColor(string status)
        {
            switch (status?.ToLower())
            {
                case "available":
                    return Color.FromArgb(220, 255, 220); // Light green
                case "on delivery":
                    return Color.FromArgb(255, 255, 200); // Light yellow
                case "maintenance":
                    return Color.FromArgb(255, 220, 220); // Light red
                default:
                    return Color.White;
            }
        }

    }
}