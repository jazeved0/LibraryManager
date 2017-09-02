using System.Collections;
using System.ComponentModel;

namespace LibraryManager
{
    public interface ICustomSorter : IComparer
    {
        ListSortDirection SortDirection { get; set; }
    }
}