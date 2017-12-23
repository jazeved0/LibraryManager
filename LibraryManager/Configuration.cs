using LibraryManager.Data.Item;
using LibraryManager.Data.Member;
using System;
using static LibraryManager.Data.Item.Status.ItemStatus;

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

        public void UpdateDependencies()
        {
            foreach(IssuableItem item in MainWindowViewModel.Instance.ItemsVM.Items)
            {
                // TODO Optimize based on which properties changed
                item.Status.ConfigChanged();
            }
        }

        public TimeSpan GetMaxDuration(StatusType statusType, MemberType memberType)
        {
            if (memberType == MemberType.Student)
            {
                if(statusType == StatusType.Issued) return new TimeSpan(StudentIssuanceMaxDuration, 0, 0, 0);
                else return new TimeSpan(StudentReservationMaxDuration, 0, 0, 0);
            }
            else
            {
                if (statusType == StatusType.Issued) return new TimeSpan(TeacherIssuanceMaxDuration, 0, 0, 0);
                else return new TimeSpan(TeacherReservationMaxDuration, 0, 0, 0);
            }
        }

        internal decimal GetOverdueFee(MemberType type2)
        {
            throw new NotImplementedException();
        }
    }
}
