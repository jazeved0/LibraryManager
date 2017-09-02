using LibraryManager.ViewModels;
using System.Windows.Controls;

namespace LibraryManager.Views
{
    /// <summary>
    /// Interaction logic for ConfigView.xaml
    /// </summary>
    public partial class ConfigView : UserControl
    {
        public ConfigView()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            InitializeComponent();
            this.DataContext = new ConfigViewModel();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            (this.DataContext as ConfigViewModel).CommitChanges();
        }

        private void NumericUpDown_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double?> e)
        {
            (this.DataContext as ConfigViewModel).UpdateEnable();
        }

        private void NumericUpDown_ValueChanged_1(object sender, System.Windows.RoutedPropertyChangedEventArgs<double?> e)
        {
            (this.DataContext as ConfigViewModel).UpdateEnable();
        }

        private void NumericUpDown_ValueChanged_2(object sender, System.Windows.RoutedPropertyChangedEventArgs<double?> e)
        {
            (this.DataContext as ConfigViewModel).UpdateEnable();
        }

        private void NumericUpDown_ValueChanged_3(object sender, System.Windows.RoutedPropertyChangedEventArgs<double?> e)
        {
            (this.DataContext as ConfigViewModel).UpdateEnable();
        }

        private void NumericUpDown_ValueChanged_4(object sender, System.Windows.RoutedPropertyChangedEventArgs<double?> e)
        {
            (this.DataContext as ConfigViewModel).UpdateEnable();
        }

        private void NumericUpDown_ValueChanged_5(object sender, System.Windows.RoutedPropertyChangedEventArgs<double?> e)
        {
            (this.DataContext as ConfigViewModel).UpdateEnable();
        }
    }
}
