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
    public partial class Success_PopUp : UserControl
    {
        public Success_PopUp()
        {
            InitializeComponent();
        }

        #region Properties

        [Category("Custom Properties")]
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; lblMessage.Text = value; }
        }

        #endregion
    }
}
