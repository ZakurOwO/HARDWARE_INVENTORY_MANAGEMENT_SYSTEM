using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log
{
    public partial class AuditDataGrid : UserControl
    {
        public AuditDataGrid()
        {
            InitializeComponent();
        }

        // Public property to access the DataGridView without changing designer
        public DataGridView GridView
        {
            get { return dgvAudit; }
        }
    }
}