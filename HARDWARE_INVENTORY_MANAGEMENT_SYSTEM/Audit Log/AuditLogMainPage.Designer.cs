namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log
{
    partial class AuditLogMainPage
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
            this.btnMainButtonIcon = new Guna.UI2.WinForms.Guna2Button();
            this.inventoryFilter_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryFilter_Button();
            this.searchField1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.SearchField();
            this.auditDataGrid1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log.AuditDataGrid();
            this.auditLogTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log.AuditLogTopBar();
            this.auditDataGrid2 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log.AuditDataGrid();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.auditDataGrid2);
            this.guna2Panel1.Controls.Add(this.btnMainButtonIcon);
            this.guna2Panel1.Controls.Add(this.inventoryFilter_Button1);
            this.guna2Panel1.Controls.Add(this.searchField1);
            this.guna2Panel1.Location = new System.Drawing.Point(18, 88);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(929, 608);
            this.guna2Panel1.TabIndex = 1;
            // 
            // btnMainButtonIcon
            // 
            this.btnMainButtonIcon.BorderRadius = 6;
            this.btnMainButtonIcon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMainButtonIcon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMainButtonIcon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMainButtonIcon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMainButtonIcon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.btnMainButtonIcon.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainButtonIcon.ForeColor = System.Drawing.Color.White;
            this.btnMainButtonIcon.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.upload;
            this.btnMainButtonIcon.Location = new System.Drawing.Point(746, 14);
            this.btnMainButtonIcon.Name = "btnMainButtonIcon";
            this.btnMainButtonIcon.Size = new System.Drawing.Size(162, 40);
            this.btnMainButtonIcon.TabIndex = 7;
            this.btnMainButtonIcon.Text = "Download as CSV";
            // 
            // inventoryFilter_Button1
            // 
            this.inventoryFilter_Button1.BackColor = System.Drawing.Color.Transparent;
            this.inventoryFilter_Button1.Location = new System.Drawing.Point(324, 16);
            this.inventoryFilter_Button1.Name = "inventoryFilter_Button1";
            this.inventoryFilter_Button1.Size = new System.Drawing.Size(102, 43);
            this.inventoryFilter_Button1.TabIndex = 6;
            // 
            // searchField1
            // 
            this.searchField1.BackColor = System.Drawing.Color.Transparent;
            this.searchField1.Location = new System.Drawing.Point(17, 14);
            this.searchField1.Name = "searchField1";
            this.searchField1.PromptMessage = "Search";
            this.searchField1.Size = new System.Drawing.Size(299, 54);
            this.searchField1.TabIndex = 5;
            // 
            // auditDataGrid1
            // 
            this.auditDataGrid1.Location = new System.Drawing.Point(21, 80);
            this.auditDataGrid1.Name = "auditDataGrid1";
            this.auditDataGrid1.Size = new System.Drawing.Size(887, 505);
            this.auditDataGrid1.TabIndex = 8;
            // 
            // auditLogTopBar1
            // 
            this.auditLogTopBar1.BackColor = System.Drawing.Color.White;
            this.auditLogTopBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.auditLogTopBar1.Location = new System.Drawing.Point(0, 0);
            this.auditLogTopBar1.Name = "auditLogTopBar1";
            this.auditLogTopBar1.Size = new System.Drawing.Size(965, 69);
            this.auditLogTopBar1.TabIndex = 0;
            // 
            // auditDataGrid2
            // 
            this.auditDataGrid2.Location = new System.Drawing.Point(17, 84);
            this.auditDataGrid2.Name = "auditDataGrid2";
            this.auditDataGrid2.Size = new System.Drawing.Size(887, 478);
            this.auditDataGrid2.TabIndex = 8;
            // 
            // AuditLogMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.auditLogTopBar1);
            this.DoubleBuffered = true;
            this.Name = "AuditLogMainPage";
            this.Size = new System.Drawing.Size(965, 720);
            this.Load += new System.EventHandler(this.AuditLogMainPage_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AuditLogTopBar auditLogTopBar1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private AuditDataGrid auditDataGrid1;
        private Guna.UI2.WinForms.Guna2Button btnMainButtonIcon;
        private Inventory_Module.InventoryFilter_Button inventoryFilter_Button1;
        private Accounts_Module.SearchField searchField1;
        private AuditDataGrid auditDataGrid2;
    }
}
