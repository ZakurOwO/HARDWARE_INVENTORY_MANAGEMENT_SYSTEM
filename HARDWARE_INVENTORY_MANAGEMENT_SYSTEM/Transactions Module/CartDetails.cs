using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class CartDetails : UserControl
    {
        NumericUpDown qtyUpDown = new NumericUpDown();
        public CartDetails()
        {
            InitializeComponent();
        }

        private void Walk_inCartDetails_Load(object sender, EventArgs e)
        {
            
            SelectTab(btnWalkIn);
            dgvCartDetails.Rows.Add("Roofing Shingles", 1, "₱125.00");
            dgvCartDetails.Rows.Add("Cement", 1, "₱230.00");
            dgvCartDetails.Rows.Add("Paint", 2, "₱350.00");

            dgvCartDetails.Columns["Price"].ReadOnly = true;

            // Add the NumericUpDown control (initially hidden)

            qtyUpDown.Minimum = 1;
            qtyUpDown.Maximum = 1000;
            qtyUpDown.Visible = false;

            // Style
            qtyUpDown.BorderStyle = BorderStyle.None;
            qtyUpDown.TextAlign = HorizontalAlignment.Center;
            qtyUpDown.Font = dgvCartDetails.DefaultCellStyle.Font;
            qtyUpDown.BackColor = dgvCartDetails.DefaultCellStyle.BackColor;
            qtyUpDown.ForeColor = dgvCartDetails.DefaultCellStyle.ForeColor;

            // Ensure manual sizing and positioning
            qtyUpDown.AutoSize = false;
            dgvCartDetails.Controls.Add(qtyUpDown);

            // Event handlers
            dgvCartDetails.CellClick += dgvCartDetails_CellClick;
            qtyUpDown.Leave += qtyUpDown_Leave;
            qtyUpDown.ValueChanged += qtyUpDown_ValueChanged;

        }

        private void SelectTab(Guna2Button selectedButton)
        {
            //reset buttons
            btnWalkIn.FillColor = Color.White;
            btnWalkIn.ForeColor = Color.Black;
            btnWalkIn.Font = new Font (btnWalkIn.Font, FontStyle.Regular);

            btnDelivery.FillColor = Color.White;
            btnDelivery.ForeColor = Color.Black;
            btnDelivery.Font = new Font(btnWalkIn.Font, FontStyle.Regular);

            
            selectedButton.FillColor = Color.FromArgb(229, 240, 249); //light blue
            selectedButton.ForeColor = Color.FromArgb(42, 134, 205);   //dark blue
            selectedButton.Font = new Font (selectedButton.Font, FontStyle.Bold);
            selectedButton.BorderRadius = 3;
        }

        private void btnWalkIn_Click(object sender, EventArgs e)
        {
            SelectTab(btnWalkIn);
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            SelectTab(btnDelivery);
        }

        private void dgvCartDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvCartDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Only show NumericUpDown for the QTY column
            if (e.RowIndex >= 0 && dgvCartDetails.Columns[e.ColumnIndex].HeaderText == "QTY")
            {
                Rectangle rect = dgvCartDetails.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                // Calculate perfect fit
                int cellHeight = rect.Height;
                qtyUpDown.Height = cellHeight - 9;     // match cell height closely
                qtyUpDown.Width = rect.Width - 4;      // avoid touching borders

                qtyUpDown.Location = new Point(rect.X + 2, rect.Y + (rect.Height - qtyUpDown.Height) / 2);


                // Set current value
                qtyUpDown.Value = Convert.ToDecimal(dgvCartDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? 1);

                qtyUpDown.Tag = e; // store which cell is active
                qtyUpDown.Visible = true;
                qtyUpDown.BringToFront();
                qtyUpDown.Focus();
                
            }
            else
            {
                qtyUpDown.Visible = false;
            }
        }

        private void qtyUpDown_Leave(object sender, EventArgs e)
        {
            SaveNumericValue();
        }

        private void qtyUpDown_ValueChanged(object sender, EventArgs e)
        {
            SaveNumericValue();
        }

        private void SaveNumericValue()
        {
            if (qtyUpDown.Tag is DataGridViewCellEventArgs cell)
            {
                dgvCartDetails.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = qtyUpDown.Value;
            }
        }
    

}
}
