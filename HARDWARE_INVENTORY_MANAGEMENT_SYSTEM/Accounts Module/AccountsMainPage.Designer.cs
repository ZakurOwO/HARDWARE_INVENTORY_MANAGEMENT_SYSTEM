﻿namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.accountsTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.AccountsTopBar();
            this.addNewUserButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.AddNewUserButton();
            this.userAccountsPanel1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.UserAccountsPanel();
            this.searchField1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.SearchField();
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
            this.guna2Panel1.Controls.Add(this.searchField1);
            this.guna2Panel1.Controls.Add(this.userAccountsPanel1);
            this.guna2Panel1.Controls.Add(this.addNewUserButton1);
            this.guna2Panel1.Location = new System.Drawing.Point(19, 73);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(915, 580);
            this.guna2Panel1.TabIndex = 0;
            // 
            // accountsTopBar1
            // 
            this.accountsTopBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.accountsTopBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.accountsTopBar1.Location = new System.Drawing.Point(0, 0);
            this.accountsTopBar1.Name = "accountsTopBar1";
            this.accountsTopBar1.Size = new System.Drawing.Size(960, 69);
            this.accountsTopBar1.TabIndex = 0;
            // 
            // addNewUserButton1
            // 
            this.addNewUserButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addNewUserButton1.BackColor = System.Drawing.Color.Transparent;
            this.addNewUserButton1.Location = new System.Drawing.Point(749, 23);
            this.addNewUserButton1.Name = "addNewUserButton1";
            this.addNewUserButton1.Size = new System.Drawing.Size(147, 47);
            this.addNewUserButton1.TabIndex = 1;
            // 
            // userAccountsPanel1
            // 
            this.userAccountsPanel1.BackColor = System.Drawing.Color.Transparent;
            this.userAccountsPanel1.Location = new System.Drawing.Point(26, 84);
            this.userAccountsPanel1.Name = "userAccountsPanel1";
            this.userAccountsPanel1.Size = new System.Drawing.Size(590, 128);
            this.userAccountsPanel1.TabIndex = 2;
            // 
            // searchField1
            // 
            this.searchField1.BackColor = System.Drawing.Color.Transparent;
            this.searchField1.Location = new System.Drawing.Point(27, 23);
            this.searchField1.Name = "searchField1";
            this.searchField1.Size = new System.Drawing.Size(299, 54);
            this.searchField1.TabIndex = 3;
            // 
            // AccountsMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.accountsTopBar1);
            this.Name = "AccountsMainPage";
            this.Size = new System.Drawing.Size(960, 680);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AccountsTopBar accountsTopBar1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private AddNewUserButton addNewUserButton1;
        private UserAccountsPanel userAccountsPanel1;
        private SearchField searchField1;
    }
}
