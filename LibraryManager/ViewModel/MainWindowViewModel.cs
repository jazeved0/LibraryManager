using LibraryManager.Data;
using LibraryManager.Data.Item;
using LibraryManager.Data.Member;
using LibraryManager.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace LibraryManager
{
    class MainWindowViewModel : NotifyPropertyChanged
    {
        public static MainWindowViewModel Instance;

        private ItemsViewModel _itemsVM;
        private MembersViewModel _membersVM;
        private int _openTab;


        public ItemsViewModel ItemsVM
        {
            get { return _itemsVM; }
        }

        public MembersViewModel MembersVM
        {
            get { return _membersVM; }
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

        public MainWindowViewModel()
        {
            Instance = this;

            SampleData.Seed();
            _itemsVM = new ItemsViewModel(SampleData.Items);
            _membersVM = new MembersViewModel(SampleData.Members);
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
    }
}
