using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    partial class ReceiptPreviewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel panelMain;
        private PictureBox picLogo;
        private Label lblCompanyName;
        private Label lblTagline;
        private Label lblReceiptTitle;

        private Label lblTxnIdLabel;
        private Label lblTxnIdValue;
        private Label lblDateLabel;
        private Label lblDateValue;
        private Label lblTypeLabel;
        private Label lblTypeValue;

        private Guna2Panel pnlItemsCard;
        private Label lblItemHeader;
        private Label lblQtyHeader;
        private Label lblPriceHeader;
        private Label lblItemTotalHeader;
        private Panel itemsContainer;

        private Guna2Panel pnlTotalsCard;
        private Label lblSubtotalText;
        private Label lblSubtotalValue;
        private Label lblTaxText;
        private Label lblTaxValue;
        private Guna2Panel pnlTotalBlue;
        private Label lblTotalText;
        private Label lblTotalValue;

        private Label lblPaymentMethodLabel;
        private Label lblPaymentMethodValue;

        private Label lblFooterText;
        private Guna2Button btnNewTransaction;
        private Guna2Button btnPrintInvoice;

        /// <summary>
        /// Clean up any resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ReceiptPreviewForm
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(551, 400);
            this.Name = "ReceiptPreviewForm";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
