namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    partial class PurchaseOrdersPage
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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvSupplier = new Guna.UI2.WinForms.Guna2DataGridView();
            this.POID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.View = new System.Windows.Forms.DataGridViewImageColumn();
            this.Cancel = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Actions = new System.Windows.Forms.Label();
            this.btnMainButtonIcon = new Guna.UI2.WinForms.Guna2Button();
            this.inventoryFilter_Button1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module.InventoryFilter_Button();
            this.searchFieldForSupplier1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module.SearchFieldForSupplier();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupplier)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.panel1);
            this.guna2Panel1.Controls.Add(this.dgvSupplier);
            this.guna2Panel1.Controls.Add(this.btnMainButtonIcon);
            this.guna2Panel1.Controls.Add(this.inventoryFilter_Button1);
            this.guna2Panel1.Controls.Add(this.searchFieldForSupplier1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(927, 585);
            this.guna2Panel1.TabIndex = 7;
            // 
            // dgvSupplier
            // 
            this.dgvSupplier.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvSupplier.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSupplier.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSupplier.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSupplier.ColumnHeadersHeight = 45;
            this.dgvSupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvSupplier.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.POID,
            this.Supplier,
            this.DateCreated,
            this.Total,
            this.Status,
            this.View,
            this.Cancel});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Lexend Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSupplier.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSupplier.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSupplier.Location = new System.Drawing.Point(17, 79);
            this.dgvSupplier.Name = "dgvSupplier";
            this.dgvSupplier.RowHeadersVisible = false;
            this.dgvSupplier.RowTemplate.Height = 45;
            this.dgvSupplier.Size = new System.Drawing.Size(893, 458);
            this.dgvSupplier.TabIndex = 11;
            this.dgvSupplier.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvSupplier.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvSupplier.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvSupplier.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvSupplier.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvSupplier.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvSupplier.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSupplier.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvSupplier.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvSupplier.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSupplier.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvSupplier.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvSupplier.ThemeStyle.HeaderStyle.Height = 45;
            this.dgvSupplier.ThemeStyle.ReadOnly = false;
            this.dgvSupplier.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvSupplier.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSupplier.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSupplier.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvSupplier.ThemeStyle.RowsStyle.Height = 45;
            this.dgvSupplier.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSupplier.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // POID
            // 
            this.POID.HeaderText = "PO ID";
            this.POID.Name = "POID";
            // 
            // Supplier
            // 
            this.Supplier.HeaderText = "Supplier";
            this.Supplier.Name = "Supplier";
            // 
            // DateCreated
            // 
            this.DateCreated.HeaderText = "Date Created";
            this.DateCreated.Name = "DateCreated";
            // 
            // Total
            // 
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            // 
            // View
            // 
            this.View.FillWeight = 20F;
            this.View.HeaderText = "";
            this.View.Name = "View";
            this.View.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.View.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Cancel
            // 
            this.Cancel.FillWeight = 20F;
            this.Cancel.HeaderText = "";
            this.Cancel.Name = "Cancel";
            this.Cancel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Cancel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.panel1.Controls.Add(this.Actions);
            this.panel1.Location = new System.Drawing.Point(838, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(72, 42);
            this.panel1.TabIndex = 12;
            // 
            // Actions
            // 
            this.Actions.AutoSize = true;
            this.Actions.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold);
            this.Actions.ForeColor = System.Drawing.Color.DimGray;
            this.Actions.Location = new System.Drawing.Point(3, 12);
            this.Actions.Name = "Actions";
            this.Actions.Size = new System.Drawing.Size(54, 19);
            this.Actions.TabIndex = 0;
            this.Actions.Text = "Actions";
            this.Actions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.btnMainButtonIcon.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.add_circle;
            this.btnMainButtonIcon.Location = new System.Drawing.Point(721, 26);
            this.btnMainButtonIcon.Name = "btnMainButtonIcon";
            this.btnMainButtonIcon.Size = new System.Drawing.Size(189, 40);
            this.btnMainButtonIcon.TabIndex = 7;
            this.btnMainButtonIcon.Text = "Add Purchase Order";
            // 
            // inventoryFilter_Button1
            // 
            this.inventoryFilter_Button1.BackColor = System.Drawing.Color.Transparent;
            this.inventoryFilter_Button1.Location = new System.Drawing.Point(315, 22);
            this.inventoryFilter_Button1.Name = "inventoryFilter_Button1";
            this.inventoryFilter_Button1.Size = new System.Drawing.Size(102, 43);
            this.inventoryFilter_Button1.TabIndex = 6;
            // 
            // searchFieldForSupplier1
            // 
            this.searchFieldForSupplier1.Location = new System.Drawing.Point(17, 23);
            this.searchFieldForSupplier1.Name = "searchFieldForSupplier1";
            this.searchFieldForSupplier1.Size = new System.Drawing.Size(291, 40);
            this.searchFieldForSupplier1.TabIndex = 3;
            // 
            // PurchaseOrdersPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.guna2Panel1);
            this.Name = "PurchaseOrdersPage";
            this.Size = new System.Drawing.Size(927, 585);
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupplier)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Inventory_Module.InventoryFilter_Button inventoryFilter_Button1;
        private SearchFieldForSupplier searchFieldForSupplier1;
        private Guna.UI2.WinForms.Guna2Button btnMainButtonIcon;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSupplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn POID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewImageColumn View;
        private System.Windows.Forms.DataGridViewImageColumn Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Actions;
    }
}
