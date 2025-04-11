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

namespace Castle.Manager
{
    /// <summary>
    /// Логика взаимодействия для ManagerrWindow.xaml
    /// </summary>
    public partial class ManagerrWindow : Window
    {
        public ManagerrWindow()
        {
            InitializeComponent();
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateToMainWindow(this);
        }
    }
}
