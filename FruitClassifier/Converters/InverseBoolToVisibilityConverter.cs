using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FruitClassifier.Converters
{
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return null;
            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals(value, Visibility.Visible))
                return false;
            if (Equals(value, Visibility.Collapsed))
                return true;
            return null;
        }
    }
}
