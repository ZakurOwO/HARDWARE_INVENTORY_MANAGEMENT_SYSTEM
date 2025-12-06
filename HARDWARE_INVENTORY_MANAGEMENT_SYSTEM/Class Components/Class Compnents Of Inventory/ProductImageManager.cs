using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Services;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public static class ProductImageManager
    {
        public static Image GetProductImage(string imageName)
        {
            Image productImage = ImageService.GetImage(imageName, ImageCategory.Product);
            if (productImage != null)
            {
                return productImage;
            }

            return CreateDefaultImage();
        }

        public static Image GetProductImage(byte[] imageBytes, string imageName)
        {
            if (imageBytes != null && imageBytes.Length > 0)
            {
                return ImageService.ConvertBytesToImage(imageBytes);
            }

            return GetProductImage(imageName);
        }

        private static Image CreateDefaultImage()
        {
            Bitmap defaultImage = new Bitmap(50, 50);
            using (Graphics g = Graphics.FromImage(defaultImage))
            {
                g.Clear(Color.LightGray);
                using (Font font = new Font("Arial", 8))
                using (Brush brush = new SolidBrush(Color.DarkGray))
                {
                    g.DrawString("No Image", font, brush, 5, 15);
                }
            }
            return defaultImage;
        }
    }
}