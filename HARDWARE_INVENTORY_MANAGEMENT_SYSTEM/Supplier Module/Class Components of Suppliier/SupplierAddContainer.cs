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
        private SupplierTable supplierTable;
        private EventHandler supplierAddedHandler;
        private EventHandler cancelHandler;

        public bool IsFormOpen => scrollContainer != null && scrollContainer.Visible;
        public void ShowSupplierAddForm(MainDashBoard main, SupplierTable table = null)
        {
            if (IsFormOpen)
                return;

            mainForm = main;
            supplierTable = table;

            SupplierAddForm = new SupplierAddForm();
            SupplierAddForm.Dock = DockStyle.None;

            supplierAddedHandler = (s, e) => supplierTable?.LoadSuppliersFromDatabase();
            cancelHandler = (s, e) => CloseSupplierAddForm();

            SupplierAddForm.SupplierAdded += supplierAddedHandler;
            SupplierAddForm.CancelRequested += cancelHandler;

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

            if (SupplierAddForm != null)
            {
                SupplierAddForm.SupplierAdded -= supplierAddedHandler;
                SupplierAddForm.CancelRequested -= cancelHandler;
                SupplierAddForm.Dispose();
                SupplierAddForm = null;
            }

            if (scrollContainer != null)
            {
                scrollContainer.Controls.Clear();
                if (mainForm.Controls.Contains(scrollContainer))
                    mainForm.Controls.Remove(scrollContainer);
                scrollContainer.Dispose();
                scrollContainer = null;
            }
        }
    }
}