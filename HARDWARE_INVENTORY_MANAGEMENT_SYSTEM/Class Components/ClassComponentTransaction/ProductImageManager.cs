using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Services;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.ClasComponentsTransaction
{
    public static class ProductImageManager
    {
        // Get product image with default size (50x50)
        public static Image GetProductImage(string imageFileName)
        {
            return GetProductImage(imageFileName, 50, 50);
        }

        // Get product image with custom dimensions
        public static Image GetProductImage(string imageFileName, int width, int height)
        {
            try
            {
                Image productImage = ImageService.GetImage(imageFileName, ImageCategory.Product);

                if (productImage != null)
                {
                    return ResizeImage(productImage, width, height);
                }

                return CreateDefaultImage(width, height);
            }
            catch (Exception)
            {
                return CreateDefaultImage(width, height);
            }
        }

        private static Image ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                g.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }

        private static Image CreateDefaultImage(int width, int height)
        {
            Bitmap defaultImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(defaultImage))
            {
                g.Clear(Color.LightGray);

                using (Pen borderPen = new Pen(Color.DarkGray, 1))
                {
                    g.DrawRectangle(borderPen, 0, 0, width - 1, height - 1);
                }

                using (Font font = new Font("Arial", Math.Max(6, width / 10), FontStyle.Regular))
                using (Brush brush = new SolidBrush(Color.DarkGray))
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    g.DrawString("No Image", font, brush,
                                new RectangleF(0, 0, width, height), format);
                }
            }
            return defaultImage;
        }

        public static void SetImageBasePath(string path)
        {
            // The image base path is no longer used when loading from embedded resources
        }

        public static string GetImageBasePath()
        {
            return string.Empty;
        }

        public static bool ImageExists(string imageFileName)
        {
            return Resources.ResourceManager.GetObject(imageFileName) != null
                   || Resources.ResourceManager.GetObject(Path.GetFileNameWithoutExtension(imageFileName)) != null;
        }

        public static string GetFullImagePath(string imageFileName)
        {
            return imageFileName;
        }
    }
}
