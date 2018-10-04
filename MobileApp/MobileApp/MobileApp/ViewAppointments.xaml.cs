using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewAppointments : ContentPage
	{
        AppointmentDB database = App.Database;
        List<Appointment> currAppointmentsList = new List<Appointment>();
        public ViewAppointments()
        {
			InitializeComponent();

            dbTest();
            currAppointmentsList = database.GetAllAppointments();
            RemoveOutdatedAppointments();

            String temp = "";
            
            if(currAppointmentsList.Count > 0)
            {
                foreach (var s in currAppointmentsList)
                {
                    if (s.Day.Equals("-1"))
                        throw new Exception("Creation of appointment doesn't work - date not formatted");
                    temp += "You have an appointment scheduled for " + s.Day + " " + s.Time + "\n";
                }
            }
            else
            {
                temp = "You currently do not have any appointments scheduled.";
            }

            seeAppointmentBox.Text = temp;              
        }

        // removes any appointments from the database if scheduled for a date that has already passed
        public void RemoveOutdatedAppointments()
        { 
            DateTime currDate = DateTime.Now;
            List<Appointment> AppointmentToRemove = new List<Appointment>();

            foreach(var s in currAppointmentsList)
            {
                // throws exception if date not stored in "month/day" form
                if (s.Day == null || !s.Day.Contains("/"))
                    throw new Exception("Date not properly formatted for removal of appointment");

                // takes String Date information from db and converts to integer form of month and day
                String[] monthAndDay = s.Day.Split('/');
                int theMonth = Convert.ToInt32(monthAndDay[0]);
                int theDay = Convert.ToInt32(monthAndDay[1]);

                // sees if the appointment date happened before the current date, if so adds to removal list
                    if (theMonth == currDate.Month)
                    {
                        if (theDay < currDate.Day)
                            AppointmentToRemove.Add(s);
                        
                    }
                    else if (theMonth < currDate.Month)
                        AppointmentToRemove.Add(s);

            }

            // removes necessary dates from db - could potentially just screen this as the messages come in
            for(int i = 0; i < AppointmentToRemove.Count; i++)
            {
                database.DeleteAppointment(AppointmentToRemove[i]);
            }
        }

        public void dbTest()
        {
            Appointment testApptmnt = new Appointment();
            testApptmnt.Day = "Feb";
            testApptmnt.Time = "23";
            testApptmnt.Service = "TestingServices";
            database.SaveAppointment(testApptmnt);

            //temp += database.GetAppointment(testApptmnt.Id).Service;

            //seeAppointmentBox.Text = temp;
        }

        /*
         * Trying something to see if I could programmatically create separate labels for each appointment.
                if (currAppointmentsList != null)
                {
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
                            if (seeAppointment.Text != null)
                                seeAppointment.Text = "You have an appointment at Industry Salon scheduled for " + s.Day + " at " + s.Time + ".";
                        }
                    }
                }

                else
                {
                    Debug.WriteLine("Database access is given as null");
                    Label noAppointments = new Label();
                    noAppointments.Style = this.Resources["eachAppointmentStyle"] as Style;
                    noAppointments.Text = "You currently do not have any appointments scheduled.";
                }
            } */
    }
}
 