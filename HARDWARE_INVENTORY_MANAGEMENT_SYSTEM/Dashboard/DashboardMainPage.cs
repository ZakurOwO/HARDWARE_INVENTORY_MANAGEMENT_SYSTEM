using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        
    }
}
