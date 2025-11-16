namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    partial class SuppplierMainPage
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
            this.supplierTable1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.SupplierTable();
            this.supplierButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.SupplierButton();
            this.searchFieldForSupplier1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.SearchFieldForSupplier();
            this.supplierTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.SupplierTopBar();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.inventoryFilter_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryFilter_Button();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // supplierTable1
            // 
            this.supplierTable1.BackColor = System.Drawing.Color.White;
            this.supplierTable1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.supplierTable1.Location = new System.Drawing.Point(17, 88);
            this.supplierTable1.Name = "supplierTable1";
            this.supplierTable1.Size = new System.Drawing.Size(893, 500);
            this.supplierTable1.TabIndex = 5;
            // 
            // supplierButton1
            // 
            this.supplierButton1.Location = new System.Drawing.Point(747, 23);
            this.supplierButton1.Name = "supplierButton1";
            this.supplierButton1.Size = new System.Drawing.Size(150, 44);
            this.supplierButton1.TabIndex = 4;
            // 
            // searchFieldForSupplier1
            // 
            this.searchFieldForSupplier1.Location = new System.Drawing.Point(17, 23);
            this.searchFieldForSupplier1.Name = "searchFieldForSupplier1";
            this.searchFieldForSupplier1.Size = new System.Drawing.Size(291, 40);
            this.searchFieldForSupplier1.TabIndex = 3;
            // 
            // supplierTopBar1
            // 
            this.supplierTopBar1.BackColor = System.Drawing.Color.White;
            this.supplierTopBar1.Location = new System.Drawing.Point(0, 0);
            this.supplierTopBar1.Name = "supplierTopBar1";
            this.supplierTopBar1.Size = new System.Drawing.Size(965, 69);
            this.supplierTopBar1.TabIndex = 0;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.inventoryFilter_Button1);
            this.guna2Panel1.Controls.Add(this.supplierButton1);
            this.guna2Panel1.Controls.Add(this.supplierTable1);
            this.guna2Panel1.Controls.Add(this.searchFieldForSupplier1);
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(18, 75);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(927, 620);
            this.guna2Panel1.TabIndex = 6;
            // 
            // inventoryFilter_Button1
            // 
            this.inventoryFilter_Button1.BackColor = System.Drawing.Color.Transparent;
            this.inventoryFilter_Button1.Location = new System.Drawing.Point(315, 22);
            this.inventoryFilter_Button1.Name = "inventoryFilter_Button1";
            this.inventoryFilter_Button1.Size = new System.Drawing.Size(102, 43);
            this.inventoryFilter_Button1.TabIndex = 6;
            // 
            // SuppplierMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.supplierTopBar1);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "SuppplierMainPage";
            this.Size = new System.Drawing.Size(965, 712);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SupplierTopBar supplierTopBar1;
        private SearchFieldForSupplier searchFieldForSupplier1;
        private SupplierButton supplierButton1;
        private SupplierTable supplierTable1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Inventory_Module.InventoryFilter_Button inventoryFilter_Button1;
    }
}
