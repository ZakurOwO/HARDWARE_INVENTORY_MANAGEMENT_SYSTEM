using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.Class_Compnents_Of_Inventory
{
    public class EditItemContainer
    {
        private Panel scrollContainer;
        private MainDashBoard mainForm;
        private EditItem_Form editForm;

        // MOVEABLE: expose the container panel so the caller can adjust position safely
        public Panel ActiveContainerPanel => scrollContainer;

        public void ShowEditItemForm(MainDashBoard main, string productId)
        {
            mainForm = main;

            // Create the EditItem_Form
            editForm = new EditItem_Form(productId, main.pcbBlurOverlay);

            // Create scroll container
            scrollContainer = new Panel();
            scrollContainer.Size = new Size(600, 505);

            // CENTER PLACEMENT (MOVEABLE): this is the default placement
            scrollContainer.Location = new Point(
                (main.Width - scrollContainer.Width) / 2,
                (main.Height - scrollContainer.Height) / 2
            );

            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            scrollContainer.AutoScroll = true;

            // Add form to container
            editForm.Size = new Size(scrollContainer.ClientSize.Width, 813);
            editForm.Location = new Point(0, 0);
            editForm.Dock = DockStyle.Top;
            editForm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            scrollContainer.Controls.Add(editForm);

            // Set up main form overlay
            mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.InventoryOverlay;
            mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
            mainForm.pcbBlurOverlay.Visible = true;
            mainForm.pcbBlurOverlay.BringToFront();

            // Add container to main form
            mainForm.Controls.Add(scrollContainer);
            scrollContainer.BringToFront();

            editForm.ParentScrollContainer = scrollContainer;
        }

        public void CloseEditItemForm()
        {
            if (mainForm != null)
                mainForm.pcbBlurOverlay.Visible = false;

            if (editForm != null)
            {
                editForm.Dispose();
                editForm = null;
            }

            if (scrollContainer != null)
            {
                scrollContainer.Controls.Clear();
                if (mainForm != null && mainForm.Controls.Contains(scrollContainer))
                    mainForm.Controls.Remove(scrollContainer);

                scrollContainer.Dispose();
                scrollContainer = null;
            }
        }
    }
}
