using LibraryManager.Data.Item.Status;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LibraryManager.Converters
{
    /// <summary>
    /// Formats an ItemStatus by showing its type and its remainder
    /// </summary>
    public class StatusFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is ItemStatus)
            {
                ItemStatus status = value as ItemStatus;

                // If the status has relevant remainder text
                if (status.Type == ItemStatus.StatusType.Issued || status.Type == ItemStatus.StatusType.Reserved)
                {
                    String prefix;

                    // If the parameter specifies no prefix on the formatted text:
                    if (parameter != null && parameter is string && (parameter as string).Equals("no_msg"))
                    {
                        // Use no prefix
                        prefix = "";
                    }
                    else
                    {
                        // Otherwise, generate the prefix by using the StatusType as a verb
                        prefix = status.Type + " for: ";
                    }

                    TimeSpan remainder = status.Remainder;

                    // Depending on the magnitude of the remainder TimeSpan, print either the total minutes, hours, or days
                    if (remainder.TotalMinutes < 60) return prefix + Round(remainder.TotalMinutes).ToString() + " minutes";
                    else if (remainder.TotalHours < 60) return prefix + Round(remainder.TotalHours).ToString() + " hours";
                    else return prefix + Round(remainder.TotalDays).ToString() + " days";
                }
                // If not, simply return the StatusType label
                else return status.Type;
            }

            // If the conversion failed, return the original value
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Does not support TwoWay binding
            throw new NotImplementedException();
        }

        /// <summary>
        /// Helper method to round a double into an int
        /// </summary>
        /// <param name="toRound">A double floating-point number to be rounded</param>
        /// <returns>The closest integer to the specified double</returns>
        private int Round(double toRound)
        {
            return (int) Math.Round(toRound);
        }
    }
}
