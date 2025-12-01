using System;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public static class ProductImageManager
    {
        public static Image GetProductImage(string imageName)
        {
            Image img = TryGetResourceImage(imageName);

            if (img != null)
            {
                return ResizeImage(img, 50, 50);
            }

            return CreateDefaultImage();
        }

        private static Image TryGetResourceImage(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                return null;

            string key = Path.GetFileNameWithoutExtension(imageName);
            if (string.IsNullOrWhiteSpace(key))
                return null;

            object resource = Resources.ResourceManager.GetObject(key);
            if (resource is Image img)
            {
                return img;
            }

            return null;
        }

        private static Image ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
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
