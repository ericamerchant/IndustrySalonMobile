/*
 * Creates the SQL db for storing appointment info
 * */

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp
{
    public class ApptmntDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ApptmntDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Appointment>().Wait();
        }
    }
}
