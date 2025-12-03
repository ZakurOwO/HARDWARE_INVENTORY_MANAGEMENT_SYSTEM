using System;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    /// <summary>
    /// Handles creation, display, and disposal of the AdjustStock pop-up and its overlay
    /// within the inventory page. The overlay and popup are scoped to the InventoryMainPage
    /// to avoid covering the entire dashboard and to prevent duplicate helpers.
    /// </summary>
    public class AdjustStockManager
    {
        private readonly InventoryMainPage inventoryPage;
        private readonly Control parentContainer;

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

            EnsureOverlayAdded();
            EnsurePopupAdded();

            adjustStockPopup.ShowAdjustStock(productId, productName, sku, brand, stock, imagePath);

            overlayPanel.Visible = true;
            popupContainer.Visible = true;
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
                Size = new Size(550, 420),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            popupContainer.Controls.Add(adjustStockPopup);
            adjustStockPopup.Dock = DockStyle.Fill;
        }

        private void InitializeOverlay()
        {
            overlayPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                BackColor = Color.Transparent,
                BackgroundImage = Properties.Resources.InventoryOverlay,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            overlayPanel.Controls.Add(popupContainer);
            overlayPanel.SizeChanged += (s, e) => CenterPopup();
        }

        #endregion

        #region Overlay Management

        private void EnsureOverlayAdded()
        {
            Control host = inventoryPage ?? parentContainer;
            if (host == null)
            {
                return;
            }

            if (overlayPanel.Parent != host)
            {
                overlayPanel.Parent?.Controls.Remove(overlayPanel);
                host.Controls.Add(overlayPanel);
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
