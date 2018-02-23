using LibraryManager.Data.Item;
using LibraryManager.Data.Member;
using LibraryManager.ViewModels.Pages;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManager.ViewModels.Dialogs
{
    class IssueItemDialogViewModel : DialogViewModelBase
    {
        public override string FinalActionText => "Issue";

        protected override void AddPages()
        {
            PageViewModels.Add(new SelectMemberViewModel(x => x.IssuanceCount < App.Instance.Config.DiscreteValues[x.Type]["issuance_max"].CurrentValue));
            PageViewModels.Add(new SelectItemViewModel(x => !x.Status.HasOwner));
        }

        protected override void TransitionTo(IPageViewModel newViewModel)
        {
            if(newViewModel is SelectItemViewModel)
            {
                // Add reserved items to the view
                (newViewModel as SelectItemViewModel).AdditionalDisplayItems.Clear();
                IEnumerable<IssuableItem> reservedItems = (PageViewModels[0] as SelectMemberViewModel).SelectedMember.Items.Where(x => x.Status.Type == Data.Item.Status.ItemStatus.StatusType.Reserved);
                foreach(IssuableItem reservedItem in reservedItems) (newViewModel as SelectItemViewModel).AdditionalDisplayItems.Add(reservedItem);
            }
        }

        protected override void Complete()
        {
            IssuableItem item = ((SelectItemViewModel)PageViewModels[1]).SelectedItem;
            Member member = ((SelectMemberViewModel)PageViewModels[0]).SelectedMember;
            item.Status.Issue(member);
        }
    }
}