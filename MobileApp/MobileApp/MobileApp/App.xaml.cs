using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MobileApp
{
	public partial class App : Application
	{
        static ApptmntDatabase database;

		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MobileApp.MainPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        //creates a new db, otherwise accesses come from existing
        public static ApptmntDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ApptmntDatabase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SalonSQLite.db3"));
                }
                return database;
            }
        }

        
    }
}
