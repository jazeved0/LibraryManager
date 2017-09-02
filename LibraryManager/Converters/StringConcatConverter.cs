using System;
using System.Globalization;
using System.Windows.Data;

namespace LibraryManager.Converters
{
    class StringConcatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is String)
            {
                return ConvertCore(value, targetType, parameter, culture);
            }

            Type type = value.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private String ConvertCore(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as String) + (parameter as String);
        }
    }
}
