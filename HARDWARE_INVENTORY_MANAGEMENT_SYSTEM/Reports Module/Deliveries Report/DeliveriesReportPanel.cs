using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Deliveries_Report;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class DeliveriesReportPanel : UserControl
    {
        private DeliveriesDataAccess deliveriesData;

        private DataTable deliveriesDataTable;
        private int currentPage = 1;
        private int itemsPerPage = 10;
        private int totalPages = 1;

        private Guna2ComboBox exportScopeComboBox;
        private Guna2Button exportCSVBtn;

        public DeliveriesReportPanel()
        {
            InitializeComponent();
            deliveriesData = new DeliveriesDataAccess();

            this.Load += DeliveriesReportPanel_Load;

            HidePaginationButtons();
            CreateExportControls();
        }

        // -----------------------------------------------------
        //  EXPORT CONTROLS (UNIFIED WITH ALL OTHER MODULES)
        // -----------------------------------------------------
        private void CreateExportControls()
        {
            // === Export Scope ComboBox ===
            exportScopeComboBox = new Guna2ComboBox();
            exportScopeComboBox.Width = 200;
            exportScopeComboBox.Height = 35;
            exportScopeComboBox.Location = new Point(595, 10);
            exportScopeComboBox.BorderRadius = 8;
            exportScopeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            exportScopeComboBox.Font = new Font("Lexend SemiBold", 9F, FontStyle.Bold);

            exportScopeComboBox.Items.Add("Current Page Only");
            exportScopeComboBox.Items.Add("Export Entire Module");
            exportScopeComboBox.SelectedIndex = 0;

            Controls.Add(exportScopeComboBox);

            // === Export CSV Button ===
            exportCSVBtn = new Guna2Button();
            exportCSVBtn.Text = "Export CSV";
            exportCSVBtn.Width = 135;
            exportCSVBtn.Height = 35;
            exportCSVBtn.Location = new Point(800, 10);
            exportCSVBtn.BorderRadius = 8;
            exportCSVBtn.FillColor = Color.FromArgb(0, 110, 196);
            exportCSVBtn.ForeColor = Color.White;
            exportCSVBtn.Font = new Font("Lexend SemiBold", 9, FontStyle.Bold);

            exportCSVBtn.Click += ExportCSVBtn_Click;

            Controls.Add(exportCSVBtn);

            // Make sure they appear above everything
            exportScopeComboBox.BringToFront();
            exportCSVBtn.BringToFront();
        }

        // -----------------------------------------------------
        //  LOAD REPORT
        // -----------------------------------------------------
        private void DeliveriesReportPanel_Load(object sender, EventArgs e)
        {
            LoadDeliveryData();
        }

        private void LoadDeliveryData()
        {
            panel1.Controls.Clear();

            deliveriesDataTable = deliveriesData.GetDeliverySummary();

            if (deliveriesDataTable == null)
            {
                MessageBox.Show("Error loading delivery report data.");
                return;
            }

            totalPages = (int)Math.Ceiling(deliveriesDataTable.Rows.Count / (double)itemsPerPage);
            if (totalPages < 1) totalPages = 1;

            DeliveriesPage1 page = new DeliveriesPage1();
            page.LoadDataDirectly(GetCurrentPageData());
            page.Dock = DockStyle.Fill;

            panel1.Controls.Add(page);

            UpdatePaginationLabel();
            UpdateNavigationButtons();
        }

        // -----------------------------------------------------
        //  PAGINATION
        // -----------------------------------------------------
        private DataTable GetCurrentPageData()
        {
            DataTable dt = deliveriesDataTable.Clone();
            int start = (currentPage - 1) * itemsPerPage;
            int end = Math.Min(start + itemsPerPage, deliveriesDataTable.Rows.Count);

            for (int i = start; i < end; i++)
                dt.ImportRow(deliveriesDataTable.Rows[i]);

            return dt;
        }

        private void UpdatePaginationLabel()
        {
            int totalRecords = deliveriesDataTable.Rows.Count;
            int startRec = (currentPage - 1) * itemsPerPage + 1;
            int endRec = Math.Min(currentPage * itemsPerPage, totalRecords);

            if (totalRecords == 0) startRec = 0;

            label2.Text = $"Showing {startRec}-{endRec} of {totalRecords} records (Page {currentPage}/{totalPages})";
        }

        private void UpdateNavigationButtons()
        {
            guna2Button6.Enabled = currentPage > 1;          // <
            guna2Button4.Enabled = currentPage < totalPages;  // >
        }

        private void HidePaginationButtons()
        {
            if (guna2Button5 != null) guna2Button5.Visible = false;
            if (guna2Button2 != null) guna2Button2.Visible = false;

            guna2Button6.Visible = true;  // <
            guna2Button4.Visible = true;  // >
            label2.Visible = true;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadDeliveryData();
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadDeliveryData();
            }
        }

        public void RefreshReport()
        {
            LoadDeliveryData();
        }
        private void ExportCSVBtn_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count == 0)
                return;

            IReportExportable exportable = panel1.Controls[0] as IReportExportable;

            if (exportable == null)
            {
                MessageBox.Show("This report page cannot be exported.");
                return;
            }

            bool exportModule = exportScopeComboBox.SelectedIndex == 1;

            bool exported = false;

            if (exportModule)
            {
                List<ReportTable> reports = BuildModuleReportsForExport();

                if (reports.Count == 0)
                {
                    MessageBox.Show("No data to export.");
                    return;
                }

                exported = ReportCsvExporter2.ExportModule("Supplier", reports);
            }
            else
            {
                ReportTable table = exportable.BuildReportForExport();

                if (table == null || table.Rows.Count == 0)
                {
                    MessageBox.Show("No data to export.");
                    return;
                }

                exported = ReportCsvExporter2.ExportReportTable(table);
            }

            // ❌ Stop showing success when CANCEL is clicked!
            if (!exported)
                return;

            // ✔ Only show when SaveFileDialog result == OK
            MessageBox.Show("Report exported successfully!",
                "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private List<ReportTable> BuildModuleReportsForExport()
        {
            List<ReportTable> reports = new List<ReportTable>();

            // Iterate through all pages of the module
            for (int page = 1; page <= totalPages; page++)
            {
                // Get data for the current page
                DataTable pageData = GetPageData(page);

                // Convert the page data into a ReportTable
                ReportTable reportTable = ConvertDataTableToReportTable(pageData, $"Page {page}");

                if (reportTable != null)
                {
                    reports.Add(reportTable);
                }
            }

            return reports;
        }

        private DataTable GetPageData(int page)
        {
            DataTable dt = deliveriesDataTable.Clone();
            int start = (page - 1) * itemsPerPage;
            int end = Math.Min(start + itemsPerPage, deliveriesDataTable.Rows.Count);

            for (int i = start; i < end; i++)
                dt.ImportRow(deliveriesDataTable.Rows[i]);

            return dt;
        }

        private ReportTable ConvertDataTableToReportTable(DataTable dataTable, string title)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
                return null;

            ReportTable reportTable = new ReportTable
            {
                Title = title,
                Headers = dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList(),
                Rows = dataTable.Rows.Cast<DataRow>().Select(r => r.ItemArray.Select(i => i.ToString()).ToList()).ToList()
            };

            return reportTable;
        }

    }
}
