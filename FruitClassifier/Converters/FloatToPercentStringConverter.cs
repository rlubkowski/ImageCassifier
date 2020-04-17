using System;
using System.Globalization;
using System.Windows.Data;

namespace FruitClassifier.Converters
{
    public class FloatToPercentStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float)
                return ((float)value).ToString("0.##%");
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
                return float.Parse("0.##%");
            return null;
        }
    }
}
