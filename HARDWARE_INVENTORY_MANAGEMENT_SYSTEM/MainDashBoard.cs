using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public partial class MainDashBoard : Form
    {
        public MainDashBoard()
        {
            InitializeComponent();
        }

        private void sidePanel1_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sidePanel1_Load_1(object sender, EventArgs e)
        {

        }

        private void MainDashBoard_Load(object sender, EventArgs e)
        {

        }
        

        public Panel MainContentPanelAccess
        {
            get { return MainContentPanel; }
        }

        private void MainContentPanel_Paint(object sender, PaintEventArgs e)
        {

        }


        public void ShowSettingsPanel(Panel parent)
        {
            // Toggle behavior
            var existing = parent.Controls.OfType<Settings_Signout>().FirstOrDefault();
            if (existing != null) 
            {
                parent.Controls.Remove(existing);

                foreach (Control c in parent.Controls)
                    c.BringToFront();

                return;
            }

            Settings_Signout settings = new Settings_Signout();
         
            settings.Location = new Point(parent.Width - settings.Width - 25, 60);

            parent.Controls.Add(settings);

            foreach (Control c in parent.Controls)
                c.SendToBack();

            settings.BringToFront();
        }




    }
}
