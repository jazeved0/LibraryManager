using LibraryManager.Data.Action;
using LibraryManager.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManager.View
{
    /// <summary>
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        public HistoryView()
        {
            InitializeComponent();
        }

        private void ItemLinkClick(object sender, RoutedEventArgs e)
        {
            LoggedAction action = ((Button)sender).CommandParameter as LoggedAction;
            // Link to action.Item
            if (action?.Item != null) MainWindowViewModel.Instance.SelectItem(action.Item);
        }

        private void MemberLinkClick(object sender, RoutedEventArgs e)
        {
            LoggedAction action = ((Button)sender).CommandParameter as LoggedAction;
            // Link to action.Member
            if(action?.Member != null) MainWindowViewModel.Instance.SelectMember(action.Member);
        }
    }
}
