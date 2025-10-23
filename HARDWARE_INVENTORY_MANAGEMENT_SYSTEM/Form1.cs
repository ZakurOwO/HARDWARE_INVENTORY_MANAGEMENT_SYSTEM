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
        }

        private void PasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                if (actualPassword.Length > 0)
                    actualPassword = actualPassword.Substring(0, actualPassword.Length - 1);

                if (Password.Text.Length > 0)
                    Password.Text = Password.Text.Substring(0, Password.Text.Length - 1);

                e.Handled = true;
                Password.SelectionStart = Password.Text.Length;
                return;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                return;
            }

            actualPassword += e.KeyChar;

            if (isEyeOpen)
            {
                Password.Text = actualPassword;
            }
            else
            {
                Password.Text += "*";
            }

            e.Handled = true;
            Password.SelectionStart = Password.Text.Length;
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
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
    }
}
