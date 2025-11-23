using System;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public class SupplierAddContainer
    {
        private Panel scrollContainer;
        private SupplierAddForm SupplierAddForm;
        private MainDashBoard mainForm;

        public void ShowSupplierAddForm(MainDashBoard main)
        {
            mainForm = main;

            SupplierAddForm = new SupplierAddForm();
            SupplierAddForm.Dock = DockStyle.None;

            scrollContainer = new Panel();
            scrollContainer.Size = new Size(600, 520);
            scrollContainer.Location = new Point(
                (main.Width - scrollContainer.Width) / 2,
                (main.Height - scrollContainer.Height) / 2
            );
            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            scrollContainer.AutoScroll = true;
            scrollContainer.AutoScrollMinSize = new Size(0, 850);

            scrollContainer.Controls.Add(SupplierAddForm);

            SupplierAddForm.Size = new Size(600, 850);
            SupplierAddForm.Location = new Point(0, 0);
            SupplierAddForm.Show();

            mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.CustomerOvelay;
            mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
            mainForm.pcbBlurOverlay.Visible = true;
            mainForm.pcbBlurOverlay.BringToFront();

            mainForm.Controls.Add(scrollContainer);
            scrollContainer.BringToFront();

            SupplierAddForm.CancelClicked += (s, e) => CloseSupplierAddForm();
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
