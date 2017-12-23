using LibraryManager.Data.Item.Status;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManager.Controls
{
    /// <summary>
    /// Interaction logic for ItemStatusDisplay.xaml
    /// </summary>
    public partial class ItemStatusDisplay : UserControl
    {
        public ItemStatusDisplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Selects the current data context's owner as a Member in the main VM (which delegates it to MembersView)
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="e">Ignored</param>
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ItemStatus status = this.DataContext as ItemStatus;
            if(status.HasOwner) MainWindowViewModel.Instance.SelectMember(status.Owner);
        }
    }
}
