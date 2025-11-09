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
            this.auditLogTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log.AuditLogTopBar();
            this.auditDataGrid1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log.AuditDataGrid();
            this.SuspendLayout();
            // 
            // auditLogTopBar1
            // 
            this.auditLogTopBar1.BackColor = System.Drawing.Color.White;
            this.auditLogTopBar1.Location = new System.Drawing.Point(0, 0);
            this.auditLogTopBar1.Name = "auditLogTopBar1";
            this.auditLogTopBar1.Size = new System.Drawing.Size(965, 69);
            this.auditLogTopBar1.TabIndex = 0;
            // 
            // auditDataGrid1
            // 
            this.auditDataGrid1.Location = new System.Drawing.Point(103, 115);
            this.auditDataGrid1.Name = "auditDataGrid1";
            this.auditDataGrid1.Size = new System.Drawing.Size(778, 442);
            this.auditDataGrid1.TabIndex = 1;
            // 
            // AuditLogMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.auditDataGrid1);
            this.Controls.Add(this.auditLogTopBar1);
            this.DoubleBuffered = true;
            this.Name = "AuditLogMainPage";
            this.Size = new System.Drawing.Size(965, 696);
            this.Load += new System.EventHandler(this.AuditLogMainPage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AuditLogTopBar auditLogTopBar1;
        private AuditDataGrid auditDataGrid1;
    }
}
