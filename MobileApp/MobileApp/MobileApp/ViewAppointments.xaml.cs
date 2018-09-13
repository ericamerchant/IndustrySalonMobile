using Android.Widget;
using SQLite;
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
        AppointmentDB database = App.Database;
        List<Appointment> currAppointmentsList;
        public ViewAppointments()
        {
            Debug.WriteLine("Are we in view appointments yet");
			InitializeComponent ();

            GetAllAppointments();
            RemoveOutdatedAppointments();

            // generates a single Label for when no current appointments found
            if (currAppointmentsList.Count == 0)
            {
                Label noAppointments = new Label();
                noAppointments.Style = this.Resources["eachAppointmentStyle"] as Style;
                noAppointments.Text = "You currently do not have any appointments scheduled.";
            }

            // goes through each Appointment in list, creates a label, and sets to explicit style from xaml
            else
            {
                foreach (var s in currAppointmentsList)
                {
                    Label seeAppointment = new Label();
                    seeAppointment.Style = this.Resources["eachAppointmentStyle"] as Style;
                    seeAppointment.Text = "You have an appointment at Industry Salon scheduled for " + s.Day + " at " + s.Time + ".";
                }
            }
            // maybe use a listView


        }

        // helper method to get a list of all Appointments from database through waiting for Task to return
        async void GetAllAppointments()
        {
            currAppointmentsList = await database.GetAllAppointmentsAsync();
        }

        // removes any appointments from the database if scheduled for a date that has already passed
        public void RemoveOutdatedAppointments()
        { 
            DateTime currDate = DateTime.Now;
            List<Appointment> AppointmentToRemove = new List<Appointment>();

            foreach(var s in currAppointmentsList)
            {
                // takes String Date information from db and converts to integer form of month and day
                String[] monthAndDay = s.Day.Split('/');
                int theMonth = Convert.ToInt32(monthAndDay[0]);
                int theDay = Convert.ToInt32(monthAndDay[1]);

                // sees if the appointment date happened before the current date, if so adds to removal list
                    if (theMonth == currDate.Month)
                    {
                        if (theDay < currDate.Day)
                            AppointmentToRemove.Add(s);
                        else if (theMonth < currDate.Month)
                            AppointmentToRemove.Add(s);
                    }
                    
            }

            // removes necessary dates from db - could potentially just screen this as the messages come in
            for(int i = 0; i < AppointmentToRemove.Count; i++)
            {
                DeleteAppointments(AppointmentToRemove[i]);
            }
        }

        // helper method to delete appointments by waiting for async Task to finish/return
        async void DeleteAppointments(Appointment toDelete)
        {
            await database.DeleteAppointmointmentAsync(toDelete);
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