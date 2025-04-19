using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;
using System.IO;
using ClosedXML.Excel;

namespace Castle.Manager
{
    public partial class UWorkers : Page
    {
        private Amo_CastleEntities1 _context;
        private CollectionViewSource _userViewSource;

        public UWorkers()
        {
            InitializeComponent();
        }


        private void ExportUsersToExcel(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Работники");
                var users = _context.User.ToList(); // Получаем список работников из базы данных

                // Добавляем заголовки
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Логин";
                worksheet.Cell(1, 3).Value = "Пароль";
                worksheet.Cell(1, 4).Value = "Фамилия";
                worksheet.Cell(1, 5).Value = "Имя";
                worksheet.Cell(1, 6).Value = "Отчество";
                worksheet.Cell(1, 7).Value = "Email";
                worksheet.Cell(1, 8).Value = "Роль";
                worksheet.Cell(1, 9).Value = "Фото";

                // Заполняем данными
                for (int i = 0; i < users.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = users[i].IdUser;
                    worksheet.Cell(i + 2, 2).Value = users[i].Login;
                    worksheet.Cell(i + 2, 3).Value = users[i].Password;
                    worksheet.Cell(i + 2, 4).Value = users[i].Surname;
                    worksheet.Cell(i + 2, 5).Value = users[i].UserName;
                    worksheet.Cell(i + 2, 6).Value = users[i].Patronymic;
                    worksheet.Cell(i + 2, 7).Value = users[i].Email;
                    worksheet.Cell(i + 2, 8).Value = users[i].RoleID;
                    worksheet.Cell(i + 2, 9).Value = users[i].PhotoID;
                }

                // Сохраняем файл
                workbook.SaveAs(filePath);
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _context = new Amo_CastleEntities1();

            _context.Roles.Load();
            _context.User
                .Include(u => u.Roles)
                .Include(u => u.Photos)
                .Load();

            _userViewSource = new CollectionViewSource();
            _userViewSource.Source = _context.User.Local;
            DGWorkers.ItemsSource = _userViewSource.View;

            var rolesViewSource = (CollectionViewSource)FindResource("RolesViewSource");
            var rolesList = _context.Roles.Local.ToList();
            var allRoles = new Roles { RoleID = -1, RoleName = "Все роли" };
            rolesList.Insert(0, allRoles);
            rolesViewSource.Source = rolesList;
            RoleComboBox.SelectedItem = allRoles;

            var editingRolesViewSource = (CollectionViewSource)FindResource("EditingRolesViewSource");
            editingRolesViewSource.Source = _context.Roles.Local;
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

        private void DeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить работника?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var user = (User)((Button)sender).DataContext;
                if (_context.Entry(user).State == EntityState.Detached)
                {
                    _context.User.Attach(user);
                }
                _context.User.Remove(user);
                _context.SaveChanges();
                RefreshData();
            }
        }

        private void DGWorkers_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var user = e.Row.Item as User;
                if (string.IsNullOrWhiteSpace(user.Surname) || string.IsNullOrWhiteSpace(user.UserName))
                {
                    MessageBox.Show("Фамилия и имя не могут быть пустыми!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Cancel = true;
                }
                else
                {
                    _context.SaveChanges();
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFilter();
        }

        private void RoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter();
        }

        private void UpdateFilter()
        {
            if (_userViewSource?.View == null || Seek == null) return;

            var listView = _userViewSource.View as ListCollectionView;
            if (listView != null && (listView.IsAddingNew || listView.IsEditingItem))
            {
                return;
            }

            var searchText = Seek.Text?.ToLower() ?? "";
            var selectedRole = RoleComboBox.SelectedItem as Roles;
            _userViewSource.View.Filter = item =>
            {
                if (item is User user)
                {
                    bool matchesSearch = (user.UserName != null && user.UserName.ToLower().Contains(searchText)) ||
                                         (user.Surname != null && user.Surname.ToLower().Contains(searchText)) ||
                                         user.IdUser.ToString().Contains(searchText);
                    bool matchesRole = selectedRole == null || selectedRole.RoleID == -1 ||
                                       (selectedRole.RoleID != -1 && user.RoleID == selectedRole.RoleID);
                    return matchesSearch && matchesRole;
                }
                return false;
            };
        }

        private void RefreshData()
        {
            _context.User
                .Include(u => u.Roles)
                .Include(u => u.Photos)
                .Load();
            _userViewSource.Source = _context.User.Local;
            DGWorkers.Items.Refresh();
        }

        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            var addWorkerWindow = new AddWorker(_context);
            addWorkerWindow.Owner = Window.GetWindow(this);
            addWorkerWindow.ShowDialog();
            RefreshData();
        }

        private void ChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            var user = (sender as Button).DataContext as User;
            if (user == null) return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.png)|*.jpg;*.png|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(openFileDialog.FileName);

                    if (user.Photos == null)
                    {
                        var newPhoto = new Photos
                        {
                            Photo = imageBytes,
                        };
                        _context.Photos.Add(newPhoto);
                        user.Photos = newPhoto;
                        user.PhotoID = newPhoto.PhotoID;
                    }
                    else
                    {
                        user.Photos.Photo = imageBytes;
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

        private void ExportUsersButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx",
                FileName = "users.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                ExportUsersToExcel(saveFileDialog.FileName);
                MessageBox.Show("Экспорт работников завершён!");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}