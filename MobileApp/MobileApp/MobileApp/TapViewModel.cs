using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace MobileApp
{
    // View models can be used regardless of whether the UI is built in code or with Xaml.
    // Here, the view model is referenced by a Xaml page, but the same bindings can be done in C#.
    public class TapViewModel : INotifyPropertyChanged
    {
        ICommand tapCommand;
        public TapViewModel ()
        {
            tapCommand = new Command(OnTapped);
        }

        //Expose the TapCommand via a property so that Xaml can bind to it
        public ICommand TapCommand
        {
            get { return tapCommand; }
        }

        //Called whenever TapCommand is executed (because it was wired up in the constructor)
        void OnTapped ()
        {
            Device.OpenUri(new Uri("http://www.instagram.com/industrysalonseattle"));
        }

        #region INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
