using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public class AddItemContainer
    {
        private Panel scrollContainer;
        private AddNewItem_Form addForm;
        private MainDashBoard mainForm;

        public void ShowAddItemForm(MainDashBoard main)
        {
            mainForm = main;

            // Create the AddNewItem_Form
            addForm = new AddNewItem_Form();
            addForm.OnProductAdded += AddForm_OnProductAdded;

            // Create scroll container
            scrollContainer = new Panel();
            scrollContainer.Size = new Size(583, 505);
            scrollContainer.Location = new Point(472, 100);
            scrollContainer.AutoScroll = true;
            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            scrollContainer.HorizontalScroll.Enabled = false;
            scrollContainer.HorizontalScroll.Visible = false;
            scrollContainer.AutoScrollMinSize = new Size(0, 813);

            // Add form to container
            scrollContainer.Controls.Add(addForm);
            addForm.Size = new Size(583, 813);
            addForm.Location = new Point(0, 0);

            // Set up main form
            mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.InventoryOverlay;
            mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
            mainForm.pcbBlurOverlay.Visible = true;
            mainForm.pcbBlurOverlay.BringToFront();

            // Add container to main form
            mainForm.Controls.Add(scrollContainer);
            scrollContainer.BringToFront();
        }

        private void AddForm_OnProductAdded(object sender, EventArgs e)
        {
            CloseAddItemForm();
        }

        public void CloseAddItemForm()
        {
            if (mainForm != null)
            {
                mainForm.pcbBlurOverlay.Visible = false;
            }

            if (addForm != null)
            {
                addForm.OnProductAdded -= AddForm_OnProductAdded;
                addForm.Dispose();
                addForm = null;
            }

            if (scrollContainer != null)
            {
                scrollContainer.Controls.Clear();
                scrollContainer.Parent?.Controls.Remove(scrollContainer);
                scrollContainer.Dispose();
                scrollContainer = null;
            }
        }

        public bool IsVisible()
        {
            return scrollContainer != null && scrollContainer.Visible;
        }
    }
}