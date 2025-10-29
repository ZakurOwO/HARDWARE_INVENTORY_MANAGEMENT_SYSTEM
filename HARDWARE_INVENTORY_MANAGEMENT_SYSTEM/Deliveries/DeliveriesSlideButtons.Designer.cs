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
            this.PanelTabs = new Krypton.Toolkit.KryptonPanel();
            this.BTNDeliveries = new Krypton.Toolkit.KryptonButton();
            this.BTNVehicles = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.PanelTabs)).BeginInit();
            this.PanelTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelTabs
            // 
            this.PanelTabs.Controls.Add(this.BTNDeliveries);
            this.PanelTabs.Controls.Add(this.BTNVehicles);
            this.PanelTabs.Location = new System.Drawing.Point(0, 3);
            this.PanelTabs.Name = "PanelTabs";
            this.PanelTabs.Size = new System.Drawing.Size(267, 37);
            this.PanelTabs.StateCommon.Color1 = System.Drawing.Color.White;
            this.PanelTabs.StateCommon.Color2 = System.Drawing.Color.White;
            this.PanelTabs.TabIndex = 0;
            this.PanelTabs.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelTabs_Paint);
            // 
            // BTNDeliveries
            // 
            this.BTNDeliveries.Location = new System.Drawing.Point(138, 5);
            this.BTNDeliveries.Name = "BTNDeliveries";
            this.BTNDeliveries.PaletteMode = Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.BTNDeliveries.Size = new System.Drawing.Size(126, 25);
            this.BTNDeliveries.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.BTNDeliveries.StateCommon.Back.Color2 = System.Drawing.Color.White;
            this.BTNDeliveries.StateCommon.Border.Color1 = System.Drawing.Color.DimGray;
            this.BTNDeliveries.StateCommon.Border.Color2 = System.Drawing.Color.DimGray;
            this.BTNDeliveries.StateCommon.Border.Rounding = 4F;
            this.BTNDeliveries.StateCommon.Border.Width = 1;
            this.BTNDeliveries.StateCommon.Content.LongText.Font = new System.Drawing.Font("Lexend Light", 8.25F);
            this.BTNDeliveries.TabIndex = 1;
            this.BTNDeliveries.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.BTNDeliveries.Values.Text = "Deliveries";
            this.BTNDeliveries.Click += new System.EventHandler(this.BTNDeliveries_Click);
            // 
            // BTNVehicles
            // 
            this.BTNVehicles.Location = new System.Drawing.Point(4, 5);
            this.BTNVehicles.Name = "BTNVehicles";
            this.BTNVehicles.PaletteMode = Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.BTNVehicles.Size = new System.Drawing.Size(126, 25);
            this.BTNVehicles.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.BTNVehicles.StateCommon.Border.Rounding = 4F;
            this.BTNVehicles.StateCommon.Border.Width = 1;
            this.BTNVehicles.StateCommon.Content.LongText.Color1 = System.Drawing.Color.DodgerBlue;
            this.BTNVehicles.StateCommon.Content.LongText.Color2 = System.Drawing.Color.DodgerBlue;
            this.BTNVehicles.StateCommon.Content.LongText.Font = new System.Drawing.Font("Lexend Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNVehicles.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.DodgerBlue;
            this.BTNVehicles.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.DodgerBlue;
            this.BTNVehicles.TabIndex = 0;
            this.BTNVehicles.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.BTNVehicles.Values.Text = "Vehicles";
            this.BTNVehicles.Click += new System.EventHandler(this.BTNVehicles_Click);
            // 
            // DeliveriesSlideButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.PanelTabs);
            this.DoubleBuffered = true;
            this.Name = "DeliveriesSlideButtons";
            this.Size = new System.Drawing.Size(267, 37);
            this.Load += new System.EventHandler(this.DeliveriesSlideButtons_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PanelTabs)).EndInit();
            this.PanelTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel PanelTabs;
        private Krypton.Toolkit.KryptonButton BTNVehicles;
        private Krypton.Toolkit.KryptonButton BTNDeliveries;
    }
}
