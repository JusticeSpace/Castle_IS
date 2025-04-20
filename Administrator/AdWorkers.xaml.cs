using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;
using System.IO;
using ClosedXML.Excel;

namespace Castle.Administrator
{
    public partial class AdWorkers : Page
    {
        private Amo_CastleEntities1 _context;
        private CollectionViewSource _userViewSource;

        public AdWorkers()
        {
            InitializeComponent();
        }

        private void ExportUsersToExcel(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Пользователи");
                var users = _context.User.ToList();

                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Логин";
                worksheet.Cell(1, 3).Value = "Пароль";
                worksheet.Cell(1, 4).Value = "Фамилия";
                worksheet.Cell(1, 5).Value = "Имя";
                worksheet.Cell(1, 6).Value = "Отчество";
                worksheet.Cell(1, 7).Value = "Email";
                worksheet.Cell(1, 8).Value = "Роль";
                // Если добавишь поле RegistrationDate, раскомментируй:
                // worksheet.Cell(1, 9).Value = "Дата регистрации";

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
                    // Если добавишь поле RegistrationDate, раскомментируй:
                    // worksheet.Cell(i + 2, 9).Value = users[i].RegistrationDate?.ToString("dd.MM.yyyy HH:mm:ss");
                }

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
            if (MessageBox.Show("Удалить пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var user = (User)((Button)sender).DataContext;
                if (_context.Entry(user).State == EntityState.Detached)
                {
                    _context.User.Attach(user);
                }
                _context.User.Remove(user);
                _context.SaveChanges();

                Logger.LogAction(
                    $"Удалён пользователь: {user.Surname} {user.UserName} (ID: {user.IdUser}) пользователем (ID: {App.CurrentUserId})",
                    App.CurrentUserId,
                    $"Логин: {user.Login}, Роль: {_context.Roles.FirstOrDefault(r => r.RoleID == user.RoleID)?.RoleName ?? "Неизвестная роль"}"
                );

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
                    return;
                }
                if (string.IsNullOrWhiteSpace(user.Login))
                {
                    MessageBox.Show("Логин не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Cancel = true;
                    return;
                }
                if (string.IsNullOrWhiteSpace(user.Password))
                {
                    MessageBox.Show("Пароль не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Cancel = true;
                    return;
                }

                // Проверяем уникальность логина
                var existingUser = _context.User.FirstOrDefault(u => u.Login == user.Login && u.IdUser != user.IdUser);
                if (existingUser != null)
                {
                    MessageBox.Show("Этот логин уже занят другим пользователем!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Cancel = true;
                    return;
                }

                // Сохраняем оригинальные значения для логирования изменений
                var originalUser = _context.Entry(user).OriginalValues.ToObject() as User;
                _context.SaveChanges();

                // Формируем подробности изменений
                string changes = "";
                if (originalUser.Login != user.Login)
                {
                    changes += $"Логин изменён с '{originalUser.Login}' на '{user.Login}'. ";
                }
                if (originalUser.Password != user.Password)
                {
                    changes += $"Пароль изменён с '{originalUser.Password}' на '{user.Password}'. ";
                }
                if (originalUser.Surname != user.Surname)
                {
                    changes += $"Фамилия изменена с '{originalUser.Surname}' на '{user.Surname}'. ";
                }
                if (originalUser.UserName != user.UserName)
                {
                    changes += $"Имя изменено с '{originalUser.UserName}' на '{user.UserName}'. ";
                }
                if (originalUser.Patronymic != user.Patronymic)
                {
                    changes += $"Отчество изменено с '{originalUser.Patronymic ?? "не указано"}' на '{user.Patronymic ?? "не указано"}'. ";
                }
                if (originalUser.Email != user.Email)
                {
                    changes += $"Email изменён с '{originalUser.Email ?? "не указано"}' на '{user.Email ?? "не указано"}'. ";
                }
                if (originalUser.RoleID != user.RoleID)
                {
                    var oldRoleName = _context.Roles.FirstOrDefault(r => r.RoleID == originalUser.RoleID)?.RoleName ?? "Неизвестная роль";
                    var newRoleName = _context.Roles.FirstOrDefault(r => r.RoleID == user.RoleID)?.RoleName ?? "Неизвестная роль";
                    changes += $"Роль изменена с '{oldRoleName}' на '{newRoleName}'. ";
                }

                // Логируем изменения
                if (!string.IsNullOrEmpty(changes))
                {
                    Logger.LogAction(
                        $"Отредактирован пользователь: {user.Surname} {user.UserName} (ID: {user.IdUser}) пользователем (ID: {App.CurrentUserId})",
                        App.CurrentUserId,
                        changes.Trim()
                    );
                }
                else
                {
                    Logger.LogAction(
                        $"Отредактирован пользователь: {user.Surname} {user.UserName} (ID: {user.IdUser}) пользователем (ID: {App.CurrentUserId})",
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
                                         (user.Login != null && user.Login.ToLower().Contains(searchText)) ||
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
            var addWorkerWindow = new Castle.Manager.AddWorker(_context);
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

                    Logger.LogAction(
                        $"Изменено фото пользователя: {user.Surname} {user.UserName} (ID: {user.IdUser}) пользователем (ID: {App.CurrentUserId})",
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

                Logger.LogAction(
                    $"Экспортированы данные пользователей в Excel пользователем (ID: {App.CurrentUserId})",
                    App.CurrentUserId,
                    "Экспорт завершён"
                );

                MessageBox.Show("Экспорт пользователей завершён!");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}