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
            this.pnlContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.supplierNavBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.SupplierNavBar();
            this.supplierTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.SupplierTopBar();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlContainer.BorderColor = System.Drawing.Color.Gainsboro;
            this.pnlContainer.BorderRadius = 15;
            this.pnlContainer.BorderThickness = 1;
            this.pnlContainer.FillColor = System.Drawing.Color.White;
            this.pnlContainer.Location = new System.Drawing.Point(18, 112);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(927, 585);
            this.pnlContainer.TabIndex = 6;
            // 
            // supplierNavBar1
            // 
            this.supplierNavBar1.BackColor = System.Drawing.Color.Transparent;
            this.supplierNavBar1.Location = new System.Drawing.Point(309, 60);
            this.supplierNavBar1.Name = "supplierNavBar1";
            this.supplierNavBar1.Size = new System.Drawing.Size(363, 46);
            this.supplierNavBar1.TabIndex = 7;
            // 
            // supplierTopBar1
            // 
            this.supplierTopBar1.BackColor = System.Drawing.Color.White;
            this.supplierTopBar1.Location = new System.Drawing.Point(0, 0);
            this.supplierTopBar1.Name = "supplierTopBar1";
            this.supplierTopBar1.Size = new System.Drawing.Size(965, 69);
            this.supplierTopBar1.TabIndex = 0;
            // 
            // SuppplierMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.supplierNavBar1);
            this.Controls.Add(this.supplierTopBar1);
            this.Controls.Add(this.pnlContainer);
            this.Name = "SuppplierMainPage";
            this.Size = new System.Drawing.Size(965, 712);
            this.Load += new System.EventHandler(this.SuppplierMainPage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private SupplierTopBar supplierTopBar1;
        private Guna.UI2.WinForms.Guna2Panel pnlContainer;
        private SupplierNavBar supplierNavBar1;
    }
}
