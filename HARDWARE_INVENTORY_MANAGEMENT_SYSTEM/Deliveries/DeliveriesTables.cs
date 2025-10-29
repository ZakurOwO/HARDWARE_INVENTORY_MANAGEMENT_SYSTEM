using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class DeliveriesTables : UserControl
    {
        public DeliveriesTables()
        {
            InitializeComponent();
            StyleDataGridView();
        }

        private void dgvDeliveries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void StyleDataGridView()
        {
            dgvDeliveries.EnableHeadersVisualStyles = false;

            // Header styling
            dgvDeliveries.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvDeliveries.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dgvDeliveries.ColumnHeadersDefaultCellStyle.Font = new Font("Lexend", 8, FontStyle.Bold);
            dgvDeliveries.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Background and grid style
            dgvDeliveries.BackgroundColor = Color.White;
            dgvDeliveries.BorderStyle = BorderStyle.None;
            dgvDeliveries.CellBorderStyle = DataGridViewCellBorderStyle.None; 
            dgvDeliveries.GridColor = Color.White; 

            // Row styling
            dgvDeliveries.DefaultCellStyle.BackColor = Color.White;
            dgvDeliveries.DefaultCellStyle.ForeColor = Color.Black;
            dgvDeliveries.DefaultCellStyle.Font = new Font("Lexend", 8);
            dgvDeliveries.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dgvDeliveries.DefaultCellStyle.SelectionForeColor = Color.White;

            // Row header
            dgvDeliveries.RowHeadersVisible = false;
        }
    }
}
