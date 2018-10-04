using System;

using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Diagnostics;

namespace MobileApp
{
    // View models can be used regardless of whether the UI is built in code or with Xaml.
    // Here, the view model is referenced by a Xaml page, but the same bindings can be done in C#.
    public class TapViewModel : INotifyPropertyChanged
    {
        ICommand tapCommand;
        ICommand tapCommand2;
        ICommand tapCommand3;
        ICommand tapCommand4;

        public TapViewModel ()
        {
            tapCommand = new Command(OnTapped);
            tapCommand2 = new Command(OnTapped2);
            tapCommand3 = new Command(OnTapped3);
            tapCommand4 = new Command(OnTapped4);
        }

        //Expose the TapCommand via a property so that Xaml can bind to it
        public ICommand TapCommand
        {
            get { return tapCommand; }
        }
        public ICommand TapCommand2
        {
            get { return tapCommand2; }
        }

        public ICommand TapCommand3
        {
            get { return tapCommand3; }
        }

        public ICommand TapCommand4
        {
            get { return tapCommand4; }
        }

        //Called whenever TapCommand is executed (because it was wired up in the constructor)
        void OnTapped (object s)
        {
            Debug.WriteLine("parameter: " + s);
            Device.OpenUri(new Uri("http://www.instagram.com/industrysalonseattle"));
        }
        void OnTapped2 (object s)
        {
            Debug.WriteLine("parameter: " + s);
            Device.OpenUri(new Uri("http://www.yelp.com"));
        }

        // pulls up keyboard with phone number auto filled in
        void OnTapped3 (object s)
        {
            //check this  is the proper format for the phoneNum
            Device.OpenUri(new Uri("tel:206588-05-70"));
        }

        // opens up email ready to compose to Industry Salon
        void OnTapped4(object s)
        {
            Device.OpenUri(new Uri("mailto:industrysalonseattle@gmail.com"));
        }


        #region INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        //Source from github.com/xamarin/xamarin-forms-samples/tree/master/WorkingWithGestures/TapGesture/WorkingWithGestures
        //docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/gestures/tap
    }
}
