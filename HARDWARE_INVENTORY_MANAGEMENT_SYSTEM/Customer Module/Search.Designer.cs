namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    partial class Search
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
            this.tbxSearchField = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // tbxSearchField
            // 
            this.tbxSearchField.BorderRadius = 8;
            this.tbxSearchField.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxSearchField.DefaultText = "Search User";
            this.tbxSearchField.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbxSearchField.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbxSearchField.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxSearchField.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxSearchField.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxSearchField.Font = new System.Drawing.Font("Lexend Light", 9F);
            this.tbxSearchField.ForeColor = System.Drawing.Color.DimGray;
            this.tbxSearchField.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxSearchField.IconLeft = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.search_02;
            this.tbxSearchField.IconLeftOffset = new System.Drawing.Point(5, 0);
            this.tbxSearchField.Location = new System.Drawing.Point(0, 0);
            this.tbxSearchField.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbxSearchField.Name = "tbxSearchField";
            this.tbxSearchField.PlaceholderText = "";
            this.tbxSearchField.SelectedText = "";
            this.tbxSearchField.Size = new System.Drawing.Size(291, 40);
            this.tbxSearchField.TabIndex = 2;
            this.tbxSearchField.TextOffset = new System.Drawing.Point(3, 0);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.tbxSearchField);
            this.DoubleBuffered = true;
            this.Name = "Search";
            this.Size = new System.Drawing.Size(288, 37);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox tbxSearchField;
    }
}
