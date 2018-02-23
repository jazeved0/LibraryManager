using LibraryManager.Data.Item;
using LibraryManager.ViewModels.Pages;

namespace LibraryManager.ViewModels.Dialogs
{
    class ReturnItemDialogViewModel : DialogViewModelBase
    {
        public override string FinalActionText => "Return";

        protected override void AddPages()
        {
            PageViewModels.Add(new SelectItemViewModel(x => x.Status.Type == Data.Item.Status.ItemStatus.StatusType.Issued));
        }

        protected override void Complete()
        {
            IssuableItem item = ((SelectItemViewModel)PageViewModels[0]).SelectedItem;
            item.Status.CheckIn();
        }
    }
}