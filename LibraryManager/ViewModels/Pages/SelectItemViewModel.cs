using LibraryManager.Data.Item;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace LibraryManager.ViewModels.Pages
{
    class SelectItemViewModel : NotifyPropertyChanged, IPageViewModel
    {
        public SelectItemViewModel(Func<IssuableItem, bool> predicate, ObservableCollection<IssuableItem> additionalDisplayItems = null)
        {
            _filter = predicate;
            if (additionalDisplayItems != null) AdditionalDisplayItems = additionalDisplayItems;
            else AdditionalDisplayItems = new ObservableCollection<IssuableItem>();
            AdditionalDisplayItems.CollectionChanged += delegate (object s, NotifyCollectionChangedEventArgs e) { ForcePropertyChanged("FilteredItems"); };
        }

        private string _searchText;
        private IssuableItem _selectedItem;
        private Func<IssuableItem, bool> _filter;

        public ObservableCollection<IssuableItem> AdditionalDisplayItems { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText == value) return;
                _searchText = value;
                OnPropertyChanged();
                ForcePropertyChanged("FilteredItems");
            }
        }

        public IssuableItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value) return;
                _selectedItem = value;
                OnPropertyChanged();
                ForcePropertyChanged("CanProceed");
            }
        }

        public string Title => "Select Item";

        private ICollection<IssuableItem> GetItems() { return MainWindowViewModel.Instance.ItemsVM.Items; }

        public bool CanProceed
        {
            get { return SelectedItem != null; }
        }

        public IEnumerable<IssuableItem> FilteredItems
        {
            get
            {
                if (SearchText == null || SearchText.Equals("")) return GetItems().Where(_filter).Concat(AdditionalDisplayItems).Reverse();

                return GetItems().Where(x => x.Title.ToUpper().Contains(SearchText.ToUpper())).Where(_filter).Concat(AdditionalDisplayItems).Reverse();
            }
        }
    }
}
