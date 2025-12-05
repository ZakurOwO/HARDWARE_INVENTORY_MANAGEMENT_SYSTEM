using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Sales_Report;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public static class ReportPdfExporter
    {
        public static bool ExportSalesByProduct(IEnumerable<SalesProductReport> data, string reportTitle, DateTime? startDate, DateTime? endDate)
        {
            return ExportReport(data, reportTitle, startDate, endDate, BuildSalesByProductTable);
        }

        public static bool ExportSalesByCustomer(IEnumerable<SalesCustomerReport> data, string reportTitle, DateTime? startDate, DateTime? endDate)
        {
            return ExportReport(data, reportTitle, startDate, endDate, BuildSalesByCustomerTable);
        }

        public static bool ExportSalesSummary(IEnumerable<SalesSummaryReport> data, string reportTitle, DateTime? startDate, DateTime? endDate)
        {
            return ExportReport(data, reportTitle, startDate, endDate, BuildSalesSummaryTable);
        }

        public static bool ExportDeliverySummary(IEnumerable<DeliverySummaryReportModel> data, string reportTitle, DateTime? startDate, DateTime? endDate)
        {
            return ExportReport(data, reportTitle, startDate, endDate, BuildDeliverySummaryTable);
        }

        public static bool ExportDeliveryVehicleUtilization(IEnumerable<DeliveryVehicleUtilizationReport> data, string reportTitle, DateTime? startDate, DateTime? endDate)
        {
            return ExportReport(data, reportTitle, startDate, endDate, BuildVehicleUtilizationTable);
        }

        public static bool ExportVehicleUsageTimeline(IEnumerable<VehicleUsageTimelineReport> data, string reportTitle, DateTime? startDate, DateTime? endDate)
        {
            return ExportReport(data, reportTitle, startDate, endDate, BuildVehicleTimelineTable);
        }

        private static bool ExportReport<T>(IEnumerable<T> data, string reportTitle, DateTime? startDate, DateTime? endDate, Action<Document, IEnumerable<T>> tableBuilder)
        {
            var dataList = data?.ToList() ?? new List<T>();
            if (!dataList.Any())
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (!TryGetSavePath(reportTitle, out string? filePath))
            {
                return false;
            }

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var document = new Document(PageSize.A4.Rotate(), 36, 36, 54, 54))
                {
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    AddDocumentHeader(document, reportTitle, startDate, endDate);
                    tableBuilder(document, dataList);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to export report: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static bool TryGetSavePath(string reportTitle, out string? path)
        {
            path = null;
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveDialog.Title = "Save Report as PDF";
                saveDialog.FileName = $"{SanitizeFileName(reportTitle)}_{DateTime.Now:yyyyMMdd}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    path = saveDialog.FileName;
                    return true;
                }
            }

            return false;
        }

        private static void AddDocumentHeader(Document document, string reportTitle, DateTime? startDate, DateTime? endDate)
        {
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var subTitleFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            var title = new Paragraph(reportTitle, titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 6f
            };
            document.Add(title);

            string dateRange = "All Dates";
            if (startDate.HasValue || endDate.HasValue)
            {
                string startText = startDate?.ToString("yyyy-MM-dd") ?? "...";
                string endText = endDate?.ToString("yyyy-MM-dd") ?? "...";
                dateRange = $"Date Range: {startText} - {endText}";
            }

            var subTitle = new Paragraph($"{dateRange}\nGenerated: {DateTime.Now:yyyy-MM-dd HH:mm}", subTitleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12f
            };
            document.Add(subTitle);
        }

        private static void BuildSalesByProductTable(Document document, IEnumerable<SalesProductReport> data)
        {
            var table = CreateTable(6, new[] { "Product ID", "Product Name", "Category", "Quantity Sold", "Unit Price", "Total Sales" });

            int totalQuantity = 0;
            decimal totalSales = 0;

            foreach (var item in data)
            {
                totalQuantity += item.QuantitySold;
                totalSales += item.TotalSales;

                table.AddCell(item.ProductID);
                table.AddCell(item.ProductName);
                table.AddCell(item.Category);
                table.AddCell(item.QuantitySold.ToString());
                table.AddCell(item.UnitPrice.ToString("₱#,##0.00"));
                table.AddCell(item.TotalSales.ToString("₱#,##0.00"));
            }

            AddTotalsRow(table, new[] { "Totals", string.Empty, string.Empty, totalQuantity.ToString(), string.Empty, totalSales.ToString("₱#,##0.00") });
            document.Add(table);
        }

        private static void BuildSalesByCustomerTable(Document document, IEnumerable<SalesCustomerReport> data)
        {
            var table = CreateTable(6, new[] { "Customer ID", "Customer Name", "Contact", "Orders", "Total Quantity", "Total Sales" });

            int totalOrders = 0;
            int totalQuantity = 0;
            decimal totalSales = 0;

            foreach (var item in data)
            {
                totalOrders += item.TotalOrders;
                totalQuantity += item.TotalQuantity;
                totalSales += item.TotalSales;

                table.AddCell(item.CustomerID);
                table.AddCell(item.CustomerName);
                table.AddCell(item.Contact);
                table.AddCell(item.TotalOrders.ToString());
                table.AddCell(item.TotalQuantity.ToString());
                table.AddCell(item.TotalSales.ToString("₱#,##0.00"));
            }

            AddTotalsRow(table, new[] { "Totals", string.Empty, string.Empty, totalOrders.ToString(), totalQuantity.ToString(), totalSales.ToString("₱#,##0.00") });
            document.Add(table);
        }

        private static void BuildSalesSummaryTable(Document document, IEnumerable<SalesSummaryReport> data)
        {
            var table = CreateTable(6, new[] { "Date", "Transactions", "Quantity Sold", "Total Sales", "Total Profit", "Avg Sale / Transaction" });

            int totalTransactions = 0;
            int totalQuantity = 0;
            decimal totalSales = 0;
            decimal totalProfit = 0;

            foreach (var item in data)
            {
                totalTransactions += item.NoOfTransactions;
                totalQuantity += item.TotalQuantitySold;
                totalSales += item.TotalSales;
                totalProfit += item.TotalProfit;

                table.AddCell(item.Date.ToString("yyyy-MM-dd"));
                table.AddCell(item.NoOfTransactions.ToString());
                table.AddCell(item.TotalQuantitySold.ToString());
                table.AddCell(item.TotalSales.ToString("₱#,##0.00"));
                table.AddCell(item.TotalProfit.ToString("₱#,##0.00"));
                table.AddCell(item.AvgSalePerTransaction.ToString("₱#,##0.00"));
            }

            AddTotalsRow(table, new[]
            {
                "Totals",
                totalTransactions.ToString(),
                totalQuantity.ToString(),
                totalSales.ToString("₱#,##0.00"),
                totalProfit.ToString("₱#,##0.00"),
                string.Empty
            });

            document.Add(table);
        }

        private static void BuildDeliverySummaryTable(Document document, IEnumerable<DeliverySummaryReportModel> data)
        {
            var table = CreateTable(4, new[] { "Delivery Date", "Status", "Deliveries", "Total Items" });

            int totalDeliveries = 0;
            int totalItems = 0;

            foreach (var item in data)
            {
                totalDeliveries += item.DeliveryCount;
                totalItems += item.TotalItems;

                table.AddCell(item.DeliveryDate.ToString("yyyy-MM-dd"));
                table.AddCell(item.Status);
                table.AddCell(item.DeliveryCount.ToString());
                table.AddCell(item.TotalItems.ToString());
            }

            AddTotalsRow(table, new[] { "Totals", string.Empty, totalDeliveries.ToString(), totalItems.ToString() });
            document.Add(table);
        }

        private static void BuildVehicleUtilizationTable(Document document, IEnumerable<DeliveryVehicleUtilizationReport> data)
        {
            var table = CreateTable(7, new[] { "Vehicle ID", "Plate Number", "Brand", "Model", "Type", "Status", "Assignments" });

            int totalAssignments = 0;
            foreach (var item in data)
            {
                totalAssignments += item.DeliveryAssignments;

                table.AddCell(item.VehicleID);
                table.AddCell(item.PlateNumber);
                table.AddCell(item.Brand);
                table.AddCell(item.Model);
                table.AddCell(item.VehicleType);
                table.AddCell(item.Status);
                table.AddCell(item.DeliveryAssignments.ToString());
            }

            AddTotalsRow(table, new[] { "Totals", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, totalAssignments.ToString() });
            document.Add(table);
        }

        private static void BuildVehicleTimelineTable(Document document, IEnumerable<VehicleUsageTimelineReport> data)
        {
            var table = CreateTable(3, new[] { "Assignment Date", "Plate Number", "Deliveries" });

            int totalDeliveries = 0;
            foreach (var item in data)
            {
                totalDeliveries += item.DeliveriesHandled;
                table.AddCell(item.AssignmentDate.ToString("yyyy-MM-dd"));
                table.AddCell(item.PlateNumber);
                table.AddCell(item.DeliveriesHandled.ToString());
            }

            AddTotalsRow(table, new[] { "Totals", string.Empty, totalDeliveries.ToString() });
            document.Add(table);
        }

        private static PdfPTable CreateTable(int columnCount, IEnumerable<string> headers)
        {
            var table = new PdfPTable(columnCount)
            {
                WidthPercentage = 100,
                SpacingBefore = 10f
            };

            var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            foreach (var header in headers)
            {
                var cell = new PdfPCell(new Phrase(header, headerFont))
                {
                    BackgroundColor = new BaseColor(230, 230, 230),
                    Padding = 5,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                table.AddCell(cell);
            }

            return table;
        }

        private static void AddTotalsRow(PdfPTable table, string[] totals)
        {
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            foreach (var value in totals)
            {
                table.AddCell(new PdfPCell(new Phrase(value, boldFont))
                {
                    Padding = 5,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                });
            }
        }

        private static string SanitizeFileName(string input)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                input = input.Replace(c, '_');
            }
            return input;
        }
    }
}
