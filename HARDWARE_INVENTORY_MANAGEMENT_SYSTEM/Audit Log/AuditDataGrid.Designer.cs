namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log
{
    partial class AuditDataGrid
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAudit = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Activity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Timestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Module = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAudit)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAudit
            // 
            this.dgvAudit.AllowUserToAddRows = false;
            this.dgvAudit.AllowUserToDeleteRows = false;
            this.dgvAudit.AllowUserToResizeColumns = false;
            this.dgvAudit.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.dgvAudit.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAudit.BackgroundColor = System.Drawing.Color.White;
            this.dgvAudit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAudit.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvAudit.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvAudit.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAudit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAudit.ColumnHeadersHeight = 35;
            this.dgvAudit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAudit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Activity,
            this.Timestamp,
            this.Module});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAudit.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAudit.EnableHeadersVisualStyles = false;
            this.dgvAudit.GridColor = System.Drawing.Color.White;
            this.dgvAudit.Location = new System.Drawing.Point(0, 0);
            this.dgvAudit.MultiSelect = false;
            this.dgvAudit.Name = "dgvAudit";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAudit.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAudit.RowHeadersVisible = false;
            this.dgvAudit.RowHeadersWidth = 15;
            this.dgvAudit.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(7)))), ((int)(((byte)(96)))));
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(7)))), ((int)(((byte)(96)))));
            this.dgvAudit.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvAudit.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.dgvAudit.RowTemplate.Height = 40;
            this.dgvAudit.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvAudit.Size = new System.Drawing.Size(935, 580);
            this.dgvAudit.TabIndex = 8;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Action";
            this.dataGridViewImageColumn1.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.menu_circle_vertical;
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 160;
            // 
            // Activity
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            this.Activity.DefaultCellStyle = dataGridViewCellStyle3;
            this.Activity.HeaderText = "Activity";
            this.Activity.Name = "Activity";
            this.Activity.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Activity.Width = 312;
            // 
            // Timestamp
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Timestamp.DefaultCellStyle = dataGridViewCellStyle4;
            this.Timestamp.HeaderText = "Timestamp";
            this.Timestamp.Name = "Timestamp";
            this.Timestamp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Timestamp.Width = 312;
            // 
            // Module
            // 
            this.Module.HeaderText = "Module";
            this.Module.Name = "Module";
            this.Module.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Module.Width = 312;
            // 
            // AuditDataGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvAudit);
            this.Name = "AuditDataGrid";
            this.Size = new System.Drawing.Size(935, 580);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAudit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvAudit;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Activity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Timestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Module;
    }
}
