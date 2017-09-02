using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibraryManager.Data.Item
{
    public class IssuableItem : INotifyPropertyChanged
    {
        private String _id;
        private String _name;
        private String _author;
        private Member.Member _owner;
        private ItemStatus _status;
        private ItemType _type;

        public String Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public String Author
        {
            get { return _author; }
            set
            {
                if (value == _author) return;
                _author = value;
                OnPropertyChanged();
            }
        }

        public ItemType Type
        {
            get { return _type; }
            set
            {
                if (value == _type) return;
                _type = value;
                OnPropertyChanged();
            }
        }

        public String ID
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public Member.Member Owner
        {
            get { return _owner; }
            set
            {
                if (value == _owner) return;
                _owner = value;
                OnPropertyChanged();
            }
        }

        public ItemStatus Status
        {
            get { return _status; }
            set
            {
                if (value.Equals(_status)) return;
                _status = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void StatusChanged(Object sender, PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs("Status"));
        }

        public IssuableItem()
        {
            _status = new ItemStatus(this);
            _status.PropertyChanged += StatusChanged;
        }

        public IssuableItem Issue()
        {
            this.Status.Type = ItemStatus.StatusType.Issued;
            this.Status.ContextualDate = System.DateTime.Now;
            return this;
        }

        public IssuableItem Reserve()
        {
            this.Status.Type = ItemStatus.StatusType.Reserved;
            this.Status.ContextualDate = System.DateTime.Now;
            return this;
        }
    }
}
