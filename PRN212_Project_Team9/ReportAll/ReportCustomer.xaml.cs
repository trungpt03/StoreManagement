using ClosedXML.Excel;
using PRN212_Project_Team9.ChartReport;
using PRN212_Project_Team9.Models;
using PRN212_Project_Team9.ReportAll;
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
using ClosedXML.Excel;
using Microsoft.Win32;

namespace PRN212_Project_Team9.Report
{
    /// <summary>
    /// Interaction logic for ReportCustomer.xaml
    /// </summary>
    public partial class ReportCustomer : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();
        private ChartReport.CharCustomer chartCustomer;
        public ReportCustomer()
        {
            InitializeComponent();
            loadData();
            chartCustomer = new CharCustomer();
            chartCustomer.Show();
            this.Closing += ReportCustomer_Closing;

        }
        private void ReportCustomer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (chartCustomer != null)
            {
                chartCustomer.Close(); // Đóng ChartEmployee
            }
        }


        public void loadData()
        {
            var reportData = from cp in context.CustomerPoints
                             join c in context.Customers on cp.CustomerId equals c.CustomerId
                             join lp in context.LoyaltyPrograms on cp.ProgramId equals lp.ProgramId
                             select new
                             {
                                 CustomerId = c.CustomerId, // Lấy CustomerID từ bảng Customers
                                 CustomerName = c.CustomerName,
                                 PhoneNumber = c.PhoneNumber,
                                 ProgramName = lp.ProgramName,
                                 Points = cp.Points
                             };
            dgReportCustomer.ItemsSource = reportData.ToList();
        }

        private void btnReportEmployee_Click(object sender, RoutedEventArgs e)
        {
            ReportEmployee reportEmployee = new ReportEmployee();
            reportEmployee.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Close();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            var reportData = (IEnumerable<dynamic>)dgReportCustomer.ItemsSource;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = "CustomerReport.xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Customer Points");

                    // Ghi tiêu đề cho từng cột
                    worksheet.Cell(1, 1).Value = "Customer ID";
                    worksheet.Cell(1, 2).Value = "Customer Name";
                    worksheet.Cell(1, 3).Value = "Phone Number";
                    worksheet.Cell(1, 4).Value = "Program Name";
                    worksheet.Cell(1, 5).Value = "Points";

                    // Ghi dữ liệu của từng khách hàng
                    int row = 2; // Bắt đầu ghi dữ liệu từ hàng thứ 2
                    foreach (var data in reportData)
                    {
                        worksheet.Cell(row, 1).Value = data.CustomerId;
                        worksheet.Cell(row, 2).Value = data.CustomerName;
                        worksheet.Cell(row, 3).Value = data.PhoneNumber;
                        worksheet.Cell(row, 4).Value = data.ProgramName;
                        worksheet.Cell(row, 5).Value = data.Points;
                        row++;
                    }

                    // Lưu workbook vào đường dẫn đã chọn
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Export Successfully", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
