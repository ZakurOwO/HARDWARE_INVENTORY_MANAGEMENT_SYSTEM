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
    public partial class SearchField : UserControl
    {
        public SearchField()
        {
            InitializeComponent();
        }

        private void tbxSearchField_TextChanged(object sender, EventArgs e)
        {

        }

        #region Properties

        private string promptMsg;

        [Category("Custom Properties")]
        public string PromptMessage
        {
            get { return promptMsg; }
            set { promptMsg = value; tbxSearchField.Text = value; }
        }

        #endregion
    }
}
