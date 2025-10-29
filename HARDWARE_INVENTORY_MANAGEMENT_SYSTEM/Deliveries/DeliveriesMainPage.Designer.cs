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
            this.add_New_Truck_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries.Add_New_Truck_Button();
            this.searchField1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.SearchField();
            this.vehiclesInfoBox1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries.VehiclesInfoBox();
            this.deliveriesTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries.DeliveriesTopBar();
            this.deliveriesTable1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.DeliveriesTable();
            this.deliveriesSlideButtons1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries.DeliveriesSlideButtons();
            this.SuspendLayout();
            // 
            // add_New_Truck_Button1
            // 
            this.add_New_Truck_Button1.Location = new System.Drawing.Point(725, 98);
            this.add_New_Truck_Button1.Name = "add_New_Truck_Button1";
            this.add_New_Truck_Button1.Size = new System.Drawing.Size(196, 44);
            this.add_New_Truck_Button1.TabIndex = 5;
            // 
            // searchField1
            // 
            this.searchField1.BackColor = System.Drawing.Color.Transparent;
            this.searchField1.Location = new System.Drawing.Point(27, 98);
            this.searchField1.Name = "searchField1";
            this.searchField1.Size = new System.Drawing.Size(295, 44);
            this.searchField1.TabIndex = 4;
            // 
            // vehiclesInfoBox1
            // 
            this.vehiclesInfoBox1.BackColor = System.Drawing.Color.White;
            this.vehiclesInfoBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.vehiclesInfoBox1.ForeColor = System.Drawing.Color.LawnGreen;
            this.vehiclesInfoBox1.Location = new System.Drawing.Point(80, 151);
            this.vehiclesInfoBox1.Name = "vehiclesInfoBox1";
            this.vehiclesInfoBox1.Size = new System.Drawing.Size(801, 525);
            this.vehiclesInfoBox1.TabIndex = 2;
            // 
            // deliveriesTopBar1
            // 
            this.deliveriesTopBar1.BackColor = System.Drawing.Color.White;
            this.deliveriesTopBar1.Location = new System.Drawing.Point(0, 0);
            this.deliveriesTopBar1.Name = "deliveriesTopBar1";
            this.deliveriesTopBar1.Size = new System.Drawing.Size(965, 60);
            this.deliveriesTopBar1.TabIndex = 0;
            // 
            // deliveriesTable1
            // 
            this.deliveriesTable1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.deliveriesTable1.Location = new System.Drawing.Point(-3, -3);
            this.deliveriesTable1.Name = "deliveriesTable1";
            this.deliveriesTable1.Size = new System.Drawing.Size(965, 680);
            this.deliveriesTable1.TabIndex = 6;
            // 
            // deliveriesSlideButtons1
            // 
            this.deliveriesSlideButtons1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.deliveriesSlideButtons1.Location = new System.Drawing.Point(368, 66);
            this.deliveriesSlideButtons1.Name = "deliveriesSlideButtons1";
            this.deliveriesSlideButtons1.Size = new System.Drawing.Size(267, 37);
            this.deliveriesSlideButtons1.TabIndex = 7;
            // 
            // DeliveriesMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.deliveriesSlideButtons1);
            this.Controls.Add(this.add_New_Truck_Button1);
            this.Controls.Add(this.searchField1);
            this.Controls.Add(this.vehiclesInfoBox1);
            this.Controls.Add(this.deliveriesTopBar1);
            this.Controls.Add(this.deliveriesTable1);
            this.Name = "DeliveriesMainPage";
            this.Size = new System.Drawing.Size(965, 680);
            this.ResumeLayout(false);

        }

        #endregion

        private DeliveriesTopBar deliveriesTopBar1;
        private VehiclesInfoBox vehiclesInfoBox1;
        private Accounts_Module.SearchField searchField1;
        private Add_New_Truck_Button add_New_Truck_Button1;
        private DeliveriesTable deliveriesTable1;
        private DeliveriesSlideButtons deliveriesSlideButtons1;
    }
}
