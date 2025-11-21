namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    partial class Inventory_Pagination
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
            this.PaginationPageNumber = new System.Windows.Forms.Label();
            this.GoleftButton = new Guna.UI2.WinForms.Guna2Button();
            this.GoRightButton = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // PaginationPageNumber
            // 
            this.PaginationPageNumber.AutoSize = true;
            this.PaginationPageNumber.Font = new System.Drawing.Font("Lexend Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PaginationPageNumber.Location = new System.Drawing.Point(367, 26);
            this.PaginationPageNumber.Name = "PaginationPageNumber";
            this.PaginationPageNumber.Size = new System.Drawing.Size(153, 17);
            this.PaginationPageNumber.TabIndex = 16;
            this.PaginationPageNumber.Text = "Showing 1 out of 40 records";
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
            this.GoleftButton.Location = new System.Drawing.Point(841, 18);
            this.GoleftButton.Name = "GoleftButton";
            this.GoleftButton.PressedColor = System.Drawing.Color.White;
            this.GoleftButton.Size = new System.Drawing.Size(29, 29);
            this.GoleftButton.TabIndex = 13;
            this.GoleftButton.Click += new System.EventHandler(this.GoleftButton_Click);
            // 
            // GoRightButton
            // 
            this.GoRightButton.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.direction_right_01;
            this.GoRightButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.GoRightButton.BorderColor = System.Drawing.Color.LightGray;
            this.GoRightButton.BorderRadius = 5;
            this.GoRightButton.BorderThickness = 1;
            this.GoRightButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.GoRightButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.GoRightButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.GoRightButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.GoRightButton.FillColor = System.Drawing.Color.Transparent;
            this.GoRightButton.Font = new System.Drawing.Font("Lexend Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GoRightButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(28)))), ((int)(((byte)(35)))));
            this.GoRightButton.Location = new System.Drawing.Point(876, 18);
            this.GoRightButton.Name = "GoRightButton";
            this.GoRightButton.PressedColor = System.Drawing.Color.White;
            this.GoRightButton.Size = new System.Drawing.Size(29, 29);
            this.GoRightButton.TabIndex = 12;
            this.GoRightButton.Click += new System.EventHandler(this.GoRightButton_Click);
            // 
            // Inventory_Pagination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.PaginationPageNumber);
            this.Controls.Add(this.GoleftButton);
            this.Controls.Add(this.GoRightButton);
            this.Name = "Inventory_Pagination";
            this.Size = new System.Drawing.Size(920, 55);
            this.Load += new System.EventHandler(this.Inventory_Pagination_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PaginationPageNumber;
        private Guna.UI2.WinForms.Guna2Button GoleftButton;
        private Guna.UI2.WinForms.Guna2Button GoRightButton;
    }
}
