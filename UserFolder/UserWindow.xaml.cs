using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YourNamespace;

namespace Castle.UserFolder
{
    public partial class UserWindow : Window
    {
        public bool IsProfileSelected { get; set; }
        public bool IsGoodsSelected { get; set; }
        public bool IsLogoutSelected { get; set; }

        public UserWindow()
        {
            InitializeComponent();
            DataContext = this; // Убедитесь, что привязки работают
            MainFrame.Navigate(new UProfile());
            IsProfileSelected = true; // Начальное состояние для профиля
        }

        private void Goods_Click(object sender, RoutedEventArgs e)
        {
            IsGoodsSelected = true;
            IsProfileSelected = false;
            IsLogoutSelected = false;
            MainFrame.Navigate(new USuplies());
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            IsProfileSelected = true;
            IsGoodsSelected = false;
            IsLogoutSelected = false;
            MainFrame.Navigate(new UProfile());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            IsLogoutSelected = true;
            IsProfileSelected = false;
            IsGoodsSelected = false;
            NavigationHelper.NavigateToMainWindow(this);
        }
    }
}