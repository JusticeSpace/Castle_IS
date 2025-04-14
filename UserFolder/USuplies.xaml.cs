using Castle; // Обновляем пространство имён на Castle
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32; // Для OpenFileDialog
using Castle.ForEveryone;

namespace Castle.UserFolder
{
    public partial class USuplies : Page
    {
        private Amo_CastleEntities1 _context;
        private CollectionViewSource _productViewSource;

        public USuplies()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _context = new Amo_CastleEntities1();

            _context.Categories.Load();
            _context.Suppliers.Load();
            _context.Product
                .Include(p => p.Categories)
                .Include(p => p.Suppliers)
                .Include(p => p.Photos) // Добавляем загрузку Photos
                .Load();

            _productViewSource = new CollectionViewSource();
            _productViewSource.Source = _context.Product.Local;
            DGProduct.ItemsSource = _productViewSource.View;

            var categoriesViewSource = (CollectionViewSource)FindResource("CategoriesViewSource");
            categoriesViewSource.Source = _context.Categories.Local;

            var suppliersViewSource = (CollectionViewSource)FindResource("SuppliersViewSource");
            suppliersViewSource.Source = _context.Suppliers.Local;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить товар?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var product = (Product)((Button)sender).DataContext;
                if (_context.Entry(product).State == EntityState.Detached)
                {
                    _context.Product.Attach(product);
                }
                _context.Product.Remove(product);
                _context.SaveChanges();
                RefreshData();
            }
        }

        private void DGProduct_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var product = e.Row.Item as Product;
                if (product?.Price < 0)
                {
                    MessageBox.Show("Цена не может быть отрицательной!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Cancel = true;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFilter();
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter();
        }

        private void UpdateFilter()
        {
            if (_productViewSource?.View == null || Seek == null) return;

            var searchText = Seek.Text?.ToLower() ?? "";
            var selectedCategory = CategoryComboBox.SelectedItem as Categories;

            _productViewSource.View.Filter = item =>
            {
                if (item is Product product)
                {
                    bool matchesSearch = (product.ProductName != null && product.ProductName.ToLower().Contains(searchText)) ||
                                         product.ProductID.ToString().Contains(searchText);
                    bool matchesCategory = selectedCategory == null || product.CategoryID == selectedCategory.CategoryID;
                    return matchesSearch && matchesCategory;
                }
                return false;
            };
        }

        private void RefreshData()
        {
            _context.Product
                .Include(p => p.Categories)
                .Include(p => p.Suppliers)
                .Include(p => p.Photos)
                .Load();
            _productViewSource.Source = _context.Product.Local;
            DGProduct.Items.Refresh();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProd(_context);
            addProductWindow.Owner = Window.GetWindow(this);
            addProductWindow.ShowDialog();
            RefreshData();
        }

        private void ChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as Product;
            if (product == null) return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.png)|*.jpg;*.png|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(openFileDialog.FileName);

                    if (product.Photos == null)
                    {
                        // Если фото ещё нет, создаём новый объект Photos
                        var newPhoto = new Photos
                        {
                            Photo = imageBytes // Предполагаем, что поле называется ImageData
                        };
                        _context.Photos.Add(newPhoto);
                        product.Photos = newPhoto;
                        product.PhotoID = newPhoto.PhotoID; // Связываем через PhotoID
                    }
                    else
                    {
                        // Если фото уже есть, обновляем его
                        product.Photos.Photo = imageBytes;
                    }

                    _context.SaveChanges();
                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке фото: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Логика экспорта в Excel будет добавлена позже.");
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}