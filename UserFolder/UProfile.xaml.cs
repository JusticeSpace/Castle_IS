using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace Castle.UserFolder
{
    public partial class UProfile : Page
    {
        private readonly Amo_CastleEntities1 _context;
        private User _currentUser;

        public UProfile()
        {
            InitializeComponent();
            _context = new Amo_CastleEntities1();
            LoadUserData();
        }

        private void LoadUserData()
        {
            _currentUser = _context.User
                .Include(u => u.Roles)
                .Include(u => u.Warehouse)
                .FirstOrDefault(u => u.IdUser == App.CurrentUserId);

            if (_currentUser != null)
            {
                DataContext = _currentUser;
            }
            else
            {
                MessageBox.Show("Пользователь не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            var changePasswordWindow = new ChangePasswordWindow(_currentUser.IdUser);
            changePasswordWindow.Owner = Window.GetWindow(this);
            changePasswordWindow.ShowDialog();
        }

        // Добавляем метод для загрузки фото
        private void UploadPhoto_Click(object sender, RoutedEventArgs e)
        {
            // Здесь можно добавить логику загрузки фото, например, открыть диалог выбора файла
            MessageBox.Show("Логика загрузки фото будет добавлена позже.");
        }
    }
}