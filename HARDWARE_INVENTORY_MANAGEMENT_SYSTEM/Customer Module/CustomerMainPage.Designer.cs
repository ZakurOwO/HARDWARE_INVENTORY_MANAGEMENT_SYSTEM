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
            this.refreshbtn = new Guna.UI2.WinForms.Guna2Panel();
            this.refreshbtnn = new Guna.UI2.WinForms.Guna2Button();
            this.btnMainButtonIcon = new Guna.UI2.WinForms.Guna2Button();
            this.pcbBlurOverlay = new System.Windows.Forms.PictureBox();
            this.pageNumber2 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module.PageNumber();
            this.searchField1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.SearchField();
            this.dataGridTable1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.DataGridTable();
            this.customerTopBar1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.CustomerTopBar();
            this.refreshbtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbBlurOverlay)).BeginInit();
            this.SuspendLayout();
            // 
            // refreshbtn
            // 
            this.refreshbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshbtn.BackColor = System.Drawing.Color.White;
            this.refreshbtn.BorderColor = System.Drawing.Color.Silver;
            this.refreshbtn.BorderRadius = 15;
            this.refreshbtn.BorderThickness = 1;
            this.refreshbtn.Controls.Add(this.refreshbtnn);
            this.refreshbtn.Controls.Add(this.btnMainButtonIcon);
            this.refreshbtn.Controls.Add(this.pageNumber2);
            this.refreshbtn.Controls.Add(this.searchField1);
            this.refreshbtn.Controls.Add(this.dataGridTable1);
            this.refreshbtn.Location = new System.Drawing.Point(22, 74);
            this.refreshbtn.Name = "refreshbtn";
            this.refreshbtn.Size = new System.Drawing.Size(920, 617);
            this.refreshbtn.TabIndex = 1;
            // 
            // refreshbtnn
            // 
            this.refreshbtnn.BorderRadius = 6;
            this.refreshbtnn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.refreshbtnn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.refreshbtnn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.refreshbtnn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.refreshbtnn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.refreshbtnn.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshbtnn.ForeColor = System.Drawing.Color.White;
            this.refreshbtnn.Location = new System.Drawing.Point(322, 16);
            this.refreshbtnn.Name = "refreshbtnn";
            this.refreshbtnn.Size = new System.Drawing.Size(91, 40);
            this.refreshbtnn.TabIndex = 13;
            this.refreshbtnn.Text = "Refresh";
            this.refreshbtnn.Click += new System.EventHandler(this.refreshbtnn_Click);
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
            this.btnMainButtonIcon.Click += new System.EventHandler(this.btnMainButtonIcon_Click);
            // 
            // pcbBlurOverlay
            // 
            this.pcbBlurOverlay.BackColor = System.Drawing.Color.Transparent;
            this.pcbBlurOverlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcbBlurOverlay.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.CustomerOvelay;
            this.pcbBlurOverlay.Location = new System.Drawing.Point(0, 0);
            this.pcbBlurOverlay.Margin = new System.Windows.Forms.Padding(2);
            this.pcbBlurOverlay.Name = "pcbBlurOverlay";
            this.pcbBlurOverlay.Size = new System.Drawing.Size(965, 710);
            this.pcbBlurOverlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbBlurOverlay.TabIndex = 3;
            this.pcbBlurOverlay.TabStop = false;
            this.pcbBlurOverlay.Visible = false;
            // 
            // pageNumber2
            // 
            this.pageNumber2.BackColor = System.Drawing.Color.Transparent;
            this.pageNumber2.Location = new System.Drawing.Point(800, 574);
            this.pageNumber2.Margin = new System.Windows.Forms.Padding(4);
            this.pageNumber2.Name = "pageNumber2";
            this.pageNumber2.Size = new System.Drawing.Size(108, 38);
            this.pageNumber2.TabIndex = 9;
            // 
            // searchField1
            // 
            this.searchField1.BackColor = System.Drawing.Color.Transparent;
            this.searchField1.Location = new System.Drawing.Point(16, 11);
            this.searchField1.Margin = new System.Windows.Forms.Padding(4);
            this.searchField1.Name = "searchField1";
            this.searchField1.PromptMessage = null;
            this.searchField1.Size = new System.Drawing.Size(299, 44);
            this.searchField1.TabIndex = 3;
            // 
            // dataGridTable1
            // 
            this.dataGridTable1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.dataGridTable1.Location = new System.Drawing.Point(16, 78);
            this.dataGridTable1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridTable1.Name = "dataGridTable1";
            this.dataGridTable1.PaginationControl = null;
            this.dataGridTable1.Size = new System.Drawing.Size(884, 489);
            this.dataGridTable1.TabIndex = 7;
            this.dataGridTable1.Load += new System.EventHandler(this.dataGridTable1_Load);
            // 
            // customerTopBar1
            // 
            this.customerTopBar1.BackColor = System.Drawing.Color.White;
            this.customerTopBar1.Location = new System.Drawing.Point(0, 3);
            this.customerTopBar1.Margin = new System.Windows.Forms.Padding(4);
            this.customerTopBar1.Name = "customerTopBar1";
            this.customerTopBar1.Size = new System.Drawing.Size(965, 69);
            this.customerTopBar1.TabIndex = 2;
            // 
            // CustomerMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.refreshbtn);
            this.Controls.Add(this.customerTopBar1);
            this.Controls.Add(this.pcbBlurOverlay);
            this.Name = "CustomerMainPage";
            this.Size = new System.Drawing.Size(965, 710);
            this.refreshbtn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbBlurOverlay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel refreshbtn;
        private Accounts_Module.SearchField searchField1;
        private CustomerTopBar customerTopBar1;
        private DataGridTable dataGridTable1;
        private PageNumber pageNumber2;
        private Guna.UI2.WinForms.Guna2Button btnMainButtonIcon;
        private System.Windows.Forms.PictureBox pcbBlurOverlay;
        private Guna.UI2.WinForms.Guna2Button refreshbtnn;
    }
}
