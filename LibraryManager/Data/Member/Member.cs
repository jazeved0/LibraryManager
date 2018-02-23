using LibraryManager.Data.Item;
using LibraryManager.Data.Item.Status;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;

namespace LibraryManager.Data.Member
{
    /// <summary>
    /// Data structure that represents a single Library member that can have items issued or reserved to.
    /// Extends NotifyPropertyChanged to allow DataBinding via the MVVM WPF structure.
    /// </summary>
    public class Member : LibraryObject
    {
        #region PrivateFields
        
        private string _name;
        private ObservableCollection<IssuableItem> _items;
        private MemberType _type;

        #endregion

        #region Properties

        /// <summary>
        /// The full name of the library member, as printed on their card
        /// </summary>
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

        /// <summary>
        /// The type of the library member
        /// </summary>
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

        /// <summary>
        /// A non-settable collection of each Issuable item that is initialized upon Member initialization
        /// </summary>
        public ObservableCollection<IssuableItem> Items
        {
            get { return _items; }
        }

        /// <summary>
        /// The fee that the library member has incurred from each overdue IssuableItem
        /// Each time an item's status' type changes from [Issued -> Overdue] or [Overdue -> Shelved], ForcePropertyChanged("Fee") must be called to update bindings to the fee
        /// </summary>
        [Pure]
        public decimal Fee
        {
            get
            {
                decimal _fee = 0m;
                // Loop through each item
                foreach(IssuableItem item in Items)
                {
                    // Add its fee to the overall library member's fee
                    _fee += item.Status.Fee;
                }
                return _fee;
            }
        }

        [Pure]
        public int IssuanceCount
        {
            get
            {
                int count = 0;
                foreach (IssuableItem item in Items)
                {
                    if (item.Status.Type == ItemStatus.StatusType.Issued || item.Status.Type == ItemStatus.StatusType.Overdue) ++count;
                }
                return count;
            }
        }

        [Pure]
        public int ReservationCount
        {
            get
            {
                int count = 0;
                foreach (IssuableItem item in Items)
                {
                    if (item.Status.Type == ItemStatus.StatusType.Reserved) ++count;
                }
                return count;
            }
        }

        #endregion

        /// <summary>
        /// Default constructor for Member that is called on Database parse and on new row creation in MembersView
        /// </summary>
        public Member()
        {
            // Initialize the Items collection and register our delegate to its CollectionChanged event
            _items = new ObservableCollection<IssuableItem>();
            _items.CollectionChanged += Items_CollectionChanged;
        }

        /// <summary>
        /// Delegate for the Item ObservableCollection that is called each time the collection is modified
        /// Passes through our object and notifies listeners of this for property 'Items'
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="e">Ignored</param>
        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ForcePropertyChanged("Items");
        }
    }
}