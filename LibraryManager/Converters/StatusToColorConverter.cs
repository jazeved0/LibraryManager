using LibraryManager.Data.Item;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LibraryManager.Converters
{
    class StatusToColorConverter : IValueConverter
    {
        private static Color SHELVED = Color.FromRgb(0, 0, 0);
        private static Color RESERVED = Color.FromRgb(20, 111, 49);
        private static Color ISSUED = Color.FromRgb(29, 35, 154);
        private static Color OVERDUE = Color.FromRgb(154, 46, 29);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ItemStatus)
                return new SolidColorBrush(GetColor((ItemStatus)value));
            else return SHELVED;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Color GetColor(ItemStatus status)
        {
            if (status.Type == ItemStatus.StatusType.Shelved) return SHELVED;
            if (status.Type == ItemStatus.StatusType.Reserved) return RESERVED;
            if (status.Type == ItemStatus.StatusType.Issued) return ISSUED;
            if (status.Type == ItemStatus.StatusType.Overdue) return OVERDUE;
            return SHELVED;
        }
    }
}
