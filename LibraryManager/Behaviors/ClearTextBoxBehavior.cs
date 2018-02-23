using System.Windows;
using System.Windows.Controls;

namespace LibraryManager.Behaviors
{
    public static class ClearTextBoxBehavior
    {
        public static bool GetClearTextBox(TextBox target)
        {
            return (bool)target.GetValue(ClearTextBoxAttachedProperty);
        }

        public static void SetClearTextBox(TextBox target, bool value)
        {
            target.SetValue(ClearTextBoxAttachedProperty, value);
        }

        public static readonly DependencyProperty ClearTextBoxAttachedProperty = DependencyProperty.RegisterAttached("ClearTextBox", typeof(bool), typeof(ClearTextBoxBehavior), new UIPropertyMetadata(false, OnClearTextBoxAttachedPropertyChanged));

        static void OnClearTextBoxAttachedPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((TextBox)o).Clear();
        }
    }
}
