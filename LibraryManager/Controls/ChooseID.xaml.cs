using LibraryManager.ViewModels.Pages;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManager.Controls
{
    /// <summary>
    /// Interaction logic for ChooseID.xaml
    /// </summary>
    public partial class ChooseID : UserControl
    {
        public ChooseID()
        {
            InitializeComponent();
            AddHandler(System.Windows.Controls.Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
        }
        
        private void OnErrorEvent(object sender, RoutedEventArgs e)
        {
            if (DataContext == null) return;
            var validationEventArgs = e as ValidationErrorEventArgs;
            if (validationEventArgs == null)
                throw new Exception("Unexpected event args");
            if (validationEventArgs.Action == ValidationErrorEventAction.Added) (DataContext as ChooseIDViewModel).HasError = true;
            else (DataContext as ChooseIDViewModel).HasError = false;
        }
    }
}
