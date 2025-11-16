namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module
{
    partial class HistoryMainPage
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
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.historyTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module.HistoryTopBar();
            this.upperPanels1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module.UpperPanels();
            this.datGridTableHistory1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module.DatGridTableHistory();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.historyTopBar1);
            this.kryptonPanel1.Controls.Add(this.guna2Panel1);
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(965, 732);
            this.kryptonPanel1.StateCommon.Color1 = System.Drawing.Color.White;
            this.kryptonPanel1.StateCommon.Color2 = System.Drawing.Color.White;
            this.kryptonPanel1.StateNormal.Color1 = System.Drawing.Color.Transparent;
            this.kryptonPanel1.TabIndex = 0;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.upperPanels1);
            this.guna2Panel1.Controls.Add(this.datGridTableHistory1);
            this.guna2Panel1.Location = new System.Drawing.Point(20, 75);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(927, 604);
            this.guna2Panel1.TabIndex = 5;
            // 
            // historyTopBar1
            // 
            this.historyTopBar1.BackColor = System.Drawing.Color.White;
            this.historyTopBar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.historyTopBar1.Location = new System.Drawing.Point(0, 0);
            this.historyTopBar1.Name = "historyTopBar1";
            this.historyTopBar1.Size = new System.Drawing.Size(965, 69);
            this.historyTopBar1.TabIndex = 4;
            // 
            // upperPanels1
            // 
            this.upperPanels1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.upperPanels1.Location = new System.Drawing.Point(13, 3);
            this.upperPanels1.Name = "upperPanels1";
            this.upperPanels1.Size = new System.Drawing.Size(911, 73);
            this.upperPanels1.TabIndex = 2;
            // 
            // datGridTableHistory1
            // 
            this.datGridTableHistory1.BackColor = System.Drawing.Color.White;
            this.datGridTableHistory1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.datGridTableHistory1.Location = new System.Drawing.Point(24, 82);
            this.datGridTableHistory1.Name = "datGridTableHistory1";
            this.datGridTableHistory1.Size = new System.Drawing.Size(883, 505);
            this.datGridTableHistory1.TabIndex = 3;
            // 
            // HistoryMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.kryptonPanel1);
            this.DoubleBuffered = true;
            this.Name = "HistoryMainPage";
            this.Size = new System.Drawing.Size(965, 696);
            this.Load += new System.EventHandler(this.HistoryMainPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private UpperPanels upperPanels1;
        private DatGridTableHistory datGridTableHistory1;
        private HistoryTopBar historyTopBar1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}
