using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module
{
    public partial class AccountsMainPage : UserControl
    {
        private AddNewUser_Form addUserForm;
        private Panel overlayPanel;
        private Panel userDetailOverlayPanel;
        private ViewAccountsDetail_PopUp userDetailPopup;
        private Panel mainContentPanel; // Reference to your main content panel

        public AccountsMainPage()
        {
            InitializeComponent();
            InitializeOverlay();
            InitializeUserDetailOverlay();
            InitializeSearch();
            WireUpUserPanelEvents();

            // Find the main content panel (guna2Panel1 from your designer)
            mainContentPanel = guna2Panel1;

            addNewUserButton1.AddUserClicked += (s, e) => ShowAddUserForm();
        }

        private void InitializeUserDetailOverlay()
        {
            userDetailOverlayPanel = new Panel();
            userDetailOverlayPanel.Dock = DockStyle.Fill;
            userDetailOverlayPanel.BackColor = Color.White;
            userDetailOverlayPanel.Visible = false;
            userDetailOverlayPanel.Click += (s, e) => HideUserDetails();

            this.Controls.Add(userDetailOverlayPanel);
            userDetailOverlayPanel.SendToBack();
        }

        private void WireUpUserPanelEvents()
        {
            userAccountsPanel1.UserPanelClicked += (s, e) => ShowUserDetails(userAccountsPanel1);
            userAccountsPanel2.UserPanelClicked += (s, e) => ShowUserDetails(userAccountsPanel2);
        }

        private void ShowUserDetails(UserAccountsPanel clickedPanel)
        {
            if (userDetailPopup == null)
            {
                userDetailPopup = new ViewAccountsDetail_PopUp();
                userDetailPopup.Size = new Size(600, 400);
                userDetailPopup.Visible = false;
                userDetailPopup.ClosePopup += (s, e) => HideUserDetails();
                userDetailOverlayPanel.Controls.Add(userDetailPopup);
            }

            // HIDE the main content panel completely
            if (mainContentPanel != null)
            {
                mainContentPanel.Visible = false;
            }

            // Set user data
            userDetailPopup.UserName = clickedPanel._Name;
            userDetailPopup.Position = clickedPanel.Position;
            userDetailPopup.UserIcon = clickedPanel.Icon;

            // Center the popup
            userDetailPopup.Location = new Point(
                (this.Width - userDetailPopup.Width) / 2,
                (this.Height - userDetailPopup.Height) / 2
            );

            userDetailPopup.Visible = true;
            userDetailOverlayPanel.Visible = true;
            userDetailOverlayPanel.BringToFront();
            userDetailPopup.BringToFront();
        }

        private void HideUserDetails()
        {
            if (userDetailPopup != null)
            {
                userDetailPopup.Visible = false;
            }

            // SHOW the main content panel again
            if (mainContentPanel != null)
            {
                mainContentPanel.Visible = true;
            }

            userDetailOverlayPanel.Visible = false;
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
                addUserForm.UserAdded += (s, e) => HideAddUserForm();
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

        // Rest of your existing methods remain the same
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

            if (string.IsNullOrEmpty(searchText))
            {
                userAccountsPanel1.Visible = true;
                userAccountsPanel2.Visible = true;
                ResetPanelPositions();
            }
            else
            {
                string user1Name = "richard faulkerson";
                string user2Name = "dimpol navarro";
                bool user1Matches = user1Name.Contains(searchText);
                bool user2Matches = user2Name.Contains(searchText);
                userAccountsPanel1.Visible = user1Matches;
                userAccountsPanel2.Visible = user2Matches;
                ReorderPanels(user1Matches, user2Matches);
            }
        }

        private void ResetPanelPositions()
        {
            userAccountsPanel1.Location = new Point(28, 100);
            userAccountsPanel2.Location = new Point(318, 100);
        }

        private void ReorderPanels(bool user1Matches, bool user2Matches)
        {
            int xStart = 28, yPosition = 100, xSpacing = 290, currentX = xStart;
            if (user1Matches)
            {
                userAccountsPanel1.Location = new Point(currentX, yPosition);
                currentX += xSpacing;
            }
            if (user2Matches)
            {
                userAccountsPanel2.Location = new Point(currentX, yPosition);
                currentX += xSpacing;
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

        // Remove empty methods
        private void userAccountsPanel2_Load(object sender, EventArgs e) { }
        private void searchField1_Load(object sender, EventArgs e) { }
        private void Layout_Paint(object sender, PaintEventArgs e) { }
        private void userAccountsPanel1_Load(object sender, EventArgs e) { }
        private void OverlayPanel_Click(object sender, EventArgs e) { }
    }
}