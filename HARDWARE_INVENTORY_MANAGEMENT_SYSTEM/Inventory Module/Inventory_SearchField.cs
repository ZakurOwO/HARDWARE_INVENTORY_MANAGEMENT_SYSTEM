using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class Inventory_SearchField : UserControl
    {
        public Inventory_SearchField()
        {
            InitializeComponent();
            // Make sure the click event is wired up to the textbox
            if (searchtxtbox != null)
            {
                searchtxtbox.Click += searchtxtbox_Click;
            }
        }

        private void searchtxtbox_Click(object sender, EventArgs e)
        {
          
        }
    }
}