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
            this.panelAddItem = new Guna.UI2.WinForms.Guna2Panel();
            this.addNewItem_Form1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.AddNewItem_Form();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnAddItem = new Guna.UI2.WinForms.Guna2Button();
            this.inventory_Pagination1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.Inventory_Pagination();
            this.inventoryList_Table1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryList_Table();
            this.inventory_SearchField1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.Inventory_SearchField();
            this.inventoryFilter_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryFilter_Button();
            this.inventoryList_Panel1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryList_Panel();
            this.inventoryTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryTopBar();
            this.panelAddItem.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAddItem
            // 
            this.panelAddItem.BackColor = System.Drawing.Color.Transparent;
            this.panelAddItem.BorderColor = System.Drawing.Color.DarkGray;
            this.panelAddItem.BorderRadius = 15;
            this.panelAddItem.BorderThickness = 1;
            this.panelAddItem.Controls.Add(this.addNewItem_Form1);
            this.panelAddItem.Location = new System.Drawing.Point(74, 18);
            this.panelAddItem.Name = "panelAddItem";
            this.panelAddItem.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(10);
            this.panelAddItem.Size = new System.Drawing.Size(600, 505);
            this.panelAddItem.TabIndex = 2;
            this.panelAddItem.Visible = false;
            // 
            // addNewItem_Form1
            // 
            this.addNewItem_Form1.BackColor = System.Drawing.Color.White;
            this.addNewItem_Form1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addNewItem_Form1.Location = new System.Drawing.Point(0, 0);
            this.addNewItem_Form1.Name = "addNewItem_Form1";
            this.addNewItem_Form1.Size = new System.Drawing.Size(600, 505);
            this.addNewItem_Form1.TabIndex = 0;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.panelAddItem);
            this.guna2Panel1.Controls.Add(this.btnAddItem);
            this.guna2Panel1.Controls.Add(this.inventory_Pagination1);
            this.guna2Panel1.Controls.Add(this.inventoryList_Table1);
            this.guna2Panel1.Controls.Add(this.inventory_SearchField1);
            this.guna2Panel1.Controls.Add(this.inventoryFilter_Button1);
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(15, 78);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(920, 618);
            this.guna2Panel1.TabIndex = 3;
            // 
            // btnAddItem
            // 
            this.btnAddItem.BorderRadius = 6;
            this.btnAddItem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddItem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddItem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddItem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddItem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.btnAddItem.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Vector;
            this.btnAddItem.Location = new System.Drawing.Point(783, 18);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(121, 38);
            this.btnAddItem.TabIndex = 5;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
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
            // inventoryList_Panel1
            // 
            this.inventoryList_Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.inventoryList_Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inventoryList_Panel1.Location = new System.Drawing.Point(0, 64);
            this.inventoryList_Panel1.Name = "inventoryList_Panel1";
            this.inventoryList_Panel1.Size = new System.Drawing.Size(975, 656);
            this.inventoryList_Panel1.TabIndex = 1;
            this.inventoryList_Panel1.Load += new System.EventHandler(this.inventoryList_Panel1_Load);
            // 
            // inventoryTopBar1
            // 
            this.inventoryTopBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.inventoryTopBar1.Location = new System.Drawing.Point(0, 0);
            this.inventoryTopBar1.Name = "inventoryTopBar1";
            this.inventoryTopBar1.Size = new System.Drawing.Size(975, 70);
            this.inventoryTopBar1.TabIndex = 0;
            // 
            // InventoryMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.inventoryList_Panel1);
            this.Controls.Add(this.inventoryTopBar1);
            this.Name = "InventoryMainPage";
            this.Size = new System.Drawing.Size(975, 720);
            this.panelAddItem.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private InventoryTopBar inventoryTopBar1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnAddItem;
        private Inventory_Pagination inventory_Pagination1;
        private InventoryList_Table inventoryList_Table1;
        private Inventory_SearchField inventory_SearchField1;
        private InventoryFilter_Button inventoryFilter_Button1;
        private InventoryList_Panel inventoryList_Panel1;
        private AddNewItem_Form addNewItem_Form1;
        public Guna.UI2.WinForms.Guna2Panel panelAddItem;
    }
}
