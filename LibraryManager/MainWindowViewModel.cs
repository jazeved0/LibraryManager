using LibraryManager.Data.Item;
using LibraryManager.Data.Member;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace LibraryManager
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<Member> _members;
        private readonly ObservableCollection<IssuableItem> _items;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Member> Members
        {
            get { return _members; }
        }

        public ObservableCollection<IssuableItem> Items
        {
            get { return _items; }
        }

        public MainWindowViewModel()
        {
            SampleData.Seed();
            _members = SampleData.Members;
            _items = SampleData.Items;

            _members.CollectionChanged += Members_CollectionChanged;
            _items.CollectionChanged += Items_CollectionChanged;
        }

        private void Members_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Members"));
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Items"));
        }
    }
}
