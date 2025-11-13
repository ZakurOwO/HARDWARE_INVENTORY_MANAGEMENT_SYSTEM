using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log
{
    public partial class AuditDataGrid : UserControl
    {
        private SqlConnection con;
        private SqlDataAdapter daAudit;
        private DataTable dtAudit;

        public AuditDataGrid()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=.;Initial Catalog=InventoryCapstone;Integrated Security=True");
            LoadAuditLogs();
        }

        private void LoadAuditLogs()
        {
            dtAudit = new DataTable();
            daAudit = new SqlDataAdapter("SELECT * FROM AuditLog", con);
            SqlCommandBuilder cb = new SqlCommandBuilder(daAudit);
            daAudit.Fill(dtAudit);

            // If you have a DataGridView named auditDataGridView
            // auditDataGridView.DataSource = dtAudit;
        }

        public void AddAudit(int userId, string moduleName)
        {
            try
            {
                DataRow newRow = dtAudit.NewRow();
                newRow["user_id"] = userId;
                newRow["module"] = moduleName;
                newRow["timestamp"] = DateTime.Now;

                dtAudit.Rows.Add(newRow);
                daAudit.Update(dtAudit);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Audit Log Error: " + ex.Message);
            }
        }
    }
}
