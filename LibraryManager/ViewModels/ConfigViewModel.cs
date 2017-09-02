using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManager.ViewModels
{
    class ConfigViewModel : INotifyPropertyChanged
    {
        public ConfigViewModel()
        {
            _studentIssuanceMaxDuration = App.Instance.Config.StudentIssuanceMaxDuration;
            _studentReservationMaxDuration = App.Instance.Config.StudentReservationMaxDuration;
            _studentIssuanceMax = App.Instance.Config.StudentIssuanceMax;

            _teacherIssuanceMaxDuration = App.Instance.Config.TeacherIssuanceMaxDuration;
            _teacherReservationMaxDuration = App.Instance.Config.TeacherReservationMaxDuration;
            _teacherIssuanceMax = App.Instance.Config.TeacherIssuanceMax;
        }

        private int _studentIssuanceMaxDuration;
        private int _studentReservationMaxDuration;
        private int _studentIssuanceMax;

        private int _teacherIssuanceMaxDuration;
        private int _teacherReservationMaxDuration;
        private int _teacherIssuanceMax;

        public int StudentIssuanceMaxDuration
        {
            get { return _studentIssuanceMaxDuration; }
            set
            {
                if (value == _studentIssuanceMaxDuration) return;
                _studentIssuanceMaxDuration = value;
                OnPropertyChanged();
            }
        }

        public int StudentReservationMaxDuration
        {
            get { return _studentReservationMaxDuration; }
            set
            {
                if (value == _studentReservationMaxDuration) return;
                _studentReservationMaxDuration = value;
                OnPropertyChanged();
            }
        }

        public int StudentIssuanceMax
        {
            get { return _studentIssuanceMax; }
            set
            {
                if (value == _studentIssuanceMax) return;
                _studentIssuanceMax = value;
                OnPropertyChanged();
            }
        }

        public int TeacherIssuanceMaxDuration
        {
            get { return _teacherIssuanceMaxDuration; }
            set
            {
                if (value == _teacherIssuanceMaxDuration) return;
                _teacherIssuanceMaxDuration = value;
                OnPropertyChanged();
            }
        }

        public int TeacherReservationMaxDuration
        {
            get { return _teacherReservationMaxDuration; }
            set
            {
                if (value == _teacherReservationMaxDuration) return;
                _teacherReservationMaxDuration = value;
                OnPropertyChanged();
            }
        }

        public int TeacherIssuanceMax
        {
            get { return _teacherIssuanceMax; }
            set
            {
                if (value == _teacherIssuanceMax) return;
                _teacherIssuanceMax = value;
                OnPropertyChanged();
            }
        }
        
        public bool CommitEnable
        {
            get
            {
                bool disable = true;
                disable &= (_studentIssuanceMaxDuration == App.Instance.Config.StudentIssuanceMaxDuration);
                disable &= (_studentReservationMaxDuration == App.Instance.Config.StudentReservationMaxDuration);
                disable &= (_studentIssuanceMax == App.Instance.Config.StudentIssuanceMax);

                disable &= (_teacherIssuanceMaxDuration == App.Instance.Config.TeacherIssuanceMaxDuration);
                disable &= (_teacherReservationMaxDuration == App.Instance.Config.TeacherReservationMaxDuration);
                disable &= (_teacherIssuanceMax == App.Instance.Config.TeacherIssuanceMax);
                if (disable) return false;
                else return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void UpdateProperty(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void UpdateEnable()
        {
            UpdateProperty("CommitEnable");
        }

        internal void CommitChanges()
        {
            App.Instance.Config.StudentIssuanceMaxDuration = _studentIssuanceMaxDuration;
            App.Instance.Config.StudentReservationMaxDuration = _studentReservationMaxDuration;
            App.Instance.Config.StudentIssuanceMax = _studentIssuanceMax;

            App.Instance.Config.TeacherIssuanceMaxDuration = _teacherIssuanceMaxDuration;
            App.Instance.Config.TeacherReservationMaxDuration = _teacherReservationMaxDuration;
            App.Instance.Config.TeacherIssuanceMax = _teacherIssuanceMax;
            UpdateEnable();
        }
    }
}
