using System;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public class SupplierAddContainer
    {
        private Panel scrollContainer;
        private SupplierAddForm SupplierAddForm;
        private MainDashBoard mainForm;
        private SupplierTable supplierTable; // Reference to refresh table

        public void ShowSupplierAddForm(MainDashBoard main, SupplierTable table = null)
        {
            mainForm = main;
            supplierTable = table;

            SupplierAddForm = new SupplierAddForm();
            SupplierAddForm.Dock = DockStyle.None;

            // Subscribe to events
            SupplierAddForm.CancelClicked += (s, e) => CloseSupplierAddForm();
            SupplierAddForm.SupplierAdded += (s, e) => {
                // Refresh the supplier table when a new supplier is added
                supplierTable?.LoadSuppliersFromDatabase();
            };

            scrollContainer = new Panel();
            scrollContainer.Size = new Size(600, 520);
            scrollContainer.Location = new Point(
                (main.Width - scrollContainer.Width) / 2,
                (main.Height - scrollContainer.Height) / 2
            );
            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            scrollContainer.AutoScroll = false;

            scrollContainer.Controls.Add(SupplierAddForm);

            SupplierAddForm.Size = new Size(600, 850);
            SupplierAddForm.Location = new Point(0, 0);
            SupplierAddForm.Show();

            mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.SupplierOverlay;
            mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
            mainForm.pcbBlurOverlay.Visible = true;
            mainForm.pcbBlurOverlay.BringToFront();

            mainForm.Controls.Add(scrollContainer);
            scrollContainer.BringToFront();
        }

        public void CloseSupplierAddForm()
        {
            if (mainForm != null)
                mainForm.pcbBlurOverlay.Visible = false;

            scrollContainer?.Controls.Clear();
            scrollContainer?.Parent?.Controls.Remove(scrollContainer);
            scrollContainer?.Dispose();
            SupplierAddForm?.Dispose();
        }
    }
}