namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    partial class TransactionsMainPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.pagination1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module.Pagination();
            this.products1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module.Products();
            this.transactionsFilterButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module.TransactionsFilterButton();
            this.transactionsSearchBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module.TransactionsSearchBar();
            this.transactionsTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module.TransactionsTopBar();
            this.cartDetails1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module.CartDetails();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2Panel1.BorderRadius = 20;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.pagination1);
            this.guna2Panel1.Controls.Add(this.products1);
            this.guna2Panel1.Controls.Add(this.transactionsFilterButton1);
            this.guna2Panel1.Controls.Add(this.transactionsSearchBar1);
            this.guna2Panel1.Location = new System.Drawing.Point(8, 64);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(630, 624);
            this.guna2Panel1.TabIndex = 1;
            // 
            // pagination1
            // 
            this.pagination1.Location = new System.Drawing.Point(7, 554);
            this.pagination1.Name = "pagination1";
            this.pagination1.Size = new System.Drawing.Size(615, 58);
            this.pagination1.TabIndex = 3;
            // 
            // products1
            // 
            this.products1.BackColor = System.Drawing.Color.White;
            this.products1.Location = new System.Drawing.Point(13, 84);
            this.products1.Name = "products1";
            this.products1.Size = new System.Drawing.Size(601, 445);
            this.products1.TabIndex = 2;
            // 
            // transactionsFilterButton1
            // 
            this.transactionsFilterButton1.BackColor = System.Drawing.Color.Transparent;
            this.transactionsFilterButton1.Location = new System.Drawing.Point(315, 20);
            this.transactionsFilterButton1.Name = "transactionsFilterButton1";
            this.transactionsFilterButton1.Size = new System.Drawing.Size(113, 47);
            this.transactionsFilterButton1.TabIndex = 1;
            // 
            // transactionsSearchBar1
            // 
            this.transactionsSearchBar1.BackColor = System.Drawing.Color.Transparent;
            this.transactionsSearchBar1.Location = new System.Drawing.Point(13, 20);
            this.transactionsSearchBar1.Name = "transactionsSearchBar1";
            this.transactionsSearchBar1.Size = new System.Drawing.Size(299, 48);
            this.transactionsSearchBar1.TabIndex = 0;
            // 
            // transactionsTopBar1
            // 
            this.transactionsTopBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.transactionsTopBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.transactionsTopBar1.Location = new System.Drawing.Point(0, 0);
            this.transactionsTopBar1.Name = "transactionsTopBar1";
            this.transactionsTopBar1.Size = new System.Drawing.Size(960, 60);
            this.transactionsTopBar1.TabIndex = 0;
            // 
            // cartDetails1
            // 
            this.cartDetails1.BackColor = System.Drawing.Color.Transparent;
            this.cartDetails1.Location = new System.Drawing.Point(652, 64);
            this.cartDetails1.Name = "cartDetails1";
            this.cartDetails1.Size = new System.Drawing.Size(295, 624);
            this.cartDetails1.TabIndex = 2;
            // 
            // TransactionsMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.cartDetails1);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.transactionsTopBar1);
            this.Name = "TransactionsMainPage";
            this.Size = new System.Drawing.Size(960, 720);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TransactionsTopBar transactionsTopBar1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private TransactionsFilterButton transactionsFilterButton1;
        private TransactionsSearchBar transactionsSearchBar1;
        private Products products1;
        private Pagination pagination1;
        private CartDetails cartDetails1;
    }
}
