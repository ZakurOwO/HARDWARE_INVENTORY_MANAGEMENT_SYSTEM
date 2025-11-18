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

        // UPDATE THESE PROPERTIES WITH YOUR ACTUAL CONTROL NAMES:
        public string UserName
        {
            set
            {
                // Change "lblUserName" to your actual label name for username
                if (lblUserName != null) lblUserName.Text = value;
            }
        }

        public string Position
        {
            set
            {
                // Change "lblPosition" to your actual label name for position
                if (lblPosition != null) lblPosition.Text = value;
            }
        }

     

        public Image UserIcon
        {
            set
            {
                // Change "pictureBoxUser" to your actual picturebox name
                if (guna2CirclePictureBox1 != null) guna2CirclePictureBox1.Image = value;
            }
        }

        // Wire this method to your close button's Click event in the designer
        private void btnClose_Click(object sender, EventArgs e)
        {
            ClosePopup?.Invoke(this, EventArgs.Empty);
            this.Visible = false;
        }
    }
}