using LibraryManager.Data;
using LibraryManager.Data.Item;
using LibraryManager.Data.Member;
using LibraryManager.ViewModels;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibraryManager.Validation
{
    /// <summary>
    /// Validation rule that prevents duplicate ID's in collections for items that implement ILibraryObject and fulfill its contract
    /// </summary>
    /// <typeparam name="T">The target object type to perform comparisons against</typeparam>
    public class UniqueIDRule<T> : ValidationRule where T : LibraryObject
    {
        public CollectionViewSource CurrentCollection { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                if(CurrentCollection == null)
                {
                    // In direct comparison mode
                    string proposedID = (string)value;
                    return MainWindowViewModel.Instance.ItemsVM.ContainsID(proposedID) ? new ValidationResult(false, "Duplicate ID!") : new ValidationResult(true, null);
                }

                ObservableCollection<T> castedCollection = (ObservableCollection<T>)CurrentCollection.Source;

                T curValue = (T)((BindingExpression)value).DataItem;

                // Ensure uniqueness of curValue's ID in comparison to all other rows' values
                foreach (T rowValue in castedCollection)
                {
                    if (curValue.GetHashCode() != rowValue.GetHashCode() && curValue.ID == rowValue.ID)
                    {
                        return new ValidationResult(false, "Duplicate ID!");
                    }
                }
            }

            return new ValidationResult(true, null);
        }
    }

    // Default implementations
    public class MemberUniqueIDRule : UniqueIDRule<Member> { }
    public class ItemUniqueIDRule : UniqueIDRule<IssuableItem> { }
}
