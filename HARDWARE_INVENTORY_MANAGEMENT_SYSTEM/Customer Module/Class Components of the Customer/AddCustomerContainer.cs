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
            mainForm = main;

            // Create the AddCustomerForm (converted into UserControl style)
            addCustomerForm = new AddCustomerForm();
            addCustomerForm.TopLevel = false;       // IMPORTANT: treat form as embedded
            addCustomerForm.FormBorderStyle = FormBorderStyle.None;
            addCustomerForm.Dock = DockStyle.None;

            // SCROLL CONTAINER (same as inventory)
            scrollContainer = new Panel();
            scrollContainer.Size = new Size(583, 505);        // SAME SIZE AS INVENTORY
            scrollContainer.Location = new Point(472, 100);   // SAME POSITION AS INVENTORY
            scrollContainer.AutoScroll = true;
            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            scrollContainer.HorizontalScroll.Enabled = false;
            scrollContainer.HorizontalScroll.Visible = false;
            scrollContainer.AutoScrollMinSize = new Size(0, 813);

            // add form into scroll container
            scrollContainer.Controls.Add(addCustomerForm);
            addCustomerForm.Size = new Size(583, 813);
            addCustomerForm.Location = new Point(0, 0);
            addCustomerForm.Show();

            // overlay
            mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.CustomerOvelay;
            mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
            mainForm.pcbBlurOverlay.Visible = true;
            mainForm.pcbBlurOverlay.BringToFront();

            // bring container
            mainForm.Controls.Add(scrollContainer);
            scrollContainer.BringToFront();

            // close event
            addCustomerForm.FormClosed += (s, e) => CloseAddCustomerForm();
        }

        public void CloseAddCustomerForm()
        {
            if (mainForm != null)
                mainForm.pcbBlurOverlay.Visible = false;

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
    }
}