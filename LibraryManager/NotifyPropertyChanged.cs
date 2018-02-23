using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibraryManager
{
    /// <summary>
    /// Abstract base class that implements common features of INotifyPropertyChanged that are used through out LibraryManager.Data as well as LibraryManager.ViewModel
    /// </summary>
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// An event that is called each time a property (that calls OnPropoertyChanged()) in the sub-class is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Generic call of PropertyChanged that allows non-property code to force updates of Bindings to the specied property (used especially with GetX() equivalent Properties)
        /// </summary>
        /// <param name="propertyName"></param> The name of the property to pass on to the event's listeners
        public void ForcePropertyChanged(string propertyName = null)
        {
            // If handler(s) exist(s) for the event, call them with the specified property name
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        /// <summary>
        /// Helper method that delegates calls to ForcePropertyChanged() and resolves the name if its caller member (must be called from the Property that is being changed)
        /// </summary>
        /// <param name="propertyName"></param> Optional parameter that gets filled with the resolved name of the caller member
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            ForcePropertyChanged(propertyName);
        }

        public bool HasHandlers()
        {
            return PropertyChanged == null ? false : true;
        }
    }
}
