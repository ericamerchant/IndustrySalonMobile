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
            
            //RegisterReceiver(smsReceiver, filter);
            Debug.WriteLine("!!!!!!!!!!!!Receiver Registered!!!!!!!!!!!!!!!!!!!!!!");
            //smsReceiver.OnReceive(this.ApplicationContext, this.Intent);

            List<string> possibleMsgs = readInbox();

            if(possibleMsgs != null)
            {
                //loops through each SMS message body to gain date and time info for each appointment
                foreach (var s in possibleMsgs)
                {
                    String date = "";
                    String time = "";

                    if (s.IndexOf("on") > 0)
                    {
                        var dateTemp = s.Substring(s.IndexOf("on") + 3);
                        date = dateTemp.Substring(0, dateTemp.IndexOf(" "));

                        if (dateTemp.IndexOf(" ") > 0)
                        {
                            var timeTemp = dateTemp.Substring(dateTemp.IndexOf(" ") + 1);
                            time = timeTemp.Substring(0, timeTemp.IndexOf("has") - 1);
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
                    upcomingAppointment.Service = s;
                    upcomingAppointment.IsConfirmed = true;

                    //check if already in the db, if not save to db, if so do nothing
                    List<Appointment> alreadyInDB = db.GetAppointmentSameDay(upcomingAppointment.Day);
                    if(alreadyInDB.Count > 0)
                    {
                        foreach(var apptmnt in alreadyInDB)
                        {
                            if (!apptmnt.Time.Equals(upcomingAppointment.Time))
                                db.SaveAppointment(upcomingAppointment);
                        }
                    }
                    else if(alreadyInDB.Count == 0) 
                        db.SaveAppointment(upcomingAppointment);

                }
            }
            

            dbTester();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());


        }

        protected override void OnResume()
        {
            base.OnResume();
            /*
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
            } */
        }
        
        protected override void OnPause()
        {
            base.OnPause();
            //UnregisterReceiver(smsReceiver);
        } 

        /*
         * Reads all SMS messages from inbox and adds those that match the phone number (address) of the 
         * confirmation text for Industry Salon Appointments to a List.
         * 
        */
        public List<String> readInbox()
        {
            List<String> sms = new List<String>();

            Android.Net.Uri uriSms = Android.Net.Uri.Parse("content://sms/inbox");
            Android.Database.ICursor cursor = this.ContentResolver.Query(uriSms, new String[] { "_id", "address", "date", "body" }, null, null, null);

            if (cursor.MoveToFirst())
            {
                while (cursor.MoveToNext())
                {
                    String address = cursor.GetString(1);
                    String body = cursor.GetString(3);
                    //maybe add another check to see what the date is, idk if that would help remove some unnecessary ones
                    if (address.Equals("2244273491"))
                        sms.Add(body);

                    //sms.Add("Address=&gt; " + address + "n SMS =&gt; " + body);
                }
            }
            else
            {
                // no sms found
            }
            return sms;
        }     
        
        /*
         * Creates a sample Appointment and adds it to the db to make sure the connection is right
         * 
        */
        public void dbTester()
        {
            Console.WriteLine("Inside dbTester MainActivity");
            Appointment testApptmnt = new Appointment();
            testApptmnt.Day = "Feb";
            testApptmnt.Time = "23";
            testApptmnt.Service = "Balayage";
            db.SaveAppointment(testApptmnt);
        } 
    }
}

