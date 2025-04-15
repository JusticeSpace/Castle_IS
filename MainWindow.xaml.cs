using Castle.Administrator;
using Castle.UserFolder;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using Castle.DataFolder;
using System.Windows.Input;
using System.Linq;

namespace Castle
{
    public partial class MainWindow : Window
    {
        private readonly Amo_CastleEntities1 _context;
        private bool _isPasswordVisible = false;

        public MainWindow()
        {
            _context = new Amo_CastleEntities1();
            InitializeComponent();
        }
        private void Pas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Autorization(sender, e);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Autorization(object sender, RoutedEventArgs e)
        {
            string login = Login.Text.Trim();
            string password = _isPasswordVisible ? VisiblePas.Text.Trim() : Password.Password.Trim();

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Введите логин!");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль!");
                return;
            }

            // Логика авторизации с использованием _context
            var user = _context.User.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user == null)
            {
                MessageBox.Show("Неверные учетные данные!");
                return;
            }

            // Сохраняем IdUser в App
            App.CurrentUserId = user.IdUser;

            // Переход на другие окна в зависимости от роли
            switch (user.RoleID)
            {
                case 1:
                    new AdWindow().Show();
                    break;
                case 2:
                    new UserWindow().Show();
                    break;
                case 3:
                    new ManagerWindow().Show();
                    break;
                default:
                    MessageBox.Show("Роль пользователя не определена!");
                    return;
            }
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void TogglePasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;
            if (_isPasswordVisible)
            {
                VisiblePas.Text = Password.Password;
                VisiblePas.Visibility = Visibility.Visible;
                Password.Visibility = Visibility.Collapsed;
                PasswordEyeIcon.Kind = PackIconKind.EyeOff;
            }
            else
            {
                Password.Password = VisiblePas.Text;
                VisiblePas.Visibility = Visibility.Collapsed;
                Password.Visibility = Visibility.Visible;
                PasswordEyeIcon.Kind = PackIconKind.Eye;
            }
        }

        private void ForgotPassword_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Функция восстановления пароля пока не реализована.");
        }
    }
}