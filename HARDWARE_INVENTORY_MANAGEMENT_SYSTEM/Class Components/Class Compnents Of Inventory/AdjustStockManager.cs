using System;
using System.Drawing; 
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public class AdjustStockManager
    {
        private AdjustStock_PopUp adjustStockPopUp;   
        private MainDashBoard mainForm;
        private Panel scrollContainer;
        private InventoryMainPage inventoryPage;

        public AdjustStockManager(Control parentContainer)
        {
            this.mainForm = mainForm;   
            InitializeAdjustStockPopup();
        }

        private void InitializeAdjustStockPopup()
        {
            adjustStockPopUp = new AdjustStock_PopUp();
            adjustStockPopUp.Visible = false;
            
            adjustStockPopUp.BringToFront();
        }

        public void ShowAdjustStockPopup(string productName, string sku, string brand, int stock, string imagePath, Action refreshCallback = null)
        {
            
            // Create popup form control
            adjustStockPopUp = new AdjustStock_PopUp();
            adjustStockPopUp.Dock = DockStyle.None;

            // --- CREATE WRAPPER PANEL (same as sample code) ---
            scrollContainer = new Panel();
            scrollContainer.Size = new Size(550, 420); // Adjust pop-up container size
            scrollContainer.Location = new Point(
                (mainForm.Width - scrollContainer.Width) / 2,
                (mainForm.Height - scrollContainer.Height) / 2
            );

            scrollContainer.BorderStyle = BorderStyle.FixedSingle;
            scrollContainer.AutoScroll = false;

            // Add the popup into the panel
            scrollContainer.Controls.Add(adjustStockPopUp);

            adjustStockPopUp.Size = scrollContainer.Size;
            adjustStockPopUp.Location = new Point(0, 0);
            adjustStockPopUp.ShowAdjustStock(productName, sku, brand, stock, imagePath);

            // ---- ENABLE BLUR OVERLAY (same as sample logic) ----
            mainForm.pcbBlurOverlay.BackgroundImage = Properties.Resources.SupplierOverlay;
            mainForm.pcbBlurOverlay.BackgroundImageLayout = ImageLayout.Stretch;
            mainForm.pcbBlurOverlay.Visible = true;
            mainForm.pcbBlurOverlay.BringToFront();

            // ---- ADD PANEL TO MAIN FORM ----
            mainForm.Controls.Add(scrollContainer);
            scrollContainer.BringToFront();

            // EVENTS
            adjustStockPopUp.StockAdjusted += (s, e) =>
            {
                HideAdjustStockPopup();
                refreshCallback?.Invoke();
            };

            adjustStockPopUp.Cancelled += (s, e) =>
            {
                HideAdjustStockPopup();
            };
        }

        public void HideAdjustStockPopup()
        {
            adjustStockPopUp?.HideAdjustStock();

            if (scrollContainer != null && mainForm.Controls.Contains(scrollContainer))
                mainForm.Controls.Remove(scrollContainer);

            mainForm.pcbBlurOverlay.Visible = false;
        }

        public bool IsAdjustStockVisible()
        {
            return adjustStockPopUp?.Visible ?? false;
        }
    }
}