using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class ReceiptPreviewForm : Form
    {
        private readonly ReceiptData _data;
        private readonly List<ReceiptItem> _items;
        private readonly decimal _subtotal;
        private readonly decimal _tax;
        private readonly decimal _total;
        private readonly decimal _taxRate;

        // ===== Root =====
        private Panel root;

        // ===== Header =====
        private Panel headerWrap;
        private PictureBox picLogo;
        private Label lblCompany;
        private Label lblTagline;
        private Label lblDocTitle;

        // ===== Meta =====
        private Panel meta;
        private Label lblTxnId;
        private Label lblDate;
        private Label lblType;

        // ===== Body =====
        private Panel body;
        private RoundedPanel tableCard;
        private RoundedPanel tableHeaderPill;
        private Label hdrItem;
        private Label hdrQty;
        private Label hdrPrice;
        private Label hdrTotal;

        private DataGridView dgv;

        private Label lblPaymentTitle;
        private Label lblPaymentValue;

        private RoundedPanel totalsCard;
        private Label lblSubLabel;
        private Label lblSubValue;
        private Label lblTaxLabel;
        private Label lblTaxValue;

        private Panel totalBar;
        private Label lblTotalLabel;
        private Label lblTotalValue;

        // ===== Footer + Buttons =====
        private Panel footerLine;
        private Label lblFooter;
        private Panel buttonsPanel;
        private Button btnNew;
        private Button btnPrint;

        // ===== Printing =====
        private PrintDocument _printDoc;
        private PrintPreviewDialog _preview;

        // ============================================================
        // Constructor you call: new ReceiptPreviewForm(data)
        // ============================================================
        public ReceiptPreviewForm(ReceiptData data)
     : this(
         data?.Items,
         data?.Subtotal ?? 0m,
         data?.Tax ?? 0m,
         data?.Total ?? 0m,
         0.12m
     )
        {
            _data = data;
            ApplyMeta();
            ApplyPayment();
            ApplyTitles();
        }

        // ============================================================
        // Keep your old signature supported too
        // ============================================================
        public ReceiptPreviewForm(List<ReceiptItem> items, decimal subtotal, decimal tax, decimal total, decimal taxRate)
        {
            _items = items ?? new List<ReceiptItem>();
            _subtotal = subtotal;
            _tax = tax;
            _total = total;
            _taxRate = taxRate;

            InitForm();
            BuildUi();
            BindGrid();
            ApplyMeta();
            ApplyPayment();
            ApplyTitles();
            WirePrinting();
        }

        private void InitForm()
        {
            Text = "Receipt Preview";
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(980, 760);
        }

        private void BuildUi()
        {
            // Root padding like mockup
            root = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(46, 28, 46, 24)
            };
            Controls.Add(root);

            // ======================
            // HEADER (centered)
            // ======================
            headerWrap = new Panel
            {
                Dock = DockStyle.Top,
                Height = 150,
                BackColor = Color.White
            };
            root.Controls.Add(headerWrap);

            picLogo = new PictureBox
            {
                Size = new Size(56, 56),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            // If you have a resource logo, set it here:
            // picLogo.Image = Properties.Resources.YourLogo;

            lblCompany = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 110, 206),
                Text = "Topaz Hardware"
            };

            lblTagline = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 13, FontStyle.Regular),
                ForeColor = Color.FromArgb(45, 45, 45),
                Text = "Hardware & Construction Supplies"
            };

            lblDocTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(45, 45, 45),
                Text = "Receipt"
            };

            headerWrap.Controls.Add(picLogo);
            headerWrap.Controls.Add(lblCompany);
            headerWrap.Controls.Add(lblTagline);
            headerWrap.Controls.Add(lblDocTitle);

            headerWrap.Resize += (s, e) =>
            {
                // Center group: logo + texts
                int groupWidth = 56 + 14 + Math.Max(lblCompany.Width, Math.Max(lblTagline.Width, lblDocTitle.Width));
                int startX = (headerWrap.Width - groupWidth) / 2;
                int topY = 10;

                picLogo.Location = new Point(startX, topY + 12);

                int textX = picLogo.Right + 14;
                lblCompany.Location = new Point(textX, topY + 2);
                lblTagline.Location = new Point(textX, lblCompany.Bottom + 4);
                lblDocTitle.Location = new Point(textX, lblTagline.Bottom + 6);
            };

            // ======================
            // META (left aligned)
            // ======================
            meta = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.White
            };
            root.Controls.Add(meta);

            lblTxnId = MetaLine("Transaction ID:", "-");
            lblDate = MetaLine("Date:", "-");
            lblType = MetaLine("Type:", "-");

            lblTxnId.Location = new Point(0, 10);
            lblDate.Location = new Point(0, 48);
            lblType.Location = new Point(0, 86);

            meta.Controls.Add(lblTxnId);
            meta.Controls.Add(lblDate);
            meta.Controls.Add(lblType);

            // ======================
            // BODY
            // ======================
            body = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            root.Controls.Add(body);

            // TABLE CARD (left)
            tableCard = new RoundedPanel
            {
                BackColor = Color.FromArgb(240, 248, 254),
                CornerRadius = 28,
                Location = new Point(0, 10),
                Size = new Size(610, 420),
                Padding = new Padding(18, 18, 18, 18)
            };
            body.Controls.Add(tableCard);

            // Rounded header pill inside table card (like mockup)
            tableHeaderPill = new RoundedPanel
            {
                BackColor = Color.FromArgb(230, 243, 252),
                CornerRadius = 18,
                Dock = DockStyle.Top,
                Height = 70,
                Padding = new Padding(18, 0, 18, 0)
            };
            tableCard.Controls.Add(tableHeaderPill);

            hdrItem = HeaderCell("Item", ContentAlignment.MiddleLeft);
            hdrQty = HeaderCell("QTY", ContentAlignment.MiddleCenter);
            hdrPrice = HeaderCell("Price", ContentAlignment.MiddleCenter);
            hdrTotal = HeaderCell("Total", ContentAlignment.MiddleCenter);

            tableHeaderPill.Controls.Add(hdrItem);
            tableHeaderPill.Controls.Add(hdrQty);
            tableHeaderPill.Controls.Add(hdrPrice);
            tableHeaderPill.Controls.Add(hdrTotal);

            tableHeaderPill.Resize += (s, e) =>
            {
                int left = 18;
                int qtyW = 90;
                int priceW = 140;
                int totalW = 150;
                int itemW = tableHeaderPill.Width - left * 2 - qtyW - priceW - totalW;

                hdrItem.Bounds = new Rectangle(left, 0, itemW, tableHeaderPill.Height);
                hdrQty.Bounds = new Rectangle(hdrItem.Right, 0, qtyW, tableHeaderPill.Height);
                hdrPrice.Bounds = new Rectangle(hdrQty.Right, 0, priceW, tableHeaderPill.Height);
                hdrTotal.Bounds = new Rectangle(hdrPrice.Right, 0, totalW, tableHeaderPill.Height);
            };

            // DataGridView (no headers, rows only)
            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = tableCard.BackColor,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(220, 235, 246),
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                MultiSelect = false,
                AutoGenerateColumns = false,
                ColumnHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ScrollBars = ScrollBars.Vertical
            };

            dgv.DefaultCellStyle.BackColor = tableCard.BackColor;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(30, 30, 30);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 235, 252);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30);
            dgv.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgv.RowTemplate.Height = 58;

            // Columns (match header widths)
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ItemName",
                DataPropertyName = "ItemName",
                Width = 0, // resized in tableCard.Resize
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                DataPropertyName = "Quantity",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UnitPrice",
                DataPropertyName = "UnitPrice",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LineTotal",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                if (!(dgv.Rows[e.RowIndex].DataBoundItem is ReceiptItem ri)) return;

                string col = dgv.Columns[e.ColumnIndex].Name;
                if (col == "UnitPrice")
                {
                    e.Value = FormatPeso(ri.UnitPrice);
                    e.FormattingApplied = true;
                }
                else if (col == "LineTotal")
                {
                    e.Value = FormatPeso(ri.UnitPrice * ri.Quantity);
                    e.FormattingApplied = true;
                }
            };

            tableCard.Controls.Add(dgv);
            tableCard.Controls.SetChildIndex(dgv, 1); // keep header pill on top

            // Resize columns to match header “grid”
            tableCard.Resize += (s, e) =>
            {
                int left = tableCard.Padding.Left;
                int right = tableCard.Padding.Right;

                int qtyW = 90;
                int priceW = 140;
                int totalW = 150;

                int totalInnerW = tableCard.Width - left - right;
                int itemW = totalInnerW - qtyW - priceW - totalW;

                if (dgv.Columns.Count >= 4)
                {
                    dgv.Columns[0].Width = Math.Max(240, itemW);
                    dgv.Columns[1].Width = qtyW;
                    dgv.Columns[2].Width = priceW;
                    dgv.Columns[3].Width = totalW;
                }
            };

            // Payment Method (below table left)
            lblPaymentTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 25),
                Text = "Payment Method:"
            };
            body.Controls.Add(lblPaymentTitle);

            lblPaymentValue = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 13, FontStyle.Regular),
                ForeColor = Color.FromArgb(25, 25, 25),
                Text = "-"
            };
            body.Controls.Add(lblPaymentValue);

            // TOTALS CARD (right)
            totalsCard = new RoundedPanel
            {
                BackColor = Color.FromArgb(240, 248, 254),
                CornerRadius = 28,
                Size = new Size(320, 260),
                Padding = new Padding(18, 18, 18, 18)
            };
            body.Controls.Add(totalsCard);

            lblSubLabel = TotalsLabel("Subtotal:", false);
            lblTaxLabel = TotalsLabel("Tax:", false);
            lblSubValue = TotalsValueLabel();
            lblTaxValue = TotalsValueLabel();

            totalsCard.Controls.Add(lblSubLabel);
            totalsCard.Controls.Add(lblSubValue);
            totalsCard.Controls.Add(lblTaxLabel);
            totalsCard.Controls.Add(lblTaxValue);

            lblSubLabel.Location = new Point(20, 26);
            lblTaxLabel.Location = new Point(20, 72);

            lblSubValue.Location = new Point(190, 26);
            lblTaxValue.Location = new Point(190, 72);

            totalBar = new Panel
            {
                BackColor = Color.FromArgb(18, 121, 214),
                Height = 90,
                Dock = DockStyle.Bottom
            };
            totalsCard.Controls.Add(totalBar);

            lblTotalLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Text = "Total:"
            };

            lblTotalValue = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleRight
            };

            totalBar.Controls.Add(lblTotalLabel);
            totalBar.Controls.Add(lblTotalValue);

            totalBar.Resize += (s, e) =>
            {
                lblTotalLabel.Location = new Point(22, (totalBar.Height - lblTotalLabel.Height) / 2);
                lblTotalValue.Location = new Point(totalBar.Width - lblTotalValue.Width - 22,
                    (totalBar.Height - lblTotalValue.Height) / 2);
            };

            // separator line above footer text
            footerLine = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = Color.FromArgb(225, 225, 225)
            };
            root.Controls.Add(footerLine);

            // Footer text
            lblFooter = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(40, 40, 40),
                Text = "Thank you for the business!"
            };
            root.Controls.Add(lblFooter);

            // Buttons panel (bottom)
            buttonsPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 90,
                BackColor = Color.White
            };
            root.Controls.Add(buttonsPanel);

            btnNew = new Button
            {
                Text = "New Transaction",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                Size = new Size(260, 52),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnNew.FlatAppearance.BorderColor = Color.FromArgb(225, 225, 225);
            btnNew.FlatAppearance.BorderSize = 2;
            btnNew.Click += (s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };

            btnPrint = new Button
            {
                Text = "Print Invoice",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(260, 52),
                BackColor = Color.FromArgb(10, 117, 205),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Click += (s, e) => ShowPrintPreview();

            buttonsPanel.Controls.Add(btnNew);
            buttonsPanel.Controls.Add(btnPrint);

            buttonsPanel.Resize += (s, e) =>
            {
                int gap = 22;
                int totalW = btnNew.Width + btnPrint.Width + gap;
                int startX = (buttonsPanel.Width - totalW) / 2;
                int y = (buttonsPanel.Height - btnNew.Height) / 2;

                btnNew.Location = new Point(startX, y);
                btnPrint.Location = new Point(startX + btnNew.Width + gap, y);
            };

            // Body positioning to match mockup
            body.Resize += (s, e) =>
            {
                // table stays left
                tableCard.Location = new Point(0, 10);
                tableCard.Size = new Size(Math.Min(640, body.Width - 360), 420);

                // totals card aligned right and vertically centered with table
                totalsCard.Location = new Point(body.Width - totalsCard.Width, 130);

                // payment below table
                lblPaymentTitle.Location = new Point(0, tableCard.Bottom + 26);
                lblPaymentValue.Location = new Point(0, lblPaymentTitle.Bottom + 8);
            };
        }

        private void BindGrid()
        {
            dgv.DataSource = _items;

            // Totals
            lblSubValue.Text = FormatPeso(_subtotal);
            lblTaxValue.Text = FormatPeso(_tax);
            lblTotalValue.Text = "₱ " + _total.ToString("N2");

            // force totalBar layout
            totalBar.PerformLayout();
        }

        private void ApplyTitles()
        {
            if (_data != null)
            {
                if (!string.IsNullOrWhiteSpace(_data.DocumentTitle))
                    lblDocTitle.Text = _data.DocumentTitle;
            }
        }

        private void ApplyPayment()
        {
            if (_data != null && !string.IsNullOrWhiteSpace(_data.PaymentMethod))
                lblPaymentValue.Text = _data.PaymentMethod;
            else
                lblPaymentValue.Text = "-";
        }

        private void ApplyMeta()
        {
            string txnId = _data != null && !string.IsNullOrWhiteSpace(_data.TransactionId)
                ? _data.TransactionId
                : "TRX-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            DateTime dt = (_data != null && _data.TransactionDate != DateTime.MinValue)
                ? _data.TransactionDate
                : DateTime.Now;

            string type = _data != null && !string.IsNullOrWhiteSpace(_data.TransactionType)
                ? _data.TransactionType
                : "-";

            lblTxnId.Text = "Transaction ID:  " + txnId;
            lblDate.Text = "Date:  " + dt.ToString("MM/dd/yyyy HH:mm");
            lblType.Text = "Type:  " + type;
        }

        // ================= Printing (basic) =================
        private void WirePrinting()
        {
            _printDoc = new PrintDocument();
            _printDoc.PrintPage += PrintDoc_PrintPage;

            _preview = new PrintPreviewDialog
            {
                Document = _printDoc,
                Width = 1000,
                Height = 800
            };
        }

        private void ShowPrintPreview()
        {
            try
            {
                _preview.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print preview failed: " + ex.Message, "Print",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Simple printable layout
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int x = 60;
            int y = 60;

            using (var titleFont = new Font("Segoe UI", 16, FontStyle.Bold))
            using (var normal = new Font("Segoe UI", 10))
            using (var bold = new Font("Segoe UI", 10, FontStyle.Bold))
            using (var pen = new Pen(Color.LightGray))
            {
                g.DrawString("Topaz Hardware", titleFont, Brushes.Black, x, y);
                y += 26;
                g.DrawString("Hardware & Construction Supplies", normal, Brushes.Black, x, y);
                y += 28;
                g.DrawString(lblDocTitle.Text, normal, Brushes.Black, x, y);
                y += 30;

                g.DrawString(lblTxnId.Text, normal, Brushes.Black, x, y); y += 18;
                g.DrawString(lblDate.Text, normal, Brushes.Black, x, y); y += 18;
                g.DrawString(lblType.Text, normal, Brushes.Black, x, y); y += 24;

                g.DrawLine(pen, x, y, x + 500, y);
                y += 12;

                g.DrawString("Item", bold, Brushes.Black, x, y);
                g.DrawString("QTY", bold, Brushes.Black, x + 320, y);
                g.DrawString("Price", bold, Brushes.Black, x + 380, y);
                g.DrawString("Total", bold, Brushes.Black, x + 470, y);
                y += 18;

                g.DrawLine(pen, x, y, x + 500, y);
                y += 10;

                foreach (var it in _items)
                {
                    g.DrawString(it.ItemName, normal, Brushes.Black, x, y);
                    g.DrawString(it.Quantity.ToString(), normal, Brushes.Black, x + 320, y);
                    g.DrawString(FormatPeso(it.UnitPrice), normal, Brushes.Black, x + 380, y);
                    g.DrawString(FormatPeso(it.UnitPrice * it.Quantity), normal, Brushes.Black, x + 470, y);
                    y += 18;
                }

                y += 18;
                g.DrawLine(pen, x, y, x + 500, y);
                y += 16;

                g.DrawString("Subtotal:", bold, Brushes.Black, x + 360, y);
                g.DrawString(FormatPeso(_subtotal), normal, Brushes.Black, x + 470, y);
                y += 18;

                g.DrawString("Tax:", bold, Brushes.Black, x + 360, y);
                g.DrawString(FormatPeso(_tax), normal, Brushes.Black, x + 470, y);
                y += 18;

                g.DrawString("Total:", bold, Brushes.Black, x + 360, y);
                g.DrawString(FormatPeso(_total), bold, Brushes.Black, x + 470, y);
                y += 26;

                g.DrawString("Payment Method: " + lblPaymentValue.Text, bold, Brushes.Black, x, y);
                y += 26;

                g.DrawString("Thank you for the business!", normal, Brushes.Black, x, y);
            }
        }

        // ================= Helpers =================
        private static Label MetaLine(string left, string right)
        {
            return new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 25),
                Text = $"{left}  {right}"
            };
        }

        private static Label HeaderCell(string text, ContentAlignment align)
        {
            return new Label
            {
                Text = text,
                TextAlign = align,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 25)
            };
        }

        private static Label TotalsLabel(string text, bool bold)
        {
            return new Label
            {
                AutoSize = true,
                Text = text,
                Font = new Font("Segoe UI", 12, bold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = Color.FromArgb(25, 25, 25)
            };
        }

        private static Label TotalsValueLabel()
        {
            return new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(25, 25, 25),
                TextAlign = ContentAlignment.MiddleRight
            };
        }

        private static string FormatPeso(decimal value) => "₱" + value.ToString("N2");

        // ============================================================
        // RoundedPanel (clips children, no black corner artifacts)
        // ============================================================
        private class RoundedPanel : Panel
        {
            public int CornerRadius { get; set; } = 20;

            public RoundedPanel()
            {
                DoubleBuffered = true;
                Resize += (s, e) => ApplyRegion();
            }

            protected override void OnCreateControl()
            {
                base.OnCreateControl();
                ApplyRegion();
            }

            private void ApplyRegion()
            {
                if (Width <= 0 || Height <= 0) return;

                var rect = new Rectangle(0, 0, Width, Height);
                using (var path = GetRoundedRectPath(rect, CornerRadius))
                {
                    Region?.Dispose();
                    Region = new Region(path); // ✅ clips children
                }
                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                using (var path = GetRoundedRectPath(rect, CornerRadius))
                using (var brush = new SolidBrush(BackColor))
                using (var pen = new Pen(Color.FromArgb(230, 235, 240)))
                {
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(pen, path);
                }

                base.OnPaint(e);
            }

            private static GraphicsPath GetRoundedRectPath(Rectangle r, int radius)
            {
                int d = radius * 2;
                var path = new GraphicsPath();
                path.AddArc(r.X, r.Y, d, d, 180, 90);
                path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                path.CloseFigure();
                return path;
            }
        }
    }
}
