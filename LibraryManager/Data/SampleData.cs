using System.Collections.ObjectModel;
using LibraryManager.Data.Member;
using LibraryManager.Data.Item;
using System;

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
                new Book { ID="010000", Name= "Orphan Train", Author= "Christina Baker Kline" },
                new Book { ID="020000", Name = "The Catcher in the Rye", Author = "J.D. Salinger" },
                new Book { ID="030000", Name = "The Book Thief", Author = "Markus Zusak" },
                new Book { ID="040000", Name = "A Long Walk to Water", Author = "Linda Sue Park" },
                new Book { ID="050000", Name = "The Giver", Author="Lois Lowry" },
                new Book { ID="060000", Name = "Of Mice and Men", Author = "John Steinbeck" },
                new Book { ID="070000", Name = "A Game of Thrones", Author = "George R. R. Martin" },
                new Book { ID="080000", Name = "The Alchemist", Author = "Paulo Coehlo" },
                new Book { ID="090000", Name = "Lord of the Flies", Author = "William Golding" },
                new Book { ID="100000", Name= "The Outsiders", Author = "S.E. Hinton" }
            };

            if (Members != null) return;
            Members = new ObservableCollection<Member>
            {
                new Teacher { ID="0A", Name="Gina Candler", Items=new ObservableCollection<Data.Item.IssuableItem> { getItemForID("010000").Issue(), getItemForID("080000").Reserve() } },
                new Teacher { ID="0B", Name="Kirsten Legac", Items=new ObservableCollection<Data.Item.IssuableItem> { getItemForID("020000").Reserve(), getItemForID("040000").Issue(), getItemForID("050000").Issue() } },
                new Teacher { ID="0C", Name="Jeffery Stetzler" },
                new Teacher { ID="0D", Name="David Kelman" },
                new Student { ID="00", Name="Joshua Lagria"},
                new Student { ID="01", Name="Kendall Tate", Items=new ObservableCollection<Data.Item.IssuableItem> { getItemForID("060000").Issue(), getItemForID("070000").Reserve() } },
                new Student { ID="02", Name="Josh Park"},
                new Student { ID="03", Name="Caleb Fox", Items=new ObservableCollection<Data.Item.IssuableItem> { getItemForID("090000").Issue()}}
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