using System;
using System.Globalization;
using System.Windows.Data;

namespace TodoMashWPF.Converters
{
    public class DateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTimeOffset dto)
            {
                return dto.LocalDateTime.ToString("dd MMM HH:mm:ss");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}