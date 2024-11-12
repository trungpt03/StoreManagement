using OxyPlot.Series;
using OxyPlot;
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

namespace PRN212_Project_Team9.ChartReport
{
    /// <summary>
    /// Interaction logic for ChartEmployee.xaml
    /// </summary>
    public partial class ChartEmployee : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();
        public ChartEmployee()
        {
            InitializeComponent();
            loadData();
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
        }
        public void loadData()
        {
            var data = context.Employees
                .GroupBy(e => e.Position.PositionName)
                .Select(g => new
                {
                    PositionName = g.Key,
                    EmployeeCount = g.Count()
                })
                .ToList();

            CreatePieChart(data);
        }
        private void CreatePieChart(dynamic data)
        {
            var plotModel = new PlotModel { Title = "Employee Distribution by Position" };
            var pieSeries = new PieSeries { StrokeThickness = 1, AngleSpan = 360, StartAngle = 0 };

            foreach (var item in data)
            {
                pieSeries.Slices.Add(new PieSlice(item.PositionName, item.EmployeeCount) { IsExploded = true });
            }

            plotModel.Series.Add(pieSeries);
            EmployeeChart.Model = plotModel;
        }

    }
}
