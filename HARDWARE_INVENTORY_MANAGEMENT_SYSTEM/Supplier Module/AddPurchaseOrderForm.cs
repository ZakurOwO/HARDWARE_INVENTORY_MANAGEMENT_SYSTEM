using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{

    public partial class AddPurchaseOrderForm : UserControl
    {
        int maxHeight = 225;
        public AddPurchaseOrderForm()
        {
            InitializeComponent();
            guna2Panel1.HorizontalScroll.Enabled = false;

        }

        private void AdjustGridHeight()
        {
            int header = dgvPurchaseItems.ColumnHeadersHeight;
            int rowHeight = dgvPurchaseItems.RowTemplate.Height;
            int rowCount = dgvPurchaseItems.Rows.Count;

            int estimatedHeight = header + (rowCount * rowHeight);

            if (estimatedHeight < maxHeight)
            {
                dgvPurchaseItems.Height = estimatedHeight + 2; // small padding
                dgvPurchaseItems.ScrollBars = ScrollBars.None;
            }
            else
            {
                dgvPurchaseItems.Height = maxHeight;
                dgvPurchaseItems.ScrollBars = ScrollBars.Vertical;
            }
        }

        private void AdjustLayout()
        {
            // ========== 1. Adjust DataGridView height ==========
            int rowHeight = dgvPurchaseItems.RowTemplate.Height;
            int headerHeight = dgvPurchaseItems.ColumnHeadersHeight;

            int rowCount = dgvPurchaseItems.Rows.Count;

            int neededHeight = headerHeight + (rowHeight * rowCount);
            int finalHeight = Math.Min(neededHeight, 225);

            dgvPurchaseItems.Height = finalHeight;

            // ========== 2. Move calculation panel ==========
            calculationPanel.Top = dgvPurchaseItems.Bottom + 10;

            // ========== 3. Resize OrderItemsParentPanel ==========
            dgvPurchaseItems.Height = calculationPanel.Bottom + 15;

            // ========== 4. Move NotesPanel ==========
            dgvPurchaseItems.Top = dgvPurchaseItems.Bottom + 20;

            // ========== 5. Resize MainParentContainer ==========
            dgvPurchaseItems.Height = dgvPurchaseItems.Bottom + 20;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AdjustGridHeight();
            AdjustLayout();
        }

        private void btnWhite_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.Close();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.Close();
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
