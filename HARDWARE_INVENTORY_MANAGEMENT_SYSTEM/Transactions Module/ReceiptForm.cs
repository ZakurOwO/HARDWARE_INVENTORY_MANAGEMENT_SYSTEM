using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class ReceiptPreviewForm : Form
    {
        private readonly List<ReceiptItem> _items;
        private readonly decimal _subtotal;
        private readonly decimal _tax;
        private readonly decimal _total;
        private readonly ReceiptData _data;

        private Panel root;
        private Panel itemsPanel;
        private Label lblTxn;
        private Label lblDate;
        private Label lblPayment;
        private Label lblSubtotal;
        private Label lblTax;
        private Label lblTotal;

        private Button btnClose;
        private Button btnPrint;

        private PrintDocument _printDoc;
        private PrintPreviewDialog _preview;

        public ReceiptPreviewForm(List<ReceiptItem> items, decimal subtotal, decimal tax, decimal total, decimal taxRate)
        {
            _items = items ?? new List<ReceiptItem>();
            _subtotal = subtotal;
            _tax = tax;
            _total = total;
            _data = null;

            BuildUI();
            FillData();
            SetupPrinting();
        }

        public ReceiptPreviewForm(ReceiptData data)
        {
            _data = data ?? new ReceiptData();
            _items = _data.Items ?? new List<ReceiptItem>();
            _subtotal = _data.Subtotal;
            _tax = _data.Tax;
            _total = _data.Total;

            BuildUI();
            FillData();
            SetupPrinting();
        }

        private void BuildUI()
        {
            Text = "Invoice / Receipt";
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(720, 500);

            root = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            Controls.Add(root);

            // Header
            var logo = new Panel { Size = new Size(54, 54), Location = new Point((ClientSize.Width - 40) / 2, 24) };
            logo.Paint += DrawLogo;
            root.Controls.Add(logo);

            var lblCompany = new Label
            {
                Text = "Topaz Hardware",
                AutoSize = true,
                Font = MakeFont(18f, true),
                ForeColor = Color.FromArgb(37, 99, 235)
            };
            lblCompany.Location = new Point((ClientSize.Width - lblCompany.PreferredWidth) / 2, 86);
            root.Controls.Add(lblCompany);

            var lblSub = new Label
            {
                Text = "Hardware & Construction Supplies",
                AutoSize = true,
                Font = MakeFont(10.5f),
                ForeColor = Color.FromArgb(75, 85, 99)
            };
            lblSub.Location = new Point((ClientSize.Width - lblSub.PreferredWidth) / 2, 120);
            root.Controls.Add(lblSub);

            var lblDoc = new Label
            {
                Text = "INVOICE / RECEIPT",
                AutoSize = true,
                Font = MakeFont(10f, true),
                ForeColor = Color.FromArgb(107, 114, 128)
            };
            lblDoc.Location = new Point((ClientSize.Width - lblDoc.PreferredWidth) / 2, 146);
            root.Controls.Add(lblDoc);

            // Meta
            int left = 60;
            int top = 190;

            lblTxn = NewMetaLabel(left, top);
            root.Controls.Add(lblTxn);
            top += 22;

            lblDate = NewMetaLabel(left, top);
            root.Controls.Add(lblDate);
            top += 22;

            lblPayment = NewMetaLabel(left, top);
            root.Controls.Add(lblPayment);
            top += 26;

            // Items card
            var card = new Panel
            {
                Location = new Point(left, top),
                Size = new Size(ClientSize.Width - left * 2, 430),
                BackColor = Color.White
            };
            card.Paint += DrawCard;
            root.Controls.Add(card);

            // Table header
            var header = new Panel
            {
                Location = new Point(0, 18),
                Size = new Size(card.Width, 42),
                BackColor = Color.FromArgb(240, 246, 255)
            };
            card.Controls.Add(header);

            header.Controls.Add(NewHeader("Item", 20, 0, 260, ContentAlignment.MiddleLeft));
            header.Controls.Add(NewHeader("Qty", 290, 0, 60, ContentAlignment.MiddleCenter));
            header.Controls.Add(NewHeader("Price", 350, 0, 110, ContentAlignment.MiddleRight));
            header.Controls.Add(NewHeader("Total", 460, 0, 130, ContentAlignment.MiddleRight));

            itemsPanel = new Panel
            {
                Location = new Point(0, 60),
                Size = new Size(card.Width, 260),
                BackColor = Color.Transparent,
                AutoScroll = true
            };
            card.Controls.Add(itemsPanel);

            // If you want it to keep bottom when panel size changes:
            itemsPanel.Resize += (s, e) => ScrollItemsToBottom();

            // Totals area
            int totalsY = 330;
            lblSubtotal = NewTotalLine(card, "Subtotal:", totalsY); totalsY += 15;
            lblTax = NewTotalLine(card, "Tax:", totalsY); totalsY += 15;

            // Total bar
            var totalBar = new Panel
            {
                Location = new Point(0, card.Height - 64),
                Size = new Size(card.Width, 64),
                BackColor = Color.FromArgb(37, 99, 235)
            };
            card.Controls.Add(totalBar);

            var t1 = new Label
            {
                Text = "TOTAL",
                AutoSize = true,
                Font = MakeFont(14f, true),
                ForeColor = Color.White,
                Location = new Point(20, 20)
            };
            totalBar.Controls.Add(t1);

            lblTotal = new Label
            {
                Text = "₱0.00",
                AutoSize = true,
                Font = MakeFont(14f, true),
                ForeColor = Color.White
            };
            totalBar.Controls.Add(lblTotal);

            totalBar.Resize += delegate
            {
                lblTotal.Location = new Point(totalBar.Width - lblTotal.PreferredWidth - 20, 20);
            };
            lblTotal.Location = new Point(totalBar.Width - lblTotal.PreferredWidth - 20, 20);

            // Buttons
            int btnY = card.Bottom + 40;

            btnClose = new Button
            {
                Text = "Close",
                Size = new Size(180, 46),
                Location = new Point((ClientSize.Width / 2) - 200, btnY),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(17, 24, 39),
                Font = MakeFont(10.5f)
            };
            btnClose.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnClose.FlatAppearance.BorderSize = 2;
            btnClose.Click += (s, e) => Close();
            root.Controls.Add(btnClose);

            btnPrint = new Button
            {
                Text = "Print Preview",
                Size = new Size(180, 46),
                Location = new Point((ClientSize.Width / 2) + 20, btnY),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(37, 99, 235),
                ForeColor = Color.White,
                Font = MakeFont(10.5f, true)
            };
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Click += BtnPrint_Click;
            root.Controls.Add(btnPrint);
        }

        private void FillData()
        {
            string txnId = _data != null && !string.IsNullOrWhiteSpace(_data.TransactionId)
                ? _data.TransactionId
                : "SALE-" + DateTime.Now.ToString("yyyyMMddHHmmss");

            DateTime dt = _data != null && _data.TransactionDate != default(DateTime)
                ? _data.TransactionDate
                : DateTime.Now;

            string payment = _data != null && !string.IsNullOrWhiteSpace(_data.PaymentMethod)
                ? _data.PaymentMethod
                : "-";

            lblTxn.Text = "Transaction ID:  " + txnId;
            lblDate.Text = "Date:            " + dt.ToString("MM/dd/yyyy HH:mm");
            lblPayment.Text = "Payment:         " + payment;

            itemsPanel.Controls.Clear();

            int y = 0;
            foreach (var item in _items)
            {
                itemsPanel.Controls.Add(NewRow(item, y));
                y += 36;
            }

            // Make scroll area explicit (helps AutoScroll)
            itemsPanel.AutoScrollMinSize = new Size(0, y + 10);

            // Scroll after layout completes (reliable)
            ScrollItemsToBottom();

            lblSubtotal.Text = FormatPeso(_subtotal);
            lblTax.Text = FormatPeso(_tax);
            lblTotal.Text = FormatPeso(_total);
        }

        private Control NewRow(ReceiptItem item, int y)
        {
            var row = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(itemsPanel.Width - 18, 36),
                BackColor = Color.Transparent
            };

            row.Controls.Add(NewCell(item.ItemName, 20, 8, 260, ContentAlignment.MiddleLeft));
            row.Controls.Add(NewCell(item.Quantity.ToString(), 290, 8, 60, ContentAlignment.MiddleCenter));
            row.Controls.Add(NewCell(FormatPeso(item.UnitPrice), 350, 8, 110, ContentAlignment.MiddleRight));
            row.Controls.Add(NewCell(FormatPeso(item.UnitPrice * item.Quantity), 460, 8, 130, ContentAlignment.MiddleRight));

            var line = new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = Color.FromArgb(229, 231, 235) };
            row.Controls.Add(line);

            return row;
        }

        private Label NewMetaLabel(int x, int y)
        {
            return new Label
            {
                AutoSize = true,
                Location = new Point(x, y),
                Font = MakeFont(10f, true),
                ForeColor = Color.FromArgb(17, 24, 39)
            };
        }

        private Label NewHeader(string text, int x, int y, int w, ContentAlignment align)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, 42),
                TextAlign = align,
                Font = MakeFont(10f, true),
                ForeColor = Color.FromArgb(31, 41, 55)
            };
        }

        private Label NewCell(string text, int x, int y, int w, ContentAlignment align)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, 20),
                TextAlign = align,
                Font = MakeFont(9.5f),
                ForeColor = Color.FromArgb(31, 41, 55)
            };
        }

        private Label NewTotalLine(Panel card, string title, int y)
        {
            var t = new Label
            {
                Text = title,
                AutoSize = true,
                Font = MakeFont(10f),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(card.Width - 240, y)
            };
            card.Controls.Add(t);

            var v = new Label
            {
                Text = "₱0.00",
                AutoSize = true,
                Font = MakeFont(10f),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(card.Width - 90, y)
            };
            card.Controls.Add(v);

            return v;
        }

        private Font MakeFont(float size, bool bold = false)
        {
            FontStyle style = bold ? FontStyle.Bold : FontStyle.Regular;
            try { return new Font("Lexend", size, style); }
            catch { return new Font("Segoe UI", size, style); }
        }

        private static string FormatPeso(decimal value) => "₱" + value.ToString("N2");

        private void DrawLogo(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(4, 4, 46, 46);

            using (var path = Rounded(rect, 12))
            using (var brush = new LinearGradientBrush(rect, Color.FromArgb(59, 130, 246), Color.FromArgb(234, 179, 8), 45f))
            {
                e.Graphics.FillPath(brush, path);
            }
        }

        private void DrawCard(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var p = (Panel)sender;
            Rectangle rect = new Rectangle(0, 0, p.Width - 1, p.Height - 1);

            using (var path = Rounded(rect, 22))
            using (var bg = new SolidBrush(Color.FromArgb(245, 249, 255)))
            using (var pen = new Pen(Color.FromArgb(229, 231, 235)))
            {
                e.Graphics.FillPath(bg, path);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private static GraphicsPath Rounded(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void SetupPrinting()
        {
            _printDoc = new PrintDocument();
            _printDoc.PrintPage += PrintDoc_PrintPage;

            _preview = new PrintPreviewDialog
            {
                Document = _printDoc,
                Width = 1000,
                Height = 750
            };
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try { _preview.ShowDialog(this); }
            catch (Exception ex)
            {
                MessageBox.Show("Print preview failed: " + ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int x = 80, y = 60;

            using (var titleFont = MakeFont(16f, true))
            using (var normal = MakeFont(10f))
            using (var bold = MakeFont(10f, true))
            {
                g.DrawString("Topaz Hardware", titleFont, Brushes.Black, x, y); y += 28;
                g.DrawString("INVOICE / RECEIPT", bold, Brushes.Gray, x, y); y += 28;

                g.DrawString(lblTxn.Text, bold, Brushes.Black, x, y); y += 18;
                g.DrawString(lblDate.Text, bold, Brushes.Black, x, y); y += 18;
                g.DrawString(lblPayment.Text, bold, Brushes.Black, x, y); y += 26;

                g.DrawString("Item", bold, Brushes.Black, x, y);
                g.DrawString("Qty", bold, Brushes.Black, x + 260, y);
                g.DrawString("Price", bold, Brushes.Black, x + 320, y);
                g.DrawString("Total", bold, Brushes.Black, x + 420, y);
                y += 20;

                foreach (var it in _items)
                {
                    g.DrawString(it.ItemName, normal, Brushes.Black, x, y);
                    g.DrawString(it.Quantity.ToString(), normal, Brushes.Black, x + 260, y);
                    g.DrawString(FormatPeso(it.UnitPrice), normal, Brushes.Black, x + 320, y);
                    g.DrawString(FormatPeso(it.UnitPrice * it.Quantity), normal, Brushes.Black, x + 420, y);
                    y += 18;
                }

                y += 18;
                g.DrawString("Subtotal: " + lblSubtotal.Text, bold, Brushes.Black, x + 300, y); y += 18;
                g.DrawString("Tax:      " + lblTax.Text, bold, Brushes.Black, x + 300, y); y += 18;
                g.DrawString("TOTAL:    " + lblTotal.Text, titleFont, Brushes.Black, x + 220, y);
            }
        }

        // ✅ Reliable auto-scroll to bottom (after layout)
        private void ScrollItemsToBottom()
        {
            if (itemsPanel == null) return;

            void doScroll()
            {
                if (itemsPanel.Controls.Count == 0) return;

                var last = itemsPanel.Controls[itemsPanel.Controls.Count - 1];
                itemsPanel.ScrollControlIntoView(last);
            }

            // If handle isn't ready yet, wait for it.
            if (!itemsPanel.IsHandleCreated)
            {
                EventHandler handler = null;
                handler = (s, e) =>
                {
                    itemsPanel.HandleCreated -= handler;
                    itemsPanel.BeginInvoke((Action)doScroll);
                };
                itemsPanel.HandleCreated += handler;
                return;
            }

            // Handle exists: safe to BeginInvoke now
            itemsPanel.BeginInvoke((Action)doScroll);
        }

    }
}
