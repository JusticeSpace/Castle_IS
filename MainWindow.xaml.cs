using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Castle
{
    public partial class MainWindow : Window
    {
        private bool _isPasswordVisible = false;

        public MainWindow()
        {
            InitializeComponent();
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
            string login = Log.Text.Trim();
            string password = _isPasswordVisible ? VisiblePas.Text.Trim() : Pas.Password.Trim();
            // Логика авторизации остаётся прежней
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
                VisiblePas.Text = Pas.Password;
                VisiblePas.Visibility = Visibility.Visible;
                Pas.Visibility = Visibility.Collapsed;
                PasswordEyeIcon.Kind = PackIconKind.EyeOff;
            }
            else
            {
                Pas.Password = VisiblePas.Text;
                VisiblePas.Visibility = Visibility.Collapsed;
                Pas.Visibility = Visibility.Visible;
                PasswordEyeIcon.Kind = PackIconKind.Eye;
            }
        }

        private void ForgotPassword_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Функция восстановления пароля пока не реализована.");
        }
    }
}