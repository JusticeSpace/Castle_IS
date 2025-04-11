using Castle;
using System.Windows;

namespace YourNamespace
{
    public static class NavigationHelper
    {
        // Метод для перехода на главное окно
        public static void NavigateToMainWindow(Window currentWindow)
        {
            // Создаем экземпляр главного окна
            MainWindow mainWindow = new MainWindow();

            // Отображаем главное окно
            mainWindow.Show();

            // Закрываем текущее окно
            currentWindow.Close();
        }
    }
}