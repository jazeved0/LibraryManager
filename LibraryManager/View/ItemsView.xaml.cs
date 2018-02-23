using LibraryManager.Data.Item;
using LibraryManager.ViewModels;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LibraryManager.View
{
    /// <summary>
    /// Interaction logic for ItemsView.xaml
    /// </summary>
    public partial class ItemsView : UserControl
    {
        public ItemsView()
        {
            InitializeComponent();
            MainWindowViewModel.Instance.ItemsVM.SelectItem.SelectItem += SelectItem;
        }

        private void SelectItem(object sender, Messenger.SelectItemMessenger<IssuableItem>.SelectItemEventArgs e)
        {
            dgItems.SelectedItem = e.Item as IssuableItem;
        }

        private void ExpandCollapseDetails(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow row)
                {
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }

        private void dgItems_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (sender is DataGrid dg)
            {
                DataGridRow dgRow = (DataGridRow)(dg.ItemContainerGenerator.ContainerFromIndex(dg.SelectedIndex));
                if (e.Key == Key.Delete && !dgRow.IsEditing)
                {
                    // User is attempting to delete the row
                    IssuableItem itemToDelete = dg.SelectedItem as IssuableItem;
                    if (itemToDelete?.Status.HasOwner ?? false)
                    {
                        // Display confirmation dialog
                        MainWindowViewModel.Instance.ShowDeleteConfirmDialogAsync("The item you are attempting to delete has an owner associated with it. Continue?", itemToDelete);
                        // Defer deletion upon confirmation to MainWindowViewModel
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
