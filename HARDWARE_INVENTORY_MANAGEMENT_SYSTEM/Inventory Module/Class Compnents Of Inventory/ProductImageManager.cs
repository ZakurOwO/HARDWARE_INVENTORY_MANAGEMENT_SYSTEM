using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public static class ProductImageManager
    {
        public static Image GetProductImage(string imageName)
        {
            Image img;

            // Check if file exists in ImageInventory folder
            string imagePath = Path.Combine(Application.StartupPath, "ImageInventory", imageName);

            if (File.Exists(imagePath))
            {
                img = Image.FromFile(imagePath);
                return ResizeImage(img, 50, 50);
            }
            else
            {
                // Return default image
                img = CreateDefaultImage();
                return img;
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