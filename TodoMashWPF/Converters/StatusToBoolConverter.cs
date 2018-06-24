using MashTodo.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace TodoMashWPF.Model
{
    public class StatusToIsCompletedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TodoStatus status && status == TodoStatus.Completed)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                if (b == true)
                    return TodoStatus.Completed;
                else if (b == false)
                    return TodoStatus.Open;
            }
            return TodoStatus.Unknown;
        }
    }
}