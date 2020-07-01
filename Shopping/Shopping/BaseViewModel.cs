using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shopping
{
    /// <summary>
    /// Don't build your own framework, you will probably regret it. But here's a good start if you do.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (oldValue == null ||  !oldValue.Equals(newValue))
            {
                oldValue = newValue;
                RaisePropertyChanged(propertyName);
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}