using LibraryManager.Data.Item;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibraryManager.Data.Member
{
    public class Member : INotifyPropertyChanged
    {
        private string _id;
        private string _name;
        private ObservableCollection<IssuableItem> _items;
        private MemberType _type;

        public string ID
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public MemberType Type
        {
            get { return _type; }
            set
            {
                if (value == _type) return;
                _type = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<IssuableItem> Items
        {
            get { return _items; }
            internal set
            {
                _items = value;
                if(value != null)
                {
                    foreach (IssuableItem item in value)
                    {
                        item.Owner = this;
                    }
                }
            }
        }

        public decimal Fee
        {
            get { return 0.00m; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public Member()
        {
            _items = new ObservableCollection<IssuableItem>();
            _items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
                foreach(object obj in e.NewItems)
                {
                    (obj as IssuableItem).Owner = this;
                }
            }
        }
    }
}