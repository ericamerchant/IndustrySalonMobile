using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Debug.Write("Maybe app is starting");
            InitializeComponent();
            Debug.Write("Is anything being sent to output");
        }

        public async void onMyAppointmentsClicked(object sender, EventArgs e)
        {
            Debug.Write("Clicked my appointments");
            await Navigation.PushAsync(new ViewAppointments());
        }

        public async void onNewAppointmentClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewAppointment());
        }

        public async void onContactUsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactSalon());
        }
    }
}
