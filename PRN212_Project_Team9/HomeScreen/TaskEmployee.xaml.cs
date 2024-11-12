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
    /// Interaction logic for TaskEmployee.xaml
    /// </summary>
    public partial class TaskEmployee : Window
    {
        public TaskEmployee()
        {
            InitializeComponent();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderForCustomer orderForCustomer = new OrderForCustomer();
            orderForCustomer.Show();
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileEmployee profileEmployee = new ProfileEmployee();
            profileEmployee.Show();
            this.Close();
        }


        private void Supplier_Click(object sender, RoutedEventArgs e)
        {
            Supplierforproduct supplierforproduct = new Supplierforproduct();
            supplierforproduct.Show();
        }


        private void Discount_Click_1(object sender, RoutedEventArgs e)
        {
            Discounts discounts = new Discounts();
            discounts.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ProductManage productManage = new ProductManage();
            productManage.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("HelloSlyly");
        }
    }
}
