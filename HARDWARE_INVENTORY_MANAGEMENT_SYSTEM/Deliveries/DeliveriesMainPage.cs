using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class DeliveriesMainPage: UserControl
    {

        public DeliveriesMainPage()
        {
            InitializeComponent();
        }

        

        private void DeliveriesSlideButtons1_ShowDeliveries(object sender, EventArgs e)
        {
            ShowDeliveries();
        }

        private void DeliveriesSlideButtons1_ShowVehicles(object sender, EventArgs e)
        {
            ShowVehicles();
        }

        private void ShowDeliveries()
        {
            pnlPanelContainer.Controls.Clear();
            var deliveriesTable = new DeliveriesTable();
            deliveriesTable.Dock = DockStyle.Fill;
            pnlPanelContainer.Controls.Add(deliveriesTable);
        }

        private void ShowVehicles()
        {
            pnlPanelContainer.Controls.Clear();
            var vehiclesTable = new DeliveriesMainPage2();
            vehiclesTable.Dock = DockStyle.Fill;
            pnlPanelContainer.Controls.Add(vehiclesTable);
        }

        private void DeliveriesMainPage_Load(object sender, EventArgs e)
        {
            deliveriesSlideButtons1.ShowDeliveries += DeliveriesSlideButtons1_ShowDeliveries;
            deliveriesSlideButtons1.ShowVehicles += DeliveriesSlideButtons1_ShowVehicles;

            ShowDeliveries();
        }
    }
}
