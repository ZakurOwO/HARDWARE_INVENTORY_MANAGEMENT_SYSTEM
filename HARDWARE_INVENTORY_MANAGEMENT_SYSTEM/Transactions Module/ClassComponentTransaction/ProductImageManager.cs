using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.ClasComponentsTransaction
{
    public static class ProductImageManager
    {
        private static string imageBasePath = Path.Combine(Application.StartupPath, "ImageInventory");
        private static Image defaultImage;

        static ProductImageManager()
        {
            defaultImage = CreateDefaultImage();
        }

        // Get product image with default size (50x50)
        public static Image GetProductImage(string imageFileName)
        {
            return GetProductImage(imageFileName, 50, 50);
        }

        // Get product image with custom dimensions - ADD THIS OVERLOAD
        public static Image GetProductImage(string imageFileName, int width, int height)
        {
            if (string.IsNullOrEmpty(imageFileName) || imageFileName == "Boysen.png")
            {
                return CreateDefaultImage(width, height);
            }

            try
            {
                string fullPath = Path.Combine(imageBasePath, imageFileName);

                if (File.Exists(fullPath))
                {
                    Image originalImage = Image.FromFile(fullPath);
                    return ResizeImage(originalImage, width, height);
                }
                else
                {
                    return CreateDefaultImage(width, height);
                }
            }
            catch (Exception)
            {
                return CreateDefaultImage(width, height);
            }
        }

        // Rest of your existing methods remain the same...
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

        private static Image GetDefaultProductImage()
        {
            return defaultImage;
        }

        private static Image CreateDefaultImage()
        {
            return CreateDefaultImage(50, 50);
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
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                imageBasePath = path;
            }
            else
            {
                imageBasePath = Path.Combine(Application.StartupPath, "ImageInventory");

                if (!Directory.Exists(imageBasePath))
                {
                    try
                    {
                        Directory.CreateDirectory(imageBasePath);
                    }
                    catch (Exception)
                    {
                        // Directory creation failed, continue with default path
                    }
                }
            }
        }

        public static string GetImageBasePath()
        {
            return imageBasePath;
        }

        public static bool ImageExists(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
                return false;

            string fullPath = Path.Combine(imageBasePath, imageFileName);
            return File.Exists(fullPath);
        }

        public static string GetFullImagePath(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
                return string.Empty;

            return Path.Combine(imageBasePath, imageFileName);
        }
    }
}