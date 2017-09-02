using LibraryManager.Data.Item;
using LibraryManager.Data.Member;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LibraryManager.Converters
{
    class ShowNameOrDefault : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter is String && (parameter as String).Equals("text"))
            {
                if (value == null) return "";
                if (value.ToString() == "{DataGrid.NewItemPlaceholder}")
                    return "Add new row...";
                else
                {
                    if (value is Member) return ((Member)value).Name;
                    else if (value is IssuableItem) return ((IssuableItem)value).Name;
                    else return "Name";
                }
            }

            if (parameter is String && (parameter as String).Equals("fontStyle"))
            {
                if (value == null) return "";
                if (value.ToString() == "{DataGrid.NewItemPlaceholder}")
                    return FontStyles.Italic;
                else
                    return FontStyles.Normal;
            }

            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception();
        }
    }
}
