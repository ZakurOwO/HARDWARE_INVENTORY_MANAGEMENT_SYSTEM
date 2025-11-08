namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    partial class ReportsNavigationBar
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
            this.pnlNavBar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDeliveries = new Guna.UI2.WinForms.Guna2Button();
            this.btnSuppliers = new Guna.UI2.WinForms.Guna2Button();
            this.btnCustomers = new Guna.UI2.WinForms.Guna2Button();
            this.btnSales = new Guna.UI2.WinForms.Guna2Button();
            this.btnInventory = new Guna.UI2.WinForms.Guna2Button();
            this.pnlNavBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNavBar
            // 
            this.pnlNavBar.BorderColor = System.Drawing.Color.LightGray;
            this.pnlNavBar.BorderRadius = 7;
            this.pnlNavBar.BorderThickness = 1;
            this.pnlNavBar.Controls.Add(this.btnDeliveries);
            this.pnlNavBar.Controls.Add(this.btnSuppliers);
            this.pnlNavBar.Controls.Add(this.btnCustomers);
            this.pnlNavBar.Controls.Add(this.btnSales);
            this.pnlNavBar.Controls.Add(this.btnInventory);
            this.pnlNavBar.Location = new System.Drawing.Point(4, 4);
            this.pnlNavBar.Name = "pnlNavBar";
            this.pnlNavBar.Size = new System.Drawing.Size(700, 38);
            this.pnlNavBar.TabIndex = 0;
            // 
            // btnDeliveries
            // 
            this.btnDeliveries.BorderRadius = 5;
            this.btnDeliveries.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDeliveries.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDeliveries.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDeliveries.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDeliveries.FillColor = System.Drawing.Color.White;
            this.btnDeliveries.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeliveries.ForeColor = System.Drawing.Color.Black;
            this.btnDeliveries.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.fast_delivery2;
            this.btnDeliveries.ImageOffset = new System.Drawing.Point(-1, 0);
            this.btnDeliveries.Location = new System.Drawing.Point(558, 5);
            this.btnDeliveries.Name = "btnDeliveries";
            this.btnDeliveries.Size = new System.Drawing.Size(130, 30);
            this.btnDeliveries.TabIndex = 17;
            this.btnDeliveries.Text = "Deliveries";
            this.btnDeliveries.Click += new System.EventHandler(this.btnDeliveries_Click);
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
            this.btnSuppliers.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.open_box_add;
            this.btnSuppliers.ImageOffset = new System.Drawing.Point(-1, 0);
            this.btnSuppliers.Location = new System.Drawing.Point(422, 5);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(130, 30);
            this.btnSuppliers.TabIndex = 16;
            this.btnSuppliers.Text = "Suppliers";
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // btnCustomers
            // 
            this.btnCustomers.BorderRadius = 5;
            this.btnCustomers.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCustomers.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCustomers.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCustomers.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCustomers.FillColor = System.Drawing.Color.White;
            this.btnCustomers.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomers.ForeColor = System.Drawing.Color.Black;
            this.btnCustomers.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.users_021;
            this.btnCustomers.ImageOffset = new System.Drawing.Point(-1, 0);
            this.btnCustomers.Location = new System.Drawing.Point(286, 5);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(130, 30);
            this.btnCustomers.TabIndex = 15;
            this.btnCustomers.Text = "Customers";
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            // 
            // btnSales
            // 
            this.btnSales.BorderRadius = 5;
            this.btnSales.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSales.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSales.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSales.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSales.FillColor = System.Drawing.Color.White;
            this.btnSales.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSales.ForeColor = System.Drawing.Color.Black;
            this.btnSales.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.bill;
            this.btnSales.ImageOffset = new System.Drawing.Point(-1, 0);
            this.btnSales.Location = new System.Drawing.Point(150, 5);
            this.btnSales.Name = "btnSales";
            this.btnSales.Size = new System.Drawing.Size(130, 30);
            this.btnSales.TabIndex = 14;
            this.btnSales.Text = "Sales";
            this.btnSales.Click += new System.EventHandler(this.btnSales_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.BorderRadius = 5;
            this.btnInventory.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnInventory.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnInventory.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnInventory.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnInventory.FillColor = System.Drawing.Color.White;
            this.btnInventory.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventory.ForeColor = System.Drawing.Color.Black;
            this.btnInventory.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.shopping_bag;
            this.btnInventory.ImageOffset = new System.Drawing.Point(-1, 0);
            this.btnInventory.Location = new System.Drawing.Point(14, 4);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(130, 30);
            this.btnInventory.TabIndex = 13;
            this.btnInventory.Text = "Inventory";
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // ReportsNavigationBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlNavBar);
            this.Name = "ReportsNavigationBar";
            this.Size = new System.Drawing.Size(708, 46);
            this.Load += new System.EventHandler(this.ReportsNavigationBar_Load);
            this.pnlNavBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlNavBar;
        private Guna.UI2.WinForms.Guna2Button btnDeliveries;
        private Guna.UI2.WinForms.Guna2Button btnSuppliers;
        private Guna.UI2.WinForms.Guna2Button btnCustomers;
        private Guna.UI2.WinForms.Guna2Button btnSales;
        private Guna.UI2.WinForms.Guna2Button btnInventory;
    }
}
