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
            this.datGridTableHistory1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module.DatGridTableHistory();
            this.upperPanels1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module.UpperPanels();
            this.historyTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module.HistoryTopBar();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.datGridTableHistory1);
            this.kryptonPanel1.Controls.Add(this.upperPanels1);
            this.kryptonPanel1.Controls.Add(this.historyTopBar1);
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(886, 732);
            this.kryptonPanel1.StateNormal.Color1 = System.Drawing.Color.Transparent;
            this.kryptonPanel1.TabIndex = 0;
            // 
            // datGridTableHistory1
            // 
            this.datGridTableHistory1.BackColor = System.Drawing.Color.White;
            this.datGridTableHistory1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.datGridTableHistory1.Location = new System.Drawing.Point(64, 205);
            this.datGridTableHistory1.Name = "datGridTableHistory1";
            this.datGridTableHistory1.Size = new System.Drawing.Size(781, 442);
            this.datGridTableHistory1.TabIndex = 3;
            // 
            // upperPanels1
            // 
            this.upperPanels1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.upperPanels1.Location = new System.Drawing.Point(89, 122);
            this.upperPanels1.Name = "upperPanels1";
            this.upperPanels1.Size = new System.Drawing.Size(733, 90);
            this.upperPanels1.TabIndex = 2;
            // 
            // historyTopBar1
            // 
            this.historyTopBar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.historyTopBar1.Location = new System.Drawing.Point(53, 21);
            this.historyTopBar1.Name = "historyTopBar1";
            this.historyTopBar1.Size = new System.Drawing.Size(792, 69);
            this.historyTopBar1.TabIndex = 1;
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
            this.Size = new System.Drawing.Size(886, 732);
            this.Load += new System.EventHandler(this.HistoryMainPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private HistoryTopBar historyTopBar1;
        private UpperPanels upperPanels1;
        private DatGridTableHistory datGridTableHistory1;
    }
}
