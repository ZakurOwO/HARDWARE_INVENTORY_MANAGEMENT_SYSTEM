using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    public partial class AccountsMainPage : UserControl
    {
        private AddNewUser_Form addUserForm;
        private Panel overlayPanel;
        private Panel userDetailOverlayPanel;
        private ViewAccountsDetail_PopUp userDetailPopup;
        private Panel mainContentPanel;
        private List<UserAccountsPanel> userPanels = new List<UserAccountsPanel>();

        public AccountsMainPage()
        {
            InitializeComponent();
            InitializeOverlay();
            InitializeUserDetailOverlay();
            InitializeSearch();

            LoadExistingUsersFromDatabase();

            mainContentPanel = guna2Panel1;

            addNewUserButton1.AddUserClicked += (s, e) => ShowAddUserForm();
        }

        private void LoadExistingUsersFromDatabase()
        {
            LayoutAccounts.Controls.Clear();
            userPanels.Clear();

            // Load users from database using DatabaseHelper
            DataTable existingUsers = DatabaseHelper.LoadExistingUsersFromDatabase();

            if (existingUsers != null && existingUsers.Rows.Count > 0)
            {
                foreach (DataRow userRow in existingUsers.Rows)
                {
                    UserAccountsPanel userPanel = CreateUserPanel(userRow);
                    userPanel.Tag = userRow;
                    userPanels.Add(userPanel);
                    LayoutAccounts.Controls.Add(userPanel);
                }

                WireUpUserPanelEvents();
            }
            else
            {
                Console.WriteLine("No users found in database.");
            }
        }

        private UserAccountsPanel CreateUserPanel(DataRow userRow)
        {
            UserAccountsPanel panel = new UserAccountsPanel();

            panel._Name = userRow.Field<string>("Fullname") ?? "N/A";
            panel.Position = userRow.Field<string>("Role") ?? "N/A";
            panel.Role = userRow.Field<string>("Role") ?? "N/A";
            panel.Status = userRow.Field<string>("Account_status") ?? "N/A";

            SetPanelIconByStatus(panel, userRow.Field<string>("Account_status"));

            panel.Size = new Size(284, 128);
            panel.Margin = new Padding(5);

            return panel;
        }

        private void SetPanelIconByStatus(UserAccountsPanel panel, string status)
        {
            try
            {
                string resourceName = status?.ToLower() == "active"
                    ? "Employees_1"
                    : "Employees_2";

                var image = Properties.Resources.ResourceManager.GetObject(resourceName) as Image;

                if (image == null)
                {
                    resourceName = status?.ToLower() == "active"
                        ? "Employees1"
                        : "Employees2";
                    image = Properties.Resources.ResourceManager.GetObject(resourceName) as Image;
                }

                if (image != null)
                {
                    // Set image if your panel has an image property
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading panel icon: {ex.Message}");
            }
        }

        private void WireUpUserPanelEvents()
        {
            foreach (var panel in userPanels)
            {
                panel.UserPanelClicked += (s, e) => ShowUserDetails((UserAccountsPanel)s);
            }
        }

        private void ShowUserDetails(UserAccountsPanel clickedPanel)
        {
            if (clickedPanel == null || clickedPanel.Tag == null) return;

            DataRow userData = clickedPanel.Tag as DataRow;
            DataRow latestData = TryReloadUserData(userData);

            if (userDetailPopup == null)
            {
                userDetailPopup = new ViewAccountsDetail_PopUp();
                userDetailPopup.Size = new Size(600, 400);
                userDetailPopup.Visible = false;
                userDetailOverlayPanel.Controls.Add(userDetailPopup);
            }

            if (mainContentPanel != null)
            {
                mainContentPanel.Visible = false;
            }

            userDetailPopup.PopulateFromDataRow(latestData ?? userData);

            userDetailPopup.Location = new Point(
                (this.Width - userDetailPopup.Width) / 2,
                (this.Height - userDetailPopup.Height) / 2
            );

            userDetailPopup.Visible = true;
            userDetailOverlayPanel.Visible = true;
            userDetailOverlayPanel.BringToFront();
            userDetailPopup.BringToFront();
        }

        private DataRow TryReloadUserData(DataRow currentData)
        {
            try
            {
                if (currentData == null) return currentData;

                string accountId = currentData.Field<string>("AccountID");
                if (string.IsNullOrEmpty(accountId))
                    return currentData;

                DataTable all = DatabaseHelper.LoadExistingUsersFromDatabase();
                if (all == null || all.Rows.Count == 0) return currentData;

                var found = all.AsEnumerable()
                    .FirstOrDefault(row => string.Equals(row.Field<string>("AccountID"), accountId, StringComparison.OrdinalIgnoreCase));

                return found ?? currentData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reloading user data: " + ex.Message);
                return currentData;
            }
        }

        private void OnUserAdded(object sender, (string AccountID, string FullName, string Role, string Status) userInfo)
        {
            if (!string.IsNullOrEmpty(userInfo.AccountID))
            {
                LoadExistingUsersFromDatabase();
            }

            HideAddUserForm();
        }

        private void InitializeUserDetailOverlay()
        {
            userDetailOverlayPanel = new Panel();
            userDetailOverlayPanel.Dock = DockStyle.Fill;
            userDetailOverlayPanel.BackColor = Color.White;
            userDetailOverlayPanel.Visible = false;

            this.Controls.Add(userDetailOverlayPanel);
            userDetailOverlayPanel.SendToBack();
        }

        private void InitializeOverlay()
        {
            overlayPanel = new Panel();
            overlayPanel.Dock = DockStyle.Fill;
            overlayPanel.BackColor = Color.FromArgb(200, 0, 0, 0);
            overlayPanel.Visible = false;
            overlayPanel.Click += (s, e) => HideAddUserForm();

            this.Controls.Add(overlayPanel);
            overlayPanel.SendToBack();
        }

        private void ShowAddUserForm()
        {
            if (addUserForm == null)
            {
                addUserForm = new AddNewUser_Form();
                addUserForm.Size = new Size(567, 525);
                addUserForm.Location = new Point(
                    (this.Width - addUserForm.Width) / 2,
                    (this.Height - addUserForm.Height) / 2
                );
                addUserForm.UserAdded += OnUserAdded;
                overlayPanel.Controls.Add(addUserForm);
            }

            addUserForm.Visible = true;
            overlayPanel.Visible = true;
            overlayPanel.BringToFront();
            addUserForm.BringToFront();
        }

        private void HideAddUserForm()
        {
            overlayPanel.Visible = false;
            overlayPanel.SendToBack();
        }

        private void InitializeSearch()
        {
            TextBox textBox = FindTextBoxInSearchField();
            if (textBox != null)
            {
                textBox.TextChanged += SearchField_TextChanged;
                textBox.Enter += SearchField_Enter;
                textBox.Leave += SearchField_Leave;
                textBox.MaxLength = 20;
                SetPlaceholderText();
            }
        }

        private TextBox FindTextBoxInSearchField()
        {
            return FindControlRecursive<TextBox>(searchField1);
        }

        private T FindControlRecursive<T>(Control parent) where T : Control
        {
            if (parent == null) return null;
            foreach (Control control in parent.Controls)
            {
                if (control is T found) return found;
                var child = FindControlRecursive<T>(control);
                if (child != null) return child;
            }
            return null;
        }

        private void SetPlaceholderText()
        {
            TextBox textBox = FindTextBoxInSearchField();
            if (textBox != null && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Search User";
                textBox.ForeColor = Color.Gray;
            }
        }

        private void RemovePlaceholderText()
        {
            TextBox textBox = FindTextBoxInSearchField();
            if (textBox != null && textBox.Text == "Search User")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void SearchField_Enter(object sender, EventArgs e)
        {
            RemovePlaceholderText();
        }

        private void SearchField_Leave(object sender, EventArgs e)
        {
            TextBox textBox = FindTextBoxInSearchField();
            if (textBox != null && string.IsNullOrEmpty(textBox.Text))
            {
                SetPlaceholderText();
            }
        }

        private void SearchField_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = FindTextBoxInSearchField();
            if (textBox != null && textBox.Text == "Search User") return;

            string searchText = GetSearchText().ToLower().Trim();

            foreach (UserAccountsPanel panel in userPanels)
            {
                bool matches = panel._Name.ToLower().Contains(searchText) ||
                              panel.Position.ToLower().Contains(searchText) ||
                              panel.Role.ToLower().Contains(searchText);
                panel.Visible = matches;
            }
        }

        private void ClearSearchField()
        {
            TextBox textBox = FindTextBoxInSearchField();
            if (textBox != null)
            {
                textBox.Clear();
                SetPlaceholderText();
            }
        }

        private string GetSearchText()
        {
            TextBox textBox = FindTextBoxInSearchField();
            string text = textBox?.Text ?? "";
            return text == "Search User" ? "" : text;
        }

        private void userAccountsPanel2_Load(object sender, EventArgs e) { }
        private void searchField1_Load(object sender, EventArgs e) { }
        private void Layout_Paint(object sender, PaintEventArgs e) { }
        private void userAccountsPanel1_Load(object sender, EventArgs e) { }
        private void OverlayPanel_Click(object sender, EventArgs e) { }
    }
}