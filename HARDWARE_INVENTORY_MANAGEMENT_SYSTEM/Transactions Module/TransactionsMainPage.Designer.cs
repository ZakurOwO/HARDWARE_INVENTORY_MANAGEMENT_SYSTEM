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
            this.panel1 = new System.Windows.Forms.Panel();
            this.transactionsSearchBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module.TransactionsSearchBar();
            this.transactionsTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module.TransactionsTopBar();
            this.MainpageProductLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.btnMainButtonIcon = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(652, 148);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 624);
            this.panel1.TabIndex = 34;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
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
            // MainpageProductLayout
            // 
            this.MainpageProductLayout.Location = new System.Drawing.Point(13, 148);
            this.MainpageProductLayout.Name = "MainpageProductLayout";
            this.MainpageProductLayout.Size = new System.Drawing.Size(633, 435);
            this.MainpageProductLayout.TabIndex = 33;
            this.MainpageProductLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.MainpageProductLayout_Paint);
            // 
            // btnMainButtonIcon
            // 
            this.btnMainButtonIcon.BorderRadius = 6;
            this.btnMainButtonIcon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMainButtonIcon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMainButtonIcon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMainButtonIcon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMainButtonIcon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.btnMainButtonIcon.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainButtonIcon.ForeColor = System.Drawing.Color.White;
            this.btnMainButtonIcon.Location = new System.Drawing.Point(333, 73);
            this.btnMainButtonIcon.Name = "btnMainButtonIcon";
            this.btnMainButtonIcon.Size = new System.Drawing.Size(99, 40);
            this.btnMainButtonIcon.TabIndex = 4;
            this.btnMainButtonIcon.Text = "Refresh";
            this.btnMainButtonIcon.Click += new System.EventHandler(this.btnMainButtonIcon_Click);
            // 
            // TransactionsMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnMainButtonIcon);
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel MainpageProductLayout;
        private Guna.UI2.WinForms.Guna2Button btnMainButtonIcon;
    }
}