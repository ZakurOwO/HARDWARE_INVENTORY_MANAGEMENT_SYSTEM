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
            if (!this.DesignMode)
            {
                SetupDataGrid();
                LoadSampleRow();
            }
        }

        private void SetupDataGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // Base settings
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.GridColor = Color.WhiteSmoke;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;

            // Header styling (flat, soft gray background)
            dataGridView1.ColumnHeadersHeight = 52;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Lexend SemiBold", 10F, FontStyle.Bold);
           

            // Default cell style (white background, black text)
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.Font = new Font("Lexend SemiBold", 10F, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 235, 235); // very light gray on select
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Columns
            dataGridView1.Columns.Add("CompanyName", "Company Name");
            dataGridView1.Columns.Add("PersonContact", "Person Contact");
            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns.Add("ContactNumber", "Contact Number");

            // “⋯” button column
            var actionColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Action",
                Name = "Action",
                Text = "⋯",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };

            // Match cell style for button
            var flatButtonStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(64, 64, 64),
                SelectionBackColor = Color.FromArgb(235, 235, 235),
                SelectionForeColor = Color.Black,
                Font = new Font("Lexend SemiBold", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            actionColumn.DefaultCellStyle = flatButtonStyle;

            dataGridView1.Columns.Add(actionColumn);
        }

        private void LoadSampleRow()
        {
            dataGridView1.Rows.Add("EXAMPLE NAME", "Person Contact", "example@gmail.com", "(555) 987-6543");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Action"].Index && e.RowIndex >= 0)
            {
                string company = dataGridView1.Rows[e.RowIndex].Cells["CompanyName"].Value?.ToString();
                MessageBox.Show($"Action clicked for: {company}", "Action", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
