using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public static class ProductGridImageBinder
    {
        public class ProductRowModel
        {
            public int ProductInternalId { get; set; }
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string Category { get; set; }
            public int CurrentStock { get; set; }
            public int ReorderPoint { get; set; }
            public string Status { get; set; }
            public string SKU { get; set; }
            public string Brand { get; set; }
            public byte[] ProductImage { get; set; }
            public string ImagePath { get; set; }
        }

        private static readonly Dictionary<int, Image> imageCacheByProductId = new Dictionary<int, Image>();

        public static void EnsureImageColumn(DataGridView grid)
        {
            if (grid == null)
            {
                return;
            }

            if (!grid.Columns.Contains("ProductImage"))
            {
                if (grid.Columns.Contains("Image"))
                {
                    grid.Columns["Image"].Name = "ProductImage";
                }
                else
                {
                    var imageColumn = new DataGridViewImageColumn
                    {
                        Name = "ProductImage",
                        HeaderText = "Image",
                        ImageLayout = DataGridViewImageCellLayout.Zoom,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        FillWeight = 50
                    };
                    grid.Columns.Insert(1, imageColumn);
                }
            }
            else if (grid.Columns["ProductImage"] is DataGridViewImageColumn imgColumn)
            {
                imgColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

        public static void BindRows(DataGridView grid, IEnumerable<ProductRowModel> rows)
        {
            if (grid == null || rows == null)
            {
                return;
            }

            EnsureImageColumn(grid);

            Image placeholder = ImageService.GetPlaceholderImage();

            foreach (ProductRowModel row in rows)
            {
                Image resolvedImage = ResolveImage(row, placeholder);

                int rowIndex = grid.Rows.Add(
                    row.ProductName,
                    resolvedImage,
                    row.Category,
                    row.CurrentStock,
                    row.ReorderPoint,
                    row.Status,
                    null,
                    null,
                    null,
                    null
                );

                if (rowIndex >= 0 && rowIndex < grid.Rows.Count)
                {
                    grid.Rows[rowIndex].Tag = row;
                }
            }
        }

        private static Image ResolveImage(ProductRowModel row, Image placeholder)
        {
            if (row == null)
            {
                return placeholder;
            }

            if (row.ProductInternalId > 0 && imageCacheByProductId.TryGetValue(row.ProductInternalId, out Image cachedImage))
            {
                return cachedImage;
            }

            Image image = placeholder;

            if (row.ProductImage != null && row.ProductImage.Length > 0)
            {
                image = ImageService.ConvertBytesToImage(row.ProductImage) ?? placeholder;
            }
            else if (!string.IsNullOrWhiteSpace(row.ImagePath))
            {
                image = ProductImageManager.GetProductImage(row.ImagePath);
            }

            if (row.ProductInternalId > 0)
            {
                imageCacheByProductId[row.ProductInternalId] = image;
            }

            return image ?? placeholder;
        }

        public static void ClearCache()
        {
            imageCacheByProductId.Clear();
        }
    }
}
