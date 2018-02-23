using LibraryManager.Data.Action;
using LibraryManager.Data.Item;
using LibraryManager.ViewModels.Pages;
using System;

namespace LibraryManager.ViewModels.Dialogs
{
    class DuplicateItemDialogViewModel : DialogViewModelBase
    {
        public override string FinalActionText => "Duplicate";

        protected override void AddPages()
        {
            PageViewModels.Add(new SelectItemViewModel(x => true));
            PageViewModels.Add(new ChooseIDViewModel());
        }

        protected override void Complete()
        {
            IssuableItem masterItem = (PageViewModels[0] as SelectItemViewModel).SelectedItem;
            string newID = (PageViewModels[1] as ChooseIDViewModel).ID;
            IssuableItem newItem = masterItem.Clone() as IssuableItem;
            newItem.ID = newID;
            MainWindowViewModel.Instance.ItemsVM.Items.Add(newItem);
            MainWindowViewModel.Instance.HistoryVM.History.Add(new LoggedAction { Timestamp = DateTime.Now, Member = null, Item = newItem, Type = ActionType.Addition });
        }

        protected override void TransitionTo(IPageViewModel newViewModel)
        {
            // Reset error state
            if (newViewModel is ChooseIDViewModel) (newViewModel as ChooseIDViewModel).HasError = false;
        }
    }
}
