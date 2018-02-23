using LibraryManager.ViewModels;
using LibraryManager.ViewModels.Dialogs;
using MahApps.Metro.SimpleChildWindow;

namespace LibraryManager.Dialogs
{
    /// <summary>
    /// Interaction logic for GenerateIssuanceReportDialog.xaml
    /// </summary>
    public partial class GenerateIssuanceReportDialog : ChildWindow
    {
        public GenerateIssuanceReportDialog()
        {
            DataContext = new GenerateIssuanceReportDialogViewModel();
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
            ((GenerateIssuanceReportDialogViewModel)DataContext).GoBack();
        }

        private void ProceedButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ((GenerateIssuanceReportDialogViewModel)DataContext).Proceed();
        }
    }
}