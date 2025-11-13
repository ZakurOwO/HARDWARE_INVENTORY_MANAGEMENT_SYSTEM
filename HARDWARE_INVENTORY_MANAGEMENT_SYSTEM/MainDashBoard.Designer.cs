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
            this.ucKeyMetrics1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.ucKeyMetrics();
            this.ucTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.ucTopBar();
            this.sidePanel1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.SidePanel();
            this.itemDescription_Form1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.ItemDescription_Form();
            ((System.ComponentModel.ISupportInitialize)(this.MainContentPanel)).BeginInit();
            this.MainContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainContentPanel
            // 
            this.MainContentPanel.Controls.Add(this.itemDescription_Form1);
            this.MainContentPanel.Location = new System.Drawing.Point(234, 0);
            this.MainContentPanel.Name = "MainContentPanel";
            this.MainContentPanel.Size = new System.Drawing.Size(976, 720);
            this.MainContentPanel.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.MainContentPanel.StateCommon.Color2 = System.Drawing.Color.Transparent;
            this.MainContentPanel.TabIndex = 3;
            this.MainContentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainContentPanel_Paint);
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
            // itemDescription_Form1
            // 
            this.itemDescription_Form1.BackColor = System.Drawing.Color.White;
            this.itemDescription_Form1.Location = new System.Drawing.Point(138, 103);
            this.itemDescription_Form1.Name = "itemDescription_Form1";
            this.itemDescription_Form1.Size = new System.Drawing.Size(841, 575);
            this.itemDescription_Form1.TabIndex = 0;
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
        private Inventory_Module.ItemDescription_Form itemDescription_Form1;
    }
}