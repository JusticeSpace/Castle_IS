using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace Castle.UserFolder
{
    public partial class SupplierDetails : UserControl
    {
        public SupplierDetails()
        {
            InitializeComponent();
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}