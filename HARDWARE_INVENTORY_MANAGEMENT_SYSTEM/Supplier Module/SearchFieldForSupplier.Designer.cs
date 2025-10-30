namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    partial class SearchFieldForSupplier
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
            this.SupplierTextBoxSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // SupplierTextBoxSearch
            // 
            this.SupplierTextBoxSearch.BorderRadius = 8;
            this.SupplierTextBoxSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SupplierTextBoxSearch.DefaultText = "Search User";
            this.SupplierTextBoxSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.SupplierTextBoxSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SupplierTextBoxSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SupplierTextBoxSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SupplierTextBoxSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SupplierTextBoxSearch.Font = new System.Drawing.Font("Lexend Light", 9F);
            this.SupplierTextBoxSearch.ForeColor = System.Drawing.Color.DimGray;
            this.SupplierTextBoxSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SupplierTextBoxSearch.IconLeft = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.search_02;
            this.SupplierTextBoxSearch.IconLeftOffset = new System.Drawing.Point(5, 0);
            this.SupplierTextBoxSearch.Location = new System.Drawing.Point(0, 0);
            this.SupplierTextBoxSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SupplierTextBoxSearch.Name = "SupplierTextBoxSearch";
            this.SupplierTextBoxSearch.PlaceholderText = "";
            this.SupplierTextBoxSearch.SelectedText = "";
            this.SupplierTextBoxSearch.Size = new System.Drawing.Size(291, 40);
            this.SupplierTextBoxSearch.TabIndex = 2;
            this.SupplierTextBoxSearch.TextOffset = new System.Drawing.Point(3, 0);
            // 
            // SearchFieldForSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SupplierTextBoxSearch);
            this.Name = "SearchFieldForSupplier";
            this.Size = new System.Drawing.Size(291, 40);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox SupplierTextBoxSearch;
    }
}
