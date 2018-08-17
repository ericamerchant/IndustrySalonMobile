using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Content;
using Android;
using Java.Lang;
using Android.Content;
using Android.Util;
using System.Collections.Generic;
using System.IO;
using SQLite;

namespace MobileApp.Droid
{
    [Activity(Label = "Industry Salon Seattle", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        SMSReceiver smsReceiver;
        IntentFilter filter;
        
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            smsReceiver = new SMSReceiver();
            filter = new IntentFilter("android.provider.Telephony.SMS_RECEIVED");
            RegisterReceiver(smsReceiver, filter);

            List<string> possibleMsgs = smsReceiver.confirmMsgs;
            Log.Debug("MessageRead", possibleMsgs[0]);

            /*
             * store dates for appointments confirmed via text
             * can we scrape data from the link in the confirmation text to know what their
             * appointment specifically is so they can see if they have a cut/color/whatever 
             * 
             * just needed something quick to get some functionality working
             * 
             * TO DO:
             * add date, time, appointment type, potentially if it was confirmed to db instead
            */

            var apptmntTimes = new Dictionary<string, string>();
            var numApptmt = 1;
            Console.WriteLine("Adding msgs to database");

            //may need to be using Android.OS.Environment???
            //basically makes new db if not doesn't already exist
            var db = new SQLiteConnection(
                  Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "SalonSQLite.db3"));

            for (var i = 0; i < possibleMsgs.Count; i++)
            {
                Console.WriteLine(possibleMsgs[i]);
                var beginTrim = possibleMsgs[i].IndexOf("Industry Salon Seattle");
                var endTrim = possibleMsgs[i].IndexOf("PDT");
                Appointment theApptmnt = new Appointment();
                var temp = possibleMsgs[i].Substring(beginTrim + 27);
                var theDay = temp.Substring(0, temp.IndexOf("at"));
                var theTime = temp.Substring(temp.IndexOf("at") + 4, temp.IndexOf("PDT"));
                theApptmnt.Day = theDay;
                theApptmnt.Time = theTime;
                theApptmnt.Service = "Standard Trim";
                
                db.Insert(theApptmnt);
                
                /*
                var theTime = possibleMsgs[i].Substring(beginTrim, endTrim);
                apptmntTimes.Add("" + numApptmt, theTime);
                numApptmt++;
                */

            }

            dbTester(db);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public void dbTester(SQLiteConnection db)
        {
            Appointment testApptmnt = new Appointment();
            testApptmnt.Day = "Feb";
            testApptmnt.Time = "23";
            testApptmnt.Service = "Balayage";
            db.Insert(testApptmnt);

            var sample = from s in db.Table<Appointment>()
                         where s.Service.StartsWith("Balayage")
                         select s;

            Console.WriteLine(sample.FirstOrDefault().Service);
        }

        protected override void OnResume()
        {
            base.OnResume();
            try
            {
                // Registering the broadcast receiver to detect sms from local phone when activity restarts
                //potentially remove the null check
                if (null == this.smsReceiver)
                {
                    smsReceiver = new SMSReceiver();
                    this.RegisterReceiver(this.smsReceiver, new IntentFilter("android.provider.Telephony.SMS_RECEIVED"));
                }


            }
            catch (System.Exception ex)
            {
                Log.Debug("App2", ex.Message);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            UnregisterReceiver(smsReceiver);
        }


    }
    
}

