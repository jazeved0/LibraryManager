using System.ComponentModel;

namespace LibraryManager.ViewModels.Pages
{
    interface IPageViewModel : INotifyPropertyChanged
    {
        bool CanProceed { get; }
        string Title { get; }
    }
}
