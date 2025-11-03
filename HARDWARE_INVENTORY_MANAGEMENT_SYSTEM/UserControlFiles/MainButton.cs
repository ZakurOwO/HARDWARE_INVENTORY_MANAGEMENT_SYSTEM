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
    public partial class MainButton : UserControl
    {
        public MainButton()
        {
            InitializeComponent();
        }

        #region Properties

        [Category("Custom Properties")]
        private string btnName;

        public string ButtonName
        {
            get { return btnName; }
            set { btnName = value; btnMainButton.Text = value; }
        }

        #endregion
    }
}
