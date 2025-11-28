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
    public partial class DeliveriesMainPage : UserControl
    {
        private Pagination_Deliveries pagination;

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
            var deliveriesTable = new DeliveriesTables();
            deliveriesTable.Dock = DockStyle.Fill;
            pnlPanelContainer.Controls.Add(deliveriesTable);

            // HIDE pagination for deliveries
            if (pagination != null)
                pagination.Visible = false;
        }

        private void ShowVehicles()
        {
            pnlPanelContainer.Controls.Clear();
            var vehiclesTable = new DeliveriesMainPage2();
            vehiclesTable.Dock = DockStyle.Fill;
            pnlPanelContainer.Controls.Add(vehiclesTable);

            // SHOW pagination only for vehicles
            if (pagination != null)
            {
                pagination.Visible = true;
                // Reposition pagination to be below the panel container
                pagination.Location = new Point(
                    pnlPanelContainer.Left,
                    pnlPanelContainer.Bottom + 10
                );
                pagination.BringToFront();
            }
        }

        

        private void DeliveriesMainPage_Load(object sender, EventArgs e)
        {
            deliveriesSlideButtons1.ShowDeliveries += DeliveriesSlideButtons1_ShowDeliveries;
            deliveriesSlideButtons1.ShowVehicles += DeliveriesSlideButtons1_ShowVehicles;

          
            ShowDeliveries(); // Start with deliveries view
        }

        // Handle resize to keep pagination positioned correctly
        private void DeliveriesMainPage_Resize(object sender, EventArgs e)
        {
            if (pagination != null)
            {
                pagination.Location = new Point(
                    pnlPanelContainer.Left,
                    pnlPanelContainer.Bottom + 10
                );
                pagination.Width = pnlPanelContainer.Width;
            }
        }

        private void pagination_Deliveries1_Load(object sender, EventArgs e)
        {

        }
    }
}