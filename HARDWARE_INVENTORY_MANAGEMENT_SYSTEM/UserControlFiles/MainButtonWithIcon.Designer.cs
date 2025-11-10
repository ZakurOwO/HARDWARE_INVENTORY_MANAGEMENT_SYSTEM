namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles
{
    partial class MainButtonWithIcon
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
            this.btnMainButtonIcon = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
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
            this.btnMainButtonIcon.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Add_User;
            this.btnMainButtonIcon.Location = new System.Drawing.Point(3, 3);
            this.btnMainButtonIcon.Name = "btnMainButtonIcon";
            this.btnMainButtonIcon.Size = new System.Drawing.Size(151, 40);
            this.btnMainButtonIcon.TabIndex = 3;
            this.btnMainButtonIcon.Text = "Add New User";
            // 
            // MainButtonWithIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnMainButtonIcon);
            this.Name = "MainButtonWithIcon";
            this.Size = new System.Drawing.Size(157, 47);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnMainButtonIcon;
    }
}
