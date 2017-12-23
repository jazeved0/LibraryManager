using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LibraryManager.Converters
{
    /// <summary>
    /// Fades the provided color/colorBrush by multiplying its alpha with the supplied parameter
    /// </summary>
    public class ColorFadeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If either the value or alpha parameter weren't supplied, the conversion cannot continue
            if (value == null || parameter == null) return null;

            if (value is Color)
            {
                // Return the faded color
                return ConvertCore(value, parameter);
            }
            else if (value is SolidColorBrush)
            {
                // Return the faded solid color brush
                return new SolidColorBrush(ConvertCore(((SolidColorBrush)value).Color, parameter));
            }
            else
            {
                // Invalid input type
                Type type = value.GetType();
                throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Does not support TwoWay binding
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs the actual conversion by multiplying the value as a color's alpha by the parameter as a float
        /// </summary>
        /// <param name="value">A Color object to be faded</param>
        /// <param name="parameter">A floating-point number representing a multiplier for the color's alpha</param>
        /// <returns>A new Color object with a faded alpha</returns>
        private Color ConvertCore(object value, object parameter)
        {
            Color c = (Color)value;
            float ratio = System.Convert.ToSingle(parameter);
            c.A = (byte)((float)c.A * ratio);
            return c;
        }
    }
}
