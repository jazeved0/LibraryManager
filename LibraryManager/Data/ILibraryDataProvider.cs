using LibraryManager.Data.Action;
using LibraryManager.Data.Item;
using LibraryManager.Data.Member;
using System.Collections.ObjectModel;

namespace LibraryManager
{
    public interface ILibraryDataProvider
    {
        ObservableCollection<Member> Members { get; }
        ObservableCollection<IssuableItem> Items { get; }
        ObservableCollection<LoggedAction> History { get; }
        void Initialize();
        void Update();
        void LoadConfigurationData(Configuration config);
    }
}