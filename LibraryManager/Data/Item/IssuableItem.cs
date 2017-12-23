using LibraryManager.Data.Item.Status;
using System;
using System.ComponentModel;

namespace LibraryManager.Data.Item
{
    /// <summary>
    /// Data structure that represents a single (not neccessarily unique) issuable library item that can be shelved, issued, reserved, or made overdue.
    /// Extends NotifyPropertyChanged to allow DataBinding via the MVVM WPF structure.
    /// </summary>
    public class IssuableItem : NotifyPropertyChanged
    {
        #region PrivateFields

        private String _id;
        private String _title;
        private String _author;
        private ItemType _type;
        private ItemStatus _status;

        #endregion

        #region Properties

        /// <summary>
        /// A unique identifier for the issuable item
        /// </summary>
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

        /// <summary>
        /// The item's title
        /// </summary>
        public String Title
        {
            get { return _title; }
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The item's author's full name
        /// </summary>
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

        /// <summary>
        /// The type of the item, or the type of media it is published in
        /// </summary>
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

        /// <summary>
        /// The current status of the item, whether it be shelved, issued, reserved or overdue
        /// Provides the contract that it will always be NonNull
        /// </summary>
        public ItemStatus Status
        {
            get { return _status; }
        }

        #endregion

        /// <summary>
        /// The default initializer of IssuableItem that intiailizes its ItemStatus object and registers our property changed delegate to the ItemStatus' property changed event
        /// </summary>
        public IssuableItem()
        {
            _status = new ItemStatus(this);
            _status.PropertyChanged += StatusChanged;
        }

        /// <summary>
        /// Delegate for the ItemStatus that is called each time one of the object's properties is changed
        /// Passes through our object and notifies listeners of this for property 'Status'
        /// </summary>
        /// <param name="sender">Ignored</param>
        /// <param name="e">Ignored</param>
        private void StatusChanged(Object sender, PropertyChangedEventArgs e)
        {
            ForcePropertyChanged("Status");
        }
    }
}
