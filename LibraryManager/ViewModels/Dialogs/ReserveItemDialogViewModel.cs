using LibraryManager.Data.Item;
using LibraryManager.Data.Member;
using LibraryManager.ViewModels.Pages;

namespace LibraryManager.ViewModels.Dialogs
{
    class ReserveItemDialogViewModel : DialogViewModelBase
    {
        public override string FinalActionText => "Reserve";

        protected override void AddPages()
        {
            PageViewModels.Add(new SelectMemberViewModel(x => x.ReservationCount < App.Instance.Config.DiscreteValues[x.Type]["reservation_max"].CurrentValue));
            PageViewModels.Add(new SelectItemViewModel(x => !x.Status.HasOwner));
        }

        protected override void Complete()
        {
            IssuableItem item = ((SelectItemViewModel)PageViewModels[1]).SelectedItem;
            Member member = ((SelectMemberViewModel)PageViewModels[0]).SelectedMember;
            item.Status.Reserve(member);
        }
    }
}
