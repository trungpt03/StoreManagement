
using PRN212_Project_Team9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using PRN212_Project_Team9.ReportAll;

namespace PRN212_Project_Team9
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();
        public Admin()
        {
            InitializeComponent();
            LoadDashboardData();
            LoadMonthlyRevenueChart();
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employees employee = new();
            employee.Show();
            this.Close();
        }

        private void btnDashBoard_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Close();
        }
        private void LoadDashboardData()
        {
            TotalCustomersText.Text = context.Customers.Count().ToString();
            ActiveProgramsText.Text = context.LoyaltyPrograms.Count(lp => lp.StartDate <= System.DateTime.Now && (lp.EndDate == null || lp.EndDate >= System.DateTime.Now)).ToString();
            TotalPointsText.Text = context.CustomerPoints.Sum(cp => cp.Points).ToString();
            TotalOrdersText.Text = context.Orders.Count().ToString();
            TotalStockText.Text = context.Inventories.Sum(inv => inv.Quantity).ToString();
            TotalRevenueText.Text = context.Orders.Sum(order => order.TotalAmount).ToString();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow logout = new MainWindow();
            logout.Show();
            this.Close();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            ReportEmployee report = new ReportEmployee();
            report.Show();
            this.Close();
        }
        private void LoadMonthlyRevenueChart()
        {
            // Tạo một đối tượng PlotModel để chứa các thông tin của biểu đồ
            var model = new PlotModel { Title = "Monthly Revenue" };

            // Thiết lập trục X là trục danh mục (CategoryAxis) để hiển thị tháng
            var xAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                Title = $"Month ({DateTime.Now.Year})",
                Labels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
            };
            model.Axes.Add(xAxis);

            // Thiết lập trục Y để hiển thị doanh thu
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Revenue ($)",
                Minimum = 0
            };
            model.Axes.Add(yAxis);

            // Lấy dữ liệu doanh thu hàng tháng
            var monthlyRevenue = context.Orders
                .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == DateTime.Now.Year)
                .GroupBy(o => o.OrderDate.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Revenue = g.Sum(o => o.TotalAmount)
                })
                .ToList();

            // Tạo một LineSeries để biểu diễn dữ liệu thay cho BarSeries
            var lineSeries = new LineSeries
            {
                Title = "Revenue",
                Color = OxyColor.Parse("#4CAF50"), // Màu của đường biểu đồ
                MarkerType = MarkerType.Circle, // Marker cho mỗi điểm dữ liệu
                MarkerSize = 4 // Kích thước của Marker
            };

            // Thêm dữ liệu vào LineSeries
            for (int i = 1; i <= 12; i++)
            {
                var revenue = monthlyRevenue.FirstOrDefault(m => m.Month == i)?.Revenue ?? 0;
                lineSeries.Points.Add(new DataPoint(i - 1, (double)revenue)); // Trục X là i - 1 để trùng khớp với CategoryAxis
            }

            // Thêm LineSeries vào model
            model.Series.Add(lineSeries);

            // Gán model cho PlotView trong XAML
            RevenueChart.Model = model;
            RevenueChart.InvalidatePlot(true); // Làm mới biểu đồ
        }




    }
}
