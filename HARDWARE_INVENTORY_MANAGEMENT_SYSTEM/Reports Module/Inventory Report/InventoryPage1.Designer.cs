namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report
{
    partial class InventoryPage1
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
            this.reportsTable1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsTable();
            this.panel3 = new System.Windows.Forms.Panel();
            this.reportsKeyMetrics4 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsKeyMetrics();
            this.reportsKeyMetrics3 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsKeyMetrics();
            this.reportsKeyMetrics2 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsKeyMetrics();
            this.reportsKeyMetrics1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsKeyMetrics();
            this.ExportPDFBtn = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportsTable1
            // 
            this.reportsTable1.BackColor = System.Drawing.Color.Transparent;
            this.reportsTable1.Location = new System.Drawing.Point(2, 101);
            this.reportsTable1.Name = "reportsTable1";
            this.reportsTable1.Size = new System.Drawing.Size(910, 373);
            this.reportsTable1.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.reportsKeyMetrics4);
            this.panel3.Controls.Add(this.reportsKeyMetrics3);
            this.panel3.Controls.Add(this.reportsKeyMetrics2);
            this.panel3.Controls.Add(this.reportsKeyMetrics1);
            this.panel3.Location = new System.Drawing.Point(2, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(900, 90);
            this.panel3.TabIndex = 8;
            // 
            // reportsKeyMetrics4
            // 
            this.reportsKeyMetrics4.BackColor = System.Drawing.Color.Transparent;
            this.reportsKeyMetrics4.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Red_Warning;
            this.reportsKeyMetrics4.Location = new System.Drawing.Point(692, 2);
            this.reportsKeyMetrics4.Name = "reportsKeyMetrics4";
            this.reportsKeyMetrics4.Size = new System.Drawing.Size(201, 86);
            this.reportsKeyMetrics4.TabIndex = 3;
            this.reportsKeyMetrics4.Title = "Expiry Alert";
            this.reportsKeyMetrics4.Value = 1234;
            // 
            // reportsKeyMetrics3
            // 
            this.reportsKeyMetrics3.BackColor = System.Drawing.Color.Transparent;
            this.reportsKeyMetrics3.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Sales_Logo;
            this.reportsKeyMetrics3.Location = new System.Drawing.Point(472, 2);
            this.reportsKeyMetrics3.Name = "reportsKeyMetrics3";
            this.reportsKeyMetrics3.Size = new System.Drawing.Size(201, 86);
            this.reportsKeyMetrics3.TabIndex = 2;
            this.reportsKeyMetrics3.Title = "Low Stock Alert";
            this.reportsKeyMetrics3.Value = 1234;
            // 
            // reportsKeyMetrics2
            // 
            this.reportsKeyMetrics2.BackColor = System.Drawing.Color.Transparent;
            this.reportsKeyMetrics2.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Group_1000004200;
            this.reportsKeyMetrics2.Location = new System.Drawing.Point(245, 2);
            this.reportsKeyMetrics2.Name = "reportsKeyMetrics2";
            this.reportsKeyMetrics2.Size = new System.Drawing.Size(201, 86);
            this.reportsKeyMetrics2.TabIndex = 1;
            this.reportsKeyMetrics2.Title = "Inventory Value";
            this.reportsKeyMetrics2.Value = 1234;
            // 
            // reportsKeyMetrics1
            // 
            this.reportsKeyMetrics1.BackColor = System.Drawing.Color.Transparent;
            this.reportsKeyMetrics1.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Quantity1;
            this.reportsKeyMetrics1.Location = new System.Drawing.Point(17, 2);
            this.reportsKeyMetrics1.Name = "reportsKeyMetrics1";
            this.reportsKeyMetrics1.Size = new System.Drawing.Size(201, 86);
            this.reportsKeyMetrics1.TabIndex = 0;
            this.reportsKeyMetrics1.Title = "Total Products";
            this.reportsKeyMetrics1.Value = 1234;
            // 
            // ExportPDFBtn
            // 
            this.ExportPDFBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportPDFBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.ExportPDFBtn.FlatAppearance.BorderSize = 0;
            this.ExportPDFBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportPDFBtn.Font = new System.Drawing.Font("Lexend", 9F, System.Drawing.FontStyle.Bold);
            this.ExportPDFBtn.ForeColor = System.Drawing.Color.White;
            this.ExportPDFBtn.Location = new System.Drawing.Point(747, 12);
            this.ExportPDFBtn.Name = "ExportPDFBtn";
            this.ExportPDFBtn.Size = new System.Drawing.Size(150, 30);
            this.ExportPDFBtn.TabIndex = 10;
            this.ExportPDFBtn.Text = "Export PDF";
            this.ExportPDFBtn.UseVisualStyleBackColor = false;
           // this.ExportPDFBtn.Click += new System.EventHandler(this.ExportPDFBtn_Click);
            // 
            // InventoryPage1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ExportPDFBtn);
            this.Controls.Add(this.reportsTable1);
            this.Controls.Add(this.panel3);
            this.Name = "InventoryPage1";
            this.Size = new System.Drawing.Size(905, 470);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ReportsTable reportsTable1;
        private System.Windows.Forms.Panel panel3;
        private ReportsKeyMetrics reportsKeyMetrics4;
        private ReportsKeyMetrics reportsKeyMetrics3;
        private ReportsKeyMetrics reportsKeyMetrics2;
        private ReportsKeyMetrics reportsKeyMetrics1;
        private System.Windows.Forms.Button ExportPDFBtn;
    }
}
