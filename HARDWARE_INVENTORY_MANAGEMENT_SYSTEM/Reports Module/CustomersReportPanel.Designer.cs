namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    partial class CustomersReportPanel
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
            this.inventoryFilter_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryFilter_Button();
            this.mainButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles.MainButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.reportsKeyMetrics4 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsKeyMetrics();
            this.reportsKeyMetrics3 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsKeyMetrics();
            this.reportsKeyMetrics2 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsKeyMetrics();
            this.reportsKeyMetrics1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsKeyMetrics();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // inventoryFilter_Button1
            // 
            this.inventoryFilter_Button1.BackColor = System.Drawing.Color.Transparent;
            this.inventoryFilter_Button1.Location = new System.Drawing.Point(661, 11);
            this.inventoryFilter_Button1.Name = "inventoryFilter_Button1";
            this.inventoryFilter_Button1.Size = new System.Drawing.Size(102, 43);
            this.inventoryFilter_Button1.TabIndex = 14;
            // 
            // mainButton1
            // 
            this.mainButton1.ButtonName = "Generate Report";
            this.mainButton1.Location = new System.Drawing.Point(764, 11);
            this.mainButton1.Name = "mainButton1";
            this.mainButton1.Size = new System.Drawing.Size(149, 44);
            this.mainButton1.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.reportsKeyMetrics4);
            this.panel3.Controls.Add(this.reportsKeyMetrics3);
            this.panel3.Controls.Add(this.reportsKeyMetrics2);
            this.panel3.Controls.Add(this.reportsKeyMetrics1);
            this.panel3.Location = new System.Drawing.Point(18, 56);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(900, 90);
            this.panel3.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lexend SemiBold", 11.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 24);
            this.label1.TabIndex = 11;
            this.label1.Text = "Customer Reports";
            // 
            // reportsKeyMetrics4
            // 
            this.reportsKeyMetrics4.BackColor = System.Drawing.Color.Transparent;
            this.reportsKeyMetrics4.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Sales_Logo;
            this.reportsKeyMetrics4.Location = new System.Drawing.Point(692, 2);
            this.reportsKeyMetrics4.Name = "reportsKeyMetrics4";
            this.reportsKeyMetrics4.Size = new System.Drawing.Size(201, 86);
            this.reportsKeyMetrics4.TabIndex = 3;
            this.reportsKeyMetrics4.Title = "High Balance";
            this.reportsKeyMetrics4.Value = 1234;
            // 
            // reportsKeyMetrics3
            // 
            this.reportsKeyMetrics3.BackColor = System.Drawing.Color.Transparent;
            this.reportsKeyMetrics3.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.people_green;
            this.reportsKeyMetrics3.Location = new System.Drawing.Point(468, 2);
            this.reportsKeyMetrics3.Name = "reportsKeyMetrics3";
            this.reportsKeyMetrics3.Size = new System.Drawing.Size(201, 86);
            this.reportsKeyMetrics3.TabIndex = 2;
            this.reportsKeyMetrics3.Title = "Active Customers";
            this.reportsKeyMetrics3.Value = 1234;
            // 
            // reportsKeyMetrics2
            // 
            this.reportsKeyMetrics2.BackColor = System.Drawing.Color.Transparent;
            this.reportsKeyMetrics2.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.dollar_red;
            this.reportsKeyMetrics2.Location = new System.Drawing.Point(242, 2);
            this.reportsKeyMetrics2.Name = "reportsKeyMetrics2";
            this.reportsKeyMetrics2.Size = new System.Drawing.Size(201, 86);
            this.reportsKeyMetrics2.TabIndex = 1;
            this.reportsKeyMetrics2.Title = "Outstanding Bal.";
            this.reportsKeyMetrics2.Value = 1234;
            // 
            // reportsKeyMetrics1
            // 
            this.reportsKeyMetrics1.BackColor = System.Drawing.Color.Transparent;
            this.reportsKeyMetrics1.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.People_Blue;
            this.reportsKeyMetrics1.Location = new System.Drawing.Point(17, 2);
            this.reportsKeyMetrics1.Name = "reportsKeyMetrics1";
            this.reportsKeyMetrics1.Size = new System.Drawing.Size(201, 86);
            this.reportsKeyMetrics1.TabIndex = 0;
            this.reportsKeyMetrics1.Title = "Total Customers";
            this.reportsKeyMetrics1.Value = 1234;
            // 
            // CustomersReportPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.inventoryFilter_Button1);
            this.Controls.Add(this.mainButton1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Name = "CustomersReportPanel";
            this.Size = new System.Drawing.Size(937, 570);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Inventory_Module.InventoryFilter_Button inventoryFilter_Button1;
        private UserControlFiles.MainButton mainButton1;
        private System.Windows.Forms.Panel panel3;
        private ReportsKeyMetrics reportsKeyMetrics4;
        private ReportsKeyMetrics reportsKeyMetrics3;
        private ReportsKeyMetrics reportsKeyMetrics2;
        private ReportsKeyMetrics reportsKeyMetrics1;
        private System.Windows.Forms.Label label1;
    }
}
