namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    partial class TransactionsMainPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.MainpageProductLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.transactionsSearchBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module.TransactionsSearchBar();
            this.transactionsTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module.TransactionsTopBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // MainpageProductLayout
            // 
            this.MainpageProductLayout.Location = new System.Drawing.Point(13, 148);
            this.MainpageProductLayout.Name = "MainpageProductLayout";
            this.MainpageProductLayout.Size = new System.Drawing.Size(633, 435);
            this.MainpageProductLayout.TabIndex = 33;
            this.MainpageProductLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.MainpageProductLayout_Paint);
            // 
            // transactionsSearchBar1
            // 
            this.transactionsSearchBar1.BackColor = System.Drawing.Color.Transparent;
            this.transactionsSearchBar1.Location = new System.Drawing.Point(28, 69);
            this.transactionsSearchBar1.Name = "transactionsSearchBar1";
            this.transactionsSearchBar1.Size = new System.Drawing.Size(299, 48);
            this.transactionsSearchBar1.TabIndex = 18;
            // 
            // transactionsTopBar1
            // 
            this.transactionsTopBar1.BackColor = System.Drawing.Color.White;
            this.transactionsTopBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.transactionsTopBar1.Location = new System.Drawing.Point(0, 0);
            this.transactionsTopBar1.Name = "transactionsTopBar1";
            this.transactionsTopBar1.Size = new System.Drawing.Size(960, 60);
            this.transactionsTopBar1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(652, 148);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 624);
            this.panel1.TabIndex = 34;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // TransactionsMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.transactionsSearchBar1);
            this.Controls.Add(this.transactionsTopBar1);
            this.Controls.Add(this.MainpageProductLayout);
            this.Name = "TransactionsMainPage";
            this.Size = new System.Drawing.Size(960, 720);
            this.ResumeLayout(false);
        }

        #endregion
        private TransactionsTopBar transactionsTopBar1;
        private TransactionsSearchBar transactionsSearchBar1;
        private System.Windows.Forms.FlowLayoutPanel MainpageProductLayout;
        private System.Windows.Forms.Panel panel1;
    }
}