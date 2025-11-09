using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public partial class SidePanel : UserControl
    {
        private Button activeButton = null;

        public SidePanel()
        {
            InitializeComponent();
            this.Load += SidePanel_Load;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int radius = 20;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(Width - radius, Height - radius, radius, radius, 0, 90);
                path.AddArc(0, Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                this.Region = new Region(path);
            }
        }

        private void SidePanel_Load(object sender, EventArgs e)
        {

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Paint += SidebarButton_Paint;
                }
            }


            if (this.Controls.ContainsKey("DashboardBTN"))
            {
                HighlightButton((Button)this.Controls["DashboardBTN"]);
            }
        }

        private void HighlightButton(Button clickedButton)
        {
            Color defaultBack = Color.FromArgb(209, 228, 242);
            Color activeBack = Color.FromArgb(184, 213, 229);
            Color activeText = Color.FromArgb(0, 87, 158);


            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = defaultBack;
                    btn.ForeColor = Color.Black;
                    btn.Font = new Font("Lexend", 9, FontStyle.Regular);
                    btn.Tag = "inactive";
                    btn.Invalidate();
                }
            }


            clickedButton.BackColor = activeBack;
            clickedButton.ForeColor = activeText;
            clickedButton.Font = new Font("Lexend SemiBold", 9, FontStyle.Regular);
            clickedButton.Tag = "active";
            activeButton = clickedButton;


            clickedButton.Invalidate();
            this.Invalidate();
        }

        private void SidebarButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Tag != null && btn.Tag.ToString() == "active")
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 87, 158)))
                {

                    Rectangle lineRect = new Rectangle(0, 4, 4, btn.Height - 8);
                    e.Graphics.FillRectangle(brush, lineRect);
                }
            }
        }

        private void DashboardBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);

        }
        private void AccountBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);

            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                AccountsMainPage accountPage = new AccountsMainPage();
                mainForm.MainContentPanelAccess.Controls.Add(accountPage);
            }
        }
        private void InventoryBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                InventoryMainPage inventory = new InventoryMainPage();
                mainForm.MainContentPanelAccess.Controls.Add(inventory);
            }


        }
        private void TransactionBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                TransactionsMainPage transation = new TransactionsMainPage();
                mainForm.MainContentPanelAccess.Controls.Add(transation);
            }
        }


        private void CustomerBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                CustomerMainPage customerForm = new CustomerMainPage();
                mainForm.MainContentPanelAccess.Controls.Add(customerForm);
            }
        }
        private void SupplierBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                SuppplierMainPage supplierForm = new SuppplierMainPage();
                mainForm.MainContentPanelAccess.Controls.Add(supplierForm);
            }
        }
        private void DeliveriesBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                DeliveriesMainPage vehicleForm = new DeliveriesMainPage();
                mainForm.MainContentPanelAccess.Controls.Add(vehicleForm);
            }

        }
        private void ReportBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                ReportsMainPage reports = new ReportsMainPage();
                mainForm.MainContentPanelAccess.Controls.Add(reports);
            }
        }
        private void HistoryBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                mainForm.MainContentPanelAccess.Controls.Clear();
                HistoryMainPage history = new HistoryMainPage();
                mainForm.MainContentPanelAccess.Controls.Add(history);
            }
        }
        private void AuditlogBTN_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            {
                var mainForm = this.FindForm() as MainDashBoard;
                if (mainForm != null)
                {
                    mainForm.MainContentPanelAccess.Controls.Clear();
                    Audit_Log.AuditLogMainPage auditlog = new Audit_Log.AuditLogMainPage();
                    mainForm.MainContentPanelAccess.Controls.Add(auditlog);
                }
            }
        }
    }
}
