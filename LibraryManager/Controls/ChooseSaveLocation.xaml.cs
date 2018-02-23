using Microsoft.Win32;
using System;
using System.Windows.Controls;

namespace LibraryManager.Controls
{
    /// <summary>
    /// Interaction logic for ChooseSaveLocation.xaml
    /// </summary>
    public partial class ChooseSaveLocation : UserControl
    {
        public ChooseSaveLocation()
        {
            InitializeComponent();
        }

        private void BrowseButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveFileDialog sfg = new SaveFileDialog
            {
                Filter = "PDF File (*.pdf)|*.pdf",
                Title = "Choose location",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if(sfg.ShowDialog() == true)
            {
                FilePathTextBox.Text = sfg.FileName;
            }
        }
    }
}
