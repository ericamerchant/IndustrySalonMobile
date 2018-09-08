using System.Diagnostics;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using Android.Util;
using System.Collections.Generic;

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

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            smsReceiver = new SMSReceiver();
            filter = new IntentFilter("android.provider.Telephony.SMS_RECEIVED");
            RegisterReceiver(smsReceiver, filter);

            List<string> possibleMsgs = smsReceiver.confirmMsgs;
            Log.Debug("MessageRead", possibleMsgs[0]);

            System.Diagnostics.Debug.WriteLine("Checking things out");

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

