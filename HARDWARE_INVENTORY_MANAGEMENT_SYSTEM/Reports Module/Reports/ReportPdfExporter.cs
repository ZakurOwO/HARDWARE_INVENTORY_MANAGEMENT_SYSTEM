using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Sales_Report;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public static class ReportPdfExporter
    {
        private static readonly PdfFont RegularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
        private static readonly PdfFont BoldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

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

            string filePath;
            if (!TryGetSavePath(reportTitle, out filePath))
            {
                return false;
            }

            try
            {
                using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                using var pdfWriter = new PdfWriter(stream);
                using var pdfDocument = new PdfDocument(pdfWriter);

                pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());

                using var document = new Document(pdfDocument);
                document.SetMargins(54, 36, 54, 36);

                AddDocumentHeader(document, reportTitle, startDate, endDate);
                tableBuilder(document, dataList);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to export report: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static bool TryGetSavePath(string reportTitle, out string path)
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
            var title = new Paragraph(reportTitle)
                .SetFont(BoldFont)
                .SetFontSize(16)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginBottom(6f);
            document.Add(title);

            string dateRange = "All Dates";
            if (startDate.HasValue || endDate.HasValue)
            {
                string startText = startDate?.ToString("yyyy-MM-dd") ?? "...";
                string endText = endDate?.ToString("yyyy-MM-dd") ?? "...";
                dateRange = $"Date Range: {startText} - {endText}";
            }

            var subTitle = new Paragraph($"{dateRange}\nGenerated: {DateTime.Now:yyyy-MM-dd HH:mm}")
                .SetFont(RegularFont)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginBottom(12f);
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

                table.AddCell(CreateBodyCell(item.ProductID));
                table.AddCell(CreateBodyCell(item.ProductName));
                table.AddCell(CreateBodyCell(item.Category));
                table.AddCell(CreateBodyCell(item.QuantitySold.ToString()));
                table.AddCell(CreateBodyCell(item.UnitPrice.ToString("₱#,##0.00")));
                table.AddCell(CreateBodyCell(item.TotalSales.ToString("₱#,##0.00")));
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

                table.AddCell(CreateBodyCell(item.CustomerID));
                table.AddCell(CreateBodyCell(item.CustomerName));
                table.AddCell(CreateBodyCell(item.Contact));
                table.AddCell(CreateBodyCell(item.TotalOrders.ToString()));
                table.AddCell(CreateBodyCell(item.TotalQuantity.ToString()));
                table.AddCell(CreateBodyCell(item.TotalSales.ToString("₱#,##0.00")));
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

                table.AddCell(CreateBodyCell(item.Date.ToString("yyyy-MM-dd")));
                table.AddCell(CreateBodyCell(item.NoOfTransactions.ToString()));
                table.AddCell(CreateBodyCell(item.TotalQuantitySold.ToString()));
                table.AddCell(CreateBodyCell(item.TotalSales.ToString("₱#,##0.00")));
                table.AddCell(CreateBodyCell(item.TotalProfit.ToString("₱#,##0.00")));
                table.AddCell(CreateBodyCell(item.AvgSalePerTransaction.ToString("₱#,##0.00")));
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

                table.AddCell(CreateBodyCell(item.DeliveryDate.ToString("yyyy-MM-dd")));
                table.AddCell(CreateBodyCell(item.Status));
                table.AddCell(CreateBodyCell(item.DeliveryCount.ToString()));
                table.AddCell(CreateBodyCell(item.TotalItems.ToString()));
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

                table.AddCell(CreateBodyCell(item.VehicleID));
                table.AddCell(CreateBodyCell(item.PlateNumber));
                table.AddCell(CreateBodyCell(item.Brand));
                table.AddCell(CreateBodyCell(item.Model));
                table.AddCell(CreateBodyCell(item.VehicleType));
                table.AddCell(CreateBodyCell(item.Status));
                table.AddCell(CreateBodyCell(item.DeliveryAssignments.ToString()));
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
                table.AddCell(CreateBodyCell(item.AssignmentDate.ToString("yyyy-MM-dd")));
                table.AddCell(CreateBodyCell(item.PlateNumber));
                table.AddCell(CreateBodyCell(item.DeliveriesHandled.ToString()));
            }

            AddTotalsRow(table, new[] { "Totals", string.Empty, totalDeliveries.ToString() });
            document.Add(table);
        }

        private static Table CreateTable(int columnCount, IEnumerable<string> headers)
        {
            var table = new Table(columnCount)
                .SetWidth(UnitValue.CreatePercentValue(100))
                .SetMarginTop(10f);

            var headerFont = BoldFont;
            foreach (var header in headers)
            {
                var cell = new Cell()
                    .Add(new Paragraph(header).SetFont(headerFont).SetFontSize(10))
                    .SetBackgroundColor(new DeviceRgb(230, 230, 230))
                    .SetPadding(5)
                    .SetTextAlignment(TextAlignment.CENTER);
                table.AddCell(cell);
            }

            return table;
        }

        private static void AddTotalsRow(Table table, string[] totals)
        {
            foreach (var value in totals)
            {
                var cell = new Cell()
                    .Add(new Paragraph(value).SetFont(BoldFont).SetFontSize(10))
                    .SetPadding(5)
                    .SetTextAlignment(TextAlignment.RIGHT);
                table.AddCell(cell);
            }
        }

        private static Cell CreateBodyCell(string text)
        {
            return new Cell()
                .Add(new Paragraph(text).SetFont(RegularFont).SetFontSize(10))
                .SetPadding(5);
        }

        private static string SanitizeFileName(string input)
        {
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                input = input.Replace(c, '_');
            }
            return input;
        }
    }
}
