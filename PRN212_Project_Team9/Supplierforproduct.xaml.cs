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
    public partial class Supplierforproduct : Window
    {
        SalesManagementDbContext context = new SalesManagementDbContext();
        public Supplierforproduct()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            var listSupplier = context.Suppliers.Select(s => new
            {
                SupplierID = s.SupplierId,
                SupplierName = s.SupplierName,
                ContactNumber = s.ContactNumber,
                Address = s.Address,
                Email = s.Email,
            }).ToList();
            lvSupplier.ItemsSource = listSupplier;
        }

        private void lvSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var supplier = lvSupplier.SelectedItem as dynamic;
            if (supplier != null)
            {
                tbSupplierID.Text = supplier.SupplierID.ToString();
                tbSupplierName.Text = supplier.SupplierName.ToString();
                tbSupplierCN.Text = supplier.ContactNumber.ToString();
                tbSupplierAddress.Text = supplier.Address.ToString();
                tbSupplierEmail.Text = supplier.Email.ToString();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            tbSupplierID.Text = "";
            tbSupplierName.Text = "";
            tbSupplierName.Text = "";
            tbSupplierCN.Text = "";
            tbSupplierCN.Text = "";
            tbSupplierAddress.Text = "";
            tbSupplierEmail.Text = "";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbSupplierName.Text) ||
                    string.IsNullOrEmpty(tbSupplierCN.Text) ||
                    string.IsNullOrEmpty(tbSupplierAddress.Text) ||
                    string.IsNullOrEmpty(tbSupplierEmail.Text))
                {
                    return;
                }

                Supplier supplier = new Supplier
                {
                    SupplierName = tbSupplierName.Text,
                    ContactNumber = tbSupplierCN.Text,
                    Address = tbSupplierAddress.Text,
                    Email = tbSupplierEmail.Text
                };

                context.Suppliers.Add(supplier);
                context.SaveChanges();
                LoadData();
                Refresh_Click(null, null);
            }
            catch (Exception) { }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbSupplierID.Text))
                {
                    return;
                }

                int supplierId = int.Parse(tbSupplierID.Text);
                var supplier = context.Suppliers.FirstOrDefault(s => s.SupplierId == supplierId);

                if (supplier != null)
                {
                    context.Suppliers.Remove(supplier);
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
                var searchBy = (cbSearchBy.SelectedItem as ComboBoxItem).Content.ToString();
                var query = context.Suppliers.AsQueryable();

                switch (searchBy)
                {
                    case "SupplierID":
                        if (int.TryParse(searchText, out int id))
                        {
                            query = query.Where(s => s.SupplierId == id);
                        }
                        break;
                    case "SupplierName":
                        query = query.Where(s => s.SupplierName.ToLower().Contains(searchText));
                        break;
                    case "ContactNumber":
                        query = query.Where(s => s.ContactNumber.ToLower().Contains(searchText));
                        break;
                    case "Address":
                        query = query.Where(s => s.Address.ToLower().Contains(searchText));
                        break;
                    case "Email":
                        query = query.Where(s => s.Email.ToLower().Contains(searchText));
                        break;
                }

                var searchResult = query.Select(s => new
                {
                    SupplierID = s.SupplierId,
                    SupplierName = s.SupplierName,
                    ContactNumber = s.ContactNumber,
                    Address = s.Address,
                    Email = s.Email,
                }).ToList();

                lvSupplier.ItemsSource = searchResult;
            }
            catch (Exception) { }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadData();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Supplier supplier = context.Suppliers.FirstOrDefault(s => s.SupplierId == int.Parse(tbSupplierID.Text));
                supplier.SupplierName = tbSupplierName.Text;
                supplier.ContactNumber = tbSupplierCN.Text;
                supplier.Address = tbSupplierAddress.Text;
                supplier.Email = tbSupplierEmail.Text;
                context.SaveChanges();
                LoadData();
            }
            catch (Exception) { }
        }

        private void btnSupplierProduct_Click(object sender, RoutedEventArgs e)
        {
            SupplierProduct supplierProduct = new SupplierProduct();
            supplierProduct.Show();
        }
    }
}