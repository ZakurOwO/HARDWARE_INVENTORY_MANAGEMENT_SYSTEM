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
            // LoadSalesVsPurchaseChart();
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
            chartSalesPurchase.Series.Clear();
            chartSalesPurchase.ChartAreas[0].BackColor = Color.Transparent;

            Series salesSeries = chartSalesPurchase.Series.Add("Sales");
            salesSeries.ChartType = SeriesChartType.Column;

            Series purchaseSeries = chartSalesPurchase.Series.Add("Purchases");
            purchaseSeries.ChartType = SeriesChartType.Column;

            using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
            {
                con.Open();

                // SALES
                string salesQuery = @"
                    SELECT 
                        FORMAT(transaction_date, 'MMM') AS MonthName,
                        SUM(TI.quantity * TI.selling_price) AS TotalSales
                    FROM Transactions T
                    INNER JOIN TransactionItems TI ON T.transaction_id = TI.transaction_id
                    GROUP BY FORMAT(transaction_date, 'MMM'), MONTH(transaction_date)
                    ORDER BY MONTH(transaction_date);
                ";

                SqlCommand cmdSales = new SqlCommand(salesQuery, con);
                SqlDataReader sdr = cmdSales.ExecuteReader();

                Dictionary<string, decimal> salesData = new Dictionary<string, decimal>();

                while (sdr.Read())
                {
                    string month = sdr["MonthName"].ToString();
                    decimal value = sdr["TotalSales"] == DBNull.Value ? 0 : Convert.ToDecimal(sdr["TotalSales"]);
                    salesData[month] = value;
                }
                sdr.Close();

                // PURCHASES
                string purchaseQuery = @"
                    SELECT 
                        FORMAT(po_date, 'MMM') AS MonthName,
                        SUM(POI.quantity_ordered * POI.unit_price) AS TotalPurchase
                    FROM PurchaseOrders PO
                    INNER JOIN PurchaseOrderItems POI ON PO.po_id = POI.po_id
                    GROUP BY FORMAT(po_date, 'MMM'), MONTH(po_date)
                    ORDER BY MONTH(po_date);
                ";

                SqlCommand cmdPurchase = new SqlCommand(purchaseQuery, con);
                SqlDataReader pdr = cmdPurchase.ExecuteReader();

                Dictionary<string, decimal> purchaseData = new Dictionary<string, decimal>();

                while (pdr.Read())
                {
                    string month = pdr["MonthName"].ToString();
                    decimal value = pdr["TotalPurchase"] == DBNull.Value ? 0 : Convert.ToDecimal(pdr["TotalPurchase"]);
                    purchaseData[month] = value;
                }
                pdr.Close();

                // Add both to chart (combined months)
                foreach (var month in salesData.Keys.Union(purchaseData.Keys))
                {
                    decimal s = salesData.ContainsKey(month) ? salesData[month] : 0;
                    decimal p = purchaseData.ContainsKey(month) ? purchaseData[month] : 0;

                    salesSeries.Points.AddXY(month, s);
                    purchaseSeries.Points.AddXY(month, p);
                }

                con.Close();
            }
        }

        public void LoadSalesTrend()
        {
            if (chartSalesTrend.Series.IndexOf("Sales") == -1)
                chartSalesTrend.Series.Add("Sales");

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
                    WHERE current_stock <= reorder_point
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
    }
}
