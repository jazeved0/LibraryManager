using System;
using System.Globalization;
using System.Windows.Data;

namespace LibraryManager.Converters
{
    class IsNewRowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the value wasn't supplied, the conversion cannot continue
            if (value == null) return null;

            if (value.GetType().Name == "NamedObject")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Does not support TwoWay binding
            throw new NotImplementedException();
        }
    }
}