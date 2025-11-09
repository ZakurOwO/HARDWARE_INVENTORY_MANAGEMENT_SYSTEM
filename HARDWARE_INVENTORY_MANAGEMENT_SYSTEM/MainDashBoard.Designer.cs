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
            this.MainContentPanel = new Krypton.Toolkit.KryptonPanel();
            this.reportsTable1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsTable();
            this.ucKeyMetrics1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.ucKeyMetrics();
            this.ucTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.ucTopBar();
            this.sidePanel1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.SidePanel();
            ((System.ComponentModel.ISupportInitialize)(this.MainContentPanel)).BeginInit();
            this.MainContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainContentPanel
            // 
            this.MainContentPanel.Controls.Add(this.reportsTable1);
            this.MainContentPanel.Location = new System.Drawing.Point(233, 12);
            this.MainContentPanel.Name = "MainContentPanel";
            this.MainContentPanel.Size = new System.Drawing.Size(965, 696);
            this.MainContentPanel.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.MainContentPanel.StateCommon.Color2 = System.Drawing.Color.Transparent;
            this.MainContentPanel.TabIndex = 3;
            this.MainContentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainContentPanel_Paint);
            // 
            // reportsTable1
            // 
            this.reportsTable1.BackColor = System.Drawing.Color.Transparent;
            this.reportsTable1.Location = new System.Drawing.Point(38, 119);
            this.reportsTable1.Name = "reportsTable1";
            this.reportsTable1.Size = new System.Drawing.Size(910, 385);
            this.reportsTable1.TabIndex = 0;
            // 
            // ucKeyMetrics1
            // 
            this.ucKeyMetrics1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ucKeyMetrics1.Location = new System.Drawing.Point(271, 88);
            this.ucKeyMetrics1.Name = "ucKeyMetrics1";
            this.ucKeyMetrics1.Size = new System.Drawing.Size(879, 93);
            this.ucKeyMetrics1.TabIndex = 2;
            // 
            // ucTopBar1
            // 
            this.ucTopBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ucTopBar1.Location = new System.Drawing.Point(244, -2);
            this.ucTopBar1.Name = "ucTopBar1";
            this.ucTopBar1.Size = new System.Drawing.Size(965, 69);
            this.ucTopBar1.TabIndex = 1;
            // 
            // sidePanel1
            // 
            this.sidePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.sidePanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sidePanel1.Location = new System.Drawing.Point(22, 23);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(202, 670);
            this.sidePanel1.TabIndex = 0;
            this.sidePanel1.Load += new System.EventHandler(this.sidePanel1_Load_1);
            // 
            // MainDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources._05_Dashboard;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1210, 720);
            this.Controls.Add(this.MainContentPanel);
            this.Controls.Add(this.ucKeyMetrics1);
            this.Controls.Add(this.ucTopBar1);
            this.Controls.Add(this.sidePanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainDashBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainDashBoard";
            this.Load += new System.EventHandler(this.MainDashBoard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainContentPanel)).EndInit();
            this.MainContentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SidePanel sidePanel1;
        private ucTopBar ucTopBar1;
        private ucKeyMetrics ucKeyMetrics1;
        private Krypton.Toolkit.KryptonPanel MainContentPanel;
        private Reports_Module.ReportsTable reportsTable1;
    }
}