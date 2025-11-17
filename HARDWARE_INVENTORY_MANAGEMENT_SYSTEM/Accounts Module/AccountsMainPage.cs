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

        public AccountsMainPage()
        {
            InitializeComponent();
            InitializeOverlay();
            InitializeSearch();

            // Wire up the AddNewUserButton's event
            addNewUserButton1.AddUserClicked += (s, e) => ShowAddUserForm();
        }

        private void InitializeSearch()
        {
            // Find the actual TextBox inside searchField1
            TextBox textBox = FindTextBoxInSearchField();
            if (textBox != null)
            {
                textBox.TextChanged += SearchField_TextChanged;
                textBox.Enter += SearchField_Enter;
                textBox.Leave += SearchField_Leave;
                textBox.MaxLength = 20; // Set 20 character limit

                // Set initial placeholder text
                SetPlaceholderText();
            }
        }

        private TextBox FindTextBoxInSearchField()
        {
            // Recursively search for a TextBox in searchField1
            return FindControlRecursive<TextBox>(searchField1);
        }

        private T FindControlRecursive<T>(Control parent) where T : Control
        {
            if (parent == null) return null;

            foreach (Control control in parent.Controls)
            {
                if (control is T found)
                    return found;

                var child = FindControlRecursive<T>(control);
                if (child != null)
                    return child;
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
            // When user clicks the search field, remove placeholder
            RemovePlaceholderText();
        }

        private void SearchField_Leave(object sender, EventArgs e)
        {
            // When user leaves the search field, show placeholder if empty
            TextBox textBox = FindTextBoxInSearchField();
            if (textBox != null && string.IsNullOrEmpty(textBox.Text))
            {
                SetPlaceholderText();
            }
        }

        private void SearchField_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = FindTextBoxInSearchField();
            if (textBox != null && textBox.Text == "Search User")
            {
                // Ignore text changes when it's just the placeholder
                return;
            }

            string searchText = GetSearchText().ToLower().Trim();

            // Filter and reorder by any matching text (not just first letter)
            if (string.IsNullOrEmpty(searchText))
            {
                // Show all panels in original order
                userAccountsPanel1.Visible = true;
                userAccountsPanel2.Visible = true;

                // Reset to original positions
                ResetPanelPositions();
            }
            else
            {
                string user1Name = "richard faulkerson";
                string user2Name = "dimpol navarro";

                // Check if the name CONTAINS the search text (anywhere in the name)
                bool user1Matches = user1Name.Contains(searchText);
                bool user2Matches = user2Name.Contains(searchText);

                // Set visibility
                userAccountsPanel1.Visible = user1Matches;
                userAccountsPanel2.Visible = user2Matches;

                // Reorder panels - matching ones come first
                ReorderPanels(user1Matches, user2Matches);
            }
        }

        private void ResetPanelPositions()
        {
            // Reset to original positions from your designer
            userAccountsPanel1.Location = new Point(28, 100);  // Original position
            userAccountsPanel2.Location = new Point(318, 100); // Original position
        }

        private void ReorderPanels(bool user1Matches, bool user2Matches)
        {
            int xStart = 28;  // Starting X position
            int yPosition = 100; // Y position
            int xSpacing = 290; // Horizontal spacing between panels

            int currentX = xStart;

            // Show matching panels first
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
                SetPlaceholderText(); // Restore placeholder after clearing
            }
        }

        private string GetSearchText()
        {
            // Get text from the TextBox inside searchField1
            TextBox textBox = FindTextBoxInSearchField();
            string text = textBox?.Text ?? "";

            // If it's placeholder text, return empty
            if (text == "Search User")
                return "";

            return text;
        }

        private void InitializeOverlay()
        {
            // Create overlay panel
            overlayPanel = new Panel();
            overlayPanel.Dock = DockStyle.Fill;
            overlayPanel.BackColor = Color.Transparent;
            overlayPanel.Visible = false;
            overlayPanel.Click += OverlayPanel_Click;

            this.Controls.Add(overlayPanel);
            overlayPanel.SendToBack();
        }

        private void OverlayPanel_Click(object sender, EventArgs e)
        {
            HideAddUserForm();
        }

        private void ShowAddUserForm()
        {
            if (addUserForm == null)
            {
                addUserForm = new AddNewUser_Form();
                addUserForm.Size = new Size(567, 525);

                // Center the form in the overlay
                addUserForm.Location = new Point(
                    (this.Width - addUserForm.Width) / 2,
                    (this.Height - addUserForm.Height) / 2
                );

                // Handle the UserAdded event to close the form
                addUserForm.UserAdded += (s, e) => HideAddUserForm();

                overlayPanel.Controls.Add(addUserForm);
            }

            overlayPanel.Visible = true;
            overlayPanel.BringToFront();
            addUserForm.BringToFront();
        }

        private void HideAddUserForm()
        {
            overlayPanel.Visible = false;
            overlayPanel.SendToBack();
        }

        private void userAccountsPanel2_Load(object sender, EventArgs e)
        {
            // You can remove this if not needed
        }

        private void searchField1_Load(object sender, EventArgs e)
        {
            // You can remove this if not needed
        }

        private void Layout_Paint(object sender, EventArgs e)
        {
            // You can remove this if not needed
        }
    }
}