using System;
using System.Net.Mail;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

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

            // Attach key press events - FIXED: Use GmailAddress instead of Email
            Email.KeyPress += Email_KeyPress;
            Password.KeyPress += Password_KeyPress;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Focus on username field when form loads
            Email.Focus();

            // Test database connection on form load
            if (!DatabaseHelper.TestConnection())
            {
                MessageBox.Show("Unable to connect to the database. Please check your connection settings.",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // FIXED: Use GmailAddress instead of Email
            string username = Email.Text.Trim();
            string password = actualPassword.Trim();

            // Debug: Show what's being sent to authentication
            Console.WriteLine($"Attempting login with - Username: '{username}', Password: '{password}'");

            // Validate inputs
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter your username.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Email.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your password.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Password.Focus();
                return;
            }

            // Disable login button to prevent multiple clicks
            LoginButton.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // Authenticate user using AuthenticationService
                var loginResult = AuthenticationService.AuthenticateUser(username, password);

                if (loginResult.IsAuthenticated)
                {
                    // Store user session
                    UserSession.AccountID = loginResult.AccountID;
                    UserSession.FullName = loginResult.FullName;
                    UserSession.Role = loginResult.Role;
                    UserSession.Username = loginResult.Username;
                    UserSession.IsLoggedIn = true;

                    MessageBox.Show($"Welcome, {loginResult.FullName}!", "Login Successful",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Open main dashboard
                    MainDashBoard mainDashBoard = new MainDashBoard();
                    mainDashBoard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(loginResult.ErrorMessage ?? "Invalid username or password. Please try again.",
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Clear password field
                    actualPassword = string.Empty;
                    Password.Clear();
                    Email.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable login button
                LoginButton.Enabled = true;
                this.Cursor = Cursors.Default;
            }
            
        }

        private void Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                LoginButton_Click(sender, e);
                e.Handled = true;
            }
        }

        private void Email_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Password.Focus();
                e.Handled = true;
            }
        }

        // Empty event handlers
        private void label1_Click(object sender, EventArgs e) { }
        private void EmailTextBox_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void Email_TextChanged(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void guna2Shapes1_Click(object sender, EventArgs e) { }
    }
}