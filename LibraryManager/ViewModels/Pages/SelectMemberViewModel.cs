using LibraryManager.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManager.ViewModels.Pages
{
    class SelectMemberViewModel : NotifyPropertyChanged, IPageViewModel
    {
        public SelectMemberViewModel(Func<Member, bool> predicate)
        {
            _filter = predicate;
        }

        private string _searchText;
        private Member _selectedMember;
        private Func<Member, bool> _filter;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText == value) return;
                _searchText = value;
                OnPropertyChanged();
                ForcePropertyChanged("FilteredMembers");
            }
        }

        public Member SelectedMember
        {
            get { return _selectedMember; }
            set
            {
                if (_selectedMember == value) return;
                _selectedMember = value;
                OnPropertyChanged();
                ForcePropertyChanged("CanProceed");
            }
        }

        public string Title => "Select Member";

        private ICollection<Member> GetMembers() { return MainWindowViewModel.Instance.MembersVM.Members; }
        
        public bool CanProceed
        {
            get { return SelectedMember != null; }
        }

        public IEnumerable<Member> FilteredMembers
        {
            get
            {
                if (SearchText == null || SearchText.Equals("")) return GetMembers().Where(_filter);

                return GetMembers().Where(x => x.Name.ToUpper().Contains(SearchText.ToUpper())).Where(_filter);
            }
        }
    }
}
