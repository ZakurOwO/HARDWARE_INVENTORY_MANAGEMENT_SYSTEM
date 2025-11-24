namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    partial class Deliveries_btncs
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainButtonWithIcon1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles.MainButtonWithIcon();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mainButtonWithIcon1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(157, 47);
            this.panel1.TabIndex = 0;
            // 
            // mainButtonWithIcon1
            // 
            this.mainButtonWithIcon1.BackColor = System.Drawing.Color.Transparent;
            this.mainButtonWithIcon1.ButtonName = "Add New Vehicle";
            this.mainButtonWithIcon1.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.add_circle;
            this.mainButtonWithIcon1.Location = new System.Drawing.Point(0, 0);
            this.mainButtonWithIcon1.Name = "mainButtonWithIcon1";
            this.mainButtonWithIcon1.Size = new System.Drawing.Size(157, 47);
            this.mainButtonWithIcon1.TabIndex = 2;
            this.mainButtonWithIcon1.Load += new System.EventHandler(this.mainButtonWithIcon1_Load);
            // 
            // Deliveries_btncs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "Deliveries_btncs";
            this.Size = new System.Drawing.Size(157, 47);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UserControlFiles.MainButtonWithIcon mainButtonWithIcon1;
    }
}
