using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for ProductManage.xaml
    /// </summary>
    public partial class ProductManage : Window
    {
        SalesManagementDbContext db = new SalesManagementDbContext();

        public ProductManage()
        {
            InitializeComponent();
            LoadLvProduct();
            LoadCbCategory();
            LoadLvCategory();
        }

        //// Phương thức chung để lấy product theo ID
        //private async Task<Product> GetProductById(int productId) =>
        //    await db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

        //// Phương thức chung để lấy inventory theo ProductId
        //private async Task<Inventory> GetInventoryByProductId(int productId) =>
        //    await db.Inventories.FirstOrDefaultAsync(x => x.ProductId == productId);

        //// Phương thức chung để lấy category theo tên
        //private async Task<Category> GetCategoryByName(string categoryName) =>
        //    await db.Categories.FirstOrDefaultAsync(x => x.CategoryName == categoryName);

        // Phương thức kiểm tra dữ liệu đầu vào
        private bool ValidateProductInput()
        {
            if (string.IsNullOrEmpty(txtProductName.Text) || string.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBox.Show("Please Enter Product Name and Quantity!");
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out _))
            {
                MessageBox.Show("Invalid Price!");
                return false;
            }
            return true;
        }

        // Load sản phẩm
        private void LoadLvProduct()
        {
            var listProduct = db.Products
                .Select(x => new
                {
                    ProductID = x.ProductId,
                    ProductName = x.ProductName,
                    Category = x.Category.CategoryName,
                    Price = x.Price,
                    Quantity = db.Inventories.Where(c => c.ProductId == x.ProductId).Select(c => c.Quantity).FirstOrDefault() ?? 0,
                    Description = x.Description,
                }).ToList();

            lvProduct.ItemsSource = listProduct;
            ResetProductForm();
        }

        // Load danh mục sản phẩm
        private void LoadLvCategory()
        {
            var listCategory = db.Categories
                .Select(x => new
                {
                    CategoryID = x.CategoryId,
                    CategoryName = x.CategoryName,
                })
                .ToList();

            lvCategory.ItemsSource = listCategory;
            ResetCategoryForm();
        }

        // Load combobox danh mục
        private void LoadCbCategory()
        {
            var listCategory = db.Categories
                .Select(x => x.CategoryName)
                .ToList();
            cbCategory.ItemsSource = listCategory;
            cbCategory.SelectedIndex = 0;
        }

        // Reset form product
        private void ResetProductForm()
        {
            lvProduct.SelectedItem = null;
            txtProductID.Clear();
            txtProductName.Clear();
            cbCategory.SelectedIndex = 0;
            txtPrice.Clear();
            txtQuantity.Clear();
            txtDescription.Clear();
            txtImport.Clear();
        }

        // Reset form category
        private void ResetCategoryForm()
        {
            lvCategory.SelectedItem = null;
            txtCategoryId.Clear();
            txtCategoryName.Clear();
        }

        // Thêm sản phẩm
        private void AddProduct()
        {
            if (!ValidateProductInput()) return;

            var category = db.Categories.FirstOrDefault(x => x.CategoryName == cbCategory.Text);
            if (category == null)
            {
                MessageBox.Show("Category not found.");
                return;
            }

            var product = new Product()
            {
                ProductName = txtProductName.Text,
                Category = category,
                Price = decimal.Parse(txtPrice.Text),
                Description = txtDescription.Text,
            };

            try
            {
                db.Products.Add(product);
                db.SaveChanges();

                var inventory = new Inventory()
                {
                    ProductId = product.ProductId,
                    Quantity = int.Parse(txtQuantity.Text),
                };
                db.Inventories.Add(inventory);
                db.SaveChanges();

                MessageBox.Show("Add Product Success!");
                LoadLvProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}");
            }
        }

        // Cập nhật sản phẩm
        private void UpdateProduct()
        {
            if (string.IsNullOrEmpty(txtProductID.Text))
            {
                MessageBox.Show("Not Found Product to Update!");
                return;
            }

            var productId = int.Parse(txtProductID.Text);
            var product = db.Products.FirstOrDefault(x => x.ProductId == productId);
            if (product == null)
            {
                MessageBox.Show("Product not found.");
                return;
            }

            if (!ValidateProductInput()) return;

            var category = db.Categories.FirstOrDefault(x => x.CategoryName == cbCategory.Text);
            if (category == null)
            {
                MessageBox.Show("Category not found.");
                return;
            }

            product.ProductName = txtProductName.Text;
            product.Category = category;
            product.Price = decimal.Parse(txtPrice.Text);
            product.Description = txtDescription.Text;

            var inventory = db.Inventories.FirstOrDefault(x => x.ProductId == productId);
            if (inventory != null)
            {
                inventory.Quantity = int.Parse(txtQuantity.Text);
                db.Update(inventory); // Chỉ gọi Update nếu inventory không null
            }

            try
            {
                db.Update(product);
                db.SaveChanges();
                MessageBox.Show("Update Product Success!");
                LoadLvProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}");
            }
        }


        // Xóa sản phẩm
        private void DeleteProduct()
        {
            if (string.IsNullOrEmpty(txtProductID.Text))
            {
                MessageBox.Show("Not Found Product to Delete!");
                return;
            }

            var productId = int.Parse(txtProductID.Text);
            var product = db.Products.FirstOrDefault(x => x.ProductId == productId);
            var inventory = db.Inventories.FirstOrDefault(x => x.ProductId == productId);

            if (product == null || inventory == null)
            {
                MessageBox.Show("Product or Inventory not found.");
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    db.Remove(inventory);
                    db.Remove(product);
                    db.SaveChanges();
                    MessageBox.Show("Delete Success!");
                    LoadLvProduct();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting product: {ex.Message}");
                }
            }
        }

        // Nhập hàng (Import)
        private void ImportProduct(int quantity)
        {
            var productId = int.Parse(txtProductID.Text);
            var product = db.Products.FirstOrDefault(x => x.ProductId == productId);
            var inventory = db.Inventories.FirstOrDefault(x => x.ProductId == productId);

            // Kiểm tra nếu product hoặc inventory không tồn tại
            if (product == null)
            {
                MessageBox.Show("Product not found.");
                return;
            }

            if (inventory == null)
            {
                MessageBox.Show("Inventory not found.");
                return;
            }

            // Cập nhật thông tin inventory nếu tìm thấy
            inventory.LastUpdate = DateTime.Now;
            inventory.Quantity += quantity;

            try
            {
                db.Update(inventory);
                db.SaveChanges();
                MessageBox.Show($"Import {quantity} {product.ProductName} Success!");
                LoadLvProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Import Product Quantity {ex.Message}");
            }
        }


        private void ExportProduct(int quantity)
        {
            var productId = int.Parse(txtProductID.Text);
            var product = db.Products.FirstOrDefault(x => x.ProductId == productId);
            var inventory = db.Inventories.FirstOrDefault(x => x.ProductId == productId);

            // Kiểm tra nếu product hoặc inventory không tìm thấy
            if (product == null)
            {
                MessageBox.Show("Product not found.");
                return;
            }

            if (inventory == null)
            {
                MessageBox.Show("Inventory not found.");
                return;
            }

            inventory.LastUpdate = DateTime.Now;
            inventory.Quantity -= quantity;

            try
            {
                db.Update(inventory);
                db.SaveChanges();
                MessageBox.Show($"Export {quantity} {product.ProductName} Success!");
                LoadLvProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Export Product Quantity {ex.Message}");
            }
        }


        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtImport.Text, out var importValue))
            {
                ImportProduct(importValue);
            }
            else
            {
                MessageBox.Show("Invalid Import Value!");
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtImport.Text, out var exportValue))
            {
                ExportProduct(exportValue);
            }
            else
            {
                MessageBox.Show("Invalid Export Value!");
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadLvProduct();
            ResetProductForm();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddProduct();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteProduct();
        }

        private void btnRefreshCategory_Click(object sender, RoutedEventArgs e)
        {
            ResetCategoryForm();
        }

        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtCategoryName.Text))
            {
                MessageBox.Show("Category Name is Null");
                return;
            }

            Category category = new Category()
            {
                CategoryName = txtCategoryName.Text,
            };
            db.Categories.Add(category);
            db.SaveChanges();
            MessageBox.Show($"Add {txtCategoryName.Text} Success!");
            LoadLvCategory();
            LoadCbCategory();
        }

        private void btnUpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryId.Text))
            {
                MessageBox.Show("Not found Category!");
                return;
            }

            // Tìm kiếm Category và kiểm tra nếu tồn tại
            var selectedCate = db.Categories.FirstOrDefault(x => x.CategoryId == int.Parse(txtCategoryId.Text));
            if (selectedCate == null)
            {
                MessageBox.Show("Category not found.");
                return;
            }

            // Cập nhật tên Category nếu tìm thấy
            selectedCate.CategoryName = txtCategoryName.Text;
            db.Update(selectedCate);
            db.SaveChanges();
            MessageBox.Show("Update Category Success!");
            LoadLvCategory();
            LoadCbCategory();
            LoadLvProduct();
        }


        private void btnDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryId.Text))
            {
                MessageBox.Show("Not found Category!");
                return;
            }

            // Lấy đối tượng Category và kiểm tra nếu tồn tại
            var selectedCate = db.Categories.FirstOrDefault(x => x.CategoryId == int.Parse(txtCategoryId.Text));
            if (selectedCate == null)
            {
                MessageBox.Show("Category not found.");
                return;
            }

            // Kiểm tra nếu danh mục có sản phẩm liên kết
            bool hasProducts = db.Products.Any(x => x.CategoryId == selectedCate.CategoryId);
            if (hasProducts)
            {
                MessageBox.Show("Cannot delete category because it has associated products.");
                return;
            }

            db.Remove(selectedCate);
            db.SaveChanges();
            MessageBox.Show("Delete Category Success!");
            LoadLvCategory();
            LoadCbCategory();
        }



        private void lvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sltCate = lvCategory.SelectedItem as dynamic;
            if (sltCate != null)
            {
                txtCategoryId.Text = sltCate.CategoryID.ToString();
                txtCategoryName.Text = sltCate.CategoryName;
            }
        }

        private void lvProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = lvProduct.SelectedItem as dynamic;
            if (selected != null)
            {
                txtProductID.Text = selected.ProductID.ToString();
                txtProductName.Text = selected.ProductName;
                cbCategory.Text = selected.Category;
                txtPrice.Text = selected.Price.ToString();
                txtQuantity.Text = selected.Quantity.ToString();
                txtDescription.Text = selected.Description;
            }

        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var lst = db.Products
                .Where(x => x.ProductName.Contains(txtSearch.Text))
                .Select(x => new
                {
                    ProductID = x.ProductId,
                    ProductName = x.ProductName,
                    Category = x.Category.CategoryName,
                    Price = x.Price,
                    Quantity = db.Inventories.Where(c => c.ProductId == x.ProductId).Select(c => c.Quantity).FirstOrDefault() ?? 0,
                    Description = x.Description,
                }).ToList();
            lvProduct.ItemsSource = lst;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow logout = new MainWindow();
            logout.Show();
            this.Close();
        }

        private void btnBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
