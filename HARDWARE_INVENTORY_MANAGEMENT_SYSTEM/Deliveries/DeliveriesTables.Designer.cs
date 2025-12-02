namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    partial class DeliveriesTables
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDeliveries = new System.Windows.Forms.DataGridView();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.inventoryFilter_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryFilter_Button();
            this.searchField1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.SearchField();
            this.DeliveryInternalId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeliveryID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transaction_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delivery_Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delivery_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Created_At = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updated_at = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveries)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDeliveries
            // 
            this.dgvDeliveries.AllowUserToAddRows = false;
            this.dgvDeliveries.BackgroundColor = System.Drawing.Color.White;
            this.dgvDeliveries.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDeliveries.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDeliveries.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDeliveries.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDeliveries.ColumnHeadersHeight = 45;
            this.dgvDeliveries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeliveryInternalId,
            this.DeliveryID,
            this.Transaction_ID,
            this.Delivery_Number,
            this.Delivery_Date,
            this.Status,
            this.Customer_name,
            this.Created_At,
            this.updated_at});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDeliveries.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDeliveries.EnableHeadersVisualStyles = false;
            this.dgvDeliveries.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDeliveries.Location = new System.Drawing.Point(17, 66);
            this.dgvDeliveries.Margin = new System.Windows.Forms.Padding(0);
            this.dgvDeliveries.Name = "dgvDeliveries";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDeliveries.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDeliveries.RowHeadersVisible = false;
            this.dgvDeliveries.RowTemplate.Height = 45;
            this.dgvDeliveries.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDeliveries.Size = new System.Drawing.Size(902, 512);
            this.dgvDeliveries.TabIndex = 1;
            this.dgvDeliveries.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDeliveries_CellContentClick);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.inventoryFilter_Button1);
            this.guna2Panel1.Controls.Add(this.searchField1);
            this.guna2Panel1.Controls.Add(this.dgvDeliveries);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(935, 593);
            this.guna2Panel1.TabIndex = 2;
            // 
            // inventoryFilter_Button1
            // 
            this.inventoryFilter_Button1.BackColor = System.Drawing.Color.Transparent;
            this.inventoryFilter_Button1.Location = new System.Drawing.Point(330, 14);
            this.inventoryFilter_Button1.Name = "inventoryFilter_Button1";
            this.inventoryFilter_Button1.Size = new System.Drawing.Size(102, 43);
            this.inventoryFilter_Button1.TabIndex = 8;
            // 
            // searchField1
            // 
            this.searchField1.BackColor = System.Drawing.Color.Transparent;
            this.searchField1.Location = new System.Drawing.Point(25, 11);
            this.searchField1.Name = "searchField1";
            this.searchField1.PromptMessage = "Search Deliveries";
            this.searchField1.Size = new System.Drawing.Size(299, 54);
            this.searchField1.TabIndex = 7;
            //
            // DeliveryInternalId
            //
            this.DeliveryInternalId.DataPropertyName = "delivery_id";
            this.DeliveryInternalId.HeaderText = "Delivery Internal ID";
            this.DeliveryInternalId.Name = "DeliveryInternalId";
            this.DeliveryInternalId.Visible = false;
            //
            // DeliveryID
            //
            this.DeliveryID.DataPropertyName = "DeliveryID";
            this.DeliveryID.FillWeight = 120F;
            this.DeliveryID.HeaderText = "Delivery ID";
            this.DeliveryID.Name = "DeliveryID";
            this.DeliveryID.Width = 120;
            //
            // Transaction_ID
            //
            this.Transaction_ID.DataPropertyName = "transaction_id";
            this.Transaction_ID.FillWeight = 120F;
            this.Transaction_ID.HeaderText = "Transaction ID";
            this.Transaction_ID.Name = "Transaction_ID";
            this.Transaction_ID.Width = 120;
            //
            // Delivery_Number
            //
            this.Delivery_Number.DataPropertyName = "delivery_number";
            this.Delivery_Number.FillWeight = 120F;
            this.Delivery_Number.HeaderText = "Delivery Number";
            this.Delivery_Number.Name = "Delivery_Number";
            this.Delivery_Number.Width = 120;
            //
            // Delivery_Date
            //
            this.Delivery_Date.DataPropertyName = "delivery_date";
            this.Delivery_Date.HeaderText = "Delivery Date";
            this.Delivery_Date.Name = "Delivery_Date";
            //
            // Status
            //
            this.Status.DataPropertyName = "status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 120;
            //
            // Customer_name
            //
            this.Customer_name.DataPropertyName = "customer_name";
            this.Customer_name.FillWeight = 120F;
            this.Customer_name.HeaderText = "Customer Name";
            this.Customer_name.Name = "Customer_name";
            this.Customer_name.Width = 120;
            //
            // Created_At
            //
            this.Created_At.DataPropertyName = "created_at";
            this.Created_At.FillWeight = 120F;
            this.Created_At.HeaderText = "Created At Date";
            this.Created_At.Name = "Created_At";
            this.Created_At.Width = 120;
            //
            // updated_at
            //
            this.updated_at.DataPropertyName = "updated_at";
            this.updated_at.FillWeight = 125F;
            this.updated_at.HeaderText = "Update at Date";
            this.updated_at.Name = "updated_at";
            this.updated_at.Width = 125;
            // 
            // DeliveriesTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.guna2Panel1);
            this.DoubleBuffered = true;
            this.Name = "DeliveriesTables";
            this.Size = new System.Drawing.Size(935, 593);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveries)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDeliveries;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Inventory_Module.InventoryFilter_Button inventoryFilter_Button1;
        private Accounts_Module.SearchField searchField1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeliveryInternalId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeliveryID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Transaction_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Delivery_Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Delivery_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Created_At;
        private System.Windows.Forms.DataGridViewTextBoxColumn updated_at;
    }
}
