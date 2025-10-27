namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    partial class LoginForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Krypton.Toolkit.KryptonRichTextBox Email;
            this.label1 = new System.Windows.Forms.Label();
            this.Password = new Krypton.Toolkit.KryptonRichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LoginButton = new Krypton.Toolkit.KryptonButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.guna2Shapes1 = new Guna.UI2.WinForms.Guna2Shapes();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.CheckBoxToggle = new System.Windows.Forms.PictureBox();
            this.pictureBoxEye = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pageNumber1 = new HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module.PageNumber();
            Email = new Krypton.Toolkit.KryptonRichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckBoxToggle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Email
            // 
            Email.CueHint.Color1 = System.Drawing.Color.DodgerBlue;
            Email.CueHint.Font = new System.Drawing.Font("Lexend Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Email.CueHint.Padding = new System.Windows.Forms.Padding(6, 0, 25, 23);
            Email.InputControlStyle = Krypton.Toolkit.InputControlStyle.PanelClient;
            Email.Location = new System.Drawing.Point(696, 307);
            Email.Name = "Email";
            Email.PaletteMode = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            Email.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            Email.Size = new System.Drawing.Size(434, 58);
            Email.StateCommon.Back.Color1 = System.Drawing.Color.White;
            Email.StateCommon.Border.Color1 = System.Drawing.Color.DodgerBlue;
            Email.StateCommon.Border.Color2 = System.Drawing.Color.DodgerBlue;
            Email.StateCommon.Border.Rounding = 10F;
            Email.StateCommon.Border.Width = 1;
            Email.StateCommon.Content.Color1 = System.Drawing.Color.Black;
            Email.StateCommon.Content.Font = new System.Drawing.Font("Lexend Light", 10F);
            Email.StateCommon.Content.Padding = new System.Windows.Forms.Padding(12, 26, 0, 0);
            Email.TabIndex = 3;
            Email.Text = "";
            Email.TextChanged += new System.EventHandler(this.Email_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(692, 256);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please login here";
            // 
            // Password
            // 
            this.Password.CueHint.Color1 = System.Drawing.Color.DodgerBlue;
            this.Password.CueHint.Font = new System.Drawing.Font("Lexend Light", 11.25F);
            this.Password.CueHint.Padding = new System.Windows.Forms.Padding(10, 0, 25, 23);
            this.Password.InputControlStyle = Krypton.Toolkit.InputControlStyle.PanelClient;
            this.Password.Location = new System.Drawing.Point(696, 380);
            this.Password.Multiline = false;
            this.Password.Name = "Password";
            this.Password.PaletteMode = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            this.Password.Size = new System.Drawing.Size(434, 58);
            this.Password.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.Password.StateCommon.Border.Color1 = System.Drawing.Color.DodgerBlue;
            this.Password.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.Password.StateCommon.Border.Rounding = 10F;
            this.Password.StateCommon.Border.Width = 1;
            this.Password.StateCommon.Content.Font = new System.Drawing.Font("Lexend Light", 10F);
            this.Password.StateCommon.Content.Padding = new System.Windows.Forms.Padding(12, 26, 0, 0);
            this.Password.TabIndex = 4;
            this.Password.Text = "";
            this.Password.TextChanged += new System.EventHandler(this.Password_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Lexend Light", 11F);
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(732, 448);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "Remember Me";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(696, 496);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(434, 59);
            this.LoginButton.StateCommon.Back.Color1 = System.Drawing.Color.DodgerBlue;
            this.LoginButton.StateCommon.Back.Color2 = System.Drawing.Color.DodgerBlue;
            this.LoginButton.StateCommon.Border.Rounding = 10F;
            this.LoginButton.StateCommon.Border.Width = 1;
            this.LoginButton.StateCommon.Content.LongText.Font = new System.Drawing.Font("Lexend Light", 10F);
            this.LoginButton.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.LoginButton.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.LoginButton.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Lexend Light", 10F);
            this.LoginButton.TabIndex = 8;
            this.LoginButton.UseMnemonic = false;
            this.LoginButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.LoginButton.Values.Text = "Login";
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Lexend Light", 10F);
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(708, 314);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 22);
            this.label3.TabIndex = 9;
            this.label3.Text = "Email Address:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Lexend Light", 10F);
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(708, 387);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 22);
            this.label4.TabIndex = 10;
            this.label4.Text = "Password:";
            // 
            // guna2Shapes1
            // 
            this.guna2Shapes1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Shapes1.BorderColor = System.Drawing.Color.Transparent;
            this.guna2Shapes1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.guna2Shapes1.Location = new System.Drawing.Point(-28, -52);
            this.guna2Shapes1.Name = "guna2Shapes1";
            this.guna2Shapes1.PolygonSkip = 1;
            this.guna2Shapes1.Rotate = 0F;
            this.guna2Shapes1.Shape = Guna.UI2.WinForms.Enums.ShapeType.Rounded;
            this.guna2Shapes1.Size = new System.Drawing.Size(708, 816);
            this.guna2Shapes1.TabIndex = 12;
            this.guna2Shapes1.Text = "guna2Shapes1";
            this.guna2Shapes1.Zoom = 80;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.label5.Font = new System.Drawing.Font("Lexend SemiBold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(118, 408);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(427, 77);
            this.label5.TabIndex = 14;
            this.label5.Text = "Topaz Hardware";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.pictureBox2.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Topaz_Icon__3_;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(214, 187);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(231, 218);
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // CheckBoxToggle
            // 
            this.CheckBoxToggle.BackColor = System.Drawing.Color.White;
            this.CheckBoxToggle.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Disable;
            this.CheckBoxToggle.Location = new System.Drawing.Point(700, 443);
            this.CheckBoxToggle.Name = "CheckBoxToggle";
            this.CheckBoxToggle.Size = new System.Drawing.Size(30, 30);
            this.CheckBoxToggle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.CheckBoxToggle.TabIndex = 6;
            this.CheckBoxToggle.TabStop = false;
            this.CheckBoxToggle.Click += new System.EventHandler(this.CheckBoxToggle_Click);
            // 
            // pictureBoxEye
            // 
            this.pictureBoxEye.BackColor = System.Drawing.Color.White;
            this.pictureBoxEye.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.EyeClosed;
            this.pictureBoxEye.Location = new System.Drawing.Point(1083, 390);
            this.pictureBoxEye.Name = "pictureBoxEye";
            this.pictureBoxEye.Size = new System.Drawing.Size(36, 36);
            this.pictureBoxEye.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxEye.TabIndex = 5;
            this.pictureBoxEye.TabStop = false;
            this.pictureBoxEye.Click += new System.EventHandler(this.pictureBoxEye_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.WelcomebackFabiana;
            this.pictureBox1.Location = new System.Drawing.Point(686, 203);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(188, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pageNumber1
            // 
            this.pageNumber1.Location = new System.Drawing.Point(637, 119);
            this.pageNumber1.Name = "pageNumber1";
            this.pageNumber1.Size = new System.Drawing.Size(149, 44);
            this.pageNumber1.TabIndex = 15;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1194, 711);
            this.Controls.Add(this.pageNumber1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CheckBoxToggle);
            this.Controls.Add(this.pictureBoxEye);
            this.Controls.Add(this.Password);
            this.Controls.Add(Email);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.guna2Shapes1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckBoxToggle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private Krypton.Toolkit.KryptonRichTextBox Password;
        private System.Windows.Forms.PictureBox pictureBoxEye;
        private System.Windows.Forms.PictureBox CheckBoxToggle;
        private System.Windows.Forms.Label label2;
        private Krypton.Toolkit.KryptonButton LoginButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2Shapes guna2Shapes1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label5;
        private Customer_Module.PageNumber pageNumber1;
    }
}

