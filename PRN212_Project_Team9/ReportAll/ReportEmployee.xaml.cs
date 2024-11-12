using PRN212_Project_Team9.ChartReport;
using PRN212_Project_Team9.Models;
using PRN212_Project_Team9.Report;
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

namespace PRN212_Project_Team9.ReportAll
{
    /// <summary>
    /// Interaction logic for ReportEmployee.xaml
    /// </summary>
    public partial class ReportEmployee : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();
        private ChartEmployee chartEmployee;
        public ReportEmployee()
        {
            InitializeComponent();
            loadData();
            chartEmployee = new ChartEmployee();
            chartEmployee.Show();
            this.Closing += ReportEmployee_Closing;

        }
        private void ReportEmployee_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (chartEmployee != null)
            {
                chartEmployee.Close();
            }
        }

        public void loadData()
        {
            var listEmployee = context.Employees.ToList();
            dgReportEmployee.ItemsSource = listEmployee;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Close();
        }

        private void btnReportCustomer_Click(object sender, RoutedEventArgs e)
        {
            ReportCustomer customer = new ReportCustomer();
            customer.Show();
            this.Close();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            var listEmployee = context.Employees.ToList();

            // Chọn đường dẫn để lưu tệp Excel
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = "EmployeeReport.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employees");

                    // Ghi tiêu đề cho từng cột
                    worksheet.Cell(1, 1).Value = "Employee ID";
                    worksheet.Cell(1, 2).Value = "Employee Name";
                    worksheet.Cell(1, 3).Value = "Account";
                    worksheet.Cell(1, 4).Value = "Phone Number";
                    worksheet.Cell(1, 5).Value = "Hire Date";
                    worksheet.Cell(1, 6).Value = "Position ID";

                    // Ghi dữ liệu của từng nhân viên
                    for (int i = 0; i < listEmployee.Count; i++)
                    {
                        var employee = listEmployee[i];
                        worksheet.Cell(i + 2, 1).Value = employee.EmployeeId;
                        worksheet.Cell(i + 2, 2).Value = employee.EmployeeName;
                        worksheet.Cell(i + 2, 3).Value = employee.Account;
                        worksheet.Cell(i + 2, 4).Value = employee.PhoneNumber;
                        worksheet.Cell(i + 2, 5).Value = employee.HireDate;
                        worksheet.Cell(i + 2, 6).Value = employee.PositionId;
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
