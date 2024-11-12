using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using OxyPlot.Series;
using OxyPlot;
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
using PRN212_Project_Team9.Models;

namespace PRN212_Project_Team9.ChartReport
{
    /// <summary>
    /// Interaction logic for CharCustomer.xaml
    /// </summary>
    public partial class CharCustomer : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();
        public CharCustomer()
        {
            InitializeComponent();
            LoadData();
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;

        }
        public class ProgramPoints
        {
            public string ProgramName { get; set; }
            public int TotalPoints { get; set; }
        }

        public void LoadData()
        {
            var reportData = from cp in context.CustomerPoints
                             join lp in context.LoyaltyPrograms on cp.ProgramId equals lp.ProgramId
                             group cp by lp.ProgramName into g
                             select new ProgramPoints
                             {
                                 ProgramName = g.Key,
                                 TotalPoints = g.Sum(cp => cp.Points) ?? 0
                             };

            var dataList = reportData.ToList();

            // Kiểm tra số lượng dữ liệu
            if (dataList.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị biểu đồ.");
                return;
            }

            CreatePieChart(dataList);
        }

        private void CreatePieChart(List<ProgramPoints> data)
        {
            var plotModel = new PlotModel { Title = "Customer Program Points" };
            var pieSeries = new PieSeries { StrokeThickness = 1, AngleSpan = 360, StartAngle = 0 };

            foreach (var item in data)
            {
                // Kiểm tra giá trị TotalPoints để tránh lỗi
                if (item.TotalPoints > 0)
                {
                    pieSeries.Slices.Add(new PieSlice(item.ProgramName, item.TotalPoints) { IsExploded = true });
                }
            }

            plotModel.Series.Add(pieSeries); // Thêm pieSeries vào plotModel
            CustomerChart.Model = plotModel; // Gán mô hình cho CustomerChart
        }


    }
}
