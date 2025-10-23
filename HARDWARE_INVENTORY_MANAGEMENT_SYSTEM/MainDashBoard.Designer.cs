namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    partial class MainDashBoard
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sidePanel1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.SidePanel();
            this.SuspendLayout();
            // 
            // sidePanel1
            // 
            this.sidePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.sidePanel1.Location = new System.Drawing.Point(27, 38);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(263, 988);
            this.sidePanel1.TabIndex = 0;
            this.sidePanel1.Load += new System.EventHandler(this.sidePanel1_Load_1);
            // 
            // MainDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources._05_Dashboard;
            this.ClientSize = new System.Drawing.Size(1534, 1061);
            this.Controls.Add(this.sidePanel1);
            this.Name = "MainDashBoard";
            this.Text = "MainDashBoard";
            this.ResumeLayout(false);

        }

        #endregion

        private SidePanel sidePanel1;
    }
}