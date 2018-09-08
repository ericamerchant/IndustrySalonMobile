using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace MobileApp.Droid
{
    [BroadcastReceiver]
    public class SMSReceiver : BroadcastReceiver
    {
        private const string IntentAction = "android.provider.Telephony.SMS_RECEIVED";
        string version_code = Build.VERSION.Sdk;
        public List<String> confirmMsgs = new List<String>();
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
            try
            {
                if (intent.Action != IntentAction)
                {
                    return;
                }
                var bundle = intent.Extras;
                if (bundle == null)
                {
                    return;
                }
                var pdus = bundle.Get("pdus");
                var castedPdus = JNIEnv.GetArray<Java.Lang.Object>(pdus.Handle);
                var msgs = new SmsMessage[castedPdus.Length];
                var theMessage = "";
                for (var i = 0; i < msgs.Length; i++)
                {
                    var bytes = new byte[JNIEnv.GetArrayLength(castedPdus[i].Handle)];
                    JNIEnv.CopyArray(castedPdus[i].Handle, bytes);

                    //likely should do add if/else of if SDK > Marshmallow, else use deprecated form
                    string format = bundle.GetString("format");
                    msgs[i] = SmsMessage.CreateFromPdu(bytes, format);

                    //check for if the message contains confirmation info - could also look at senderInfo
                    var sender = msgs[i].OriginatingAddress;
                    theMessage = msgs[i].DisplayMessageBody;
                    if (theMessage.ToUpper().StartsWith("Hi ") && sender.Equals("(206) 755-4942"))
                    {
                        confirmMsgs.Add(theMessage);
                        Toast.MakeText(context, "Received SMS: " + theMessage, ToastLength.Long).Show();

                    }

                    /*
                    if (null != msgs[i].DisplayMessageBody && msgs[i].DisplayMessageBody.ToUpper().StartsWith("SMS from source"))
                    {
                        string verificationCode = msgs[i].DisplayMessageBody.Split(':')[1].Split('.')[0];
                        Intent otpIntent = new Intent(Application.Context, typeof(your activity to validate sms));

                        otpIntent.PutExtra("verificationCode", verificationCode.Trim());
                        otpIntent.PutExtra("fromsms", "OK");
                        otpIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.SingleTop);
                        context.StartActivity(otpIntent);
                    } */
                }
            }

            catch (Exception ex)
            {
                Log.Error("SMS", ex.Message);
            }
        }
        }
}