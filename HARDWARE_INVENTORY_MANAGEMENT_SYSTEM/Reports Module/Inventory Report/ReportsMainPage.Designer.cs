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
            this.pnlMainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.reportsNavigationBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsNavigationBar();
            this.reportsTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsTopBar();
            this.SuspendLayout();
            // 
            // pnlMainPanel
            // 
            this.pnlMainPanel.BackColor = System.Drawing.Color.Transparent;
            this.pnlMainPanel.BorderColor = System.Drawing.Color.LightGray;
            this.pnlMainPanel.BorderRadius = 13;
            this.pnlMainPanel.BorderThickness = 1;
            this.pnlMainPanel.FillColor = System.Drawing.Color.White;
            this.pnlMainPanel.Location = new System.Drawing.Point(18, 124);
            this.pnlMainPanel.Name = "pnlMainPanel";
            this.pnlMainPanel.Size = new System.Drawing.Size(937, 570);
            this.pnlMainPanel.TabIndex = 3;
            // 
            // reportsNavigationBar1
            // 
            this.reportsNavigationBar1.BackColor = System.Drawing.Color.White;
            this.reportsNavigationBar1.Location = new System.Drawing.Point(127, 67);
            this.reportsNavigationBar1.Margin = new System.Windows.Forms.Padding(4);
            this.reportsNavigationBar1.Name = "reportsNavigationBar1";
            this.reportsNavigationBar1.Size = new System.Drawing.Size(708, 46);
            this.reportsNavigationBar1.TabIndex = 1;
            // 
            // reportsTopBar1
            // 
            this.reportsTopBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.reportsTopBar1.Location = new System.Drawing.Point(0, 0);
            this.reportsTopBar1.Margin = new System.Windows.Forms.Padding(4);
            this.reportsTopBar1.Name = "reportsTopBar1";
            this.reportsTopBar1.Size = new System.Drawing.Size(974, 70);
            this.reportsTopBar1.TabIndex = 0;
            // 
            // ReportsMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlMainPanel);
            this.Controls.Add(this.reportsNavigationBar1);
            this.Controls.Add(this.reportsTopBar1);
            this.Name = "ReportsMainPage";
            this.Size = new System.Drawing.Size(975, 720);
            this.Load += new System.EventHandler(this.ReportsMainPage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ReportsTopBar reportsTopBar1;
        private ReportsNavigationBar reportsNavigationBar1;
        private Guna.UI2.WinForms.Guna2Panel pnlMainPanel;
    }
}
