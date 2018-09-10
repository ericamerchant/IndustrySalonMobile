using Android.Widget;
//using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewAppointments : ContentPage
	{
        public ViewAppointments()
        {
            //Label seeMyAppointments = new Label();
            //seeMyAppointments.Style = "{StaticResource eachAppointmentStyle}";
            //maybe use a listView
            Debug.WriteLine("Are we in view appointments yet");
			InitializeComponent ();
            Debug.WriteLine("Are we in view appointments yet 2");

        }
        /*
        async void dbTester()
        {
            var db = new AppointmentDB(
                  Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "SalonSQLite.db3"));
            Appointment testApptmnt = new Appointment();
            testApptmnt.Day = "Feb";
            testApptmnt.Time = "23";
            testApptmnt.Service = "Balayage";
            //testApptmnt.Id = 1;
            await App.Database.SaveAppointmentAsync(testApptmnt);

            
            var sample = from s in db.Table<Appointment>()
                         where s.Service.StartsWith("Balayage")
                         select s;
            

            

            Console.WriteLine(sample.FirstOrDefault().Service);

            return db.GetService(sample);
        }*/
    }
}