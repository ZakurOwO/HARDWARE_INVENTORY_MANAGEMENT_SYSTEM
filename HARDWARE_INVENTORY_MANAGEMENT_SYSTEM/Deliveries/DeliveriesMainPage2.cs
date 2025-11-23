using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class DeliveriesMainPage2 : UserControl
    {
        private VehicleDataAccess vehicleData;
        private List<VehicleRecord> vehicles;
        private DataTable vehiclesDataTable;
        private Pagination_Deliveries pagination;
        private AddVehicleForm addVehicleForm;

        public DeliveriesMainPage2()
        {
            InitializeComponent();
            vehicleData = new VehicleDataAccess();
            WireUpAddButton();
            InitializePagination();
            LoadVehicles();
        }

        private void WireUpAddButton()
        {
            try
            {
                Control foundControl = FindControlRecursive(this, "mainButtonWithIcon1");
                if (foundControl != null)
                {
                    foundControl.Click += (s, e) => ShowAddVehicleFormDirectly();
                }
            }
            catch
            {
                // Silent fail - button will use designer event if wiring fails
            }
        }

        private Control FindControlRecursive(Control parent, string name)
        {
            if (parent.Name == name)
                return parent;

            foreach (Control child in parent.Controls)
            {
                Control found = FindControlRecursive(child, name);
                if (found != null)
                    return found;
            }
            return null;
        }

        private void ShowAddVehicleFormDirectly()
        {
            try
            {
                Debug.WriteLine("=== ShowAddVehicleFormDirectly ===");

                // Hide main content instead of clearing
                flowLayoutPanel1.Visible = false;
                if (pagination != null)
                    pagination.Visible = false;

                // Clear any existing form
                if (addVehicleForm != null)
                {
                    addVehicleForm.VehicleSaved -= AddVehicleForm_VehicleSaved;
                    addVehicleForm.CancelRequested -= AddVehicleForm_CancelRequested;
                    guna2Panel1.Controls.Remove(addVehicleForm);
                    addVehicleForm.Dispose();
                }

                // Create new form
                addVehicleForm = new AddVehicleForm();

                // Make it highly visible for testing
                addVehicleForm.BackColor = Color.LightBlue;
                addVehicleForm.BorderStyle = BorderStyle.FixedSingle;

                // Set size and position - make it smaller than panel for testing
                addVehicleForm.Size = new Size(600, 400);
                addVehicleForm.Location = new Point(
                    (guna2Panel1.Width - addVehicleForm.Width) / 2,
                    (guna2Panel1.Height - addVehicleForm.Height) / 2
                );

                // Subscribe to events
                addVehicleForm.VehicleSaved += AddVehicleForm_VehicleSaved;
                addVehicleForm.CancelRequested += AddVehicleForm_CancelRequested;

                // Add to panel
                guna2Panel1.Controls.Add(addVehicleForm);
                addVehicleForm.BringToFront();

                Debug.WriteLine($"Form added - Parent: {addVehicleForm.Parent}, Bounds: {addVehicleForm.Bounds}");

                // Test if form is visible
                if (addVehicleForm.Visible && addVehicleForm.Parent != null)
                {
                    MessageBox.Show($"AddVehicleForm should be visible!\nSize: {addVehicleForm.Size}\nLocation: {addVehicleForm.Location}",
                        "Form Added Successfully");
                }
                else
                {
                    MessageBox.Show("Form is not properly initialized", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n\nStack Trace: {ex.StackTrace}", "Detailed Error");
            }
        }

        private void InitializePagination()
        {
            pagination = new Pagination_Deliveries();
            pagination.Location = new Point(12, 559);
            pagination.Size = new Size(920, 34);
            pagination.Visible = true;
            pagination.PageChanged += Pagination_PageChanged;

            this.guna2Panel1.Controls.Add(pagination);
            pagination.BringToFront();
        }

        private void LoadVehicles()
        {
            try
            {
                vehicles = vehicleData.GetActiveVehicles();
                ConvertVehiclesToDataTable();
                InitializeOrUpdatePagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConvertVehiclesToDataTable()
        {
            vehiclesDataTable = new DataTable();
            vehiclesDataTable.Columns.Add("VehicleInternalID", typeof(int));
            vehiclesDataTable.Columns.Add("VehicleID", typeof(string));
            vehiclesDataTable.Columns.Add("PlateNumber", typeof(string));
            vehiclesDataTable.Columns.Add("Brand", typeof(string));
            vehiclesDataTable.Columns.Add("Model", typeof(string));
            vehiclesDataTable.Columns.Add("VehicleType", typeof(string));
            vehiclesDataTable.Columns.Add("Capacity", typeof(string));
            vehiclesDataTable.Columns.Add("Status", typeof(string));
            vehiclesDataTable.Columns.Add("ImagePath", typeof(string));
            vehiclesDataTable.Columns.Add("VehicleName", typeof(string));

            foreach (var vehicle in vehicles)
            {
                vehiclesDataTable.Rows.Add(
                    vehicle.VehicleInternalID,
                    vehicle.VehicleID,
                    vehicle.PlateNumber,
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.VehicleType,
                    vehicle.Capacity,
                    vehicle.Status,
                    vehicle.ImagePath,
                    vehicle.VehicleName
                );
            }
        }

        private void InitializeOrUpdatePagination()
        {
            if (pagination != null && vehiclesDataTable != null)
            {
                pagination.InitializePagination(vehiclesDataTable, 6);
                DisplayCurrentPage();
            }
        }

        private void Pagination_PageChanged(object sender, int pageNumber)
        {
            DisplayCurrentPage();
        }

        private void DisplayCurrentPage()
        {
            if (pagination == null) return;

            flowLayoutPanel1.Controls.Clear();

            DataTable currentPageData = pagination.GetCurrentPageData();

            if (currentPageData == null || currentPageData.Rows.Count == 0)
            {
                Label noVehiclesLabel = new Label()
                {
                    Text = "No vehicles available",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    ForeColor = Color.Gray
                };
                flowLayoutPanel1.Controls.Add(noVehiclesLabel);
                return;
            }

            foreach (DataRow row in currentPageData.Rows)
            {
                var vehicle = new VehicleRecord
                {
                    VehicleInternalID = Convert.ToInt32(row["VehicleInternalID"]),
                    VehicleID = row["VehicleID"].ToString(),
                    PlateNumber = row["PlateNumber"].ToString(),
                    Brand = row["Brand"].ToString(),
                    Model = row["Model"].ToString(),
                    VehicleType = row["VehicleType"].ToString(),
                    Capacity = row["Capacity"].ToString(),
                    Status = row["Status"].ToString(),
                    ImagePath = row["ImagePath"].ToString()
                };

                var vehicleControl = new VehiclesInfoBox()
                {
                    Type = vehicle.VehicleType,
                    Vehicle_Name = $"{vehicle.Brand} {vehicle.Model}",
                    Plate_No = vehicle.PlateNumber,
                    Vehicle_ID = vehicle.VehicleID,
                    Status = vehicle.Status,
                    Size = new Size(261, 246),
                    Margin = new Padding(3)
                };

                if (!string.IsNullOrEmpty(vehicle.ImagePath))
                {
                    var vehicleImage = VehicleImageManager.GetVehicleImage(vehicle.ImagePath);
                    vehicleControl.Image = vehicleImage;
                }

                flowLayoutPanel1.Controls.Add(vehicleControl);
            }
        }

        private void SearchVehicles(string searchTerm)
        {
            try
            {
                vehicles = vehicleData.SearchVehicles(searchTerm);
                ConvertVehiclesToDataTable();
                InitializeOrUpdatePagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching vehicles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshVehicles()
        {
            if (addVehicleForm != null && guna2Panel1.Controls.Contains(addVehicleForm))
            {
                guna2Panel1.Controls.Remove(addVehicleForm);
                addVehicleForm = null;
            }

            if (!guna2Panel1.Controls.Contains(flowLayoutPanel1))
            {
                RestoreMainView();
            }

            LoadVehicles();
        }

        private void RestoreMainView()
        {
            guna2Panel1.Controls.Clear();
            guna2Panel1.Controls.Add(flowLayoutPanel1);
            guna2Panel1.Controls.Add(pagination);

            flowLayoutPanel1.BringToFront();
            pagination.BringToFront();
        }

        private void deliveriesSlideButtons2_Load(object sender, EventArgs e)
        {
            // Initialization code if needed
        }

        private void vehiclesInfoBox3_Load(object sender, EventArgs e)
        {
            // Individual vehicle control load event
        }

        private void searchField1_SearchTextChanged(object sender, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadVehicles();
            }
            else
            {
                SearchVehicles(searchText);
            }
        }

        private void AddVehicleForm_VehicleSaved(object sender, EventArgs e)
        {
            CloseAddVehicleForm();
            RefreshVehicles();
        }

        private void AddVehicleForm_CancelRequested(object sender, EventArgs e)
        {
            CloseAddVehicleForm();
        }

        private void CloseAddVehicleForm()
        {
            if (addVehicleForm != null)
            {
                addVehicleForm.VehicleSaved -= AddVehicleForm_VehicleSaved;
                addVehicleForm.CancelRequested -= AddVehicleForm_CancelRequested;

                guna2Panel1.Controls.Remove(addVehicleForm);
                addVehicleForm.Dispose();
                addVehicleForm = null;

                flowLayoutPanel1.Visible = true;
                pagination.Visible = true;
            }
        }

        private void mainButtonWithIcon1_Load(object sender, EventArgs e)
        {
            // Initialization code if needed
        }

        private void mainButtonWithIcon1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("mainButtonWithIcon1_Click event fired!");
            MessageBox.Show("Button click detected!", "Debug");
            ShowAddVehicleFormDirectly();
        }

        private void searchField1_Load(object sender, EventArgs e)
        {

        }
    }
}