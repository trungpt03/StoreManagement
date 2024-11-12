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

namespace PRN212_Project_Team9.HomeScreen
{
    /// <summary>
    /// Interaction logic for EditProfile.xaml
    /// </summary>
    public partial class EditProfile : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();
        public event Action OnProfileUpdated;
        public EditProfile()
        {
            InitializeComponent();
            loadData();
        }
        public void loadData()
        {
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == AppMemory.Id);

            if (employee != null)
            {
                txtEmployeeName.Text = employee.EmployeeName;
                txtPhoneNumber.Text = employee.PhoneNumber;
                txtAccount.Text = employee.Account;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên.");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == AppMemory.Id);

            // Kiểm tra xem nhân viên có tồn tại hay không
            if (employee != null)
            {
                // Kiểm tra mật khẩu cũ (sử dụng .Password để lấy giá trị mật khẩu từ PasswordBox)
                if (!string.IsNullOrEmpty(txtOldPassword.Password) && txtOldPassword.Password != employee.Password)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng.");
                    return; // Dừng cập nhật nếu mật khẩu cũ không đúng
                }

                // Kiểm tra xem người dùng có thay đổi mật khẩu không
                if (!string.IsNullOrEmpty(txtNewPassword.Password) || !string.IsNullOrEmpty(txtConfirmNewPassword.Password))
                {
                    // Kiểm tra mật khẩu mới và xác nhận mật khẩu mới có khớp nhau không
                    if (txtNewPassword.Password != txtConfirmNewPassword.Password)
                    {
                        MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu mới không khớp.");
                        return; // Dừng cập nhật nếu mật khẩu mới và xác nhận không khớp
                    }

                    // Cập nhật mật khẩu mới nếu tất cả điều kiện trên đúng
                    employee.Password = txtNewPassword.Password;
                }
                else
                {
                    // Nếu không thay đổi mật khẩu, bỏ qua phần cập nhật mật khẩu
                }

                // Cập nhật thông tin khác như tên, số điện thoại, tài khoản
                employee.EmployeeName = txtEmployeeName.Text;
                employee.PhoneNumber = txtPhoneNumber.Text;
                employee.Account = txtAccount.Text;

                try
                {
                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();
                    MessageBox.Show("Thông tin đã được cập nhật.");
                    OnProfileUpdated?.Invoke();
                    this.Close(); // Đóng cửa sổ sau khi lưu thành công
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi lưu thông tin: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên.");
            }
            OnProfileUpdated?.Invoke();
        }
    }
}
