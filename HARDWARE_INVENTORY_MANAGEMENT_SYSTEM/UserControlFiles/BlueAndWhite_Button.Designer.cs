namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    partial class Proceed_ClearButton
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
            this.btnWhite = new Guna.UI2.WinForms.Guna2Button();
            this.btnBlue = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // btnWhite
            // 
            this.btnWhite.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnWhite.BorderRadius = 8;
            this.btnWhite.BorderThickness = 1;
            this.btnWhite.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnWhite.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnWhite.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnWhite.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnWhite.FillColor = System.Drawing.Color.White;
            this.btnWhite.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWhite.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnWhite.Location = new System.Drawing.Point(1, 3);
            this.btnWhite.Name = "btnWhite";
            this.btnWhite.PressedColor = System.Drawing.Color.Azure;
            this.btnWhite.Size = new System.Drawing.Size(120, 40);
            this.btnWhite.TabIndex = 4;
            this.btnWhite.Text = "Clear";
            // 
            // btnBlue
            // 
            this.btnBlue.BorderRadius = 8;
            this.btnBlue.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBlue.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBlue.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBlue.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBlue.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.btnBlue.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBlue.ForeColor = System.Drawing.Color.White;
            this.btnBlue.Location = new System.Drawing.Point(132, 3);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(120, 40);
            this.btnBlue.TabIndex = 3;
            this.btnBlue.Text = "Proceed";
            // 
            // Proceed_ClearButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnWhite);
            this.Controls.Add(this.btnBlue);
            this.Name = "Proceed_ClearButton";
            this.Size = new System.Drawing.Size(256, 45);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnWhite;
        private Guna.UI2.WinForms.Guna2Button btnBlue;
    }
}
