using System.Collections.ObjectModel;
using LibraryManager.Data.Member;
using LibraryManager.Data.Item;
using LibraryManager.Data.Action;
using System.Threading;
using System;
using LibraryManager.ViewModels;

namespace LibraryManager
{
    /// <summary>
    /// Library provider that gives sample data from an imaginary database
    /// </summary>
    internal class SampleData : ILibraryDataProvider
    {
        public ObservableCollection<Member> Members { get; private set; }
        public ObservableCollection<IssuableItem> Items { get; private set; }
        public ObservableCollection<LoggedAction> History { get; private set; }

        public SampleData()
        {
            History = new ObservableCollection<LoggedAction>();
        }

        public void Initialize()
        {
            if (Items != null) return;
            Items = new ObservableCollection<IssuableItem>
            {
                CreateItem(ItemType.Book, "010000", "Orphan Train", "Christina Baker Kline"),
                CreateItem(ItemType.Book, "020000", "The Catcher in the Rye", "J.D. Salinger" ),
                CreateItem(ItemType.Book, "030000", "The Book Thief", "Markus Zusak" ),
                CreateItem(ItemType.Book, "040000", "A Long Walk to Water", "Linda Sue Park" ),
                CreateItem(ItemType.Book, "050000", "The Giver", "Lois Lowry" ),
                CreateItem(ItemType.Book, "060000", "Of Mice and Men", "John Steinbeck" ),
                CreateItem(ItemType.Book, "070000", "A Game of Thrones", "George R. R. Martin" ),
                CreateItem(ItemType.Book, "080000", "The Alchemist", "Paulo Coehlo" ),
                CreateItem(ItemType.Book, "090000", "Lord of the Flies", "William Golding" ),
                CreateItem(ItemType.Book, "100000", "The Outsiders", "S.E. Hinton" ),
                CreateItem(ItemType.Book, "100001", "The Outsiders", "S.E. Hinton" ),
                CreateItem(ItemType.Book, "020001", "The Escape Artist", "Brad Meltzer" ),
                CreateItem(ItemType.Book, "030001", "The Great Alone", "Kristin Hannah" ),
                CreateItem(ItemType.Book, "040001", "Educated", "Tara Westover" ),
                CreateItem(ItemType.Book, "050001", "The Fault in Our Stars", "John Green" ),
                CreateItem(ItemType.Book, "060001", "One of Us is Lying", "Karen M. McManus" ),
                CreateItem(ItemType.Book, "070001", "The Woman in the Window", "A. J. Finn" ),
                CreateItem(ItemType.Book, "080001", "Fifty Fifty", "James Patterson, Candice Fox" ),
                CreateItem(ItemType.Book, "090001", "The Hazel Wood", "Melissa Albert" ),
                CreateItem(ItemType.Book, "090002", "The Great Gatsby", "F. Scott Fitzgerald" ),
                CreateItem(ItemType.Book, "090003", "The Great Gatsby", "F. Scott Fitzgerald" ),

                CreateItem(ItemType.Movie, "110000", "The Great Gatsby", "Baz Luhrmann" ),
                CreateItem(ItemType.Movie, "110001", "Guardians of the Galaxy Vol. 2", "James Gunn" ),
                CreateItem(ItemType.Movie, "110002", "Logan", "James Mangold" ),
                CreateItem(ItemType.Movie, "110003", "Get Out", "Jordan Peele" ),
                CreateItem(ItemType.Movie, "110004", "John Wick: Chapter 2", "Chad Stahelski" ),
                CreateItem(ItemType.Movie, "110005", "Split", "M. Night Shyamalan" ),
                CreateItem(ItemType.Movie, "110006", "The Lost City of Z", "James Gray" ),
                CreateItem(ItemType.Movie, "110007", "Citizen Kane", "Orson Welles" )
            };

            if (Members != null) return;
            Members = new ObservableCollection<Member>
            {
                new Member { Type=MemberType.Teacher, ID="0A", Name="Gina Candler" },
                new Member { Type=MemberType.Teacher, ID="0B", Name="Kirsten Legac" },
                new Member { Type=MemberType.Teacher, ID="0C", Name="Jeffery Stetzler" },
                new Member { Type=MemberType.Teacher, ID="0D", Name="David Kelman" },
                new Member { Type=MemberType.Teacher, ID="0E", Name="Jason Wohlers" },
                new Member { Type=MemberType.Teacher, ID="0F", Name="Susan Morrison" },
                new Member { Type=MemberType.Teacher, ID="0G", Name="Michael Franks" },

                new Member { Type=MemberType.Student, ID="00", Name="Joshua Lagria"},
                new Member { Type=MemberType.Student, ID="01", Name="Kendall Tate" },
                new Member { Type=MemberType.Student, ID="00", Name="Josh Park"},
                new Member { Type=MemberType.Student, ID="02", Name="Abbi Ellis"},
                new Member { Type=MemberType.Student, ID="03", Name="Caleb Fox" },
                new Member { Type=MemberType.Student, ID="04", Name="Christian Daniels"},
                new Member { Type=MemberType.Student, ID="05", Name="Brody Helton"},
                new Member { Type=MemberType.Student, ID="06", Name="Jared Azevedo"},
                new Member { Type=MemberType.Student, ID="07", Name="Joseph Azevedo"},
                new Member { Type=MemberType.Student, ID="08", Name="Andre Luo"},
            };

            IssueSampleData();
        }

        private void IssueSampleData()
        {
            Issue("00", "110007");
            Issue("02", "100001");
            CheckIn("100001");
            Reserve("00", "080001");
            Issue("05", "020000");
            Issue("07", "060001");
            CheckIn("110007");
            Issue("08", "110005");
            Reserve("0C", "110001");
            Issue("0A", "010000");
            Issue("0A", "040000");
            Issue("03", "070000");
            Reserve("0C", "070001");
            Issue("0B", "090002");
            Issue("0G", "050001");
            Issue("05", "110003");
            CheckIn("090002");
            Issue("0F", "110007");
            MakeOverdue("020000", 30);
            MakeOverdue("110003", 42);
            MakeOverdue("070001", 58);
            Clone("110004", "110005");
        }

        private IssuableItem CreateItem(ItemType type, string id, string title, string author)
        {
            IssuableItem item = new IssuableItem() { Type = type, ID = id, Title = title, Author = author };
            MainWindowViewModel.Instance.HistoryVM.History.Add(new LoggedAction { Item = item, Member = null, Type = ActionType.Addition, Timestamp = DateTime.Now });
            return item;
        }

        private void Issue(string memberID, string itemID)
        {
            GetItemForID(itemID).Status.Issue(GetMemberforID(memberID));
        }

        private void Reserve(string memberID, string itemID)
        {
            GetItemForID(itemID).Status.Reserve(GetMemberforID(memberID));
        }

        private void MakeOverdue(string itemID, int days)
        {
            GetItemForID(itemID).Status.InitialDate = DateTime.Now.Subtract(new TimeSpan(days, 0, 0, 0));
            GetItemForID(itemID).Status.MakeOverdue();
        }

        private void CheckIn(string itemID)
        {
            GetItemForID(itemID).Status.CheckIn();
        }

        private void Clone(string itemID, string newID)
        {
            IssuableItem newItem = (IssuableItem)GetItemForID(itemID).Clone();
            newItem.ID = newID;
            Items.Add(newItem);
            MainWindowViewModel.Instance.HistoryVM.History.Add(new LoggedAction { Timestamp = DateTime.Now, Member = null, Item = newItem, Type = ActionType.Addition });
        }

        private IssuableItem GetItemForID(string idToFind)
        {
            foreach(IssuableItem item in Items)
            {
                if (item.ID.Equals(idToFind)) return item;
            }
            return null;
        }

        private Member GetMemberforID(string idToFind)
        {
            foreach (Member member in Members)
            {
                if (member.ID.Equals(idToFind)) return member;
            }
            return null;
        }

        public void LoadConfigurationData(Configuration config)
        {
            config.DiscreteValues[MemberType.Teacher]["issuance_max_duration"].CurrentValue = 21;
            config.DiscreteValues[MemberType.Teacher]["reservation_max_duration"].CurrentValue = 7;
            config.DiscreteValues[MemberType.Teacher]["issuance_max"].CurrentValue = 5;
            config.DiscreteValues[MemberType.Teacher]["reservation_max"].CurrentValue = 4;
            config.DecimalValues[MemberType.Teacher]["max_fee"].CurrentValue = 1.00m;
        }

        public void Update()
        {
            Thread.Sleep(1400); // Sample data retreival delay
            foreach(IssuableItem item in Items)
            {
                item.Status.UpdateStatus(true);
            }
        }
    }
}