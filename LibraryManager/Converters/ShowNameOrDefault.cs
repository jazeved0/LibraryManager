using LibraryManager.Data.Item;
using LibraryManager.Data.Member;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LibraryManager.Converters
{
    /// <summary>
    /// Formats the given data object's name by displaying 'Add new row' if the object is actually a DataGrid placeholder object
    /// This is the case with the final row of WPF DataGrids that allow new row creation
    /// </summary>
    public class ShowNameOrDefault : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is String)
            {
                String param = (parameter as String).Trim();
                // If the conversion is for text formatting,
                if (param.Equals("text"))
                {
                    if (value == null) return "";
                    if (value.ToString() == "{DataGrid.NewItemPlaceholder}") return "Add new row..."; // If the row is a placeholder, return stock text
                    else
                    {
                        if (value is Member) return ((Member)value).Name; // If the value is a member, return its Name
                        else if (value is IssuableItem) return ((IssuableItem)value).Title; // If the value is an item, return its Title
                    }
                }
                // If the conversion is for font style formatting,
                else if (param.Equals("fontStyle"))
                {
                    if (value.ToString() == "{DataGrid.NewItemPlaceholder}") return FontStyles.Italic; // If the row is a placeholder, return Italics
                    else return FontStyles.Normal; // Otherwise, return Normal text
                }
            }

            // If the conversion was setup incorrectly, return the original value
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Does not support TwoWay binding
            throw new Exception();
        }
    }
}
