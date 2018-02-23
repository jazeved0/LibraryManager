using System;
using System.IO;
using System.Text.RegularExpressions;

namespace LibraryManager.ViewModels.Pages
{
    class ChooseSaveLocationViewModel : NotifyPropertyChanged, IPageViewModel
    {
        private String _filePath = "";

        public String FilePath
        {
            get { return _filePath; }
            set
            {
                if (value == _filePath) return;
                _filePath = value;
                OnPropertyChanged();
                ForcePropertyChanged("CanProceed");
            }
        }

        public bool CanProceed
        {
            get
            {
                try
                {
                    String p = Path.GetFullPath(FilePath);
                    return IsValidFileName(FilePath.Substring(FilePath.LastIndexOf("\\") + 1), false);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public string Title => "Choose a Save Location";

        public static bool IsValidFileName(string expression, bool platformIndependent)
        {
            string sPattern = @"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\"";|/]+$";
            if (platformIndependent)
            {
                sPattern = @"^(([a-zA-Z]:|\\)\\)?(((\.)|(\.\.)|([^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?))\\)*[^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?$";
            }
            return (Regex.IsMatch(expression, sPattern, RegexOptions.CultureInvariant));
        }
    }
}
