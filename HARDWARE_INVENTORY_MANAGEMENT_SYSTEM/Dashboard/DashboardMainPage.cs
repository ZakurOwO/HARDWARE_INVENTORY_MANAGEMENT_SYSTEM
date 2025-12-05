using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Dashboard
{
    public partial class DashboardMainPage : UserControl
    {
        private ProfileMenuPainter profileMenuPainter = ProfileMenuPainter.CreateFromUserSession();
        public DashboardMainPage()
        {
            InitializeComponent();
            ProfileMenu.Paint += ProfileBtn_Paint;
            this.Load += DashboardMainPage_Load;
        }

        
        private void ProfileBtn_Paint(object sender, PaintEventArgs e)
        {
            profileMenuPainter?.Draw(e);
        }

        private void DashboardMainPage_Load(object sender, EventArgs e)
        {
            dgvLowStock.ClearSelection();
            LoadKeyMetrics();
            LoadSalesVsPurchase();
            LoadSalesTrend();
            LoadBestSellers();
            LoadLowStock();

            // Style for Sales Trend Chart
            if (chartSalesTrend.Series.Count == 0)
                chartSalesTrend.Series.Add("Sales");

            chartSalesTrend.Series[0].ChartType = SeriesChartType.Line;
            chartSalesTrend.Series[0].BorderWidth = 3;
            chartSalesTrend.ChartAreas[0].AxisX.Interval = 1;
            chartSalesTrend.ChartAreas[0].AxisX.LabelStyle.Format = "MMM dd";

            chartSalesPurchase.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartSalesPurchase.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Silver;
            chartSalesPurchase.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.DimGray;
            chartSalesPurchase.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Lexend Light", 8);
            chartSalesPurchase.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.DimGray;
            chartSalesPurchase.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Lexend Light", 8);
            chartSalesPurchase.ChartAreas[0].BackColor = Color.Transparent;
            chartSalesPurchase.BackColor = Color.Transparent;

        }
        private void LoadSalesVsPurchaseChart()
        {


        }

        public void LoadSalesVsPurchase()
        {
            chartSalesPurchase.ChartAreas[0].BackColor = Color.Transparent;

            EnsureSeries(chartSalesPurchase, "Sales", SeriesChartType.Column);
            EnsureSeries(chartSalesPurchase, "Purchases", SeriesChartType.Column);

            chartSalesPurchase.Series["Sales"].Points.Clear();
            chartSalesPurchase.Series["Purchases"].Points.Clear();

            using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
            {
                con.Open();

                Dictionary<DateTime, decimal> salesData = new Dictionary<DateTime, decimal>();
                Dictionary<DateTime, decimal> purchaseData = new Dictionary<DateTime, decimal>();

                string salesQuery = @"
                    SELECT
                        YEAR(transaction_date) AS TranYear,
                        MONTH(transaction_date) AS TranMonth,
                        SUM(TI.quantity * TI.selling_price) AS TotalSales
                    FROM Transactions T
                    INNER JOIN TransactionItems TI ON T.transaction_id = TI.transaction_id
                    GROUP BY YEAR(transaction_date), MONTH(transaction_date)
                    ORDER BY YEAR(transaction_date), MONTH(transaction_date);
                ";

                SqlCommand cmdSales = new SqlCommand(salesQuery, con);
                SqlDataReader sdr = cmdSales.ExecuteReader();

                while (sdr.Read())
                {
                    int year = Convert.ToInt32(sdr["TranYear"]);
                    int month = Convert.ToInt32(sdr["TranMonth"]);
                    decimal value = sdr["TotalSales"] == DBNull.Value ? 0 : Convert.ToDecimal(sdr["TotalSales"]);
                    DateTime key = new DateTime(year, month, 1);
                    salesData[key] = value;
                }
                sdr.Close();

                string purchaseQuery = @"
                    SELECT
                        YEAR(po_date) AS POYear,
                        MONTH(po_date) AS POMonth,
                        SUM(POI.quantity_ordered * POI.unit_price) AS TotalPurchase
                    FROM PurchaseOrders PO
                    INNER JOIN PurchaseOrderItems POI ON PO.po_id = POI.po_id
                    GROUP BY YEAR(po_date), MONTH(po_date)
                    ORDER BY YEAR(po_date), MONTH(po_date);
                ";

                SqlCommand cmdPurchase = new SqlCommand(purchaseQuery, con);
                SqlDataReader pdr = cmdPurchase.ExecuteReader();

                while (pdr.Read())
                {
                    int year = Convert.ToInt32(pdr["POYear"]);
                    int month = Convert.ToInt32(pdr["POMonth"]);
                    decimal value = pdr["TotalPurchase"] == DBNull.Value ? 0 : Convert.ToDecimal(pdr["TotalPurchase"]);
                    DateTime key = new DateTime(year, month, 1);
                    purchaseData[key] = value;
                }
                pdr.Close();

                foreach (DateTime monthKey in salesData.Keys.Union(purchaseData.Keys).OrderBy(k => k))
                {
                    decimal s = salesData.ContainsKey(monthKey) ? salesData[monthKey] : 0;
                    decimal p = purchaseData.ContainsKey(monthKey) ? purchaseData[monthKey] : 0;

                    chartSalesPurchase.Series["Sales"].Points.AddXY(monthKey.ToString("MMM yyyy"), s);
                    chartSalesPurchase.Series["Purchases"].Points.AddXY(monthKey.ToString("MMM yyyy"), p);
                }

                con.Close();
            }
        }

        public void LoadSalesTrend()
        {
            EnsureSeries(chartSalesTrend, "Sales", SeriesChartType.Line);
            chartSalesTrend.Series["Sales"].Points.Clear();

            using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
            {
                string query = @"
                    SELECT
                        CAST(transaction_date AS DATE) AS SalesDate,
                        SUM(TI.quantity * TI.selling_price) AS Total
                    FROM Transactions T
                    INNER JOIN TransactionItems TI ON T.transaction_id = TI.transaction_id
                    GROUP BY CAST(transaction_date AS DATE)
                    ORDER BY SalesDate;
                ";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DateTime date = Convert.ToDateTime(dr["SalesDate"]);
                    decimal value = dr["Total"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Total"]);

                    chartSalesTrend.Series["Sales"].Points.AddXY(
                        date.ToString("MMM dd"),
                        value
                    );
                }

                con.Close();
            }
        }

        public void LoadBestSellers()
        {
            EnsureSeries(chartBestSeller, "Products", SeriesChartType.Column);
            chartBestSeller.Series["Products"].Points.Clear();

            using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
            {
                string query = @"
                    SELECT TOP 5 
                        P.product_name, 
                        SUM(TI.quantity) AS TotalSold
                    FROM TransactionItems TI
                    INNER JOIN Products P ON TI.product_id = P.ProductInternalID
                    GROUP BY P.product_name
                    ORDER BY TotalSold DESC;
                ";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    chartBestSeller.Series["Products"].Points.AddXY(
                        dr["product_name"].ToString(),
                        Convert.ToInt32(dr["TotalSold"])
                    );
                }

                con.Close();
            }
        }

        public void LoadLowStock()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
            {
                string query = @"
                    SELECT
                        product_name AS [Product],
                        current_stock AS [Stock],
                        reorder_point AS [Reorder]
                    FROM Products
                    WHERE current_stock <= reorder_point AND active = 1
                    ORDER BY current_stock ASC;
                ";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvLowStock.DataSource = dt;
            }

        }

        private void ProfileMenu_Click(object sender, EventArgs e)
        {
            var mainForm = this.FindForm() as MainDashBoard;
            if (mainForm != null)
            {
                SettingsMainClass.ShowSettingsPanel(mainForm.MainContentPanelAccess);
            }
        }

        private void LoadKeyMetrics()
        {
            DateTime now = DateTime.Now;
            DateTime todayStart = StartOfDay(now);
            DateTime tomorrowStart = todayStart.AddDays(1);
            DateTime monthStart = StartOfMonth(now);
            DateTime nextMonthStart = monthStart.AddMonths(1);

            decimal todaySales = GetSalesTotal(todayStart, tomorrowStart);
            decimal monthSales = GetSalesTotal(monthStart, nextMonthStart);
            int transactionsToday = GetTransactionsCount(todayStart, tomorrowStart);
            int lowStockCount = GetLowStockCount();
            int outOfStockCount = GetOutOfStockCount();
            int pendingDeliveries = GetPendingDeliveriesCount();
            int activeProducts = GetActiveProductsCount();

            decimal totalSalesAllTime = GetSalesTotal(null, null);
            int totalOrders = GetTransactionsCount(null, null);
            int totalStocks = GetTotalStocks();

            SetLabelTextSafe(this, "lblTodaySales", "₱" + todaySales.ToString("N2"));
            SetLabelTextSafe(this, "lblMonthlySales", "₱" + monthSales.ToString("N2"));
            SetLabelTextSafe(this, "lblTodayTransactions", transactionsToday.ToString());
            SetLabelTextSafe(this, "lblLowStockCount", lowStockCount.ToString());
            SetLabelTextSafe(this, "lblOutOfStockCount", outOfStockCount.ToString());
            SetLabelTextSafe(this, "lblPendingDeliveries", pendingDeliveries.ToString());
            SetLabelTextSafe(this, "lblActiveProducts", activeProducts.ToString());

            SetKeyMetricsValue(reportsKeyMetrics1, "Total Sales", FormatCurrency(totalSalesAllTime));
            SetKeyMetricsValue(reportsKeyMetrics2, "Total Orders", totalOrders.ToString());
            SetKeyMetricsValue(reportsKeyMetrics3, "Low Stock Alert", lowStockCount.ToString());
            SetKeyMetricsValue(reportsKeyMetrics4, "Total Stocks", totalStocks.ToString());
        }

        private decimal GetSalesTotal(DateTime? startDate, DateTime? endDate)
        {
            string query = @"
                SELECT ISNULL(SUM(TI.quantity * TI.selling_price), 0)
                FROM Transactions T
                INNER JOIN TransactionItems TI ON T.transaction_id = TI.transaction_id
                WHERE (@startDate IS NULL OR T.transaction_date >= @startDate)
                    AND (@endDate IS NULL OR T.transaction_date < @endDate);
            ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@startDate", (object)startDate ?? DBNull.Value),
                new SqlParameter("@endDate", (object)endDate ?? DBNull.Value)
            };

            return ExecuteScalarDecimal(query, parameters);
        }

        private int GetTransactionsCount(DateTime? startDate, DateTime? endDate)
        {
            string query = @"
                SELECT COUNT(*)
                FROM Transactions
                WHERE (@startDate IS NULL OR transaction_date >= @startDate)
                    AND (@endDate IS NULL OR transaction_date < @endDate);
            ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@startDate", (object)startDate ?? DBNull.Value),
                new SqlParameter("@endDate", (object)endDate ?? DBNull.Value)
            };

            return ExecuteScalarInt(query, parameters);
        }

        private int GetLowStockCount()
        {
            string query = @"
                SELECT COUNT(*)
                FROM Products
                WHERE current_stock <= reorder_point AND active = 1;
            ";

            return ExecuteScalarInt(query, null);
        }

        private int GetOutOfStockCount()
        {
            string query = @"
                SELECT COUNT(*)
                FROM Products
                WHERE current_stock <= 0 AND active = 1;
            ";

            return ExecuteScalarInt(query, null);
        }

        private int GetPendingDeliveriesCount()
        {
            string query = @"
                SELECT COUNT(*)
                FROM Deliveries
                WHERE status IS NULL OR status <> 'Completed' OR status IN ('Scheduled', 'Assigned', 'Pending');
            ";

            return ExecuteScalarInt(query, null);
        }

        private int GetActiveProductsCount()
        {
            string query = @"
                SELECT COUNT(*)
                FROM Products
                WHERE active = 1;
            ";

            return ExecuteScalarInt(query, null);
        }

        private int GetTotalStocks()
        {
            string query = @"
                SELECT ISNULL(SUM(current_stock), 0)
                FROM Products
                WHERE active = 1;
            ";

            return ExecuteScalarInt(query, null);
        }

        private decimal ExecuteScalarDecimal(string query, SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        return 0m;
                    }

                    decimal value;
                    if (decimal.TryParse(result.ToString(), out value))
                    {
                        return value;
                    }

                    return 0m;
                }
            }
        }

        private int ExecuteScalarInt(string query, SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        return 0;
                    }

                    int value;
                    if (int.TryParse(result.ToString(), out value))
                    {
                        return value;
                    }

                    return 0;
                }
            }
        }

        private DateTime StartOfMonth(DateTime now)
        {
            return new DateTime(now.Year, now.Month, 1);
        }

        private DateTime StartOfDay(DateTime now)
        {
            return new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
        }

        private void EnsureSeries(Chart chart, string seriesName, SeriesChartType type)
        {
            if (chart.Series.IndexOf(seriesName) == -1)
            {
                chart.Series.Add(seriesName);
            }

            chart.Series[seriesName].ChartType = type;
        }

        private void SetLabelTextSafe(Control parent, string name, string text)
        {
            Control[] foundControls = parent.Controls.Find(name, true);
            if (foundControls != null && foundControls.Length > 0)
            {
                foundControls[0].Text = text;
            }
            else
            {
                // Label placeholder for developers to map later if renamed
            }
        }

        private void SetKeyMetricsValue(Reports_Module.ReportsKeyMetrics control, string title, string value)
        {
            if (control != null)
            {
                control.Title = title;
                control.ValueText = value;
            }
        }

        private string FormatCurrency(decimal amount)
        {
            return "₱" + amount.ToString("N2");
        }
    }
}
