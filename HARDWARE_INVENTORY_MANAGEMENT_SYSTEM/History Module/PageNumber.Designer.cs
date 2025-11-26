namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.History_Module
{
    partial class PageNumber
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
            this.NextBtnInHistory = new Guna.UI2.WinForms.Guna2Button();
            this.PrevBtnInHistory = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // NextBtnInHistory
            // 
            this.NextBtnInHistory.BackColor = System.Drawing.Color.White;
            this.NextBtnInHistory.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.direction_right_01;
            this.NextBtnInHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.NextBtnInHistory.BorderColor = System.Drawing.Color.LightGray;
            this.NextBtnInHistory.BorderRadius = 5;
            this.NextBtnInHistory.BorderThickness = 1;
            this.NextBtnInHistory.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.NextBtnInHistory.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.NextBtnInHistory.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.NextBtnInHistory.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.NextBtnInHistory.FillColor = System.Drawing.Color.Transparent;
            this.NextBtnInHistory.Font = new System.Drawing.Font("Lexend Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextBtnInHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(28)))), ((int)(((byte)(35)))));
            this.NextBtnInHistory.Location = new System.Drawing.Point(38, 0);
            this.NextBtnInHistory.Name = "NextBtnInHistory";
            this.NextBtnInHistory.Size = new System.Drawing.Size(29, 29);
            this.NextBtnInHistory.TabIndex = 10;
            // 
            // PrevBtnInHistory
            // 
            this.PrevBtnInHistory.BackColor = System.Drawing.Color.White;
            this.PrevBtnInHistory.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.direction_right_2;
            this.PrevBtnInHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PrevBtnInHistory.BorderColor = System.Drawing.Color.LightGray;
            this.PrevBtnInHistory.BorderRadius = 5;
            this.PrevBtnInHistory.BorderThickness = 1;
            this.PrevBtnInHistory.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.PrevBtnInHistory.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.PrevBtnInHistory.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.PrevBtnInHistory.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.PrevBtnInHistory.FillColor = System.Drawing.Color.Transparent;
            this.PrevBtnInHistory.Font = new System.Drawing.Font("Lexend Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrevBtnInHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(28)))), ((int)(((byte)(35)))));
            this.PrevBtnInHistory.Location = new System.Drawing.Point(3, 0);
            this.PrevBtnInHistory.Name = "PrevBtnInHistory";
            this.PrevBtnInHistory.Size = new System.Drawing.Size(29, 29);
            this.PrevBtnInHistory.TabIndex = 11;
            // 
            // PageNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.PrevBtnInHistory);
            this.Controls.Add(this.NextBtnInHistory);
            this.DoubleBuffered = true;
            this.Name = "PageNumber";
            this.Size = new System.Drawing.Size(72, 29);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button NextBtnInHistory;
        private Guna.UI2.WinForms.Guna2Button PrevBtnInHistory;
    }
}
