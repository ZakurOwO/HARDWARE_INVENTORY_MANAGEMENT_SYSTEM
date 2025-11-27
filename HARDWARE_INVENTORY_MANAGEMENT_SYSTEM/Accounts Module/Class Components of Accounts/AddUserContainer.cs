using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.Class_Components_of_Accounts
{
    internal class AddUserContainer
    {
        private Panel scrollContainer;
        private AddNewUser_Form addForm;
        private MainDashBoard mainForm;

        public void ShowAddUserForm(MainDashBoard main)
        {
            mainForm = main;

            addForm = new AddNewUser_Form();


            scrollContainer = new Panel();
            scrollContainer.Size = new Size(574, 570);
            scrollContainer.Location = new Point(
                (main.Width - scrollContainer.Width) / 2,
                (main.Height - scrollContainer.Height) / 2
            );
            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            scrollContainer.AutoScroll = false; // disable scrollbars

            scrollContainer.Controls.Add(addForm);

            addForm.Size = new Size(574, 570); // keep original size
            addForm.Location = new Point(0, 0);
            addForm.Show();

            mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.AccountsOverlay;
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
            addForm?.Dispose();
        }
    }
}
