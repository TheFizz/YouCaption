using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using System;

namespace YouCaptionBase.Droid
{
    [Activity(Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static NotificationHelper ntfHelper;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ntfHelper = new NotificationHelper(this, (NotificationManager)GetSystemService(NotificationService));
            LoadApplication(new App());
        }

        protected override void OnDestroy()
        {
            ntfHelper?.Manager?.CancelAll();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public static void CreateProgressNtf(string title, string text)
        {
            ntfHelper.ProgressBuilder
                  .SetOngoing(true)
                  .SetAutoCancel(false) // Dismiss the notification from the notification area when the user clicks on it
                  .SetContentTitle(title) // Set the title
                  .SetSmallIcon(Resource.Drawable.icon_download) // This is the icon to display
                  .SetContentText(text)
                  .SetProgress(100, 0, false)
                  .SetContentIntent(ntfHelper.PendingIntent)
                  .SetOnlyAlertOnce(true);

            ntfHelper.NotifyProgress();
        }
        public static void UpdateProgressNtf(string text, int max, int cur)
        {
            ntfHelper.ProgressBuilder
                .SetProgress(max, cur, false)
                .SetContentText(text);

            ntfHelper.NotifyProgress();
        }
        public static void ShowFinishNtf(string title, string text)
        {
            ntfHelper.Manager.CancelAll();
            ntfHelper.OneshotBuilder
                  .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                  .SetContentTitle(title) // Set the title
                  .SetSmallIcon(Resource.Drawable.icon_download) // This is the icon to display
                  .SetContentText(text)
                  .SetContentIntent(ntfHelper.PendingIntent);

            ntfHelper.NotifyOneshot();
        }
    }
}