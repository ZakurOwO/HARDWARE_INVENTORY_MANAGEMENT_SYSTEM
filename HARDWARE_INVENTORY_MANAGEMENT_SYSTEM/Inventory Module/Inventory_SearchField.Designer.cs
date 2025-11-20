namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    partial class Inventory_SearchField
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
            this.searchtxtbox = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // searchtxtbox
            // 
            this.searchtxtbox.BorderColor = System.Drawing.Color.Gainsboro;
            this.searchtxtbox.BorderRadius = 6;
            this.searchtxtbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.searchtxtbox.DefaultText = "";
            this.searchtxtbox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.searchtxtbox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.searchtxtbox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.searchtxtbox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.searchtxtbox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.searchtxtbox.Font = new System.Drawing.Font("Lexend Light", 8.7F);
            this.searchtxtbox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.searchtxtbox.IconLeft = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.search_02;
            this.searchtxtbox.IconLeftOffset = new System.Drawing.Point(5, 0);
            this.searchtxtbox.Location = new System.Drawing.Point(3, 4);
            this.searchtxtbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.searchtxtbox.Name = "searchtxtbox";
            this.searchtxtbox.PlaceholderText = "Search Item";
            this.searchtxtbox.SelectedText = "";
            this.searchtxtbox.Size = new System.Drawing.Size(327, 36);
            this.searchtxtbox.TabIndex = 3;
            this.searchtxtbox.TextOffset = new System.Drawing.Point(3, -2);
            this.searchtxtbox.Click += new System.EventHandler(this.searchtxtbox_Click);
            // 
            // Inventory_SearchField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.searchtxtbox);
            this.Name = "Inventory_SearchField";
            this.Size = new System.Drawing.Size(336, 47);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox searchtxtbox;
    }
}
