using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Settings;
using Xamarin.Forms;

namespace MSNew.Droid
{
    [BroadcastReceiver(Enabled = true)]
    public class BarcodeReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {

            String action = intent.Action;
            string barcodeString = CrossSettings.Current.GetValueOrDefault("BarcodeString", "");
            string barcodeEvent = CrossSettings.Current.GetValueOrDefault("BarcodeEvent", "");
            if (barcodeString == "" || barcodeEvent == "")
            {
                MessagingCenter.Send<string, string>("MainActivity", "ErrorSetting", "Нет настроек сканера");
            }
            else if (action.Equals(barcodeEvent))
            {
                string q = intent.GetStringExtra(barcodeString);
                MessagingCenter.Send<string, string>("MainActivity", "GetBarcode", q);

            }

        }
    }
}