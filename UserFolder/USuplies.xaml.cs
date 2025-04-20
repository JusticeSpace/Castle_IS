using Castle;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32;
using Castle.ForEveryone;
using ClosedXML.Excel;
using MaterialDesignThemes.Wpf;

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
            _context.Suppliers
                .Include(s => s.Address)
                .Include(s => s.Address.Country)
                .Include(s => s.Address.City)
                .Include(s => s.Address.Street)
                .Load();
            _context.Product
                .Include(p => p.Categories)
                .Include(p => p.Suppliers)
                .Include(p => p.Suppliers.Address)
                .Include(p => p.Suppliers.Address.Country)
                .Include(p => p.Suppliers.Address.City)
                .Include(p => p.Suppliers.Address.Street)
                .Include(p => p.Photos)
                .Load();

            _productViewSource = new CollectionViewSource();
            _productViewSource.Source = _context.Product.Local;
            DGProduct.ItemsSource = _productViewSource.View;

            var categoriesViewSource = (CollectionViewSource)FindResource("CategoriesViewSource");
            var categoriesList = _context.Categories.Local.ToList();
            var allCategories = new Categories { CategoryID = -1, CategoryName = "Все товары" };
            categoriesList.Insert(0, allCategories);
            categoriesViewSource.Source = categoriesList;
            CategoryComboBox.SelectedItem = allCategories;

            var editingCategoriesViewSource = (CollectionViewSource)FindResource("EditingCategoriesViewSource");
            editingCategoriesViewSource.Source = _context.Categories.Local;

            var deleteCategoriesList = _context.Categories.Local.ToList();
            DeleteCategoryComboBox.ItemsSource = deleteCategoriesList;

            var suppliersViewSource = (CollectionViewSource)FindResource("SuppliersViewSource");
            var suppliersList = _context.Suppliers.Local.OrderBy(s => s.SupplierName).ToList();
            suppliersViewSource.Source = suppliersList;

            var deleteSuppliersList = _context.Suppliers.Local.OrderBy(s => s.SupplierName).ToList();
            DeleteSupplierComboBox.ItemsSource = deleteSuppliersList;
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

                Logger.LogAction(
                    $"Удалён товар: {product.ProductName} (ID: {product.ProductID}) пользователем (ID: {App.CurrentUserId})",
                    App.CurrentUserId,
                    $"Категория: {product.Categories?.CategoryName ?? "не указана"}"
                );

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
                    return;
                }
                if (string.IsNullOrWhiteSpace(product.ProductName))
                {
                    MessageBox.Show("Название товара не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Cancel = true;
                    return;
                }
                if (product.Quantity < 0)
                {
                    MessageBox.Show("Количество не может быть отрицательным!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Cancel = true;
                    return;
                }

                if (!_context.Categories.Any(c => c.CategoryID == product.CategoryID))
                {
                    MessageBox.Show("Выбранная категория не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Cancel = true;
                    return;
                }

                if (product.SuppliersID != 0 && !_context.Suppliers.Any(s => s.SuppliersID == product.SuppliersID))
                {
                    MessageBox.Show("Выбранный поставщик не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Cancel = true;
                    return;
                }

                var originalProduct = _context.Entry(product).OriginalValues.ToObject() as Product;
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    e.Cancel = true;
                    return;
                }

                string changes = "";
                if (originalProduct.ProductName != product.ProductName)
                {
                    changes += $"Название изменено с '{originalProduct.ProductName}' на '{product.ProductName}'. ";
                }
                if (originalProduct.Price != product.Price)
                {
                    changes += $"Цена изменена с '{originalProduct.Price}' на '{product.Price}'. ";
                }
                if (originalProduct.Quantity != product.Quantity)
                {
                    changes += $"Количество изменено с '{originalProduct.Quantity}' на '{product.Quantity}'. ";
                }
                if (originalProduct.CategoryID != product.CategoryID)
                {
                    var oldCategoryName = _context.Categories.FirstOrDefault(c => c.CategoryID == originalProduct.CategoryID)?.CategoryName ?? "не указана";
                    var newCategoryName = _context.Categories.FirstOrDefault(c => c.CategoryID == product.CategoryID)?.CategoryName ?? "не указана";
                    changes += $"Категория изменена с '{oldCategoryName}' на '{newCategoryName}'. ";
                }
                if (originalProduct.SuppliersID != product.SuppliersID)
                {
                    var oldSupplierName = _context.Suppliers.FirstOrDefault(s => s.SuppliersID == originalProduct.SuppliersID)?.SupplierName ?? "не указан";
                    var newSupplierName = _context.Suppliers.FirstOrDefault(s => s.SuppliersID == product.SuppliersID)?.SupplierName ?? "не указан";
                    changes += $"Поставщик изменён с '{oldSupplierName}' на '{newSupplierName}'. ";
                }
                if (originalProduct.Comment != product.Comment)
                {
                    changes += $"Комментарий изменён с '{originalProduct.Comment ?? "не указан"}' на '{product.Comment ?? "не указан"}'. ";
                }

                if (!string.IsNullOrEmpty(changes))
                {
                    Logger.LogAction(
                        $"Отредактирован товар: {product.ProductName} (ID: {product.ProductID}) пользователем (ID: {App.CurrentUserId})",
                        App.CurrentUserId,
                        changes.Trim()
                    );
                }
                else
                {
                    Logger.LogAction(
                        $"Отредактирован товар: {product.ProductName} (ID: {product.ProductID}) пользователем (ID: {App.CurrentUserId})",
                        App.CurrentUserId,
                        "Изменения отсутствуют"
                    );
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

            var listView = _productViewSource.View as ListCollectionView;
            if (listView != null && (listView.IsAddingNew || listView.IsEditingItem))
            {
                return;
            }

            var searchText = Seek.Text?.ToLower() ?? "";
            var selectedCategory = CategoryComboBox.SelectedItem as Categories;
            _productViewSource.View.Filter = item =>
            {
                if (item is Product product)
                {
                    bool matchesSearch = (product.ProductName != null && product.ProductName.ToLower().Contains(searchText)) ||
                                         product.ProductID.ToString().Contains(searchText);
                    bool matchesCategory = selectedCategory == null || selectedCategory.CategoryID == -1 ||
                                          (selectedCategory.CategoryID != -1 && product.CategoryID == selectedCategory.CategoryID);
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
                .Include(p => p.Suppliers.Address)
                .Include(p => p.Suppliers.Address.Country)
                .Include(p => p.Suppliers.Address.City)
                .Include(p => p.Suppliers.Address.Street)
                .Include(p => p.Photos)
                .Load();
            _productViewSource.Source = _context.Product.Local;

            var categoriesViewSource = (CollectionViewSource)FindResource("CategoriesViewSource");
            var categoriesList = _context.Categories.Local.ToList();
            var allCategories = new Categories { CategoryID = -1, CategoryName = "Все товары" };
            categoriesList.Insert(0, allCategories);
            categoriesViewSource.Source = categoriesList;

            var editingCategoriesViewSource = (CollectionViewSource)FindResource("EditingCategoriesViewSource");
            editingCategoriesViewSource.Source = _context.Categories.Local;

            var deleteCategoriesList = _context.Categories.Local.ToList();
            DeleteCategoryComboBox.ItemsSource = deleteCategoriesList;

            var suppliersViewSource = (CollectionViewSource)FindResource("SuppliersViewSource");
            var suppliersList = _context.Suppliers.Local.OrderBy(s => s.SupplierName).ToList();
            suppliersViewSource.Source = suppliersList;

            var deleteSuppliersList = _context.Suppliers.Local.OrderBy(s => s.SupplierName).ToList();
            DeleteSupplierComboBox.ItemsSource = deleteSuppliersList;

            DGProduct.Items.Refresh();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProd(_context);
            addProductWindow.Owner = Window.GetWindow(this);
            addProductWindow.ShowDialog();
            RefreshData();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var categoryName = CategoryNameTextBox.Text?.Trim();
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                MessageBox.Show("Название категории не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_context.Categories.Any(c => c.CategoryName == categoryName))
                {
                    MessageBox.Show("Категория с таким названием уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newCategory = new Categories
                {
                    CategoryName = categoryName
                };
                _context.Categories.Add(newCategory);
                _context.SaveChanges();

                Logger.LogAction(
                    $"Добавлена категория: {newCategory.CategoryName} (ID: {newCategory.CategoryID}) пользователем (ID: {App.CurrentUserId})",
                    App.CurrentUserId,
                    "Категория успешно добавлена"
                );

                CategoryNameTextBox.Text = string.Empty;
                RefreshData();
                MessageBox.Show("Категория успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении категории: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            var selectedCategory = DeleteCategoryComboBox.SelectedItem as Categories;
            if (selectedCategory == null)
            {
                MessageBox.Show("Выберите категорию для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedCategory.CategoryID == -1)
            {
                MessageBox.Show("Нельзя удалить категорию 'Все товары'!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_context.Product.Any(p => p.CategoryID == selectedCategory.CategoryID))
            {
                MessageBox.Show("Эта категория используется в товарах. Удалите или измените категорию у товаров перед удалением!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Вы уверены, что хотите удалить категорию '{selectedCategory.CategoryName}'?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    if (_context.Entry(selectedCategory).State == EntityState.Detached)
                    {
                        _context.Categories.Attach(selectedCategory);
                    }
                    _context.Categories.Remove(selectedCategory);
                    _context.SaveChanges();

                    Logger.LogAction(
                        $"Удалена категория: {selectedCategory.CategoryName} (ID: {selectedCategory.CategoryID}) пользователем (ID: {App.CurrentUserId})",
                        App.CurrentUserId,
                        "Категория успешно удалена"
                    );

                    RefreshData();
                    MessageBox.Show("Категория успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении категории: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            var supplierName = SupplierNameTextBox.Text?.Trim();
            var contactPerson = ContactPersonTextBox.Text?.Trim();
            var phone = PhoneTextBox.Text?.Trim();
            var email = EmailTextBox.Text?.Trim();
            var addressIdText = AddressTextBox.Text?.Trim();

            if (string.IsNullOrWhiteSpace(supplierName))
            {
                MessageBox.Show("Название поставщика не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int? addressId = null;
            if (!string.IsNullOrWhiteSpace(addressIdText))
            {
                if (!int.TryParse(addressIdText, out int parsedAddressId))
                {
                    MessageBox.Show("ID адреса должен быть числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                addressId = parsedAddressId;

                if (!_context.Address.Any(a => a.AddressID == addressId))
                {
                    MessageBox.Show("Указанный ID адреса не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            try
            {
                if (_context.Suppliers.Any(s => s.SupplierName == supplierName))
                {
                    MessageBox.Show("Поставщик с таким названием уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newSupplier = new Suppliers
                {
                    SupplierName = supplierName,
                    ContactPerson = string.IsNullOrWhiteSpace(contactPerson) ? null : contactPerson,
                    Phone = string.IsNullOrWhiteSpace(phone) ? null : phone,
                    Email = string.IsNullOrWhiteSpace(email) ? null : email,
                    AddressID = addressId
                };
                _context.Suppliers.Add(newSupplier);
                _context.SaveChanges();

                Logger.LogAction(
                    $"Добавлен поставщик: {newSupplier.SupplierName} (ID: {newSupplier.SuppliersID}) пользователем (ID: {App.CurrentUserId})",
                    App.CurrentUserId,
                    $"Контактное лицо: {newSupplier.ContactPerson ?? "не указано"}, Телефон: {newSupplier.Phone ?? "не указан"}, Email: {newSupplier.Email ?? "не указан"}, Адрес ID: {newSupplier.AddressID?.ToString() ?? "не указан"}"
                );

                SupplierNameTextBox.Text = string.Empty;
                ContactPersonTextBox.Text = string.Empty;
                PhoneTextBox.Text = string.Empty;
                EmailTextBox.Text = string.Empty;
                AddressTextBox.Text = string.Empty;

                RefreshData();
                MessageBox.Show("Поставщик успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении поставщика: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            var selectedSupplier = DeleteSupplierComboBox.SelectedItem as Suppliers;
            if (selectedSupplier == null)
            {
                MessageBox.Show("Выберите поставщика для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_context.Product.Any(p => p.SuppliersID == selectedSupplier.SuppliersID))
            {
                MessageBox.Show("Этот поставщик используется в товарах. Удалите или измените поставщика у товаров перед удалением!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Вы уверены, что хотите удалить поставщика '{selectedSupplier.SupplierName}'?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    if (_context.Entry(selectedSupplier).State == EntityState.Detached)
                    {
                        _context.Suppliers.Attach(selectedSupplier);
                    }
                    _context.Suppliers.Remove(selectedSupplier);
                    _context.SaveChanges();

                    Logger.LogAction(
                        $"Удалён поставщик: {selectedSupplier.SupplierName} (ID: {selectedSupplier.SuppliersID}) пользователем (ID: {App.CurrentUserId})",
                        App.CurrentUserId,
                        "Поставщик успешно удалён"
                    );

                    RefreshData();
                    MessageBox.Show("Поставщик успешно удалён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении поставщика: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
                        var newPhoto = new Photos
                        {
                            Photo = imageBytes,
                        };
                        _context.Photos.Add(newPhoto);
                        product.Photos = newPhoto;
                        product.PhotoID = newPhoto.PhotoID;
                    }
                    else
                    {
                        product.Photos.Photo = imageBytes;
                    }

                    _context.SaveChanges();

                    Logger.LogAction(
                        $"Изменено фото товара: {product.ProductName} (ID: {product.ProductID}) пользователем (ID: {App.CurrentUserId})",
                        App.CurrentUserId,
                        "Фото обновлено"
                    );

                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке фото: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ViewSupplierDetails_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as Product;
            if (product?.Suppliers == null) return;

            var supplierDetails = new SupplierDetails
            {
                DataContext = product.Suppliers
            };

            DialogHost.Show(supplierDetails, "SupplierDialog");
        }

        private void ExportProductsToExcel(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Товары");
                var products = _context.Product.ToList();

                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Название";
                worksheet.Cell(1, 3).Value = "Цена";
                worksheet.Cell(1, 4).Value = "Категория";
                worksheet.Cell(1, 5).Value = "Поставщик";
                worksheet.Cell(1, 6).Value = "Фото";

                for (int i = 0; i < products.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = products[i].ProductID;
                    worksheet.Cell(i + 2, 2).Value = products[i].ProductName;
                    worksheet.Cell(i + 2, 3).Value = products[i].Price;
                    worksheet.Cell(i + 2, 4).Value = products[i].CategoryID;
                    worksheet.Cell(i + 2, 5).Value = products[i].SuppliersID;
                    worksheet.Cell(i + 2, 6).Value = products[i].PhotoID;
                }

                workbook.SaveAs(filePath);
            }
        }

        private void ExportProductsButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx",
                FileName = "supplies.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                ExportProductsToExcel(saveFileDialog.FileName);

                Logger.LogAction(
                    $"Экспортированы данные товаров в Excel пользователем (ID: {App.CurrentUserId})",
                    App.CurrentUserId,
                    "Экспорт завершён"
                );

                MessageBox.Show("Экспорт товаров завершён!");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}