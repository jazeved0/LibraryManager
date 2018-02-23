using System;

namespace LibraryManager.Data
{
    /// <summary>
    /// Common subclass for LibraryManager data structures that support unique ID assignment and storage, and immutable hashcode persistence
    /// </summary>
    public abstract class LibraryObject : NotifyPropertyChanged
    {
        // ID Property Constructs
        private string _id;
        public string ID
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        // Hashcode persistence
        public LibraryObject()
        {
            UniqueToken = NextToken;
        }
        public LibraryObject(string id) : this()
        {
            ID = id;
        }
        protected UInt32 UniqueToken { get; }
        public override int GetHashCode()
        {
            unchecked // Ignore integer overflow
            {
                return UniqueToken.GetHashCode();
            }
        }

        // Factory members
        private static UInt32 _prevToken = 0;
        private static UInt32 NextToken { get { return ++_prevToken; } }
    }
}
