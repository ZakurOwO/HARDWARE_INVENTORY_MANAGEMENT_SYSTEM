using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log.ClassComponent;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log
{
    public partial class AuditLogMainPage : UserControl
    {
        private AuditLogManager auditLogManager;
        private List<AuditLogEntry> currentAuditLogs;
        private string currentSearchTerm;

        public AuditLogMainPage()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadAuditLogs();
            SetupEventHandlers();
        }

        private void InitializeDataGridView()
        {
            // Configure the data grid view settings
            auditDataGrid2.GridView.AutoGenerateColumns = false;
            auditDataGrid2.GridView.ReadOnly = true;
            auditDataGrid2.GridView.AllowUserToAddRows = false;
            auditDataGrid2.GridView.AllowUserToDeleteRows = false;

            // Clear existing columns
            auditDataGrid2.GridView.Columns.Clear();

            // Add columns to match your database schema
            auditDataGrid2.GridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Username",
                HeaderText = "User",
                DataPropertyName = "Username",
                Width = 120
            });

            auditDataGrid2.GridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Activity",
                HeaderText = "Activity",
                DataPropertyName = "Activity",
                Width = 200
            });

            auditDataGrid2.GridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Module",
                HeaderText = "Module",
                DataPropertyName = "Module",
                Width = 120
            });

            auditDataGrid2.GridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ActivityType",
                HeaderText = "Activity Type",
                DataPropertyName = "ActivityType",
                Width = 100
            });

            auditDataGrid2.GridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Timestamp",
                HeaderText = "Timestamp",
                DataPropertyName = "Timestamp",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Format = "yyyy-MM-dd HH:mm:ss"
                }
            });
        }

        private void SetupEventHandlers()
        {
            this.tbxSearchField.TextChanged += TbxSearchField_TextChanged;
            this.btnMainButtonIcon.Click += BtnDownloadCSV_Click;
            this.auditDataGrid2.GridView.CellDoubleClick += DgvAudit_CellDoubleClick;
        }

        private void LoadAuditLogs()
        {
            try
            {
                if (auditLogManager == null)
                    auditLogManager = new AuditLogManager();

                currentAuditLogs = auditLogManager.GetAuditLogs();
                auditDataGrid2.GridView.DataSource = currentAuditLogs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading audit logs: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TbxSearchField_TextChanged(object sender, EventArgs e)
        {
            currentSearchTerm = tbxSearchField.Text;
            ApplyFilters();
        }

        private void BtnDownloadCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentAuditLogs?.Any() == true)
                {
                    string filePath = AuditHelper.ExportToCsv(currentAuditLogs, "AuditLogs");
                    MessageBox.Show($"Audit logs exported to: {filePath}", "Export Successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No audit logs to export.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting audit logs: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvAudit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && currentAuditLogs != null && e.RowIndex < currentAuditLogs.Count)
            {
                var selectedAuditLog = currentAuditLogs[e.RowIndex];
                ShowAuditLogDetails(selectedAuditLog);
            }
        }

        private void ShowAuditLogDetails(AuditLogEntry auditLog)
        {
            StringBuilder details = new StringBuilder();
            details.AppendLine($"Audit ID: {auditLog.AuditID}");
            details.AppendLine($"User: {auditLog.Username} (ID: {auditLog.UserId})");
            details.AppendLine($"Module: {auditLog.Module}");
            details.AppendLine($"Activity: {auditLog.Activity}");
            details.AppendLine($"Activity Type: {auditLog.ActivityType}");
            details.AppendLine($"Table Affected: {auditLog.TableAffected ?? "N/A"}");
            details.AppendLine($"Record ID: {auditLog.RecordId ?? "N/A"}");
            details.AppendLine($"Timestamp: {auditLog.Timestamp:yyyy-MM-dd HH:mm:ss}");
            details.AppendLine($"IP Address: {auditLog.IpAddress ?? "N/A"}");

            if (!string.IsNullOrEmpty(auditLog.OldValues))
                details.AppendLine($"Old Values: {auditLog.OldValues}");

            if (!string.IsNullOrEmpty(auditLog.NewValues))
                details.AppendLine($"New Values: {auditLog.NewValues}");

            MessageBox.Show(details.ToString(), "Audit Log Details",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ApplyFilters()
        {
            if (currentAuditLogs == null) return;

            var filteredLogs = currentAuditLogs.AsEnumerable();

            // Apply search term filter
            if (!string.IsNullOrEmpty(currentSearchTerm))
            {
                string searchLower = currentSearchTerm.ToLower();
                filteredLogs = filteredLogs.Where(log =>
                    (log.Username != null && log.Username.ToLower().Contains(searchLower)) ||
                    (log.Activity != null && log.Activity.ToLower().Contains(searchLower)) ||
                    (log.Module != null && log.Module.ToLower().Contains(searchLower)) ||
                    (log.ActivityType != null && log.ActivityType.ToLower().Contains(searchLower)));
            }

            auditDataGrid2.GridView.DataSource = filteredLogs.ToList();
        }

        public void RefreshData()
        {
            LoadAuditLogs();
        }

        private void AuditLogMainPage_Load(object sender, EventArgs e)
        {
            auditLogManager = new AuditLogManager();
            currentAuditLogs = new List<AuditLogEntry>();
        }
    }
}