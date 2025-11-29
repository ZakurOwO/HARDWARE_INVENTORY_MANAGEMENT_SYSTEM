using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using Guna.UI2.WinForms;
using System.IO;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class DeliveriesMainPage2 : UserControl
    {
        private VehicleDataAccess vehicleData;
        private List<VehicleRecord> vehicles;
        private List<VehicleRecord> allVehicles;
        private DataTable vehiclesDataTable;
        private Pagination_Deliveries pagination;
        private VehicleFormContainer vehicleFormContainer;

        public event EventHandler VehicleAdded;

        public DeliveriesMainPage2()
        {
            InitializeComponent();

            try
            {
                vehicleData = new VehicleDataAccess();
                vehicleFormContainer = new VehicleFormContainer();

                if (!vehicleData.TestConnection())
                {
                    MessageBox.Show("Cannot connect to database. Please check your connection.",
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create and position the Guna2Button
                CreateGuna2Button();

                // Initialize search functionality
                InitializeSearch();

                // Initialize pagination control - MUST be done before LoadVehicles
                InitializePaginationControl();

                // Load vehicles
                LoadVehicles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing vehicle management: {ex.Message}",
                    "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializePaginationControl()
        {


            // Remove existing pagination if any
            if (pagination != null)
            {
                pagination.PageChanged -= Pagination_PageChanged;
                this.Controls.Remove(pagination);
                pagination.Dispose();
            }

            // Create new pagination control
            pagination = new Pagination_Deliveries();

            // Wire up the PageChanged event BEFORE adding to controls
            pagination.PageChanged += Pagination_PageChanged;

            // FIXED: Position it at the BOTTOM of the visible area
            // DeliveriesMainPage2 size is 935x593, so position near bottom
            pagination.Size = new Size(900, 50);
            pagination.Location = new Point(3, 570);

            // Add to THIS control's Controls collection
            this.Controls.Add(pagination);

            // Bring to front and ensure visibility
            pagination.BringToFront();
            pagination.Visible = true;
            pagination.Enabled = true;
        }

          

        private void CreateGuna2Button()
        {
            // Remove the problematic deliveries_btncs1 if it exists
            if (deliveries_btncs1 != null)
            {
                this.Controls.Remove(deliveries_btncs1);
                deliveries_btncs1.Dispose();
                deliveries_btncs1 = null;
            }

            // Create a Guna2Button with the exact styling you provided
            var btnAddVehicle = new Guna2Button();
            btnAddVehicle.BorderRadius = 8;
            btnAddVehicle.DisabledState.BorderColor = Color.DarkGray;
            btnAddVehicle.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAddVehicle.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAddVehicle.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAddVehicle.FillColor = Color.FromArgb(0, 110, 196);

            btnAddVehicle.Font = new Font("Lexend SemiBold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAddVehicle.ForeColor = Color.White;
            btnAddVehicle.Location = new Point(740, 15);
            btnAddVehicle.Size = new Size(150, 40);
            btnAddVehicle.TabIndex = 3;
            btnAddVehicle.Text = "Add New Vehicle";
            btnAddVehicle.Image = Resources.add_circle;
            btnAddVehicle.ImageAlign = HorizontalAlignment.Center;

            // Add click event
            btnAddVehicle.Click += (s, e) =>
            {
                ShowAddVehicleForm(null);
            };

            // Add to controls
            this.Controls.Add(btnAddVehicle);
            btnAddVehicle.BringToFront();
        }

        #region Search Functionality

        private void InitializeSearch()
        {
            // Find the TextBox inside the SearchField control
            TextBox searchTextBox = FindTextBoxInSearchField();
            if (searchTextBox != null)
            {
                searchTextBox.TextChanged += SearchField_TextChanged;
                searchTextBox.MaxLength = 50;
            }
        }

        private TextBox FindTextBoxInSearchField()
        {
            if (searchField1 != null)
            {
                return FindControlRecursive<TextBox>(searchField1);
            }
            return null;
        }

        private T FindControlRecursive<T>(Control parent) where T : Control
        {
            if (parent == null) return null;

            foreach (Control control in parent.Controls)
            {
                if (control is T found)
                {
                    return found;
                }

                var child = FindControlRecursive<T>(control);
                if (child != null)
                {
                    return child;
                }
            }
            return null;
        }

        private void SearchField_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null) return;

            string searchText = textBox.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadVehicles(); // Reload all vehicles
            }
            else
            {
                SearchVehicles(searchText);
            }
        }

        #endregion

        private void LoadVehicles()
        {
            try
            {
                // Check if vehicleData is null
                if (vehicleData == null)
                {
                    MessageBox.Show("Vehicle data access is not initialized.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Load all vehicles from database
                allVehicles = vehicleData.GetActiveVehicles();

                // Check if GetActiveVehicles returned null
                if (allVehicles == null)
                {
                    allVehicles = new List<VehicleRecord>(); // Initialize empty list
                }

                vehicles = new List<VehicleRecord>(allVehicles);

                // Convert to DataTable for pagination
                ConvertVehiclesToDataTable();

                // Initialize pagination with data
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
                    vehicle.ImagePath
                );
            }
        }

        private void InitializeOrUpdatePagination()
        {
            if (pagination == null)
            {
                InitializePaginationControl();
            }

            if (pagination != null && vehiclesDataTable != null)
            {
                // Initialize pagination with the data
                pagination.InitializePagination(vehiclesDataTable, 6);

                // Force display of first page
                DisplayCurrentPage();

                // Force refresh of pagination display
                pagination.ForceShow();
                pagination.Refresh();
            }
        }

        private void Pagination_PageChanged(object sender, int pageNumber)
        {
            DisplayCurrentPage();
        }

        private void DisplayCurrentPage()
        {
            if (pagination == null)
            {
                return;
            }

            // Clear the flowLayoutPanel first
            flowLayoutPanel1.Controls.Clear();

            DataTable currentPageData = pagination.GetCurrentPageData();

            if (currentPageData == null)
            {
                ShowNoResultsMessage();
                return;
            }

            if (currentPageData.Rows.Count == 0)
            {
                ShowNoResultsMessage();
                return;
            }

            // Display exactly 6 vehicles per page (or less if it's the last page)
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

                // Create VehiclesInfoBox control for each vehicle
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

                // Load vehicle image
                LoadVehicleImage(vehicle, vehicleControl);

                // Add event handlers for edit and delete
                vehicleControl.EditClicked += (s, e) => EditVehicle(vehicle);
                vehicleControl.DeleteClicked += (s, e) => DeleteVehicle(vehicle);

                // Add to flowLayoutPanel
                flowLayoutPanel1.Controls.Add(vehicleControl);
            }
        }

        private void ShowNoResultsMessage()
        {
            string searchText = GetSearchText();
            string message = string.IsNullOrEmpty(searchText)
                ? "No vehicles available"
                : $"No vehicles found matching '{searchText}'";

            Label noVehiclesLabel = new Label()
            {
                Text = message,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                ForeColor = Color.Gray,
                AutoSize = false,
                Height = 100
            };
            flowLayoutPanel1.Controls.Add(noVehiclesLabel);
        }

        private void LoadVehicleImage(VehicleRecord vehicle, VehiclesInfoBox vehicleControl)
        {
            if (!string.IsNullOrEmpty(vehicle.ImagePath))
            {
                try
                {
                    var vehicleImage = VehicleImageManager.GetVehicleImage(vehicle.ImagePath);
                    if (vehicleImage != null)
                    {
                        vehicleControl.Image = vehicleImage;
                    }
                }
                catch (Exception ex)
                {
                    // Image loading failed, control will use default image
                }
            }
        }

        private void EditVehicle(VehicleRecord vehicle)
        {
            ShowEditVehicleForm(vehicle);
        }

        private void ShowEditVehicleForm(VehicleRecord vehicle)
        {
            var main = this.FindForm() as MainDashBoard;
            if (main != null)
            {
                vehicleFormContainer.ShowEditVehicleForm(main, vehicle);
                VehicleAdded?.Invoke(this, EventArgs.Empty);
            }
        }

        private void DeleteVehicle(VehicleRecord vehicle)
        {
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete vehicle {vehicle.Brand} {vehicle.Model}?\nPlate Number: {vehicle.PlateNumber}",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    vehicleData.DeleteVehicle(vehicle.VehicleInternalID);
                    MessageBox.Show("Vehicle deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVehicles(); // Refresh the list
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting vehicle: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Search vehicles method
        private void SearchVehicles(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    LoadVehicles(); // Show all if search is empty
                }
                else
                {
                    // Search in database
                    var searchResults = vehicleData.SearchVehicles(searchTerm);

                    vehicles = searchResults;
                    allVehicles = searchResults; // Update allVehicles for filtering

                    // Convert search results to DataTable
                    ConvertVehiclesToDataTable();

                    // Update pagination with search results
                    InitializeOrUpdatePagination();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching vehicles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowAddVehicleForm(VehicleRecord vehicle = null)
        {
            var main = this.FindForm() as MainDashBoard;
            if (main != null)
            {
                vehicleFormContainer.ShowAddVehicleForm(main, vehicle);
                VehicleAdded?.Invoke(this, EventArgs.Empty);
            }
        }

        public void CloseAddVehicleForm()
        {
            vehicleFormContainer.CloseAddVehicleForm();
        }

        public void RefreshVehicles()
        {
            LoadVehicles();
        }

        // Search field event - legacy support
        private void searchField1_SearchTextChanged(object sender, string searchText)
        {
            SearchVehicles(searchText);
        }

        private string GetSearchText()
        {
            TextBox textBox = FindTextBoxInSearchField();
            return textBox?.Text?.Trim() ?? "";
        }

        // Other existing event handlers
        private void deliveriesSlideButtons2_Load(object sender, EventArgs e) { }
        private void vehiclesInfoBox3_Load(object sender, EventArgs e) { }
        private void deliveries_btncs1_Load(object sender, EventArgs e) { }

        // Add this to handle control resize
        private void DeliveriesMainPage2_Resize(object sender, EventArgs e)
        {
            // Reposition pagination when control is resized
            if (pagination != null)
            {
                pagination.Location = new Point(8, this.Height - 50);
                pagination.Width = this.Width - 16;
                pagination.BringToFront();
            }
        }
    }
}