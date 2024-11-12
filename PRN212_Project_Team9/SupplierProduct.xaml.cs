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
    /// Interaction logic for SupplierProduct.xaml
    /// </summary>
    public partial class SupplierProduct : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();

        public SupplierProduct()
        {
            InitializeComponent();
            LoadData();
            LoadComboBoxes();
        }

        public void LoadData()
        {
            var query = from sp in context.SupplierProducts
                        join s in context.Suppliers on sp.SupplierId equals s.SupplierId
                        join p in context.Products on sp.ProductId equals p.ProductId
                        select new
                        {
                            SupplierProductId = sp.SupplierProductId,
                            SupplierId = sp.SupplierId,
                            SupplierName = s.SupplierName,
                            ProductId = sp.ProductId,
                            ProductName = p.ProductName,
                            SupplierPrice = sp.SupplierPrice
                        };

            lvSupplierProduct.ItemsSource = query.ToList();
        }

        private void LoadComboBoxes()
        {
            cbSupplier.ItemsSource = context.Suppliers.Select(s => new { s.SupplierId, s.SupplierName }).ToList();
            cbSupplier.DisplayMemberPath = "SupplierName";
            cbSupplier.SelectedValuePath = "SupplierId";

            cbProduct.ItemsSource = context.Products.Select(p => new { p.ProductId, p.ProductName }).ToList();
            cbProduct.DisplayMemberPath = "ProductName";
            cbProduct.SelectedValuePath = "ProductId";
        }

        private void lvSupplierProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = lvSupplierProduct.SelectedItem as dynamic;
            if (item != null)
            {
                tbSupplierProductId.Text = item.SupplierProductId.ToString();
                tbSupplierPrice.Text = item.SupplierPrice.ToString();

                var supplierProduct = context.SupplierProducts.Find(item.SupplierProductId);
                if (supplierProduct != null)
                {
                    cbSupplier.SelectedValue = supplierProduct.SupplierId;
                    cbProduct.SelectedValue = supplierProduct.ProductId;
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            tbSupplierProductId.Text = "";
            cbSupplier.SelectedIndex = -1;
            cbProduct.SelectedIndex = -1;
            tbSupplierPrice.Text = "";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSupplier.SelectedItem == null || cbProduct.SelectedItem == null ||
                    string.IsNullOrEmpty(tbSupplierPrice.Text))
                {
                    return;
                }

                dynamic supplierItem = cbSupplier.SelectedItem;
                dynamic productItem = cbProduct.SelectedItem;
                int supplierId = supplierItem.SupplierId;
                int productId = productItem.ProductId;

                Models.SupplierProduct supplierProduct = new Models.SupplierProduct
                {
                    SupplierId = supplierId,
                    ProductId = productId,
                    SupplierPrice = decimal.Parse(tbSupplierPrice.Text)
                };

                context.SupplierProducts.Add(supplierProduct);
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
                if (string.IsNullOrEmpty(tbSupplierProductId.Text))
                {
                    return;
                }

                int id = int.Parse(tbSupplierProductId.Text);
                var supplierProduct = context.SupplierProducts.Find(id);

                if (supplierProduct != null)
                {
                    supplierProduct.SupplierId = (int)cbSupplier.SelectedValue;
                    supplierProduct.ProductId = (int)cbProduct.SelectedValue;
                    supplierProduct.SupplierPrice = decimal.Parse(tbSupplierPrice.Text);

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
                if (string.IsNullOrEmpty(tbSupplierProductId.Text))
                {
                    return;
                }

                int id = int.Parse(tbSupplierProductId.Text);
                var supplierProduct = context.SupplierProducts.Find(id);

                if (supplierProduct != null)
                {
                    context.SupplierProducts.Remove(supplierProduct);
                    context.SaveChanges();
                    LoadData();
                    Refresh_Click(null, null);
                }
            }
            catch (Exception) { }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (cbSearchBy.SelectedItem == null)
            {
                return;
            }

            string searchText = txtSearch.Text.Trim().ToLower();
            var query = from sp in context.SupplierProducts
                        join s in context.Suppliers on sp.SupplierId equals s.SupplierId
                        join p in context.Products on sp.ProductId equals p.ProductId
                        select new
                        {
                            SupplierProductId = sp.SupplierProductId,
                            SupplierId = sp.SupplierId,
                            SupplierName = s.SupplierName,
                            ProductId = sp.ProductId,
                            ProductName = p.ProductName,
                            SupplierPrice = sp.SupplierPrice
                        };

            switch (((ComboBoxItem)cbSearchBy.SelectedItem).Content.ToString())
            {
                case "SupplierID":
                    if (int.TryParse(searchText, out int supplierId))
                        query = query.Where(x => x.SupplierId == supplierId);
                    break;
                case "ProductID":
                    if (int.TryParse(searchText, out int productId))
                        query = query.Where(x => x.ProductId == productId);
                    break;
                case "SupplierName":
                    query = query.Where(x => x.SupplierName.ToLower().Contains(searchText));
                    break;
                case "ProductName":
                    query = query.Where(x => x.ProductName.ToLower().Contains(searchText));
                    break;
            }

            lvSupplierProduct.ItemsSource = query.ToList();
        }
    }
}
