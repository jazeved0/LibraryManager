using LibraryManager.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace LibraryManager.ViewModels.Dialogs
{
    abstract class DialogViewModelBase : NotifyPropertyChanged, ICloseable
    {
        #region Fields

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        public event EventHandler<EventArgs> RequestClose;
        public ICommand CloseCommand { get; private set; }

        #endregion

        public DialogViewModelBase()
        {
            // Add available pages
            AddPages();

            // Set starting page
            if (PageViewModels.Count == 0) throw new InvalidOperationException("0 Pages Registered");
            CurrentPageViewModel = PageViewModels[0];
        }

        #region Properties / Commands

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                    OnPropertyChanged("CanGoBack");
                    OnPropertyChanged("ProceedText");
                }
            }
        }

        public bool CanGoBack
        {
            get
            {
                return _pageViewModels.IndexOf(_currentPageViewModel) != 0;
            }
        }

        public String ProceedText
        {
            get
            {
                return _pageViewModels.IndexOf(_currentPageViewModel) + 1 == _pageViewModels.Count ? FinalActionText : "Next";
            }
        }

        public abstract String FinalActionText { get; }

        #endregion

        #region Methods

        protected abstract void AddPages();
        protected abstract void Complete();

        protected virtual void TransitionTo(IPageViewModel newViewModel)
        {
            // Do nothing
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        public void GoBack()
        {
            if (CanGoBack)
            {
                IPageViewModel newViewModel = _pageViewModels[_pageViewModels.IndexOf(CurrentPageViewModel) - 1];
                TransitionTo(newViewModel);
                ChangeViewModel(newViewModel);
            }
        }

        public void Proceed()
        {
            if (_pageViewModels.IndexOf(_currentPageViewModel) + 1 != _pageViewModels.Count)
            {
                IPageViewModel newViewModel = _pageViewModels[_pageViewModels.IndexOf(CurrentPageViewModel) + 1];
                TransitionTo(newViewModel);
                ChangeViewModel(newViewModel);
            }
            else
            {
                Complete();
                RequestClose?.Invoke(this, new EventArgs());
            }
        }

        #endregion
    }
}
