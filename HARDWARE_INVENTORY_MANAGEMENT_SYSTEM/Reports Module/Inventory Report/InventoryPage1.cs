using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report
{
    public partial class InventoryPage1 : UserControl
    {
        private InventoryReportsDataAccess dataAccess;

        public InventoryPage1()
        {
            InitializeComponent();
            dataAccess = new InventoryReportsDataAccess();
            this.Load += InventoryPage1_Load;
            this.ExportPDFBtn.Click += ExportPDFBtn_Click;
        }

        private void InventoryPage1_Load(object sender, EventArgs e)
        {
            LoadData();
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
            ReportTable report = ReportQueries.BuildInventoryCurrentStockReport();
            DataGridView dgv = FindDataGridView(reportsTable1);

            if (dgv != null)
            {
                dgv.Rows.Clear();
                for (int i = 0; i < report.Rows.Count; i++)
                {
                    List<string> row = report.Rows[i];
                    dgv.Rows.Add(row.ToArray());
                }
            }
        }

        private void ExportPDFBtn_Click(object sender, EventArgs e)
        {
            ReportTable report = ReportQueries.BuildInventoryCurrentStockReport();
            bool exported = ReportPdfExporter.ExportReportTable(report);
            if (exported)
            {
                MessageBox.Show("Report exported to PDF successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
