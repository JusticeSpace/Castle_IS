using System.Windows;

namespace Castle
{
    public partial class App : Application
    {
        public static int CurrentUserId { get; set; }

        public App()
        {
            // Инициализация, если нужно
            CurrentUserId = -1; // Значение по умолчанию, например, -1 для "не авторизован"
        }
    }
}