using LibraryManager.Data.Action;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManager.Converters
{
    public class ActionTypeTemplateSelector : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object obj, System.Windows.DependencyObject container)
        {
            if (obj != null && obj is LoggedAction)
            {
                switch((obj as LoggedAction).Type)
                {
                    case (ActionType.Issuance): return Issuance;
                    case (ActionType.Reservation): return Reservation;
                    case (ActionType.Return): return Return;
                    default: return Addition;
                }
            }
            else return null;
        }
        public DataTemplate Issuance { get; set; }
        public DataTemplate Reservation { get; set; }
        public DataTemplate Return { get; set; }
        public DataTemplate Addition { get; set; }
    }
}
