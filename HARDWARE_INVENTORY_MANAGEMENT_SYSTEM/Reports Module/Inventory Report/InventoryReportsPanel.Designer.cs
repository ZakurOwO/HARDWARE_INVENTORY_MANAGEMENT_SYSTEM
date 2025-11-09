namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    partial class InventoryReportsPanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles.MainButton();
            this.reportsPagination1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsPagination();
            this.inventoryFilter_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryFilter_Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lexend SemiBold", 11.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Inventory Reports";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(18, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(905, 470);
            this.panel1.TabIndex = 7;
            // 
            // mainButton1
            // 
            this.mainButton1.ButtonName = "Generate Report";
            this.mainButton1.Location = new System.Drawing.Point(764, 11);
            this.mainButton1.Name = "mainButton1";
            this.mainButton1.Size = new System.Drawing.Size(149, 44);
            this.mainButton1.TabIndex = 6;
            // 
            // reportsPagination1
            // 
            this.reportsPagination1.BackColor = System.Drawing.Color.White;
            this.reportsPagination1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.reportsPagination1.Location = new System.Drawing.Point(0, 529);
            this.reportsPagination1.Name = "reportsPagination1";
            this.reportsPagination1.Size = new System.Drawing.Size(937, 41);
            this.reportsPagination1.TabIndex = 8;
            // 
            // inventoryFilter_Button1
            // 
            this.inventoryFilter_Button1.BackColor = System.Drawing.Color.Transparent;
            this.inventoryFilter_Button1.Location = new System.Drawing.Point(661, 11);
            this.inventoryFilter_Button1.Name = "inventoryFilter_Button1";
            this.inventoryFilter_Button1.Size = new System.Drawing.Size(102, 43);
            this.inventoryFilter_Button1.TabIndex = 11;
            // 
            // ReportsMainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.inventoryFilter_Button1);
            this.Controls.Add(this.reportsPagination1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainButton1);
            this.Controls.Add(this.label1);
            this.Name = "ReportsMainPanel";
            this.Size = new System.Drawing.Size(937, 570);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private UserControlFiles.MainButton mainButton1;
        private System.Windows.Forms.Panel panel1;
        private ReportsPagination reportsPagination1;
        private Inventory_Module.InventoryFilter_Button inventoryFilter_Button1;
    }
}
