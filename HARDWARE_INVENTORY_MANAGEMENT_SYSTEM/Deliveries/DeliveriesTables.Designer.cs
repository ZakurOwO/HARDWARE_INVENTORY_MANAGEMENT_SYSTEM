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
            this.DeliveryOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrackingNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeliveryStatus = new Krypton.Toolkit.KryptonDataGridViewComboBoxColumn();
            this.DateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveries)).BeginInit();
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
            this.DeliveryOrder,
            this.TrackingNumber,
            this.Location,
            this.TransactionType,
            this.VehicleType,
            this.DeliveryStatus,
            this.DateCreated,
            this.Action});
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
            this.dgvDeliveries.Location = new System.Drawing.Point(0, 0);
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
            this.dgvDeliveries.Size = new System.Drawing.Size(932, 580);
            this.dgvDeliveries.TabIndex = 1;
            this.dgvDeliveries.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDeliveries_CellContentClick);
            // 
            // DeliveryOrder
            // 
            this.DeliveryOrder.HeaderText = "Delivery Order #";
            this.DeliveryOrder.Name = "DeliveryOrder";
            this.DeliveryOrder.Width = 140;
            // 
            // TrackingNumber
            // 
            this.TrackingNumber.HeaderText = "Tracking Number";
            this.TrackingNumber.Name = "TrackingNumber";
            this.TrackingNumber.Width = 140;
            // 
            // Location
            // 
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.Width = 130;
            // 
            // TransactionType
            // 
            this.TransactionType.HeaderText = "Transaction Type";
            this.TransactionType.Name = "TransactionType";
            this.TransactionType.Width = 140;
            // 
            // VehicleType
            // 
            this.VehicleType.HeaderText = "Vehicle Type";
            this.VehicleType.Name = "VehicleType";
            // 
            // DeliveryStatus
            // 
            this.DeliveryStatus.DropDownWidth = 121;
            this.DeliveryStatus.HeaderText = "Delivery Status";
            this.DeliveryStatus.Name = "DeliveryStatus";
            this.DeliveryStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DeliveryStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DeliveryStatus.Width = 120;
            // 
            // DateCreated
            // 
            this.DateCreated.HeaderText = "Date Created";
            this.DateCreated.Name = "DateCreated";
            this.DateCreated.Width = 120;
            // 
            // Action
            // 
            this.Action.HeaderText = "";
            this.Action.Name = "Action";
            this.Action.Width = 42;
            // 
            // DeliveriesTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.dgvDeliveries);
            this.DoubleBuffered = true;
            this.Name = "DeliveriesTables";
            this.Size = new System.Drawing.Size(935, 580);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveries)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDeliveries;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeliveryOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrackingNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleType;
        private Krypton.Toolkit.KryptonDataGridViewComboBoxColumn DeliveryStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action;
    }
}
