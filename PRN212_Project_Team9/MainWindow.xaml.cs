
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PRN212_Project_Team9.Models;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRN212_Project_Team9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SalesManagementDbContext _con = new SalesManagementDbContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var data = _con.Employees.FirstOrDefault(x => x.Account.Equals(acc.Text) && x.Password.Equals(pass.Password));

            if (data != null && data.PositionId == 1)
            {
                MessageBox.Show("Login successful, Admin!");
                Admin adminWindow = new Admin();
                adminWindow.Show();
                this.Close();
            }
            else if (data != null && data.PositionId == 2)
            {
                MessageBox.Show("Login successful, Employee!");
                AppMemory.Id = data.EmployeeId;
                TaskEmployee employeeTask = new TaskEmployee();
                employeeTask.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid email or password");
            }

        }
    }
}