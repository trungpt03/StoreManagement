using Microsoft.EntityFrameworkCore;
using PRN212_Project_Team9.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Xceed.Wpf.Toolkit;

namespace PRN212_Project_Team9
{
    /// <summary>
    /// Interaction logic for ProductSelect.xaml
    /// </summary>
    public partial class ProductSelect : Window
    {
        public event Action<string> ProductSelected;

        SalesManagementDbContext _con = new SalesManagementDbContext();
        public ProductSelect()
        {
            InitializeComponent();
            LoadData();
        }

 
        private void LoadData()
        {
            List<string> stringType = new List<string>()
            {    "Id" , "ProductName" , "CategoryName" , "Price" , "Price" , "StockQuantity"  };
            TypeSearch.ItemsSource = stringType.ToList();
            DataProductListToOrder.ItemsSource = _con.Products.Include(x => x.Category).Select(x => new
            {
                x.ProductId,
                x.ProductName,
                x.Category.CategoryName,
                x.Price,
                x.StockQuantity,
            }).ToList();

        }

        public void LoadDiscountOfProduct(int idProduct)
        {
            DateTime dateTime = DateTime.Now;

            DiscountsOfProduct.ItemsSource = _con.ProductDiscounts.Include(x => x.Product).Include(x => x.Discount)
            .Where(x => x.ProductId == idProduct && x.Discount.StartDate <= dateTime && dateTime <= x.Discount.EndDate).Select(x => new {
                x.ProductId,
                x.Product.ProductName,
                x.DiscountId,
                x.Discount.DiscountPercentage,
            })
            .ToList();
        }

        private void DataProductListToOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic productSelect = DataProductListToOrder.SelectedItem;

            tbxIdProduct.Text = productSelect.ProductId + "";
            tbxNameProduct.Text = productSelect.ProductName;
            QuantityProduct.Maximum = productSelect.StockQuantity;
            QuantityProduct.Minimum = 0;


            DateTime dateTime = DateTime.Now;
            decimal totalPercentage = (decimal)(_con.ProductDiscounts.Include(x => x.Product).Include(x => x.Discount)
            .Where(x => x.ProductId == Int32.Parse(tbxIdProduct.Text) && x.Discount.StartDate <= dateTime && dateTime <= x.Discount.EndDate).Select(x => new {
                x.Discount.DiscountPercentage,
            })
            .ToList().Sum(x => x.DiscountPercentage) ?? 0);

            decimal totalPriceFinal = _con.Products.Find(int.Parse(tbxIdProduct.Text)).Price
                          * (totalPercentage == 0 ? 1 : (1 - (totalPercentage / 100m)))
                          * (QuantityProduct.Value ?? 0);

            tbxTotalPrice.Text = totalPriceFinal.ToString();

            LoadDiscountOfProduct(Int32.Parse(tbxIdProduct.Text));
        }

        private void QuantityProduct_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int newQuantityValue = (int)(e.NewValue ?? 0);

            if (tbxIdProduct.Text.Length > 0)
            {
                DateTime dateTime = DateTime.Now;
                decimal totalPercentage = (decimal)(_con.ProductDiscounts.Include(x => x.Product).Include(x => x.Discount)
                .Where(x => x.ProductId == Int32.Parse(tbxIdProduct.Text) && x.Discount.StartDate <= dateTime && dateTime <= x.Discount.EndDate).Select(x => new {
                    x.Discount.DiscountPercentage,
                })
                .ToList().Sum(x => x.DiscountPercentage) ?? 0);

                decimal totalPriceFinal = _con.Products.Find(int.Parse(tbxIdProduct.Text)).Price
                          * (totalPercentage == 0 ? 1 : (1 - (totalPercentage / 100m)))
                          * (QuantityProduct.Value ?? 0);

                tbxTotalPrice.Text = totalPriceFinal.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TypeSearch.Text.Equals("Id"))
                {
                    int getId;
                    if (int.TryParse(InputSearch.Text, out getId))
                    {
                        DataProductListToOrder.ItemsSource = _con.Products.Include(x => x.Category).Where(x => x.ProductId == getId).Select(x => new
                        {
                            x.ProductId,
                            x.ProductName,
                            x.Category.CategoryName,
                            x.Price,
                            x.StockQuantity,
                        }).ToList();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Hãy nhập số để tìm kiếm");
                    }

                }
                else if (TypeSearch.Text.Equals("ProductName"))
                {
                    DataProductListToOrder.ItemsSource = _con.Products.Include(x => x.Category).Where(x => x.ProductName.ToLower().Contains(InputSearch.Text.ToLower())).Select(x => new
                    {
                        x.ProductId,
                        x.ProductName,
                        x.Category.CategoryName,
                        x.Price,
                        x.StockQuantity,
                    }).ToList();
                }
                else if (TypeSearch.Text.Equals("CategoryName"))
                {
                    DataProductListToOrder.ItemsSource = _con.Products.Include(x => x.Category).Where(x => x.Category.CategoryName.Equals(InputSearch.Text)).Select(x => new
                    {
                        x.ProductId,
                        x.ProductName,
                        x.Category.CategoryName,
                        x.Price,
                        x.StockQuantity,
                    }).ToList();
                }
                else if (TypeSearch.Text.Equals("Price"))
                {
                    DataProductListToOrder.ItemsSource = _con.Products.Include(x => x.Category).Where(x => (decimal.Parse(InputSearch.Text)-1) <= x.Price && (decimal.Parse(InputSearch.Text) + 1) >= x.Price).Select(x => new
                    {
                        x.ProductId,
                        x.ProductName,
                        x.Category.CategoryName,
                        x.Price,
                        x.StockQuantity,
                    }).ToList();
                }
                else if (TypeSearch.Text.Equals("StockQuantity"))
                {
                    DataProductListToOrder.ItemsSource = _con.Products.Include(x => x.Category).Where(x => x.StockQuantity ==  Int32.Parse(InputSearch.Text)).Select(x => new
                    {
                        x.ProductId,
                        x.ProductName,
                        x.Category.CategoryName,
                        x.Price,
                        x.StockQuantity,
                    }).ToList();
                }
                else
                {
                    System.Windows.MessageBox.Show("Chưa chọn kiểu tìm kiếm");
                }

            } catch 
            {
                System.Windows.MessageBox.Show("Nhập đầu vào không đúng");
            };

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            OrderForCustomer orderForCustomer = new OrderForCustomer();
            AppMemory.IdProduct = Int32.Parse(tbxIdProduct.Text);
            AppMemory.NameProduct = tbxNameProduct.Text;
            AppMemory.TotalPrice = decimal.Parse(tbxTotalPrice.Text);
            AppMemory.QuantityProduct = (QuantityProduct.Value ?? 0);

            if (AppMemory.QuantityProduct <= 0)
            {
                System.Windows.MessageBox.Show("Ko Thể không lấy sản phẩm");
            }else if (AppMemory.QuantityProduct > 0)
            {
                string selectedProductData = "Dữ liệu sản phẩm đã chọn";
                ProductSelected?.Invoke(selectedProductData);
                this.Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            LoadData();
        }
    }
}
