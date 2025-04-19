using System;
using System.Windows;
using System.Data.Entity;
using Microsoft.Win32;
using System.Linq;
using System.Windows.Media.Imaging;
using System.IO;

namespace Castle.Manager
{
    public partial class AddWorker : Window
    {
        private readonly Amo_CastleEntities1 _context;
        private User _newUser;
        private byte[] _photoData;

        public AddWorker(Amo_CastleEntities1 context)
        {
            InitializeComponent();
            _context = context;
            _newUser = new User();
            DataContext = _newUser;
            LoadRoles();
        }

        private void LoadRoles()
        {
            RoleComboBox.ItemsSource = _context.Roles.ToList();
            RoleComboBox.SelectedIndex = -1;
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
                    WorkerImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    WorkerImage.Visibility = Visibility.Visible;
                    ImageStatusText.Visibility = Visibility.Collapsed;

                    // Логируем загрузку фото
                    Logger.LogAction(
                        $"Загружено фото для нового работника пользователем (ID: {App.CurrentUserId})",
                        App.CurrentUserId,
                        "Фото добавлено для нового работника"
                    );
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
                // Проверка обязательных полей
                if (string.IsNullOrWhiteSpace(_newUser.Login))
                {
                    MessageBox.Show("Введите логин!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(_newUser.Password))
                {
                    MessageBox.Show("Введите пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(_newUser.Surname))
                {
                    MessageBox.Show("Введите фамилию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(_newUser.UserName))
                {
                    MessageBox.Show("Введите имя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (_newUser.RoleID == 0)
                {
                    MessageBox.Show("Выберите роль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка уникальности логина
                if (_context.User.Any(u => u.Login == _newUser.Login))
                {
                    MessageBox.Show("Логин уже занят!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Создаём нового работника
                var userToAdd = new User
                {
                    Login = _newUser.Login,
                    Password = _newUser.Password,
                    Surname = _newUser.Surname,
                    UserName = _newUser.UserName,
                    Patronymic = _newUser.Patronymic,
                    Email = _newUser.Email,
                    RoleID = _newUser.RoleID
                };

                _context.User.Add(userToAdd);
                _context.SaveChanges();

                _newUser = userToAdd;

                // Сохраняем фото, если оно есть
                if (_photoData != null)
                {
                    var photo = new Photos
                    {
                        Photo = _photoData
                    };
                    _context.Photos.Add(photo);
                    _context.SaveChanges();
                    _newUser.PhotoID = photo.PhotoID;
                    _context.SaveChanges();
                }

                // Получаем название роли для подробностей
                var roleName = _context.Roles.FirstOrDefault(r => r.RoleID == _newUser.RoleID)?.RoleName ?? "Неизвестная роль";

                // Логируем добавление нового работника с подробностями
                Logger.LogAction(
                    $"Добавлен новый работник: {_newUser.Surname} {_newUser.UserName} (ID: {_newUser.IdUser}) пользователем (ID: {App.CurrentUserId})",
                    App.CurrentUserId,
                    $"Логин: {_newUser.Login}, Роль: {roleName}, Email: {_newUser.Email ?? "не указан"}"
                );

                MessageBox.Show("Работник добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}\nInner Exception: {ex.InnerException?.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}