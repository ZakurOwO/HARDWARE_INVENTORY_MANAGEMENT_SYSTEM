using System;
using System.Data;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Supplier_Reports
{
    public partial class SupplierPage3 : UserControl, IReportExportable
    {
        private SupplierReportDataAccess dataAccess;
        private DataTable poDetailsTable;

        public SupplierPage3()
        {
            InitializeComponent();
            dataAccess = new SupplierReportDataAccess();
            this.Load += SupplierPage3_Load;
        }

        private void SupplierPage3_Load(object sender, EventArgs e)
        {
            poDetailsTable = dataAccess.GetPurchaseOrderDetails();
            dgvCurrentStockReport.DataSource = poDetailsTable;
        }

        public ReportTable BuildReportForExport()
        {
            if (poDetailsTable == null || poDetailsTable.Rows.Count == 0)
                return null;

            return ReportTableFactory.FromDataTable(
                poDetailsTable,
                "Purchase Order Details",
                "Detailed items per purchase order"
            );
        }
    }
}
