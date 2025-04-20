using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace Castle.Administrator
{
    public partial class ULogs : Page
    {
        private readonly Amo_CastleEntities1 _context = new Amo_CastleEntities1();

        public ULogs()
        {
            InitializeComponent();
            LoadLogs();
        }

        private void LoadLogs()
        {
            try
            {
                var logs = _context.Logs
                    .Include(l => l.User) // Подгружаем данные пользователя
                    .OrderByDescending(l => l.Date)
                    .ToList();
                DGLogs.ItemsSource = logs;

                // Проверяем, загружаются ли данные пользователя
                if (logs.Any() && logs.First().User == null)
                {
                    MessageBox.Show("Не удалось загрузить данные пользователя для логов. Проверьте связь с таблицей User.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки логов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadLogs();
                MessageBox.Show("Данные обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Amo_CastleEntities1())
            {
                try
                {
                    foreach (var log in DGLogs.Items.Cast<Logs>())
                    {
                        var entry = context.Logs.Find(log.Id);
                        if (entry != null)
                        {
                            entry.Details = log.Details;
                        }
                    }
                    context.SaveChanges();
                    MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    var errorMessages = dbEx.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                    var fullErrorMessage = string.Join("\n", errorMessages);
                    MessageBox.Show($"Ошибка валидации:\n{fullErrorMessage}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения изменений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}