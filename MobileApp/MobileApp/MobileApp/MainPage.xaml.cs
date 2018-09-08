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
            InitializeComponent();
        }

        public async void onMyAppointmentsClicked(object sender, EventArgs e)
        {
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
