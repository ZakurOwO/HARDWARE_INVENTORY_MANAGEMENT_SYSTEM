using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public static class VehicleImageManager
    {
        private static string imageBasePath = Path.Combine(Application.StartupPath, "ImageVehicles");
        private static Image defaultImage;

        static VehicleImageManager()
        {
            // Ensure directory exists
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
            defaultImage = CreateDefaultImage();
        }

        // Get vehicle image with default size
        public static Image GetVehicleImage(string imageFileName)
        {
            return GetVehicleImage(imageFileName, 220, 119);
        }

        // Get vehicle image with custom dimensions
        public static Image GetVehicleImage(string imageFileName, int width, int height)
        {
            if (string.IsNullOrEmpty(imageFileName))
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

        // Save vehicle image
        public static string SaveVehicleImage(string sourcePath)
        {
            try
            {
                if (string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath))
                {
                    return string.Empty;
                }

                // Generate unique filename
                string extension = Path.GetExtension(sourcePath);
                string fileName = $"vehicle_{DateTime.Now:yyyyMMddHHmmss}{extension}";
                string destinationPath = Path.Combine(imageBasePath, fileName);

                // Copy file to ImageVehicles directory
                File.Copy(sourcePath, destinationPath, true);

                return fileName;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving vehicle image: {ex.Message}");
            }
        }

        // Resize image to specified dimensions with high quality
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

        // Get cached default image
        private static Image GetDefaultVehicleImage()
        {
            return defaultImage;
        }

        // Create default placeholder image
        private static Image CreateDefaultImage()
        {
            return CreateDefaultImage(220, 119);
        }

        // Create default placeholder image with custom dimensions
        private static Image CreateDefaultImage(int width, int height)
        {
            Bitmap defaultImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(defaultImage))
            {
                g.Clear(Color.LightGray);

                // Draw border
                using (Pen borderPen = new Pen(Color.DarkGray, 1))
                {
                    g.DrawRectangle(borderPen, 0, 0, width - 1, height - 1);
                }

                // Draw "No Image" text
                using (Font font = new Font("Arial", Math.Max(8, width / 15), FontStyle.Regular))
                using (Brush brush = new SolidBrush(Color.DarkGray))
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    g.DrawString("No Vehicle Image", font, brush,
                                new RectangleF(0, 0, width, height), format);
                }
            }
            return defaultImage;
        }

        // Set custom image base path
        public static void SetImageBasePath(string path)
        {
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                imageBasePath = path;
            }
        }

        // Get current image base path
        public static string GetImageBasePath()
        {
            return imageBasePath;
        }

        // Check if image file exists
        public static bool ImageExists(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
                return false;

            string fullPath = Path.Combine(imageBasePath, imageFileName);
            return File.Exists(fullPath);
        }

        // Get full path for image file
        public static string GetFullImagePath(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
                return string.Empty;

            return Path.Combine(imageBasePath, imageFileName);
        }

        // Delete vehicle image
        public static void DeleteVehicleImage(string imageFileName)
        {
            try
            {
                if (string.IsNullOrEmpty(imageFileName))
                    return;

                string fullPath = Path.Combine(imageBasePath, imageFileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch (Exception)
            {
                // Silent fail - image deletion is not critical
            }
        }
    }
}