using System;
using System.Net.Mail;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;

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

            // Attach key press events
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
                    LoginSuccess(loginResult);
                }
                else
                {
                    LoginFailed(loginResult.ErrorMessage);
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

        private void BypassLogin_Click(object sender, EventArgs e)
        {
            // Disable button to prevent multiple clicks
            BypassLogin.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // Create a fake login result for bypass - SET UserId = 1 for admin
                var bypassResult = new LoginResult
                {
                    IsAuthenticated = true,
                    UserId = 1,  // ADD THIS - Use the actual admin user ID
                    AccountID = "ACC-00001",
                    FullName = "Bypass User",
                    Username = "bypass",
                    Role = "Administrator"
                };

                LoginSuccess(bypassResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during bypass login: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable bypass button
                BypassLogin.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void LoginSuccess(LoginResult loginResult)
        {
            // MODIFIED: Store user session with UserId
            UserSession.UserId = loginResult.UserId;  // ADD THIS - Critical for audit logging!
            UserSession.AccountID = loginResult.AccountID;
            UserSession.FullName = loginResult.FullName;
            UserSession.Role = loginResult.Role;
            UserSession.Username = loginResult.Username;
            UserSession.IsLoggedIn = true;
            UserSession.LoginTime = DateTime.Now;

            // DEBUG: Verify session data
            Console.WriteLine($"DEBUG - Session initialized:");
            Console.WriteLine($"  UserId: {UserSession.UserId}");
            Console.WriteLine($"  AccountID: {UserSession.AccountID}");
            Console.WriteLine($"  Username: {UserSession.Username}");
            Console.WriteLine($"  IsLoggedIn: {UserSession.IsLoggedIn}");

            // Log the login activity using AuditHelper
            try
            {
                AuditHelper.Log(
                    AuditModule.AUTHENTICATION,
                    $"User {loginResult.Username} logged in successfully",
                    AuditActivityType.LOGIN,
                    tableAffected: "Accounts",
                    recordId: loginResult.AccountID
                );
                Console.WriteLine("DEBUG - Login audit log created successfully");
            }
            catch (Exception auditEx)
            {
                // Don't fail the login if audit logging fails, just log it
                Console.WriteLine($"WARNING - Audit log error: {auditEx.Message}");
            }

            // Open main dashboard
            MainDashBoard mainDashBoard = new MainDashBoard();
            mainDashBoard.Show();
            this.Hide();
        }

        private void LoginFailed(string errorMessage)
        {
            MessageBox.Show(errorMessage ?? "Invalid username or password. Please try again.",
                "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Clear password field
            actualPassword = string.Empty;
            Password.Clear();
            Email.Focus();
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