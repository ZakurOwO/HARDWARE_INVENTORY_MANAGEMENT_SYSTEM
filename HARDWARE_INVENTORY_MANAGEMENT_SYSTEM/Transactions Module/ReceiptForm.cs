using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public sealed class ReceiptForm : Form
    {
        private readonly ReceiptData _data;

        private readonly Guna2Panel root = new Guna2Panel();
        private readonly Guna2Panel card = new Guna2Panel();

        private readonly Guna2HtmlLabel lblStore = new Guna2HtmlLabel();
        private readonly Guna2HtmlLabel lblSubtitle = new Guna2HtmlLabel();
        private readonly Guna2HtmlLabel lblDocTitle = new Guna2HtmlLabel();

        private readonly Guna2HtmlLabel lblTxn = new Guna2HtmlLabel();
        private readonly Guna2HtmlLabel lblDate = new Guna2HtmlLabel();
        private readonly Guna2HtmlLabel lblType = new Guna2HtmlLabel();

        private readonly Guna2Panel tableShell = new Guna2Panel();
        private readonly Guna2DataGridView dgv = new Guna2DataGridView();

        private readonly Guna2HtmlLabel lblPayTitle = new Guna2HtmlLabel();
        private readonly Guna2HtmlLabel lblPayValue = new Guna2HtmlLabel();

        private readonly Guna2HtmlLabel lblSubtotalTitle = new Guna2HtmlLabel();
        private readonly Guna2HtmlLabel lblTaxTitle = new Guna2HtmlLabel();
        private readonly Guna2HtmlLabel lblSubtotalValue = new Guna2HtmlLabel();
        private readonly Guna2HtmlLabel lblTaxValue = new Guna2HtmlLabel();

        private readonly Guna2Panel totalBar = new Guna2Panel();
        private readonly Guna2HtmlLabel lblTotalTitle = new Guna2HtmlLabel();
        private readonly Guna2HtmlLabel lblTotalValue = new Guna2HtmlLabel();

        private readonly Guna2HtmlLabel lblThanks = new Guna2HtmlLabel();

        private readonly Guna2Button btnNew = new Guna2Button();
        private readonly Guna2Button btnPrint = new Guna2Button();

        private readonly PrintDocument printDoc = new PrintDocument();
        private Bitmap cardBitmap;

        public ReceiptForm(ReceiptData data)
        {
            if (data == null) throw new ArgumentNullException("data");
            _data = data;

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.FromArgb(245, 247, 250);

            BuildUI();
            BindData();

            btnNew.Click += delegate { Close(); };
            btnPrint.Click += delegate { PrintReceipt(); };
            printDoc.PrintPage += PrintDoc_PrintPage;
        }

        private Font Lexend(float size, FontStyle style)
        {
            try { return new Font("Lexend", size, style); }
            catch { return new Font("Lexend", size, style); }
        }

        private static string Money(decimal v)
        {
            return "₱" + v.ToString("N2", CultureInfo.InvariantCulture);
        }

        private void BuildUI()
        {
            root.Dock = DockStyle.Fill;
            root.BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(root);

            card.Size = new Size(980, 820);
            card.FillColor = Color.White;
            card.BorderRadius = 18;
            card.ShadowDecoration.Enabled = true;
            card.ShadowDecoration.Shadow = new Padding(0, 6, 0, 12);
            root.Controls.Add(card);

            root.Resize += delegate
            {
                card.Location = new Point((root.Width - card.Width) / 2, 60);

                int y = card.Bottom + 40;
                int totalWidth = 280 + 28 + 280;
                int x = (root.Width - totalWidth) / 2;

                btnNew.Location = new Point(x, y);
                btnPrint.Location = new Point(x + 280 + 28, y);
            };
            card.Location = new Point((root.Width - card.Width) / 2, 60);

            lblStore.Font = Lexend(22f, FontStyle.Bold);
            lblStore.ForeColor = Color.FromArgb(22, 117, 255);
            lblStore.BackColor = Color.Transparent;
            lblStore.AutoSize = true;

            lblSubtitle.Font = Lexend(12f, FontStyle.Regular);
            lblSubtitle.ForeColor = Color.FromArgb(70, 70, 70);
            lblSubtitle.BackColor = Color.Transparent;
            lblSubtitle.AutoSize = true;

            lblDocTitle.Font = Lexend(13f, FontStyle.Regular);
            lblDocTitle.ForeColor = Color.FromArgb(70, 70, 70);
            lblDocTitle.BackColor = Color.Transparent;
            lblDocTitle.AutoSize = true;

            card.Controls.Add(lblStore);
            card.Controls.Add(lblSubtitle);
            card.Controls.Add(lblDocTitle);

            lblTxn.Font = Lexend(11f, FontStyle.Regular);
            lblTxn.ForeColor = Color.FromArgb(35, 35, 35);
            lblTxn.BackColor = Color.Transparent;
            lblTxn.AutoSize = true;
            lblTxn.Location = new Point(60, 150);

            lblDate.Font = Lexend(11f, FontStyle.Regular);
            lblDate.ForeColor = Color.FromArgb(35, 35, 35);
            lblDate.BackColor = Color.Transparent;
            lblDate.AutoSize = true;
            lblDate.Location = new Point(60, 180);

            lblType.Font = Lexend(11f, FontStyle.Regular);
            lblType.ForeColor = Color.FromArgb(35, 35, 35);
            lblType.BackColor = Color.Transparent;
            lblType.AutoSize = true;
            lblType.Location = new Point(60, 210);

            card.Controls.Add(lblTxn);
            card.Controls.Add(lblDate);
            card.Controls.Add(lblType);

            tableShell.FillColor = Color.FromArgb(242, 248, 252);
            tableShell.BorderRadius = 16;
            tableShell.Location = new Point(60, 250);
            tableShell.Size = new Size(860, 360);
            card.Controls.Add(tableShell);

            dgv.Dock = DockStyle.Fill;
            dgv.BackgroundColor = tableShell.FillColor;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = Color.FromArgb(220, 234, 244);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowTemplate.Height = 48;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = tableShell.FillColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dgv.ColumnHeadersDefaultCellStyle.Font = Lexend(12f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersHeight = 54;

            dgv.DefaultCellStyle.BackColor = tableShell.FillColor;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(35, 35, 35);
            dgv.DefaultCellStyle.Font = Lexend(11f, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 240, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 35, 35);

            dgv.Columns.Clear();
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Item",
                HeaderText = "Item",
                FillWeight = 55,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "QTY",
                HeaderText = "QTY",
                FillWeight = 15,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Price",
                HeaderText = "Price",
                FillWeight = 15,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Total",
                HeaderText = "Total",
                FillWeight = 15,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            tableShell.Controls.Add(dgv);

            lblPayTitle.Text = "Payment Method:";
            lblPayTitle.Font = Lexend(11f, FontStyle.Bold);
            lblPayTitle.ForeColor = Color.FromArgb(35, 35, 35);
            lblPayTitle.BackColor = Color.Transparent;
            lblPayTitle.AutoSize = true;
            lblPayTitle.Location = new Point(80, 630);

            lblPayValue.Font = Lexend(11f, FontStyle.Regular);
            lblPayValue.ForeColor = Color.FromArgb(35, 35, 35);
            lblPayValue.BackColor = Color.Transparent;
            lblPayValue.AutoSize = true;
            lblPayValue.Location = new Point(80, 660);

            card.Controls.Add(lblPayTitle);
            card.Controls.Add(lblPayValue);

            lblSubtotalTitle.Text = "Subtotal:";
            lblSubtotalTitle.Font = Lexend(11f, FontStyle.Regular);
            lblSubtotalTitle.ForeColor = Color.FromArgb(35, 35, 35);
            lblSubtotalTitle.BackColor = Color.Transparent;
            lblSubtotalTitle.AutoSize = true;
            lblSubtotalTitle.Location = new Point(560, 630);

            lblSubtotalValue.Font = Lexend(11f, FontStyle.Regular);
            lblSubtotalValue.ForeColor = Color.FromArgb(35, 35, 35);
            lblSubtotalValue.BackColor = Color.Transparent;
            lblSubtotalValue.AutoSize = true;
            lblSubtotalValue.Location = new Point(840, 630);

            lblTaxTitle.Text = "Tax:";
            lblTaxTitle.Font = Lexend(11f, FontStyle.Regular);
            lblTaxTitle.ForeColor = Color.FromArgb(35, 35, 35);
            lblTaxTitle.BackColor = Color.Transparent;
            lblTaxTitle.AutoSize = true;
            lblTaxTitle.Location = new Point(560, 660);

            lblTaxValue.Font = Lexend(11f, FontStyle.Regular);
            lblTaxValue.ForeColor = Color.FromArgb(35, 35, 35);
            lblTaxValue.BackColor = Color.Transparent;
            lblTaxValue.AutoSize = true;
            lblTaxValue.Location = new Point(840, 660);

            card.Controls.Add(lblSubtotalTitle);
            card.Controls.Add(lblSubtotalValue);
            card.Controls.Add(lblTaxTitle);
            card.Controls.Add(lblTaxValue);

            totalBar.FillColor = Color.FromArgb(38, 132, 255);
            totalBar.BorderRadius = 14;
            totalBar.Size = new Size(860, 70);
            totalBar.Location = new Point(60, 700);
            card.Controls.Add(totalBar);

            lblTotalTitle.Text = "Total:";
            lblTotalTitle.Font = Lexend(16f, FontStyle.Bold);
            lblTotalTitle.ForeColor = Color.White;
            lblTotalTitle.BackColor = Color.Transparent;
            lblTotalTitle.AutoSize = true;
            lblTotalTitle.Location = new Point(24, 20);

            lblTotalValue.Font = Lexend(16f, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.White;
            lblTotalValue.BackColor = Color.Transparent;
            lblTotalValue.AutoSize = true;
            lblTotalValue.Location = new Point(600, 20);

            totalBar.Controls.Add(lblTotalTitle);
            totalBar.Controls.Add(lblTotalValue);

            lblThanks.Text = "Thank you for the business!";
            lblThanks.Font = Lexend(11f, FontStyle.Regular);
            lblThanks.ForeColor = Color.FromArgb(60, 60, 60);
            lblThanks.BackColor = Color.Transparent;
            lblThanks.AutoSize = true;
            lblThanks.Location = new Point(0, 785);
            card.Controls.Add(lblThanks);

            btnNew.Text = "New Transaction";
            btnNew.Size = new Size(280, 56);
            btnNew.FillColor = Color.White;
            btnNew.BorderColor = Color.FromArgb(220, 220, 220);
            btnNew.BorderThickness = 1;
            btnNew.BorderRadius = 14;
            btnNew.Font = Lexend(12f, FontStyle.Regular);
            btnNew.ForeColor = Color.FromArgb(40, 40, 40);
            btnNew.HoverState.FillColor = Color.FromArgb(245, 245, 245);
            root.Controls.Add(btnNew);

            btnPrint.Text = "Print Invoice";
            btnPrint.Size = new Size(280, 56);
            btnPrint.FillColor = Color.FromArgb(14, 99, 213);
            btnPrint.BorderRadius = 14;
            btnPrint.Font = Lexend(12f, FontStyle.Regular);
            btnPrint.ForeColor = Color.White;
            btnPrint.HoverState.FillColor = Color.FromArgb(18, 112, 235);
            root.Controls.Add(btnPrint);

            // initial button placement
            int y0 = card.Bottom + 40;
            int totalWidth0 = 280 + 28 + 280;
            int x0 = (root.Width - totalWidth0) / 2;
            btnNew.Location = new Point(x0, y0);
            btnPrint.Location = new Point(x0 + 280 + 28, y0);
        }

        private void BindData()
        {
            lblStore.Text = _data.StoreName;
            lblSubtitle.Text = _data.StoreSubtitle;
            lblDocTitle.Text = _data.DocumentTitle;

            // center header
            lblStore.Location = new Point((card.Width - lblStore.Width) / 2, 30);
            lblSubtitle.Location = new Point((card.Width - lblSubtitle.Width) / 2, 72);
            lblDocTitle.Location = new Point((card.Width - lblDocTitle.Width) / 2, 105);

            lblTxn.Text = "<b>Transaction ID:</b> " + _data.TransactionId;
            lblDate.Text = "<b>Date:</b> " + _data.TransactionDate.ToString("MM/dd/yyyy HH:mm");
            lblType.Text = "<b>Type:</b> " + _data.TransactionType;

            dgv.Rows.Clear();
            for (int i = 0; i < _data.Items.Count; i++)
            {
                ReceiptItem it = _data.Items[i];
                dgv.Rows.Add(it.ItemName, it.Quantity.ToString(), Money(it.UnitPrice), Money(it.LineTotal));
            }

            lblPayValue.Text = _data.PaymentMethod;

            lblSubtotalValue.Text = Money(_data.Subtotal);
            lblTaxValue.Text = Money(_data.Tax);
            lblTotalValue.Text = " " + Money(_data.Total);

            // right-align subtotal&tax values visually
            lblSubtotalValue.Location = new Point(900 - lblSubtotalValue.Width, lblSubtotalValue.Location.Y);
            lblTaxValue.Location = new Point(900 - lblTaxValue.Width, lblTaxValue.Location.Y);

            // right-align total inside blue bar
            lblTotalValue.Location = new Point(totalBar.Width - 24 - lblTotalValue.Width, lblTotalValue.Location.Y);

            // center thanks
            lblThanks.Location = new Point((card.Width - lblThanks.Width) / 2, lblThanks.Location.Y);
        }

        private void PrintReceipt()
        {
            cardBitmap = new Bitmap(card.Width, card.Height);
            card.DrawToBitmap(cardBitmap, new Rectangle(0, 0, card.Width, card.Height));

            using (PrintDialog dlg = new PrintDialog())
            {
                dlg.Document = printDoc;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    printDoc.Print();
                }
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (cardBitmap == null) return;

            Rectangle m = e.MarginBounds;

            int w = cardBitmap.Width;
            int h = cardBitmap.Height;

            float ratio = Math.Min((float)m.Width / (float)w, (float)m.Height / (float)h);
            int pw = (int)(w * ratio);
            int ph = (int)(h * ratio);

            int x = m.Left + (m.Width - pw) / 2;
            int y = m.Top + (m.Height - ph) / 2;

            e.Graphics.DrawImage(cardBitmap, x, y, pw, ph);
        }
    }
}
