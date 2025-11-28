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
        private List<VehicleRecord> allVehicles; // Store all vehicles for filtering
        private DataTable vehiclesDataTable;
        private Pagination_Deliveries pagination;
        private VehicleFormContainer vehicleFormContainer;

        public event EventHandler VehicleAdded;

        public DeliveriesMainPage2()
        {
            InitializeComponent();

            vehicleData = new VehicleDataAccess();
            vehicleFormContainer = new VehicleFormContainer();

            // Create and position the Guna2Button at (737, 4)
            CreateGuna2Button();

            // Add debug buttons
            AddDebugButtons();

            // Initialize search functionality
            InitializeSearch();

            InitializePagination();
            LoadVehicles();
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

            // Use optimal font size and button width for "ADD VEHICLE" text
            btnAddVehicle.Font = new Font("Lexend SemiBold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAddVehicle.ForeColor = Color.White;
            btnAddVehicle.Location = new Point(740, 15);

            // Increased width to better fit the text
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

        private void AddDebugButtons()
        {
            // Debug Images button
            var debugBtn = new Button();
            debugBtn.Text = "Debug Images";
            debugBtn.Location = new Point(600, 4);
            debugBtn.Size = new Size(100, 40);
            debugBtn.BackColor = Color.Orange;
            debugBtn.ForeColor = Color.White;
            debugBtn.Click += (s, e) => DebugImageLoading();
            this.Controls.Add(debugBtn);

            // Test Display button
            var testBtn = new Button();
            testBtn.Text = "Test Display";
            testBtn.Location = new Point(500, 4);
            testBtn.Size = new Size(90, 40);
            testBtn.BackColor = Color.Green;
            testBtn.ForeColor = Color.White;
            testBtn.Click += (s, e) => TestImageDisplay();
            this.Controls.Add(testBtn);
        }

        #region Search Functionality

        private void InitializeSearch()
        {
            // Find the TextBox inside the SearchField control
            TextBox searchTextBox = FindTextBoxInSearchField();
            if (searchTextBox != null)
            {
                searchTextBox.TextChanged += SearchField_TextChanged;
                searchTextBox.MaxLength = 50; // Set reasonable max length
            }
        }

        private TextBox FindTextBoxInSearchField()
        {
            // Assuming searchField1 is your SearchField control
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
                // Show all vehicles if search is empty
                vehicles = new List<VehicleRecord>(allVehicles);
            }
            else
            {
                // Filter vehicles based on search text
                FilterVehicles(searchText);
            }

            // Update the display with filtered results
            ConvertVehiclesToDataTable();
            InitializeOrUpdatePagination();
        }

        private void FilterVehicles(string searchText)
        {
            // Progressive search - case insensitive matching
            vehicles = allVehicles.Where(vehicle =>
                // Search by Brand
                (!string.IsNullOrEmpty(vehicle.Brand) &&
                 vehicle.Brand.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||

                // Search by Model
                (!string.IsNullOrEmpty(vehicle.Model) &&
                 vehicle.Model.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||

                // Search by Plate Number
                (!string.IsNullOrEmpty(vehicle.PlateNumber) &&
                 vehicle.PlateNumber.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||

                // Search by Vehicle Type
                (!string.IsNullOrEmpty(vehicle.VehicleType) &&
                 vehicle.VehicleType.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||

                // Search by Status
                (!string.IsNullOrEmpty(vehicle.Status) &&
                 vehicle.Status.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||

                // Search by VehicleID
                (!string.IsNullOrEmpty(vehicle.VehicleID) &&
                 vehicle.VehicleID.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
            ).ToList();
        }

        private string GetSearchText()
        {
            TextBox textBox = FindTextBoxInSearchField();
            return textBox?.Text?.Trim() ?? "";
        }

        #endregion

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
                // Load all vehicles from database
                allVehicles = vehicleData.GetActiveVehicles();
                vehicles = new List<VehicleRecord>(allVehicles); // Create a copy for display

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
                ShowNoResultsMessage();
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

                // Enhanced image loading with detailed debugging
                LoadVehicleImage(vehicle, vehicleControl);

                vehicleControl.EditClicked += (s, e) => EditVehicle(vehicle);
                vehicleControl.DeleteClicked += (s, e) => DeleteVehicle(vehicle);

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
                ForeColor = Color.Gray
            };
            flowLayoutPanel1.Controls.Add(noVehiclesLabel);
        }

        private void LoadVehicleImage(VehicleRecord vehicle, VehiclesInfoBox vehicleControl)
        {
            if (!string.IsNullOrEmpty(vehicle.ImagePath))
            {
                try
                {
                    Console.WriteLine($"=== Loading Image for {vehicle.Brand} {vehicle.Model} ===");
                    Console.WriteLine($"Image Path: {vehicle.ImagePath}");
                    Console.WriteLine($"Full Path: {VehicleImageManager.GetFullImagePath(vehicle.ImagePath)}");
                    Console.WriteLine($"Image Exists: {VehicleImageManager.ImageExists(vehicle.ImagePath)}");

                    var vehicleImage = VehicleImageManager.GetVehicleImage(vehicle.ImagePath);
                    if (vehicleImage != null)
                    {
                        Console.WriteLine($"Image loaded successfully: {vehicleImage.Size}");
                        vehicleControl.Image = vehicleImage;
                    }
                    else
                    {
                        Console.WriteLine($"Image is NULL - using default");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading image: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"No image path for {vehicle.Brand} {vehicle.Model}");
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
                    LoadVehicles();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting vehicle: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Legacy search method - kept for backward compatibility
        private void SearchVehicles(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    LoadVehicles();
                }
                else
                {
                    vehicles = vehicleData.SearchVehicles(searchTerm);
                    ConvertVehiclesToDataTable();
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

        // Debug Methods
        private void DebugImageLoading()
        {
            try
            {
                string debugInfo = "=== IMAGE DEBUGGING ===\n\n";

                // Test the image directory
                debugInfo += $"Image Directory: {VehicleImageManager.GetImageBasePath()}\n";
                debugInfo += $"Directory Exists: {Directory.Exists(VehicleImageManager.GetImageBasePath())}\n";

                // Test with a known image
                string testImagePath = "2000-Isuzu-mini-dump-truck-970-3730728_1 1.png";
                debugInfo += $"\nTesting with image: {testImagePath}\n";
                debugInfo += $"Image Exists: {VehicleImageManager.ImageExists(testImagePath)}\n";

                // Try to load the image
                try
                {
                    Image testImage = VehicleImageManager.GetVehicleImage(testImagePath);
                    debugInfo += $"Image Loaded: {(testImage != null ? "SUCCESS" : "FAILED")}\n";
                    debugInfo += $"Image Size: {testImage.Size}\n";
                    testImage.Dispose();
                }
                catch (Exception ex)
                {
                    debugInfo += $"Image Load Error: {ex.Message}\n";
                }

                // Check current vehicles
                if (vehicles != null && vehicles.Count > 0)
                {
                    debugInfo += $"\n=== VEHICLE DATA ===\n";
                    debugInfo += $"Total Vehicles: {vehicles.Count}\n";

                    foreach (var vehicle in vehicles.Take(5)) // Check first 5 vehicles
                    {
                        debugInfo += $"\nVehicle: {vehicle.Brand} {vehicle.Model}\n";
                        debugInfo += $"Image Path: '{vehicle.ImagePath}'\n";
                        debugInfo += $"Image Exists: {VehicleImageManager.ImageExists(vehicle.ImagePath)}\n";

                        if (!string.IsNullOrEmpty(vehicle.ImagePath))
                        {
                            try
                            {
                                Image img = VehicleImageManager.GetVehicleImage(vehicle.ImagePath);
                                debugInfo += $"Image Loaded: {(img != null ? "SUCCESS" : "FAILED")}\n";
                                img.Dispose();
                            }
                            catch (Exception ex)
                            {
                                debugInfo += $"Load Error: {ex.Message}\n";
                            }
                        }
                    }
                }
                else
                {
                    debugInfo += "\nNo vehicles loaded!\n";
                }

                MessageBox.Show(debugInfo, "Image Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Debug Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TestImageDisplay()
        {
            try
            {
                // Create a test form with an image
                Form testForm = new Form();
                testForm.Text = "Image Test";
                testForm.Size = new Size(400, 500);

                // Try to load an image from the first vehicle
                if (vehicles != null && vehicles.Count > 0)
                {
                    var firstVehicle = vehicles.First();

                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Size = new Size(350, 200);
                    pictureBox.Location = new Point(20, 20);
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.BorderStyle = BorderStyle.FixedSingle;

                    Label infoLabel = new Label();
                    infoLabel.Text = $"Vehicle: {firstVehicle.Brand} {firstVehicle.Model}\nImage: {firstVehicle.ImagePath}";
                    infoLabel.Location = new Point(20, 240);
                    infoLabel.Size = new Size(350, 60);

                    // Try to load the image
                    if (!string.IsNullOrEmpty(firstVehicle.ImagePath))
                    {
                        Image img = VehicleImageManager.GetVehicleImage(firstVehicle.ImagePath);
                        if (img != null)
                        {
                            pictureBox.Image = img;

                            Label statusLabel = new Label();
                            statusLabel.Text = $"Image loaded successfully!\nSize: {img.Size}";
                            statusLabel.Location = new Point(20, 310);
                            statusLabel.Size = new Size(350, 40);
                            statusLabel.ForeColor = Color.Green;
                            testForm.Controls.Add(statusLabel);
                        }
                        else
                        {
                            pictureBox.Image = CreateTestImage();
                            Label statusLabel = new Label();
                            statusLabel.Text = "Image is NULL - using test image";
                            statusLabel.Location = new Point(20, 310);
                            statusLabel.Size = new Size(350, 40);
                            statusLabel.ForeColor = Color.Red;
                            testForm.Controls.Add(statusLabel);
                        }
                    }
                    else
                    {
                        pictureBox.Image = CreateTestImage();
                        Label statusLabel = new Label();
                        statusLabel.Text = "No image path - using test image";
                        statusLabel.Location = new Point(20, 310);
                        statusLabel.Size = new Size(350, 40);
                        statusLabel.ForeColor = Color.Orange;
                        testForm.Controls.Add(statusLabel);
                    }

                    testForm.Controls.Add(pictureBox);
                    testForm.Controls.Add(infoLabel);
                }
                else
                {
                    Label noDataLabel = new Label();
                    noDataLabel.Text = "No vehicles found to test!";
                    noDataLabel.Location = new Point(20, 20);
                    noDataLabel.Size = new Size(350, 30);
                    testForm.Controls.Add(noDataLabel);
                }

                testForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Test failed: {ex.Message}", "Test Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Image CreateTestImage()
        {
            Bitmap bmp = new Bitmap(200, 150);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightBlue);
                g.DrawString("TEST IMAGE", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 10, 50);
                g.DrawRectangle(Pens.Black, 0, 0, 199, 149);
            }
            return bmp;
        }

        // Search field event - legacy support
        private void searchField1_SearchTextChanged(object sender, string searchText)
        {
            SearchVehicles(searchText);
        }

        // Other existing event handlers
        private void deliveriesSlideButtons2_Load(object sender, EventArgs e) { }
        private void vehiclesInfoBox3_Load(object sender, EventArgs e) { }
    }
}