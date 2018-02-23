using LibraryManager.Data.Action;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManager.Converters
{
    public class MemberNullTemplateSelector : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object obj, System.Windows.DependencyObject container)
        {
            if (obj != null && obj is LoggedAction)
            {
                if ((obj as LoggedAction).Member == null) return Null;
                return NonNull;
            }
            else return null;
        }
        public DataTemplate NonNull { get; set; }
        public DataTemplate Null { get; set; }
    }
}
