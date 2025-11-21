namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    partial class PaginationTransation
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
            this.GoleftButton = new Guna.UI2.WinForms.Guna2Button();
            this.GorightButton = new Guna.UI2.WinForms.Guna2Button();
            this.PaginationPageNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GoleftButton
            // 
            this.GoleftButton.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.direction_right_2;
            this.GoleftButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.GoleftButton.BorderColor = System.Drawing.Color.LightGray;
            this.GoleftButton.BorderRadius = 5;
            this.GoleftButton.BorderThickness = 1;
            this.GoleftButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.GoleftButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.GoleftButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.GoleftButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.GoleftButton.FillColor = System.Drawing.Color.Transparent;
            this.GoleftButton.Font = new System.Drawing.Font("Lexend Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GoleftButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(28)))), ((int)(((byte)(35)))));
            this.GoleftButton.Location = new System.Drawing.Point(536, 17);
            this.GoleftButton.Name = "GoleftButton";
            this.GoleftButton.Size = new System.Drawing.Size(29, 29);
            this.GoleftButton.TabIndex = 5;
            this.GoleftButton.Click += new System.EventHandler(this.GoleftButton_Click);
            // 
            // GorightButton
            // 
            this.GorightButton.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.direction_right_01;
            this.GorightButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.GorightButton.BorderColor = System.Drawing.Color.LightGray;
            this.GorightButton.BorderRadius = 5;
            this.GorightButton.BorderThickness = 1;
            this.GorightButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.GorightButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.GorightButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.GorightButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.GorightButton.FillColor = System.Drawing.Color.Transparent;
            this.GorightButton.Font = new System.Drawing.Font("Lexend Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GorightButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(28)))), ((int)(((byte)(35)))));
            this.GorightButton.Location = new System.Drawing.Point(571, 17);
            this.GorightButton.Name = "GorightButton";
            this.GorightButton.Size = new System.Drawing.Size(29, 29);
            this.GorightButton.TabIndex = 4;
            this.GorightButton.Click += new System.EventHandler(this.GorightButton_Click);
            // 
            // PaginationPageNumber
            // 
            this.PaginationPageNumber.AutoSize = true;
            this.PaginationPageNumber.Font = new System.Drawing.Font("Lexend Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PaginationPageNumber.Location = new System.Drawing.Point(185, 30);
            this.PaginationPageNumber.Name = "PaginationPageNumber";
            this.PaginationPageNumber.Size = new System.Drawing.Size(153, 17);
            this.PaginationPageNumber.TabIndex = 8;
            this.PaginationPageNumber.Text = "Showing 1 out of 40 records";
            this.PaginationPageNumber.Click += new System.EventHandler(this.PaginationPageNumber_Click);
            // 
            // PaginationTransation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PaginationPageNumber);
            this.Controls.Add(this.GoleftButton);
            this.Controls.Add(this.GorightButton);
            this.Name = "PaginationTransation";
            this.Size = new System.Drawing.Size(615, 58);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button GorightButton;
        private Guna.UI2.WinForms.Guna2Button GoleftButton;
        private System.Windows.Forms.Label PaginationPageNumber;
    }
}
