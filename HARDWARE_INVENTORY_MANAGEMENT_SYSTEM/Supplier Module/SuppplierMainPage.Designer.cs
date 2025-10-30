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
            this.supplierTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.SupplierTopBar();
            this.supplierTable1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.SupplierTable();
            this.tableSupplier1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.TableSupplier();
            this.searchFieldForSupplier1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.SearchFieldForSupplier();
            this.supplierButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.SupplierButton();
            this.SuspendLayout();
            // 
            // supplierTopBar1
            // 
            this.supplierTopBar1.BackColor = System.Drawing.Color.White;
            this.supplierTopBar1.Location = new System.Drawing.Point(-3, 0);
            this.supplierTopBar1.Name = "supplierTopBar1";
            this.supplierTopBar1.Size = new System.Drawing.Size(965, 69);
            this.supplierTopBar1.TabIndex = 0;
            // 
            // supplierTable1
            // 
            this.supplierTable1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.supplierTable1.Location = new System.Drawing.Point(77, 181);
            this.supplierTable1.Name = "supplierTable1";
            this.supplierTable1.Size = new System.Drawing.Size(820, 395);
            this.supplierTable1.TabIndex = 1;
            // 
            // tableSupplier1
            // 
            this.tableSupplier1.Location = new System.Drawing.Point(-3, 0);
            this.tableSupplier1.Name = "tableSupplier1";
            this.tableSupplier1.Size = new System.Drawing.Size(965, 680);
            this.tableSupplier1.TabIndex = 2;
            // 
            // searchFieldForSupplier1
            // 
            this.searchFieldForSupplier1.Location = new System.Drawing.Point(26, 98);
            this.searchFieldForSupplier1.Name = "searchFieldForSupplier1";
            this.searchFieldForSupplier1.Size = new System.Drawing.Size(291, 40);
            this.searchFieldForSupplier1.TabIndex = 3;
            // 
            // supplierButton1
            // 
            this.supplierButton1.Location = new System.Drawing.Point(747, 98);
            this.supplierButton1.Name = "supplierButton1";
            this.supplierButton1.Size = new System.Drawing.Size(150, 44);
            this.supplierButton1.TabIndex = 4;
            // 
            // SuppplierMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.supplierButton1);
            this.Controls.Add(this.searchFieldForSupplier1);
            this.Controls.Add(this.supplierTable1);
            this.Controls.Add(this.supplierTopBar1);
            this.Controls.Add(this.tableSupplier1);
            this.Name = "SuppplierMainPage";
            this.Size = new System.Drawing.Size(965, 680);
            this.ResumeLayout(false);

        }

        #endregion

        private SupplierTopBar supplierTopBar1;
        private SupplierTable supplierTable1;
        private TableSupplier tableSupplier1;
        private SearchFieldForSupplier searchFieldForSupplier1;
        private SupplierButton supplierButton1;
    }
}
