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
        
        public CartDetails()
        {
            InitializeComponent();
            
        }

        private void CartDetails_Load(object sender, EventArgs e)
        {
            walkinOrDeliveryButton1.ShowWalkIn += WalkinOrDeliveryButton_ShowWalkIn;
            walkinOrDeliveryButton1.ShowDelivery += WalkinOrDeliveryButton_ShowDelivery;

            // Show Walk-In by default
            ShowWalkInControl();
        }

        private void WalkinOrDeliveryButton_ShowWalkIn(object sender, EventArgs e)
        {
            ShowWalkInControl();
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
                qtyUpDown.Height = cellHeight - 2;     // match cell height closely
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
            ShowDeliveryControl();
        }

        private void ShowWalkInControl()
        {
            panelContainer.Controls.Clear();
            var walkInUC = new Walk_inCartDetails();
            walkInUC.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(walkInUC);
        }

        private void ShowDeliveryControl()
        {
            panelContainer.Controls.Clear();
            var deliveryUC = new DeliveryCartDetails();
            deliveryUC.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(deliveryUC);
        }

    }
}
