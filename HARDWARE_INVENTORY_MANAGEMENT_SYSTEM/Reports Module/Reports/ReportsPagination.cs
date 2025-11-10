using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class ReportsPagination : UserControl
    {
        public event EventHandler ShowPage1;
        public event EventHandler ShowPage2;
        public event EventHandler ShowPage3;
        public event EventHandler ShowPage4;

        public ReportsPagination()
        {
            InitializeComponent();
        }

        private void ReportsPagination_Load(object sender, EventArgs e)
        {
            SelectedTab(guna2Button5);
        }

        private void SelectedTab(Guna.UI2.WinForms.Guna2Button selectedButton)
        {
            //reset buttons
            guna2Button5.FillColor = Color.White;
            guna2Button5.ForeColor = Color.Black;
            guna2Button5.Font = new Font(guna2Button5.Font, FontStyle.Regular);

            guna2Button2.FillColor = Color.White;
            guna2Button2.ForeColor = Color.Black;
            guna2Button2.Font = new Font(guna2Button2.Font, FontStyle.Regular);

            guna2Button1.FillColor = Color.White;
            guna2Button1.ForeColor = Color.Black;
            guna2Button1.Font = new Font(guna2Button1.Font, FontStyle.Regular);

            guna2Button3.FillColor = Color.White;
            guna2Button3.ForeColor = Color.Black;
            guna2Button3.Font = new Font(guna2Button3.Font, FontStyle.Regular);

            //highlight selected button
            selectedButton.FillColor = Color.FromArgb(229, 240, 249); //light blue
            selectedButton.ForeColor = Color.FromArgb(42, 134, 205);   //dark blue
            selectedButton.Font = new Font(selectedButton.Font, FontStyle.Bold);
            selectedButton.BorderRadius = 5;
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            //page 1
            SelectedTab(guna2Button5);
            ShowPage1?.Invoke(this, EventArgs.Empty);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //page 2
            SelectedTab(guna2Button2);
            ShowPage2?.Invoke(this, EventArgs.Empty);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //page 3
            SelectedTab(guna2Button1);
            ShowPage3?.Invoke(this, EventArgs.Empty);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            //page 4
            SelectedTab(guna2Button3);
            ShowPage4?.Invoke(this, EventArgs.Empty);
        }
    }
}
