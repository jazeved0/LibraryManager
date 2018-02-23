using LibraryManager.ViewModels;
using LibraryManager.ViewModels.Dialogs;
using MahApps.Metro.SimpleChildWindow;

namespace LibraryManager.Dialogs
{
    /// <summary>
    /// Interaction logic for GenerateFeeReportDialog.xaml
    /// </summary>
    public partial class GenerateFeeReportDialog : ChildWindow
    {
        public GenerateFeeReportDialog()
        {
            DataContext = new GenerateFeeReportDialogViewModel();
            InitializeComponent();

            Loaded += (s, e) =>
            {
                if (DataContext is ICloseable)
                {
                    (DataContext as ICloseable).RequestClose += (_, __) => Close();
                }
            };
        }

        private void CancelButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void GoBackButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ((GenerateFeeReportDialogViewModel)DataContext).GoBack();
        }

        private void ProceedButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ((GenerateFeeReportDialogViewModel)DataContext).Proceed();
        }
    }
}