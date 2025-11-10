namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    partial class AccountsTopBar
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
            this.btnProfileMenu = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
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
            this.btnProfileMenu.Location = new System.Drawing.Point(789, 16);
            this.btnProfileMenu.Name = "btnProfileMenu";
            this.btnProfileMenu.Size = new System.Drawing.Size(146, 38);
            this.btnProfileMenu.TabIndex = 12;
            this.btnProfileMenu.Click += new System.EventHandler(this.btnProfileMenu_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lexend Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(35, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Manage system users";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lexend SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 24);
            this.label1.TabIndex = 13;
            this.label1.Text = "Accounts";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(473, 14);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(173, 40);
            this.kryptonButton1.TabIndex = 15;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Text = "kryptonButton1";
            // 
            // AccountsTopBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnProfileMenu);
            this.Name = "AccountsTopBar";
            this.Size = new System.Drawing.Size(960, 69);
            this.Click += new System.EventHandler(this.AccountsTopBar_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnProfileMenu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}
