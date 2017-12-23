using LibraryManager.Data.Item;
using System.Windows.Controls;

namespace LibraryManager.Controls
{
    /// <summary>
    /// Interaction logic for ItemDisplay.xaml
    /// </summary>
    public partial class ItemDisplay : UserControl
    {
        public ItemDisplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Selects the current data context as an item in the main VM (which delegates it to ItemsView)
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="e">Ignored</param>
        private void Hyperlink_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindowViewModel.Instance.SelectItem(this.DataContext as IssuableItem);
        }
    }
}
