using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public class SearchTextBox
    {
        private TextBox textBox;
        private string placeholderText;
        private Color placeholderColor = Color.Gray;
        private Color normalColor = Color.Black;

        public event EventHandler<string> SearchTextChanged;

        public SearchTextBox(TextBox textBoxControl, string placeholder = "Search...")
        {
            this.textBox = textBoxControl;
            this.placeholderText = placeholder;
            Initialize();
        }

        private void Initialize()
        {
            SetPlaceholderText();

            textBox.Enter += TextBox_Enter;
            textBox.Leave += TextBox_Leave;
            textBox.TextChanged += TextBox_TextChanged;
            textBox.Click += TextBox_Click; // Add Click event
        }

        private void TextBox_Click(object sender, EventArgs e)
        {
            RemovePlaceholderText();
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            RemovePlaceholderText();
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text.Trim()))
            {
                SetPlaceholderText();
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox.Text == placeholderText && textBox.ForeColor == placeholderColor)
                return;

            string searchText = GetSearchText();
            SearchTextChanged?.Invoke(this, searchText);
        }

        private void SetPlaceholderText()
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = placeholderText;
                textBox.ForeColor = placeholderColor;
            }
        }

        private void RemovePlaceholderText()
        {
            if (textBox.Text == placeholderText && textBox.ForeColor == placeholderColor)
            {
                textBox.Text = "";
                textBox.ForeColor = normalColor;
            }
        }

        public string GetSearchText()
        {
            if (textBox.Text == placeholderText && textBox.ForeColor == placeholderColor)
                return "";

            return textBox.Text.Trim();
        }

        public void ClearSearch()
        {
            textBox.Text = "";
            SetPlaceholderText();
        }

        // Method to filter DataGridView rows
        public void FilterDataGridView(DataGridView dataGridView, string searchText, int columnIndex = 0)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                ShowAllDataGridViewRows(dataGridView);
                ShowNoResultsMessage(dataGridView, false);
                return;
            }

            bool anyResultsFound = FilterDataGridViewBySearch(dataGridView, searchText, columnIndex);
            ShowNoResultsMessage(dataGridView, !anyResultsFound);
        }

        private bool FilterDataGridViewBySearch(DataGridView dataGridView, string searchText, int columnIndex)
        {
            bool anyResultsFound = false;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue;

                string cellValue = row.Cells[columnIndex].Value?.ToString() ?? "";
                bool matches = cellValue.StartsWith(searchText, StringComparison.OrdinalIgnoreCase);
                row.Visible = matches;

                if (matches) anyResultsFound = true;
            }

            return anyResultsFound;
        }

        private void ShowAllDataGridViewRows(DataGridView dataGridView)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Visible = true;
                }
            }
        }

        private void ShowNoResultsMessage(DataGridView dataGridView, bool show)
        {
            var existingLabel = dataGridView.Parent?.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "NoResultsLabel");
            if (existingLabel != null)
            {
                dataGridView.Parent.Controls.Remove(existingLabel);
            }

            if (show && dataGridView.Parent != null)
            {
                Label noResultsLabel = new Label
                {
                    Name = "NoResultsLabel",
                    Text = "No items found matching your search.",
                    Font = new Font("LEXEND", 12, FontStyle.Bold),
                    ForeColor = Color.Gray,
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Size = new Size(300, 50),
                    Dock = DockStyle.None,
                    Anchor = AnchorStyles.None
                };

                noResultsLabel.Location = new Point(
                    dataGridView.Left + (dataGridView.Width - noResultsLabel.Width) / 2,
                    dataGridView.Top + (dataGridView.Height - noResultsLabel.Height) / 2
                );

                dataGridView.Parent.Controls.Add(noResultsLabel);
                noResultsLabel.BringToFront();
            }
        }
    }
}