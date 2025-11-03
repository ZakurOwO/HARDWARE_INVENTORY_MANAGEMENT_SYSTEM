namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    partial class ReportsMainPage
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
            this.reportsTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsTopBar();
            this.reportsNavigationBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsNavigationBar();
            this.generateReport_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.GenerateReport_Button();
            this.inventory_KeyMetrics1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_KeyMetrics();
            this.SuspendLayout();
            // 
            // reportsTopBar1
            // 
            this.reportsTopBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.reportsTopBar1.Location = new System.Drawing.Point(0, 0);
            this.reportsTopBar1.Name = "reportsTopBar1";
            this.reportsTopBar1.Size = new System.Drawing.Size(974, 70);
            this.reportsTopBar1.TabIndex = 0;
            // 
            // reportsNavigationBar1
            // 
            this.reportsNavigationBar1.BackColor = System.Drawing.Color.White;
            this.reportsNavigationBar1.Location = new System.Drawing.Point(127, 67);
            this.reportsNavigationBar1.Name = "reportsNavigationBar1";
            this.reportsNavigationBar1.Size = new System.Drawing.Size(708, 46);
            this.reportsNavigationBar1.TabIndex = 1;
            // 
            // generateReport_Button1
            // 
            this.generateReport_Button1.BackColor = System.Drawing.Color.Transparent;
            this.generateReport_Button1.Location = new System.Drawing.Point(799, 138);
            this.generateReport_Button1.Name = "generateReport_Button1";
            this.generateReport_Button1.Size = new System.Drawing.Size(141, 40);
            this.generateReport_Button1.TabIndex = 2;
            // 
            // inventory_KeyMetrics1
            // 
            this.inventory_KeyMetrics1.BackColor = System.Drawing.Color.Transparent;
            this.inventory_KeyMetrics1.Location = new System.Drawing.Point(40, 184);
            this.inventory_KeyMetrics1.Name = "inventory_KeyMetrics1";
            this.inventory_KeyMetrics1.Size = new System.Drawing.Size(900, 90);
            this.inventory_KeyMetrics1.TabIndex = 3;
            // 
            // ReportsMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.inventory_KeyMetrics1);
            this.Controls.Add(this.generateReport_Button1);
            this.Controls.Add(this.reportsNavigationBar1);
            this.Controls.Add(this.reportsTopBar1);
            this.Name = "ReportsMainPage";
            this.Size = new System.Drawing.Size(975, 720);
            this.ResumeLayout(false);

        }

        #endregion

        private ReportsTopBar reportsTopBar1;
        private ReportsNavigationBar reportsNavigationBar1;
        private GenerateReport_Button generateReport_Button1;
        private Inventory_KeyMetrics inventory_KeyMetrics1;
    }
}
