using System;
using System.Drawing; 
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public class AdjustStockManager
    {
        private AdjustStock_PopUp adjustStockPopUp;
        private Control parentContainer;

        public AdjustStockManager(Control parentContainer)
        {
            this.parentContainer = parentContainer;
            InitializeAdjustStockPopup();
        }

        private void InitializeAdjustStockPopup()
        {
            adjustStockPopUp = new AdjustStock_PopUp();
            adjustStockPopUp.Visible = false;
            parentContainer.Controls.Add(adjustStockPopUp);
            adjustStockPopUp.BringToFront();
        }

        public void ShowAdjustStockPopup(string productName, string sku, string brand, int stock, string imagePath, string productId, Action refreshCallback = null)
        {
            if (adjustStockPopUp == null)
                InitializeAdjustStockPopup();

            // Center the popup in the main page
            CenterPopupInParent();

            adjustStockPopUp.ShowAdjustStock(productName, sku, brand, stock, imagePath, productId);

            // Handle events
            adjustStockPopUp.StockAdjusted += (s, e) => {
                adjustStockPopUp.HideAdjustStock();
                refreshCallback?.Invoke(); // Refresh your data
            };

            adjustStockPopUp.Cancelled += (s, e) => {
                adjustStockPopUp.HideAdjustStock();
            };
        }

        private void CenterPopupInParent()
        {
            if (adjustStockPopUp != null && parentContainer != null)
            {
                adjustStockPopUp.Location = new Point(
                    (parentContainer.Width - adjustStockPopUp.Width) / 2,
                    (parentContainer.Height - adjustStockPopUp.Height) / 2
                );
            }
        }

        public void HideAdjustStockPopup()
        {
            adjustStockPopUp?.HideAdjustStock();
        }

        public bool IsAdjustStockVisible()
        {
            return adjustStockPopUp?.Visible ?? false;
        }
    }
}