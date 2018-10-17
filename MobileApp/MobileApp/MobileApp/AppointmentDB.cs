using System;
using System.Collections.Generic;
using SQLite;

namespace MobileApp
{
    public class AppointmentDB
    {
        
        //readonly in front???
        SQLiteConnection database;

        public AppointmentDB(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            database.CreateTable<Appointment>();
        }
        

        public void SaveAppointment(Appointment confirmed)
        {
            if (confirmed.Id != 0)
            {
                database.Update(confirmed);
            }
            else
            {
                database.Insert(confirmed);
            }
        }

        public void DeleteAppointment(Appointment appointment)
        {
            database.Delete(appointment);
        }

        public Appointment GetAppointment(int id)
        {
            return database.Get<Appointment>(id);
        }

        public List<Appointment> GetAllAppointments()
        {
            List < Appointment > list = new List<Appointment>();
            var table = database.Table<Appointment>();
            foreach( var s in table)
            {
                list.Add(s);
            }
            return list;
        }

        public List<Appointment> GetAppointmentSameDay(String day)
        {
            List<Appointment> list = new List<Appointment>();

            var things = from s in database.Table < Appointment >()
                         where s.Day.StartsWith(day)
                         select s;
            foreach(var elem in things)
            {
                list.Add(elem);
            }

            return list;
        }

        public void DeleteAllAppointments()
        {
            List<Appointment> list = GetAllAppointments();
            for(int i = 0; i < list.Count; i++)
            {
                database.Delete(list[i]);
            }
        }

        //set up a method to access all appointments whose date is beyond the current date
        //then delete from db those dates so they aren't unnecessarily stored (or keep? if we can 
        //get more info on if we can know what stylist they've seen or something then maybe)
        //maybe that's a separate section for you to input your own info to keep track
        
    }
}
