namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    partial class CustomerMainPage
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
            this.inventoryFilter_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryFilter_Button();
            this.pageNumber2 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module.PageNumber();
            this.dataGridTable1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.DataGridTable();
            this.searchField1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.SearchField();
            this.customerTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.CustomerTopBar();
            this.btnMainButtonIcon = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Silver;
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.btnMainButtonIcon);
            this.guna2Panel1.Controls.Add(this.inventoryFilter_Button1);
            this.guna2Panel1.Controls.Add(this.pageNumber2);
            this.guna2Panel1.Controls.Add(this.dataGridTable1);
            this.guna2Panel1.Controls.Add(this.searchField1);
            this.guna2Panel1.Location = new System.Drawing.Point(22, 74);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(920, 617);
            this.guna2Panel1.TabIndex = 1;
            // 
            // inventoryFilter_Button1
            // 
            this.inventoryFilter_Button1.BackColor = System.Drawing.Color.Transparent;
            this.inventoryFilter_Button1.Location = new System.Drawing.Point(319, 13);
            this.inventoryFilter_Button1.Name = "inventoryFilter_Button1";
            this.inventoryFilter_Button1.Size = new System.Drawing.Size(102, 43);
            this.inventoryFilter_Button1.TabIndex = 10;
            // 
            // pageNumber2
            // 
            this.pageNumber2.BackColor = System.Drawing.Color.Transparent;
            this.pageNumber2.Location = new System.Drawing.Point(671, 573);
            this.pageNumber2.Name = "pageNumber2";
            this.pageNumber2.Size = new System.Drawing.Size(246, 44);
            this.pageNumber2.TabIndex = 9;
            // 
            // dataGridTable1
            // 
            this.dataGridTable1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.dataGridTable1.Location = new System.Drawing.Point(16, 78);
            this.dataGridTable1.Name = "dataGridTable1";
            this.dataGridTable1.Size = new System.Drawing.Size(884, 489);
            this.dataGridTable1.TabIndex = 7;
            this.dataGridTable1.Load += new System.EventHandler(this.dataGridTable1_Load);
            // 
            // searchField1
            // 
            this.searchField1.BackColor = System.Drawing.Color.Transparent;
            this.searchField1.Location = new System.Drawing.Point(16, 11);
            this.searchField1.Name = "searchField1";
            this.searchField1.PromptMessage = null;
            this.searchField1.Size = new System.Drawing.Size(299, 44);
            this.searchField1.TabIndex = 3;
            // 
            // customerTopBar1
            // 
            this.customerTopBar1.BackColor = System.Drawing.Color.White;
            this.customerTopBar1.Location = new System.Drawing.Point(0, 3);
            this.customerTopBar1.Name = "customerTopBar1";
            this.customerTopBar1.Size = new System.Drawing.Size(965, 69);
            this.customerTopBar1.TabIndex = 2;
            this.customerTopBar1.Load += new System.EventHandler(this.customerTopBar1_Load);
            // 
            // btnMainButtonIcon
            // 
            this.btnMainButtonIcon.BorderRadius = 6;
            this.btnMainButtonIcon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMainButtonIcon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMainButtonIcon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMainButtonIcon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMainButtonIcon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.btnMainButtonIcon.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainButtonIcon.ForeColor = System.Drawing.Color.White;
            this.btnMainButtonIcon.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Add_User;
            this.btnMainButtonIcon.Location = new System.Drawing.Point(749, 16);
            this.btnMainButtonIcon.Name = "btnMainButtonIcon";
            this.btnMainButtonIcon.Size = new System.Drawing.Size(151, 40);
            this.btnMainButtonIcon.TabIndex = 12;
            this.btnMainButtonIcon.Text = "Add Customer";
            // 
            // CustomerMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.customerTopBar1);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "CustomerMainPage";
            this.Size = new System.Drawing.Size(965, 710);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Accounts_Module.SearchField searchField1;
        private CustomerTopBar customerTopBar1;
        private DataGridTable dataGridTable1;
        private PageNumber pageNumber2;
        private Inventory_Module.InventoryFilter_Button inventoryFilter_Button1;
        private Guna.UI2.WinForms.Guna2Button btnMainButtonIcon;
    }
}
