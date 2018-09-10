using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using SQLite;

namespace MobileApp
{
    public class AppointmentDB
    {
        /*
        //readonly in front???
        SQLiteAsyncConnection database;


        public AppointmentDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Appointment>().Wait();
        }
        

        public Task<int> SaveAppointmentAsync(Appointment confirmed)
        {
            if (confirmed.Id != 0)
            {
                return database.UpdateAsync(confirmed);
            }
            else
            {
                return database.InsertAsync(confirmed);
            }
        }

        public Task<int> DeleteAppointmointmentAsync(Appointment appointment)
        {
            return database.DeleteAsync(appointment);
        }

        public Task<Appointment> GetAppointmentAsync(int id)
        {
            return database.Table<Appointment>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return database.Table<Appointment>().ToListAsync();
        }

        public String GetService(Appointment appointment)
        {
            var findService = from s in database.Table<Appointment>()
                         where s.Service.StartsWith("Balayage")
                         select s;
            //var findService = database.QueryAsync<Appointment>("Select )

            //var strFindService = (Appointment)BindingContext;
            //return findService.FirstOrDefaultAsync ().Service;
            return "idk";
        }

        //set up a method to access all appointments whose date is beyond the current date
        //then delete from db those dates so they aren't unnecessarily stored (or keep? if we can 
        //get more info on if we can know what stylist they've seen or something then maybe)
        //maybe that's a separate section for you to input your own info to keep track
        */
    }
}
