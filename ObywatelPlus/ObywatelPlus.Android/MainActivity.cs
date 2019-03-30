using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Plugin.Media;
using Plugin.Permissions;

namespace ObywatelPlus.Droid
{
    [Activity(Label = "ObywatelPlus", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar; 

            base.OnCreate(bundle);

            await CrossMedia.Current.Initialize();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            RejestracjaChecker.RejestracjaOznaczona += RejestracjaChecker_RejestracjaOznaczona;
        }

        private void RejestracjaChecker_RejestracjaOznaczona(string rejestracja)
        {
            RunOnUiThread(() =>
            {
                Toast.MakeText(ApplicationContext, "U", ToastLength.Short).Show();
            });
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

