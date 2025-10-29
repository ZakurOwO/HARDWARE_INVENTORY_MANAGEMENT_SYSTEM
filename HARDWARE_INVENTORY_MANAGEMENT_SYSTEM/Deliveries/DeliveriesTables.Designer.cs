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
            this.DeliveryStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveries)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDeliveries
            // 
            this.dgvDeliveries.BackgroundColor = System.Drawing.Color.White;
            this.dgvDeliveries.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDeliveries.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDeliveries.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDeliveries.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDeliveries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeliveries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeliveryOrder,
            this.TrackingNumber,
            this.Location,
            this.TransactionType,
            this.VehicleType,
            this.DeliveryStatus,
            this.DateCreated,
            this.Action});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Lexend SemiBold", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDeliveries.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDeliveries.EnableHeadersVisualStyles = false;
            this.dgvDeliveries.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDeliveries.Location = new System.Drawing.Point(3, 0);
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
            this.dgvDeliveries.Size = new System.Drawing.Size(790, 395);
            this.dgvDeliveries.TabIndex = 1;
            this.dgvDeliveries.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDeliveries_CellContentClick);
            // 
            // DeliveryOrder
            // 
            this.DeliveryOrder.HeaderText = "Delivery Order #";
            this.DeliveryOrder.Name = "DeliveryOrder";
            // 
            // TrackingNumber
            // 
            this.TrackingNumber.HeaderText = "Tracking Number";
            this.TrackingNumber.Name = "TrackingNumber";
            // 
            // Location
            // 
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            // 
            // TransactionType
            // 
            this.TransactionType.HeaderText = "Transaction Type";
            this.TransactionType.Name = "TransactionType";
            // 
            // VehicleType
            // 
            this.VehicleType.HeaderText = "Vehicle Type";
            this.VehicleType.Name = "VehicleType";
            // 
            // DeliveryStatus
            // 
            this.DeliveryStatus.HeaderText = "Delivery Status";
            this.DeliveryStatus.Name = "DeliveryStatus";
            // 
            // DateCreated
            // 
            this.DateCreated.HeaderText = "Date Created";
            this.DateCreated.Name = "DateCreated";
            // 
            // Action
            // 
            this.Action.HeaderText = "Action";
            this.Action.Name = "Action";
            // 
            // DeliveriesTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.dgvDeliveries);
            this.DoubleBuffered = true;
            this.Name = "DeliveriesTables";
            this.Size = new System.Drawing.Size(790, 395);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn DeliveryStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action;
    }
}
