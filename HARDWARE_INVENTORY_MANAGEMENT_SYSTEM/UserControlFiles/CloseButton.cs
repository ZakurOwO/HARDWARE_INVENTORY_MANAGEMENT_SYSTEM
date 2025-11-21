using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles
{
    public partial class CloseButton : UserControl
    {
        public event EventHandler CloseClicked;
        public CloseButton()
        {
            InitializeComponent();

            this.guna2Button3.Click += (s, e) => OnClick(e);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
