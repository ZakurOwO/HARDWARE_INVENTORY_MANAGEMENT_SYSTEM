using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class DeliveryCartDetails : UserControl
    {
        NumericUpDown qtyUpDown = new NumericUpDown();

        public DeliveryCartDetails()
        {
            InitializeComponent();
        }

        // Initialize cart with sample data
        private void DeliveryCartDetails_Load(object sender, EventArgs e)
        {
            // Add sample items
            dgvCartDetails.Rows.Add("Roofing Shingles", 1, "₱125.00");
            dgvCartDetails.Rows.Add("Cement", 1, "₱230.00");
            dgvCartDetails.Rows.Add("Paint", 2, "₱350.00");

            dgvCartDetails.Columns["Price"].ReadOnly = true;
            dgvCartDetails.ClearSelection();

            SetupNumericUpDown();
        }

        // Configure NumericUpDown control for quantity editing
        private void SetupNumericUpDown()
        {
            qtyUpDown.Minimum = 1;
            qtyUpDown.Maximum = 1000;
            qtyUpDown.Visible = false;
            qtyUpDown.BorderStyle = BorderStyle.None;
            qtyUpDown.TextAlign = HorizontalAlignment.Center;
            qtyUpDown.Font = dgvCartDetails.DefaultCellStyle.Font;
            qtyUpDown.BackColor = dgvCartDetails.DefaultCellStyle.BackColor;
            qtyUpDown.ForeColor = dgvCartDetails.DefaultCellStyle.ForeColor;
            qtyUpDown.AutoSize = false;

            dgvCartDetails.Controls.Add(qtyUpDown);

            dgvCartDetails.CellClick += dgvCartDetails_CellClick;
            qtyUpDown.Leave += qtyUpDown_Leave;
            qtyUpDown.ValueChanged += qtyUpDown_ValueChanged;
        }

        // Handle delete button click
        private void dgvCartDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCartDetails.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to remove this item from the cart?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dgvCartDetails.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        // Show NumericUpDown when QTY cell is clicked
        private void dgvCartDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCartDetails.Columns[e.ColumnIndex].HeaderText == "QTY")
            {
                Rectangle rect = dgvCartDetails.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                qtyUpDown.Height = rect.Height - 2;
                qtyUpDown.Width = rect.Width - 4;
                qtyUpDown.Location = new Point(rect.X + 2, rect.Y + (rect.Height - qtyUpDown.Height) / 2);

                qtyUpDown.Value = Convert.ToDecimal(dgvCartDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? 1);
                qtyUpDown.Tag = e;
                qtyUpDown.Visible = true;
                qtyUpDown.BringToFront();
                qtyUpDown.Focus();
            }
            else
            {
                qtyUpDown.Visible = false;
            }
        }

        // Save value when NumericUpDown loses focus
        private void qtyUpDown_Leave(object sender, EventArgs e)
        {
            SaveNumericValue();
        }

        // Update cell value when quantity changes
        private void qtyUpDown_ValueChanged(object sender, EventArgs e)
        {
            SaveNumericValue();
        }

        // Save NumericUpDown value to cell
        private void SaveNumericValue()
        {
            if (qtyUpDown.Tag is DataGridViewCellEventArgs cell)
            {
                dgvCartDetails.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = qtyUpDown.Value;
            }
        }
    }
}