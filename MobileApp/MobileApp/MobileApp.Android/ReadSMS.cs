#if __ANDROID__
    using Android;
    using Android.App;
    using Android.Content;
    using System;
#endif
[assembly: Android.App.UsesPermission(Manifest.Permission.ReceiveSms)]

namespace MobileApp
{
    //Exported allows it to receive messages outside the application
    [BroadcastReceiver(Enabled = true, Exported = true)]
    //think this is statically registering it
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]
    public class ReadSMS : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            String value = intent.GetStringExtra("key");
        }
        /*
        public Boolean HasSMSPermission()
        {
            return ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadSms) == PackageManager.PERMISSION_GRANTED;

        }

        private void RequestReadAndSendSmsPermission()
        {
            if (ActivityCompat.shouldShowRequestPermissionRationale(this, Manifest.permission.READ_SMS))
            {
                // You may display a non-blocking explanation here, read more in the documentation:
                // https://developer.android.com/training/permissions/requesting.html
            }
            ActivityCompat.requestPermissions(this, new String[] { Manifest.permission.READ_SMS }, SMS_PERMISSION_CODE);
        }

        public void showRequestPermissionsInfoAlertDialog()
        {
            showRequestPermissionsInfoAlertDialog(true);
        }

        public void showRequestPermissionsInfoAlertDialog(Boolean makeSystemRequest)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("SMS Permission"); // Your own title
            builder.SetMessage("Industry Salon would like to request read SMS permission on your device." +
                " This is necessary to accurately show your current appointments."); // Your own message

            // R.string.action_ok meant to be an int ID to a string except there's no strings.xml file to reference
            builder.SetPositiveButton(R.string.action_ok, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which)
            {
                dialog.dismiss();
                // Display system runtime permission request?
                if (makeSystemRequest)
                {
                    RequestReadAndSendSmsPermission(this);
                }
            }

            builder.setCancelable(false);
            builder.show();
        }
        */

    }
}