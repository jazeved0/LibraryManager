using System.Collections.ObjectModel;
using System.Collections.Specialized;
using LibraryManager.Data.Action;

namespace LibraryManager.ViewModels
{
    class HistoryViewModel : NotifyPropertyChanged
    {
        private readonly ObservableCollection<LoggedAction> _history;

        public ObservableCollection<LoggedAction> History
        {
            get { return _history; }
        }

        public HistoryViewModel(ObservableCollection<LoggedAction> history)
        {
            _history = history;
            History.CollectionChanged += delegate (object s, NotifyCollectionChangedEventArgs e) { ForcePropertyChanged("History"); };
        }
    }
}
