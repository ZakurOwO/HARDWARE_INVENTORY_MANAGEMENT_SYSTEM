namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    partial class DeliveriesSlideButtons
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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDeliveries = new Guna.UI2.WinForms.Guna2Button();
            this.btnVehicles = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.Silver;
            this.guna2Panel1.BorderRadius = 10;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.btnDeliveries);
            this.guna2Panel1.Controls.Add(this.btnVehicles);
            this.guna2Panel1.Location = new System.Drawing.Point(2, 3);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(286, 38);
            this.guna2Panel1.TabIndex = 2;
            // 
            // btnDeliveries
            // 
            this.btnDeliveries.BorderRadius = 5;
            this.btnDeliveries.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDeliveries.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDeliveries.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDeliveries.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDeliveries.FillColor = System.Drawing.Color.White;
            this.btnDeliveries.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeliveries.ForeColor = System.Drawing.Color.Black;
            this.btnDeliveries.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.open_box_add;
            this.btnDeliveries.ImageOffset = new System.Drawing.Point(-1, 0);
            this.btnDeliveries.Location = new System.Drawing.Point(10, 4);
            this.btnDeliveries.Name = "btnDeliveries";
            this.btnDeliveries.Size = new System.Drawing.Size(130, 30);
            this.btnDeliveries.TabIndex = 18;
            this.btnDeliveries.Text = "Deliveries";
            this.btnDeliveries.Click += new System.EventHandler(this.btnDeliveries_Click);
            // 
            // btnVehicles
            // 
            this.btnVehicles.BorderRadius = 5;
            this.btnVehicles.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnVehicles.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnVehicles.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnVehicles.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnVehicles.FillColor = System.Drawing.Color.White;
            this.btnVehicles.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVehicles.ForeColor = System.Drawing.Color.Black;
            this.btnVehicles.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Deliveries;
            this.btnVehicles.ImageOffset = new System.Drawing.Point(-1, 0);
            this.btnVehicles.Location = new System.Drawing.Point(147, 4);
            this.btnVehicles.Name = "btnVehicles";
            this.btnVehicles.Size = new System.Drawing.Size(130, 30);
            this.btnVehicles.TabIndex = 17;
            this.btnVehicles.Text = "Vehicles";
            this.btnVehicles.Click += new System.EventHandler(this.btnVehicles_Click);
            // 
            // DeliveriesSlideButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.guna2Panel1);
            this.DoubleBuffered = true;
            this.Name = "DeliveriesSlideButtons";
            this.Size = new System.Drawing.Size(296, 46);
            this.Load += new System.EventHandler(this.DeliveriesSlideButtons_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnVehicles;
        private Guna.UI2.WinForms.Guna2Button btnDeliveries;
    }
}
