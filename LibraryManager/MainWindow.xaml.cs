using LibraryManager.Dialogs;
using LibraryManager.ViewModels;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.SimpleChildWindow;
using System.Windows.Input;

namespace LibraryManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel(DialogCoordinator.Instance, new SampleData());
            InitializeComponent();
            SchoolLabel.Content = App.SCHOOL_NAME;
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private async void IssueItemClickAsync(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.ShowChildWindowAsync(new IssueItemDialog() { IsModal = true });
        }

        private async void ReturnItemClickAsync(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.ShowChildWindowAsync(new ReturnItemDialog() { IsModal = true });
        }

        private async void ReserveItemClickAsync(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.ShowChildWindowAsync(new ReserveItemDialog() { IsModal = true });
        }

        private async void DuplicateItemClickAsync(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.ShowChildWindowAsync(new DuplicateItemDialog() { IsModal = true });
        }

        private async void GenerateIssuanceReportClickAsync(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.ShowChildWindowAsync(new GenerateIssuanceReportDialog() { IsModal = true });
        }

        private async void GenerateFeeReportClickAsync(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.ShowChildWindowAsync(new GenerateFeeReportDialog() { IsModal = true });
        }

        private void RefreshClick(object sender, System.Windows.RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).Refresh();
        }
    }
}
