using Microsoft.EntityFrameworkCore.Diagnostics;
using PRN212_Project_Team9.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderForCustomer.xaml
    /// </summary>
    public partial class OrderForCustomer : Window
    {
        int? orderId = null;

        SalesManagementDbContext _con = new SalesManagementDbContext();

        public OrderForCustomer()
        {
            InitializeComponent();
            LoadData();
        }


        private void ProductSelect_ProductSelected(string productData)
        {
            tbxIdProduct.Text = AppMemory.IdProduct.ToString();
            tbxNameProduct.Text = AppMemory.NameProduct.ToString();
            tbxTotalPrice.Text = AppMemory.TotalPrice.ToString();
            tbxQuantityProduct.Text = AppMemory.QuantityProduct.ToString();
        }



        private void LoadData()
        {

            List<string> stringType = new List<string>()
            {    "Id" , "Name" , "Phone"    };


            TypeSearch.ItemsSource = stringType.ToList();
            ListCustomer.ItemsSource = _con.Customers.Select(x => new
            {
                x.CustomerId,
                x.CustomerName,
                x.PhoneNumber
            }).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TypeSearch.Text.Equals("Id"))
            {
                int getId;
                if (int.TryParse(InputSearch.Text, out getId))
                {
                    ListCustomer.ItemsSource = _con.Customers.Where(x => x.CustomerId == getId).Select(x => new
                    {
                        x.CustomerId,
                        x.CustomerName,
                        x.PhoneNumber
                    }).ToList();
                }
                else
                {
                    MessageBox.Show("Hãy nhập số để tìm kiếm");
                }

            } 
            else if (TypeSearch.Text.Equals("Name"))
            {
                ListCustomer.ItemsSource = _con.Customers.Where(x => x.CustomerName.ToLower().Contains(InputSearch.Text.ToLower())).Select(x => new
                {
                    x.CustomerId,
                    x.CustomerName,
                    x.PhoneNumber
                }).ToList();
            } 
            else if (TypeSearch.Text.Equals("Phone"))
            {
                ListCustomer.ItemsSource = _con.Customers.Where(x => x.PhoneNumber.Equals(InputSearch.Text)).Select(x => new
                {
                    x.CustomerId,
                    x.CustomerName,
                    x.PhoneNumber
                }).ToList();
            }
            else
            {
                MessageBox.Show("Chưa chọn kiểu tìm kiếm");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ProductSelect productSelect = new ProductSelect();
            productSelect.ProductSelected += ProductSelect_ProductSelected;
            productSelect.ShowDialog();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void OrderSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DateTime? selectedDateTime = OrderSelected.Items[OrderSelected.SelectedIndex] as DateTime?;

            DateTime dateTime = DateTime.Now;

            if (selectedDateTime != null)
            {
                Order? myOrder = _con.Orders.FirstOrDefault(x => x.OrderDate == selectedDateTime && x.CustomerId == Int32.Parse(tbxIdCustomer.Text));

                if (myOrder != null)
                {
                    OrderDetail.ItemsSource = _con.OrderDetails.Where(x => x.OrderId == myOrder.OrderId).ToList();
                }

            }
        }


        private void ListCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic customerSelect = ListCustomer.SelectedItem as dynamic;
            if (customerSelect != null && orderId == null)
            {
                tbxIdCustomer.Text = customerSelect.CustomerId.ToString();
                tbxNameCustomer.Text = customerSelect.CustomerName;
                tbxPhoneCustomer.Text = customerSelect.PhoneNumber;

                List<DateTime?> TimeOrder = new List<DateTime?>() { };
                TimeOrder.AddRange(_con.Orders.Where(x => x.CustomerId == Int32.Parse(tbxIdCustomer.Text)).OrderByDescending(x => x.OrderDate).Select(x => x.OrderDate).ToList());
                LoadDataOfOrder(); 
            }
        }

        private void OrderDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                dynamic st = OrderDetail.SelectedItem;
                tbxIdProduct.Text = st.ProductId.ToString();
                Product? product = _con.Products.Find(st.ProductId);
                tbxNameProduct.Text = product != null ? product.ProductName : "";
                tbxTotalPrice.Text = st.TotalPrice.ToString();
                tbxQuantityProduct.Text = st.Quantity.ToString();
            } catch { MessageBox.Show("Không có giá trị để chọn"); }
            
        }

        private void AddOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? selectedDateTime = OrderSelected.Items[OrderSelected.SelectedIndex] as DateTime?;
                DateTime dateTime = DateTime.Now;

                if (selectedDateTime != null)
                {
                    Order? myOrder = _con.Orders.FirstOrDefault(x => x.OrderDate == selectedDateTime && x.CustomerId == Int32.Parse(tbxIdCustomer.Text));

                    if (myOrder != null)
                    {
                        List<OrderDetail> dataOrderDetailList = _con.OrderDetails.Where(x => x.OrderId == myOrder.OrderId).ToList();

                        if(dataOrderDetailList.Count > 0)
                        {
                            OrderDetail? orderDetail = dataOrderDetailList.FirstOrDefault(x => x.ProductId == Int32.Parse(tbxIdProduct.Text) && x.OrderId == myOrder.OrderId);
                            if (orderDetail != null) 
                            {
                                orderDetail.Quantity = Int32.Parse(tbxQuantityProduct.Text);
                                orderDetail.UnitPrice = _con.Products.Find(orderDetail.ProductId).Price;
                                orderDetail.TotalPrice = Decimal.Parse(tbxTotalPrice.Text);
                                _con.SaveChanges();
                            }
                            else
                            {
                                _con.OrderDetails.Add(new OrderDetail { OrderId = myOrder.OrderId, ProductId = Int32.Parse(tbxIdProduct.Text), Quantity = Int32.Parse(tbxQuantityProduct.Text), UnitPrice = _con.Products.Find(Int32.Parse(tbxIdProduct.Text)).Price, TotalPrice = Decimal.Parse(tbxTotalPrice.Text) });
                                _con.SaveChanges();
                            }
                        }
                        else
                        {
                            _con.OrderDetails.Add(new OrderDetail { OrderId = myOrder.OrderId, ProductId = Int32.Parse(tbxIdProduct.Text), Quantity = Int32.Parse(tbxQuantityProduct.Text), UnitPrice = _con.Products.Find(Int32.Parse(tbxIdProduct.Text)).Price, TotalPrice = Decimal.Parse(tbxTotalPrice.Text) });
                            _con.SaveChanges();
                        }
                        OrderDetail.ItemsSource = _con.OrderDetails.Where(x => x.OrderId == myOrder.OrderId).ToList();
                    }

                }
            } catch { MessageBox.Show("Chưa chọn Order"); }
        }




        private void DeleteOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? selectedDateTime = OrderSelected.Items[OrderSelected.SelectedIndex] as DateTime?;
                DateTime dateTime = DateTime.Now;

                if (selectedDateTime != null)
                {
                    Order? myOrder = _con.Orders.FirstOrDefault(x => x.OrderDate == selectedDateTime && x.CustomerId == Int32.Parse(tbxIdCustomer.Text));

                    if (myOrder != null)
                    {
                        List<OrderDetail> dataOrderDetailList = _con.OrderDetails.Where(x => x.OrderId == myOrder.OrderId).ToList();

                        if (dataOrderDetailList.Count > 0)
                        {
                            OrderDetail? orderDetail = dataOrderDetailList.FirstOrDefault(x => x.ProductId == Int32.Parse(tbxIdProduct.Text) && x.OrderId == myOrder.OrderId);
                            if (orderDetail != null)
                            {
                                _con.OrderDetails.Remove(orderDetail);
                                _con.SaveChanges();
                            }
                            else
                            {
                                MessageBox.Show("Không thể xóa");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa");
                        }
                        OrderDetail.ItemsSource = _con.OrderDetails.Where(x => x.OrderId == myOrder.OrderId).ToList();
                    }

                }
            }
            catch { MessageBox.Show("Chưa chọn Order"); }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? selectedDateTime = OrderSelected.Items[OrderSelected.SelectedIndex] as DateTime?;
                DateTime dateTime = DateTime.Now;

                if (selectedDateTime != null)
                {
                    Order? myOrder = _con.Orders.FirstOrDefault(x => x.OrderDate == selectedDateTime && x.CustomerId == Int32.Parse(tbxIdCustomer.Text));

                    if (myOrder != null)
                    {
                        List<OrderDetail> dataOrderDetailList = _con.OrderDetails.Where(x => x.OrderId == myOrder.OrderId).ToList();

                        if (dataOrderDetailList.Count > 0)
                        {
                            TotalOrderAmount.Text = dataOrderDetailList.Sum(x => x.TotalPrice).ToString();

                            try
                            {
                                myOrder.TotalAmount = Decimal.Parse(TotalOrderAmount.Text);
                            }
                            catch { myOrder.TotalAmount = 0; }
                        }
                        else
                        {
                            TotalOrderAmount.Text = "0";
                        }
                        OrderDetail.ItemsSource = _con.OrderDetails.Where(x => x.OrderId == myOrder.OrderId).ToList();
                    }

                }
            }
            catch { MessageBox.Show("Chưa chọn Order"); }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (tbxIdCustomer.Text.Length != 0)
            {
                Order orderToday = new Order { CustomerId = Int32.Parse(tbxIdCustomer.Text) , EmployeeId = AppMemory.Id , OrderDate = DateTime.Now , TotalAmount = 0 };
                _con.Orders.Add(orderToday);
                _con.SaveChanges();
                orderId = orderToday.OrderId;
                LoadDataOfOrder();
                OrderSelected.SelectedIndex = 0;

            }
        }

        private void LoadDataOfOrder()
        {
            try
            {
                List<DateTime?> TimeOrder = new List<DateTime?>() { };
                TimeOrder.AddRange(_con.Orders.Where(x => x.CustomerId == Int32.Parse(tbxIdCustomer.Text)).OrderByDescending(x => x.OrderDate).Select(x => x.OrderDate).ToList());
                OrderSelected.ItemsSource = TimeOrder.ToList();
            } catch { MessageBox.Show("Chưa chọn khách hàng"); };

        }


        private void DeteleOrderSelect_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDateTime = OrderSelected.Items[OrderSelected.SelectedIndex] as DateTime?;

            if (selectedDateTime != null)
            {
                Order? myOrder = _con.Orders.FirstOrDefault(x => x.OrderDate == selectedDateTime && x.CustomerId == Int32.Parse(tbxIdCustomer.Text));

                if (myOrder != null)
                {
                    _con.RemoveRange(_con.OrderDetails.Where(x => x.OrderId == myOrder.OrderId));
                    _con.Orders.Remove(myOrder);
                    _con.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để xóa");
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn ngày Order");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
