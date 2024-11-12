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
    /// Interaction logic for LoyaltyPrograms.xaml
    /// </summary>
    public partial class LoyaltyPrograms : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();
        public LoyaltyPrograms()
        {
            InitializeComponent();
            loadData();
        }
        public void loadData()
        {
            var listProgram = context.LoyaltyPrograms.Select(l => new
            {
                ProgramId = l.ProgramId,
                ProgramName = l.ProgramName,
                PointMultiplier = l.PointMultiplier,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
            }).ToList();
            lvProgram.ItemsSource = listProgram;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            TaskEmployee taskEmployee = new TaskEmployee();
            taskEmployee.Show();
            this.Close();
        }

        private void btnProgram_Click(object sender, RoutedEventArgs e)
        {
            LoyaltyPrograms programs = new LoyaltyPrograms();
            programs.Show();
            this.Close();
        }

        private void btnCustomerPoint_Click(object sender, RoutedEventArgs e)
        {
            CustomerPoint point = new CustomerPoint();
            point.Show();
            this.Close();
        }

        private void lvProgram_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectItem = lvProgram.SelectedItem as dynamic;
            if (SelectItem != null)
            {
                txtProgramId.Text = SelectItem.ProgramId.ToString();
                txtProgramName.Text = SelectItem.ProgramName.ToString();
                txtPointMultiplier.Text = SelectItem.PointMultiplier.ToString();
                dpStartDate.Text = SelectItem.StartDate.ToString();
                dpEndDate.Text = SelectItem.EndDate.ToString();
            }
        }

        private void btnSearchProgram_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearchProgram.Text.Trim().ToLower();
            var results = context.LoyaltyPrograms.Where(c =>
                c.ProgramId.ToString().Contains(keyword) ||
                c.ProgramName.ToString().Contains(keyword) ||
                c.PointMultiplier.ToString().Contains(keyword) ||
                (c.StartDate != null && c.StartDate.ToString().Contains(keyword)) ||
                (c.EndDate != null && c.EndDate.ToString().Contains(keyword))
            ).Select(c => new
            {
                ProgramId = c.ProgramId,
                ProgramName = c.ProgramName,
                PointMultiplier = c.PointMultiplier,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
            }).ToList();
            lvProgram.ItemsSource = results;
        }

        private void btnAddProgram_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string programName = txtProgramName.Text.Trim();
                decimal pointMultiplier = decimal.Parse(txtPointMultiplier.Text);
                DateTime? startDate = dpStartDate.SelectedDate;
                DateTime? endDate = dpEndDate.SelectedDate;

                // Thêm điểm khách hàng
                Models.LoyaltyProgram c = new Models.LoyaltyProgram
                {
                    ProgramName = programName,
                    PointMultiplier = pointMultiplier,
                    StartDate = startDate,
                    EndDate = endDate,
                };

                context.LoyaltyPrograms.Add(c);
                context.SaveChanges();

                MessageBox.Show("Thêm chương trình thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

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

        private void btnUpdateProgram_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtProgramId.Text, out int programId))
                {
                    var program = context.LoyaltyPrograms.FirstOrDefault(c => c.ProgramId == programId);
                    if (program != null)
                    {
                        program.ProgramName = txtProgramName.Text;
                        program.PointMultiplier = decimal.Parse(txtPointMultiplier.Text);
                        program.StartDate = dpStartDate.SelectedDate;
                        program.EndDate = dpEndDate.SelectedDate;

                        context.SaveChanges();

                        MessageBox.Show("Cập nhật chương trình thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        loadData();
                    }

                }
                else
                {
                    MessageBox.Show("Vui lòng chọn chương trình từ danh sách để cập nhật.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật chương trình: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteProgram_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int programId = int.Parse(txtProgramId.Text);
                var program = context.LoyaltyPrograms.Where(p => p.ProgramId == programId).ToList();
                context.LoyaltyPrograms.RemoveRange(program);
                context.SaveChanges();

                MessageBox.Show("Xóa chương trình thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                loadData();
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            this.Close();
        }
    }
}
