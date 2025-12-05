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
            Debug.WriteLine("InventoryPage1 constructor initialized");
            EnsureExportHandler();
        }

        private void InventoryPage1_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("InventoryPage1_Load start");
            LoadData();
            Debug.WriteLine("InventoryPage1_Load end");
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

        private async void ExportPDFBtn_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("ExportPDFBtn_Click start");
            Button exportButton = this.ExportPDFBtn;
            Cursor previousCursor = Cursor.Current;
            bool previousUseWaitCursor = Application.UseWaitCursor;

            try
            {
                if (exportButton != null)
                {
                    exportButton.Enabled = false;
                }

                Application.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;

                if (currentReport == null)
                {
                    currentReport = ReportQueries.BuildInventoryCurrentStockReport();
                }

                if (currentReport == null || currentReport.Headers == null || currentReport.Headers.Count == 0 || currentReport.Rows == null || currentReport.Rows.Count == 0)
                {
                    MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string filePath;
                if (!ReportPdfExporter.TryGetSavePath(currentReport.Title, out filePath))
                {
                    return;
                }

                ReportTable reportToExport = currentReport;
                bool exported = false;
                Exception exportException = null;

                await Task.Run(delegate
                {
                    try
                    {
                        exported = ReportPdfExporter.ExportReportTableToPath(reportToExport, filePath, false);
                    }
                    catch (Exception ex)
                    {
                        exportException = ex;
                    }
                });

                if (exportException != null)
                {
                    MessageBox.Show(
                        "Failed to export report: " + exportException.GetType().FullName + ": " + exportException.Message,
                        "Export Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                if (exported)
                {
                    MessageBox.Show("Report exported to PDF successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                Application.UseWaitCursor = previousUseWaitCursor;
                Cursor.Current = previousCursor;

                if (exportButton != null)
                {
                    exportButton.Enabled = true;
                }

                Debug.WriteLine("ExportPDFBtn_Click end");
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

        private void EnsureExportHandler()
        {
            if (this.ExportPDFBtn != null)
            {
                this.ExportPDFBtn.Click -= ExportPDFBtn_Click;
                this.ExportPDFBtn.Click += ExportPDFBtn_Click;
            }
        }
    }
}
