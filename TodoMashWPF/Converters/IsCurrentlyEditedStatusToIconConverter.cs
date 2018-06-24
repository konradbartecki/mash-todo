using FontAwesome.WPF.Converters;
using System;
using System.Globalization;
using System.Windows.Data;

namespace TodoMashWPF.Converters
{
    public class IsCurrentlyEditedStatusToIconConverter : IValueConverter
    {
        private CssClassNameConverter fontAwesomeConverter = new CssClassNameConverter()
        {
            Mode = CssClassConverterMode.FromStringToIcon
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                if (b == true)
                {
                    return fontAwesomeConverter.Convert("floppy-o", targetType, parameter, culture);
                }
                else
                {
                    return fontAwesomeConverter.Convert("pencil", targetType, parameter, culture);
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}