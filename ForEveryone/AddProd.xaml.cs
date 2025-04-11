using System;
using System.Windows;
using System.Data.Entity;
using Microsoft.Win32;
using System.Linq;
using System.Windows.Media.Imaging;
using System.IO; // Добавлено для использования класса File
using System.Windows.Input;

namespace Castle.ForEveryone
{
    public partial class AddProd : Window
    {
        private readonly Amo_CastleEntities1 _context;
        private Product _newProduct;

        public AddProd(Amo_CastleEntities1 context)
        {
            InitializeComponent();
            _context = context;
            _newProduct = new Product();
            DataContext = _newProduct;
        }

        private void LoadCategoriesAndSuppliers()
        {
            CategoryComboBox.ItemsSource = _context.Categories.ToList();
            SupplierComboBox.ItemsSource = _context.Suppliers.ToList();
        }

        private void UploadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    byte[] photoData = File.ReadAllBytes(openFileDialog.FileName);
                    ProductPhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось загрузить фото: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.Product.Add(_newProduct);
                _context.SaveChanges();
                MessageBox.Show("Продукт добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}