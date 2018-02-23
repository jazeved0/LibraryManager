using LibraryManager.Data.Item;
using System;

namespace LibraryManager.Data.Action
{
    public class LoggedAction : NotifyPropertyChanged
    {
        #region PrivateFields

        private DateTime _timestamp;
        private ActionType _type;
        private IssuableItem _item;
        private Member.Member _member;

        #endregion

        #region Properties
        
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set
            {
                if (value == _timestamp) return;
                _timestamp = value;
                OnPropertyChanged();
            }
        }

        public ActionType Type
        {
            get { return _type; }
            set
            {
                if (value == _type) return;
                _type = value;
                OnPropertyChanged();
            }
        }

        public IssuableItem Item
        {
            get { return _item; }
            set
            {
                if (value == _item) return;
                _item = value;
                OnPropertyChanged();
            }
        }

        public Member.Member Member
        {
            get { return _member; }
            set
            {
                if (value == _member) return;
                _member = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public LoggedAction()
        {
            
        }
    }
}
