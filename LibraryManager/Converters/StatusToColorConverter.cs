using LibraryManager.Data.Item.Status;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LibraryManager.Converters
{
    /// <summary>
    /// Converts from a ItemStatus to a corresponding color
    /// </summary>
    public class StatusToColorConverter : IValueConverter
    {
        // Each color code:
        private static readonly Color SHELVED = Color.FromRgb(0, 0, 0); // Black
        private static readonly Color RESERVED = Color.FromRgb(20, 111, 49); // Dark green
        private static readonly Color ISSUED = Color.FromRgb(29, 35, 154); // Dark blue
        private static readonly Color OVERDUE = Color.FromRgb(154, 46, 29); // Dark maroon

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ItemStatus)
                // Create a new solid brush for the status
                return new SolidColorBrush(GetColor((ItemStatus)value));
            // If the value is not a valid status, return Black
            else return Brushes.Black; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Does not support TwoWay binding
            throw new NotImplementedException();
        }

        /// <summary>
        /// Helper method that returns the correct color for the inputted status
        /// </summary>
        private Color GetColor(ItemStatus status)
        {
            if (status.Type == ItemStatus.StatusType.Reserved) return RESERVED;
            if (status.Type == ItemStatus.StatusType.Issued) return ISSUED;
            if (status.Type == ItemStatus.StatusType.Overdue) return OVERDUE;
            else return SHELVED; // Default to black
        }
    }
}
