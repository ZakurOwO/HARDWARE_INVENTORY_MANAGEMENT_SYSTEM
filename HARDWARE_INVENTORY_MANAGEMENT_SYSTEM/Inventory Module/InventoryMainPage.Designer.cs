namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    partial class InventoryMainPage
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
            this.inventoryTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryTopBar();
            this.inventoryList_Panel1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryList_Panel();
            this.SuspendLayout();
            // 
            // inventoryTopBar1
            // 
            this.inventoryTopBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.inventoryTopBar1.Location = new System.Drawing.Point(0, 0);
            this.inventoryTopBar1.Name = "inventoryTopBar1";
            this.inventoryTopBar1.Size = new System.Drawing.Size(975, 70);
            this.inventoryTopBar1.TabIndex = 0;
            // 
            // inventoryList_Panel1
            // 
            this.inventoryList_Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.inventoryList_Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inventoryList_Panel1.Location = new System.Drawing.Point(0, 64);
            this.inventoryList_Panel1.Name = "inventoryList_Panel1";
            this.inventoryList_Panel1.Size = new System.Drawing.Size(975, 656);
            this.inventoryList_Panel1.TabIndex = 1;
            // 
            // InventoryMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.inventoryList_Panel1);
            this.Controls.Add(this.inventoryTopBar1);
            this.Name = "InventoryMainPage";
            this.Size = new System.Drawing.Size(975, 720);
            this.ResumeLayout(false);

        }

        #endregion

        private InventoryTopBar inventoryTopBar1;
        private InventoryList_Panel inventoryList_Panel1;
    }
}
