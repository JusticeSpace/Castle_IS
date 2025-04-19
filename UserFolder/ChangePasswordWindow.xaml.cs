using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace Castle.UserFolder
{
    public partial class ChangePasswordWindow : Window
    {
        private readonly Amo_CastleEntities1 _context;
        private readonly int _userId;
        private bool _isOldPasswordVisible;

        public ChangePasswordWindow(int userId)
        {
            InitializeComponent();
            _context = new Amo_CastleEntities1();
            _userId = userId;
            _isOldPasswordVisible = false;
        }

        private void ToggleOldPasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            _isOldPasswordVisible = !_isOldPasswordVisible;
            if (_isOldPasswordVisible)
            {
                VisibleOldPassword.Text = OldPassword.Password;
                VisibleOldPassword.Visibility = Visibility.Visible;
                OldPassword.Visibility = Visibility.Collapsed;
                OldPasswordEyeIcon.Kind = PackIconKind.EyeOff;
            }
            else
            {
                OldPassword.Password = VisibleOldPassword.Text;
                VisibleOldPassword.Visibility = Visibility.Collapsed;
                OldPassword.Visibility = Visibility.Visible;
                OldPasswordEyeIcon.Kind = PackIconKind.Eye;
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = _isOldPasswordVisible ? VisibleOldPassword.Text : OldPassword.Password;
            string newPassword = NewPassword.Password;

            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (oldPassword == newPassword)
            {
                MessageBox.Show("Старый и новый пароли не могут совпадать!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = _context.User.FirstOrDefault(u => u.IdUser == _userId && u.Password == oldPassword);
            if (user == null)
            {
                MessageBox.Show("Старый пароль неверен!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            user.Password = newPassword;
            _context.SaveChanges();

            // Логируем изменение пароля с подробностями
            Logger.LogAction(
                $"Изменён пароль пользователя: {user.Login} (ID: {user.IdUser})",
                App.CurrentUserId,
                "Пароль успешно обновлён"
            );

            MessageBox.Show("Пароль успешно изменён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}