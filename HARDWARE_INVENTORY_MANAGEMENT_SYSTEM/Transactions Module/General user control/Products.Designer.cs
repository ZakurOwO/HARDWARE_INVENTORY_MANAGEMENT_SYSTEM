namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    partial class Products
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
            this.pnlProduct1 = new Guna.UI2.WinForms.Guna2Panel();
            this.pbxProductImage = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnAddToCart = new Guna.UI2.WinForms.Guna2Button();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.pnlProduct1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxProductImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlProduct1
            // 
            this.pnlProduct1.BackColor = System.Drawing.Color.White;
            this.pnlProduct1.BorderColor = System.Drawing.Color.Silver;
            this.pnlProduct1.BorderRadius = 15;
            this.pnlProduct1.BorderThickness = 1;
            this.pnlProduct1.Controls.Add(this.pbxProductImage);
            this.pnlProduct1.Controls.Add(this.btnAddToCart);
            this.pnlProduct1.Controls.Add(this.lblPrice);
            this.pnlProduct1.Controls.Add(this.lblProductName);
            this.pnlProduct1.Location = new System.Drawing.Point(5, 5);
            this.pnlProduct1.Name = "pnlProduct1";
            this.pnlProduct1.Size = new System.Drawing.Size(140, 130);
            this.pnlProduct1.TabIndex = 5;
            // 
            // pbxProductImage
            // 
            this.pbxProductImage.BackColor = System.Drawing.Color.Transparent;
            this.pbxProductImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbxProductImage.BorderRadius = 10;
            this.pbxProductImage.FillColor = System.Drawing.Color.Transparent;
            this.pbxProductImage.ImageRotate = 0F;
            this.pbxProductImage.Location = new System.Drawing.Point(10, 9);
            this.pbxProductImage.Name = "pbxProductImage";
            this.pbxProductImage.Size = new System.Drawing.Size(120, 70);
            this.pbxProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxProductImage.TabIndex = 4;
            this.pbxProductImage.TabStop = false;
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.BorderRadius = 3;
            this.btnAddToCart.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddToCart.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddToCart.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddToCart.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddToCart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.btnAddToCart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.shopping_basket_add;
            this.btnAddToCart.ImageOffset = new System.Drawing.Point(0, -1);
            this.btnAddToCart.ImageSize = new System.Drawing.Size(18, 18);
            this.btnAddToCart.Location = new System.Drawing.Point(101, 100);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(25, 25);
            this.btnAddToCart.TabIndex = 3;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Lexend Light", 8F);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(139)))), ((int)(((byte)(207)))));
            this.lblPrice.Location = new System.Drawing.Point(8, 101);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(52, 17);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "₱598.00";
            this.lblPrice.Click += new System.EventHandler(this.lblPrice_Click);
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Lexend Light", 8F);
            this.lblProductName.Location = new System.Drawing.Point(5, 82);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(127, 17);
            this.lblProductName.TabIndex = 1;
            this.lblProductName.Text = "Deformed Bar G33-20..";
            this.lblProductName.Click += new System.EventHandler(this.lblProductName_Click);
            // 
            // Products
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlProduct1);
            this.Name = "Products";
            this.Size = new System.Drawing.Size(146, 136);
            this.pnlProduct1.ResumeLayout(false);
            this.pnlProduct1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxProductImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlProduct1;
        private Guna.UI2.WinForms.Guna2Button btnAddToCart;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblProductName;
        private Guna.UI2.WinForms.Guna2PictureBox pbxProductImage;
    }
}
