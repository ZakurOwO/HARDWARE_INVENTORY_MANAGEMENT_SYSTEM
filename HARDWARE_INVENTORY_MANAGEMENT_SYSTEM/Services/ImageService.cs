using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Services
{
    public enum ImageCategory
    {
        Product,
        Vehicle,
        Customer,
        Supplier,
        Any
    }

    public static class ImageService
    {
        private const string PlaceholderKey = "__placeholder__";
        private static readonly Dictionary<string, Image> Cache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
        private static readonly object CacheLock = new object();
        private static Image placeholderImage;

        static ImageService()
        {
            placeholderImage = CreatePlaceholderImage();
            Cache[PlaceholderKey] = placeholderImage;
        }

        public static Image GetImage(string dbImagePath, ImageCategory category)
        {
            string resolvedPath = ResolvePath(dbImagePath, category);
            string cacheKey = string.IsNullOrEmpty(resolvedPath) ? PlaceholderKey : resolvedPath;

            lock (CacheLock)
            {
                Image cached;
                if (Cache.TryGetValue(cacheKey, out cached))
                {
                    return cached;
                }
            }

            Image loadedImage = null;

            if (!string.IsNullOrEmpty(resolvedPath) && File.Exists(resolvedPath))
            {
                loadedImage = LoadImageSafely(resolvedPath);
            }

            if (loadedImage == null)
            {
                loadedImage = GetPlaceholder();
            }

            lock (CacheLock)
            {
                Image cached;
                if (Cache.TryGetValue(cacheKey, out cached))
                {
                    return cached;
                }

                Cache[cacheKey] = loadedImage;
            }

            return loadedImage;
        }

        public static string ResolvePath(string dbImagePath, ImageCategory category)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dbImagePath))
                {
                    return null;
                }

                string trimmedPath = dbImagePath.Trim();

                if (Path.IsPathRooted(trimmedPath))
                {
                    string rootedPath = Path.GetFullPath(trimmedPath);
                    if (File.Exists(rootedPath))
                    {
                        return rootedPath;
                    }
                }

                string startupPath = Application.StartupPath;
                string normalizedRelative = trimmedPath.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);

                string combinedRelative = Path.GetFullPath(Path.Combine(startupPath, normalizedRelative));
                if (File.Exists(combinedRelative))
                {
                    return combinedRelative;
                }

                List<string> searchFolders = new List<string>();

                string categoryFolder = GetCategoryFolder(category, startupPath);
                if (!string.IsNullOrEmpty(categoryFolder))
                {
                    searchFolders.Add(categoryFolder);
                }

                searchFolders.Add(Path.Combine(startupPath, "Images"));
                searchFolders.Add(Path.Combine(startupPath, "Assets"));
                searchFolders.Add(startupPath);

                foreach (string baseFolder in searchFolders)
                {
                    string candidate = Path.GetFullPath(Path.Combine(baseFolder, normalizedRelative));
                    if (File.Exists(candidate))
                    {
                        return candidate;
                    }
                }
            }
            catch
            {
                // Ignore resolution errors and fall back to placeholder
            }

            return null;
        }

        public static void ClearCache()
        {
            lock (CacheLock)
            {
                foreach (KeyValuePair<string, Image> entry in Cache)
                {
                    if (!object.ReferenceEquals(entry.Value, placeholderImage))
                    {
                        entry.Value.Dispose();
                    }
                }

                Cache.Clear();
                Cache[PlaceholderKey] = GetPlaceholder();
            }
        }

        private static Image LoadImageSafely(string path)
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(path);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    using (Image source = Image.FromStream(stream))
                    {
                        Bitmap clone = new Bitmap(source);
                        return clone;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        private static Image GetPlaceholder()
        {
            lock (CacheLock)
            {
                if (placeholderImage == null)
                {
                    placeholderImage = CreatePlaceholderImage();
                    Cache[PlaceholderKey] = placeholderImage;
                }

                return placeholderImage;
            }
        }

        private static string GetCategoryFolder(ImageCategory category, string startupPath)
        {
            string categoryFolder = string.Empty;

            switch (category)
            {
                case ImageCategory.Product:
                    categoryFolder = Path.Combine(startupPath, "Images", "Products");
                    break;
                case ImageCategory.Vehicle:
                    categoryFolder = Path.Combine(startupPath, "Images", "Vehicles");
                    break;
                case ImageCategory.Customer:
                    categoryFolder = Path.Combine(startupPath, "Images", "Customers");
                    break;
                case ImageCategory.Supplier:
                    categoryFolder = Path.Combine(startupPath, "Images", "Suppliers");
                    break;
                default:
                    categoryFolder = string.Empty;
                    break;
            }

            return categoryFolder;
        }

        private static Image CreatePlaceholderImage()
        {
            int width = 180;
            int height = 120;

            Bitmap placeholder = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(placeholder);
            try
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.Clear(Color.LightGray);

                using (Pen borderPen = new Pen(Color.Gray, 2))
                {
                    graphics.DrawRectangle(borderPen, 1, 1, width - 2, height - 2);
                }

                using (Font font = new Font("Segoe UI", 12, FontStyle.Bold))
                using (SolidBrush brush = new SolidBrush(Color.DimGray))
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    graphics.DrawString("No Image", font, brush, new RectangleF(0, 0, width, height), format);
                }
            }
            finally
            {
                graphics.Dispose();
            }

            return placeholder;
        }
    }
}
