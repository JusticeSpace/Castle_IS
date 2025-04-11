using Castle.Administrator;
using Castle.Manager;
using Castle.UserFolder;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Castle
{
    public partial class MainWindow : Window
    {
        private Amo_CastleEntities1 _context = new Amo_CastleEntities1();

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

        private void Label_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
        }

        private void Autorization(object sender, RoutedEventArgs e)
        {
            string login = Log.Text.Trim();
            string password = Pas.Password.Trim();

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

            var user = _context.User
                .FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user == null)
            {
                MessageBox.Show("Неверные учетные данные!");
                return;
            }

            // Сохраняем IdUser в App
            App.CurrentUserId = user.IdUser;

            // Открываем окно в зависимости от роли
            switch (user.RoleID)
            {
                case 1: // Администратор
                    AdWindow adWindow = new AdWindow();
                    adWindow.Show();
                    break;
                case 2: // Пользователь
                    UserWindow userWindow = new UserWindow();
                    userWindow.Show();
                    break;
                case 3: // Менеджер
                    ManagerrWindow managerWindow = new ManagerrWindow();
                    managerWindow.Show();
                    break;
                default:
                    MessageBox.Show("Роль пользователя не определена!");
                    return;
            }

            this.Close();
        }
    }
}