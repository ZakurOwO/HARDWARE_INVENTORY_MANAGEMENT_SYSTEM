namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    partial class CartDetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDelivery = new Guna.UI2.WinForms.Guna2Button();
            this.btnWalkIn = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvCartDetails = new Guna.UI2.WinForms.Guna2DataGridView();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.panelContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.walkinOrDeliveryButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module.WalkinOrDeliveryButton();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartDetails)).BeginInit();
            this.guna2Panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(200, 100);
            this.guna2Panel1.TabIndex = 0;
            // 
            // btnDelivery
            // 
            this.btnDelivery.BorderRadius = 3;
            this.btnDelivery.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDelivery.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDelivery.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDelivery.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDelivery.FillColor = System.Drawing.Color.White;
            this.btnDelivery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelivery.ForeColor = System.Drawing.Color.Black;
            this.btnDelivery.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.fast_delivery;
            this.btnDelivery.Location = new System.Drawing.Point(119, 5);
            this.btnDelivery.Name = "btnDelivery";
            this.btnDelivery.Size = new System.Drawing.Size(95, 28);
            this.btnDelivery.TabIndex = 4;
            this.btnDelivery.Text = "Delivery";
            // 
            // btnWalkIn
            // 
            this.btnWalkIn.BorderRadius = 3;
            this.btnWalkIn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnWalkIn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnWalkIn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnWalkIn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnWalkIn.FillColor = System.Drawing.Color.White;
            this.btnWalkIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWalkIn.ForeColor = System.Drawing.Color.Black;
            this.btnWalkIn.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.users_02;
            this.btnWalkIn.Location = new System.Drawing.Point(12, 5);
            this.btnWalkIn.Name = "btnWalkIn";
            this.btnWalkIn.Size = new System.Drawing.Size(95, 28);
            this.btnWalkIn.TabIndex = 3;
            this.btnWalkIn.Text = "Walk-In";
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Controls.Add(this.guna2Panel3);
            this.guna2Panel2.Controls.Add(this.label1);
            this.guna2Panel2.Controls.Add(this.guna2TextBox1);
            this.guna2Panel2.Location = new System.Drawing.Point(0, 59);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(295, 565);
            this.guna2Panel2.TabIndex = 1;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BorderColor = System.Drawing.Color.Silver;
            this.guna2Panel3.BorderThickness = 1;
            this.guna2Panel3.Controls.Add(this.dgvCartDetails);
            this.guna2Panel3.Controls.Add(this.label2);
            this.guna2Panel3.Location = new System.Drawing.Point(0, 81);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(295, 484);
            this.guna2Panel3.TabIndex = 4;
            // 
            // dgvCartDetails
            // 
            this.dgvCartDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.dgvCartDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvCartDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCartDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvCartDetails.ColumnHeadersHeight = 45;
            this.dgvCartDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvCartDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemName,
            this.Quantity,
            this.Price,
            this.Delete});
            this.dgvCartDetails.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvCartDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCartDetails.Location = new System.Drawing.Point(1, 40);
            this.dgvCartDetails.Name = "dgvCartDetails";
            this.dgvCartDetails.ReadOnly = true;
            this.dgvCartDetails.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCartDetails.RowHeadersVisible = false;
            this.dgvCartDetails.RowHeadersWidth = 10;
            this.dgvCartDetails.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvCartDetails.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvCartDetails.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvCartDetails.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvCartDetails.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Silver;
            this.dgvCartDetails.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvCartDetails.RowTemplate.Height = 40;
            this.dgvCartDetails.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCartDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvCartDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCartDetails.Size = new System.Drawing.Size(294, 290);
            this.dgvCartDetails.TabIndex = 1;
            this.dgvCartDetails.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvCartDetails.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvCartDetails.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvCartDetails.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvCartDetails.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvCartDetails.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvCartDetails.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCartDetails.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvCartDetails.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvCartDetails.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvCartDetails.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvCartDetails.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvCartDetails.ThemeStyle.HeaderStyle.Height = 45;
            this.dgvCartDetails.ThemeStyle.ReadOnly = true;
            this.dgvCartDetails.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvCartDetails.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCartDetails.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvCartDetails.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvCartDetails.ThemeStyle.RowsStyle.Height = 40;
            this.dgvCartDetails.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCartDetails.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // ItemName
            // 
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemName.DefaultCellStyle = dataGridViewCellStyle7;
            this.ItemName.FillWeight = 173.2845F;
            this.ItemName.Frozen = true;
            this.ItemName.HeaderText = "Items Name";
            this.ItemName.MinimumWidth = 6;
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 127;
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle8;
            this.Quantity.FillWeight = 81.00632F;
            this.Quantity.HeaderText = "QTY";
            this.Quantity.MinimumWidth = 6;
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Quantity.Width = 60;
            // 
            // Price
            // 
            this.Price.FillWeight = 84.79551F;
            this.Price.HeaderText = "Price";
            this.Price.MinimumWidth = 6;
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // Delete
            // 
            this.Delete.FillWeight = 60.9137F;
            this.Delete.HeaderText = "";
            this.Delete.MinimumWidth = 6;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cart Details";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Customer Name";
            // 
            // guna2TextBox1
            // 
            this.guna2TextBox1.BorderRadius = 8;
            this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox1.DefaultText = "Walk-in Customer";
            this.guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Location = new System.Drawing.Point(30, 28);
            this.guna2TextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2TextBox1.Name = "guna2TextBox1";
            this.guna2TextBox1.PlaceholderText = "";
            this.guna2TextBox1.SelectedText = "";
            this.guna2TextBox1.Size = new System.Drawing.Size(228, 35);
            this.guna2TextBox1.TabIndex = 2;
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.BackColor = System.Drawing.Color.White;
            this.guna2Panel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2Panel4.BorderRadius = 15;
            this.guna2Panel4.BorderThickness = 1;
            this.guna2Panel4.Controls.Add(this.panelContainer);
            this.guna2Panel4.Controls.Add(this.walkinOrDeliveryButton1);
            this.guna2Panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel4.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(295, 624);
            this.guna2Panel4.TabIndex = 0;
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.Transparent;
            this.panelContainer.BorderColor = System.Drawing.Color.Silver;
            this.panelContainer.BorderRadius = 15;
            this.panelContainer.Location = new System.Drawing.Point(1, 62);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(293, 562);
            this.panelContainer.TabIndex = 3;
            // 
            // walkinOrDeliveryButton1
            // 
            this.walkinOrDeliveryButton1.BackColor = System.Drawing.Color.Transparent;
            this.walkinOrDeliveryButton1.Location = new System.Drawing.Point(34, 10);
            this.walkinOrDeliveryButton1.Name = "walkinOrDeliveryButton1";
            this.walkinOrDeliveryButton1.Size = new System.Drawing.Size(231, 43);
            this.walkinOrDeliveryButton1.TabIndex = 2;
            // 
            // CartDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.guna2Panel4);
            this.Name = "CartDetails";
            this.Size = new System.Drawing.Size(295, 624);
            this.Load += new System.EventHandler(this.CartDetails_Load);
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartDetails)).EndInit();
            this.guna2Panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnDelivery;
        private Guna.UI2.WinForms.Guna2Button btnWalkIn;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2DataGridView dgvCartDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private WalkinOrDeliveryButton walkinOrDeliveryButton1;
        private Guna.UI2.WinForms.Guna2Panel panelContainer;
    }
}
