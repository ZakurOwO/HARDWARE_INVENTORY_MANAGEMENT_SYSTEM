namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    partial class Pagination_Deliveries
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
            this.pagination_panel = new System.Windows.Forms.Panel();
            this.PaginationPageNumber = new System.Windows.Forms.Label();
            this.GoleftButton = new Guna.UI2.WinForms.Guna2Button();
            this.GoRightButton = new Guna.UI2.WinForms.Guna2Button();
            this.pagination_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pagination_panel
            // 
            this.pagination_panel.Controls.Add(this.PaginationPageNumber);
            this.pagination_panel.Controls.Add(this.GoleftButton);
            this.pagination_panel.Controls.Add(this.GoRightButton);
            this.pagination_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagination_panel.Location = new System.Drawing.Point(0, 0);
            this.pagination_panel.Name = "pagination_panel";
            this.pagination_panel.Size = new System.Drawing.Size(920, 34);
            this.pagination_panel.TabIndex = 0;
            this.pagination_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.pagination_panel_Paint);
            // 
            // PaginationPageNumber
            // 
            this.PaginationPageNumber.AutoSize = true;
            this.PaginationPageNumber.Font = new System.Drawing.Font("Lexend Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PaginationPageNumber.Location = new System.Drawing.Point(367, 8);
            this.PaginationPageNumber.Name = "PaginationPageNumber";
            this.PaginationPageNumber.Size = new System.Drawing.Size(153, 17);
            this.PaginationPageNumber.TabIndex = 19;
            this.PaginationPageNumber.Text = "Showing 1 out of 40 records";
            this.PaginationPageNumber.Click += new System.EventHandler(this.PaginationPageNumber_Click);
            // 
            // GoleftButton
            // 
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
            this.GoleftButton.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.direction_right_2;
            this.GoleftButton.Location = new System.Drawing.Point(797, 5);
            this.GoleftButton.Name = "GoleftButton";
            this.GoleftButton.PressedColor = System.Drawing.Color.White;
            this.GoleftButton.Size = new System.Drawing.Size(23, 23);
            this.GoleftButton.TabIndex = 18;
            this.GoleftButton.Text = "<";
            this.GoleftButton.Click += new System.EventHandler(this.GoleftButton_Click);
            // 
            // GoRightButton
            // 
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
            this.GoRightButton.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.direction_right_011;
            this.GoRightButton.Location = new System.Drawing.Point(832, 5);
            this.GoRightButton.Name = "GoRightButton";
            this.GoRightButton.PressedColor = System.Drawing.Color.White;
            this.GoRightButton.Size = new System.Drawing.Size(23, 23);
            this.GoRightButton.TabIndex = 17;
            this.GoRightButton.Click += new System.EventHandler(this.GoRightButton_Click);
            // 
            // Pagination_Deliveries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pagination_panel);
            this.Name = "Pagination_Deliveries";
            this.Size = new System.Drawing.Size(920, 34);
            this.pagination_panel.ResumeLayout(false);
            this.pagination_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pagination_panel;
        private System.Windows.Forms.Label PaginationPageNumber;
        private Guna.UI2.WinForms.Guna2Button GoleftButton;
        private Guna.UI2.WinForms.Guna2Button GoRightButton;
    }
}
