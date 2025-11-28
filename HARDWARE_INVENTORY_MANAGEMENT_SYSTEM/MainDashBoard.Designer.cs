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
            this.pcbBlurOverlay = new System.Windows.Forms.PictureBox();
            this.MainContentPanel = new System.Windows.Forms.Panel();
            this.sidePanel2 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.SidePanel();
            this.ucTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.ucTopBar();
            this.sidePanel1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.SidePanel();
            ((System.ComponentModel.ISupportInitialize)(this.pcbBlurOverlay)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbBlurOverlay
            // 
            this.pcbBlurOverlay.BackColor = System.Drawing.Color.Transparent;
            this.pcbBlurOverlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pcbBlurOverlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcbBlurOverlay.Location = new System.Drawing.Point(0, 0);
            this.pcbBlurOverlay.Name = "pcbBlurOverlay";
            this.pcbBlurOverlay.Size = new System.Drawing.Size(1210, 720);
            this.pcbBlurOverlay.TabIndex = 4;
            this.pcbBlurOverlay.TabStop = false;
            this.pcbBlurOverlay.Visible = false;
            // 
            // MainContentPanel
            // 
            this.MainContentPanel.BackColor = System.Drawing.Color.White;
            this.MainContentPanel.Location = new System.Drawing.Point(234, 0);
            this.MainContentPanel.Name = "MainContentPanel";
            this.MainContentPanel.Size = new System.Drawing.Size(976, 720);
            this.MainContentPanel.TabIndex = 5;
            // 
            // sidePanel2
            // 
            this.sidePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.sidePanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sidePanel2.Location = new System.Drawing.Point(12, 12);
            this.sidePanel2.Name = "sidePanel2";
            this.sidePanel2.Size = new System.Drawing.Size(216, 698);
            this.sidePanel2.TabIndex = 6;
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
            // 
            // MainDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources._05_Dashboard;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1210, 720);
            this.Controls.Add(this.sidePanel2);
            this.Controls.Add(this.MainContentPanel);
            this.Controls.Add(this.pcbBlurOverlay);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainDashBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainDashBoard";
            this.Load += new System.EventHandler(this.MainDashBoard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbBlurOverlay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SidePanel sidePanel1;
        private ucTopBar ucTopBar1;
        public System.Windows.Forms.PictureBox pcbBlurOverlay;
        public System.Windows.Forms.Panel MainContentPanel;
        public SidePanel sidePanel2;
    }
}