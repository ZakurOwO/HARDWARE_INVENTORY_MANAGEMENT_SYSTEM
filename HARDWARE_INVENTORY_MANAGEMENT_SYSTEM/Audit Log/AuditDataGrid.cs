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

            // Optional: define primary key if using Update()
            // DataColumn[] keyColumns = new DataColumn[1];
            // keyColumns[0] = dtAudit.Columns["audit_id"];
            // dtAudit.PrimaryKey = keyColumns;

            // If you have a DataGridView named auditDataGridView
            // auditDataGridView.DataSource = dtAudit;
        }

        public void AddAudit(int userId, string moduleName)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO AuditLog (user_id, module, timestamp) VALUES (@userId, @module, @timestamp)", con))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@module", moduleName);
                    cmd.Parameters.AddWithValue("@timestamp", DateTime.Now);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Audit Log Error: " + ex.Message);
            }
        }
    }
}