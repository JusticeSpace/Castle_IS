using System;
using System.Windows;
using System.Data.Entity;
using Microsoft.Win32;
using System.Linq;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;

namespace Castle.ForEveryone
{
    public partial class AddProd : Window
    {
        private readonly Amo_CastleEntities1 _context;
        private Product _newProduct;
        private byte[] _photoData; // Временное хранение фото до сохранения

        public AddProd(Amo_CastleEntities1 context)
        {
            InitializeComponent();
            _context = context;
            _newProduct = new Product();
            DataContext = _newProduct;
            LoadCategoriesAndSuppliers(); // Загружаем категории и поставщиков
        }

        private void LoadCategoriesAndSuppliers()
        {
            CategoryComboBox.ItemsSource = _context.Categories.ToList();
            SupplierComboBox.ItemsSource = _context.Suppliers.ToList();
            SupplierComboBox.SelectedIndex = -1; // Ничего не выбрано по умолчанию
        }

        private void UploadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _photoData = File.ReadAllBytes(openFileDialog.FileName);
                    ProductImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    ProductImage.Visibility = Visibility.Visible;
                    ImageStatusText.Visibility = Visibility.Collapsed;
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
                // Проверяем обязательные поля
                if (string.IsNullOrWhiteSpace(_newProduct.ProductName))
                {
                    MessageBox.Show("Введите название товара!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (_newProduct.CategoryID == 0)
                {
                    MessageBox.Show("Выберите категорию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (_newProduct.Price <= 0)
                {
                    MessageBox.Show("Введите корректную цену!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (_newProduct.Quantity <= 0)
                {
                    MessageBox.Show("Введите корректное количество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // (Опционально) Если фото обязательно
                if (_photoData == null)
                {
                    MessageBox.Show("Загрузите фото товара!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // (Опционально) Если поставщик обязателен
                if (string.IsNullOrEmpty(SupplierComboBox.Text))
                {
                    MessageBox.Show("Выберите поставщика!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Отладка: проверяем ProductID до сохранения
                MessageBox.Show($"ProductID до сохранения: {_newProduct.ProductID}", "Отладка");

                // Добавляем продукт в контекст и сохраняем
                _context.Product.Add(_newProduct);
                _context.SaveChanges();

                // Отладка: проверяем ProductID после сохранения
                MessageBox.Show($"ProductID после сохранения: {_newProduct.ProductID}", "Отладка");

                // Проверяем, что ProductID сгенерирован
                if (_newProduct.ProductID <= 0)
                {
                    throw new Exception("ProductID не был сгенерирован корректно.");
                }

                // После сохранения продукта ProductID сгенерирован, теперь можно привязать фото
                if (_photoData != null)
                {
                    // Проверяем, не используется ли этот ProductID уже в Photos
                    if (_context.Photos.Any(p => p.EntityID == _newProduct.ProductID))
                    {
                        throw new Exception($"EntityID {_newProduct.ProductID} уже используется в таблице Photos.");
                    }

                    _newProduct.Photos = new Photos
                    {
                        Photo = _photoData,
                        EntityType = "Product",
                        EntityID = _newProduct.ProductID
                    };
                    _context.Photos.Add(_newProduct.Photos);
                    _newProduct.PhotoID = _newProduct.Photos.PhotoID;
                    _context.SaveChanges();
                }

                MessageBox.Show("Продукт добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                // Более подробное сообщение об ошибке
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}\nInner Exception: {ex.InnerException?.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчики для PriceTextBox
        private void PriceTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PriceHint.Visibility = Visibility.Collapsed;
        }

        private void PriceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PriceTextBox.Text))
            {
                PriceHint.Visibility = Visibility.Visible;
                _newProduct.Price = 0; // Сбрасываем значение
            }
        }

        private void PriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                _newProduct.Price = price;
            }
            else
            {
                _newProduct.Price = 0; // Если введено некорректное значение, сбрасываем
            }

            PriceHint.Visibility = string.IsNullOrWhiteSpace(PriceTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        // Обработчики для QuantityTextBox
        private void QuantityTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            QuantityHint.Visibility = Visibility.Collapsed;
        }

        private void QuantityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(QuantityTextBox.Text))
            {
                QuantityHint.Visibility = Visibility.Visible;
                _newProduct.Quantity = 0; // Сбрасываем значение
            }
        }

        private void QuantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(QuantityTextBox.Text, out int quantity))
            {
                _newProduct.Quantity = quantity;
            }
            else
            {
                _newProduct.Quantity = 0; // Если введено некорректное значение, сбрасываем
            }

            QuantityHint.Visibility = string.IsNullOrWhiteSpace(QuantityTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}