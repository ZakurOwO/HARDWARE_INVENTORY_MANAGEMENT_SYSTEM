using System;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    /// <summary>
    /// Coordinates showing and hiding the AdjustStock_PopUp within the inventory page.
    /// The manager keeps the overlay scoped to the InventoryMainPage and drives the
    /// blur effect on the hosting MainDashBoard.
    /// </summary>
    public class AdjustStockManager
    {
        private readonly InventoryMainPage inventoryPage;
        private readonly Control hostContainer;
        private MainDashBoard mainForm;
        private Panel popupContainer;
        private AdjustStock_PopUp adjustStockPopUp;
        private Action refreshCallback;

        public AdjustStockManager(Control parentContainer)
        {
            inventoryPage = parentContainer as InventoryMainPage;
            hostContainer = parentContainer;
            mainForm = FindMainDashboard(parentContainer);
            InitializeAdjustStockPopup();
        }

        public void ShowAdjustStockPopup(string productId, string productName, string sku, string brand, int stock, string imagePath, Action refreshAction)
        {
            EnsureMainForm();
            refreshCallback = refreshAction;

            if (adjustStockPopUp == null)
            {
                InitializeAdjustStockPopup();
            }

            WirePopupEvents();
            PreparePopupContainer();

            adjustStockPopUp.ShowAdjustStock(productId, productName, sku, brand, stock, imagePath);
            DisplayOverlay();
            AddPopupToHost();
        }

        public void HideAdjustStockPopup()
        {
            if (adjustStockPopUp != null)
            {
                adjustStockPopUp.HideAdjustStock();
            }

            if (popupContainer != null && popupContainer.Parent != null)
            {
                popupContainer.Parent.Controls.Remove(popupContainer);
            }

            HideOverlay();
        }

        public bool IsAdjustStockVisible()
        {
            return adjustStockPopUp != null && adjustStockPopUp.Visible;
        }

        private void InitializeAdjustStockPopup()
        {
            adjustStockPopUp = new AdjustStock_PopUp();
            adjustStockPopUp.Visible = false;
        }

        private void WirePopupEvents()
        {
            adjustStockPopUp.StockAdjusted -= AdjustStockPopUp_StockAdjusted;
            adjustStockPopUp.Cancelled -= AdjustStockPopUp_Cancelled;

            adjustStockPopUp.StockAdjusted += AdjustStockPopUp_StockAdjusted;
            adjustStockPopUp.Cancelled += AdjustStockPopUp_Cancelled;
        }

        private void AdjustStockPopUp_StockAdjusted(object sender, EventArgs e)
        {
            HideAdjustStockPopup();
            if (refreshCallback != null)
            {
                refreshCallback();
            }
        }

        private void AdjustStockPopUp_Cancelled(object sender, EventArgs e)
        {
            HideAdjustStockPopup();
        }

        private void PreparePopupContainer()
        {
            popupContainer = new Panel();
            popupContainer.Size = new Size(550, 420);
            popupContainer.BackColor = Color.White;
            popupContainer.BorderStyle = BorderStyle.FixedSingle;

            CenterPopupContainer();

            popupContainer.Controls.Clear();
            popupContainer.Controls.Add(adjustStockPopUp);

            adjustStockPopUp.Dock = DockStyle.Fill;
            adjustStockPopUp.BringToFront();
        }

        private void CenterPopupContainer()
        {
            Control target = GetOverlayHost();
            Control anchor = hostContainer ?? inventoryPage as Control;

            if (popupContainer == null)
            {
                return;
            }

            if (target == null || anchor == null)
            {
                popupContainer.Location = new Point(0, 0);
                return;
            }

            Point anchorScreen = anchor.PointToScreen(Point.Empty);
            Point targetScreen = target.PointToScreen(Point.Empty);
            Point relative = new Point(anchorScreen.X - targetScreen.X, anchorScreen.Y - targetScreen.Y);

            int left = relative.X + (anchor.Width - popupContainer.Width) / 2;
            int top = relative.Y + (anchor.Height - popupContainer.Height) / 2;

            if (left < 0)
            {
                left = 0;
            }

            if (top < 0)
            {
                top = 0;
            }

            popupContainer.Location = new Point(left, top);
        }

        private void AddPopupToHost()
        {
            Control container = GetOverlayHost();
            if (container == null)
            {
                return;
            }

            popupContainer.Parent = null;
            container.Controls.Add(popupContainer);
            popupContainer.BringToFront();
        }

        private void DisplayOverlay()
        {
            if (mainForm == null)
            {
                return;
            }

            if (mainForm.pcbBlurOverlay != null)
            {
                if (mainForm.pcbBlurOverlay.BackgroundImage == null)
                {
                    mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.SupplierOverlay;
                    mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
                }

                mainForm.pcbBlurOverlay.Visible = true;
                mainForm.pcbBlurOverlay.BringToFront();
            }

            CenterPopupContainer();
            popupContainer.BringToFront();
        }

        private void HideOverlay()
        {
            if (mainForm != null && mainForm.pcbBlurOverlay != null)
            {
                if (popupContainer != null && popupContainer.Parent == mainForm.pcbBlurOverlay)
                {
                    mainForm.pcbBlurOverlay.Controls.Remove(popupContainer);
                }

                mainForm.pcbBlurOverlay.Visible = false;
            }
        }

        private Control GetOverlayHost()
        {
            if (mainForm != null && mainForm.pcbBlurOverlay != null)
            {
                return mainForm.pcbBlurOverlay;
            }

            return hostContainer ?? inventoryPage as Control;
        }

        private MainDashBoard FindMainDashboard(Control startingControl)
        {
            Control current = startingControl;
            while (current != null)
            {
                MainDashBoard dashboard = current as MainDashBoard;
                if (dashboard != null)
                {
                    return dashboard;
                }
                current = current.Parent;
            }

            if (startingControl != null)
            {
                Form form = startingControl.FindForm();
                return form as MainDashBoard;
            }

            return null;
        }

        private void EnsureMainForm()
        {
            if (mainForm == null)
            {
                mainForm = FindMainDashboard(hostContainer);
            }
        }
    }
}
