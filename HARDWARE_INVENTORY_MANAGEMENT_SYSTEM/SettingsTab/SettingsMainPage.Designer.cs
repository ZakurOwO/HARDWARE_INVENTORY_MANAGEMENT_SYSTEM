namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles
{
    partial class SettingsMainPage
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
            this.pnlDisplaySettings = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnProfileMenu = new Guna.UI2.WinForms.Guna2Button();
            this.tabModules1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.SettingsTab.TabModules();
            this.SuspendLayout();
            // 
            // pnlDisplaySettings
            // 
            this.pnlDisplaySettings.BackColor = System.Drawing.Color.Transparent;
            this.pnlDisplaySettings.Location = new System.Drawing.Point(14, 116);
            this.pnlDisplaySettings.Name = "pnlDisplaySettings";
            this.pnlDisplaySettings.Size = new System.Drawing.Size(945, 580);
            this.pnlDisplaySettings.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lexend Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(45, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "View for Accounts, History, and Customers";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lexend SemiBold", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(41, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 38);
            this.label1.TabIndex = 22;
            this.label1.Text = "Settings";
            // 
            // btnProfileMenu
            // 
            this.btnProfileMenu.BackColor = System.Drawing.Color.Transparent;
            this.btnProfileMenu.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.My_Profile__3_;
            this.btnProfileMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnProfileMenu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnProfileMenu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnProfileMenu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnProfileMenu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnProfileMenu.FillColor = System.Drawing.Color.Transparent;
            this.btnProfileMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnProfileMenu.ForeColor = System.Drawing.Color.White;
            this.btnProfileMenu.Location = new System.Drawing.Point(796, 9);
            this.btnProfileMenu.Name = "btnProfileMenu";
            this.btnProfileMenu.Size = new System.Drawing.Size(146, 38);
            this.btnProfileMenu.TabIndex = 21;
            // 
            // tabModules1
            // 
            this.tabModules1.BackColor = System.Drawing.Color.White;
            this.tabModules1.Location = new System.Drawing.Point(203, 63);
            this.tabModules1.Name = "tabModules1";
            this.tabModules1.Size = new System.Drawing.Size(599, 46);
            this.tabModules1.TabIndex = 4;
            // 
            // SettingsMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnProfileMenu);
            this.Controls.Add(this.tabModules1);
            this.Controls.Add(this.pnlDisplaySettings);
            this.Name = "SettingsMainPage";
            this.Size = new System.Drawing.Size(975, 720);
            this.Load += new System.EventHandler(this.SettingsMainPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel pnlDisplaySettings;
        private SettingsTab.TabModules tabModules1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnProfileMenu;
    }
}
