using LibraryManager.Data.Member;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LibraryManager.View
{
    /// <summary>
    /// Interaction logic for MembersView.xaml
    /// </summary>
    public partial class MembersView : UserControl
    {
        public MembersView()
        {
            InitializeComponent();
            MainWindowViewModel.Instance.MembersVM.SelectItem.SelectItem += SelectMember;
        }

        private void SelectMember(object sender, Messenger.SelectItemMessenger<Member>.SelectItemEventArgs e)
        {
            dgMembers.SelectedItem = e.Item as Member;
        }

        void ExpandCollapseDetails(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    row.DetailsVisibility =
                    row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }
    }
}
