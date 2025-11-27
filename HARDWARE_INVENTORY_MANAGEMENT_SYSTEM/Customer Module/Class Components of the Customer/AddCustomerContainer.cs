using System;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public class AddCustomerContainer
    {
        private Panel scrollContainer;
        private AddCustomerForm addCustomerForm;
        private MainDashBoard mainForm;

        public void ShowAddCustomerForm(MainDashBoard main)
        {
            try
            {
                mainForm = main;

                // Create the AddCustomerForm
                addCustomerForm = new AddCustomerForm();
                addCustomerForm.TopLevel = false;
                addCustomerForm.FormBorderStyle = FormBorderStyle.None;
                addCustomerForm.Dock = DockStyle.Fill;

                // SCROLL CONTAINER
                scrollContainer = new Panel();
                scrollContainer.Size = new Size(583, 505);
                scrollContainer.Location = new Point(
                (main.Width - scrollContainer.Width) / 2,
                (main.Height - scrollContainer.Height) / 2
            );
                scrollContainer.BorderStyle = BorderStyle.FixedSingle;
                scrollContainer.AutoScroll = true;

                // Add form into scroll container
                scrollContainer.Controls.Add(addCustomerForm);
                addCustomerForm.Dock = DockStyle.Fill;

                // Make sure the form is properly shown
                addCustomerForm.Show();
                addCustomerForm.BringToFront();

                // Setup overlay
                if (mainForm.pcbBlurOverlay != null)
                {
                    mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.CustomerOvelay;
                    mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
                    mainForm.pcbBlurOverlay.Visible = true;
                    mainForm.pcbBlurOverlay.BringToFront();
                }

                // Add container to main form and bring to front
                mainForm.Controls.Add(scrollContainer);
                scrollContainer.BringToFront();

                // Handle form closed event
                addCustomerForm.FormClosed += (s, e) => CloseAddCustomerForm();

                // Test form functionality after a short delay
                TestFormFunctionality();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing customer form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TestFormFunctionality()
        {
            // Add a small delay to ensure form is fully loaded
            var timer = new Timer();
            timer.Interval = 100;
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                timer.Dispose();

                // Test if we can access form controls
                if (addCustomerForm != null && !addCustomerForm.IsDisposed)
                {
                    DebugFormControls();
                }
            };
            timer.Start();
        }

        private void DebugFormControls()
        {
            try
            {
                // Debug method to check if form controls are accessible
                bool hasCompanyName = addCustomerForm.Controls.Find("tbxCompanyName", true).Length > 0;
                bool hasContactPerson = addCustomerForm.Controls.Find("tbxContactPerson", true).Length > 0;
                bool hasCityCombo = addCustomerForm.Controls.Find("CityCombobox", true).Length > 0;
                bool hasProvinceCombo = addCustomerForm.Controls.Find("ProvinceCombobox", true).Length > 0;
                bool hasAddButton = addCustomerForm.Controls.Find("btnBlue", true).Length > 0;

                string debugInfo = $"Form Controls Found:\n" +
                                 $"Company Name: {hasCompanyName}\n" +
                                 $"Contact Person: {hasContactPerson}\n" +
                                 $"City Combo: {hasCityCombo}\n" +
                                 $"Province Combo: {hasProvinceCombo}\n" +
                                 $"Add Button: {hasAddButton}";

                // Uncomment below for debugging
                // MessageBox.Show(debugInfo, "Form Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Debug error: {ex.Message}", "Debug Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddCustomerForm(object sender, EventArgs e)
        {
            CloseAddCustomerForm();
        }

        public void CloseAddCustomerForm()
        {
            if (mainForm != null)
            {
                mainForm.pcbBlurOverlay.Visible = false;
            }

            if (addCustomerForm != null)
            {
                addCustomerForm.Dispose();
                addCustomerForm = null;
            }

            if (scrollContainer != null)
            {
                scrollContainer.Controls.Clear();
                scrollContainer.Parent?.Controls.Remove(scrollContainer);
                scrollContainer.Dispose();
                scrollContainer = null;
            }
        }

        // Public method to refresh the form if needed
        public void RefreshForm()
        {
            if (addCustomerForm != null && !addCustomerForm.IsDisposed)
            {
                addCustomerForm.Refresh();
            }
        }

        // Method to test if data is being saved (for debugging)
        public void TestSaveFunctionality()
        {
            if (addCustomerForm != null && !addCustomerForm.IsDisposed)
            {
                // Call the test method on the form
                addCustomerForm.TestForm();
            }
        }
    }
}