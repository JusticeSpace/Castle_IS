using System;
using System.Globalization;
using System.Windows.Data;

namespace Castle
{
    public class PriceConverter : IValueConverter
    {
        // Преобразование значения для отображения (добавляем ₽)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal price)
            {
                return $"{price.ToString("N2", culture)} ₽";
            }
            return value?.ToString();
        }

        // Преобразование значения обратно при редактировании (убираем ₽)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                // Убираем пробелы и символ рубля
                string cleanedValue = stringValue.Replace("₽", "").Replace(" ", "").Trim();
                if (decimal.TryParse(cleanedValue, NumberStyles.Any, culture, out decimal result))
                {
                    return result;
                }
            }
            return Binding.DoNothing; // Если преобразование не удалось, не меняем значение
        }
    }
}