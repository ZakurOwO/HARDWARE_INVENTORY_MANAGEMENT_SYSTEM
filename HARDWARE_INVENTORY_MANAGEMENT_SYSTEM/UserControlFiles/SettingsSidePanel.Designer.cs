namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles
{
    partial class SettingsSidePanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AuditLogBTN = new System.Windows.Forms.Button();
            this.HistoryBTN = new System.Windows.Forms.Button();
            this.AccountsBTN = new System.Windows.Forms.Button();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lexend SemiBold", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(61, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 38);
            this.label1.TabIndex = 23;
            this.label1.Text = "Settings";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.Controls.Add(this.pictureBox1);
            this.guna2Panel1.Controls.Add(this.AuditLogBTN);
            this.guna2Panel1.Controls.Add(this.HistoryBTN);
            this.guna2Panel1.Controls.Add(this.AccountsBTN);
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(197, 698);
            this.guna2Panel1.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Topaz_Icon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(18, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 38);
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // AuditLogBTN
            // 
            this.AuditLogBTN.BackColor = System.Drawing.Color.Transparent;
            this.AuditLogBTN.FlatAppearance.BorderSize = 0;
            this.AuditLogBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AuditLogBTN.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold);
            this.AuditLogBTN.ForeColor = System.Drawing.Color.Black;
            this.AuditLogBTN.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Auditlog;
            this.AuditLogBTN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AuditLogBTN.Location = new System.Drawing.Point(6, 207);
            this.AuditLogBTN.Name = "AuditLogBTN";
            this.AuditLogBTN.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.AuditLogBTN.Size = new System.Drawing.Size(188, 39);
            this.AuditLogBTN.TabIndex = 26;
            this.AuditLogBTN.Text = "     Audit Log";
            this.AuditLogBTN.UseVisualStyleBackColor = false;
            this.AuditLogBTN.Click += new System.EventHandler(this.AuditLogBTN_Click);
            // 
            // HistoryBTN
            // 
            this.HistoryBTN.BackColor = System.Drawing.Color.Transparent;
            this.HistoryBTN.FlatAppearance.BorderSize = 0;
            this.HistoryBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HistoryBTN.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold);
            this.HistoryBTN.ForeColor = System.Drawing.Color.Black;
            this.HistoryBTN.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.HistoryIcon;
            this.HistoryBTN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.HistoryBTN.Location = new System.Drawing.Point(6, 151);
            this.HistoryBTN.Name = "HistoryBTN";
            this.HistoryBTN.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.HistoryBTN.Size = new System.Drawing.Size(188, 39);
            this.HistoryBTN.TabIndex = 25;
            this.HistoryBTN.Text = " History";
            this.HistoryBTN.UseVisualStyleBackColor = false;
            this.HistoryBTN.Click += new System.EventHandler(this.HistoryBTN_Click);
            // 
            // AccountsBTN
            // 
            this.AccountsBTN.BackColor = System.Drawing.Color.Transparent;
            this.AccountsBTN.FlatAppearance.BorderSize = 0;
            this.AccountsBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AccountsBTN.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold);
            this.AccountsBTN.ForeColor = System.Drawing.Color.Black;
            this.AccountsBTN.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Customer;
            this.AccountsBTN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AccountsBTN.Location = new System.Drawing.Point(6, 95);
            this.AccountsBTN.Name = "AccountsBTN";
            this.AccountsBTN.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.AccountsBTN.Size = new System.Drawing.Size(188, 39);
            this.AccountsBTN.TabIndex = 24;
            this.AccountsBTN.Text = "    Accounts";
            this.AccountsBTN.UseVisualStyleBackColor = false;
            this.AccountsBTN.Click += new System.EventHandler(this.AccountsBTN_Click);
            // 
            // SettingsSidePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.guna2Panel1);
            this.Name = "SettingsSidePanel";
            this.Size = new System.Drawing.Size(197, 698);
            this.Load += new System.EventHandler(this.SettingsSidePanel_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AccountsBTN;
        private System.Windows.Forms.Button HistoryBTN;
        private System.Windows.Forms.Button AuditLogBTN;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}
