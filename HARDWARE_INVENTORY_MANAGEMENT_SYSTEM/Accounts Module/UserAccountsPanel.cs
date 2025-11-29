using Guna.UI2.WinForms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.Class_Components_of_Accounts;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    public partial class UserAccountsPanel : UserControl
    {
        public event EventHandler UserPanelClicked;
        private AddUserContainer editUserContainer = new AddUserContainer();

        public UserAccountsPanel()
        {
            InitializeComponent();
            WireUpClickEvents();
        }

        // ======================
        // Properties
        // ======================

        private string name;
        private string position;
        private string role;
        private Image icon;
        private string status;

        [Category("Custom Properties")]
        public string _Name
        {
            get => name;
            set { name = value; lblUserName.Text = value; }
        }

        [Category("Custom Properties")]
        public string Position
        {
            get => position;
            set { position = value; lblPosition.Text = value; }
        }

        [Category("Custom Properties")]
        public string Role
        {
            get => role;
            set { role = value; UpdateRoleBadge(); }
        }

        
       

        [Category("Custom Properties")]
        public string Status
        {
            get => status;
            set { status = value; UpdateStatusBadge(); }
        }

        // ======================
        // EVENT WIRING
        // ======================

        private void WireUpClickEvents()
        {
            foreach (Control ctl in this.Controls)
                ctl.Click += (s, e) => OnUserPanelClicked();

            this.Click += (s, e) => OnUserPanelClicked();
        }

        protected virtual void OnUserPanelClicked()
        {
            UserPanelClicked?.Invoke(this, EventArgs.Empty);
        }

        // ======================
        // ROLE BADGE LOGIC
        // ======================

        private void UpdateRoleBadge()
        {
            if (role == null) return;

            string r = role.ToLower();

            switch (r)
            {
                case "admin":
                case "administrator":

                    // Shield badge
                    btnRole.Image = Properties.Resources.shield; 
                    btnRole.FillColor = Color.FromArgb(235, 220, 255);
                    btnRole.ForeColor = Color.FromArgb(132, 56, 232);
                    btnRole.Text = "Admin";
                    break;

                default:
                    // Staff badge (no icon)
                    btnRole.Image = null;
                    btnRole.FillColor = Color.FromArgb(219, 239, 255);
                    btnRole.ForeColor = Color.FromArgb(28, 116, 232);
                    btnRole.Text = "Staff";
                    break;
            }
        }

       

        private void UpdateStatusBadge()
        {
            if (status == null) return;

            switch (status.ToLower())
            {
                case "active":
                    btnStatus.FillColor = Color.FromArgb(219, 255, 232);
                    btnStatus.ForeColor = Color.FromArgb(47, 164, 73);
                    btnStatus.Text = "Active";
                    break;

                case "inactive":
                    btnStatus.FillColor = Color.FromArgb(255, 230, 230);
                    btnStatus.ForeColor = Color.FromArgb(190, 38, 38);
                    btnStatus.Text = "Inactive";
                    break;

                default:
                    btnStatus.FillColor = Color.LightGray;
                    btnStatus.ForeColor = Color.Black;
                    break;
            }
        }

        private void btnRole_Click(object sender, EventArgs e)
        {

        }

        private void pnlUserAccount_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnStatus_Click(object sender, EventArgs e)
        {

        }

        private void btnEditIcon_Click(object sender, EventArgs e)
        {
            MainDashBoard main = this.FindForm() as MainDashBoard;

            if (main != null)
            {
                editUserContainer.ShowEditUserForm(main);
            }
        }
    }
}
