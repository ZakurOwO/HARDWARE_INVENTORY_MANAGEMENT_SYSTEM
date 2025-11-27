using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            scrollContainer.Size = new Size(600, 505);
            scrollContainer.Location = new Point(
                (main.Width - scrollContainer.Width) / 2,
                (main.Height - scrollContainer.Height) / 2
            );
            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            scrollContainer.AutoScroll = true;

            // Add form to container
            addForm.Size = new Size(scrollContainer.ClientSize.Width, 813);
            addForm.Location = new Point(0, 0);
            addForm.Dock = DockStyle.Top;
            addForm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            scrollContainer.Controls.Add(addForm);

            // Keep the child width in sync with the container's client area so horizontal scrollbar never appears.
            scrollContainer.ClientSizeChanged += ScrollContainer_ClientSizeChanged;
            // Also handle when vertical scrollbar visibility changes (affects client width).
            scrollContainer.Layout += ScrollContainer_Layout;

            // Set up main form
            mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.InventoryOverlay;
            mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
            mainForm.pcbBlurOverlay.Visible = true;
            mainForm.pcbBlurOverlay.BringToFront();

            // Add container to main form
            mainForm.Controls.Add(scrollContainer);
            scrollContainer.BringToFront();


        }

        private void ScrollContainer_Layout(object sender, LayoutEventArgs e)
        {
            ResizeChildToAvoidHScroll();
        }

        private void ScrollContainer_ClientSizeChanged(object sender, EventArgs e)
        {
            ResizeChildToAvoidHScroll();
        }

        private void ResizeChildToAvoidHScroll()
        {
            if (scrollContainer == null || addForm == null) return;

            // Effective available width for child controls (client width minus vertical scrollbar width if visible).
            int availableWidth = scrollContainer.ClientSize.Width;

            // If the vertical scrollbar is visible, the client width already accounts for it.
            // To be safe subtract standard scrollbar width when AutoScroll is true and vertical scroll may be present.
            // This prevents the child from being wider than the space and forcing a horizontal scrollbar.
            if (scrollContainer.VerticalScroll.Visible)
            {
                availableWidth = Math.Max(0, scrollContainer.ClientSize.Width);
            }

            // Apply the width to the addForm (dock and anchor keep positioning).
            addForm.Width = availableWidth;
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