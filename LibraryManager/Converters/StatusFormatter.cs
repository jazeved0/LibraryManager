using LibraryManager.Data.Item;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LibraryManager.Converters
{
    class StatusFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is ItemStatus)
            {
                ItemStatus status = value as ItemStatus;
                if (status.Type == ItemStatus.StatusType.Reserved) return "Reserved for " + status.GetRemainder();
                if (status.Type == ItemStatus.StatusType.Issued) return "Issued for " + status.GetRemainder();
                return status.Type;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
