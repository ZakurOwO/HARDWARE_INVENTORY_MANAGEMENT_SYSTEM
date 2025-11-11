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
            this.pageNumber2 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module.PageNumber();
            this.dataGridTable1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.DataGridTable();
            this.addCustomerDetailsButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles.AddCustomerDetailsButton();
            this.searchField1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.SearchField();
            this.customerTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.CustomerTopBar();
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
            this.guna2Panel1.Controls.Add(this.pageNumber2);
            this.guna2Panel1.Controls.Add(this.dataGridTable1);
            this.guna2Panel1.Controls.Add(this.addCustomerDetailsButton1);
            this.guna2Panel1.Controls.Add(this.searchField1);
            this.guna2Panel1.Location = new System.Drawing.Point(22, 74);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(920, 568);
            this.guna2Panel1.TabIndex = 1;
            // 
            // pageNumber2
            // 
            this.pageNumber2.BackColor = System.Drawing.Color.Transparent;
            this.pageNumber2.Location = new System.Drawing.Point(648, 521);
            this.pageNumber2.Name = "pageNumber2";
            this.pageNumber2.Size = new System.Drawing.Size(269, 44);
            this.pageNumber2.TabIndex = 9;
            // 
            // dataGridTable1
            // 
            this.dataGridTable1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.dataGridTable1.Location = new System.Drawing.Point(63, 78);
            this.dataGridTable1.Name = "dataGridTable1";
            this.dataGridTable1.Size = new System.Drawing.Size(795, 442);
            this.dataGridTable1.TabIndex = 7;
            this.dataGridTable1.Load += new System.EventHandler(this.dataGridTable1_Load);
            // 
            // addCustomerDetailsButton1
            // 
            this.addCustomerDetailsButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.addCustomerDetailsButton1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.addCustomerDetailsButton1.Location = new System.Drawing.Point(706, 7);
            this.addCustomerDetailsButton1.Name = "addCustomerDetailsButton1";
            this.addCustomerDetailsButton1.Size = new System.Drawing.Size(196, 44);
            this.addCustomerDetailsButton1.TabIndex = 4;
            // 
            // searchField1
            // 
            this.searchField1.BackColor = System.Drawing.Color.Transparent;
            this.searchField1.Location = new System.Drawing.Point(22, 7);
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
            // CustomerMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.customerTopBar1);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "CustomerMainPage";
            this.Size = new System.Drawing.Size(965, 680);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private UserControlFiles.AddCustomerDetailsButton addCustomerDetailsButton1;
        private Accounts_Module.SearchField searchField1;
        private CustomerTopBar customerTopBar1;
        private DataGridTable dataGridTable1;
        private PageNumber pageNumber2;
    }
}
