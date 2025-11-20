namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    partial class AddCustomerForm
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
            this.label8 = new System.Windows.Forms.Label();
            this.closeButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles.CloseButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxCompanyName = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tbxContactNumber = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxContactPerson = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxAddress = new Guna.UI2.WinForms.Guna2TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxProvince = new Guna.UI2.WinForms.Guna2TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxCityMunicipality = new Guna.UI2.WinForms.Guna2TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnWhite = new Guna.UI2.WinForms.Guna2Button();
            this.btnBlue = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(87, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "*";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // closeButton1
            // 
            this.closeButton1.BackColor = System.Drawing.Color.Transparent;
            this.closeButton1.Location = new System.Drawing.Point(501, 17);
            this.closeButton1.Name = "closeButton1";
            this.closeButton1.Size = new System.Drawing.Size(35, 35);
            this.closeButton1.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lexend SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 27);
            this.label1.TabIndex = 31;
            this.label1.Text = "Add Customer";
            // 
            // tbxCompanyName
            // 
            this.tbxCompanyName.BorderRadius = 8;
            this.tbxCompanyName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxCompanyName.DefaultText = "Ex. Topaz Hardware";
            this.tbxCompanyName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbxCompanyName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbxCompanyName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxCompanyName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxCompanyName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxCompanyName.Font = new System.Drawing.Font("Lexend Light", 9F);
            this.tbxCompanyName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxCompanyName.Location = new System.Drawing.Point(26, 95);
            this.tbxCompanyName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbxCompanyName.Name = "tbxCompanyName";
            this.tbxCompanyName.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.tbxCompanyName.PlaceholderText = "";
            this.tbxCompanyName.SelectedText = "";
            this.tbxCompanyName.Size = new System.Drawing.Size(228, 36);
            this.tbxCompanyName.TabIndex = 33;
            this.tbxCompanyName.TextOffset = new System.Drawing.Point(0, -2);
            this.tbxCompanyName.TextChanged += new System.EventHandler(this.tbxCompanyName_TextChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(21, 70);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(113, 21);
            this.lblTitle.TabIndex = 32;
            this.lblTitle.Text = "Company Name";
            // 
            // tbxContactNumber
            // 
            this.tbxContactNumber.BorderRadius = 8;
            this.tbxContactNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxContactNumber.DefaultText = "Ex. 09123456789";
            this.tbxContactNumber.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbxContactNumber.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbxContactNumber.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxContactNumber.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxContactNumber.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxContactNumber.Font = new System.Drawing.Font("Lexend Light", 9F);
            this.tbxContactNumber.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxContactNumber.Location = new System.Drawing.Point(26, 178);
            this.tbxContactNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbxContactNumber.Name = "tbxContactNumber";
            this.tbxContactNumber.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.tbxContactNumber.PlaceholderText = "";
            this.tbxContactNumber.SelectedText = "";
            this.tbxContactNumber.Size = new System.Drawing.Size(228, 36);
            this.tbxContactNumber.TabIndex = 35;
            this.tbxContactNumber.TextOffset = new System.Drawing.Point(0, -2);
            this.tbxContactNumber.TextChanged += new System.EventHandler(this.tbxContactNumber_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 21);
            this.label2.TabIndex = 34;
            this.label2.Text = "Contact Number";
            // 
            // tbxContactPerson
            // 
            this.tbxContactPerson.BorderRadius = 8;
            this.tbxContactPerson.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxContactPerson.DefaultText = "Ex. Juan Dela Cruz";
            this.tbxContactPerson.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbxContactPerson.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbxContactPerson.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxContactPerson.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxContactPerson.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxContactPerson.Font = new System.Drawing.Font("Lexend Light", 9F);
            this.tbxContactPerson.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxContactPerson.Location = new System.Drawing.Point(319, 94);
            this.tbxContactPerson.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbxContactPerson.Name = "tbxContactPerson";
            this.tbxContactPerson.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.tbxContactPerson.PlaceholderText = "";
            this.tbxContactPerson.SelectedText = "";
            this.tbxContactPerson.Size = new System.Drawing.Size(221, 36);
            this.tbxContactPerson.TabIndex = 37;
            this.tbxContactPerson.TextOffset = new System.Drawing.Point(0, -2);
            this.tbxContactPerson.TextChanged += new System.EventHandler(this.tbxContactPerson_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(315, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 21);
            this.label3.TabIndex = 36;
            this.label3.Text = "Contact Person";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(419, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "*";
            // 
            // tbxEmail
            // 
            this.tbxEmail.BorderRadius = 8;
            this.tbxEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxEmail.DefaultText = "Ex. customer@gmail.com";
            this.tbxEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbxEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbxEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxEmail.Font = new System.Drawing.Font("Lexend Light", 9F);
            this.tbxEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxEmail.Location = new System.Drawing.Point(315, 178);
            this.tbxEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbxEmail.Name = "tbxEmail";
            this.tbxEmail.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.tbxEmail.PlaceholderText = "";
            this.tbxEmail.SelectedText = "";
            this.tbxEmail.Size = new System.Drawing.Size(228, 36);
            this.tbxEmail.TabIndex = 41;
            this.tbxEmail.TextOffset = new System.Drawing.Point(0, -2);
            this.tbxEmail.TextChanged += new System.EventHandler(this.tbxEmail_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(311, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 21);
            this.label4.TabIndex = 40;
            this.label4.Text = "E-mail";
            // 
            // tbxAddress
            // 
            this.tbxAddress.BorderRadius = 8;
            this.tbxAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxAddress.DefaultText = "Ex. Blk 20 Lot 12, Village, Borol 1st";
            this.tbxAddress.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbxAddress.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbxAddress.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxAddress.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxAddress.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxAddress.Font = new System.Drawing.Font("Lexend Light", 9F);
            this.tbxAddress.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxAddress.Location = new System.Drawing.Point(25, 262);
            this.tbxAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbxAddress.Name = "tbxAddress";
            this.tbxAddress.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.tbxAddress.PlaceholderText = "";
            this.tbxAddress.SelectedText = "";
            this.tbxAddress.Size = new System.Drawing.Size(518, 36);
            this.tbxAddress.TabIndex = 43;
            this.tbxAddress.TextOffset = new System.Drawing.Point(0, -2);
            this.tbxAddress.TextChanged += new System.EventHandler(this.tbxAddress_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(21, 238);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 21);
            this.label6.TabIndex = 42;
            this.label6.Text = "Address";
            // 
            // tbxProvince
            // 
            this.tbxProvince.BorderRadius = 8;
            this.tbxProvince.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxProvince.DefaultText = "Ex. Bulacan";
            this.tbxProvince.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbxProvince.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbxProvince.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxProvince.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxProvince.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxProvince.Font = new System.Drawing.Font("Lexend Light", 9F);
            this.tbxProvince.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxProvince.Location = new System.Drawing.Point(315, 348);
            this.tbxProvince.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbxProvince.Name = "tbxProvince";
            this.tbxProvince.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.tbxProvince.PlaceholderText = "";
            this.tbxProvince.SelectedText = "";
            this.tbxProvince.Size = new System.Drawing.Size(228, 36);
            this.tbxProvince.TabIndex = 47;
            this.tbxProvince.TextOffset = new System.Drawing.Point(0, -2);
            this.tbxProvince.TextChanged += new System.EventHandler(this.tbxProvince_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(311, 324);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 21);
            this.label7.TabIndex = 46;
            this.label7.Text = "Province";
            // 
            // tbxCityMunicipality
            // 
            this.tbxCityMunicipality.BorderRadius = 8;
            this.tbxCityMunicipality.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxCityMunicipality.DefaultText = "Ex. Balagtas";
            this.tbxCityMunicipality.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbxCityMunicipality.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbxCityMunicipality.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxCityMunicipality.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxCityMunicipality.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxCityMunicipality.Font = new System.Drawing.Font("Lexend Light", 9F);
            this.tbxCityMunicipality.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxCityMunicipality.Location = new System.Drawing.Point(26, 348);
            this.tbxCityMunicipality.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbxCityMunicipality.Name = "tbxCityMunicipality";
            this.tbxCityMunicipality.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.tbxCityMunicipality.PlaceholderText = "";
            this.tbxCityMunicipality.SelectedText = "";
            this.tbxCityMunicipality.Size = new System.Drawing.Size(228, 36);
            this.tbxCityMunicipality.TabIndex = 45;
            this.tbxCityMunicipality.TextOffset = new System.Drawing.Point(0, -2);
            this.tbxCityMunicipality.TextChanged += new System.EventHandler(this.tbxCityMunicipality_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(22, 324);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 21);
            this.label9.TabIndex = 44;
            this.label9.Text = "City/Municipality";
            // 
            // btnWhite
            // 
            this.btnWhite.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnWhite.BorderRadius = 8;
            this.btnWhite.BorderThickness = 1;
            this.btnWhite.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnWhite.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnWhite.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnWhite.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnWhite.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnWhite.FillColor = System.Drawing.Color.White;
            this.btnWhite.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWhite.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnWhite.Location = new System.Drawing.Point(292, 423);
            this.btnWhite.Name = "btnWhite";
            this.btnWhite.PressedColor = System.Drawing.Color.Azure;
            this.btnWhite.Size = new System.Drawing.Size(120, 40);
            this.btnWhite.TabIndex = 50;
            this.btnWhite.Text = "Cancel";
            // 
            // btnBlue
            // 
            this.btnBlue.BorderRadius = 8;
            this.btnBlue.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBlue.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBlue.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBlue.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBlue.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.btnBlue.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBlue.ForeColor = System.Drawing.Color.White;
            this.btnBlue.Location = new System.Drawing.Point(423, 423);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(120, 40);
            this.btnBlue.TabIndex = 49;
            this.btnBlue.Text = "Add";
            // 
            // AddCustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CancelButton = this.btnWhite;
            this.ClientSize = new System.Drawing.Size(556, 479);
            this.Controls.Add(this.btnWhite);
            this.Controls.Add(this.btnBlue);
            this.Controls.Add(this.tbxProvince);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbxCityMunicipality);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbxAddress);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbxEmail);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxContactPerson);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxContactNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxCompanyName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeButton1);
            this.Controls.Add(this.label8);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddCustomerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private UserControlFiles.CloseButton closeButton1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox tbxCompanyName;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2TextBox tbxContactNumber;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox tbxContactPerson;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox tbxEmail;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox tbxAddress;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2TextBox tbxProvince;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2TextBox tbxCityMunicipality;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2Button btnWhite;
        private Guna.UI2.WinForms.Guna2Button btnBlue;
    }
}
