namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles
{
    partial class MainButton
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
            this.btnMainButton = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // btnMainButton
            // 
            this.btnMainButton.BorderRadius = 8;
            this.btnMainButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMainButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMainButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMainButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMainButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.btnMainButton.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold);
            this.btnMainButton.ForeColor = System.Drawing.Color.White;
            this.btnMainButton.Location = new System.Drawing.Point(3, 3);
            this.btnMainButton.Name = "btnMainButton";
            this.btnMainButton.Size = new System.Drawing.Size(135, 35);
            this.btnMainButton.TabIndex = 3;
            this.btnMainButton.Text = "Generate Report";
            // 
            // MainButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnMainButton);
            this.Name = "MainButton";
            this.Size = new System.Drawing.Size(149, 44);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnMainButton;
    }
}
