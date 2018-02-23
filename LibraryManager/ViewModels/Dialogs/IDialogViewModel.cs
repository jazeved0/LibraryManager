using System.ComponentModel;

namespace LibraryManager.ViewModels.Dialogs
{
    interface IDialogViewModel : INotifyPropertyChanged
    {
        void ForcePropertyChanged(string propertyName = null);
    }
}
