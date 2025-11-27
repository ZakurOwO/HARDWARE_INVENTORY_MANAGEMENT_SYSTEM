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
        public DashboardMainPage()
        {
            InitializeComponent();
        }



        private void DashboardMainPage_Load(object sender, EventArgs e)
        {
            LoadSalesVsPurchaseChart();
            

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

         /*   //sample data for testing
            chartTopProducts.Series.Clear();
            var sp = chartTopProducts.Series.Add("Products");
            sp.ChartType = SeriesChartType.Bar;

            sp.Points.AddXY("Product A", 120);
            sp.Points.AddXY("Product B", 90);
            sp.Points.AddXY("Product C", 75);
            sp.Points.AddXY("Product D", 60);
            sp.Points.AddXY("Product E", 55);

            //sample data for testing
            chartSalesTrend.Series.Clear();
            var t = chartSalesTrend.Series.Add("Sales");
            t.ChartType = SeriesChartType.Line;
            t.BorderWidth = 3;

            t.Points.AddXY("Week 1", 12000);
            t.Points.AddXY("Week 2", 15000);
            t.Points.AddXY("Week 3", 17000);
            t.Points.AddXY("Week 4", 19000);  */
        }
        private void LoadSalesVsPurchaseChart()
        {
            chartSalesPurchase.Series.Clear();
            chartSalesPurchase.ChartAreas[0].BackColor = Color.Transparent;

            // Create 2 series (Sales & Purchase)
            Series salesSeries = chartSalesPurchase.Series.Add("Sales");
            salesSeries.ChartType = SeriesChartType.Column;
            salesSeries.Font = new Font("Lexend Light", 8);

            Series purchaseSeries = chartSalesPurchase.Series.Add("Purchase");
            purchaseSeries.ChartType = SeriesChartType.Column;
            purchaseSeries.Font = new Font("Lexend Light", 8);

            // Sample values (replace with DB values)
            int[] salesData = { 12000, 14000, 18000, 20000 };       // monthly sales
            int[] purchaseData = { 8000, 9000, 15000, 17000 };      // monthly purchases
            string[] months = { "Jan", "Feb", "Mar", "Apr" };

            // Add points
            for (int i = 0; i < months.Length; i++)
            {
                salesSeries.Points.AddXY(months[i], salesData[i]);
                purchaseSeries.Points.AddXY(months[i], purchaseData[i]);
            }

            // Styling for modern UI
            salesSeries.Color = Color.FromArgb(42, 134, 205);  
            purchaseSeries.Color = Color.FromArgb(204, 226, 243);   

            chartSalesPurchase.Legends[0].Enabled = true;
        }

        public void LoadSalesTrend()
        {
            Series salesSeries;
            if (chartSalesTrend.Series.IndexOf("Sales") >= 0)
            {
                salesSeries = chartSalesTrend.Series["Sales"];
                salesSeries.Points.Clear();
            }
            else
            {
                salesSeries = chartSalesTrend.Series.Add("Sales");
                salesSeries.ChartType = SeriesChartType.Line;
                salesSeries.BorderWidth = 3;
            }

            // If you don't have a working DB connection yet, don't let the method throw - log and return.
            try
            {
                // Use your application's connection string provider
                using (SqlConnection con = new SqlConnection(ConnectionString.DataSource))
                {
                    string query = @"
                        SELECT
                            CAST(date AS DATE) AS SalesDate,
                            SUM(amount) AS Total
                        FROM Sales
                        GROUP BY CAST(date AS DATE)
                        ORDER BY SalesDate
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            bool hasRows = false;
                            while (dr.Read())
                            {
                                hasRows = true;
                                DateTime dt = Convert.ToDateTime(dr["SalesDate"]);
                                decimal total = Convert.ToDecimal(dr["Total"]);

                                // use actual DateTime on X value so formatting/scale is correct
                                var p = salesSeries.Points.AddXY(dt, total);
                                
                            }

                            // If no rows, optionally add sample/fallback points (useful during development)
                            if (!hasRows)
                            {
                                // fallback sample for dev (comment/remove in production)
                                salesSeries.Points.AddXY(DateTime.Today.AddDays(-3), 12000);
                                salesSeries.Points.AddXY(DateTime.Today.AddDays(-2), 15000);
                                salesSeries.Points.AddXY(DateTime.Today.AddDays(-1), 17000);
                                salesSeries.Points.AddXY(DateTime.Today, 19000);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                // Log the error to Output (Debug/Console) and add fallback sample data so chart is visible
                System.Diagnostics.Debug.WriteLine("LoadSalesTrend error: " + ex.Message);

                // fallback sample - remove if you prefer no fallback
                salesSeries.Points.AddXY(DateTime.Today.AddDays(-3), 12000);
                salesSeries.Points.AddXY(DateTime.Today.AddDays(-2), 15000);
                salesSeries.Points.AddXY(DateTime.Today.AddDays(-1), 17000);
                salesSeries.Points.AddXY(DateTime.Today, 19000);
            }
        }

    }
}
