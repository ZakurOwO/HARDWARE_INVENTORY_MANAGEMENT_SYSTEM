using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class ReportsKeyMetrics : UserControl
    {
        public ReportsKeyMetrics()
        {
            InitializeComponent();
        }

        #region Properties

        private string title;
        private int _value;
        private Image icon;

        [Category("Custom Properties")]
        public string Title
        {
            get { return title; }
            set { title = value; lblTitle.Text = value; }
        }

        [Category("Custom Properties")]
        public int Value
        {
            get { return _value; }
            set { _value = value; lblValue.Text = value.ToString(); }
        }

        [Category("Custom Properties")]
        public Image Icon
        {
            get { return icon; }
            set { icon = value; ptbIcon.BackgroundImage = value;  }
        }


        #endregion

    }

}
