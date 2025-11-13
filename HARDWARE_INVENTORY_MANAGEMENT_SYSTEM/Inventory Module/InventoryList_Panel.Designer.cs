namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    partial class InventoryList_Panel
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
            this.inventory_Pagination1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.Inventory_Pagination();
            this.inventoryList_Table1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryList_Table();
            this.inventory_SearchField1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.Inventory_SearchField();
            this.inventoryFilter_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryFilter_Button();
            this.add_ItemButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.Add_ItemButton();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.inventory_Pagination1);
            this.guna2Panel1.Controls.Add(this.inventoryList_Table1);
            this.guna2Panel1.Controls.Add(this.inventory_SearchField1);
            this.guna2Panel1.Controls.Add(this.inventoryFilter_Button1);
            this.guna2Panel1.Controls.Add(this.add_ItemButton1);
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(15, 14);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(920, 618);
            this.guna2Panel1.TabIndex = 0;
            // 
            // inventory_Pagination1
            // 
            this.inventory_Pagination1.BackColor = System.Drawing.Color.White;
            this.inventory_Pagination1.Location = new System.Drawing.Point(-2, 560);
            this.inventory_Pagination1.Name = "inventory_Pagination1";
            this.inventory_Pagination1.Size = new System.Drawing.Size(920, 55);
            this.inventory_Pagination1.TabIndex = 4;
            // 
            // inventoryList_Table1
            // 
            this.inventoryList_Table1.BackColor = System.Drawing.Color.Transparent;
            this.inventoryList_Table1.Location = new System.Drawing.Point(18, 68);
            this.inventoryList_Table1.Name = "inventoryList_Table1";
            this.inventoryList_Table1.Size = new System.Drawing.Size(886, 497);
            this.inventoryList_Table1.TabIndex = 3;
            // 
            // inventory_SearchField1
            // 
            this.inventory_SearchField1.BackColor = System.Drawing.Color.Transparent;
            this.inventory_SearchField1.Location = new System.Drawing.Point(17, 18);
            this.inventory_SearchField1.Name = "inventory_SearchField1";
            this.inventory_SearchField1.Size = new System.Drawing.Size(336, 47);
            this.inventory_SearchField1.TabIndex = 2;
            // 
            // inventoryFilter_Button1
            // 
            this.inventoryFilter_Button1.BackColor = System.Drawing.Color.Transparent;
            this.inventoryFilter_Button1.Location = new System.Drawing.Point(365, 18);
            this.inventoryFilter_Button1.Name = "inventoryFilter_Button1";
            this.inventoryFilter_Button1.Size = new System.Drawing.Size(102, 43);
            this.inventoryFilter_Button1.TabIndex = 1;
            // 
            // add_ItemButton1
            // 
            this.add_ItemButton1.BackColor = System.Drawing.Color.Transparent;
            this.add_ItemButton1.Location = new System.Drawing.Point(777, 13);
            this.add_ItemButton1.Name = "add_ItemButton1";
            this.add_ItemButton1.Size = new System.Drawing.Size(127, 47);
            this.add_ItemButton1.TabIndex = 0;
            // 
            // InventoryList_Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.guna2Panel1);
            this.Name = "InventoryList_Panel";
            this.Size = new System.Drawing.Size(975, 656);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Add_ItemButton add_ItemButton1;
        private Inventory_SearchField inventory_SearchField1;
        private InventoryFilter_Button inventoryFilter_Button1;
        private Inventory_Pagination inventory_Pagination1;
        private InventoryList_Table inventoryList_Table1;
    }
}
