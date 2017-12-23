using System.Collections.ObjectModel;
using LibraryManager.Data.Member;
using LibraryManager.Data.Item;

namespace LibraryManager
{
    internal class SampleData
    {
        public static ObservableCollection<Member> Members { get; set; }
        public static ObservableCollection<IssuableItem> Items { get; set; }
        public static void Seed()
        {
            if (Items != null) return;
            Items = new ObservableCollection<IssuableItem>
            {
                new IssuableItem { Type=ItemType.Book, ID="010000", Title= "Orphan Train", Author= "Christina Baker Kline" },
                new IssuableItem { Type=ItemType.Book, ID="020000", Title = "The Catcher in the Rye", Author = "J.D. Salinger" },
                new IssuableItem { Type=ItemType.Book, ID="030000", Title = "The Book Thief", Author = "Markus Zusak" },
                new IssuableItem { Type=ItemType.Book, ID="040000", Title = "A Long Walk to Water", Author = "Linda Sue Park" },
                new IssuableItem { Type=ItemType.Book, ID="050000", Title = "The Giver", Author="Lois Lowry" },
                new IssuableItem { Type=ItemType.Book, ID="060000", Title = "Of Mice and Men", Author = "John Steinbeck" },
                new IssuableItem { Type=ItemType.Book, ID="070000", Title = "A Game of Thrones", Author = "George R. R. Martin" },
                new IssuableItem { Type=ItemType.Book, ID="080000", Title = "The Alchemist", Author = "Paulo Coehlo" },
                new IssuableItem { Type=ItemType.Book, ID="090000", Title = "Lord of the Flies", Author = "William Golding" },
                new IssuableItem { Type=ItemType.Book, ID="100000", Title= "The Outsiders", Author = "S.E. Hinton" },
                new IssuableItem { Type=ItemType.Movie, ID="110000", Title= "The Great Gatsby", Author = "Baz Luhrmann" }
            };

            if (Members != null) return;

            Member candler = new Member { Type=MemberType.Teacher, ID = "0A", Name = "Gina Candler" };
            getItemForID("010000").Status.Issue(candler);
            getItemForID("080000").Status.Reserve(candler);
            Member legac = new Member { Type = MemberType.Teacher, ID = "0B", Name = "Kirsten Legac" };
            getItemForID("020000").Status.Reserve(legac);
            getItemForID("040000").Status.Issue(legac);
            getItemForID("050000").Status.Issue(legac);
            Member tate = new Member { Type = MemberType.Student, ID = "01", Name="Kendall Tate" };
            getItemForID("060000").Status.Issue(tate);
            getItemForID("070000").Status.Reserve(tate);
            Member fox = new Member { Type = MemberType.Student, ID = "03", Name="Caleb Fox" };
            getItemForID("090000").Status.Reserve(fox);

            Members = new ObservableCollection<Member>
            {
                candler,
                legac,
                new Member { Type=MemberType.Teacher, ID="0C", Name="Jeffery Stetzler" },
                new Member { Type=MemberType.Teacher, ID="0D", Name="David Kelman" },
                new Member { Type=MemberType.Student, ID="00", Name="Joshua Lagria"},
                tate,
                new Member { Type=MemberType.Student, ID="02", Name="Josh Park"},
                fox
            };
        }

        public static IssuableItem getItemForID(string idToFind)
        {
            foreach(IssuableItem item in Items)
            {
                if (item.ID.Equals(idToFind)) return item;
            }
            return null;
        }
    }
}