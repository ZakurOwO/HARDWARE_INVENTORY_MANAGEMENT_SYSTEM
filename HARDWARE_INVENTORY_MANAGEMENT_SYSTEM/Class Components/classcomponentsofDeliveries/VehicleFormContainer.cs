using System;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public class VehicleFormContainer
    {
        private Panel scrollContainer;
        private AddVehicleForm addForm;
        private EditVehicle editForm;
        private MainDashBoard mainForm;

        // Show Add Vehicle Form
        public void ShowAddVehicleForm(MainDashBoard main, VehicleRecord vehicle = null)
        {
            mainForm = main;

            // Create the AddVehicleForm
            addForm = new AddVehicleForm();

            // Load vehicle data if editing
            if (vehicle != null)
                addForm.LoadVehicleData(vehicle);

            // Wire up events
            addForm.VehicleSaved += AddForm_VehicleSaved;
            addForm.CancelRequested += AddForm_CancelRequested;

            // Create scroll container
            scrollContainer = new Panel();
            scrollContainer.Size = new Size(578, 550);
            scrollContainer.Location = new Point(
                (main.Width - scrollContainer.Width) / 2,
                (main.Height - scrollContainer.Height) / 2
            );
            
            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            
            

            // Add form to container
            scrollContainer.Controls.Add(addForm);
            addForm.Size = new Size(578, 550);
            addForm.Location = new Point(0, 0);

            // Set up main form overlay
            ShowOverlay();

            // Add container to main form
            mainForm.Controls.Add(scrollContainer);
            scrollContainer.BringToFront();
        }

        // Show Edit Vehicle Form
        public void ShowEditVehicleForm(MainDashBoard main, VehicleRecord vehicle)
        {
            if (vehicle == null)
            {
                MessageBox.Show("No vehicle selected to edit!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mainForm = main;

            // Create the EditVehicle form
            editForm = new EditVehicle();

            // Load vehicle data
            editForm.LoadVehicleData(vehicle);

            // Wire up events
            editForm.VehicleSaved += EditForm_VehicleSaved;
            editForm.CancelRequested += EditForm_CancelRequested;

            // Create scroll container
            scrollContainer = new Panel();
            scrollContainer.Size = new Size(578, 597);
            scrollContainer.Location = new Point(
                (main.Width - scrollContainer.Width) / 2,
                (main.Height - scrollContainer.Height) / 2
            );
            
            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            
            // Add form to container
            scrollContainer.Controls.Add(editForm);
            editForm.Size = new Size(578, 597);
            editForm.Location = new Point(0, 0);

            // Set up main form overlay
            ShowOverlay();

            // Add container to main form
            mainForm.Controls.Add(scrollContainer);
            scrollContainer.BringToFront();
        }

        private void ShowOverlay()
        {
            if (mainForm.pcbBlurOverlay != null)
            {
                mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.VehicleOverlay;
                mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
                mainForm.pcbBlurOverlay.Visible = true;
                mainForm.pcbBlurOverlay.BringToFront();
            }
        }

        private void AddForm_VehicleSaved(object sender, EventArgs e)
        {
            CloseForm();
            RefreshVehiclesList();
        }

        private void AddForm_CancelRequested(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void EditForm_VehicleSaved(object sender, EventArgs e)
        {
            CloseForm();
            RefreshVehiclesList();
        }

        private void EditForm_CancelRequested(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void RefreshVehiclesList()
        {
            // Refresh the vehicles list in the main page
            if (mainForm?.MainContentPanelAccess?.Controls.Count > 0)
            {
                var deliveriesPage = mainForm.MainContentPanelAccess.Controls[0] as DeliveriesMainPage2;
                deliveriesPage?.RefreshVehicles();
            }
        }

        public void CloseForm()
        {
            // Hide overlay
            if (mainForm != null && mainForm.pcbBlurOverlay != null)
            {
                mainForm.pcbBlurOverlay.Visible = false;
            }

            // Clean up add form
            if (addForm != null)
            {
                addForm.VehicleSaved -= AddForm_VehicleSaved;
                addForm.CancelRequested -= AddForm_CancelRequested;
                addForm.Dispose();
                addForm = null;
            }

            // Clean up edit form
            if (editForm != null)
            {
                editForm.VehicleSaved -= EditForm_VehicleSaved;
                editForm.CancelRequested -= EditForm_CancelRequested;
                editForm.Dispose();
                editForm = null;
            }

            // Clean up container
            if (scrollContainer != null)
            {
                scrollContainer.Controls.Clear();
                scrollContainer.Parent?.Controls.Remove(scrollContainer);
                scrollContainer.Dispose();
                scrollContainer = null;
            }
        }

        // Legacy method name for backward compatibility
        public void CloseAddVehicleForm()
        {
            CloseForm();
        }

        public bool IsVisible()
        {
            return scrollContainer != null && scrollContainer.Visible;
        }
    }
}