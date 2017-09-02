using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibraryManager.Data.Item
{
    public class ItemStatus : INotifyPropertyChanged
    {
        public ItemStatus(IssuableItem parent)
        {
            this._parent = parent;
        }

        private StatusType _type = StatusType.Shelved;
        private DateTime _contextualDate;
        private IssuableItem _parent;

        public StatusType Type
        {
            get { return _type; }
            set
            {
                if (value == _type) return;
                _type = value;
                OnPropertyChanged();
            }
        }

        public DateTime ContextualDate
        {
            get { return _contextualDate; }
            set
            {
                if (value == _contextualDate) return;
                _contextualDate = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GetRemainder()
        {
            TimeSpan sinceContextualDate = System.DateTime.Now.Subtract(ContextualDate);
            TimeSpan maxDuration;
            if(_parent.Owner.Type == Member.MemberType.Student)
            {
                if (_type == StatusType.Reserved) maxDuration = new TimeSpan(App.Instance.Config.StudentReservationMaxDuration, 0, 0, 0);
                else maxDuration = new TimeSpan(App.Instance.Config.StudentIssuanceMaxDuration, 0, 0, 0);
            }
            else
            {
                if (_type == StatusType.Reserved) maxDuration = new TimeSpan(App.Instance.Config.TeacherReservationMaxDuration, 0, 0, 0);
                else maxDuration = new TimeSpan(App.Instance.Config.TeacherIssuanceMaxDuration, 0, 0, 0);
            }
            TimeSpan remaining = maxDuration.Subtract(sinceContextualDate);
            if (remaining.TotalMinutes <= 59) return ((int)sinceContextualDate.TotalMinutes) + " Minutes";
            else if (remaining.TotalHours <= 59) return ((int)remaining.TotalHours) + " Hours";
            else return ((int)remaining.TotalDays) + " Days";
        }

        public double GetRemainingMinutes()
        {
            TimeSpan sinceContextualDate = System.DateTime.Now.Subtract(ContextualDate);
            TimeSpan maxDuration;
            if (_parent.Owner.Type == Member.MemberType.Student)
            {
                if (_type == StatusType.Reserved) maxDuration = new TimeSpan(App.Instance.Config.StudentReservationMaxDuration, 0, 0, 0);
                else maxDuration = new TimeSpan(App.Instance.Config.StudentIssuanceMaxDuration, 0, 0, 0);
            }
            else
            {
                if (_type == StatusType.Reserved) maxDuration = new TimeSpan(App.Instance.Config.TeacherReservationMaxDuration, 0, 0, 0);
                else maxDuration = new TimeSpan(App.Instance.Config.TeacherIssuanceMaxDuration, 0, 0, 0);
            }
            TimeSpan remaining = maxDuration.Subtract(sinceContextualDate);
            return sinceContextualDate.TotalMinutes;
        }

        public enum StatusType
        {
            Overdue,
            Issued,
            Reserved,
            Shelved
        }
    }
}