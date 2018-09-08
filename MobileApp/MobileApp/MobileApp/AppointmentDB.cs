using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MobileApp
{
    class AppointmentDB
    {
        //readonly in front???
        SQLiteAsyncConnection database;


        public AppointmentDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Appointment>().Wait();
        }
        

        public Task<int> SaveApptmntAsync(Appointment confirmed)
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

        public Task<int> DeleteApptmntAsync(Appointment apptmnt)
        {
            return database.DeleteAsync(apptmnt);
        }

        public Task<Appointment> GetApptmntAsync(int id)
        {
            return database.Table<Appointment>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Appointment>> GetAllApptmntsAsync()
        {
            return database.Table<Appointment>().ToListAsync();
        }

        //set up a method to access all appointments whose date is beyond the current date
        //then delete from db those dates so they aren't unnecessarily stored (or keep? if we can 
        //get more info on if we can know what stylist they've seen or something then maybe)
        //maybe that's a separate section for you to input your own info to keep track
    }
}
