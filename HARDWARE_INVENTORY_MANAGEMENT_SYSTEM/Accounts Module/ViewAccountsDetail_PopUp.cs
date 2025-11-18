using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    public partial class ViewAccountsDetail_PopUp : UserControl
    {
        public event EventHandler ClosePopup;

        public ViewAccountsDetail_PopUp()
        {
            InitializeComponent();
        }

        // Basic properties
        public string UserName
        {
            set
            {
                if (lblUserName != null) lblUserName.Text = value;
            }
        }

        public string Position
        {
            set
            {
                if (lblPosition != null) lblPosition.Text = value;
            }
        }

        public Image UserIcon
        {
            set
            {
                if (guna2CirclePictureBox1 != null) guna2CirclePictureBox1.Image = value;
            }
        }

        // Add these missing properties for database fields
        public string AccountID
        {
            set
            {
                if (lblAccountID != null) lblAccountID.Text = value ?? "N/A";
            }
        }

        public string DateCreated
        {
            set
            {
                if (lblDateCreated != null) lblDateCreated.Text = value ?? "N/A";
            }
        }

        public string Email
        {
            set
            {
                if (lblEmail != null) lblEmail.Text = value ?? "Not provided";
            }
        }

        public string PhoneNumber
        {
            set
            {
                if (lblPhoneNo != null) lblPhoneNo.Text = value ?? "Not provided";
            }
        }

        public string Address
        {
            set
            {
                if (lblAddress != null) lblAddress.Text = value ?? "Not provided";
            }
        }

        public string Role
        {
            set
            {
                if (AdministratorPictureboxRole != null)
                {
                    AdministratorPictureboxRole.Text = value ?? "N/A";
                    UpdateRoleBadge(value);
                }
            }
        }

        public string Status
        {
            set
            {
                if (PictureboxStatus != null)
                {
                    PictureboxStatus.Text = value ?? "N/A";
                    UpdateStatusBadge(value);
                }
            }
        }

        // NEW METHOD: Populate from DataRow (direct database access)
        public void PopulateFromDataRow(DataRow userRow)
        {
            if (userRow == null) return;

            // Set all properties directly from database fields
            UserName = userRow.Field<string>("Fullname") ?? "N/A";
            Position = userRow.Field<string>("Role") ?? "N/A";
            AccountID = userRow.Field<string>("AccountID") ?? "N/A";

            // Handle date conversion
            var createdDate = userRow.Field<DateTime?>("created_at");
            DateCreated = createdDate?.ToString("MMMM d, yyyy") ?? "N/A";

            Address = userRow.Field<string>("Address") ?? "Not provided";
            Role = userRow.Field<string>("Role") ?? "N/A";
            Status = userRow.Field<string>("Account_status") ?? "N/A";

            // Set icon based on status
            UserIcon = LoadIconByStatus(userRow.Field<string>("Account_status"));

            // Note: Email and PhoneNumber fields don't exist in your database schema
            // If you add them later, uncomment these lines:
            // Email = userRow.Field<string>("Email") ?? "Not provided";
            // PhoneNumber = userRow.Field<string>("Phone") ?? "Not provided";
        }

        // NEW METHOD: Populate from individual values
        public void PopulateFromValues(string accountId, string fullName, string role, string status, string address, DateTime? dateCreated)
        {
            UserName = fullName ?? "N/A";
            Position = role ?? "N/A";
            AccountID = accountId ?? "N/A";
            DateCreated = dateCreated?.ToString("MMMM d, yyyy") ?? "N/A";
            Address = address ?? "Not provided";
            Role = role ?? "N/A";
            Status = status ?? "N/A";
            UserIcon = LoadIconByStatus(status);
        }

        // Helper method to load icon based on status
        private Image LoadIconByStatus(string status)
        {
            try
            {
                string resourceName = status?.ToLower() == "active"
                    ? "Employees_1"
                    : "Employees_2";

                var image = Properties.Resources.ResourceManager.GetObject(resourceName) as Image;

                if (image == null)
                {
                    resourceName = status?.ToLower() == "active"
                        ? "Employees1"
                        : "Employees2";
                    image = Properties.Resources.ResourceManager.GetObject(resourceName) as Image;
                }

                return image;
            }
            catch
            {
                return null;
            }
        }

        private void UpdateRoleBadge(string role)
        {
            if (AdministratorPictureboxRole == null) return;

            string r = role?.ToLower() ?? "";

            switch (r)
            {
                case "admin":
                case "administrator":
                    AdministratorPictureboxRole.FillColor = Color.FromArgb(235, 220, 255);
                    AdministratorPictureboxRole.ForeColor = Color.FromArgb(132, 56, 232);
                    break;
                default:
                    AdministratorPictureboxRole.FillColor = Color.FromArgb(219, 239, 255);
                    AdministratorPictureboxRole.ForeColor = Color.FromArgb(28, 116, 232);
                    break;
            }
        }

        private void UpdateStatusBadge(string status)
        {
            if (PictureboxStatus == null) return;

            string s = status?.ToLower() ?? "";

            switch (s)
            {
                case "active":
                    PictureboxStatus.FillColor = Color.FromArgb(219, 255, 232);
                    PictureboxStatus.ForeColor = Color.FromArgb(47, 164, 73);
                    break;
                case "inactive":
                    PictureboxStatus.FillColor = Color.FromArgb(255, 230, 230);
                    PictureboxStatus.ForeColor = Color.FromArgb(190, 38, 38);
                    break;
                default:
                    PictureboxStatus.FillColor = Color.LightGray;
                    PictureboxStatus.ForeColor = Color.Black;
                    break;
            }
        }

        // Wire this method to your close button's Click event in the designer
        private void btnClose_Click(object sender, EventArgs e)
        {
            ClosePopup?.Invoke(this, EventArgs.Empty);
            this.Visible = false;
        }

        private void lblUserName_Click(object sender, EventArgs e) { }
        private void lblPosition_Click(object sender, EventArgs e) { }
        private void AdministratorPictureboxRole_Click(object sender, EventArgs e) { }
        private void PictureboxStatus_Click(object sender, EventArgs e) { }
        private void lblAccountID_Click(object sender, EventArgs e) { }
        private void lblDateCreated_Click(object sender, EventArgs e) { }
        private void lblEmail_Click(object sender, EventArgs e) { }
        private void lblPhoneNo_Click(object sender, EventArgs e) { }
        private void lblAddress_Click(object sender, EventArgs e) { }
    }
}