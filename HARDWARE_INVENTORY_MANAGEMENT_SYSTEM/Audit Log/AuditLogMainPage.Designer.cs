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
            this.pnlAuditLog = new Guna.UI2.WinForms.Guna2Panel();
            this.tbxSearchField = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.auditDataGrid2 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log.AuditDataGrid();
            this.btnMainButtonIcon = new Guna.UI2.WinForms.Guna2Button();
            this.auditDataGrid1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log.AuditDataGrid();
            this.pnlAuditLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAuditLog
            // 
            this.pnlAuditLog.BorderColor = System.Drawing.Color.Gainsboro;
            this.pnlAuditLog.BorderRadius = 15;
            this.pnlAuditLog.BorderThickness = 1;
            this.pnlAuditLog.Controls.Add(this.tbxSearchField);
            this.pnlAuditLog.Controls.Add(this.label1);
            this.pnlAuditLog.Controls.Add(this.auditDataGrid2);
            this.pnlAuditLog.Controls.Add(this.btnMainButtonIcon);
            this.pnlAuditLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAuditLog.Location = new System.Drawing.Point(0, 0);
            this.pnlAuditLog.Name = "pnlAuditLog";
            this.pnlAuditLog.Size = new System.Drawing.Size(940, 605);
            this.pnlAuditLog.TabIndex = 1;
            // 
            // tbxSearchField
            // 
            this.tbxSearchField.BorderRadius = 8;
            this.tbxSearchField.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxSearchField.DefaultText = "";
            this.tbxSearchField.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbxSearchField.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbxSearchField.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxSearchField.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxSearchField.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxSearchField.Font = new System.Drawing.Font("Lexend Light", 9F);
            this.tbxSearchField.ForeColor = System.Drawing.Color.DimGray;
            this.tbxSearchField.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxSearchField.IconLeft = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.search_02;
            this.tbxSearchField.IconLeftOffset = new System.Drawing.Point(5, 0);
            this.tbxSearchField.Location = new System.Drawing.Point(29, 53);
            this.tbxSearchField.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbxSearchField.Name = "tbxSearchField";
            this.tbxSearchField.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.tbxSearchField.PlaceholderText = "Search Activity";
            this.tbxSearchField.SelectedText = "";
            this.tbxSearchField.Size = new System.Drawing.Size(291, 40);
            this.tbxSearchField.TabIndex = 10;
            this.tbxSearchField.TextOffset = new System.Drawing.Point(3, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lexend SemiBold", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Audit Log";
            // 
            // auditDataGrid2
            // 
            this.auditDataGrid2.Location = new System.Drawing.Point(29, 111);
            this.auditDataGrid2.Name = "auditDataGrid2";
            this.auditDataGrid2.Size = new System.Drawing.Size(887, 465);
            this.auditDataGrid2.TabIndex = 8;
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
            this.btnMainButtonIcon.Location = new System.Drawing.Point(754, 55);
            this.btnMainButtonIcon.Name = "btnMainButtonIcon";
            this.btnMainButtonIcon.Size = new System.Drawing.Size(162, 40);
            this.btnMainButtonIcon.TabIndex = 7;
            this.btnMainButtonIcon.Text = "Download as CSV";
            // 
            // auditDataGrid1
            // 
            this.auditDataGrid1.Location = new System.Drawing.Point(21, 80);
            this.auditDataGrid1.Name = "auditDataGrid1";
            this.auditDataGrid1.Size = new System.Drawing.Size(887, 505);
            this.auditDataGrid1.TabIndex = 8;
            // 
            // AuditLogMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.pnlAuditLog);
            this.DoubleBuffered = true;
            this.Name = "AuditLogMainPage";
            this.Size = new System.Drawing.Size(940, 605);
            this.Load += new System.EventHandler(this.AuditLogMainPage_Load);
            this.pnlAuditLog.ResumeLayout(false);
            this.pnlAuditLog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel pnlAuditLog;
        private AuditDataGrid auditDataGrid1;
        private Guna.UI2.WinForms.Guna2Button btnMainButtonIcon;
        private AuditDataGrid auditDataGrid2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox tbxSearchField;
    }
}
