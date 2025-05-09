﻿using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;


namespace Castle.SomeCode.Converter
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] byteArray && byteArray.Length > 0)
            {
                try
                {
                    using (var stream = new MemoryStream(byteArray))
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = stream;
                        image.EndInit();
                        return image;
                    }
                }
                catch
                {
                    return null; // Вернет пустое место вместо ошибки
                }
            }
            return null; // Нет фото — пустое место
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

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