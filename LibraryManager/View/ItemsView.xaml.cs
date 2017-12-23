using LibraryManager.Data.Item;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }

        private void dgItems_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //if(e.Column.Header.Equals("Title") && e.EditAction == DataGridEditAction.Commit && e.Row.Item == CollectionView.NewItemPlaceholder)
        }

        private void dgItems_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var x = 0;
            //if (!(e.Row.Item is IssuableItem))
            //{
            //    // Placeholder
            //    var textBlock = App.FindChild(e.Row, x =>
            //    {
            //        return x is TextBlock;
            //    }) as TextBlock;
            //    textBlock.Text = "Add new row...";
            //    textBlock.Foreground = System.Windows.Media.Brushes.Gray;
            //}
        }

        private void dgItems_Loaded(object sender, RoutedEventArgs e)
        {
            var x = 0;
            //var dataGridRow = App.FindChild(dgItems, x =>
            //{
            //    var element = x as DataGridRow;
            //    if (element != null && element.Item == System.Windows.Data.CollectionView.NewItemPlaceholder)
            //        return true;
            //    else
            //        return false;
            //}) as DataGridRow;
            //var textBlock = App.FindChild(dataGridRow, x =>
            //{
            //    return x is TextBlock;
            //}) as TextBlock;
            //textBlock.Text = "Add new row...";
            //textBlock.Foreground = System.Windows.Media.Brushes.Gray;
        }
    }
}
