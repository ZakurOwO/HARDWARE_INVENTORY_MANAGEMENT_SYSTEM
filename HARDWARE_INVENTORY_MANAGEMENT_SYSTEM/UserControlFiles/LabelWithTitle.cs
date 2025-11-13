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
    public partial class LabelWithTitle : UserControl
    {
        public LabelWithTitle()
        {
            InitializeComponent();
        }

        #region Properties

        private string title;
        private string description;

        [Category("Custom Properties")]
        public string Title
        {
            get { return title; }
            set { title = value; lblTitle.Text = value; }
        }

        [Category("Custom Properties")]
        public string Description
        {
            get { return description; }
            set { description = value; lblDescription.Text = value; }
        }

        #endregion

    }
}
