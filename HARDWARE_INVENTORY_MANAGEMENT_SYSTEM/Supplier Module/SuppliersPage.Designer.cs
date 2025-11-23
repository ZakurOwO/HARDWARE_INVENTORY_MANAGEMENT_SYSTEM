namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    partial class SuppliersPage
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
            this.btnMainButtonIcon = new Guna.UI2.WinForms.Guna2Button();
            this.inventoryFilter_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryFilter_Button();
            this.supplierTable1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.SupplierTable();
            this.searchFieldForSupplier1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.SearchFieldForSupplier();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.btnMainButtonIcon);
            this.guna2Panel1.Controls.Add(this.inventoryFilter_Button1);
            this.guna2Panel1.Controls.Add(this.supplierTable1);
            this.guna2Panel1.Controls.Add(this.searchFieldForSupplier1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1236, 720);
            this.guna2Panel1.TabIndex = 7;
            // 
            // btnMainButtonIcon
            // 
            this.btnMainButtonIcon.BorderRadius = 6;
            this.btnMainButtonIcon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMainButtonIcon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMainButtonIcon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMainButtonIcon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMainButtonIcon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.btnMainButtonIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainButtonIcon.ForeColor = System.Drawing.Color.White;
            this.btnMainButtonIcon.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Add_User;
            this.btnMainButtonIcon.Location = new System.Drawing.Point(1012, 27);
            this.btnMainButtonIcon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMainButtonIcon.Name = "btnMainButtonIcon";
            this.btnMainButtonIcon.Size = new System.Drawing.Size(201, 49);
            this.btnMainButtonIcon.TabIndex = 7;
            this.btnMainButtonIcon.Text = "Add Supplier";
            this.btnMainButtonIcon.Click += new System.EventHandler(this.btnMainButtonIcon_Click);
            // 
            // inventoryFilter_Button1
            // 
            this.inventoryFilter_Button1.BackColor = System.Drawing.Color.Transparent;
            this.inventoryFilter_Button1.Location = new System.Drawing.Point(420, 27);
            this.inventoryFilter_Button1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.inventoryFilter_Button1.Name = "inventoryFilter_Button1";
            this.inventoryFilter_Button1.Size = new System.Drawing.Size(136, 53);
            this.inventoryFilter_Button1.TabIndex = 6;
            // 
            // supplierTable1
            // 
            this.supplierTable1.BackColor = System.Drawing.Color.White;
            this.supplierTable1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.supplierTable1.Location = new System.Drawing.Point(23, 108);
            this.supplierTable1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.supplierTable1.Name = "supplierTable1";
            this.supplierTable1.Size = new System.Drawing.Size(1191, 565);
            this.supplierTable1.TabIndex = 5;
            // 
            // searchFieldForSupplier1
            // 
            this.searchFieldForSupplier1.Location = new System.Drawing.Point(23, 28);
            this.searchFieldForSupplier1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.searchFieldForSupplier1.Name = "searchFieldForSupplier1";
            this.searchFieldForSupplier1.Size = new System.Drawing.Size(388, 49);
            this.searchFieldForSupplier1.TabIndex = 3;
            // 
            // SuppliersPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.guna2Panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SuppliersPage";
            this.Size = new System.Drawing.Size(1236, 720);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Inventory_Module.InventoryFilter_Button inventoryFilter_Button1;
        private SupplierTable supplierTable1;
        private SearchFieldForSupplier searchFieldForSupplier1;
        private Guna.UI2.WinForms.Guna2Button btnMainButtonIcon;
    }
}
