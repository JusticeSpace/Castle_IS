using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Castle.UserFolder
{
    public partial class ChangePasswordWindow : Window
    {
        private readonly Amo_CastleEntities1 _context;
        private readonly int _userId;

        public ChangePasswordWindow(int userId)
        {
            InitializeComponent();
            _context = new Amo_CastleEntities1();
            _userId = userId;
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = OldPassword.Password;
            string newPassword = NewPassword.Password;

            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            MessageBox.Show("Пароль успешно изменён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}