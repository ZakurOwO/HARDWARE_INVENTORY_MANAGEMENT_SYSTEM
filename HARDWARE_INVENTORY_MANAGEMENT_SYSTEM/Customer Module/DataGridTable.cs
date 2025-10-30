using System;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public partial class DataGridTable : UserControl
    {
        public DataGridTable()
        {
            InitializeComponent();
        }

        private void DataGridTable_Load(object sender, EventArgs e)
        {

        }







        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StyleDataGridView()
        {
            dgvCustomers.EnableHeadersVisualStyles = false;

            // Header styling
            dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Lexend", 8, FontStyle.Bold);
            dgvCustomers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Background and grid style
            dgvCustomers.BackgroundColor = Color.White;
            dgvCustomers.BorderStyle = BorderStyle.None;
            dgvCustomers.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvCustomers.GridColor = Color.White;

            // Row styling
            dgvCustomers.DefaultCellStyle.BackColor = Color.White;
            dgvCustomers.DefaultCellStyle.ForeColor = Color.Black;
            dgvCustomers.DefaultCellStyle.Font = new Font("Lexend", 8);
            dgvCustomers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dgvCustomers.DefaultCellStyle.SelectionForeColor = Color.White;

            // Row header
            dgvCustomers.RowHeadersVisible = false;
        }
    }
}
