using System;
using System.Data;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Supplier_Reports
{
    public partial class SupplierPage2 : UserControl, IReportExportable
    {
        private SupplierReportDataAccess dataAccess;
        private DataTable poSummaryTable;

        public SupplierPage2()
        {
            InitializeComponent();
            dataAccess = new SupplierReportDataAccess();
            this.Load += SupplierPage2_Load;
        }

        private void SupplierPage2_Load(object sender, EventArgs e)
        {
            poSummaryTable = dataAccess.GetPurchaseOrderSummary();
            dgvCurrentStockReport.DataSource = poSummaryTable;
        }

        public ReportTable BuildReportForExport()
        {
            if (poSummaryTable == null || poSummaryTable.Rows.Count == 0)
                return null;

            return ReportTableFactory.FromDataTable(
                poSummaryTable,
                "Purchase Orders Summary",
                "Summary of purchase orders per supplier"
            );
        }
    }
}
