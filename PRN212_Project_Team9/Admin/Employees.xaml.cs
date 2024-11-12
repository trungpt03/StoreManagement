
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
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();
        public Employees()
        {
            InitializeComponent();
            LoadComboBox();
            LoadData();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new();
            admin.Show();
            this.Close();
        }
        public void LoadData()
        {
            var listEmployee = context.Employees.Select(c => new
            {
                EmployeeId = c.EmployeeId,
                EmployeeName = c.EmployeeName,
                Account = c.Account,
                Password = c.Password,
                PhoneNumber = c.PhoneNumber,
                HireDate = c.HireDate,
                Position = c.Position.PositionName,
            }).ToList();
            lvEmployee.ItemsSource = listEmployee;
        }
        public void LoadComboBox()
        {
            var listPosition = context.Positions.Select(p => p.PositionName).ToList();
            cbPosition.ItemsSource = listPosition;
            cbPosition.SelectedIndex = 0;
        }

        private void lvEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedItem = lvEmployee.SelectedItem as dynamic;
            if (SelectedItem != null)
            {
                txtEmployeeId.Text = SelectedItem.EmployeeId.ToString();
                txtEmPloyeeName.Text = SelectedItem.EmployeeName.ToString();
                txtAccount.Text = SelectedItem.Account.ToString();
                txtPassword.Text = SelectedItem.Password.ToString();
                txtPhoneNumber.Text = SelectedItem.PhoneNumber.ToString();
                dpHireDate.Text = SelectedItem.HireDate.ToString();
                cbPosition.Text = SelectedItem.Position.ToString();
            }
        }

        private void btnSearchEmployee_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearchEmployee.Text.Trim();
            var results = context.Employees.Where(c =>
                c.EmployeeName.Contains(keyword) ||
                c.Account.Contains(keyword) ||
                c.Password.Contains(keyword) ||
                c.PhoneNumber.Contains(keyword) ||
                c.HireDate.ToString().Contains(keyword) ||
                c.Position.PositionName.Contains(keyword)
            ).Select(c => new
            {
                EmployeeId = c.EmployeeId,
                EmployeeName = c.EmployeeName,
                Account = c.Account,
                Password = c.Password,
                PhoneNumber = c.PhoneNumber,
                HireDate = c.HireDate,
                Position = c.Position.PositionName,
            }).ToList();

            lvEmployee.ItemsSource = results;
        }


        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string account = txtAccount.Text.Trim();
                string employeeName = txtEmPloyeeName.Text.Trim();
                string password = txtPassword.Text.Trim();
                string phoneNumber = txtPhoneNumber.Text.Trim();

                // Kiểm tra các trường không được để trống
                if (string.IsNullOrEmpty(employeeName) || string.IsNullOrEmpty(account) ||
                    string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phoneNumber) ||
                    dpHireDate.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Kiểm tra xem tài khoản đã tồn tại hay chưa
                bool accountExists = context.Employees.Any(e => e.Account == account);

                if (accountExists)
                {
                    MessageBox.Show("Tài khoản đã tồn tại. Vui lòng chọn tài khoản khác.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // Dừng thêm nhân viên mới
                }

                // Kiểm tra xem vị trí có tồn tại không
                var position = context.Positions.FirstOrDefault(e => e.PositionName == cbPosition.Text);
                if (position == null)
                {
                    MessageBox.Show("Vị trí không hợp lệ.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Nếu tài khoản không tồn tại, thêm nhân viên mới
                Employee employee = new Employee
                {
                    EmployeeName = employeeName,
                    Account = account,
                    Password = password,
                    PhoneNumber = phoneNumber,
                    HireDate = dpHireDate.SelectedDate.Value, // Chắc chắn rằng giá trị này không null
                    PositionId = position.PositionId
                };

                context.Employees.Add(employee);
                context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                LoadData(); // Tải lại dữ liệu để cập nhật danh sách
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy thông tin từ các trường
                int employeeId = int.Parse(txtEmployeeId.Text.Trim()); // Giả định EmployeeId được nhập vào textbox
                string account = txtAccount.Text.Trim();
                string employeeName = txtEmPloyeeName.Text.Trim();
                string password = txtPassword.Text.Trim();
                string phoneNumber = txtPhoneNumber.Text.Trim();

                // Kiểm tra các trường không được để trống
                if (string.IsNullOrEmpty(employeeName) || string.IsNullOrEmpty(account) ||
                    string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phoneNumber) ||
                    dpHireDate.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Tìm nhân viên theo EmployeeId
                var employeeToUpdate = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                if (employeeToUpdate == null)
                {
                    MessageBox.Show("Không tìm thấy nhân viên để cập nhật.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Cập nhật thông tin
                employeeToUpdate.EmployeeName = employeeName;
                employeeToUpdate.Account = account;
                employeeToUpdate.Password = password;
                employeeToUpdate.PhoneNumber = phoneNumber;
                employeeToUpdate.HireDate = dpHireDate.SelectedDate.Value; // Chắc chắn rằng giá trị này không null
                employeeToUpdate.PositionId = context.Positions.FirstOrDefault(e => e.PositionName == cbPosition.Text)?.PositionId ?? 0; // Gán PositionId

                // Lưu thay đổi vào cơ sở dữ liệu
                context.SaveChanges();
                LoadData(); // Tải lại dữ liệu để cập nhật danh sách
                MessageBox.Show("Cập nhật nhân viên thành công.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật nhân viên: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy EmployeeId từ textbox
                int employeeId = int.Parse(txtEmployeeId.Text.Trim());

                // Tìm nhân viên theo EmployeeId
                var employeeToDelete = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                if (employeeToDelete == null)
                {
                    MessageBox.Show("Không tìm thấy nhân viên để xóa.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Xác nhận xóa
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Employees.Remove(employeeToDelete); // Xóa nhân viên
                    context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                    LoadData(); // Tải lại dữ liệu để cập nhật danh sách
                    MessageBox.Show("Xóa nhân viên thành công.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow logout = new MainWindow();
            logout.Show();
            this.Close();
        }


    }
}
