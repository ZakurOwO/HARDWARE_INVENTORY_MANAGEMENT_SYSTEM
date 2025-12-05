using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Supplier_Reports
{
    public partial class SupplierPage1 : UserControl, IReportExportable
    {
        public SupplierPage1()
        {
            InitializeComponent();
        }

        public ReportTable BuildReportForExport()
        {
            return new ReportTable
            {
                Title = "Supplier Report",
                Subtitle = "No export data configured"
            };
        }
    }
}
