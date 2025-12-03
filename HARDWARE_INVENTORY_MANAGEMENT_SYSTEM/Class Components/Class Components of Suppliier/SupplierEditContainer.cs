using System;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    public class SupplierEditContainer
    {
        private Panel scrollContainer;
        private EditFormSupplier editForm;
        private MainDashBoard mainForm;
        private SupplierTable supplierTable;
        private EventHandler supplierUpdatedHandler;
        private EventHandler cancelHandler;

        public bool IsFormOpen => scrollContainer != null && scrollContainer.Visible;

        public void ShowSupplierEditForm(MainDashBoard main, SupplierTable table, SupplierRecord supplier)
        {
            if (IsFormOpen)
                return;

            mainForm = main;
            supplierTable = table;

            editForm = new EditFormSupplier();
            editForm.Dock = DockStyle.None;
            editForm.LoadSupplier(supplier);

            supplierUpdatedHandler = (s, e) => supplierTable?.LoadSuppliersFromDatabase();
            cancelHandler = (s, e) => CloseSupplierEditForm();

            editForm.SupplierUpdated += supplierUpdatedHandler;
            editForm.CancelRequested += cancelHandler;

            scrollContainer = new Panel();
            scrollContainer.Size = new Size(600, 520);
            scrollContainer.Location = new Point(
                (main.Width - scrollContainer.Width) / 2,
                (main.Height - scrollContainer.Height) / 2
            );
            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            scrollContainer.AutoScroll = false;

            scrollContainer.Controls.Add(editForm);

            editForm.Size = new Size(600, 850);
            editForm.Location = new Point(0, 0);
            editForm.Show();

            mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.SupplierOverlay;
            mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
            mainForm.pcbBlurOverlay.Visible = true;
            mainForm.pcbBlurOverlay.BringToFront();

            mainForm.Controls.Add(scrollContainer);
            scrollContainer.BringToFront();
        }

        public void CloseSupplierEditForm()
        {
            if (mainForm != null)
                mainForm.pcbBlurOverlay.Visible = false;

            if (editForm != null)
            {
                editForm.SupplierUpdated -= supplierUpdatedHandler;
                editForm.CancelRequested -= cancelHandler;
                editForm.Dispose();
                editForm = null;
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
