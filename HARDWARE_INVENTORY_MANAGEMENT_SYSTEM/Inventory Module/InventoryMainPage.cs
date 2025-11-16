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
    public partial class InventoryMainPage : UserControl
    {
        public InventoryMainPage()
        {
            InitializeComponent();
        }

        private void inventoryList_Panel1_Load(object sender, EventArgs e)
        {

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            var main = this.FindForm() as MainDashBoard;
            if (main != null)
            {
                
                main.pcbBlurOverlay.BackgroundImage = Properties.Resources.InventoryOverlay;
                main.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch; // 
                main.pcbBlurOverlay.Visible = true;
                main.pcbBlurOverlay.BringToFront();

                panelAddItem.Visible = true;
                panelAddItem.BringToFront();
            }

            if (panelAddItem.Parent != main)
            {
                // Remove from current parent (this UserControl) and add to main
                panelAddItem.Parent?.Controls.Remove(panelAddItem);
                main.Controls.Add(panelAddItem);

                // Position the panel (centered example). Adjust as needed.
                panelAddItem.Anchor = AnchorStyles.None;
                panelAddItem.Location = new Point(
                    Math.Max(0, (main.ClientSize.Width - panelAddItem.Width) / 2),
                    Math.Max(0, (main.ClientSize.Height - panelAddItem.Height) / 2)
                );
            }

            // Show panel and bring it above the overlay
            panelAddItem.Visible = true;
            panelAddItem.BringToFront();
        }
    }
}
