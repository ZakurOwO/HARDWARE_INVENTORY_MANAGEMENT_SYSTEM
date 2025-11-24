using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public static class VehicleImageManager
    {
        // Use your specific debug path
        private static string imageBasePath = @"C:\Users\Admin\Source\Repos\HARDWARE_INVENTORY_MANAGEMENT_SYSTEM9\HARDWARE_INVENTORY_MANAGEMENT_SYSTEM\bin\Debug\ImageVehicles";
        private static Image defaultImage;

        static VehicleImageManager()
        {
            // Ensure directory exists
            EnsureDirectoryExists();
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
            // Always return a default image if no filename is provided
            if (string.IsNullOrEmpty(imageFileName))
            {
                return CreateDefaultImage(width, height);
            }

            try
            {
                string fullPath = Path.Combine(imageBasePath, imageFileName);
                Console.WriteLine($"Looking for image at: {fullPath}");
                Console.WriteLine($"File exists: {File.Exists(fullPath)}");

                if (File.Exists(fullPath))
                {
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        Image originalImage = Image.FromStream(stream);
                        Image resizedImage = ResizeImage(originalImage, width, height);
                        originalImage.Dispose();
                        Console.WriteLine($"Successfully loaded image: {imageFileName}");
                        return resizedImage;
                    }
                }
                else
                {
                    // File doesn't exist, return default image
                    Console.WriteLine($"Image file not found: {fullPath}");
                    return CreateDefaultImage(width, height);
                }
            }
            catch (Exception ex)
            {
                // Log the error and return default image
                Console.WriteLine($"Error loading image {imageFileName}: {ex.Message}");
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
                    Console.WriteLine("Source path is invalid or file doesn't exist");
                    return "default_vehicle.png";
                }

                // Ensure directory exists
                EnsureDirectoryExists();

                // Generate unique filename
                string extension = Path.GetExtension(sourcePath).ToLower();
                string fileName = $"vehicle_{DateTime.Now:yyyyMMddHHmmssfff}{extension}";
                string destinationPath = Path.Combine(imageBasePath, fileName);

                Console.WriteLine($"Copying from: {sourcePath}");
                Console.WriteLine($"Copying to: {destinationPath}");

                // Copy file to ImageVehicles directory
                File.Copy(sourcePath, destinationPath, true);

                Console.WriteLine($"Image saved successfully: {fileName}");
                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving vehicle image: {ex.Message}");
                return "default_vehicle.png";
            }
        }

        // Resize image to specified dimensions with high quality
        private static Image ResizeImage(Image image, int width, int height)
        {
            try
            {
                Bitmap resizedImage = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(resizedImage))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;

                    // Use clear background
                    g.Clear(Color.White);

                    // Maintain aspect ratio
                    float sourceRatio = (float)image.Width / image.Height;
                    float destRatio = (float)width / height;

                    int drawWidth, drawHeight;
                    int drawX = 0, drawY = 0;

                    if (sourceRatio > destRatio)
                    {
                        // Source is wider
                        drawWidth = width;
                        drawHeight = (int)(width / sourceRatio);
                        drawY = (height - drawHeight) / 2;
                    }
                    else
                    {
                        // Source is taller
                        drawHeight = height;
                        drawWidth = (int)(height * sourceRatio);
                        drawX = (width - drawWidth) / 2;
                    }

                    g.DrawImage(image, drawX, drawY, drawWidth, drawHeight);
                }
                return resizedImage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error resizing image: {ex.Message}");
                return CreateDefaultImage(width, height);
            }
        }

        // Create default placeholder image
        private static Image CreateDefaultImage()
        {
            return CreateDefaultImage(220, 119);
        }

        // Create default placeholder image with custom dimensions
        private static Image CreateDefaultImage(int width, int height)
        {
            try
            {
                Bitmap defaultImage = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(defaultImage))
                {
                    g.Clear(Color.LightGray);

                    // Draw border
                    using (Pen borderPen = new Pen(Color.DarkGray, 2))
                    {
                        g.DrawRectangle(borderPen, 0, 0, width - 1, height - 1);
                    }

                    // Draw vehicle icon or text
                    using (Font font = new Font("Arial", Math.Max(10, width / 15), FontStyle.Bold))
                    using (Brush brush = new SolidBrush(Color.DarkGray))
                    using (StringFormat format = new StringFormat())
                    {
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Center;

                        g.DrawString("🚗 Vehicle Image", font, brush,
                                    new RectangleF(0, 0, width, height), format);
                    }
                }
                return defaultImage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating default image: {ex.Message}");
                // Return a simple bitmap as fallback
                return new Bitmap(width, height);
            }
        }

        // Ensure directory exists
        private static void EnsureDirectoryExists()
        {
            try
            {
                Console.WriteLine($"Checking directory: {imageBasePath}");
                if (!Directory.Exists(imageBasePath))
                {
                    Directory.CreateDirectory(imageBasePath);
                    Console.WriteLine($"Created directory: {imageBasePath}");
                }
                else
                {
                    Console.WriteLine($"Directory already exists: {imageBasePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating directory: {ex.Message}");
            }
        }

        // Set custom image base path
        public static void SetImageBasePath(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                imageBasePath = path;
                Console.WriteLine($"Image base path changed to: {path}");
                EnsureDirectoryExists();
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
            bool exists = File.Exists(fullPath);
            Console.WriteLine($"Checking if image exists: {fullPath} -> {exists}");
            return exists;
        }

        // Get full path for image file
        public static string GetFullImagePath(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
                return string.Empty;

            string fullPath = Path.Combine(imageBasePath, imageFileName);
            Console.WriteLine($"Full image path: {fullPath}");
            return fullPath;
        }

        // Delete vehicle image
        public static bool DeleteVehicleImage(string imageFileName)
        {
            try
            {
                if (string.IsNullOrEmpty(imageFileName) || imageFileName == "default_vehicle.png")
                    return true; // Don't delete default images

                string fullPath = Path.Combine(imageBasePath, imageFileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    Console.WriteLine($"Deleted image: {fullPath}");
                    return true;
                }
                Console.WriteLine($"Image to delete not found: {fullPath}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting vehicle image: {ex.Message}");
                return false;
            }
        }
    }
}