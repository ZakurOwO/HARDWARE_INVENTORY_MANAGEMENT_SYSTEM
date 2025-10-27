namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    partial class PageNumber
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
            this.PrevPageBtn = new Krypton.Toolkit.KryptonButton();
            this.NumberPagePrev = new Krypton.Toolkit.KryptonButton();
            this.NextPageBtn = new Krypton.Toolkit.KryptonButton();
            this.NumberPageNext = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // PrevPageBtn
            // 
            this.PrevPageBtn.Location = new System.Drawing.Point(4, 9);
            this.PrevPageBtn.Name = "PrevPageBtn";
            this.PrevPageBtn.Size = new System.Drawing.Size(30, 25);
            this.PrevPageBtn.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            this.PrevPageBtn.StateCommon.Back.Color2 = System.Drawing.Color.Transparent;
            this.PrevPageBtn.StateCommon.Border.Color1 = System.Drawing.Color.Transparent;
            this.PrevPageBtn.StateCommon.Border.Color2 = System.Drawing.Color.Transparent;
            this.PrevPageBtn.StateCommon.Content.Padding = new System.Windows.Forms.Padding(-1, -1, -1, 7);
            this.PrevPageBtn.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Lexend", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrevPageBtn.TabIndex = 0;
            this.PrevPageBtn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.PrevPageBtn.Values.Text = "<";
            this.PrevPageBtn.Click += new System.EventHandler(this.PrevPageBtn_Click);
            // 
            // NumberPagePrev
            // 
            this.NumberPagePrev.Location = new System.Drawing.Point(40, 9);
            this.NumberPagePrev.Name = "NumberPagePrev";
            this.NumberPagePrev.Size = new System.Drawing.Size(30, 25);
            this.NumberPagePrev.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            this.NumberPagePrev.StateCommon.Back.Color2 = System.Drawing.Color.Transparent;
            this.NumberPagePrev.StateCommon.Border.Color1 = System.Drawing.Color.DodgerBlue;
            this.NumberPagePrev.StateCommon.Border.Color2 = System.Drawing.Color.DodgerBlue;
            this.NumberPagePrev.StateCommon.Border.Rounding = 3F;
            this.NumberPagePrev.StateCommon.Content.Padding = new System.Windows.Forms.Padding(-2, -1, -1, 7);
            this.NumberPagePrev.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Lexend", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberPagePrev.TabIndex = 1;
            this.NumberPagePrev.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.NumberPagePrev.Values.Text = "1";
            this.NumberPagePrev.Click += new System.EventHandler(this.NumberPagePrev_Click);
            // 
            // NextPageBtn
            // 
            this.NextPageBtn.Location = new System.Drawing.Point(112, 9);
            this.NextPageBtn.Name = "NextPageBtn";
            this.NextPageBtn.Size = new System.Drawing.Size(30, 25);
            this.NextPageBtn.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            this.NextPageBtn.StateCommon.Back.Color2 = System.Drawing.Color.Transparent;
            this.NextPageBtn.StateCommon.Border.Color1 = System.Drawing.Color.Transparent;
            this.NextPageBtn.StateCommon.Border.Color2 = System.Drawing.Color.Transparent;
            this.NextPageBtn.StateCommon.Content.Padding = new System.Windows.Forms.Padding(-1, -1, -1, 7);
            this.NextPageBtn.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Lexend", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextPageBtn.TabIndex = 3;
            this.NextPageBtn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.NextPageBtn.Values.Text = ">";
            this.NextPageBtn.Click += new System.EventHandler(this.NextPageBtn_Click);
            // 
            // NumberPageNext
            // 
            this.NumberPageNext.Location = new System.Drawing.Point(76, 9);
            this.NumberPageNext.Name = "NumberPageNext";
            this.NumberPageNext.Size = new System.Drawing.Size(30, 25);
            this.NumberPageNext.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            this.NumberPageNext.StateCommon.Back.Color2 = System.Drawing.Color.Transparent;
            this.NumberPageNext.StateCommon.Border.Color1 = System.Drawing.Color.Transparent;
            this.NumberPageNext.StateCommon.Border.Color2 = System.Drawing.Color.Transparent;
            this.NumberPageNext.StateCommon.Border.Rounding = 3F;
            this.NumberPageNext.StateCommon.Content.Padding = new System.Windows.Forms.Padding(-2, -1, -1, 7);
            this.NumberPageNext.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Lexend", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberPageNext.TabIndex = 4;
            this.NumberPageNext.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.NumberPageNext.Values.Text = "2";
            this.NumberPageNext.Click += new System.EventHandler(this.NumberPageNext_Click);
            // 
            // PageNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NumberPageNext);
            this.Controls.Add(this.NextPageBtn);
            this.Controls.Add(this.NumberPagePrev);
            this.Controls.Add(this.PrevPageBtn);
            this.Name = "PageNumber";
            this.Size = new System.Drawing.Size(149, 44);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonButton PrevPageBtn;
        private Krypton.Toolkit.KryptonButton NumberPagePrev;
        private Krypton.Toolkit.KryptonButton NextPageBtn;
        private Krypton.Toolkit.KryptonButton NumberPageNext;
    }
}
