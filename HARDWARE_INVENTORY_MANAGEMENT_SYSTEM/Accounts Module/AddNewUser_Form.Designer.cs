namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    partial class AddNewUser_Form
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
            this.RoleComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.AccountStatusComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.DateTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.EmailTxtbx = new Guna.UI2.WinForms.Guna2TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.PasswordTxtbx = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.UserNameTxtbx = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AddressTxtbx = new Guna.UI2.WinForms.Guna2TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.FullNameTxtbx = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AccountIDTxtbx = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ClearBtn = new Guna.UI2.WinForms.Guna2Button();
            this.ProceedBtn = new Guna.UI2.WinForms.Guna2Button();
            this.closeButton1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles.CloseButton();
            this.SuspendLayout();
            // 
            // RoleComboBox
            // 
            this.RoleComboBox.BackColor = System.Drawing.Color.Transparent;
            this.RoleComboBox.BorderRadius = 8;
            this.RoleComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RoleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RoleComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.RoleComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.RoleComboBox.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoleComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.RoleComboBox.ItemHeight = 30;
            this.RoleComboBox.Location = new System.Drawing.Point(309, 319);
            this.RoleComboBox.Name = "RoleComboBox";
            this.RoleComboBox.Size = new System.Drawing.Size(228, 36);
            this.RoleComboBox.TabIndex = 60;
            this.RoleComboBox.SelectedIndexChanged += new System.EventHandler(this.RoleComboBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(305, 295);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 21);
            this.label8.TabIndex = 59;
            this.label8.Text = "Role";
            // 
            // AccountStatusComboBox
            // 
            this.AccountStatusComboBox.BackColor = System.Drawing.Color.Transparent;
            this.AccountStatusComboBox.BorderRadius = 8;
            this.AccountStatusComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.AccountStatusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AccountStatusComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.AccountStatusComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.AccountStatusComboBox.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountStatusComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.AccountStatusComboBox.ItemHeight = 30;
            this.AccountStatusComboBox.Items.AddRange(new object[] {
            "Active",
            "Inactive",
            "Suspended"});
            this.AccountStatusComboBox.Location = new System.Drawing.Point(33, 396);
            this.AccountStatusComboBox.Name = "AccountStatusComboBox";
            this.AccountStatusComboBox.Size = new System.Drawing.Size(228, 36);
            this.AccountStatusComboBox.TabIndex = 58;
            this.AccountStatusComboBox.SelectedIndexChanged += new System.EventHandler(this.AccountStatusComboBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(29, 372);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 21);
            this.label7.TabIndex = 57;
            this.label7.Text = "Account Status";
            // 
            // DateTextBox
            // 
            this.DateTextBox.BorderRadius = 8;
            this.DateTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.DateTextBox.DefaultText = "";
            this.DateTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.DateTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.DateTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.DateTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.DateTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.DateTextBox.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.DateTextBox.Location = new System.Drawing.Point(309, 396);
            this.DateTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DateTextBox.Name = "DateTextBox";
            this.DateTextBox.PlaceholderText = "";
            this.DateTextBox.SelectedText = "";
            this.DateTextBox.Size = new System.Drawing.Size(228, 36);
            this.DateTextBox.TabIndex = 56;
            this.DateTextBox.TextChanged += new System.EventHandler(this.DateTextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(305, 372);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 21);
            this.label6.TabIndex = 55;
            this.label6.Text = "Date Created";
            // 
            // EmailTxtbx
            // 
            this.EmailTxtbx.BorderRadius = 8;
            this.EmailTxtbx.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.EmailTxtbx.DefaultText = "";
            this.EmailTxtbx.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.EmailTxtbx.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.EmailTxtbx.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.EmailTxtbx.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.EmailTxtbx.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.EmailTxtbx.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailTxtbx.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.EmailTxtbx.Location = new System.Drawing.Point(32, 319);
            this.EmailTxtbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EmailTxtbx.Name = "EmailTxtbx";
            this.EmailTxtbx.PlaceholderText = "";
            this.EmailTxtbx.SelectedText = "";
            this.EmailTxtbx.Size = new System.Drawing.Size(228, 36);
            this.EmailTxtbx.TabIndex = 54;
            this.EmailTxtbx.TextChanged += new System.EventHandler(this.EmailTxtbx_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(28, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 21);
            this.label5.TabIndex = 53;
            this.label5.Text = "E-mail";
            // 
            // PasswordTxtbx
            // 
            this.PasswordTxtbx.BorderRadius = 8;
            this.PasswordTxtbx.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PasswordTxtbx.DefaultText = "";
            this.PasswordTxtbx.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PasswordTxtbx.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PasswordTxtbx.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordTxtbx.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordTxtbx.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordTxtbx.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTxtbx.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordTxtbx.Location = new System.Drawing.Point(310, 243);
            this.PasswordTxtbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PasswordTxtbx.Name = "PasswordTxtbx";
            this.PasswordTxtbx.PlaceholderText = "";
            this.PasswordTxtbx.SelectedText = "";
            this.PasswordTxtbx.Size = new System.Drawing.Size(228, 36);
            this.PasswordTxtbx.TabIndex = 52;
            this.PasswordTxtbx.TextChanged += new System.EventHandler(this.PasswordTxtbx_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(306, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 21);
            this.label4.TabIndex = 51;
            this.label4.Text = "Password";
            // 
            // UserNameTxtbx
            // 
            this.UserNameTxtbx.BorderRadius = 8;
            this.UserNameTxtbx.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.UserNameTxtbx.DefaultText = "";
            this.UserNameTxtbx.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.UserNameTxtbx.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.UserNameTxtbx.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.UserNameTxtbx.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.UserNameTxtbx.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.UserNameTxtbx.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameTxtbx.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.UserNameTxtbx.Location = new System.Drawing.Point(32, 243);
            this.UserNameTxtbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UserNameTxtbx.Name = "UserNameTxtbx";
            this.UserNameTxtbx.PlaceholderText = "";
            this.UserNameTxtbx.SelectedText = "";
            this.UserNameTxtbx.Size = new System.Drawing.Size(228, 36);
            this.UserNameTxtbx.TabIndex = 50;
           // this.UserNameTxtbx.TextChanged += new System.EventHandler(this.UserNameTxtbx_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 21);
            this.label3.TabIndex = 49;
            this.label3.Text = "Username";
            // 
            // AddressTxtbx
            // 
            this.AddressTxtbx.BorderRadius = 8;
            this.AddressTxtbx.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.AddressTxtbx.DefaultText = "";
            this.AddressTxtbx.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.AddressTxtbx.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.AddressTxtbx.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.AddressTxtbx.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.AddressTxtbx.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.AddressTxtbx.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressTxtbx.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.AddressTxtbx.Location = new System.Drawing.Point(33, 166);
            this.AddressTxtbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AddressTxtbx.Name = "AddressTxtbx";
            this.AddressTxtbx.PlaceholderText = "";
            this.AddressTxtbx.SelectedText = "";
            this.AddressTxtbx.Size = new System.Drawing.Size(502, 36);
            this.AddressTxtbx.TabIndex = 48;
            this.AddressTxtbx.TextChanged += new System.EventHandler(this.AddressTxtbx_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(29, 142);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 21);
            this.label11.TabIndex = 47;
            this.label11.Text = "Address";
            // 
            // FullNameTxtbx
            // 
            this.FullNameTxtbx.BorderRadius = 8;
            this.FullNameTxtbx.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FullNameTxtbx.DefaultText = "";
            this.FullNameTxtbx.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.FullNameTxtbx.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.FullNameTxtbx.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.FullNameTxtbx.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.FullNameTxtbx.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.FullNameTxtbx.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FullNameTxtbx.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.FullNameTxtbx.Location = new System.Drawing.Point(309, 90);
            this.FullNameTxtbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FullNameTxtbx.Name = "FullNameTxtbx";
            this.FullNameTxtbx.PlaceholderText = "";
            this.FullNameTxtbx.SelectedText = "";
            this.FullNameTxtbx.Size = new System.Drawing.Size(228, 36);
            this.FullNameTxtbx.TabIndex = 46;
            this.FullNameTxtbx.TextChanged += new System.EventHandler(this.FullNameTxtbx_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(305, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 21);
            this.label2.TabIndex = 45;
            this.label2.Text = "Full Name";
            // 
            // AccountIDTxtbx
            // 
            this.AccountIDTxtbx.BorderRadius = 8;
            this.AccountIDTxtbx.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.AccountIDTxtbx.DefaultText = "";
            this.AccountIDTxtbx.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.AccountIDTxtbx.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.AccountIDTxtbx.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.AccountIDTxtbx.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.AccountIDTxtbx.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.AccountIDTxtbx.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountIDTxtbx.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.AccountIDTxtbx.Location = new System.Drawing.Point(32, 90);
            this.AccountIDTxtbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AccountIDTxtbx.Name = "AccountIDTxtbx";
            this.AccountIDTxtbx.PlaceholderText = "";
            this.AccountIDTxtbx.SelectedText = "";
            this.AccountIDTxtbx.Size = new System.Drawing.Size(228, 36);
            this.AccountIDTxtbx.TabIndex = 44;
            this.AccountIDTxtbx.TextChanged += new System.EventHandler(this.AccountIDTxtbx_TextChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(28, 66);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(83, 21);
            this.lblTitle.TabIndex = 43;
            this.lblTitle.Text = "Account ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lexend SemiBold", 12.5F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 27);
            this.label1.TabIndex = 42;
            this.label1.Text = "Add New User";
            // 
            // ClearBtn
            // 
            this.ClearBtn.BorderColor = System.Drawing.Color.Gainsboro;
            this.ClearBtn.BorderRadius = 8;
            this.ClearBtn.BorderThickness = 1;
            this.ClearBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ClearBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ClearBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ClearBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ClearBtn.FillColor = System.Drawing.Color.White;
            this.ClearBtn.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClearBtn.Location = new System.Drawing.Point(287, 466);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.PressedColor = System.Drawing.Color.Azure;
            this.ClearBtn.Size = new System.Drawing.Size(120, 40);
            this.ClearBtn.TabIndex = 63;
            this.ClearBtn.Text = "Clear";
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // ProceedBtn
            // 
            this.ProceedBtn.BorderRadius = 8;
            this.ProceedBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ProceedBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ProceedBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ProceedBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ProceedBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.ProceedBtn.Font = new System.Drawing.Font("Lexend SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProceedBtn.ForeColor = System.Drawing.Color.White;
            this.ProceedBtn.Location = new System.Drawing.Point(418, 466);
            this.ProceedBtn.Name = "ProceedBtn";
            this.ProceedBtn.Size = new System.Drawing.Size(120, 40);
            this.ProceedBtn.TabIndex = 62;
            this.ProceedBtn.Text = "Proceed";
            this.ProceedBtn.Click += new System.EventHandler(this.ProceedBtn_Click);
            // 
            // closeButton1
            // 
            this.closeButton1.BackColor = System.Drawing.Color.Transparent;
            this.closeButton1.Location = new System.Drawing.Point(524, 14);
            this.closeButton1.Name = "closeButton1";
            this.closeButton1.Size = new System.Drawing.Size(35, 35);
            this.closeButton1.TabIndex = 61;
            this.closeButton1.Click += new System.EventHandler(this.closeButton1_Click);
            // 
            // AddNewUser_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.ProceedBtn);
            this.Controls.Add(this.closeButton1);
            this.Controls.Add(this.RoleComboBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.AccountStatusComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DateTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.EmailTxtbx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PasswordTxtbx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UserNameTxtbx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AddressTxtbx);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.FullNameTxtbx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AccountIDTxtbx);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label1);
            this.Name = "AddNewUser_Form";
            this.Size = new System.Drawing.Size(567, 525);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControlFiles.CloseButton closeButton1;
        private Guna.UI2.WinForms.Guna2ComboBox RoleComboBox;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2ComboBox AccountStatusComboBox;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2TextBox DateTextBox;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2TextBox EmailTxtbx;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox PasswordTxtbx;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox UserNameTxtbx;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox AddressTxtbx;
        private System.Windows.Forms.Label label11;
        private Guna.UI2.WinForms.Guna2TextBox FullNameTxtbx;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox AccountIDTxtbx;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button ClearBtn;
        private Guna.UI2.WinForms.Guna2Button ProceedBtn;
    }
}
