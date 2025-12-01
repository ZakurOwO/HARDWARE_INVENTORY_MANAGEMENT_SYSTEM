using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.Class_Components_of_Accounts
{
    internal class AddUserContainer
    {
        private Panel scrollContainer;
        private AddNewUser_Form addForm;
        private EditUserInfo_Form editForm;
        private MainDashBoard mainForm;
        private EventHandler cancelHandler;
        private EventHandler<string> updatedHandler;
        private EventHandler<(string AccountID, string FullName, string Role, string Status)> addHandler;

        public void ShowAddUserForm(
            MainDashBoard main,
            EventHandler<(string AccountID, string FullName, string Role, string Status)> userAdded = null)
        {
            mainForm = main;

            addForm = new AddNewUser_Form();

            addForm.CancelClicked += AddForm_CancelClicked;
            if (userAdded != null)
            {
                addHandler = userAdded;
                addForm.UserAdded += addHandler;
            }
            addForm.UserAdded += AddForm_UserAdded;

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

        private void AddForm_UserAdded(object sender, (string AccountID, string FullName, string Role, string Status) e)
        {
            CloseSupplierAddForm();
        }

        private void AddForm_CancelClicked(object sender, EventArgs e)
        {
            CloseSupplierAddForm();
        }

        public void ShowEditUserForm(MainDashBoard main, DataRow userData, EventHandler<string> userUpdated = null)
        {
            mainForm = main;
            editForm = new EditUserInfo_Form();

            cancelHandler = (s, e) => CloseEditUserForm();
            updatedHandler = (s, accountId) =>
            {
                userUpdated?.Invoke(this, accountId);
                CloseEditUserForm();
            };

            editForm.CancelClicked += cancelHandler;
            editForm.UserUpdated += updatedHandler;

            editForm.LoadUser(userData);

            scrollContainer = new Panel();
            scrollContainer.Size = new Size(566, 565);
            scrollContainer.Location = new Point(
                (main.Width - scrollContainer.Width) / 2,
                (main.Height - scrollContainer.Height) / 2
            );
            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            scrollContainer.AutoScroll = false; // disable scrollbars
            scrollContainer.Controls.Add(editForm);

            editForm.Size = new Size(566, 565); // keep original size
            editForm.Location = new Point(0, 0);
            editForm.Show();

            mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.AccountsOverlay;
            mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
            mainForm.pcbBlurOverlay.Visible = true;
            mainForm.pcbBlurOverlay.BringToFront();

            mainForm.Controls.Add(scrollContainer);
            scrollContainer.BringToFront();
        }

        public void CloseSupplierAddForm()
        {
            if (addForm != null)
            {
                addForm.CancelClicked -= AddForm_CancelClicked;
                addForm.UserAdded -= AddForm_UserAdded;
                if (addHandler != null)
                {
                    addForm.UserAdded -= addHandler;
                    addHandler = null;
                }
            }

            if (mainForm != null)
                mainForm.pcbBlurOverlay.Visible = false;

            scrollContainer?.Controls.Clear();
            scrollContainer?.Parent?.Controls.Remove(scrollContainer);
            scrollContainer?.Dispose();
            addForm?.Dispose();
        }

        private void CloseEditUserForm()
        {
            if (mainForm != null)
            {
                mainForm.pcbBlurOverlay.Visible = false;
            }

            scrollContainer?.Controls.Clear();
            scrollContainer?.Parent?.Controls.Remove(scrollContainer);
            scrollContainer?.Dispose();

            if (editForm != null)
            {
                if (cancelHandler != null)
                {
                    editForm.CancelClicked -= cancelHandler;
                }

                if (updatedHandler != null)
                {
                    editForm.UserUpdated -= updatedHandler;
                }
            }

            editForm?.Dispose();
        }
    }
}
