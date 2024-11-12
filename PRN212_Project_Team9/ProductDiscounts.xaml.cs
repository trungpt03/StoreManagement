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
    /// Interaction logic for ProductDiscounts.xaml
    /// </summary>
    public partial class ProductDiscounts : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();

        public ProductDiscounts()
        {
            InitializeComponent();
            LoadData();
            LoadComboBoxes();
        }

        public void LoadData()
        {
            var query = from pd in context.ProductDiscounts
                        join p in context.Products on pd.ProductId equals p.ProductId
                        join d in context.Discounts on pd.DiscountId equals d.DiscountId
                        select new
                        {
                            ProductDiscountId = pd.ProductDiscountId,
                            ProductId = pd.ProductId,
                            ProductName = p.ProductName,
                            DiscountId = pd.DiscountId,
                            DiscountName = d.DiscountName
                        };

            lvProductDiscount.ItemsSource = query.ToList();
        }

        private void LoadComboBoxes()
        {
            cbProduct.ItemsSource = context.Products.Select(p => new { p.ProductId, p.ProductName }).ToList();
            cbProduct.DisplayMemberPath = "ProductName";
            cbProduct.SelectedValuePath = "ProductId";

            cbDiscount.ItemsSource = context.Discounts.Select(d => new { d.DiscountId, d.DiscountName }).ToList();
            cbDiscount.DisplayMemberPath = "DiscountName";
            cbDiscount.SelectedValuePath = "DiscountId";
        }

        private void lvProductDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = lvProductDiscount.SelectedItem as dynamic;
            if (item != null)
            {
                tbProductDiscountId.Text = item.ProductDiscountId.ToString();

                var productDiscount = context.ProductDiscounts.Find(item.ProductDiscountId);
                if (productDiscount != null)
                {
                    cbProduct.SelectedValue = productDiscount.ProductId;
                    cbDiscount.SelectedValue = productDiscount.DiscountId;
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            tbProductDiscountId.Text = "";
            cbProduct.SelectedIndex = -1;
            cbDiscount.SelectedIndex = -1;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbProduct.SelectedItem == null || cbDiscount.SelectedItem == null)
                {
                    return;
                }

                var productDiscount = new Models.ProductDiscount
                {
                    ProductId = (int)cbProduct.SelectedValue,
                    DiscountId = (int)cbDiscount.SelectedValue
                };

                context.ProductDiscounts.Add(productDiscount);
                context.SaveChanges();
                LoadData();
                Refresh_Click(null, null);
            }
            catch (Exception) { }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbProductDiscountId.Text))
                {
                    return;
                }

                int id = int.Parse(tbProductDiscountId.Text);
                var productDiscount = context.ProductDiscounts.Find(id);

                if (productDiscount != null)
                {
                    productDiscount.ProductId = (int)cbProduct.SelectedValue;
                    productDiscount.DiscountId = (int)cbDiscount.SelectedValue;

                    context.SaveChanges();
                    LoadData();
                    Refresh_Click(null, null);
                }
            }
            catch (Exception) { }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbProductDiscountId.Text))
                {
                    return;
                }

                int id = int.Parse(tbProductDiscountId.Text);
                var productDiscount = context.ProductDiscounts.Find(id);

                if (productDiscount != null)
                {
                    context.ProductDiscounts.Remove(productDiscount);
                    context.SaveChanges();
                    LoadData();
                    Refresh_Click(null, null);
                }
            }
            catch (Exception) { }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSearchBy.SelectedItem == null)
                {
                    return;
                }

                string searchText = txtSearch.Text.Trim().ToLower();
                var query = from pd in context.ProductDiscounts
                            join p in context.Products on pd.ProductId equals p.ProductId
                            join d in context.Discounts on pd.DiscountId equals d.DiscountId
                            select new
                            {
                                ProductDiscountId = pd.ProductDiscountId,
                                ProductId = pd.ProductId,
                                ProductName = p.ProductName,
                                DiscountId = pd.DiscountId,
                                DiscountName = d.DiscountName
                            };

                switch (((ComboBoxItem)cbSearchBy.SelectedItem).Content.ToString())
                {
                    case "ProductDiscountID":
                        if (int.TryParse(searchText, out int id))
                            query = query.Where(x => x.ProductDiscountId == id);
                        break;
                    case "ProductID":
                        if (int.TryParse(searchText, out int productId))
                            query = query.Where(x => x.ProductId == productId);
                        break;
                    case "DiscountID":
                        if (int.TryParse(searchText, out int discountId))
                            query = query.Where(x => x.DiscountId == discountId);
                        break;
                }

                lvProductDiscount.ItemsSource = query.ToList();
            }
            catch (Exception) { }
        }
    }
}
