namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    partial class Add_New_Truck_Button
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
            this.AddVehicleButton = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // AddVehicleButton
            // 
            this.AddVehicleButton.Location = new System.Drawing.Point(0, 0);
            this.AddVehicleButton.Name = "AddVehicleButton";
            this.AddVehicleButton.Size = new System.Drawing.Size(169, 44);
            this.AddVehicleButton.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.AddVehicleButton.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.AddVehicleButton.StateCommon.Border.Rounding = 8F;
            this.AddVehicleButton.StateCommon.Content.LongText.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddVehicleButton.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.AddVehicleButton.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.AddVehicleButton.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddVehicleButton.StateNormal.Back.Color1 = System.Drawing.Color.DodgerBlue;
            this.AddVehicleButton.StateNormal.Back.Color2 = System.Drawing.Color.DodgerBlue;
            this.AddVehicleButton.TabIndex = 1;
            this.AddVehicleButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.AddVehicleButton.Values.Text = "Add Vehicle";
            // 
            // Add_New_Truck_Button
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AddVehicleButton);
            this.Name = "Add_New_Truck_Button";
            this.Size = new System.Drawing.Size(172, 44);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonButton AddVehicleButton;
    }
}
