namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    partial class SupplierNavBar
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
            this.btnSuppliers = new Guna.UI2.WinForms.Guna2Button();
            this.btnPurchaseOrders = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.LightGray;
            this.guna2Panel1.BorderRadius = 10;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.btnSuppliers);
            this.guna2Panel1.Controls.Add(this.btnPurchaseOrders);
            this.guna2Panel1.Location = new System.Drawing.Point(3, 3);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(356, 38);
            this.guna2Panel1.TabIndex = 3;
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.BorderRadius = 5;
            this.btnSuppliers.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSuppliers.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSuppliers.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSuppliers.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSuppliers.FillColor = System.Drawing.Color.White;
            this.btnSuppliers.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuppliers.ForeColor = System.Drawing.Color.Black;
            this.btnSuppliers.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.users_022;
            this.btnSuppliers.ImageOffset = new System.Drawing.Point(-1, 0);
            this.btnSuppliers.Location = new System.Drawing.Point(8, 4);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(165, 30);
            this.btnSuppliers.TabIndex = 18;
            this.btnSuppliers.Text = "Suppliers";
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // btnPurchaseOrders
            // 
            this.btnPurchaseOrders.BorderRadius = 5;
            this.btnPurchaseOrders.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPurchaseOrders.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPurchaseOrders.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPurchaseOrders.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPurchaseOrders.FillColor = System.Drawing.Color.White;
            this.btnPurchaseOrders.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPurchaseOrders.ForeColor = System.Drawing.Color.Black;
            this.btnPurchaseOrders.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.open_box_add;
            this.btnPurchaseOrders.Location = new System.Drawing.Point(183, 4);
            this.btnPurchaseOrders.Name = "btnPurchaseOrders";
            this.btnPurchaseOrders.Size = new System.Drawing.Size(165, 30);
            this.btnPurchaseOrders.TabIndex = 17;
            this.btnPurchaseOrders.Text = "Purchase Orders";
            this.btnPurchaseOrders.TextOffset = new System.Drawing.Point(1, 0);
            this.btnPurchaseOrders.Click += new System.EventHandler(this.btnPurchaseOrders_Click);
            // 
            // SupplierNavBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.guna2Panel1);
            this.Name = "SupplierNavBar";
            this.Size = new System.Drawing.Size(362, 46);
            this.Load += new System.EventHandler(this.SupplierNavBar_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnSuppliers;
        private Guna.UI2.WinForms.Guna2Button btnPurchaseOrders;
    }
}
