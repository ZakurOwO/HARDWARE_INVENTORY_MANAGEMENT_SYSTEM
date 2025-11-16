namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    partial class AccountsMainPage
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
            this.addNewUserButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.AddNewUserButton();
            this.searchField1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.SearchField();
            this.userAccountsPanel1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.UserAccountsPanel();
            this.userAccountsPanel2 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.UserAccountsPanel();
            this.inventoryFilter_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryFilter_Button();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addNewUserButton1
            // 
            this.addNewUserButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addNewUserButton1.BackColor = System.Drawing.Color.Transparent;
            this.addNewUserButton1.Location = new System.Drawing.Point(779, 51);
            this.addNewUserButton1.Name = "addNewUserButton1";
            this.addNewUserButton1.Size = new System.Drawing.Size(147, 47);
            this.addNewUserButton1.TabIndex = 1;
            // 
            // searchField1
            // 
            this.searchField1.BackColor = System.Drawing.Color.Transparent;
            this.searchField1.Location = new System.Drawing.Point(27, 51);
            this.searchField1.Name = "searchField1";
            this.searchField1.PromptMessage = "Search User";
            this.searchField1.Size = new System.Drawing.Size(299, 54);
            this.searchField1.TabIndex = 3;
            // 
            // userAccountsPanel1
            // 
            this.userAccountsPanel1._Name = "Richard Faulkerson";
            this.userAccountsPanel1.BackColor = System.Drawing.Color.Transparent;
            this.userAccountsPanel1.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.shield1;
            this.userAccountsPanel1.Location = new System.Drawing.Point(28, 100);
            this.userAccountsPanel1.Name = "userAccountsPanel1";
            this.userAccountsPanel1.Position = "Manager";
            this.userAccountsPanel1.Role = "Admin";
            this.userAccountsPanel1.Size = new System.Drawing.Size(284, 128);
            this.userAccountsPanel1.Status = "Active";
            this.userAccountsPanel1.TabIndex = 4;
            // 
            // userAccountsPanel2
            // 
            this.userAccountsPanel2._Name = "Dimpol Navarro";
            this.userAccountsPanel2.BackColor = System.Drawing.Color.Transparent;
            this.userAccountsPanel2.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.user1;
            this.userAccountsPanel2.Location = new System.Drawing.Point(318, 100);
            this.userAccountsPanel2.Name = "userAccountsPanel2";
            this.userAccountsPanel2.Position = "Sales Representative";
            this.userAccountsPanel2.Role = "User";
            this.userAccountsPanel2.Size = new System.Drawing.Size(284, 128);
            this.userAccountsPanel2.Status = "Active";
            this.userAccountsPanel2.TabIndex = 5;
            // 
            // inventoryFilter_Button1
            // 
            this.inventoryFilter_Button1.BackColor = System.Drawing.Color.Transparent;
            this.inventoryFilter_Button1.Location = new System.Drawing.Point(327, 53);
            this.inventoryFilter_Button1.Name = "inventoryFilter_Button1";
            this.inventoryFilter_Button1.Size = new System.Drawing.Size(102, 43);
            this.inventoryFilter_Button1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lexend SemiBold", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Accounts";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.inventoryFilter_Button1);
            this.guna2Panel1.Controls.Add(this.userAccountsPanel2);
            this.guna2Panel1.Controls.Add(this.userAccountsPanel1);
            this.guna2Panel1.Controls.Add(this.searchField1);
            this.guna2Panel1.Controls.Add(this.addNewUserButton1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(945, 580);
            this.guna2Panel1.TabIndex = 0;
            // 
            // AccountsMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.guna2Panel1);
            this.Name = "AccountsMainPage";
            this.Size = new System.Drawing.Size(945, 580);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AddNewUserButton addNewUserButton1;
        private SearchField searchField1;
        private UserAccountsPanel userAccountsPanel1;
        private UserAccountsPanel userAccountsPanel2;
        private Inventory_Module.InventoryFilter_Button inventoryFilter_Button1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}
