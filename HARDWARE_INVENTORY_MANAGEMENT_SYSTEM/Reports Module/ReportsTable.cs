 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class ReportsTable : UserControl
    {
        public ReportsTable()
        {
            InitializeComponent();

            dgvCurrentStockReport.Rows.Add("CMT-023", "Cement Portland 40kg", "Cement & Blocks", 150, 50, "bags", 260.00);
            dgvCurrentStockReport.Rows.Add("SNM-096", "Deformed Bar G33", "Steel & Metals", 35, 30, "pieces", 260.00);
            dgvCurrentStockReport.Rows.Add("LNW-087", "Plywood 1/2 x 4’x8’", "Lumber & Wood", 35, 30, "sheets", 260.00);
            dgvCurrentStockReport.Rows.Add("BRK-054", "Red Clay Brick", "Cement & Blocks", 5000, 2000, "pieces", 12.00);
            dgvCurrentStockReport.Rows.Add("TRS-032", "Gravel (per cu.m)", "Aggregates", 120, 50, "cubic meters", 1500.00);
            dgvCurrentStockReport.Rows.Add("SD-019", "Sand (per cu.m)", "Aggregates", 100, 40, "cubic meters", 1200.00);
            dgvCurrentStockReport.Rows.Add("GLS-045", "Window Glass 4mm", "Glass & Windows", 80, 30, "sheets", 300.00);
            dgvCurrentStockReport.Rows.Add("PNL-078", "Gypsum Board 1/2 x 4’x8’", "Drywall & Insulation", 60, 20, "sheets", 250.00);
            dgvCurrentStockReport.Rows.Add("FLR-066", "Ceramic Floor Tile 12x12", "Tiles & Flooring", 200, 100, "pieces", 80.00);
            dgvCurrentStockReport.Rows.Add("PLM-089", "PVC Plumbing Pipe 3/4”", "Plumbing Supplies", 90, 40, "pieces", 150.00);
            dgvCurrentStockReport.Rows.Add("ELC-101", "Electrical Wire 14 AWG", "Electrical Supplies", 300, 150, "meters", 20.00);
        }
    }
}
