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
    /// Interaction logic for Discounts.xaml
    /// </summary>
    public partial class Discounts : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();

        public Discounts()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            var listDiscount = context.Discounts.Select(d => new
            {
                DiscountId = d.DiscountId,
                DiscountName = d.DiscountName,
                DiscountPercentage = d.DiscountPercentage,
                StartDate = d.StartDate,
                EndDate = d.EndDate
            }).ToList();
            lvDiscount.ItemsSource = listDiscount;
        }

        private void lvDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var discount = lvDiscount.SelectedItem as dynamic;
            if (discount != null)
            {
                tbDiscountId.Text = discount.DiscountId.ToString();
                tbDiscountName.Text = discount.DiscountName;
                tbDiscountPercentage.Text = discount.DiscountPercentage.ToString();
                dpStartDate.SelectedDate = discount.StartDate;
                dpEndDate.SelectedDate = discount.EndDate;
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            tbDiscountId.Text = "";
            tbDiscountName.Text = "";
            tbDiscountPercentage.Text = "";
            dpStartDate.SelectedDate = null;
            dpEndDate.SelectedDate = null;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbDiscountName.Text) ||
                    string.IsNullOrEmpty(tbDiscountPercentage.Text) ||
                    dpStartDate.SelectedDate == null ||
                    dpEndDate.SelectedDate == null)
                {
                    return;
                }

                if (dpStartDate.SelectedDate > dpEndDate.SelectedDate)
                {
                    return;
                }

                Discount discount = new Discount
                {
                    DiscountName = tbDiscountName.Text,
                    DiscountPercentage = decimal.Parse(tbDiscountPercentage.Text),
                    StartDate = dpStartDate.SelectedDate,
                    EndDate = dpEndDate.SelectedDate
                };

                context.Discounts.Add(discount);
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
                if (string.IsNullOrEmpty(tbDiscountId.Text))
                {
                    return;
                }

                if (dpStartDate.SelectedDate > dpEndDate.SelectedDate)
                {
                    return;
                }

                int id = int.Parse(tbDiscountId.Text);
                var discount = context.Discounts.Find(id);

                if (discount != null)
                {
                    discount.DiscountName = tbDiscountName.Text;
                    discount.DiscountPercentage = decimal.Parse(tbDiscountPercentage.Text);
                    discount.StartDate = dpStartDate.SelectedDate;
                    discount.EndDate = dpEndDate.SelectedDate;

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
                if (string.IsNullOrEmpty(tbDiscountId.Text))
                {
                    return;
                }

                int id = int.Parse(tbDiscountId.Text);
                var discount = context.Discounts.Find(id);

                if (discount != null)
                {
                    context.Discounts.Remove(discount);
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
                var query = context.Discounts.AsQueryable();

                switch (((ComboBoxItem)cbSearchBy.SelectedItem).Content.ToString())
                {
                    case "DiscountID":
                        if (int.TryParse(searchText, out int id))
                            query = query.Where(d => d.DiscountId == id);
                        break;
                    case "DiscountName":
                        query = query.Where(d => d.DiscountName.ToLower().Contains(searchText));
                        break;
                    case "Percentage":
                        if (decimal.TryParse(searchText, out decimal percentage))
                            query = query.Where(d => d.DiscountPercentage == percentage);
                        break;
                }

                lvDiscount.ItemsSource = query.Select(d => new
                {
                    DiscountId = d.DiscountId,
                    DiscountName = d.DiscountName,
                    DiscountPercentage = d.DiscountPercentage,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate
                }).ToList();
            }
            catch (Exception) { }
        }

        private void btnProductDiscount_Click(object sender, RoutedEventArgs e)
        {
            ProductDiscounts productDiscounts = new ProductDiscounts();
            productDiscounts.Show();
        }
    }
}
