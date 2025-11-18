using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    public partial class UserAccountsPanel : UserControl
    {
        public event EventHandler UserPanelClicked;

        public UserAccountsPanel()
        {
            InitializeComponent();
            WireUpClickEvents();
        }

        #region Properties

        private string name;
        private string position;
        private string role;
        private Image icon;
        private string status;

        [Category("Custom Properties")]
        public string _Name
        {
            get { return name; }
            set { name = value; lblUserName.Text = value; }
        }

        [Category("Custom Properties")]
        public string Position
        {
            get { return position; }
            set { position = value; lblPosition.Text = value; }
        }

        [Category("Custom Properties")]
        public string Role
        {
            get { return role; }
            set { role = value; btnRole.Text = value; UpdateRoleColor(); }
        }

        [Category("Custom Properties")]
        public Image Icon
        {
            get { return icon; }
            set { icon = value; btnRole.Image = value; }
        }

        [Category("Custom Properties")]
        public string Status
        {
            get { return status; }
            set { status = value; btnStatus.Text = value; UpdateStatusColor(); }
        }

        #endregion

        private void WireUpClickEvents()
        {
            // Wire up click events for all controls in the panel
            pnlUserAccount.Click += (s, e) => OnUserPanelClicked();
            lblUserName.Click += (s, e) => OnUserPanelClicked();
            lblPosition.Click += (s, e) => OnUserPanelClicked();
            btnRole.Click += (s, e) => OnUserPanelClicked();
            btnStatus.Click += (s, e) => OnUserPanelClicked();

            // If you have any other controls in your panel, add them here too
        }

        protected virtual void OnUserPanelClicked()
        {
            UserPanelClicked?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateRoleColor()
        {
            // Default
            btnRole.FillColor = Color.LightGray;
            btnRole.ForeColor = Color.Black;

            switch (role?.ToLower())
            {
                case "admin":
                    btnRole.FillColor = Color.FromArgb(235, 220, 255); // light purple
                    btnRole.ForeColor = Color.FromArgb(132, 56, 232);  // purple text
                    break;
                case "user":
                case "sales representative":
                    btnRole.FillColor = Color.FromArgb(219, 239, 255); // light blue
                    btnRole.ForeColor = Color.FromArgb(28, 116, 232);
                    break;
                case "manager":
                    btnRole.FillColor = Color.FromArgb(255, 239, 200); // light yellow
                    btnRole.ForeColor = Color.FromArgb(205, 142, 0);
                    break;
            }
        }

        private void UpdateStatusColor()
        {
            // Default
            btnStatus.FillColor = Color.LightGray;
            btnStatus.ForeColor = Color.Black;

            switch (status?.ToLower())
            {
                case "active":
                    btnStatus.FillColor = Color.FromArgb(219, 255, 232); // light green
                    btnStatus.ForeColor = Color.FromArgb(47, 164, 73);
                    break;
                case "inactive":
                    btnStatus.FillColor = Color.FromArgb(255, 230, 230); // light red
                    btnStatus.ForeColor = Color.FromArgb(190, 38, 38);
                    break;
                case "suspended":
                    btnStatus.FillColor = Color.FromArgb(255, 248, 208); // light orange
                    btnStatus.ForeColor = Color.FromArgb(209, 118, 17);
                    break;
            }
        }

        private void pnlUserAccount_Click(object sender, EventArgs e)
        {
            OnUserPanelClicked();
        }

        private void lblUserName_Click(object sender, EventArgs e)
        {
            OnUserPanelClicked();
        }
    }
}