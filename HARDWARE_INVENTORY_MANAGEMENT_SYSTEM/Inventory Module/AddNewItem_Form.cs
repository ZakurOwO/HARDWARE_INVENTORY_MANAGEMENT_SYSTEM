using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class AddNewItem_Form : UserControl
    {
        public AddNewItem_Form()
        {
            InitializeComponent();
        }

        private void closeButton1_Click(object sender, EventArgs e)
        {
            var main = this.FindForm() as MainDashBoard;
            if (main != null)
            {
                main.pcbBlurOverlay.Visible = false;
            }

            if (this.Parent != null)
            {
                this.Parent.Visible = false;
            }
            else
            {
                Control ancestor = this;
                while (ancestor != null && !(ancestor is InventoryMainPage))
                {
                    ancestor = ancestor.Parent;
                }
                if (ancestor is InventoryMainPage inv)
                {
                    inv.panelAddItem.Visible = false;
                }
            }
        }

        private void tbxImageUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Images|*.png;*.jpg;*.jpeg;*.gif;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    tbxImageUpload.Text = Path.GetFileName(ofd.FileName);
                }
            }
        }
    }
}
