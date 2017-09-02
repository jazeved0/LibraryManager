namespace LibraryManager.Data.Item
{
    class Book : IssuableItem
    {
        public new string GetType()
        {
            return "Book";
        }
    }
}