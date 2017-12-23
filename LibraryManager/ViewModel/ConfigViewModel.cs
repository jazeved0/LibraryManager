namespace LibraryManager.ViewModels
{
    class ConfigViewModel : NotifyPropertyChanged
    {
        public ConfigViewModel()
        {
            config = App.Instance.Config;

            _studentIssuanceMaxDuration = config.StudentIssuanceMaxDuration;
            _studentReservationMaxDuration = config.StudentReservationMaxDuration;
            _studentIssuanceMax = config.StudentIssuanceMax;

            _teacherIssuanceMaxDuration = config.TeacherIssuanceMaxDuration;
            _teacherReservationMaxDuration = config.TeacherReservationMaxDuration;
            _teacherIssuanceMax = config.TeacherIssuanceMax;
        }

        private int _studentIssuanceMaxDuration;
        private int _studentReservationMaxDuration;
        private int _studentIssuanceMax;

        private int _teacherIssuanceMaxDuration;
        private int _teacherReservationMaxDuration;
        private int _teacherIssuanceMax;

        private Configuration config;

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
                disable &= (_studentIssuanceMaxDuration == config.StudentIssuanceMaxDuration);
                disable &= (_studentReservationMaxDuration == config.StudentReservationMaxDuration);
                disable &= (_studentIssuanceMax == config.StudentIssuanceMax);

                disable &= (_teacherIssuanceMaxDuration == config.TeacherIssuanceMaxDuration);
                disable &= (_teacherReservationMaxDuration == config.TeacherReservationMaxDuration);
                disable &= (_teacherIssuanceMax == config.TeacherIssuanceMax);
                if (disable) return false;
                else return true;
            }
        }

        internal void CommitChanges()
        { 
            config.StudentIssuanceMaxDuration = _studentIssuanceMaxDuration;
            config.StudentReservationMaxDuration = _studentReservationMaxDuration;
            config.StudentIssuanceMax = _studentIssuanceMax;

            config.TeacherIssuanceMaxDuration = _teacherIssuanceMaxDuration;
            config.TeacherReservationMaxDuration = _teacherReservationMaxDuration;
            config.TeacherIssuanceMax = _teacherIssuanceMax;
            ForcePropertyChanged("CommitEnable");
            config.UpdateDependencies();
        }
    }
}
