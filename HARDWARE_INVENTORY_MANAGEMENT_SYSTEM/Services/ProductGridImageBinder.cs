using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Services
{
    public static class ProductGridImageBinder
    {
        // simple in-memory cache so image decoding doesn't lag on paging
        private static readonly Dictionary<string, Image> _imageCache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);

        public static void ClearCache()
        {
            foreach (var kv in _imageCache)
            {
                kv.Value?.Dispose();
            }
            _imageCache.Clear();
        }

        public class ProductRowModel
        {
            public int ProductInternalId { get; set; }
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string SKU { get; set; }
            public string Category { get; set; }
            public int CurrentStock { get; set; }
            public int ReorderPoint { get; set; }
            public string Status { get; set; }
            public string ImagePath { get; set; }     // file path (optional)
            public byte[] ProductImage { get; set; }  // blob (optional)
            public string Brand { get; set; }
        }

        /// <summary>
        /// Binds your page rows into dgv, and sets row.Tag = ProductRowModel so your click handlers can read SKU etc.
        /// Assumes your DataGridView already has the expected columns (including "ProductImage" if used).
        /// </summary>
        public static void BindRows(DataGridView dgv, BindingList<ProductRowModel> rows)
        {
            if (dgv == null) throw new ArgumentNullException(nameof(dgv));
            if (rows == null) throw new ArgumentNullException(nameof(rows));

            dgv.SuspendLayout();

            // Optional: ensure ProductImage column is an Image column if it exists
            if (dgv.Columns.Contains("ProductImage") && !(dgv.Columns["ProductImage"] is DataGridViewImageColumn))
            {
                int idx = dgv.Columns["ProductImage"].Index;
                dgv.Columns.RemoveAt(idx);

                var imgCol = new DataGridViewImageColumn
                {
                    Name = "ProductImage",
                    HeaderText = "Image",
                    ImageLayout = DataGridViewImageCellLayout.Zoom
                };
                dgv.Columns.Insert(idx, imgCol);
            }

            dgv.Rows.Clear();

            foreach (var r in rows)
            {
                // Build image (prefers DB image, else loads from path)
                Image img = TryGetImage(r.ProductImage, r.ImagePath);

                // IMPORTANT:
                // Your existing code reads productName from Cells[0], category from Cells[2], stock from Cells[3], status from Cells[5]
                // So we add cells in that same order.
                //
                // If your column order is different, adjust this row.Add order OR change your CellClick indexes.
                int rowIndex = dgv.Rows.Add(
                    r.ProductName,       // 0
                    r.SKU,               // 1
                    r.Category,          // 2
                    r.CurrentStock,      // 3
                    r.ReorderPoint,      // 4
                    r.Status,            // 5
                    img                 // 6 (if your grid has ProductImage at this index)
                );

                dgv.Rows[rowIndex].Tag = r;

                // If you actually have a column named "ProductImage", set it by name to avoid index mismatches
                if (dgv.Columns.Contains("ProductImage"))
                {
                    dgv.Rows[rowIndex].Cells["ProductImage"].Value = img;
                }
            }

            dgv.ResumeLayout();
        }

        private static Image TryGetImage(byte[] blob, string path)
        {
            // 1) DB blob image
            if (blob != null && blob.Length > 0)
            {
                try
                {
                    using (var ms = new MemoryStream(blob))
                    {
                        return Image.FromStream(ms);
                    }
                }
                catch
                {
                    // ignore
                }
            }

            // 2) file path image
            if (!string.IsNullOrWhiteSpace(path))
            {
                try
                {
                    if (_imageCache.TryGetValue(path, out var cached))
                        return cached;

                    if (File.Exists(path))
                    {
                        // Use a copy so the file isn't locked
                        using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (var ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            var img = Image.FromStream(ms);
                            _imageCache[path] = img;
                            return img;
                        }
                    }
                }
                catch
                {
                    // ignore
                }
            }

            // 3) nothing
            return null;
        }
    }
}
