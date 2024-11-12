using Microsoft.Identity.Client.NativeInterop;
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

namespace PRN212_Project_Team9
{
    /// <summary>
    /// Interaction logic for ProfileEmployee.xaml
    /// </summary>
    public partial class ProfileEmployee : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();
        public ProfileEmployee()
        {
            InitializeComponent();
            loadData();
        }
        public void loadData()
        {
            try
            {
                // Tìm nhân viên dựa vào EmployeeId từ AppMemory.Id
                var employee = context.Employees
                    .Where(e => e.EmployeeId == AppMemory.Id)
                    .Select(e => new
                    {
                        e.EmployeeId,
                        e.EmployeeName,
                        e.Account, // Điều chỉnh tên nếu khác
                        e.Password,
                        e.PhoneNumber,
                        e.HireDate,
                        PositionName = e.Position.PositionName
                    })
                    .FirstOrDefault();

                if (employee != null)
                {
                    txtEmployeeId.Text = employee.EmployeeId.ToString();
                    txtEmployeeName.Text = employee.EmployeeName;
                    txtAccount.Text = employee.Account;
                    txtPassword.Password = employee.Password;
                    txtPhoneNumber.Text = employee.PhoneNumber;
                    txtHireDate.Text = employee.HireDate?.ToString("dd/MM/yyyy") ?? "N/A";
                    txtPosition.Text = "Position: " + employee.PositionName;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin nhân viên.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            HomeScreen.EditProfile editProfile = new HomeScreen.EditProfile();
            editProfile.OnProfileUpdated += loadData;
            editProfile.ShowDialog();
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TaskEmployee taskEmployee = new TaskEmployee();
            taskEmployee.Show();
            this.Close();
        }
    }
}
