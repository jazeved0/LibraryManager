using System;
using System.Diagnostics.Contracts;

namespace LibraryManager.Data.Item.Status
{
    /// <summary>
    /// Data structure that represents the contextual status of a single IssuableItem within the library. It can represent an Issuance, a Reservation, an Overdue & Fee-incurring item, or simply a Shelved item.
    /// Extends NotifyPropertyChanged to allow DataBinding via the MVVM WPF structure.
    /// </summary>
    public class ItemStatus : NotifyPropertyChanged
    {
        #region PrivateFields

        private StatusType _type = StatusType.Shelved; // Default value as Shelved
        private DateTime _initialDate;
        private Member.Member _owner;

        private IssuableItem _item;

        #endregion

        /// <summary>
        /// Enum which represents each possible state for the IssuableItem's Status
        /// </summary>
        public enum StatusType
        {
            Overdue,
            Issued,
            Reserved,
            Shelved
        }

        #region Properties

        /// <summary>
        /// The type of status that the object currently represents. All other properties/methods depend on this for context
        /// </summary>
        public StatusType Type
        {
            get { return _type; }
            set
            {
                if (value == _type) return;
                _type = value;
                OnPropertyChanged();

                // Also, update sort order
                ForcePropertyChanged("SortOrderText");
            }
        }

        /// <summary>
        /// The initial DateTime that the item's status type became what it was currently is, with the exception that Overdue items' InitialDate remains what it was upon issuance
        /// </summary>
        public DateTime InitialDate
        {
            get { return _initialDate; }
            set
            {
                if (value == _initialDate) return;
                _initialDate = value;
                OnPropertyChanged();

                // Also force update on due date, remainder, and sort order
                ForcePropertyChanged("DueDate");
                ForcePropertyChanged("Remainder");
                ForcePropertyChanged("SortOrderText");
            }
        }

        /// <summary>
        /// The library member that currently owns this item. If Shelved, the Owner will always be <code>null</code>
        /// </summary>
        public Member.Member Owner
        {
            get
            {
                // If shelved, always return null (although _owner should also be null)
                if (Type == StatusType.Shelved) return null;
                else return _owner;
            }
            set
            {
                if (value == _owner) return;
                _owner = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A DateTime that is a function of the Item's Status that provides the proper final time for the object's state to change
        /// If the Item's Status is Shelved, the function returns <code>DateTime.Now</code>
        /// </summary>
        [Pure]
        public DateTime DueDate
        {
            get
            {
                // If the item is either Issued, Reserved, or Overdue (has a valid Owner),
                if (HasOwner)
                {
                    // Return the initial date added to the configuration's specified maximum duration for the owner's type (Student or Teacher)
                    return InitialDate.Add(App.Instance.Config.GetMaxDuration(Type, Owner.Type));
                }
                // If not, return the current system DateTime
                else return DateTime.Now;
            }
        }

        /// <summary>
        /// Gets the Fee incurred by the item existing within a member's item inventory. Depends on the configuration as well as the owning member's type
        /// </summary>
        [Pure]
        public decimal Fee
        {
            get
            {
                // If the item has an owner
                if (HasOwner)
                {
                    // Return the configuration's specified overdue fee for the owner's type (Student or Teacher)
                    return App.Instance.Config.GetOverdueFee(Owner.Type);

                }
                // If not, then the status is not Overdue, so no fee exists
                else return 0m;
            }
        }

        /// <summary>
        /// Whether the Owner is NonNull and the object's state supplies a valid owner
        /// </summary>
        [Pure]
        public bool HasOwner
        {
            get { return (_owner != null) && (Type != StatusType.Shelved); }
        }

        /// <summary>
        /// Gets the remaining time that the item has before going through the type changes: [Issued -> Overdue] or [Reserved -> Shelved]
        /// Needs ForcePropertyChanged("Remainder") to be called each relevant interval for the bindings to update
        /// Note: Can be negative if the current time is past the due date
        /// </summary>
        [Pure]
        public TimeSpan Remainder
        {
            get
            {
                // If the item is not in a valid state to have a remaining time, return a zero-time interval
                if (!HasOwner || Type == StatusType.Overdue) return new TimeSpan();
                else
                {
                    // Else, return the difference between the due date and the current DateTime
                    return DueDate.Subtract(DateTime.Now);
                }
            }
        }
        
        /// <summary>
        /// Gets the text to use as a sortable and distinguishable property
        /// Used for sorting within ItemsView
        /// </summary>
        [Pure]
        public String SortOrderText
        {
            get
            {
                return Type.ToString() + Remainder.ToString(); // Combination of the type first, then remainder to be sorted lexicographically
            }
        }

        #endregion

        /// <param name="parent">The stored parent IssuableItem that the ItemStatus represents the state of</param>
        public ItemStatus(IssuableItem parent)
        {
            this._item = parent;
        }

        /// <summary>
        /// Called each time a pertinent config value is changed by the user
        /// </summary>
        public void ConfigChanged()
        {
            if (!HasOwner) return; // If the item has no owner, then no updates are needed (it's shelved)

            // If the object's state is now invalid
            if(Remainder.TotalSeconds < 0 && Type != StatusType.Overdue)
            {
                // Depending on the current state, either make the status overdue or release its reservation
                if (Type == StatusType.Issued) MakeOverdue();
                else if (Type == StatusType.Reserved) ReleaseReservation();
            }
            // If the object is overdue but now shouldn't be
            else if(Type == StatusType.Overdue && Remainder.TotalSeconds > 0)
            {
                // Revert its overdue status
                RevertOverdue();
            }
            else
            {
                // Update property bindings
                ForcePropertyChanged("DueDate");
                ForcePropertyChanged("Remainder");
                ForcePropertyChanged("SortOrderText");
            }
        }

        /// <summary>
        /// Issues the status's item to the specified Library Member
        /// </summary>
        /// <param name="to">The new owner of the IssuableItem</param>
        public void Issue(Member.Member to)
        {
            Owner = to;
            Type = StatusType.Issued;
            InitialDate = DateTime.Now;
            Owner.Items.Add(_item);
        }

        /// <summary>
        /// Reserves the status's item to the specified Library Member
        /// </summary>
        /// <param name="to"></param>
        public void Reserve(Member.Member to)
        {
            Owner = to;
            Type = StatusType.Reserved;
            InitialDate = DateTime.Now;
            Owner.Items.Add(_item);
        }

        /// <summary>
        /// Checks in the current item and shelves it
        /// </summary>
        public void CheckIn()
        {
            InitialDate = DateTime.Now;
            Owner.Items.Remove(_item);
            Type = StatusType.Shelved;
            Owner = null;
        }

        /// <summary>
        /// Releases the Library Member's reservation on the item and shelves it
        /// </summary>
        public void ReleaseReservation()
        {
            InitialDate = DateTime.Now;
            Owner.Items.Remove(_item);
            Type = StatusType.Shelved;
            Owner = null;
        }

        /// <summary>
        /// Forces the item's status to be overdue
        /// </summary>
        internal void MakeOverdue()
        {
            Type = StatusType.Overdue;
            Owner.ForcePropertyChanged("Fee");
        }

        /// <summary>
        /// Reverts the item's overdue status
        /// </summary>
        internal void RevertOverdue()
        {
            Type = StatusType.Issued;
            Owner.ForcePropertyChanged("Fee");
            ForcePropertyChanged("DueDate");
            ForcePropertyChanged("Remainder");
            ForcePropertyChanged("SortOrderText");
        }
    }
}