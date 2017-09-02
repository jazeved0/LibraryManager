using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LibraryManager.Converters
{
    class ColorFadeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            if (value is Color)
            {
                return ConvertCore(value, targetType, parameter, culture);
            }
            else if (value is SolidColorBrush)
            {
                return new SolidColorBrush(ConvertCore(((SolidColorBrush)value).Color, targetType, parameter, culture));
            }

            Type type = value.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Color ConvertCore(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color c = (Color)value;
            float ratio = System.Convert.ToSingle(parameter);
            c.A = (byte)((float)c.A * ratio);
            return c;
        }
    }
}
