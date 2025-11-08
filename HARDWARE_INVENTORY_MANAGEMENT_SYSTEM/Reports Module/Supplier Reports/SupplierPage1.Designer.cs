namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Supplier_Reports
{
    partial class SupplierPage1
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.reportsKeyMetrics4 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsKeyMetrics();
            this.reportsKeyMetrics3 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsKeyMetrics();
            this.reportsKeyMetrics2 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsKeyMetrics();
            this.reportsKeyMetrics1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.ReportsKeyMetrics();
            this.pnlTable = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvCurrentStockReport = new Guna.UI2.WinForms.Guna2DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.SupplierID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContactPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContactInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3.SuspendLayout();
            this.pnlTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentStockReport)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.reportsKeyMetrics4);
            this.panel3.Controls.Add(this.reportsKeyMetrics3);
            this.panel3.Controls.Add(this.reportsKeyMetrics2);
            this.panel3.Controls.Add(this.reportsKeyMetrics1);
            this.panel3.Location = new System.Drawing.Point(2, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(900, 90);
            this.panel3.TabIndex = 17;
            // 
            // reportsKeyMetrics4
            // 
            this.reportsKeyMetrics4.BackColor = System.Drawing.Color.Transparent;
            this.reportsKeyMetrics4.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.time_clock_yellow;
            this.reportsKeyMetrics4.Location = new System.Drawing.Point(692, 2);
            this.reportsKeyMetrics4.Name = "reportsKeyMetrics4";
            this.reportsKeyMetrics4.Size = new System.Drawing.Size(201, 86);
            this.reportsKeyMetrics4.TabIndex = 3;
            this.reportsKeyMetrics4.Title = "Pending Deliveries";
            this.reportsKeyMetrics4.Value = 1234;
            // 
            // reportsKeyMetrics3
            // 
            this.reportsKeyMetrics3.BackColor = System.Drawing.Color.Transparent;
            this.reportsKeyMetrics3.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Peso_sign_violet;
            this.reportsKeyMetrics3.Location = new System.Drawing.Point(468, 2);
            this.reportsKeyMetrics3.Name = "reportsKeyMetrics3";
            this.reportsKeyMetrics3.Size = new System.Drawing.Size(201, 86);
            this.reportsKeyMetrics3.TabIndex = 2;
            this.reportsKeyMetrics3.Title = "Purchase Orders";
            this.reportsKeyMetrics3.Value = 1234;
            // 
            // reportsKeyMetrics2
            // 
            this.reportsKeyMetrics2.BackColor = System.Drawing.Color.Transparent;
            this.reportsKeyMetrics2.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Group_1000004200;
            this.reportsKeyMetrics2.Location = new System.Drawing.Point(242, 2);
            this.reportsKeyMetrics2.Name = "reportsKeyMetrics2";
            this.reportsKeyMetrics2.Size = new System.Drawing.Size(201, 86);
            this.reportsKeyMetrics2.TabIndex = 1;
            this.reportsKeyMetrics2.Title = "Total Purchase";
            this.reportsKeyMetrics2.Value = 1234;
            // 
            // reportsKeyMetrics1
            // 
            this.reportsKeyMetrics1.BackColor = System.Drawing.Color.Transparent;
            this.reportsKeyMetrics1.Icon = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.People_Blue;
            this.reportsKeyMetrics1.Location = new System.Drawing.Point(17, 2);
            this.reportsKeyMetrics1.Name = "reportsKeyMetrics1";
            this.reportsKeyMetrics1.Size = new System.Drawing.Size(201, 86);
            this.reportsKeyMetrics1.TabIndex = 0;
            this.reportsKeyMetrics1.Title = "Total Suppliers";
            this.reportsKeyMetrics1.Value = 1234;
            // 
            // pnlTable
            // 
            this.pnlTable.AutoScroll = true;
            this.pnlTable.BackColor = System.Drawing.Color.White;
            this.pnlTable.BorderColor = System.Drawing.Color.LightGray;
            this.pnlTable.BorderRadius = 10;
            this.pnlTable.BorderThickness = 1;
            this.pnlTable.Controls.Add(this.dgvCurrentStockReport);
            this.pnlTable.Controls.Add(this.label2);
            this.pnlTable.Location = new System.Drawing.Point(2, 99);
            this.pnlTable.Name = "pnlTable";
            this.pnlTable.Size = new System.Drawing.Size(900, 365);
            this.pnlTable.TabIndex = 18;
            // 
            // dgvCurrentStockReport
            // 
            this.dgvCurrentStockReport.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvCurrentStockReport.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCurrentStockReport.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCurrentStockReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCurrentStockReport.ColumnHeadersHeight = 45;
            this.dgvCurrentStockReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvCurrentStockReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SupplierID,
            this.SupplierName,
            this.ContactPerson,
            this.ContactInfo,
            this.Address,
            this.Status});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Lexend Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCurrentStockReport.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCurrentStockReport.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCurrentStockReport.Location = new System.Drawing.Point(21, 48);
            this.dgvCurrentStockReport.Name = "dgvCurrentStockReport";
            this.dgvCurrentStockReport.RowHeadersVisible = false;
            this.dgvCurrentStockReport.RowTemplate.Height = 45;
            this.dgvCurrentStockReport.Size = new System.Drawing.Size(858, 305);
            this.dgvCurrentStockReport.TabIndex = 2;
            this.dgvCurrentStockReport.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvCurrentStockReport.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvCurrentStockReport.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvCurrentStockReport.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvCurrentStockReport.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvCurrentStockReport.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvCurrentStockReport.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCurrentStockReport.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvCurrentStockReport.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvCurrentStockReport.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvCurrentStockReport.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvCurrentStockReport.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvCurrentStockReport.ThemeStyle.HeaderStyle.Height = 45;
            this.dgvCurrentStockReport.ThemeStyle.ReadOnly = false;
            this.dgvCurrentStockReport.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvCurrentStockReport.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCurrentStockReport.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvCurrentStockReport.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvCurrentStockReport.ThemeStyle.RowsStyle.Height = 45;
            this.dgvCurrentStockReport.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCurrentStockReport.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lexend SemiBold", 11F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(27, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Supplier Directory";
            // 
            // SupplierID
            // 
            this.SupplierID.HeaderText = "Supplier ID";
            this.SupplierID.Name = "SupplierID";
            // 
            // SupplierName
            // 
            this.SupplierName.HeaderText = "Supplier Name";
            this.SupplierName.Name = "SupplierName";
            // 
            // ContactPerson
            // 
            this.ContactPerson.HeaderText = "Contact Person";
            this.ContactPerson.Name = "ContactPerson";
            // 
            // ContactInfo
            // 
            this.ContactInfo.HeaderText = "Contact Info";
            this.ContactInfo.Name = "ContactInfo";
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            // 
            // SupplierPage1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlTable);
            this.Controls.Add(this.panel3);
            this.Name = "SupplierPage1";
            this.Size = new System.Drawing.Size(905, 470);
            this.panel3.ResumeLayout(false);
            this.pnlTable.ResumeLayout(false);
            this.pnlTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentStockReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private ReportsKeyMetrics reportsKeyMetrics4;
        private ReportsKeyMetrics reportsKeyMetrics3;
        private ReportsKeyMetrics reportsKeyMetrics2;
        private ReportsKeyMetrics reportsKeyMetrics1;
        private Guna.UI2.WinForms.Guna2Panel pnlTable;
        private Guna.UI2.WinForms.Guna2DataGridView dgvCurrentStockReport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContactPerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContactInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}
