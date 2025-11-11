using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class Products : UserControl
    {
        public Products()
        {
            InitializeComponent();
        }

        #region MyRegion

        private Image image;
        private string productName;
        private int price;

        [Category("Custom Properties")]
        public Image Image
        {
            get { return image; }
            set { image = value; pbxProductImage.BackgroundImage = value; }
        }


        [Category("Custom Properties")]
        public string Product_Name
        {
            get { return productName; }
            set { productName = value; lblProductName.Text = value; }
        }


        [Category("Custom Properties")]
        public int Price
        {
            get { return price; }
            set { price = value; lblPrice.Text = $"₱{value:N2}"; }
        }

        #endregion

    }
}
