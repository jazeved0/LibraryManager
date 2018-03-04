using LibraryManager.Data.Item;
using LibraryManager.Data.Item.Status;
using LibraryManager.Data.Member;
using MahApps.Metro;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManager.ViewModels
{
    class MainWindowViewModel : NotifyPropertyChanged
    {
        public static MainWindowViewModel Instance;

        private ItemsViewModel _itemsVM;
        private MembersViewModel _membersVM;
        private HistoryViewModel _historyVM;
        private int _openTab;
        private bool _isRefreshing;
        private DateTime _lastRefresh = DateTime.Now;
        private CancellationTokenSource _cancelBackgroundTasks;
        private IDialogCoordinator _dialogCoordinator;

        public MainWindowViewModel(IDialogCoordinator instance, ILibraryDataProvider dataProvider)
        {
            var accent = ThemeManager.Accents.First(x => x.Name == "Cobalt");
            var theme = ThemeManager.AppThemes.First(x => x.Name == "BaseLight");
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme);

            Instance = this;
            _dialogCoordinator = instance;

            DataProvider = dataProvider;
            _historyVM = new HistoryViewModel(DataProvider.History);
            DataProvider.Initialize();
            _itemsVM = new ItemsViewModel(DataProvider.Items);
            _membersVM = new MembersViewModel(DataProvider.Members);

            DataProvider.LoadConfigurationData(App.Instance.Config);

            // Begin auto refresh cycle
            _cancelBackgroundTasks = new CancellationTokenSource();
            Task.Run(() => App.RunPeriodicAsync(Refresh, new TimeSpan(0, 1, 0), new TimeSpan(0, 2, 0), _cancelBackgroundTasks.Token));

            // Register collection changed listeners
            _itemsVM.Items.CollectionChanged += Items_CollectionChanged;
            _membersVM.Members.CollectionChanged += Members_CollectionChanged;
        }

        public ILibraryDataProvider DataProvider { get; private set; }

        public ItemsViewModel ItemsVM
        {
            get { return _itemsVM; }
        }

        public MembersViewModel MembersVM
        {
            get { return _membersVM; }
        }

        public HistoryViewModel HistoryVM
        {
            get { return _historyVM; }
        }

        public int OpenTab
        {
            get { return _openTab; }
            set
            {
                if (value == _openTab) return;
                _openTab = value;
                OnPropertyChanged();
            }
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            private set
            {
                if (_isRefreshing == value) return;
                _isRefreshing = value;
                OnPropertyChanged();
                ForcePropertyChanged("RefreshInfoText");
                ForcePropertyChanged("Status");
                ForcePropertyChanged("CanRefresh");
            }
        }

        public bool CanRefresh
        {
            get { return !_isRefreshing; }
        }

        public string Status
        {
            get { return IsRefreshing ? "Refreshing..." : "Ready"; }
        }

        public string RefreshInfoText
        {
            get { return IsRefreshing ? "" : String.Format("Last refreshed at {0}", _lastRefresh.ToShortTimeString()); }
        }

        private void Members_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (object obj in e.OldItems)
                {
                    if (obj is Member) ReleaseDependencies(obj as Member);
                }
            }
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (object obj in e.OldItems)
                {
                    if (obj is IssuableItem) ReleaseDependencies(obj as IssuableItem);
                }
            } else if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (object obj in e.NewItems)
                {
                    HistoryVM.History.Add(new Data.Action.LoggedAction() { Item = obj as IssuableItem, Member = null, Type = Data.Action.ActionType.Addition, Timestamp = DateTime.Now });
                }
            }
        }

        public void ReleaseDependencies(IssuableItem item)
        {
            // Return for the owner
            switch (item.Status.Type)
            {
                case ItemStatus.StatusType.Issued:
                case ItemStatus.StatusType.Overdue: item.Status.CheckIn(); break;
                case ItemStatus.StatusType.Reserved: item.Status.ReleaseReservation(); break;
                default: break; // Do nothing
            }
        }

        public void ReleaseDependencies(Member member)
        {
            // Return all items
            while (member.Items.Count > 0) ReleaseDependencies(member.Items[0]);
        }

        public void CancelBackgroundTasks()
        {
            _cancelBackgroundTasks.Cancel();
        }

        public void SelectItem(IssuableItem item)
        {
            OpenTab = 1;
            ItemsVM.SelectItem.OnSelectItem(item);
        }

        public void SelectMember(Member member)
        {
            OpenTab = 2;
            MembersVM.SelectItem.OnSelectItem(member);
        }

        public async void Refresh()
        {
            IsRefreshing = true;
            await RefreshBackground();
            _lastRefresh = DateTime.Now;
            IsRefreshing = false;
        }

        public async Task RefreshBackground()
        {
            await Task.Run(() => DataProvider.Update());
        }

        public void ShowDeleteConfirmDialogAsync<T>(String message, T toDelete)
        {
            var x = _dialogCoordinator?.ShowMessageAsync(this, "Delete?", message, MessageDialogStyle.AffirmativeAndNegative).ContinueWith(t =>
            {
                if (t.Result == MessageDialogResult.Negative) return;
                Action del = () => { };
                if (typeof(T) == typeof(Member))
                {
                    del = () => { MembersVM.Members.Remove(toDelete as Member); };
                }
                else if (typeof(T) == typeof(IssuableItem))
                {
                    del = () => { ItemsVM.Items.Remove(toDelete as IssuableItem); };
                }

                // Run deletion code;
                App.Current.Dispatcher.Invoke(del);
            });
        }
    }
}
