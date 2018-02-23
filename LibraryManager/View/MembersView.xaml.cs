using LibraryManager.Data.Member;
using LibraryManager.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                if (vis is DataGridRow row)
                {
                    row.DetailsVisibility =
                    row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }

        private void dgMembers_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (sender is DataGrid dg)
            {
                DataGridRow dgRow = (DataGridRow)(dg.ItemContainerGenerator.ContainerFromIndex(dg.SelectedIndex));
                if (e.Key == Key.Delete && !dgRow.IsEditing)
                {
                    // User is attempting to delete the row
                    Member memberToDelete = dg.SelectedItem as Member;
                    if(memberToDelete?.Items.Count > 0)
                    {
                        // Display confirmation dialog
                        MainWindowViewModel.Instance.ShowDeleteConfirmDialogAsync("The member you are attempting to remove has items associated with them. Continue?", memberToDelete);
                        // Defer deletion upon confirmation to MainWindowViewModel
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
