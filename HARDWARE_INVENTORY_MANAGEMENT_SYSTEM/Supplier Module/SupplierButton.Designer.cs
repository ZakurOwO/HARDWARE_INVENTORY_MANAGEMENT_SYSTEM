namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    partial class SupplierButton
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
            this.AddSupplierButton = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // AddSupplierButton
            // 
            this.AddSupplierButton.Location = new System.Drawing.Point(0, 0);
            this.AddSupplierButton.Name = "AddSupplierButton";
            this.AddSupplierButton.Size = new System.Drawing.Size(150, 44);
            this.AddSupplierButton.StateCommon.Border.Color1 = System.Drawing.Color.DodgerBlue;
            this.AddSupplierButton.StateCommon.Border.Color2 = System.Drawing.Color.DodgerBlue;
            this.AddSupplierButton.StateCommon.Border.Rounding = 10F;
            this.AddSupplierButton.StateCommon.Content.LongText.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddSupplierButton.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.AddSupplierButton.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.AddSupplierButton.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddSupplierButton.StateNormal.Back.Color1 = System.Drawing.Color.DodgerBlue;
            this.AddSupplierButton.StateNormal.Back.Color2 = System.Drawing.Color.DodgerBlue;
            this.AddSupplierButton.TabIndex = 2;
            this.AddSupplierButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.AddSupplierButton.Values.Text = "Add Supplier";
            // 
            // SupplierButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AddSupplierButton);
            this.Name = "SupplierButton";
            this.Size = new System.Drawing.Size(150, 44);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonButton AddSupplierButton;
    }
}
