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
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();
        public Customer()
        {
            InitializeComponent();
            LoadData();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            TaskEmployee taskEmployee = new TaskEmployee();
            taskEmployee.Show();
            this.Close();
        }
        public void LoadData()
        {
            var listCustomer = context.Customers.Select(c => new
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                PhoneNumber = c.PhoneNumber,
                CreatedDate = c.CreatedDate,
            }).ToList();
            lvCustomer.ItemsSource = listCustomer;
        }
        public void Refresh()
        {
            txtCustomerId.Text = "";
            txtCustomerName.Text = "";
            txtPhoneNumber.Text = "";
            dpCreatedDate.Text = "";
        }

        private void lvCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedCustomer = lvCustomer.SelectedItem as dynamic;
            if (SelectedCustomer != null)
            {
                txtCustomerId.Text = SelectedCustomer.CustomerId.ToString();
                txtCustomerName.Text = SelectedCustomer.CustomerName.ToString();
                txtPhoneNumber.Text = SelectedCustomer.PhoneNumber.ToString();
                dpCreatedDate.Text = SelectedCustomer.CreatedDate.ToString();
            }
        }

        private void btnSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearchCustomer.Text.Trim();
            var results = context.Customers.Where(c =>
                c.CustomerName.Contains(keyword) ||
                c.PhoneNumber.Contains(keyword) ||
                c.CreatedDate.ToString().Contains(keyword) // nếu bạn muốn tìm kiếm theo ngày
            ).Select(c => new
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                PhoneNumber = c.PhoneNumber,
                CreatedDate = c.CreatedDate,
            }).ToList();

            lvCustomer.ItemsSource = results;
        }


        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Customer c = new Models.Customer();
                c.CustomerName = txtCustomerName.Text;
                c.PhoneNumber = txtPhoneNumber.Text;
                c.CreatedDate = DateTime.Now;

                context.Customers.Add(c);
                context.SaveChanges();

                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra nếu ô CustomerId có giá trị, nghĩa là đã chọn khách hàng cần cập nhật
                if (int.TryParse(txtCustomerId.Text, out int customerId))
                {
                    // Tìm khách hàng trong cơ sở dữ liệu dựa vào CustomerId
                    var customer = context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                    if (customer != null)
                    {
                        // Cập nhật thông tin khách hàng
                        customer.CustomerName = txtCustomerName.Text;
                        customer.PhoneNumber = txtPhoneNumber.Text;
                        customer.CreatedDate = dpCreatedDate.SelectedDate;

                        context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                        MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        LoadData(); // Tải lại dữ liệu để cập nhật danh sách hiển thị
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng để cập nhật.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn khách hàng từ danh sách để cập nhật.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật khách hàng: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra nếu ô CustomerId có giá trị, nghĩa là đã chọn khách hàng cần xóa
                if (int.TryParse(txtCustomerId.Text, out int customerId))
                {
                    // Tìm khách hàng trong cơ sở dữ liệu dựa vào CustomerId
                    var customer = context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                    if (customer != null)
                    {
                        // Xác nhận xóa với người dùng
                        MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (result == MessageBoxResult.Yes)
                        {
                            context.Customers.Remove(customer); // Xóa khách hàng
                            context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                            MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            Refresh();
                            LoadData(); // Tải lại dữ liệu để cập nhật danh sách hiển thị
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng để xóa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn khách hàng từ danh sách để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow logout = new MainWindow();
            logout.Show();
            this.Close();
        }

        private void btnCustomerPoint_Click(object sender, RoutedEventArgs e)
        {
            CustomerPoint customerPoint = new CustomerPoint();
            customerPoint.Show();
            this.Close();
        }

        private void btnProgram_Click(object sender, RoutedEventArgs e)
        {
            LoyaltyPrograms program = new LoyaltyPrograms();
            program.Show();
            this.Close();
        }
    }
}
