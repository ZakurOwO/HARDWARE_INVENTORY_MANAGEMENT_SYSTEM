using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;

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

            // Initialize manager first
            auditLogManager = new AuditLogManager();
            currentAuditLogs = new List<AuditLogEntry>();
        }

        private void AuditLogMainPage_Load(object sender, EventArgs e)
        {
            Console.WriteLine("AuditLogMainPage_Load called");
            InitializeDataGridView();
            SetupEventHandlers();
            LoadAuditLogs();
        }

        private void InitializeDataGridView()
        {
            try
            {
                Console.WriteLine("Initializing DataGridView...");

                // Access the DataGridView through auditDataGrid2
                var dgv = auditDataGrid2.GridView; // This gives us dgvAudit

                dgv.AutoGenerateColumns = false;
                dgv.ReadOnly = true;
                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;

                // Map existing columns to properties
                // Based on Designer.cs, columns are: User, Activity, Module, Timestamp
                if (dgv.Columns.Count >= 4)
                {
                    dgv.Columns[0].DataPropertyName = "Username";     // User column
                    dgv.Columns[1].DataPropertyName = "Activity";     // Activity column
                    dgv.Columns[2].DataPropertyName = "Module";       // Module column
                    dgv.Columns[3].DataPropertyName = "Timestamp";    // Timestamp column
                }

                Console.WriteLine($"DataGridView has {dgv.Columns.Count} columns configured");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in InitializeDataGridView: {ex.Message}");
                MessageBox.Show($"Error initializing grid: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupEventHandlers()
        {
            try
            {
                Console.WriteLine("Setting up event handlers...");

                // Search textbox
                if (this.tbxSearchField != null)
                {
                    this.tbxSearchField.TextChanged += TbxSearchField_TextChanged;
                }

                // Download CSV button
                if (this.btnMainButtonIcon != null)
                {
                    this.btnMainButtonIcon.Click += BtnDownloadCSV_Click;
                }

                // Grid double-click
                var dgv = auditDataGrid2.GridView;
                if (dgv != null)
                {
                    dgv.CellDoubleClick += DgvAudit_CellDoubleClick;
                }

                Console.WriteLine("Event handlers set up successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting up event handlers: {ex.Message}");
            }
        }

        private void LoadAuditLogs()
        {
            try
            {
                Console.WriteLine("=== LoadAuditLogs START ===");

                if (auditLogManager == null)
                {
                    Console.WriteLine("Creating new AuditLogManager");
                    auditLogManager = new AuditLogManager();
                }

                Console.WriteLine("Calling GetAuditLogs...");
                currentAuditLogs = auditLogManager.GetAuditLogs();

                Console.WriteLine($"Retrieved {currentAuditLogs?.Count ?? 0} audit logs from database");

                var dgv = auditDataGrid2.GridView;

                if (currentAuditLogs == null || currentAuditLogs.Count == 0)
                {
                    Console.WriteLine("WARNING: No audit logs found in database");
                    dgv.DataSource = new List<AuditLogEntry>();
                    MessageBox.Show("No audit logs found in the database. Try logging in/out or performing some actions to generate logs.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Console.WriteLine($"Binding {currentAuditLogs.Count} logs to DataGridView");

                    // Clear and bind
                    dgv.DataSource = null;
                    dgv.DataSource = currentAuditLogs;

                    // Debug: Show first entry
                    var first = currentAuditLogs.FirstOrDefault();
                    if (first != null)
                    {
                        Console.WriteLine($"First entry: User={first.Username}, Activity={first.Activity}, Module={first.Module}, Time={first.Timestamp}");
                    }

                    Console.WriteLine($"DataGridView now has {dgv.Rows.Count} rows");
                }

                dgv.Refresh();
                Console.WriteLine("=== LoadAuditLogs END ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! ERROR in LoadAuditLogs: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error loading audit logs: {ex.Message}\n\nCheck the Output window (View → Output) for details.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TbxSearchField_TextChanged(object sender, EventArgs e)
        {
            try
            {
                currentSearchTerm = tbxSearchField.Text;
                Console.WriteLine($"Search term: '{currentSearchTerm}'");
                ApplyFilters();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in search: {ex.Message}");
            }
        }

        private void BtnDownloadCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentAuditLogs?.Any() == true)
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                        saveFileDialog.FilterIndex = 1;
                        saveFileDialog.RestoreDirectory = true;
                        saveFileDialog.FileName = $"AuditLog_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            bool success = auditLogManager.ExportToCSV(saveFileDialog.FileName);

                            if (success)
                            {
                                MessageBox.Show($"Audit log exported successfully to:\n{saveFileDialog.FileName}",
                                    "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to export audit log.", "Export Failed",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No audit logs to export.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error exporting CSV: {ex.Message}");
                MessageBox.Show($"Error exporting audit logs: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvAudit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && currentAuditLogs != null && e.RowIndex < currentAuditLogs.Count)
                {
                    var selectedAuditLog = currentAuditLogs[e.RowIndex];
                    ShowAuditLogDetails(selectedAuditLog);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error showing details: {ex.Message}");
            }
        }

        private void ShowAuditLogDetails(AuditLogEntry auditLog)
        {
            try
            {
                StringBuilder details = new StringBuilder();
                details.AppendLine($"Audit ID: {auditLog.AuditID}");
                details.AppendLine($"User: {auditLog.Username} (ID: {auditLog.UserId})");
                details.AppendLine($"Module: {auditLog.Module}");
                details.AppendLine($"Activity: {auditLog.Activity}");
                details.AppendLine($"Activity Type: {auditLog.ActivityType}");
                details.AppendLine($"Table Affected: {auditLog.TableAffected ?? "N/A"}");
                details.AppendLine($"Record ID: {auditLog.RecordID ?? "N/A"}");
                details.AppendLine($"Timestamp: {auditLog.Timestamp:yyyy-MM-dd HH:mm:ss}");

                if (!string.IsNullOrEmpty(auditLog.OldValues))
                    details.AppendLine($"\nOld Values:\n{auditLog.OldValues}");

                if (!string.IsNullOrEmpty(auditLog.NewValues))
                    details.AppendLine($"\nNew Values:\n{auditLog.NewValues}");

                if (!string.IsNullOrEmpty(auditLog.IPAddress))
                    details.AppendLine($"\nIP Address: {auditLog.IPAddress}");

                MessageBox.Show(details.ToString(), "Audit Log Details",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying details: {ex.Message}");
            }
        }

        private void ApplyFilters()
        {
            try
            {
                if (currentAuditLogs == null)
                {
                    Console.WriteLine("No audit logs to filter");
                    return;
                }

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

                var resultList = filteredLogs.ToList();
                Console.WriteLine($"Filtered to {resultList.Count} entries");

                var dgv = auditDataGrid2.GridView;
                dgv.DataSource = null;
                dgv.DataSource = resultList;
                dgv.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error applying filters: {ex.Message}");
            }
        }

        public void RefreshData()
        {
            Console.WriteLine("RefreshData called");
            LoadAuditLogs();
        }
    }
}