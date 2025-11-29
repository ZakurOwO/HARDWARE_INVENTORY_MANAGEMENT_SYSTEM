using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.Class_Components_of_Accounts;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    public partial class AccountsMainPage : UserControl
    {
        private AddUserContainer addUserContainer = new AddUserContainer();
        private AddNewUser_Form addUserForm;
        private Panel overlayPanel;
        private Panel userDetailOverlayPanel;
        private ViewAccountsDetail_PopUp userDetailPopup;
        private Panel mainContentPanel;
        private List<UserAccountsPanel> userPanels = new List<UserAccountsPanel>();

        public AccountsMainPage()
        {
            InitializeComponent();
            //InitializeOverlay();
            InitializeUserDetailOverlay();
            InitializeSearch();
            LoadExistingUsersFromDatabase();

            mainContentPanel = guna2Panel1;
            //addNewUserButton1.AddUserClicked += (s, e) => ShowAddUserForm();
        }

        private void LoadExistingUsersFromDatabase()
        { 
            LayoutAccounts.Controls.Clear();
            userPanels.Clear();

            DataTable existingUsers = DatabaseHelper.LoadExistingUsersFromDatabase();

            if (existingUsers?.Rows.Count > 0)
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
        }

        private UserAccountsPanel CreateUserPanel(DataRow userRow)
        {
            UserAccountsPanel panel = new UserAccountsPanel();
            panel._Name = userRow.Field<string>("Fullname") ?? "N/A";
            panel.Position = userRow.Field<string>("Role") ?? "N/A";
            panel.Role = userRow.Field<string>("Role") ?? "N/A";
            panel.Status = userRow.Field<string>("Account_status") ?? "N/A";
            panel.Size = new Size(284, 128);
            panel.Margin = new Padding(5);

            return panel;
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
            if (clickedPanel?.Tag == null) return;

            DataRow userData = clickedPanel.Tag as DataRow;
            DataRow latestData = TryReloadUserData(userData);

            if (userDetailPopup == null)
            {
                userDetailPopup = new ViewAccountsDetail_PopUp();
                userDetailPopup.Size = new Size(600, 400);
                userDetailPopup.Visible = false;

                userDetailPopup.ClosePopup += (s, e) =>
                {
                    userDetailOverlayPanel.Visible = false;
                    userDetailPopup.Visible = false;
                    mainContentPanel.Visible = true;
                };

                userDetailOverlayPanel.Controls.Add(userDetailPopup);
            }

            mainContentPanel.Visible = false;
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
                if (string.IsNullOrEmpty(accountId)) return currentData;

                DataTable all = DatabaseHelper.LoadExistingUsersFromDatabase();
                if (all?.Rows.Count == 0) return currentData;

                return all.AsEnumerable()
                    .FirstOrDefault(row => string.Equals(row.Field<string>("AccountID"), accountId, StringComparison.OrdinalIgnoreCase)) ?? currentData;
            }
            catch
            {
                return currentData;
            }
        }

        private void OnUserAdded(object sender, (string AccountID, string FullName, string Role, string Status) userInfo)
        {
            if (!string.IsNullOrEmpty(userInfo.AccountID))
            {
                LoadExistingUsersFromDatabase();
            }
            
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

        
      //  private void ShowAddUserForm()
       // {
            /*
            if (addUserForm == null)
            {
                addUserForm = new AddNewUser_Form();
                addUserForm.Size = new Size(567, 525);
                addUserForm.Location = new Point(
                    (this.Width - addUserForm.Width) / 2,
                    (this.Height - addUserForm.Height) / 2
                );
*/
        //    addUserForm.UserAdded += OnUserAdded;
            /*
                overlayPanel.Controls.Add(addUserForm);
            }

            addUserForm.Visible = true;
            overlayPanel.Visible = true;
            overlayPanel.BringToFront();
            addUserForm.BringToFront();
        }
*/
         
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

            string searchText = GetSearchText().Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                ShowAllPanels();
                return;
            }

            FilterPanels(searchText);
        }

        private void ShowAllPanels()
        {
            foreach (UserAccountsPanel panel in userPanels)
            {
                panel.Visible = true;
            }
            ShowNoResultsMessage(false);
        }

        private void FilterPanels(string searchText)
        {
            bool anyResultsFound = false;

            foreach (UserAccountsPanel panel in userPanels)
            {
                // Progressive filtering: names starting with search text
                bool matches = panel._Name?.StartsWith(searchText, StringComparison.OrdinalIgnoreCase) ?? false;
                panel.Visible = matches;

                if (matches) anyResultsFound = true;
            }

            ShowNoResultsMessage(!anyResultsFound);
        }

        private void ShowNoResultsMessage(bool show)
        {
            var existingLabel = LayoutAccounts.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "NoResultsLabel");
            if (existingLabel != null)
            {
                LayoutAccounts.Controls.Remove(existingLabel);
            }

            if (show)
            {
                Label noResultsLabel = new Label
                {
                    Name = "NoResultsLabel",
                    Text = "No users found matching your search.",
                    Font = new Font("Segoe UI", 12, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Size = new Size(300, 50),
                    Dock = DockStyle.None,
                    Anchor = AnchorStyles.None,
                    Location = new Point(
                        (LayoutAccounts.Width - 300) / 2,
                        (LayoutAccounts.Height - 50) / 2
                    )
                };

                LayoutAccounts.Controls.Add(noResultsLabel);
                noResultsLabel.BringToFront();
            }
        }

        private string GetSearchText()
        {
            TextBox textBox = FindTextBoxInSearchField();
            string text = textBox?.Text ?? "";
            return text == "Search User" ? "" : text;
        }

        // Empty event handlers
        private void userAccountsPanel2_Load(object sender, EventArgs e) { }
        private void searchField1_Load(object sender, EventArgs e) { }
        private void Layout_Paint(object sender, PaintEventArgs e) { }
        private void userAccountsPanel1_Load(object sender, EventArgs e) { }
        private void OverlayPanel_Click(object sender, EventArgs e) { }

        private void btnMainButtonIcon_Click(object sender, EventArgs e)
        {
            MainDashBoard main = this.FindForm() as MainDashBoard;

            if (main != null)
            {
                addUserContainer.ShowAddUserForm(main);
            }
        }
    }
}