using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Castle.SomeCode.Converter
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visibleWhenNull = parameter?.ToString() == "VisibleWhenNull";
            bool isNull = value == null;
            return (isNull == visibleWhenNull) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}