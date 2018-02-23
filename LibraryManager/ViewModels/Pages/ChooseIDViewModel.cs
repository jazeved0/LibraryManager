using System;

namespace LibraryManager.ViewModels.Pages
{
    public class ChooseIDViewModel : NotifyPropertyChanged, IPageViewModel
    {
        private String _id = "";
        private bool _hasError;

        public String ID
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
                ForcePropertyChanged("CanProceed");
            }
        }
        
        public bool CanProceed => !HasError && !ID.Equals("");

        public string Title => "Choose ID";

        public bool HasError {
            get { return _hasError; }
            set
            {
                if (_hasError == value) return;
                _hasError = value;
                ForcePropertyChanged("CanProceed");
            }
        }
    }
}
