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

namespace Castle.Administrator
{
    /// <summary>
    /// Логика взаимодействия для AdWindow.xaml
    /// </summary>
    public partial class AdWindow : Window
    {
        public AdWindow()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationHelper.NavigateToMainWindow(this);
        }
    }
}
