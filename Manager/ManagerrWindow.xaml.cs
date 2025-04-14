using System;
using System.Windows;
using System.Windows.Controls;
using Castle.UserFolder;

namespace Castle
{
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UProfile());
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new USuplies());
        }
    }
}