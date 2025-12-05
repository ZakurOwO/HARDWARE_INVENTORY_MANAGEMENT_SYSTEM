using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Supplier_Reports
{
    public partial class SupplierPage1 : UserControl, IReportExportable
    {
        private DataTable supplierTable;

        public SupplierPage1()
        {
            InitializeComponent();
            this.Load += SupplierPage1_Load;
        }

        private void SupplierPage1_Load(object sender, EventArgs e)
        {
            LoadSupplierDirectory();
        }

        // ----------------------------------------------------------
        // LOAD SUPPLIER DIRECTORY (Matches your designer columns)
        // ----------------------------------------------------------
        private void LoadSupplierDirectory()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString.DataSource))
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            SupplierID,
                            supplier_name,
                            contact_person,
                            contact_number,
                            address,
                            'Active' AS Status
                        FROM Suppliers
                        ORDER BY supplier_id ASC;
                    ";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    supplierTable = new DataTable();
                    da.Fill(supplierTable);

                    dgvCurrentStockReport.Rows.Clear();

                    foreach (DataRow row in supplierTable.Rows)
                    {
                        dgvCurrentStockReport.Rows.Add(
                            row["SupplierID"].ToString(),
                            row["supplier_name"].ToString(),
                            row["contact_person"].ToString(),
                            row["contact_number"].ToString(),
                            row["address"].ToString(),
                            row["Status"].ToString()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load supplier directory:\n" + ex.Message);
            }
        }

        // ----------------------------------------------------------
        // EXPORT SUPPORT
        // ----------------------------------------------------------
        public ReportTable BuildReportForExport()
        {
            ReportTable table = new ReportTable
            {
                Title = "Supplier Directory",
                Subtitle = "List of all suppliers"
            };

            // Add headers (your ReportTable uses `Headers`, NOT `Columns`)
            table.Headers.AddRange(new[]
            {
        "Supplier ID",
        "Supplier Name",
        "Contact Person",
        "Contact Info",
        "Address",
        "Status"
    });

            // Add all rows from the DataGridView
            foreach (DataGridViewRow row in dgvCurrentStockReport.Rows)
            {
                if (row.IsNewRow) continue;

                table.Rows.Add(new List<string>
        {
            row.Cells[0].Value?.ToString() ?? "",
            row.Cells[1].Value?.ToString() ?? "",
            row.Cells[2].Value?.ToString() ?? "",
            row.Cells[3].Value?.ToString() ?? "",
            row.Cells[4].Value?.ToString() ?? "",
            row.Cells[5].Value?.ToString() ?? ""
        });
            }

            return table;
        }

    }
}
