namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    partial class Checkout_PopUp
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
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.pnlChangeAmount = new Guna.UI2.WinForms.Guna2Panel();
            this.lblChangeAmount = new System.Windows.Forms.Label();
            this.cbxPaymentMethod = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Totallbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SubTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxCashReceived = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblCashReceived = new System.Windows.Forms.Label();
            this.guna2Button3 = new Guna.UI2.WinForms.Guna2Button();
            this.pnlChangeAmount.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 8;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(196)))));
            this.guna2Button1.Font = new System.Drawing.Font("Lexend SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(350, 345);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(140, 35);
            this.guna2Button1.TabIndex = 12;
            this.guna2Button1.Text = "Proceed Payment";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // pnlChangeAmount
            // 
            this.pnlChangeAmount.BorderRadius = 10;
            this.pnlChangeAmount.Controls.Add(this.lblChangeAmount);
            this.pnlChangeAmount.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(255)))), ((int)(((byte)(224)))));
            this.pnlChangeAmount.Location = new System.Drawing.Point(35, 277);
            this.pnlChangeAmount.Name = "pnlChangeAmount";
            this.pnlChangeAmount.Size = new System.Drawing.Size(450, 44);
            this.pnlChangeAmount.TabIndex = 11;
            this.pnlChangeAmount.Visible = false;
            // 
            // lblChangeAmount
            // 
            this.lblChangeAmount.AutoSize = true;
            this.lblChangeAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblChangeAmount.Font = new System.Drawing.Font("Lexend SemiBold", 10F, System.Drawing.FontStyle.Bold);
            this.lblChangeAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(172)))), ((int)(((byte)(90)))));
            this.lblChangeAmount.Location = new System.Drawing.Point(7, 10);
            this.lblChangeAmount.Name = "lblChangeAmount";
            this.lblChangeAmount.Size = new System.Drawing.Size(129, 22);
            this.lblChangeAmount.TabIndex = 4;
            this.lblChangeAmount.Text = "Change: P 262.00";
            this.lblChangeAmount.Visible = false;
            this.lblChangeAmount.Click += new System.EventHandler(this.lblChangeAmount_Click);
            // 
            // cbxPaymentMethod
            // 
            this.cbxPaymentMethod.BackColor = System.Drawing.Color.Transparent;
            this.cbxPaymentMethod.BorderRadius = 8;
            this.cbxPaymentMethod.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPaymentMethod.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbxPaymentMethod.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbxPaymentMethod.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbxPaymentMethod.ItemHeight = 30;
            this.cbxPaymentMethod.Items.AddRange(new object[] {
            "Cash",
            "Credit Card",
            "GCash",
            "Check"});
            this.cbxPaymentMethod.Location = new System.Drawing.Point(35, 223);
            this.cbxPaymentMethod.Name = "cbxPaymentMethod";
            this.cbxPaymentMethod.Size = new System.Drawing.Size(450, 36);
            this.cbxPaymentMethod.TabIndex = 10;
            this.cbxPaymentMethod.SelectedIndexChanged += new System.EventHandler(this.cbxPaymentMethod_SelectedIndexChanged);
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Font = new System.Drawing.Font("Lexend SemiBold", 10F, System.Drawing.FontStyle.Bold);
            this.lblPaymentMethod.Location = new System.Drawing.Point(31, 197);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(130, 22);
            this.lblPaymentMethod.TabIndex = 9;
            this.lblPaymentMethod.Text = "Payment Method";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.Controls.Add(this.label7);
            this.guna2Panel1.Controls.Add(this.label6);
            this.guna2Panel1.Controls.Add(this.label5);
            this.guna2Panel1.Controls.Add(this.Totallbl);
            this.guna2Panel1.Controls.Add(this.label3);
            this.guna2Panel1.Controls.Add(this.label2);
            this.guna2Panel1.Controls.Add(this.SubTotal);
            this.guna2Panel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.guna2Panel1.Location = new System.Drawing.Point(33, 65);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(452, 109);
            this.guna2Panel1.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkGray;
            this.label7.Location = new System.Drawing.Point(10, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(434, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "_____________________________________________________________";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Lexend Light", 10F);
            this.label6.Location = new System.Drawing.Point(320, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 22);
            this.label6.TabIndex = 5;
            this.label6.Text = "P 0.00";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Lexend Light", 10F);
            this.label5.Location = new System.Drawing.Point(328, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 22);
            this.label5.TabIndex = 4;
            this.label5.Text = "P 6,238.00";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // Totallbl
            // 
            this.Totallbl.BackColor = System.Drawing.Color.Transparent;
            this.Totallbl.Font = new System.Drawing.Font("Lexend SemiBold", 12.5F, System.Drawing.FontStyle.Bold);
            this.Totallbl.Location = new System.Drawing.Point(293, 70);
            this.Totallbl.Name = "Totallbl";
            this.Totallbl.Size = new System.Drawing.Size(150, 27);
            this.Totallbl.TabIndex = 3;
            this.Totallbl.Text = "P 6,238.00";
            this.Totallbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.Totallbl.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Lexend SemiBold", 12.5F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(7, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 27);
            this.label3.TabIndex = 2;
            this.label3.Text = "Total:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Lexend Light", 10F);
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tax:";
            // 
            // SubTotal
            // 
            this.SubTotal.AutoSize = true;
            this.SubTotal.BackColor = System.Drawing.Color.Transparent;
            this.SubTotal.Font = new System.Drawing.Font("Lexend Light", 10F);
            this.SubTotal.Location = new System.Drawing.Point(9, 10);
            this.SubTotal.Name = "SubTotal";
            this.SubTotal.Size = new System.Drawing.Size(71, 22);
            this.SubTotal.TabIndex = 0;
            this.SubTotal.Text = "SubTotal:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lexend SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "Checkout";
            // 
            // tbxCashReceived
            // 
            this.tbxCashReceived.BorderRadius = 8;
            this.tbxCashReceived.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxCashReceived.DefaultText = "";
            this.tbxCashReceived.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbxCashReceived.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbxCashReceived.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxCashReceived.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbxCashReceived.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxCashReceived.Font = new System.Drawing.Font("Lexend Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCashReceived.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbxCashReceived.Location = new System.Drawing.Point(277, 223);
            this.tbxCashReceived.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbxCashReceived.Name = "tbxCashReceived";
            this.tbxCashReceived.PlaceholderText = "";
            this.tbxCashReceived.SelectedText = "";
            this.tbxCashReceived.Size = new System.Drawing.Size(208, 36);
            this.tbxCashReceived.TabIndex = 14;
            this.tbxCashReceived.Visible = false;
            // 
            // lblCashReceived
            // 
            this.lblCashReceived.AutoSize = true;
            this.lblCashReceived.Font = new System.Drawing.Font("Lexend SemiBold", 10F, System.Drawing.FontStyle.Bold);
            this.lblCashReceived.Location = new System.Drawing.Point(274, 196);
            this.lblCashReceived.Name = "lblCashReceived";
            this.lblCashReceived.Size = new System.Drawing.Size(112, 22);
            this.lblCashReceived.TabIndex = 13;
            this.lblCashReceived.Text = "Cash Received";
            this.lblCashReceived.Visible = false;
            // 
            // guna2Button3
            // 
            this.guna2Button3.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button3.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.remove;
            this.guna2Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.guna2Button3.BorderColor = System.Drawing.Color.Transparent;
            this.guna2Button3.BorderRadius = 8;
            this.guna2Button3.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button3.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button3.ForeColor = System.Drawing.Color.White;
            this.guna2Button3.Location = new System.Drawing.Point(460, 16);
            this.guna2Button3.Name = "guna2Button3";
            this.guna2Button3.Size = new System.Drawing.Size(30, 30);
            this.guna2Button3.TabIndex = 47;
            this.guna2Button3.Click += new System.EventHandler(this.guna2Button3_Click);
            // 
            // Checkout_PopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.guna2Button3);
            this.Controls.Add(this.cbxPaymentMethod);
            this.Controls.Add(this.tbxCashReceived);
            this.Controls.Add(this.lblCashReceived);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.pnlChangeAmount);
            this.Controls.Add(this.lblPaymentMethod);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.label1);
            this.Name = "Checkout_PopUp";
            this.Size = new System.Drawing.Size(515, 402);
            this.pnlChangeAmount.ResumeLayout(false);
            this.pnlChangeAmount.PerformLayout();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Panel pnlChangeAmount;
        private System.Windows.Forms.Label lblChangeAmount;
        private Guna.UI2.WinForms.Guna2ComboBox cbxPaymentMethod;
        private System.Windows.Forms.Label lblPaymentMethod;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Totallbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label SubTotal;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox tbxCashReceived;
        private System.Windows.Forms.Label lblCashReceived;
        private Guna.UI2.WinForms.Guna2Button guna2Button3;
    }
}
