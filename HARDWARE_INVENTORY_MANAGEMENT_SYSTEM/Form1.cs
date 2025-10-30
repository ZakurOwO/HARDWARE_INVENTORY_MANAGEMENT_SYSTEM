using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public partial class LoginForm : Form
    {
        private string actualPassword = string.Empty;
        private bool isEyeOpen = false;
        private bool isChecked = false;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void pictureBoxEye_Click(object sender, EventArgs e)
        {
           
            isEyeOpen = !isEyeOpen;

            if (isEyeOpen)
            {
               
                Password.Text = actualPassword;
                pictureBoxEye.Image = Properties.Resources.EyeOpen;
            }
            else
            {
               
                Password.Text = new string('*', actualPassword.Length);
                pictureBoxEye.Image = Properties.Resources.EyeClosed;
            }

        
            Password.SelectionStart = Password.Text.Length;
            Password.SelectionLength = 0;
            Password.Focus();
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
        
            if (isEyeOpen)
            {
             
                actualPassword = Password.Text;
            }
            else
            {
              
                int cursorPos = Password.SelectionStart;

                if (Password.Text.Length < actualPassword.Length)
                {
                    actualPassword = actualPassword.Substring(0, Password.Text.Length);
                }
                else if (Password.Text.Length > actualPassword.Length)
                {
                    
                    string added = Password.Text.Substring(actualPassword.Length);
                    actualPassword += added;
                }

                
                Password.Text = new string('*', actualPassword.Length);
                Password.SelectionStart = cursorPos;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void CheckBoxToggle_Click(object sender, EventArgs e)
        {
            isChecked = !isChecked;

            if (isChecked)
            {
                CheckBoxToggle.Image = Properties.Resources.Enable;
            }
            else
            {
                CheckBoxToggle.Image = Properties.Resources.Disable;
            }
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            MainDashBoard mainDashBoard = new MainDashBoard();
            mainDashBoard.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Shapes1_Click(object sender, EventArgs e)
        {

        }
    }
}
