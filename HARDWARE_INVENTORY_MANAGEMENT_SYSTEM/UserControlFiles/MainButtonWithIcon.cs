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
    public partial class MainButtonWithIcon : UserControl
    {
        public MainButtonWithIcon()
        {
            InitializeComponent();
        }

        #region Properties

        private string btnName;
        private Image icon;

        [Category("Custom Properties")]

        public string ButtonName
        {
            get { return btnName; }
            set { btnName = value; btnMainButtonIcon.Text = value; }
        }


        [Category("Custom Properties")]
        public Image Icon
        {
            get { return icon; }
            set { icon = value; btnMainButtonIcon.Image = value; }
        }

        #endregion
    }
}
