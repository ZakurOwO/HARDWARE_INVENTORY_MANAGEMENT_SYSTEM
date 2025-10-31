namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    partial class CartDetails
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
            this.panelContainer = new System.Windows.Forms.Panel();
            this.walkinOrDeliveryButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module.WalkinOrDeliveryButton();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelContainer.Location = new System.Drawing.Point(0, 64);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(295, 560);
            this.panelContainer.TabIndex = 1;
            // 
            // walkinOrDeliveryButton1
            // 
            this.walkinOrDeliveryButton1.BackColor = System.Drawing.Color.Transparent;
            this.walkinOrDeliveryButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.walkinOrDeliveryButton1.Location = new System.Drawing.Point(0, 0);
            this.walkinOrDeliveryButton1.Name = "walkinOrDeliveryButton1";
            this.walkinOrDeliveryButton1.Size = new System.Drawing.Size(295, 60);
            this.walkinOrDeliveryButton1.TabIndex = 2;
            // 
            // CartDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.walkinOrDeliveryButton1);
            this.Controls.Add(this.panelContainer);
            this.Name = "CartDetails";
            this.Size = new System.Drawing.Size(295, 624);
            this.Load += new System.EventHandler(this.CartDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelContainer;
        private WalkinOrDeliveryButton walkinOrDeliveryButton1;
    }
}
