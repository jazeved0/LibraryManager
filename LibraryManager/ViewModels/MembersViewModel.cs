using LibraryManager.Data.Member;
using LibraryManager.Messenger;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace LibraryManager.ViewModels
{
    class MembersViewModel : NotifyPropertyChanged
    {
        private readonly ObservableCollection<Member> _members;

        public ObservableCollection<Member> Members
        {
            get { return _members; }
        }

        public SelectItemMessenger<Member> SelectItem { get; private set; }

        public MembersViewModel(ObservableCollection<Member> members)
        {
            _members = members;
            _members.CollectionChanged += Members_CollectionChanged;
            SelectItem = new SelectItemMessenger<Member>();
        }

        private void Members_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ForcePropertyChanged("Members");
        }
    }
}
