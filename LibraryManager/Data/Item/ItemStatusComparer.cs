using System.ComponentModel;

namespace LibraryManager.Data.Item
{
    class ItemStatusComparer : ICustomSorter
    {
        public ItemStatusComparer()
        {

        }

        public ListSortDirection SortDirection { get; set; }

        public int Compare(object x, object y)
        {
            if (x is ItemStatus && y is ItemStatus)
            {
                ItemStatus xS = x as ItemStatus;
                ItemStatus yS = y as ItemStatus;
                if (xS.Type == yS.Type) return direction(xS.Type.CompareTo(yS.Type));
                else return direction(xS.GetRemainingMinutes().CompareTo(yS.GetRemainingMinutes()));
            }
            else return 1;
        }

        private int direction(int sortResult)
        {
            if (SortDirection == ListSortDirection.Ascending) return -sortResult;
            else return sortResult;
        }
    }
}
