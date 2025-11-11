using System;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report;


namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class ReportsMainPage : UserControl
    {
        public ReportsMainPage()
        {
            InitializeComponent();
        }
        
        private void PnlNavBar_ShowInventory(object sender, EventArgs e)
        {
            ShowInventoryControl();
        }
        private void PnlNavBar_ShowSales(object sender, EventArgs e)
        {
            ShowSales();
        }
        private void PnlNavBar_ShowCustomers(object sender, EventArgs e)
        {
            ShowCustomers();
        }
        private void PnlNavBar_ShowSuppliers(object sender, EventArgs e)
        {
            ShowSuppliers();
        }
        private void PnlNavBar_ShowDeliveries(object sender, EventArgs e)
        {
            ShowDeliveries();
        }

        private void ShowInventoryControl()
        {
            pnlMainPanel.Controls.Clear();
            var inventoryReportUC = new InventoryReportsPanel();
            inventoryReportUC.Dock = DockStyle.Fill;
            pnlMainPanel.Controls.Add(inventoryReportUC);
        }


        private void ShowSales()
        {
            pnlMainPanel.Controls.Clear();
            var salesReportUC = new SalesPage();
            salesReportUC.Dock = DockStyle.Fill;
            pnlMainPanel.Controls.Add(salesReportUC);
        }

        private void ShowCustomers()
        {
            pnlMainPanel.Controls.Clear();
            var customersReportUC = new CustomersReportPanel();
            customersReportUC.Dock = DockStyle.Fill;
            pnlMainPanel.Controls.Add(customersReportUC);
        }

        private void ShowSuppliers()
        {
            pnlMainPanel.Controls.Clear();
            var suppliersReportUC = new SupplierReportsPanel();
            suppliersReportUC.Dock = DockStyle.Fill;
            pnlMainPanel.Controls.Add(suppliersReportUC);
        }

        private void ShowDeliveries()
        {
            pnlMainPanel.Controls.Clear();
            var deliveriesReportUC = new DeliveriesReportPanel();
            deliveriesReportUC.Dock = DockStyle.Fill;
            pnlMainPanel.Controls.Add(deliveriesReportUC);
        }

        private void ReportsMainPage_Load(object sender, EventArgs e)
        {
            reportsNavigationBar1.ShowInventory += PnlNavBar_ShowInventory;
            reportsNavigationBar1.ShowSales += PnlNavBar_ShowSales;
            reportsNavigationBar1.ShowCustomers += PnlNavBar_ShowCustomers;
            reportsNavigationBar1.ShowSuppliers += PnlNavBar_ShowSuppliers;
            reportsNavigationBar1.ShowDeliveries += PnlNavBar_ShowDeliveries;

            ShowInventoryControl();
        }
    }
}
