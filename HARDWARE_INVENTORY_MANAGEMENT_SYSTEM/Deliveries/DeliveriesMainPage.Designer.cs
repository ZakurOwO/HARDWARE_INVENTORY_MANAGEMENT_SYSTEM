namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    partial class DeliveriesMainPage
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
            this.pnlPanelContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.deliveriesSlideButtons1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries.DeliveriesSlideButtons();
            this.deliveriesTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries.DeliveriesTopBar();
            this.SuspendLayout();
            // 
            // pnlPanelContainer
            // 
            this.pnlPanelContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlPanelContainer.BorderColor = System.Drawing.Color.LightGray;
            this.pnlPanelContainer.BorderRadius = 13;
            this.pnlPanelContainer.BorderThickness = 1;
            this.pnlPanelContainer.FillColor = System.Drawing.Color.White;
            this.pnlPanelContainer.Location = new System.Drawing.Point(11, 92);
            this.pnlPanelContainer.Name = "pnlPanelContainer";
            this.pnlPanelContainer.Size = new System.Drawing.Size(935, 625);
            this.pnlPanelContainer.TabIndex = 8;
            // 
            // deliveriesSlideButtons1
            // 
            this.deliveriesSlideButtons1.BackColor = System.Drawing.Color.Transparent;
            this.deliveriesSlideButtons1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.deliveriesSlideButtons1.Location = new System.Drawing.Point(350, 46);
            this.deliveriesSlideButtons1.Name = "deliveriesSlideButtons1";
            this.deliveriesSlideButtons1.Size = new System.Drawing.Size(290, 43);
            this.deliveriesSlideButtons1.TabIndex = 7;
            // 
            // deliveriesTopBar1
            // 
            this.deliveriesTopBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.deliveriesTopBar1.Location = new System.Drawing.Point(0, -10);
            this.deliveriesTopBar1.Name = "deliveriesTopBar1";
            this.deliveriesTopBar1.Size = new System.Drawing.Size(965, 60);
            this.deliveriesTopBar1.TabIndex = 0;
            // 
            // DeliveriesMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pnlPanelContainer);
            this.Controls.Add(this.deliveriesSlideButtons1);
            this.Controls.Add(this.deliveriesTopBar1);
            this.Name = "DeliveriesMainPage";
            this.Size = new System.Drawing.Size(960, 720);
            this.Load += new System.EventHandler(this.DeliveriesMainPage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DeliveriesTopBar deliveriesTopBar1;
        private DeliveriesSlideButtons deliveriesSlideButtons1;
        private Guna.UI2.WinForms.Guna2Panel pnlPanelContainer;
    }
}
