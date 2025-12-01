using System;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public class EditCustomerContainer
    {
        private Panel scrollContainer;
        private EditCustomerForm editCustomerForm;
        private MainDashBoard mainForm;

        public void ShowEditCustomerForm(MainDashBoard main, int customerId, string customerName, string contactNumber, string address)
        {
            mainForm = main;

            // Create the EditCustomerForm with customer data
            editCustomerForm = new EditCustomerForm(customerId, customerName, contactNumber, address);
            editCustomerForm.TopLevel = false;       // IMPORTANT: treat form as embedded
            editCustomerForm.FormBorderStyle = FormBorderStyle.None;
            editCustomerForm.Dock = DockStyle.None;

            // SCROLL CONTAINER (same as inventory and add customer)
            scrollContainer = new Panel();
            scrollContainer.Size = new Size(583, 505);        // SAME SIZE AS INVENTORY
            scrollContainer.Location = new Point(472, 100);   // SAME POSITION AS INVENTORY
            scrollContainer.BorderStyle = BorderStyle.FixedSingle;


            // add form into scroll container
            scrollContainer.Controls.Add(editCustomerForm);
            editCustomerForm.Size = new Size(583, 813);
            editCustomerForm.Location = new Point(0, 0);
            editCustomerForm.Show();

            // overlay
            mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.CustomerOvelay;
            mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
            mainForm.pcbBlurOverlay.Visible = true;
            mainForm.pcbBlurOverlay.BringToFront();

            // bring container
            mainForm.Controls.Add(scrollContainer);
            scrollContainer.BringToFront();

            // close event - refresh customer list when form is closed
            editCustomerForm.FormClosed += (s, e) =>
            {
                CloseEditCustomerForm();
                RefreshCustomerList();
            };
        }

        public void CloseEditCustomerForm()
        {
            if (mainForm != null)
                mainForm.pcbBlurOverlay.Visible = false;

            if (editCustomerForm != null)
            {
                editCustomerForm.Dispose();
                editCustomerForm = null;
            }

            if (scrollContainer != null)
            {
                scrollContainer.Controls.Clear();
                scrollContainer.Parent?.Controls.Remove(scrollContainer);
                scrollContainer.Dispose();
                scrollContainer = null;
            }
        }

        private void RefreshCustomerList()
        {
            // Find and refresh the customer list in the main form
            var customerMainPage = FindControlRecursive<CustomerMainPage>(mainForm);
            customerMainPage?.RefreshCustomerList();
        }

        private T FindControlRecursive<T>(Control parent) where T : Control
        {
            if (parent == null) return null;

            foreach (Control control in parent.Controls)
            {
                if (control is T found)
                    return found;

                var child = FindControlRecursive<T>(control);
                if (child != null)
                    return child;
            }
            return null;
        }
    }
}