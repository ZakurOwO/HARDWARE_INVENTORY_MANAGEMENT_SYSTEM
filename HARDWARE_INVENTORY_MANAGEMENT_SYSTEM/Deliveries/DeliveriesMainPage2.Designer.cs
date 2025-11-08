namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    partial class DeliveriesMainPage2
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
            this.inventoryFilter_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryFilter_Button();
            this.searchField1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.SearchField();
            this.vehiclesInfoBox1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries.VehiclesInfoBox();
            this.add_New_Truck_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries.Add_New_Truck_Button();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // inventoryFilter_Button1
            // 
            this.inventoryFilter_Button1.BackColor = System.Drawing.Color.Transparent;
            this.inventoryFilter_Button1.Location = new System.Drawing.Point(338, 21);
            this.inventoryFilter_Button1.Name = "inventoryFilter_Button1";
            this.inventoryFilter_Button1.Size = new System.Drawing.Size(102, 43);
            this.inventoryFilter_Button1.TabIndex = 6;
            // 
            // searchField1
            // 
            this.searchField1.BackColor = System.Drawing.Color.Transparent;
            this.searchField1.Location = new System.Drawing.Point(25, 18);
            this.searchField1.Name = "searchField1";
            this.searchField1.PromptMessage = "Search Deliveries";
            this.searchField1.Size = new System.Drawing.Size(299, 54);
            this.searchField1.TabIndex = 5;
            // 
            // vehiclesInfoBox1
            // 
            this.vehiclesInfoBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.vehiclesInfoBox1.ForeColor = System.Drawing.Color.LawnGreen;
            this.vehiclesInfoBox1.Location = new System.Drawing.Point(25, 78);
            this.vehiclesInfoBox1.Name = "vehiclesInfoBox1";
            this.vehiclesInfoBox1.Size = new System.Drawing.Size(801, 525);
            this.vehiclesInfoBox1.TabIndex = 7;
            // 
            // add_New_Truck_Button1
            // 
            this.add_New_Truck_Button1.Location = new System.Drawing.Point(714, 19);
            this.add_New_Truck_Button1.Name = "add_New_Truck_Button1";
            this.add_New_Truck_Button1.Size = new System.Drawing.Size(173, 44);
            this.add_New_Truck_Button1.TabIndex = 8;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.LightGray;
            this.guna2Panel1.BorderRadius = 10;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.vehiclesInfoBox1);
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(935, 570);
            this.guna2Panel1.TabIndex = 9;
            // 
            // DeliveriesMainPage2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.add_New_Truck_Button1);
            this.Controls.Add(this.inventoryFilter_Button1);
            this.Controls.Add(this.searchField1);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "DeliveriesMainPage2";
            this.Size = new System.Drawing.Size(935, 550);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Inventory_Module.InventoryFilter_Button inventoryFilter_Button1;
        private Accounts_Module.SearchField searchField1;
        private VehiclesInfoBox vehiclesInfoBox1;
        private Add_New_Truck_Button add_New_Truck_Button1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}
