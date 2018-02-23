using LibraryManager.Data.Action;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LibraryManager.Converters
{
    public class ActionTypeToColorConverter : IValueConverter
    {
        // Each color code:
        private static readonly Color ADDITION = Color.FromRgb(106, 29, 154); // Purple
        private static readonly Color ISSUANCE = Color.FromRgb(29, 68, 154); // Blue
        private static readonly Color RESERVATION = Color.FromRgb(20, 111, 49); // Green
        private static readonly Color RETURN = Color.FromRgb(154, 46, 29); // Maroon

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LoggedAction)
                // Create a new solid brush for the action
                return new SolidColorBrush(GetColor((value as LoggedAction).Type));
            else return Brushes.Black; // Default to black
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Does not support TwoWay binding
            throw new NotImplementedException();
        }

        /// <summary>
        /// Helper method that returns the correct color for the inputted type
        /// </summary>
        private Color GetColor(ActionType type)
        {
            if (type == ActionType.Addition) return ADDITION;
            else if (type == ActionType.Issuance) return ISSUANCE;
            else if (type == ActionType.Reservation) return RESERVATION;
            else if (type == ActionType.Return) return RETURN;
            else return Color.FromRgb(0, 0, 0); // Default to black
        }
    }
}
