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
            this.deliveriesTable1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.DeliveriesTable();
            this.deliveriesTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries.DeliveriesTopBar();
            this.deliveriesSlideButtons2 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries.DeliveriesSlideButtons();
            this.searchField1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.SearchField();
            this.deliveriesTables1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries.DeliveriesTables();
            this.SuspendLayout();
            // 
            // deliveriesTable1
            // 
            this.deliveriesTable1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.deliveriesTable1.Location = new System.Drawing.Point(-2, 0);
            this.deliveriesTable1.Name = "deliveriesTable1";
            this.deliveriesTable1.Size = new System.Drawing.Size(965, 680);
            this.deliveriesTable1.TabIndex = 12;
            // 
            // deliveriesTopBar1
            // 
            this.deliveriesTopBar1.BackColor = System.Drawing.Color.White;
            this.deliveriesTopBar1.Location = new System.Drawing.Point(1, 3);
            this.deliveriesTopBar1.Name = "deliveriesTopBar1";
            this.deliveriesTopBar1.Size = new System.Drawing.Size(965, 60);
            this.deliveriesTopBar1.TabIndex = 8;
            // 
            // deliveriesSlideButtons2
            // 
            this.deliveriesSlideButtons2.BackColor = System.Drawing.Color.White;
            this.deliveriesSlideButtons2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.deliveriesSlideButtons2.Location = new System.Drawing.Point(362, 84);
            this.deliveriesSlideButtons2.Name = "deliveriesSlideButtons2";
            this.deliveriesSlideButtons2.Size = new System.Drawing.Size(267, 37);
            this.deliveriesSlideButtons2.TabIndex = 13;
            this.deliveriesSlideButtons2.Load += new System.EventHandler(this.deliveriesSlideButtons2_Load);
            // 
            // searchField1
            // 
            this.searchField1.BackColor = System.Drawing.Color.White;
            this.searchField1.Location = new System.Drawing.Point(25, 121);
            this.searchField1.Name = "searchField1";
            this.searchField1.Size = new System.Drawing.Size(295, 44);
            this.searchField1.TabIndex = 10;
            // 
            // deliveriesTables1
            // 
            this.deliveriesTables1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.deliveriesTables1.Location = new System.Drawing.Point(98, 224);
            this.deliveriesTables1.Name = "deliveriesTables1";
            this.deliveriesTables1.Size = new System.Drawing.Size(790, 395);
            this.deliveriesTables1.TabIndex = 14;
            // 
            // DeliveriesMainPage2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.deliveriesTables1);
            this.Controls.Add(this.deliveriesSlideButtons2);
            this.Controls.Add(this.searchField1);
            this.Controls.Add(this.deliveriesTopBar1);
            this.Controls.Add(this.deliveriesTable1);
            this.Name = "DeliveriesMainPage2";
            this.Size = new System.Drawing.Size(965, 680);
            this.ResumeLayout(false);

        }

        #endregion

        private DeliveriesTable deliveriesTable1;
        private DeliveriesTopBar deliveriesTopBar1;
        private DeliveriesSlideButtons deliveriesSlideButtons2;
        private Accounts_Module.SearchField searchField1;
        private DeliveriesTables deliveriesTables1;
    }
}
