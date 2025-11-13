namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles
{
    partial class ComboBoxWithLabel
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
            this.cbxCombobox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbxCombobox
            // 
            this.cbxCombobox.BackColor = System.Drawing.Color.Transparent;
            this.cbxCombobox.BorderRadius = 8;
            this.cbxCombobox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCombobox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbxCombobox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbxCombobox.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCombobox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbxCombobox.ItemHeight = 30;
            this.cbxCombobox.Location = new System.Drawing.Point(7, 28);
            this.cbxCombobox.Name = "cbxCombobox";
            this.cbxCombobox.Size = new System.Drawing.Size(228, 36);
            this.cbxCombobox.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(3, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(76, 21);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Full Name";
            // 
            // ComboBoxWithLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.cbxCombobox);
            this.Controls.Add(this.lblTitle);
            this.Name = "ComboBoxWithLabel";
            this.Size = new System.Drawing.Size(242, 68);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ComboBox cbxCombobox;
        private System.Windows.Forms.Label lblTitle;
    }
}
