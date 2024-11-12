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
    /// Interaction logic for CustomerPoint.xaml
    /// </summary>
    public partial class CustomerPoint : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();
        public CustomerPoint()
        {
            InitializeComponent();
            loadData();
        }
        public void loadData()
        {
            var listCusPoint = context.CustomerPoints.Select(c => new
            {
                CustomerId = c.CustomerId,
                ProgramId = c.ProgramId,
                Points = c.Points,
                LastUpdate = c.LastUpdate,
            }).ToList();
            lvCustomerPoint.ItemsSource = listCusPoint;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            TaskEmployee taskEmployee = new TaskEmployee();
            taskEmployee.Show();
            this.Close();
        }

        private void lvCustomerPoint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectItem = lvCustomerPoint.SelectedItem as dynamic;
            if (SelectItem != null)
            {
                txtCustomerId.Text = SelectItem.CustomerId.ToString();
                txtProgramId.Text = SelectItem.ProgramId.ToString();
                txtPoints.Text = SelectItem.Points.ToString();
                dpLastUpdate.Text = SelectItem.LastUpdate.ToString();
            }
        }

        private void btnSearchCustomerPoint_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearchCustomerPoint.Text.Trim();
            var results = context.CustomerPoints.Where(c =>
                c.CustomerId.ToString().Contains(keyword) ||
                c.ProgramId.ToString().Contains(keyword) ||
                c.Points.ToString().Contains(keyword) ||
                (c.LastUpdate != null && c.LastUpdate.ToString().Contains(keyword))
            ).Select(c => new
            {
                CustomerId = c.CustomerId,
                ProgramId = c.ProgramId,
                Points = c.Points,
                LastUpdate = c.LastUpdate,
            }).ToList();
            lvCustomerPoint.ItemsSource = results;
        }

        private void btnAddCustomerPoint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int customerId = int.Parse(txtCustomerId.Text);
                int programId = int.Parse(txtProgramId.Text);
                int points = int.Parse(txtPoints.Text);
                DateTime? lastUpdate = dpLastUpdate.SelectedDate;

                // Kiểm tra xem khách hàng có tồn tại không
                var customerExists = context.Customers.Any(c => c.CustomerId == customerId);
                if (!customerExists)
                {
                    MessageBox.Show($"ID khách hàng {customerId} không tồn tại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Kiểm tra xem chương trình có tồn tại không
                var programExists = context.LoyaltyPrograms.Any(p => p.ProgramId == programId);
                if (!programExists)
                {
                    MessageBox.Show($"ID chương trình {programId} không tồn tại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Thêm điểm khách hàng
                Models.CustomerPoint c = new Models.CustomerPoint
                {
                    CustomerId = customerId,
                    ProgramId = programId,
                    Points = points,
                    LastUpdate = lastUpdate
                };

                context.CustomerPoints.Add(c);
                context.SaveChanges();

                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                loadData();
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng cho các trường dữ liệu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnUpdateCustomerPoint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra nếu ô CustomerId có giá trị, nghĩa là đã chọn khách hàng cần cập nhật
                if (int.TryParse(txtCustomerId.Text, out int customerId))
                {
                    // Tìm khách hàng trong cơ sở dữ liệu dựa vào CustomerId
                    var customer = context.CustomerPoints.FirstOrDefault(c => c.CustomerId == customerId);
                    if (customer != null)
                    {
                        // Cập nhật thông tin khách hàng
                        customer.ProgramId = int.Parse(txtProgramId.Text);
                        customer.Points = int.Parse(txtPoints.Text);
                        customer.LastUpdate = DateTime.Now;

                        context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                        MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        loadData(); // Tải lại dữ liệu để cập nhật danh sách hiển thị
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

        private void btnDeleteCustomerPonit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy CustomerId từ ô nhập liệu
                int customerId = int.Parse(txtCustomerId.Text);

                // Tìm tất cả điểm khách hàng theo CustomerId
                var customerPoints = context.CustomerPoints.Where(cp => cp.CustomerId == customerId).ToList();
                if (!customerPoints.Any())
                {
                    MessageBox.Show($"Không tìm thấy điểm khách hàng cho ID khách hàng {customerId}.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Xóa tất cả điểm khách hàng liên quan đến CustomerId
                context.CustomerPoints.RemoveRange(customerPoints);
                context.SaveChanges();

                MessageBox.Show("Xóa điểm khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                loadData();
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng cho ID khách hàng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
            CustomerPoint point = new CustomerPoint();
            point.Show();
            this.Close();
        }

        private void btnProgram_Click(object sender, RoutedEventArgs e)
        {
            LoyaltyPrograms programs = new LoyaltyPrograms();
            programs.Show();
            this.Close();
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            this.Close();
        }
    }
}
