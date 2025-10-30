using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class DeliveriesSlideButtons : UserControl
    {
        public DeliveriesSlideButtons()
        {
            InitializeComponent();

        }
       

        private void BTNVehicles_Click(object sender, EventArgs e)
        {
            SetActiveButton(BTNVehicles);
            SetInactiveButton(BTNDeliveries);

            
            
        }

        private void BTNDeliveries_Click(object sender, EventArgs e)
        {
            SetActiveButton(BTNDeliveries);
            SetInactiveButton(BTNVehicles);

           

        }

        private void SetActiveButton(KryptonButton button)
        {
            button.StateCommon.Back.Color1 = Color.FromArgb(227, 242, 253);
            button.StateCommon.Back.Color2 = Color.FromArgb(227, 242, 253);
            button.StateCommon.Border.Color1 = Color.DodgerBlue;
            button.StateCommon.Border.Color2 = Color.DodgerBlue;
            button.StateCommon.Content.ShortText.Color1 = Color.DodgerBlue;
          
        }

        private void SetInactiveButton(KryptonButton button)
        {
            button.StateCommon.Back.Color1 = Color.White;
            button.StateCommon.Back.Color2 = Color.White;
            button.StateCommon.Border.Color1 = Color.Silver;
            button.StateCommon.Border.Color2 = Color.Silver;
            button.StateCommon.Content.ShortText.Color1 = Color.Black;
           
        }

        private void PanelTabs_Paint(object sender, PaintEventArgs e)
        {
        }

        private void DeliveriesSlideButtons_Load(object sender, EventArgs e)
        {
            SetActiveButton(BTNVehicles);
        }
        
    }
}
