using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Media;
using Plugin.Permissions;
using Android.Support.V7.App;
using Android.Support.V4.App;

namespace ObywatelPlus.Droid
{
    [Activity(Label = "ObywatelPlus", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

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
                CreateNotification();
            });
        }


        void CreateNotification()
        {
            // Instantiate the builder and set notification elements:
            var builder = new NotificationCompat.Builder(this)
                .SetContentTitle("Sample Notification")
                .SetContentText("Hello World! This is my first notification!")
                .SetSmallIcon(Resource.Drawable.car)
                .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis());

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                GetSystemService(NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 1;
            notificationManager.Notify(notificationId, notification);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            //PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

