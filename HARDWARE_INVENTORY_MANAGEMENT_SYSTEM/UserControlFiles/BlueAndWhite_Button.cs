using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class Proceed_ClearButton : UserControl
    {
        public Proceed_ClearButton()
        {
            InitializeComponent();
        }

        #region Properties

        private string whitebtnName;
        private string blubtnName;

        [Category("Custom Properties")]
        public string White_Button_Name
        {
            get { return whitebtnName; }
            set { whitebtnName = value; btnWhite.Text = value; }
        }

        [Category("Custom Properties")]
        public string Blue_Button_Name
        {
            get { return blubtnName; }
            set { blubtnName = value; btnBlue.Text = value; }
        }

        #endregion

    }
}
