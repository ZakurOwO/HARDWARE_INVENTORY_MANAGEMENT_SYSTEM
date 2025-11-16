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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.inventoryFilter_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryFilter_Button();
            this.searchField1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.SearchField();
            this.datGridTableHistory1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module.DatGridTableHistory();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.inventoryFilter_Button1);
            this.guna2Panel1.Controls.Add(this.searchField1);
            this.guna2Panel1.Controls.Add(this.datGridTableHistory1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(945, 580);
            this.guna2Panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lexend SemiBold", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "History";
            // 
            // inventoryFilter_Button1
            // 
            this.inventoryFilter_Button1.BackColor = System.Drawing.Color.Transparent;
            this.inventoryFilter_Button1.Location = new System.Drawing.Point(326, 52);
            this.inventoryFilter_Button1.Name = "inventoryFilter_Button1";
            this.inventoryFilter_Button1.Size = new System.Drawing.Size(102, 43);
            this.inventoryFilter_Button1.TabIndex = 0;
            // 
            // searchField1
            // 
            this.searchField1.BackColor = System.Drawing.Color.Transparent;
            this.searchField1.Location = new System.Drawing.Point(29, 49);
            this.searchField1.Name = "searchField1";
            this.searchField1.PromptMessage = "Search History";
            this.searchField1.Size = new System.Drawing.Size(299, 54);
            this.searchField1.TabIndex = 0;
            // 
            // datGridTableHistory1
            // 
            this.datGridTableHistory1.BackColor = System.Drawing.Color.White;
            this.datGridTableHistory1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.datGridTableHistory1.Location = new System.Drawing.Point(32, 106);
            this.datGridTableHistory1.Name = "datGridTableHistory1";
            this.datGridTableHistory1.Size = new System.Drawing.Size(885, 481);
            this.datGridTableHistory1.TabIndex = 3;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.guna2Panel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(945, 580);
            this.kryptonPanel1.StateCommon.Color1 = System.Drawing.Color.White;
            this.kryptonPanel1.StateCommon.Color2 = System.Drawing.Color.White;
            this.kryptonPanel1.StateNormal.Color1 = System.Drawing.Color.Transparent;
            this.kryptonPanel1.TabIndex = 0;
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
            this.Size = new System.Drawing.Size(945, 580);
            this.Load += new System.EventHandler(this.HistoryMainPage_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Inventory_Module.InventoryFilter_Button inventoryFilter_Button1;
        private Accounts_Module.SearchField searchField1;
        private DatGridTableHistory datGridTableHistory1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.Label label1;
    }
}
