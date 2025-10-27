using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public partial class PageNumber : UserControl
    {
        private KryptonButton currentSelected;

        public PageNumber()
        {
            InitializeComponent();
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            // Make arrow buttons (PrevPageBtn, NextPageBtn) always transparent
            MakeButtonTransparent(PrevPageBtn);
            MakeButtonTransparent(NextPageBtn);

            // Set default active numeric button (optional)
            SetActiveButton(NumberPagePrev);

            // Assign click handlers for numeric page buttons only
            NumberPagePrev.Click += (s, e) => SetActiveButton(NumberPagePrev);
            NumberPageNext.Click += (s, e) => SetActiveButton(NumberPageNext);
        }

        private void MakeButtonTransparent(KryptonButton btn)
        {
            btn.OverrideDefault.Back.Color1 = Color.Transparent;
            btn.OverrideDefault.Back.Color2 = Color.Transparent;
            btn.StateCommon.Back.Color1 = Color.Transparent;
            btn.StateCommon.Back.Color2 = Color.Transparent;
            btn.StateCommon.Border.Color1 = Color.Transparent;
            btn.StateCommon.Border.Color2 = Color.Transparent;
            btn.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
        }

        private void SetActiveButton(KryptonButton btn)
        {
            // Reset previously active button
            if (currentSelected != null)
            {
                currentSelected.StateCommon.Border.Color1 = Color.Transparent;
                currentSelected.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
                currentSelected.StateCommon.Content.ShortText.Color1 = Color.Black;
            }

            // Apply DodgerBlue highlight (border + text only)
            btn.StateCommon.Border.Color1 = Color.DodgerBlue;
            btn.StateCommon.Border.Width = 1;
            btn.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
            btn.StateCommon.Content.ShortText.Color1 = Color.DodgerBlue;

            currentSelected = btn;
        }

        private void NumberPagePrev_Click(object sender, EventArgs e)
        {
            SetActiveButton(NumberPagePrev);
        }

        private void NumberPageNext_Click(object sender, EventArgs e)
        {
            SetActiveButton(NumberPageNext);
        }

        private void PrevPageBtn_Click(object sender, EventArgs e)
        {
            // Transparent only — no color changes
        }

        private void NextPageBtn_Click(object sender, EventArgs e)
        {
            // Transparent only — no color changes
        }
    }
}
