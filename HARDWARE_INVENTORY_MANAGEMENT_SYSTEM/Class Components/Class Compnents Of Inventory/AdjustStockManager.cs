using System;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public class AdjustStockManager
    {
        private readonly Control parentContainer;
        private readonly InventoryMainPage inventoryPage;

        private Panel overlayPanel;
        private Panel popupContainer;
        private AdjustStock_PopUp adjustStockPopup;
        private Action refreshCallback;

        public AdjustStockManager(Control parentContainer)
        {
            this.parentContainer = parentContainer ?? throw new ArgumentNullException(nameof(parentContainer));
            inventoryPage = parentContainer as InventoryMainPage;

            InitializePopup();
            InitializeOverlay();
        }

        #region Public API

        public void ShowAdjustStockPopup(
            string productId,
            string productName,
            string sku,
            string brand,
            int stock,
            string imagePath,
            Action refreshAction)
        {
            refreshCallback = refreshAction;

            // Get the main form (MainDashBoard)
            Form mainForm = parentContainer.FindForm();
            if (mainForm == null) return;

            EnsureOverlayAdded(mainForm);
            EnsurePopupAdded();

            adjustStockPopup.ShowAdjustStock(productId, productName, sku, brand, stock, imagePath);

            overlayPanel.Visible = true;
            popupContainer.Visible = true;
            adjustStockPopup.Visible = true;

            overlayPanel.BringToFront();
            popupContainer.BringToFront();
            adjustStockPopup.BringToFront();

            CenterPopup();
            adjustStockPopup.Focus();
        }

        public void HideAdjustStockPopup()
        {
            if (overlayPanel != null)
            {
                overlayPanel.Visible = false;
                overlayPanel.SendToBack();
            }

            if (popupContainer != null)
            {
                popupContainer.Visible = false;
            }

            adjustStockPopup?.HideAdjustStock();
        }

        public bool IsAdjustStockVisible()
        {
            return overlayPanel != null && overlayPanel.Visible;
        }

        #endregion

        #region Initialization

        private void InitializePopup()
        {
            adjustStockPopup = new AdjustStock_PopUp();
            adjustStockPopup.StockAdjusted += AdjustStockPopup_StockAdjusted;
            adjustStockPopup.Cancelled += AdjustStockPopup_Cancelled;

            popupContainer = new Panel
            {
                Size = new Size(590, 450),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            popupContainer.Controls.Add(adjustStockPopup);
            adjustStockPopup.Dock = DockStyle.Fill;
            adjustStockPopup.Visible = true;
        }

        private void InitializeOverlay()
        {
            overlayPanel = new Panel
            {
                Visible = false,
                BackColor = Color.FromArgb(150, 0, 0, 0),
                BackgroundImageLayout = ImageLayout.Stretch
            };

            try
            {
                overlayPanel.BackgroundImage = Properties.Resources.InventoryOverlay;
            }
            catch
            {
                // Use semi-transparent color instead
            }

            overlayPanel.Controls.Add(popupContainer);
            overlayPanel.SizeChanged += (s, e) => CenterPopup();
        }

        #endregion

        #region Overlay Management

        private void EnsureOverlayAdded(Form mainForm)
        {
            if (overlayPanel.Parent != mainForm)
            {
                overlayPanel.Parent?.Controls.Remove(overlayPanel);

                // Add to main form and set to fill the entire form
                mainForm.Controls.Add(overlayPanel);
                overlayPanel.Dock = DockStyle.Fill;
                overlayPanel.BringToFront();
            }
        }

        private void EnsurePopupAdded()
        {
            if (popupContainer.Parent != overlayPanel)
            {
                popupContainer.Parent?.Controls.Remove(popupContainer);
                overlayPanel.Controls.Add(popupContainer);
            }

            CenterPopup();
        }

        private void CenterPopup()
        {
            if (overlayPanel == null || popupContainer == null)
            {
                return;
            }

            int left = Math.Max((overlayPanel.Width - popupContainer.Width) / 2, 0);
            int top = Math.Max((overlayPanel.Height - popupContainer.Height) / 2, 0);
            popupContainer.Location = new Point(left, top);
        }

        #endregion

        #region Event Handlers

        private void AdjustStockPopup_StockAdjusted(object sender, EventArgs e)
        {
            HideAdjustStockPopup();
            refreshCallback?.Invoke();
        }

        private void AdjustStockPopup_Cancelled(object sender, EventArgs e)
        {
            HideAdjustStockPopup();
        }

        #endregion
    }
}