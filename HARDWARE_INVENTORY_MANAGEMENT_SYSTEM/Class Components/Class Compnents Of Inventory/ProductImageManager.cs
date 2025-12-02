using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public static class ProductImageManager
    {
        public static Image GetProductImage(string imageName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(imageName))
                {
                    return CreateDefaultImage();
                }

                // Try to fetch the image directly using the key as stored in the database
                object resourceImage = Resources.ResourceManager.GetObject(imageName);

                // If not found, try again without the file extension to cover both naming styles
                if (resourceImage == null)
                {
                    string resourceKey = Path.GetFileNameWithoutExtension(imageName);
                    resourceImage = Resources.ResourceManager.GetObject(resourceKey);
                }

                if (resourceImage is Image foundImage)
                {
                    return ResizeImage(foundImage, 50, 50);
                }

                return CreateDefaultImage();
            }
            catch
            {
                return CreateDefaultImage();
            }
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