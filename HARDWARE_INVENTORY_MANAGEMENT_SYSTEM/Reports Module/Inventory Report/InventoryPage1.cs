using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report
{
    public partial class InventoryPage1 : UserControl
    {
        private InventoryReportsDataAccess dataAccess;
        private ReportTable currentReport;

        public InventoryPage1()
        {
            InitializeComponent();
            dataAccess = new InventoryReportsDataAccess();
            this.Load += InventoryPage1_Load;
           
           
        }

        private void InventoryPage1_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("InventoryPage1_Load start");
            LoadData();
            Debug.WriteLine("InventoryPage1_Load end");
        }

        public ReportTable GetCurrentReport()
        {
            if (currentReport == null)
                currentReport = ReportQueries.BuildInventoryCurrentStockReport();

            return currentReport;
        }


        private void LoadData()
        {
            try
            {
                LoadMetrics();
                LoadInventoryTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMetrics()
        {
            try
            {
                int totalProducts = dataAccess.GetTotalProducts();
                reportsKeyMetrics1.Value = totalProducts;

                decimal inventoryValue = dataAccess.GetInventoryValue();
                reportsKeyMetrics2.Value = (int)inventoryValue;

                int lowStockCount = dataAccess.GetLowStockCount();
                reportsKeyMetrics3.Value = lowStockCount;

                int expiryAlertCount = dataAccess.GetExpiryAlertCount();
                reportsKeyMetrics4.Value = expiryAlertCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading metrics: " + ex.Message);
            }
        }

        private void LoadInventoryTable()
        {
            currentReport = ReportQueries.BuildInventoryCurrentStockReport();
            DataGridView dgv = FindDataGridView(reportsTable1);

            if (dgv != null)
            {
                dgv.Rows.Clear();
                for (int i = 0; i < currentReport.Rows.Count; i++)
                {
                    List<string> row = currentReport.Rows[i];
                    dgv.Rows.Add(row.ToArray());
                }
            }
        }

       
        private DataGridView FindDataGridView(Control parent)
        {
            for (int i = 0; i < parent.Controls.Count; i++)
            {
                Control control = parent.Controls[i];
                DataGridView grid = control as DataGridView;
                if (grid != null)
                {
                    return grid;
                }

                if (control.HasChildren)
                {
                    DataGridView found = FindDataGridView(control);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }

            return null;
        }

        public void RefreshData()
        {
            LoadData();
        }

      
    }
}
