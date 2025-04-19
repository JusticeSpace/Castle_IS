using System.Windows;
using System.Windows.Controls;
using Castle.Manager;
using Castle.UserFolder;

namespace Castle
{
    public partial class ManagerWindow : Window
    {
        private bool _isProfileSelected;
        private bool _isGoodsSelected;
        private bool _isWorkersSelected;
        private bool _isLogoutSelected;

        public bool IsProfileSelected
        {
            get => _isProfileSelected;
            set
            {
                _isProfileSelected = value;
                UpdateButtonStates();
            }
        }

        public bool IsGoodsSelected
        {
            get => _isGoodsSelected;
            set
            {
                _isGoodsSelected = value;
                UpdateButtonStates();
            }
        }

        public bool IsWorkersSelected
        {
            get => _isWorkersSelected;
            set
            {
                _isWorkersSelected = value;
                UpdateButtonStates();
            }
        }

        public bool IsLogoutSelected
        {
            get => _isLogoutSelected;
            set
            {
                _isLogoutSelected = value;
                UpdateButtonStates();
            }
        }

        public ManagerWindow()
        {
            InitializeComponent();
            DataContext = this;
            MainFrame.Navigate(new UProfile());
            IsProfileSelected = true;
        }

        private void UpdateButtonStates()
        {
            DataContext = null;
            DataContext = this;
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UProfile());
            IsProfileSelected = true;
            IsGoodsSelected = false;
            IsWorkersSelected = false;
            IsLogoutSelected = false;
        }

        private void Goods_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new USuplies());
            IsProfileSelected = false;
            IsGoodsSelected = true;
            IsWorkersSelected = false;
            IsLogoutSelected = false;
        }

        private void Workers_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Castle.Manager.UWorkers());
            IsProfileSelected = false;
            IsGoodsSelected = false;
            IsWorkersSelected = true;
            IsLogoutSelected = false;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            IsProfileSelected = false;
            IsGoodsSelected = false;
            IsWorkersSelected = false;
            IsLogoutSelected = true;

            // Открываем окно авторизации
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Закрываем текущее окно
            this.Close();
        }
    }
}   