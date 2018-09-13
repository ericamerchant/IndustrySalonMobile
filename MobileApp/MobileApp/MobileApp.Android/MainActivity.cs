using System.Diagnostics;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using Android.Util;
using System.Collections.Generic;
using SQLite;
using System;
using Debug = System.Diagnostics.Debug;
using Trace = System.Diagnostics.Trace;

namespace MobileApp.Droid
{
    [Activity(Label = "Industry Salon Seattle", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        SMSReceiver smsReceiver = new SMSReceiver();
        IntentFilter filter = new IntentFilter("android.provider.Telephony.SMS_RECEIVED");
        AppointmentDB db = App.Database;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            //smsReceiver = new SMSReceiver();
            //filter = new IntentFilter("android.provider.Telephony.SMS_RECEIVED");
            RegisterReceiver(smsReceiver, filter);

            smsReceiver.OnReceive(this.ApplicationContext, this.Intent);

            List<string> possibleMsgs = smsReceiver.confirmMsgs;
            if (possibleMsgs.Count > 0)
                Debug.Write("MessageRead " + possibleMsgs[0]);
            else
                Debug.Write("No messages pulled");

            //loops through each SMS message body to gain date and time info for each appointment
            foreach(var s in possibleMsgs)
            {
                String date;
                String time;
                if(s.IndexOf("on") < 0)
                {
                    var dateTemp = s.Substring(s.IndexOf("on") + 3);
                    date = dateTemp.Substring(0, dateTemp.IndexOf(" "));

                    if (dateTemp.IndexOf(" ") < 0)
                    {
                        var timeTemp = dateTemp.Substring(dateTemp.IndexOf(" ") + 1);
                        time = timeTemp.Substring(0, timeTemp.IndexOf("is") - 1);
                    }
                    else
                        time = "-1";
                }
                else
                {
                    date = "-1";
                    time = "-1";
                }

                //adds the upcoming appointment to the database
                Appointment upcomingAppointment = new Appointment();
                upcomingAppointment.Day = date;
                upcomingAppointment.Time = time;
                upcomingAppointment.Service = "standard";
                upcomingAppointment.IsConfirmed = true;
                addAppointment(upcomingAppointment);
                
            }

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());


        }

        //waits for appointment to be added to database
        async void addAppointment(Appointment a)
        {
            await db.SaveAppointmentAsync(a);
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

        /*
        public List<String> readInbox()
        {
            List<String> sms = new List<String>();

            Android.Net.Uri uriSms = Android.Net.Uri.Parse("content://sms/inbox");
            ICursor cursor = this.ContentResolver.Query(uriSms, new String[] { "_id", "address", "date", "body" }, null, null, null);

            cursor.moveToFirst();
            while (cursor.moveToNext())
            {
                String address = cursor.getString(1);
                String body = cursor.getString(3);

                sms.Add("Address=&gt; " + address + "n SMS =&gt; " + body);
            }
            return sms;

        }
        */

        /*
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
        } */
    }
}

