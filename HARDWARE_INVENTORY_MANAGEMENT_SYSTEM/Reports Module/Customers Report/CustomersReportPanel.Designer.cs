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
            this.mainButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles.MainButton();
            this.exportButton = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles.MainButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // mainButton1
            //
            this.mainButton1.ButtonName = "Generate Report";
            this.mainButton1.Location = new System.Drawing.Point(764, 11);
            this.mainButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainButton1.Name = "mainButton1";
            this.mainButton1.Size = new System.Drawing.Size(149, 44);
            this.mainButton1.TabIndex = 13;
            this.mainButton1.Load += new System.EventHandler(this.mainButton1_Load);
            this.mainButton1.Click += new System.EventHandler(this.mainButton1_Click);
            //
            // exportButton
            //
            this.exportButton.ButtonName = "Export CSV";
            this.exportButton.Location = new System.Drawing.Point(603, 11);
            this.exportButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(149, 44);
            this.exportButton.TabIndex = 16;
            this.exportButton.Load += new System.EventHandler(this.exportButton_Load);
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "Customer Reports";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(18, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(905, 470);
            this.panel1.TabIndex = 15;
            // 
            // CustomersReportPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainButton1);
            this.Controls.Add(this.label1);
            this.Name = "CustomersReportPanel";
            this.Size = new System.Drawing.Size(937, 570);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private UserControlFiles.MainButton mainButton1;
        private UserControlFiles.MainButton exportButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}