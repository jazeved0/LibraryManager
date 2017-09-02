namespace LibraryManager
{
    public class Configuration
    {
        private int _studentIssuanceMaxDuration = 14;
        private int _studentReservationMaxDuration = 4;
        private int _studentIssuanceMax = 2;

        private int _teacherIssuanceMaxDuration = 27;
        private int _teacherReservationMaxDuration = 7;
        private int _teacherIssuanceMax = 5; 

        public int StudentIssuanceMaxDuration
        {
            get { return _studentIssuanceMaxDuration; }
            internal set { _studentIssuanceMaxDuration = value; }
        }

        public int StudentReservationMaxDuration
        {
            get { return _studentReservationMaxDuration; }
            internal set { _studentReservationMaxDuration = value; }
        }

        public int StudentIssuanceMax
        {
            get { return _studentIssuanceMax; }
            internal set { _studentIssuanceMax = value; }
        }

        public int TeacherIssuanceMax
        {
            get { return _teacherIssuanceMax; }
            internal set { _teacherIssuanceMax = value; }
        }

        public int TeacherReservationMaxDuration
        {
            get { return _teacherReservationMaxDuration; }
            internal set { _teacherReservationMaxDuration = value; }
        }

        public int TeacherIssuanceMaxDuration
        {
            get { return _teacherIssuanceMaxDuration; }
            internal set { _teacherIssuanceMaxDuration = value; }
        }
    }
}
