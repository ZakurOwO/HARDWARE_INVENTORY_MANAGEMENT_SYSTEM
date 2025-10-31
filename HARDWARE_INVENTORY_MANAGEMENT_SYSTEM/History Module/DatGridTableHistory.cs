using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module
{
    public partial class DatGridTableHistory : UserControl
    {
        public DatGridTableHistory()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            Load += DatGridTableHistory_Load;

        }

        private void DatGridTableHistory_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            dgvHistory.VirtualMode = false;

            // Create columns only once
            if (dgvHistory.Columns.Count == 0)
            {
                dgvHistory.Columns.Add("TransactionNo", "Transaction #");
                dgvHistory.Columns.Add("Type", "Type");
                dgvHistory.Columns.Add("Date", "Date");
                dgvHistory.Columns.Add("CustomerSupplier", "Customer/Supplier");
                dgvHistory.Columns.Add("Item", "Item");
                dgvHistory.Columns.Add("Amount", "Amount");
                dgvHistory.Columns.Add("Status", "Status");
            }

            // Clear old rows
            dgvHistory.Rows.Clear();

            // ✅ Add test row
            dgvHistory.Rows.Add(
                "SL-0001234",
                "Sale",
                "10/31/2025 8:00 AM",
                "Walk-in Customer",
                "Sample",
                "₱260.00",
                "Completed"
            );

            // ✅ Appearance
            dgvHistory.ReadOnly = true;
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.GridColor = Color.LightGray;

            // ✅ Center all columns
            foreach (DataGridViewColumn col in dgvHistory.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            // ✅ Header styling
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvHistory.EnableHeadersVisualStyles = false;

            // ✅ Row styling
            dgvHistory.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvHistory.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            dgvHistory.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvHistory.RowTemplate.Height = 35;

            // ✅ Custom cell painting for Status column
            dgvHistory.CellPainting += dgvHistory_CellPainting;
        }

        private void dgvHistory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dgvHistory.Columns["Status"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                string status = e.Value?.ToString() ?? "";
                Color backColor = Color.White;
                Color textColor = Color.Black;
                string icon = "";

                switch (status)
                {
                    case "Completed":
                        backColor = Color.FromArgb(204, 255, 204);
                        textColor = Color.FromArgb(0, 153, 0);
                        icon = "✔";
                        break;
                    case "Canceled":
                        backColor = Color.FromArgb(255, 204, 204);
                        textColor = Color.FromArgb(204, 0, 0);
                        icon = "✖";
                        break;
                    case "Pending":
                        backColor = Color.FromArgb(255, 255, 204);
                        textColor = Color.FromArgb(204, 153, 0);
                        icon = "⏳";
                        break;
                }

                // Combine icon + text
                string displayText = $"{icon} {status}";
                using (Font font = new Font(dgvHistory.Font.FontFamily, 8.25f, FontStyle.Bold))
                {
                    // Balanced, natural badge size
                    Size textSize = TextRenderer.MeasureText(displayText, font);
                    int badgeWidth = textSize.Width + 16;   // compact padding
                    int badgeHeight = textSize.Height - 1;  // fits more naturally in cell

                    // Perfect centering
                    int badgeX = e.CellBounds.X + (e.CellBounds.Width - badgeWidth) / 2;
                    int badgeY = e.CellBounds.Y + (e.CellBounds.Height - badgeHeight) / 2 + 1;

                    Rectangle rect = new Rectangle(badgeX, badgeY, badgeWidth, badgeHeight);

                    using (GraphicsPath path = RoundedRect(rect, 7))
                    using (SolidBrush brush = new SolidBrush(backColor))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.FillPath(brush, path);
                    }

                    // Draw text
                    TextRenderer.DrawText(
                        e.Graphics,
                        displayText,
                        font,
                        rect,
                        textColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                    );
                }

                e.Handled = true;
            }
        }



        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void dgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
