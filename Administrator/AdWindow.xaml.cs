using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Castle.Manager;
using Castle.UserFolder;

namespace Castle.Administrator
{
    public partial class AdWindow : Window, INotifyPropertyChanged
    {
        private bool _isProfileSelected;
        private bool _isGoodsSelected;
        private bool _isWorkersSelected;
        private bool _isLogsSelected;
        private bool _isLogoutSelected;

        public bool IsProfileSelected
        {
            get => _isProfileSelected;
            set
            {
                _isProfileSelected = value;
                OnPropertyChanged(nameof(IsProfileSelected));
            }
        }

        public bool IsGoodsSelected
        {
            get => _isGoodsSelected;
            set
            {
                _isGoodsSelected = value;
                OnPropertyChanged(nameof(IsGoodsSelected));
            }
        }

        public bool IsWorkersSelected
        {
            get => _isWorkersSelected;
            set
            {
                _isWorkersSelected = value;
                OnPropertyChanged(nameof(IsWorkersSelected));
            }
        }

        public bool IsLogsSelected
        {
            get => _isLogsSelected;
            set
            {
                _isLogsSelected = value;
                OnPropertyChanged(nameof(IsLogsSelected));
            }
        }

        public bool IsLogoutSelected
        {
            get => _isLogoutSelected;
            set
            {
                _isLogoutSelected = value;
                OnPropertyChanged(nameof(IsLogoutSelected));
            }
        }

        public AdWindow()
        {
            InitializeComponent();
            DataContext = this;
            MainFrame.Navigate(new UProfile());
            IsProfileSelected = true;
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UProfile());
            IsProfileSelected = true;
            IsGoodsSelected = false;
            IsWorkersSelected = false;
            IsLogsSelected = false;
            IsLogoutSelected = false;
        }

        private void Goods_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new USuplies());
            IsProfileSelected = false;
            IsGoodsSelected = true;
            IsWorkersSelected = false;
            IsLogsSelected = false;
            IsLogoutSelected = false;
        }

        private void Workers_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdWorkers());
            IsProfileSelected = false;
            IsGoodsSelected = false;
            IsWorkersSelected = true;
            IsLogsSelected = false;
            IsLogoutSelected = false;
        }

        private void Logs_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ULogs());
            IsProfileSelected = false;
            IsGoodsSelected = false;
            IsWorkersSelected = false;
            IsLogsSelected = true;
            IsLogoutSelected = false;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Window.GetWindow(this).Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}